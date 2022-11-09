package gov.revenue.vt6530;

import java.awt.Container;
import java.awt.Event;
import java.awt.FlowLayout;
import java.awt.Color;
import java.awt.event.KeyListener;
import java.awt.event.KeyEvent;

import gov.revenue.ASSERT;
import gov.revenue.Logger;
import gov.revenue.vt6530.ui.TextDisplay;
import gov.revenue.vt6530.ui.Keys;
import gov.revenue.vt6530.ui.MappedKeyListener;
import gov.revenue.vt6530.modes.*;
import gov.revenue.vt6530.telnet.*;
import gov.revenue.vt6530.ui.PaintSurface;
import gov.revenue.vt6530.ui.PaintPeer;

/**
 *  Term is the core of the terminal emulator.  The
 *  other principal classes (Telnet, Guardian, Keys,
 *  and TextDisplay) are created and held by Term.
 */
public class Term implements MappedKeyListener, TelnetStateListener, Runnable, PaintPeer
{
	/**
	 *  Interface to physical display.
	 */
	private PaintSurface container;
	
	/**
	 *  Virtual 80X25 text display.
	 */
	private TextDisplay display;
	
	/**
	 *  Handles key mappings
	 */
	public Keys keys;
	
	/**
	 *  Handles communication with the tandem.
	 */
	private Telnet telnet;
	
	/**
	 *  The current terminal mode.  The only mode
	 *  currently supported is Guardian.
	 */
	private Mode currentMode;
	
	/**
	 *  Flag indicating whether we're connected to
	 *  Tandem.
	 */
	private boolean connected = false;
	
	/**
	 *  Host name ("is")
	 */
	private String host;
	
	/**
	 *  Host port number (1016)
	 */
	private int port;
	
	/**
	 *  Flag indicating whether we've recived
	 *  an ENQ.
	 */
	private boolean enq = false;
	
	/**
	 *  List of TermEventListeners
	 */
	FastVector listeners = new FastVector();
		
	/**
	 *  Creates the terminal, but does not connect.
	 */
	public Term()
	{		
		if (ASSERT.debug > 0)
			Logger.setOutput("\\output.txt");
		
		container = new NullDisplay();
		container.setPaintPeer(this);
		container.setFont("Courier", java.awt.Font.BOLD, 14);
		display = new TextDisplay(2, 80, 24);
		
		keys = new Keys();
		container.registerKeyListener(keys);
		keys.setListener(this);
		
		telnet = new Telnet(this);
		currentMode = new Guardian(display, keys, telnet);		
	}

	/**
	 *  Wait for the Tandem to issue an ENQ.  This
	 *  happens in block mode after the Tandem is
	 *  finished sending display commands for the 
	 *  current screen.  A one second delay may be
	 *  required after the ENQ before sending data
	 *  to the Tandem.
	 */
	synchronized public boolean waitENQ()
	{
		if (enq)
		{
			return true;
		}
		try
		{
			wait(25000);
		}
		catch (InterruptedException ei)
		{
		}
		return enq;
	}
	
	/**
	 *  Set the interface to the physical display.
	 */
	final public void setDisplay(PaintSurface ps)
	{
		ps.setPaintPeer(this);
		ps.registerKeyListener(keys);
		container = ps;
	}
	
	/**
	 *  Set the host name.  If we're already connected,
	 *  reestablish the connection.
	 */
	final public void setHost(String host)
	{
		this.host = host;
		if (connected)
		{
			close();
			connect();
		}
	}
	
	final public String getHost()
	{
		return host;
	}
	
	/**
	 *  Set the port number.  If we're already connected,
	 *  reestablish the connection.
	 */
	final public void setPort(int port)
	{
		this.port = port;
		if (connected)
		{
			close();
			connect();
		}
	}
	
	final public int getPort()
	{
		return port;
	}
	
	/**
	 *  Render the virtual display on the physical display.
	 */
	public void paint()
	{
		display.paint(container);
	}
	
