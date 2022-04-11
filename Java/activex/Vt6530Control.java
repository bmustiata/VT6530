package activex;

import com.ms.wfc.core.*;
import com.ms.wfc.ui.*;
import com.ms.lang.Delegate;

import gov.revenue.vt6530.Term;
import gov.revenue.vt6530.TermEventListener;
import gov.revenue.vt6530.ui.PaintSurface;
import gov.revenue.vt6530.ui.PaintPeer;
import gov.revenue.vt6530.ui.Attributes;


/**
 * This class is a visual component. The entry point for class execution
 * is the constructor.
 * @com.register ( clsid=033BC599-7DD6-11D4-98D6-000102494781, typelib=033BC598-7DD6-11D4-98D6-000102494781 )
 */
public class Vt6530Control extends UserControl implements Attributes
{
	
	/**
	 * Terminal connected to the host
	 */
	EventHandler m_Connected;
	/**
	 * Debug message
	 */
	EventHandler m_Debug;
	/**
	 * The terminal closed or lost connection to the host
	 */
	EventHandler m_Disconnected;
	/**
	 * The host completed sending screen data and now is waiting for input
	 */
	EventHandler m_Enquire;
	/**
	 * Internal error
	 */
	EventHandler m_Error;
	/**
	 * Tandem DNS Name or IP
	 */
	private String m_Host = "is";
	/**
	 * Tandem TCP Port Number
	 */
	private int m_Port = 1016;
	
	private Term term;
	private TermCallBack tcb;
	private PaintInterface pi;
	
	private java.awt.Component snarbin = new java.awt.Button();
	
	public Vt6530Control()
	{
		super();

		// Required for Visual J++ Form Designer support
		//
		initForm();
		addOnResize(new EventHandler(this.resize));
		addOnKeyDown(new KeyEventHandler(this.keydown));
		addOnKeyUp(new KeyEventHandler(this.keyup));
		addOnKeyPress(new KeyPressEventHandler(this.keypress));
		
		tcb = new TermCallBack(this);
		
		// TODO: Add any constructor code after initForm call
		term = new Term();
		pi = new PaintInterface(this, term);
		term.setDisplay(pi);
		term.addListener(tcb);
		term.setBackgroundColor("BLACK");
		term.setForegroundColor("GREEN");
		term.setHost(m_Host);
		term.setPort(m_Port);
		
		this.setBackColor(new Color(java.awt.Color.darkGray.getRGB()));				
	}

	private void resize(Object source, Event e)
	{
		pi.resize();
	}
	
	private void keydown(Object source, KeyEvent e)
	{
		term.keys.keyPressed(new java.awt.event.KeyEvent(snarbin, java.awt.event.KeyEvent.KEY_PRESSED, 0, e.getModifiers(), e.getKeyCode()));
	}
	
	private void keypress(Object source, KeyPressEvent e)
	{
		term.keys.keyTyped(new java.awt.event.KeyEvent(snarbin, java.awt.event.KeyEvent.KEY_PRESSED, 0, 0, 0, e.getKeyChar()));
	}
	
	private void keyup(Object source, KeyEvent e)
	{
		term.keys.keyReleased(new java.awt.event.KeyEvent(snarbin, java.awt.event.KeyEvent.KEY_PRESSED, 0, e.getModifiers(), e.getKeyCode()));
	}

	/**
	 *  Wait until the host issues an ENQ
	 */
	public void waitENQ()
	{
		term.waitENQ();
	}
	
	public void waitConnect()
	{
		if (tcb.connected)
		{
			return;
		}
		try
		{
			wait(15000);
		}
		catch (InterruptedException ie)
		{
		}
	}
	

	/**
	 * Terminal connected to the host
	 */
	public void addOnConnected(EventHandler value)
	{
		m_Connected = (EventHandler)Delegate.combine(m_Connected, value);
	}

	/**
	 * Debug message
	 */
	public void addOnDebug(EventHandler value)
	{
		m_Debug = (EventHandler)Delegate.combine(m_Debug, value);
	}

	/**
	 * The terminal closed or lost connection to the host
	 */
	public void addOnDisconnected(EventHandler value)
	{
		m_Disconnected = (EventHandler)Delegate.combine(m_Disconnected, value);
	}

	/**
	 * The host completed sending screen data and now is waiting for input
	 */
	public void addOnEnquire(EventHandler value)
	{
		m_Enquire = (EventHandler)Delegate.combine(m_Enquire, value);
	}

