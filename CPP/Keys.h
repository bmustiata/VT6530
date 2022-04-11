#ifndef _keys_h
#define _keys_h

#include "MappedKeyListener.h"


#define KEYS_ANSI  0
#define KEYS_CONV  1
#define KEYS_BLOCK 2
	
#define SOH  ((char)1)
#define ENQUIRY  ((char)5)
#define ACK   ((char)6)
#define BELL  ((char)7)
#define BACKSPACE  "\b"
#define NAK  "\21"
#define ESC  "\27"
#define CR   ((char)13)
	
#define SPC_F1  0
#define SPC_F2  1
#define SPC_F3  2
#define SPC_F4  3
#define SPC_F5  4
#define SPC_F6  5
#define SPC_F7  6
#define SPC_F8  7
#define SPC_F9  11
#define SPC_F10  12
#define SPC_F11  14
#define SPC_F12  15
#define SPC_BREAK  16
#define SPC_PGUP  17
#define SPC_PGDN  18
#define SPC_HOME  19
#define SPC_END  20
#define SPC_INS  21
#define SPC_DEL  22
#define SPC_SCROLLOCK  23
#define SPC_UP  24
#define SPC_DOWN  25
#define SPC_LEFT  26
#define SPC_RIGHT  28
#define SPC_PRINTSCR  29
#define LAST_SPC  30


class Keys
{
	bool sendCursorWithFn;
	
	char pressedKey;
	long pressedWhen;

	char **plain;
	char **crtl;
	char **alt;
	char **shift;
	
	int *localCmd;
	int *localCmdCtl;
	int *localCmdAlt;
	
	bool ignoreKeys;
	bool protectMode;
	bool enterKeyOn;

	MappedKeyListener *listener;
						  
public:

	Keys()
	{
		setKeySet(KEYS_CONV);
		ignoreKeys = false;
		protectMode = false;
		enterKeyOn = true;
		sendCursorWithFn = false;
		pressedKey = ' ';
		pressedWhen = 0;
	}

	virtual ~Keys();

	void setProtectMode()
	{
		lockKeyboard();
		protectMode = true;
		// disable local line editing
	}
	
	void exitProtectMode()
	{
		unlockKeyboard();
		protectMode = false;
		// enable local line editing
	}
	
	void setEnterKeyOn()
	{
		enterKeyOn = true;
	}
	
	void setEnterKeyOff()
	{
		enterKeyOn = false;
	}
	
	void setListener(MappedKeyListener *listener)
	{
		this->listener = listener;
	}
	
	void setKeySet( int keySet );
	
	//public void setMap(int ch, int modifier, char *out);
	
	void keyPressed( int keycode, bool shift, bool ctrl, bool alt ) 
	{
	}
	
	void keyReleased( int keycode, bool shift, bool ctrl, bool alt );
	
	void keyTyped(  int keycode, bool shift, bool ctrl, bool alt )
	{
		pressedKey = keycode;
		keyAction(false, keycode, shift, ctrl, alt);
	}
	
	void keyAction(bool fn, int keycode, bool shift, bool ctrl, bool alt );
	
	
	void lockKeyboard()
	{
		ignoreKeys = true;
	}
	
	void unlockKeyboard()
	{
		ignoreKeys = false;
	}
	
	void setCrLfOn()
	{
	}
	
	void setCrLfOff()
	{
	}

private:
	
	void notifyListener(char b)
	{
		if (! ignoreKeys)
		{
			listener->command(b);
		}
	}
	
	void notifyListener(char *str, int len)
	{
		if (! ignoreKeys)
		{
			listener->mappedKey(str, len);
		}
	}

	static char *Keys::ansiChar[];
	static char *Keys::ansiCharCtl[];
	static char *Keys::ansiCharAlt[];
	static char *Keys::ansiCharShift[];
	static int Keys::ansiLocal[];
	static int Keys::ansiLocalCtl[];
	static int Keys::ansiLocalAlt[];
	static char *Keys::convChar[];
	static char *Keys::convCharCtl[];
	static char *Keys::convCharAlt[];
	static char *Keys::convCharShift[];
	static int Keys::convLocal[];
	static int Keys::convLocalCtl[];
	static int Keys::convLocalAlt[];
	static char *Keys::blockChar[];
	static char *Keys::blockCharCtl[];
	static char *Keys::blockCharAlt[];
	static char *Keys::blockCharShift[];
	static int Keys::blockLocal[];
	static int blockLocalCtl[];
	static int Keys::blockLocalAlt[];
	static char *shiftFn[];
	static char *plainFn[];
};

#endif