	/**
	 *  Connect to the host.
	 */
	final public void connect()
	{
		if (connected)
		{
			Logger.log("Attempt connect when already connected");
			return;
		}
		display.clearPage();
		Thread t = new Thread(this);
		t.start();
	}
	
	/**
	 *  disconect from the host
	 */
	final public void close()
	{
		try
		{
			connected = false;
			telnet.disconnect();
		}
		catch (java.io.IOException eio)
		{
		}
		dispatchDisconnect();
	}
	
	/**
	 *  Termainl thread.
	 *	  While Connected
	 *		1.  Recives data from the Tandem
	 *		2.  Current mode interprets the data
	 */
	public void run()
	{
		try
		{
			display.echoDisplay("Connecting to " + host + "...");
			dispatchDisplayChanged();
			telnet.connect(host, port);
			connected = true;
			display.carageReturn();
			display.linefeed();
			
			display.echoDisplay("Connected.");
			dispatchConnect();
			
			dispatchDisplayChanged();
						
			while (connected)
			{
				byte[] b = telnet.receive();
				if (b.length > 0)
				{
					currentMode.processRemoteString(b);
					if (display.needsRepaint())
					{
						dispatchDisplayChanged();
					}
				}
			}
		}
		catch (java.io.IOException | RuntimeException ioe)
		{
			display.writeMessage("ERROR: " + ioe.getCause().getMessage());
			dispatchDisplayChanged();

			ioe.printStackTrace();
			connected = false;
		}
	}
	
	/**
	 *  The user has pressed a key that is not
	 *  captured locally (like F1).
	 */
	final public void mappedKey(String s)
	{
		if (s.charAt(0) == Keys.SPC_PRINTSCR && ASSERT.debug == 1)
		{
			display.dumpScreen(Logger.out);
			Logger.out.println(display.dumpAttibutes());
			//Logger.out.println(display.toHTML(container.getForeGroundColor(), container.getBackGroundColor()));
		}
		// do not echo command sequences on the virutal display
		if (s.charAt(0) != (char)1)
		{
			display.echoDisplay(s);
		}
		try
		{
			// We are satisfying the ENQ, so unsignal ENQ
			enq = false;
			telnet.send(s.getBytes());
		}
		catch (java.io.IOException ioe)
		{
			connected = false;
		}
		dispatchDisplayChanged();
	}
	
	/**
	 *  The user has pressed a key which is processed
	 *  locally.
	 */
	final public void command(byte c)
	{
		if (c == Keys.SPC_BREAK)
		{
			try
			{
				telnet.send((byte)0);
				telnet.send((byte)0);
				telnet.send((byte)0);
			}
			catch (java.io.IOException ioe)
			{
				ioe.printStackTrace();
			}
			return;
		}
		else if (c == Keys.SPC_PRINTSCR)
		{
			//display.dumpScreen(Logger.out);
			//Logger.out.println(display.dumpAttibutes());
			Logger.out.println(display.toHTML(container.getForeGroundColor(), container.getBackGroundColor()));
			return;
		}
		currentMode.execLocalCommand(c);
		dispatchDisplayChanged();
	}
	
	/**
	 *  Get the current memory page being displayed
	 */
	final public int getPage()
	{
		return display.getCurrentPage();
	}
	
	/**
	 *  Get the cursor column on the display page
	 */
	final public int getCursorX()
	{
		return display.getCursorCol();
	}
	
	/**
	 *  Get the cursor row on the display page
	 */
	final public int getCursorY()
	{
		return display.getCursorRow();
	}
	
	/**
	 *  Get the location of the first unprocted field
	 *  in 6530 ASC encoded format.
	 */
	final public String getStartFieldASCII()
	{
		return display.getStartFieldASCII();
	}
	
	/**
	 *  Returns the terminal type
	 */
	final public String ttype()
	{
		return "vt6530";
	}
	