	/**
	 * Internal error
	 */
	public void addOnError(EventHandler value)
	{
		m_Error = (EventHandler)Delegate.combine(m_Error, value);
	}

	/**
	 * Tandem DNS Name or IP
	 */
	public String getHost()
	{
		// TODO: Add your own implementation.
		return m_Host;
	}

	/**
	 * Tandem TCP Port Number
	 */
	public int getPort()
	{
		// TODO: Add your own implementation.
		return m_Port;
	}

	/**
	 * Terminal connected to the host
	 */
	protected void onConnected(Event event)
	{
		if (m_Connected != null) m_Connected.invoke(this, event);
	}

	/**
	 * Debug message
	 */
	protected void onDebug(Event event)
	{
		if (m_Debug != null) m_Debug.invoke(this, event);
	}

	/**
	 * The terminal closed or lost connection to the host
	 */
	protected void onDisconnected(Event event)
	{
		if (m_Disconnected != null) m_Disconnected.invoke(this, event);
	}

	/**
	 * The host completed sending screen data and now is waiting for input
	 */
	protected void onEnquire(Event event)
	{
		if (m_Enquire != null) m_Enquire.invoke(this, event);
	}

	/**
	 * Internal error
	 */
	protected void onError(Event event)
	{
		if (m_Error != null) m_Error.invoke(this, event);
	}

    protected void onPaint(PaintEvent p) 
	{
        //super.onPaint(p);
		pi.onPaint(p);
    }

	/**
	 * Terminal connected to the host
	 */
	public void removeOnConnected(EventHandler value)
	{
		m_Connected = (EventHandler)Delegate.remove(m_Connected, value);
	}

	/**
	 * Debug message
	 */
	public void removeOnDebug(EventHandler value)
	{
		m_Debug = (EventHandler)Delegate.remove(m_Debug, value);
	}

	/**
	 * The terminal closed or lost connection to the host
	 */
	public void removeOnDisconnected(EventHandler value)
	{
		m_Disconnected = (EventHandler)Delegate.remove(m_Disconnected, value);
	}

	/**
	 * The host completed sending screen data and now is waiting for input
	 */
	public void removeOnEnquire(EventHandler value)
	{
		m_Enquire = (EventHandler)Delegate.remove(m_Enquire, value);
	}

	/**
	 * Internal error
	 */
	public void removeOnError(EventHandler value)
	{
		m_Error = (EventHandler)Delegate.remove(m_Error, value);
	}

	public void connect()
	{
		term.connect();
	}
	
	/**
	 * Tandem DNS Name or IP
	 */
	public void setHost(String value)
	{
		if (value == null)
		{
			return;
		}
		m_Host = value;
		term.setHost(value);
	}

	/**
	 * Tandem TCP Port Number
	 */
	public void setPort(int value)
	{
		if (value == 0)
			return;
		m_Port = value;
		term.setPort(value);
	}

	/**
	 * NOTE: The following code is required by the Visual J++ form
	 * designer.  It can be modified using the form editor.  Do not
	 * modify it using the code editor.
	 */
	Container components = new Container();

	private void initForm()
	{
		this.setFont(new Font("MS Sans Serif", 11.0f, FontSize.CHARACTERHEIGHT, FontWeight.NORMAL, false, false, false, com.ms.wfc.app.CharacterSet.DEFAULT, 0));
		this.setSize(new Point(326, 200));
		this.setText("Vt6530");
	}

	public static class ClassInfo extends UserControl.ClassInfo
	{
		// TODO: Add your property and event infos here
		
		public static final EventInfo connected = new EventInfo(
			Vt6530Control.class, "connected", EventHandler.class,
			new DescriptionAttribute("Terminal connected to the host"));
		public static final EventInfo debug = new EventInfo(
			Vt6530Control.class, "debug", EventHandler.class,
			new DescriptionAttribute("Debug message"));
		
		public static final EventInfo disconnected = new EventInfo(
			Vt6530Control.class, "disconnected", EventHandler.class,
			new DescriptionAttribute("The terminal closed or lost connection to the host"));
		
		public static final EventInfo enquire = new EventInfo(
			Vt6530Control.class, "enquire", EventHandler.class,
			new DescriptionAttribute("The host completed sending screen data and now is waiting for input"));
		
		public static final EventInfo error = new EventInfo(
			Vt6530Control.class, "error", EventHandler.class,
			new DescriptionAttribute("Internal error"));
		
		public static final PropertyInfo host = new PropertyInfo(
			Vt6530Control.class, "host", String.class,
			CategoryAttribute.Behavior,
			new DescriptionAttribute("Tandem DNS Name or IP"));
		
