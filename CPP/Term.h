#ifndef _term_h
#define _term_h

#include <windows.h>

#include "Logger.h"
#include "TextDisplay.h"
#include "Keys.h"
#include "MappedKeyListener.h"
#include "Mode.h"
#include "Guardian.h"
#include "Telnet.h"
#include "TelnetStateListener.h"
#include "PaintSurface.h"
#include "PaintPeer.h"
#include "Vector.h"
#include "TermEventListener.h"

DWORD WINAPI TermProc(LPVOID lpParameter);


class Term : public MappedKeyListener, public TelnetStateListener, public PaintPeer
{	
private:
	HANDLE hEnqLock;
	HANDLE h34Lock;

	HANDLE termThread;
	DWORD threadID;

	PaintSurface *container;
	TextDisplay *display;
	PaintSurface *nullDisplay;

	Keys *keys;
	Telnet *telnet;
	
	Mode *currentMode;
	
	bool connected;
	StringBuffer host;
	int port;
		
	Vector<TermEventListener> listeners;
	StringBuffer receiveBuf;

public:

	Term();
	~Term();

	void keydown(int key, bool shift, bool ctrl, bool alt)
	{
		keys->keyReleased(key, shift, ctrl, alt);
	}

	void keypress(int key, bool shift, bool ctrl, bool alt)
	{
		keys->keyTyped(key, shift, ctrl, alt);
	}

	bool waitENQ();

	bool wait34();

	void setDisplay(PaintSurface *ps)
	{
		_ASSERT(_CrtIsValidHeapPointer(ps));

		ps->setPaintPeer(this);
		//ps->registerKeyListener(keys);
		container = ps;
	}
	
	void setHost(char *host)
	{
		this->host = host;
		if (connected)
		{
			close();
			connect();
		}
	}
	
	char *getHost()
	{
		return host;
	}
	
	void setPort(int port)
	{
		this->port = port;
		if (connected)
		{
			close();
			connect();
		}
	}
	
	int getPort()
	{
		return port;
	}
	
	void paint()
	{
		_ASSERT(_CrtIsMemoryBlock(display, sizeof(TextDisplay), 0, 0, 0));
		display->paint(container);
	}
	
	void connect()
	{
		if (connected)
		{
			ATLTRACE("Connect -- SetEvent\r\n");
			SetEvent(hEnqLock);
			SetEvent(h34Lock);
			Logger::log("Attempt connect when already connected");
			return;
		}
		termThread = CreateThread(NULL, 0, TermProc, this, 0, &threadID);
	}
	
	void close()
	{
		ATLTRACE("Close -- SetEvent\r\n");
		SetEvent(hEnqLock);
		SetEvent(hEnqLock);
		connected = false;
		telnet->disconnect();
		dispatchDisconnect();
	}
	
	void run();
	
	void mappedKey(char *s, int len);
	
	void command(char c);
	
	inline int getPage()
	{
		_ASSERT(_CrtIsMemoryBlock(display, sizeof(TextDisplay), 0, 0, 0));
		return display->getCurrentPage();
	}
	
	inline int getNumRows()
	{
		_ASSERT(_CrtIsMemoryBlock(display, sizeof(TextDisplay), 0, 0, 0));
		return display->getNumRows();
	}

	inline int getNumColumns()
	{
		_ASSERT(_CrtIsMemoryBlock(display, sizeof(TextDisplay), 0, 0, 0));
		return display->getNumColumns();
	}

	inline int getCursorX()
	{
		_ASSERT(_CrtIsMemoryBlock(display, sizeof(TextDisplay), 0, 0, 0));
		return display->getCursorCol();
	}
	
	inline int getCursorY()
	{
		_ASSERT(_CrtIsMemoryBlock(display, sizeof(TextDisplay), 0, 0, 0));
		return display->getCursorRow();
	}
	
	inline void getStartFieldASCII(StringBuffer *sb)
	{
		_ASSERT(_CrtIsMemoryBlock(display, sizeof(TextDisplay), 0, 0, 0));
		display->getStartFieldASCII(sb);
	}
	
	char *ttype()
	{
		return "vt6530";
	}
	
	void localEchoOff()
	{
		_ASSERT(_CrtIsMemoryBlock(display, sizeof(TextDisplay), 0, 0, 0));
		display->setEchoOff();
	}
	
	void localEchoOn()
	{
		_ASSERT(_CrtIsMemoryBlock(display, sizeof(TextDisplay), 0, 0, 0));
		display->setEchoOn();
	}
	