	/**
	 *  Turn off local echo (ANSI terminal)
	 */
	final public void localEchoOff()
	{
		display.setEchoOff();
	}
	
	/**
	 *  Turn on local echo (ANSI terminal)
	 */
	final public void localEchoOn()
	{
		display.setEchoOn();
	}
	
	/**
	 *  Get the display size
	 */
	final public java.awt.Dimension naws()
	{
		return new java.awt.Dimension(80, 24);
	}

	/**
	 *  Display a message in line 25
	 */
	final public void message(String msg)
	{
		display.writeMessage(msg);
		dispatchDisplayChanged();
	}

	/**
	 *  The host has completed transmision and is
	 *  now waiting for input.  This can be used
	 *  to buffer keystrokes until the screen is
	 *  fully displayed.
	 */
	synchronized final public void enquire()
	{
		enq = true;
		notifyAll();
		dispatchEnquire();
	}
	
	/**
	 *  Set the major terminal mode.  The choices
	 *  are:
	 *		A	ANSI
	 *		B	Block Mode
	 *		C	Conversation mode
	 */
	final public void setMode(char mode)
	{
		switch (mode)
		{
			case 'A':
				System.out.println("ANSI Mode");
				break;
			case 'B':
				keys.setKeySet(Keys.KEYS_BLOCK);
				display.setModeBlock();
				break;
			case 'C':
				keys.setKeySet(Keys.KEYS_CONV);
				display.setModeConv();
				break;
		}
	}
	
	/**
	 *  Set the cursor row
	 */
	final public void setCursorY(int row)
	{
		display.setCursorRowCol(row, display.getCursorCol());
	}
	
	/**
	 *  Set the cursor column
	 */
	final public void setCursorX(int col)
	{
		display.setCursorRowCol(display.getCursorRow(), col);
	}
	
	/**
	 *  Returns all the characters on the screen.  Each
	 *  line is seperated by CRLF.
	 */
	final public String scrapeScreen()
	{
		return display.dumpScreen();
	}
	
	/**
	 *  Returns the attributes on the current screen
	 *  encoded as ASCII (f.e. N == vid_normal).  Lines
	 *  are seperated by CRLF.
	 */
	final public String scrapeAttributes()
	{
		return display.dumpAttibutes();
	}
	
	/**
	 *  Set the text color.
	 */
	final public void setForegroundColor(String color)
	{
		container.setForeGroundColor(getColor(color));
		dispatchDisplayChanged();
	}

	/**
	 *  Set the background color
	 */
	final public void setBackgroundColor(String color)
	{
		container.setBackGroundColor(getColor(color));
		dispatchDisplayChanged();
	}
	
	/**
	 *  Translate color names to Color
	 */
	final private Color getColor(String color)
	{
		if (color.equals("GREEN"))
			return Color.green;
			
		else if (color.equals("BLACK"))
			return Color.black;
			
		else if (color.equals("BLUE"))
				return Color.blue;
		
		else if (color.equals("DARKBLUE"))
		{
			return new Color(0, 0, 0x70);
		}
			
		else if (color.equals("LTGRAY"))
			return Color.lightGray;
				
		else if (color.equals("GRAY"))
			return Color.gray;
		
		else if (color.equals("DARKGRAY"))
		{
			return new Color(0x50, 0x50, 0x50);
		}
			
		else if (color.equals("WHITE"))
			return Color.white;
				
		else if (color.equals("YELLOW"))
			return Color.yellow;
				
		else if (color.equals("RED"))
			return Color.red;
				
		else if (color.equals("PINK"))
			return Color.pink;
				
		else if (color.equals("MAGENTA"))
			return Color.magenta;
				
		else if (color.equals("ORANGE"))
			return Color.orange;
				
		else
			return Color.darkGray;
	}