		public static final PropertyInfo port = new PropertyInfo(
			Vt6530Control.class, "port", int.class,
			CategoryAttribute.Behavior,
			new DescriptionAttribute("Tandem TCP Port Number"));
		
		public void getEvents(IEvents events)
		{
			super.getEvents(events);
			events.add(disconnected);
			events.add(enquire);
			events.add(error);
			events.add(debug);
			events.add(connected);
		}

		public void getProperties(IProperties props)
		{
			super.getProperties(props);
			props.add(host);
			props.add(port);
		}
	}

	public void disconnect()
	{
		term.close();
	}
	
	/**
	 *  Get the text of the entire screen.  Lines
	 *  on the screen are seperated by CRLF.  
	 *  Video, field, data, and key attributes 
	 *  are not returned.
	 */
	public String getScreenDump()
	{
		return term.scrapeScreen();
	}
	
	/**
	 *  Get the text and attributes of the entire
	 *  screen.  Lines are seperated by CRLF.  The
	 *  format of the dump is:
	 *		ANSI Character
	 *		
	 */
	public String getAttributeDump()
	{
		return term.scrapeAttributes();
	}
	
	/**
	 *  Get the 'index'nth field on the screen.
	 *  The first field is index ZERO.  If the
	 *  index is larger than the number of field,
	 *  an empty string is returned.
	 */
	public String getField(int index)
	{
		return term.getField(index);
	}
	
	/**
	 *  Get the video, data, and key attributes for a
	 *  field.
	 */
	public int getFieldAttributes(int index)
	{
		return term.getFieldAttributes(index);
	}
	
	/**
	 *  Is reverse video on?
	 */
	public boolean isReverse(int attributes)
	{
		return (attributes & VID_REVERSE) != 0;
	}
	
	public boolean isBlinking(int attributes)
	{
		return (attributes & VID_BLINKING) != 0;
	}
	
	public boolean isInvis(int attributes)
	{
		return (attributes & VID_INVIS) != 0;		
	}
	
	public boolean isUnderline(int attributes)
	{
		return (attributes & VID_UNDERLINE) != 0;
	}
	
	public boolean isUnprotected(int attributes)
	{
		return (attributes & DAT_UNPROTECT) != 0;
	}
	
	public boolean isUpshift(int attributes)
	{
		return (attributes & KEY_UPSHIFT) != 0;
	}
	
	public int getRGB(int attributes)
	{
		int color = (attributes & MASK_COLOR)>>SHIFT_COLOR;
		if (color == 0)
		{
			// return the forground color
			return java.awt.Color.green.getRGB();
		}
		else
		{
			return java.awt.Color.green.getRGB();
		}		
	}
	
	/**
	 *  Get the text in the field at the cursor
	 *  position.
	 */
	public String getCurrentField()
	{
		return term.getCurrentField();
	}
	
	/**
	 *  Get the 'index'nth unprotected field on 
	 *  the screen.  The first field is index 
	 *  ZERO.  If the index is larger than the 
	 *  number of field, an empty string is 
	 *  returned.
	 */
	public String getUnprotectField(int index)
	{
		return term.getUnprotectField(index);
	}
	
	/**
	 *  Write text into the 'index'nth 
	 *  unprotected field on the screen.  The 
	 *  first field is index ZERO.  If the 
	 *  index is larger than the number of field, 
	 *  the request is ignored.
	 */
	public void setField(int index, String text)
	{
		term.setField(index, text);
	}
	
	/**
	 *  Returns true if the 'index'nth unprotected
	 *  field has its MDT set. The first field is 
	 *  index ZERO.  If the index is larger than 
	 *  the  number of fields, false is returned.
	 */
	public boolean isFieldChanged(int index)
	{
		return term.isFieldChanged(index);
	}
	
	/**
	 *  Get a full line of display text.  the line
	 *  number is ranged 1-24.
	 */
	public String getLine(int lineNumber)
	{
		return term.getLine(lineNumber);
	}
	
	/**
	 *  Set the cursor at the start if the 
	 *  'index'nth unprotected field on the screen.  
	 *  The first field is index ZERO.  If the 
	 *  index is larger than the number of field, 
	 *  the request is ignored.
	 */
	public void cursorToField(int index)
	{
		term.cursorToField(index);
	}
	
	/**
	 *  Get the row the cursor is on.  This
	 *  is number 1 - 24.
	 */
	public int getCursorRow()
	{
		return term.getCursorY() + 1;
	}
	