	void naws(int *width, int *height)
	{
		*width = 80;
		*height = 24;
	}

	/**
	 *  Display a message in line 25
	 */
	void message(char *msg)
	{
		//_ASSERT(_CrtIsMemoryBlock(display, sizeof(TextDisplay), 0, 0, 0));
		//display->writeMessage(msg);
		//dispatchDisplayChanged();
		dispatchDebug(msg);
	}

	/**
	 *  The host has completed transmision and is
	 *  now waiting for input.  This can be used
	 *  to buffer keystrokes until the screen is
	 *  fully displayed.
	 */
	void enquire()
	{
		_ASSERT(_CrtIsMemoryBlock(telnet, sizeof(Telnet), 0, 0, 0));
		telnet->flush();
		ATLTRACE("ENQ -- SetEvent\r\n");
		SetEvent(hEnqLock);
		dispatchEnquire();
	}
	
	/**
	 *  Telnet has recived a 34 extended control sequence.  
	 *  The parameters seem to contain info about text
	 *  attributes (such as invisible) and characters that
	 *  signal a buffer send to Tandem (like CR).
	 */
	virtual void set34(char *operation, char *params, int len)
	{
		if (params[0] == 4)
		{
			ATLTRACE("34 -- SetEvent\r\n");
			SetEvent(h34Lock);
		}
		else
		{
			ATLTRACE("34 -- Not Firing\r\n");
		}
		dispatch34(operation, params, len);
	}

	/**
	 *  Set the major terminal mode.  The choices
	 *  are:
	 *		A	ANSI
	 *		B	Block Mode
	 *		C	Conversation mode
	 *//*
	void setMode(char mode)
	{
		switch (mode)
		{
			case 'A':
				//System.out.println("ANSI Mode");
				break;
			case 'B':
				keys->setKeySet(KEYS_BLOCK);
				display->setModeBlock();
				telnet->setBufferingOn();
				break;
			case 'C':
				keys->setKeySet(KEYS_CONV);
				display->setModeConv();
				telnet->setBufferingOff();
				break;
		}
	}*/
	
	void setCursorY(int row)
	{
		_ASSERT(_CrtIsMemoryBlock(display, sizeof(TextDisplay), 0, 0, 0));
		display->setCursorRowCol(row, display->getCursorCol());
	}
	
	void setCursorX(int col)
	{
		_ASSERT(_CrtIsMemoryBlock(display, sizeof(TextDisplay), 0, 0, 0));
		display->setCursorRowCol(display->getCursorRow(), col);
	}
	
	void scrapeScreen(StringBuffer *sb)
	{
		_ASSERT(_CrtIsMemoryBlock(display, sizeof(TextDisplay), 0, 0, 0));
		display->dumpScreen(sb);
	}
	
	void scrapeAttributes(StringBuffer *sb)
	{
		_ASSERT(_CrtIsMemoryBlock(display, sizeof(TextDisplay), 0, 0, 0));
		display->dumpAttibutes(sb);
	}
	
	void getSubString(int row, int col, int len, StringBuffer *sb)
	{
		_ASSERT(_CrtIsMemoryBlock(display, sizeof(TextDisplay), 0, 0, 0));
		display->getSubString(row, col, len, sb);
	}

	void setForegroundColor(char *color)
	{
		container->setForeGroundColor(getColor(color));
		dispatchDisplayChanged();
	}

	void setBackgroundColor(char *color)
	{
		container->setBackGroundColor(getColor(color));
		dispatchDisplayChanged();
	}
	
	void setForegroundColor(COLORREF c)
	{
		container->setForeGroundColor(c);
		dispatchDisplayChanged();
	}

	void setBackgroundColor(COLORREF c)
	{
		container->setBackGroundColor(c);
		dispatchDisplayChanged();
	}

	COLORREF getColor(char *color);

	/**
	 *  Get the 'index'nth field on the screen.
	 *  The first field is index ZERO.  If the
	 *  index is larger than the number of field,
	 *  an empty string is returned.
	 */
	void getField(int index, StringBuffer *sb)
	{
		_ASSERT(_CrtIsMemoryBlock(display, sizeof(TextDisplay), 0, 0, 0));
		display->getField(index, sb);
	}
	