	/**
	 *  Get the 'index'nth field on the screen.
	 *  The first field is index ZERO.  If the
	 *  index is larger than the number of field,
	 *  an empty string is returned.
	 */
	final public String getField(int index)
	{
		return display.getField(index);
	}
	
	/**
	 *  Get the video, data, and key attributes for a
	 *  field.
	 */
	final public int getFieldAttributes(int index)
	{
		return display.getFieldAttributes(index);
	}

	/**
	 *  Get the text in the field at the cursor
	 *  position.
	 */
	final public String getCurrentField()
	{
		return display.getCurrentField();
	}
	
	/**
	 *  Get the 'index'nth unprotected field on 
	 *  the screen.  The first field is index 
	 *  ZERO.  If the index is larger than the 
	 *  number of field, an empty string is 
	 *  returned.
	 */
	final public String getUnprotectField(int index)
	{
		return display.getUnprotectField(index);
	}
	
	/**
	 *  Write text into the 'index'nth 
	 *  unprotected field on the screen.  The 
	 *  first field is index ZERO.  If the 
	 *  index is larger than the number of field, 
	 *  the request is ignored.
	 */
	final public void setField(int index, String text)
	{
		display.setField(index, text);
	}
	
	/**
	 *  Returns true if the 'index'nth unprotected
	 *  field has its MDT set. The first field is 
	 *  index ZERO.  If the index is larger than 
	 *  the  number of fields, false is returned.
	 */
	final public boolean isFieldChanged(int index)
	{
		return display.isFieldChanged(index);
	}
	
	/**
	 *  Get a full line of display text.  the line
	 *  number is ranged 1-24.
	 */
	final public String getLine(int lineNumber)
	{
		return display.getLine(lineNumber);
	}
	
	/**
	 *  Set the cursor at the start if the 
	 *  'index'nth unprotected field on the screen.  
	 *  The first field is index ZERO.  If the 
	 *  index is larger than the number of field, 
	 *  the request is ignored.
	 */
	final public void cursorToField(int index)
	{
		display.cursorToField(index);
	}
		