	/**
	 *  Set the cursor row (1-24).  In protect sub-
	 *  mode, a tab will be performed if the new
	 *  position is a protected cell.
	 */
	public void setCursorRow(int row)
	{
		term.setCursorY(row-1);
	}

	/**
	 *  Get the cursor column (1-80)
	 */
	public int getCursorCol()
	{
		return term.getCursorX() + 1;
	}
	
	/**
	 *  Set the cursor column (1-80).  In protect 
	 *  sub-mode, a tab will be performed if the 
	 *  new position is a protected cell.
	 */
	public void setCursorCol(int col)
	{
		term.setCursorX(col-1);
	}
	
	/**
	 *  Functions as if the user entered the
	 *  string on the command line and hit F10.
	 */
	public void sendCommandLine(String command)
	{
		term.sendCommandLine(command);
	}
	
	/** 
	 *  Send a single keystroke to the terminal as
	 *  if it was typed at the keyboard.  Set shift,
	 *  alt, and/or ctrl to true to simulate these
	 *  keys being held down while the key was 
	 *  pressed.
	 */
	public void fakeKey(int keycode, boolean shift, boolean alt, boolean ctrl)
	{
		term.fakeKey(keycode, shift, alt, ctrl);
	}
	
	/**
	 *  Send a string to the terminal as if it
	 *  was typed at the keyboard.
	 */
	public void fakeKeys(String keystrokes)
	{
		term.fakeKeys(keystrokes);
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	public void fakeF1()
	{
		term.fakeF1();
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	public void fakeF2()
	{
		term.fakeF2();
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	public void fakeF3()
	{
		term.fakeF3();
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	public void fakeF4()
	{
		term.fakeF4();
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	public void fakeF5()
	{
		term.fakeF5();
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	public void fakeF6()
	{
		term.fakeF6();
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	public void fakeF7()
	{
		term.fakeF7();
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	public void fakeF8()
	{
		term.fakeF8();
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	public void fakeF9()
	{
		term.fakeF9();
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	public void fakeF10()
	{
		term.fakeF10();
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	public void fakeF11()
	{
		term.fakeF11();
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	public void fakeF12()
	{
		term.fakeF12();
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	public void fakeF13()
	{
		term.fakeF13();
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	public void fakeF14()
	{
		term.fakeF14();
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	public void fakeF15()
	{
		term.fakeF15();
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	public void fakeF16()
	{
		term.fakeF16();
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	public void fakeEnter()
	{
		term.fakeEnter();
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	public void fakeBackspace()
	{
		term.fakeBackspace();
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	public void fakeTab()
	{
		term.fakeTab();
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	public void fakeUpArrow()
	{
		term.fakeUpArrow();
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	public void fakeDownArrow()
	{
		term.fakeDownArrow();
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	public void fakeLeftArrow()
	{
		term.fakeLeftArrow();
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	public void fakeRightArrow()
	{
		term.fakeRightArrow();
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	public void fakeHome()
	{
		term.fakeHome();
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	public void fakeEnd()
	{
		term.fakeEnd();
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	public void fakeInsert()
	{
		term.fakeInsert();
	}
	
	/**
	 *  Fake the user pressing a function key.
	 */
	public void fakeDelete()
	{
		term.fakeDelete();
	}
	
	public String toHTML()
	{
		return term.toHTML();
	}

	class TermCallBack implements TermEventListener
	{
		Vt6530Control control;
		boolean connected;
		
		TermCallBack(Vt6530Control control)
		{
			this.control = control;
		}
		/**
		 *  The terminal has successfully connected
		 *  to the host.
		 */
		public void connect()
		{
			connected = true;
			control.onConnected(new Event());
		}
	
		/**
		 *  The connection to the host was lost or
		 *  closed.
		 */
		public void disconnect()
		{
			connected = false;
			control.onDisconnected(new Event());
		}
	
		/**
		 *  The host has completed rendering the
		 *  screen and is now waiting for input.
		 */
		public void enquire()
		{
			control.onEnquire(new Event());
		}
	
		/**
		 *  Changes in the display require the container
		 *  to repaint.
		 */
		public void displayChanged()
		{
			//Graphics g = control.createGraphics();
			pi.forceRepaint();
			//control.invalidate();
		}
	
		/**
		 *  There has been an internal error.
		 */
		public void error(String message)
		{
			control.onError(new Event(message));
		}
	
		/**
		 *  Debuging output -- may be ignored
		 */
		public void debug(String message)
		{
			control.onDebug(new Event(message));
		}
	}
}