	/**
	 *  Get the video, data, and key attributes for a
	 *  field.
	 */
	int getFieldAttributes(int index)
	{
		_ASSERT(_CrtIsMemoryBlock(display, sizeof(TextDisplay), 0, 0, 0));
		return display->getFieldAttributes(index);
	}

	/**
	 *  Get the text in the field at the cursor
	 *  position.
	 */
	void getCurrentField(StringBuffer *sb)
	{
		_ASSERT(_CrtIsMemoryBlock(display, sizeof(TextDisplay), 0, 0, 0));
		display->getCurrentField(sb);
	}
	
	/**
	 *  Get the 'index'nth unprotected field on 
	 *  the screen.  The first field is index 
	 *  ZERO.  If the index is larger than the 
	 *  number of field, an empty string is 
	 *  returned.
	 */
	void getUnprotectField(int index, StringBuffer *sb)
	{
		_ASSERT(_CrtIsMemoryBlock(display, sizeof(TextDisplay), 0, 0, 0));
		display->getUnprotectField(index, sb);
	}
	
	/**
	 *  Write text into the 'index'nth 
	 *  unprotected field on the screen.  The 
	 *  first field is index ZERO.  If the 
	 *  index is larger than the number of field, 
	 *  the request is ignored.
	 */
	void setField(int index, char *text)
	{
		_ASSERT(_CrtIsMemoryBlock(display, sizeof(TextDisplay), 0, 0, 0));
		display->setField(index, text);
	}
	
	/**
	 *  Returns true if the 'index'nth unprotected
	 *  field has its MDT set. The first field is 
	 *  index ZERO.  If the index is larger than 
	 *  the  number of fields, false is returned.
	 */
	bool isFieldChanged(int index)
	{
		_ASSERT(_CrtIsMemoryBlock(display, sizeof(TextDisplay), 0, 0, 0));
		return display->isFieldChanged(index);
	}
	
	/**
	 *  Get a full line of display text.  the line
	 *  number is ranged 1-24.
	 */
	void getLine(int lineNumber, StringBuffer *sb)
	{
		_ASSERT(_CrtIsMemoryBlock(display, sizeof(TextDisplay), 0, 0, 0));
		display->getLine(lineNumber, sb);
	}
	
	/**
	 *  Set the cursor at the start if the 
	 *  'index'nth unprotected field on the screen.  
	 *  The first field is index ZERO.  If the 
	 *  index is larger than the number of field, 
	 *  the request is ignored.
	 */
	void cursorToField(int index)
	{
		_ASSERT(_CrtIsMemoryBlock(display, sizeof(TextDisplay), 0, 0, 0));
		display->cursorToField(index);
	}
	
	/**
	 *  Send a break to tandem
	 */
	void sendBreak()
	{
		_ASSERT(_CrtIsMemoryBlock(telnet, sizeof(Telnet), 0, 0, 0));
		telnet->send(4);
		telnet->send(0);
	}
	/**
	 *  Functions as if the user entered the
	 *  string on the command line and hit F10.
	 */
	void sendCommandLine(char *command)
	{
		_ASSERT(_CrtIsMemoryBlock(display, sizeof(TextDisplay), 0, 0, 0));
		display->setCursorRowCol(23, 1);
		display->writeDisplay(command);
		fakeF10();
		dispatchDisplayChanged();
	}
	
	/** 
	 *  Send a single keystroke to the terminal as
	 *  if it was typed at the keyboard.  Set shift,
	 *  alt, and/or ctrl to true to simulate these
	 *  keys being held down while the key was 
	 *  pressed.
	 */
	void fakeKey(int keycode, bool shift, bool alt, bool ctrl)
	{
		//_ASSERT(_CrtIsMemoryBlock(keys, sizeof(Keys), 0, 0, 0));
		if ((keycode >= 0x30 && keycode <= 0x5B) || (keycode < 14))
		{
			keys->keyTyped(keycode, shift, ctrl, alt);
		}
		else
		{
			keys->keyReleased(keycode, shift, ctrl, alt);
		}
		//keypress(keycode, shift, ctrl, alt);
		dispatchDisplayChanged();
	}
	
	/**
	 *  Send a string to the terminal as if it
	 *  was typed at the keyboard.
	 */
	void fakeKeys(char *keystrokes);
	