	/**
	 *  Functions as if the user entered the
	 *  string on the command line and hit F10.
	 */
	final public void sendCommandLine(String command)
	{
		display.setCursorRowCol(23, 1);
		display.writeDisplay(command);
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
	final public void fakeKey(int keycode, boolean shift, boolean alt, boolean ctrl)
	{
		int mod = 0;
		if (shift)
		{
			mod |= KeyEvent.VK_SHIFT;
		}
		if (alt)
		{
			mod |= KeyEvent.VK_ALT;
		}
		if (ctrl)
		{
			mod |= KeyEvent.VK_CONTROL;
		}
		keys.keyAction(false, keycode, keycode, mod);
		dispatchDisplayChanged();
	}
	
	/**
	 *  Send a string to the terminal as if it
	 *  was typed at the keyboard.
	 */
	final public void fakeKeys(String keystrokes)
	{
		int key;
		
		for (int x = 0; x < keystrokes.length(); x++)
		{
			key = keystrokes.charAt(x);
			keys.keyAction(false, key, key, 0);
		}
		dispatchDisplayChanged();
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	final public void fakeF1()
	{
		KeyEvent e = new KeyEvent(null, KeyEvent.KEY_PRESSED, 0L, 0, KeyEvent.VK_F1);
		keys.keyReleased(e);
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	final public void fakeF2()
	{
		KeyEvent e = new KeyEvent(null, KeyEvent.KEY_PRESSED, 0L, 0, KeyEvent.VK_F2);
		keys.keyReleased(e);
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	final public void fakeF3()
	{
		KeyEvent e = new KeyEvent(null, KeyEvent.KEY_PRESSED, 0L, 0, KeyEvent.VK_F3);
		keys.keyReleased(e);
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	final public void fakeF4()
	{
		KeyEvent e = new KeyEvent(null, KeyEvent.KEY_PRESSED, 0L, 0, KeyEvent.VK_F4);
		keys.keyReleased(e);
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	final public void fakeF5()
	{
		KeyEvent e = new KeyEvent(null, KeyEvent.KEY_PRESSED, 0L, 0, KeyEvent.VK_F5);
		keys.keyReleased(e);
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	final public void fakeF6()
	{
		KeyEvent e = new KeyEvent(null, KeyEvent.KEY_PRESSED, 0L, 0, KeyEvent.VK_F6);
		keys.keyReleased(e);
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	final public void fakeF7()
	{
		KeyEvent e = new KeyEvent(null, KeyEvent.KEY_PRESSED, 0L, 0, KeyEvent.VK_F7);
		keys.keyReleased(e);
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	final public void fakeF8()
	{
		KeyEvent e = new KeyEvent(null, KeyEvent.KEY_PRESSED, 0L, 0, KeyEvent.VK_F8);
		keys.keyReleased(e);
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	final public void fakeF9()
	{
		KeyEvent e = new KeyEvent(null, KeyEvent.KEY_PRESSED, 0L, 0, KeyEvent.VK_F9);
		keys.keyReleased(e);
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	final public void fakeF10()
	{
		KeyEvent e = new KeyEvent(null, KeyEvent.KEY_PRESSED, 0L, 0, KeyEvent.VK_F10);
		keys.keyReleased(e);
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	final public void fakeF11()
	{
		KeyEvent e = new KeyEvent(null, KeyEvent.KEY_PRESSED, 0L, 0, KeyEvent.VK_F11);
		keys.keyReleased(e);
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	final public void fakeF12()
	{
		KeyEvent e = new KeyEvent(null, KeyEvent.KEY_PRESSED, 0L, 0, KeyEvent.VK_F12);
		keys.keyReleased(e);
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	final public void fakeF13()
	{
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	final public void fakeF14()
	{
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	final public void fakeF15()
	{
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	final public void fakeF16()
	{
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	final public void fakeEnter()
	{
		KeyEvent e = new KeyEvent(null, KeyEvent.KEY_PRESSED, 0, 10, 10);
		keys.keyPressed(e);
		dispatchDisplayChanged();
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	final public void fakeBackspace()
	{
		KeyEvent e = new KeyEvent(null, KeyEvent.KEY_PRESSED, 0, 8, '\b');
		keys.keyPressed(e);
		dispatchDisplayChanged();
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	final public void fakeTab()
	{
		KeyEvent e = new KeyEvent(null, KeyEvent.KEY_PRESSED, 0L, 0, 9,'\t');
		keys.keyPressed(e);
		dispatchDisplayChanged();
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	final public void fakeUpArrow()
	{
		KeyEvent e = new KeyEvent(null, KeyEvent.KEY_PRESSED, 0L, 0, KeyEvent.VK_UP);
		keys.keyPressed(e);
		dispatchDisplayChanged();
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	final public void fakeDownArrow()
	{
		KeyEvent e = new KeyEvent(null, KeyEvent.KEY_PRESSED, 0L, 0, KeyEvent.VK_DOWN);
		keys.keyPressed(e);
		dispatchDisplayChanged();
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	final public void fakeLeftArrow()
	{
		KeyEvent e = new KeyEvent(null, KeyEvent.KEY_PRESSED, 0L, 0, KeyEvent.VK_LEFT);
		keys.keyPressed(e);
		dispatchDisplayChanged();
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	final public void fakeRightArrow()
	{
		KeyEvent e = new KeyEvent(null, KeyEvent.KEY_PRESSED, 0L, 0, KeyEvent.VK_RIGHT);
		keys.keyPressed(e);
		dispatchDisplayChanged();
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	final public void fakeHome()
	{
		KeyEvent e = new KeyEvent(null, KeyEvent.KEY_PRESSED, 0L, 0, KeyEvent.VK_HOME);
		keys.keyPressed(e);
		dispatchDisplayChanged();
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	final public void fakeEnd()
	{
		KeyEvent e = new KeyEvent(null, KeyEvent.KEY_PRESSED, 0L, 0, KeyEvent.VK_END);
		keys.keyPressed(e);
		dispatchDisplayChanged();
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	final public void fakeInsert()
	{
		KeyEvent e = new KeyEvent(null, KeyEvent.KEY_PRESSED, 0L, 0, KeyEvent.VK_INSERT);
		keys.keyPressed(e);
		dispatchDisplayChanged();
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	final public void fakeDelete()
	{
		KeyEvent e = new KeyEvent(null, KeyEvent.KEY_PRESSED, 0L, 0, KeyEvent.VK_DELETE);
		keys.keyPressed(e);
		dispatchDisplayChanged();
	}

	/**
	 *  Return the current screen as a web page (fun for ASP!).
	 *  Not currently working.
	 */
	final public String toHTML()
	{
		return display.toHTML(container.getForeGroundColor(), container.getBackGroundColor());
	}
	
	/**
	 *  A dummy display for using the terminal in a non GUI
	 *  mode.
	 */
	class NullDisplay implements PaintSurface, PaintPeer
	{
		java.awt.event.KeyListener listener;
		Color back = Color.darkGray, fore = Color.white;
		
		public void paint()
		{
		}

		public int getPixelWidth()
		{
			return 0;
		}
		
		public int getPixelHeight()
		{
			return 0;
		}
	
		public void setFont(String fontName, int attributes, int fontSize)
		{
		}
		
		public int getFontWidth()
		{
			return 10;
		}
		
		public int getFontHeight()
		{
			return 10;
		}
		
		public int getFontDescent()
		{
			return 0;
		}
	
		public java.awt.Color getBackGroundColor()
		{
			return back;
		}
		
		public java.awt.Color getForeGroundColor()
		{
			return fore;
		}
		
		public void setBackGroundColor(java.awt.Color c)
		{
			back = c;
		}
		
		public void setForeGroundColor(java.awt.Color c)
		{
			fore = c;
		}
	
		public void setPaintXorMode(java.awt.Color c)
		{
		}
		
		public void setPaintMode()
		{
		}
	
		public void fillRect(int x1, int y1, int x2, int y2)
		{
		}
		
		public void drawBytes(byte[] text, int offset, int length, int x1, int y1)
		{
		}
		
		public void drawLine(int x1, int y1, int x2, int y2)
		{
		}
	
		public void registerKeyListener(KeyListener listener)
		{
			this.listener = listener;
		}
	
		public void setPaintPeer(PaintPeer peer)
		{
		}
		
		public void forceRepaint()
		{
		}
	}

	public void addListener(TermEventListener tel)
	{
		listeners.addElement(tel);
	}
	
	private void dispatchConnect()
	{
		for (int x = 0; x < listeners.size(); x++)
		{
			((TermEventListener)listeners.elementAt(x)).connect();
		}
	}

	private void dispatchDisconnect()
	{
		for (int x = 0; x < listeners.size(); x++)
		{
			((TermEventListener)listeners.elementAt(x)).disconnect();
		}
	}

	private void dispatchEnquire()
	{
		for (int x = 0; x < listeners.size(); x++)
		{
			((TermEventListener)listeners.elementAt(x)).enquire();
		}
	}

	private void dispatchDisplayChanged()
	{
		for (int x = 0; x < listeners.size(); x++)
		{
			((TermEventListener)listeners.elementAt(x)).displayChanged();
		}
	}

	private void dispatchError(String msg)
	{
		for (int x = 0; x < listeners.size(); x++)
		{
			((TermEventListener)listeners.elementAt(x)).error(msg);
		}
	}

	private void dispatchDebug(String msg)
	{
		for (int x = 0; x < listeners.size(); x++)
		{
			((TermEventListener)listeners.elementAt(x)).debug(msg);
		}
	}
}