	/**
	 *  Fake the user pressing a function key.
	 */
	void fakeF1()
	{
		ATLTRACE("FakeF1\r\n");
		_ASSERT(_CrtIsMemoryBlock(keys, sizeof(Keys), 0, 0, 0));
		keys->keyReleased(VK_F1, false, false, false);
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	void fakeF2()
	{
		_ASSERT(_CrtIsMemoryBlock(keys, sizeof(Keys), 0, 0, 0));
		keys->keyReleased(VK_F1, false, false, false);
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	void fakeF3()
	{
		_ASSERT(_CrtIsMemoryBlock(keys, sizeof(Keys), 0, 0, 0));
		keys->keyReleased(VK_F3, false, false, false);
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	void fakeF4()
	{
		_ASSERT(_CrtIsMemoryBlock(keys, sizeof(Keys), 0, 0, 0));
		keys->keyReleased(VK_F4, false, false, false);
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	void fakeF5()
	{
		_ASSERT(_CrtIsMemoryBlock(keys, sizeof(Keys), 0, 0, 0));
		keys->keyReleased(VK_F5, false, false, false);
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	void fakeF6()
	{
		_ASSERT(_CrtIsMemoryBlock(keys, sizeof(Keys), 0, 0, 0));
		keys->keyReleased(VK_F6, false, false, false);
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	void fakeF7()
	{
		_ASSERT(_CrtIsMemoryBlock(keys, sizeof(Keys), 0, 0, 0));
		keys->keyReleased(VK_F7, false, false, false);
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	void fakeF8()
	{
		_ASSERT(_CrtIsMemoryBlock(keys, sizeof(Keys), 0, 0, 0));
		keys->keyReleased(VK_F8, false, false, false);
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	void fakeF9()
	{
		_ASSERT(_CrtIsMemoryBlock(keys, sizeof(Keys), 0, 0, 0));
		keys->keyReleased(VK_F9, false, false, false);
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	void fakeF10()
	{
		_ASSERT(_CrtIsMemoryBlock(keys, sizeof(Keys), 0, 0, 0));
		keys->keyReleased(VK_F10, false, false, false);
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	void fakeF11()
	{
		_ASSERT(_CrtIsMemoryBlock(keys, sizeof(Keys), 0, 0, 0));
		keys->keyReleased(VK_F11, false, false, false);
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	void fakeF12()
	{
		_ASSERT(_CrtIsMemoryBlock(keys, sizeof(Keys), 0, 0, 0));
		keys->keyReleased(VK_F12, false, false, false);
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	void fakeF13()
	{
		_ASSERT(_CrtIsMemoryBlock(keys, sizeof(Keys), 0, 0, 0));
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	void fakeF14()
	{
		_ASSERT(_CrtIsMemoryBlock(keys, sizeof(Keys), 0, 0, 0));
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	void fakeF15()
	{
		_ASSERT(_CrtIsMemoryBlock(keys, sizeof(Keys), 0, 0, 0));
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	void fakeF16()
	{
		_ASSERT(_CrtIsMemoryBlock(keys, sizeof(Keys), 0, 0, 0));
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	void fakeEnter()
	{
		_ASSERT(_CrtIsMemoryBlock(keys, sizeof(Keys), 0, 0, 0));
		keys->keyPressed(10, false, false, false);
		dispatchDisplayChanged();
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	void fakeBackspace()
	{
		_ASSERT(_CrtIsMemoryBlock(keys, sizeof(Keys), 0, 0, 0));
		keys->keyPressed('\b', false, false, false);
		dispatchDisplayChanged();
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	void fakeTab()
	{
		_ASSERT(_CrtIsMemoryBlock(keys, sizeof(Keys), 0, 0, 0));
		keys->keyPressed('\t', false, false, false);
		dispatchDisplayChanged();
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	void fakeUpArrow()
	{
		_ASSERT(_CrtIsMemoryBlock(keys, sizeof(Keys), 0, 0, 0));
		keys->keyPressed(VK_UP, false, false, false);
		dispatchDisplayChanged();
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	void fakeDownArrow()
	{
		_ASSERT(_CrtIsMemoryBlock(keys, sizeof(Keys), 0, 0, 0));
		keys->keyPressed(VK_DOWN, false, false, false);
		dispatchDisplayChanged();
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	void fakeLeftArrow()
	{
		_ASSERT(_CrtIsMemoryBlock(keys, sizeof(Keys), 0, 0, 0));
		keys->keyPressed(VK_LEFT, false, false, false);
		dispatchDisplayChanged();
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	void fakeRightArrow()
	{
		_ASSERT(_CrtIsMemoryBlock(keys, sizeof(Keys), 0, 0, 0));
		keys->keyPressed(VK_RIGHT, false, false, false);
		dispatchDisplayChanged();
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	void fakeHome()
	{
		_ASSERT(_CrtIsMemoryBlock(keys, sizeof(Keys), 0, 0, 0));
		keys->keyPressed(VK_HOME, false, false, false);
		dispatchDisplayChanged();
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	void fakeEnd()
	{
		_ASSERT(_CrtIsMemoryBlock(keys, sizeof(Keys), 0, 0, 0));
		keys->keyPressed(VK_END, false, false, false);
		dispatchDisplayChanged();
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	void fakeInsert()
	{
		_ASSERT(_CrtIsMemoryBlock(keys, sizeof(Keys), 0, 0, 0));
		keys->keyPressed(VK_INSERT, false, false, false);
		dispatchDisplayChanged();
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	void fakeDelete()
	{
		_ASSERT(_CrtIsMemoryBlock(keys, sizeof(Keys), 0, 0, 0));
		keys->keyPressed(VK_DELETE, false, false, false);
		dispatchDisplayChanged();
	}

	void toHTML(StringBuffer *sb)
	{
		_ASSERT(_CrtIsMemoryBlock(display, sizeof(TextDisplay), 0, 0, 0));
		display->toHTML(container->getForeGroundColor(), container->getBackGroundColor(), sb);
	}

	void addListener(TermEventListener *tel)
	{
		listeners.addElement(tel);
	}

private:

	void dispatchConnect()
	{
		for (int x = 0; x < listeners.size(); x++)
		{
			listeners.elementAt(x)->connect();
		}
	}

	void dispatchDisconnect()
	{
		for (int x = 0; x < listeners.size(); x++)
		{
			listeners.elementAt(x)->disconnect();
		}
	}

	void dispatchEnquire()
	{
		for (int x = 0; x < listeners.size(); x++)
		{
			listeners.elementAt(x)->enquire();
		}
	}

	void dispatchDisplayChanged()
	{
		for (int x = 0; x < listeners.size(); x++)
		{
			listeners.elementAt(x)->displayChanged();
		}
	}

	void dispatchError(char *msg)
	{
		for (int x = 0; x < listeners.size(); x++)
		{
			listeners.elementAt(x)->error(msg);
		}
	}

	void dispatchDebug(char *msg)
	{
		for (int x = 0; x < listeners.size(); x++)
		{
			listeners.elementAt(x)->debug(msg);
		}
	}

	void dispatch34(char *operation, char *params, int len)
	{
		for (int x = 0; x < listeners.size(); x++)
		{
			listeners.elementAt(x)->recv34(operation, params, len);
		}
	}
};
	
class NullDisplay : public PaintSurface, public PaintPeer
{
public:

	//KeyListener *listener;
	COLORREF backcolor;
	COLORREF forecolor;


	NullDisplay()
	{
		backcolor = RGB(0, 255, 0);
		forecolor = RGB(0, 0, 0);
		//listener = NULL;
	}

	~NullDisplay()
	{
	}

	void paint()
	{
	}

	int getPixelWidth()
	{
		return 0;
	}
	
	int getPixelHeight()
	{
		return 0;
	}

	void setFont(char *fontName, int attributes, int fontSize)
	{
	}
	
	int getFontWidth()
	{
		return 10;
	}
	
	int getFontHeight()
	{
		return 10;
	}
	
	int getFontDescent()
	{
		return 0;
	}

	COLORREF getBackGroundColor()
	{
		return backcolor;
	}

	COLORREF getForeGroundColor()
	{
		return forecolor;
	}
	
	void setBackGroundColor(COLORREF c)
	{
		backcolor = c;
	}
	
	void setForeGroundColor(COLORREF c)
	{
		forecolor = c;
	}

	void setBkColor(COLORREF backcolor)
	{
	}

	HPEN setPen(HPEN pen)
	{
		return NULL;
	}

	void setTextColor(COLORREF c)
	{
	}

	void setPaintXorMode()
	{
	}
	
	void setPaintMode()
	{
	}

	void fillRect(int x1, int y1, int x2, int y2, HBRUSH brush)
	{
	}
	
	void drawBytes(char *text, int offset, int length, int x1, int y1)
	{
	}
	
	void drawLine(int x1, int y1, int x2, int y2)
	{
	}

	void setPaintPeer(PaintPeer *peer)
	{
	}
	
	void forceRepaint()
	{
	}
};


#endif