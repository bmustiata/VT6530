package gov.revenue.vt6530;

import java.awt.Frame;
import java.awt.event.WindowListener;
import java.awt.event.WindowEvent;
import java.awt.GridLayout;

import gov.revenue.vt6530.ui.AwtDisplay;
import gov.revenue.vt6530.TermEventListener;

/**
 *  Display the terminal in a window.
 */
public class Windowed extends Frame implements WindowListener, Runnable, TermEventListener
{
	/**
	 *  Interface to an AWT canvas
	 */
	AwtDisplay display;
	
	/**
	 *  The terminal core
	 */
	Term term;
	
	/**
	 *  Tandem host name or IP
	 */
	String host = "is";
	
	/**
	 *  Tandem port
	 */
	int port = 1016;
	
	/**
	 *  Main
	 * 
	 *  Usage:
	 *		java gov.revenue.vt6530.Windowed
	 */
	public static void main(String[] args)
	{
		if (args.length == 2)
		{
			new Windowed(args[0], Integer.parseInt(args[1]));
		}
		else
		{
			new Windowed();
		}
	}
	
	public Windowed(String host, int port)
	{
		super("VT6530");
		this.host = host;
		this.port = port;
		init();
	}
	
	public Windowed()
	{
		super("vt6530");
		init();
	}

	/**
	 *  Setup the GUI, but doesn't connect
	 */
	private final void init()
	{
		setSize(620, 480);
		display = new AwtDisplay(24, 80);
		display.setFont("Courier", java.awt.Font.BOLD, 14);

		setLayout(new GridLayout(1, 1));
		add(display);
		term = new Term();
		term.setDisplay(display);
		term.setBackgroundColor("DARKBLUE");
		term.setForegroundColor("WHITE");
		term.setHost(host);
		term.setPort(port);
		term.addListener(this);
		
		setBackground(java.awt.Color.darkGray);
		show();
		doLayout();
		pack();
		
		addWindowListener(this);
		
		Thread t = new Thread(this);
		t.start();
	}

	public void run()
	{
		term.connect();
	}
	
	/*
	 *  TermEventListner callbacks
	 */
	
	/**
	 *  The terminal has successfully connected
	 *  to the host.
	 */
	public void connect()
	{
	}
	
	/**
	 *  The connection to the host was lost or
	 *  closed.
	 */
	public void disconnect()
	{
	}
	
	/**
	 *  The host has completed rendering the
	 *  screen and is now waiting for input.
	 */
	public void enquire()
	{
	}
	
	/**
	 *  Changes in the display require the container
	 *  to repaint.
	 */
	public void displayChanged()
	{
		display.repaint();
	}
	
	/**
	 *  There has been an internal error.
	 */
	public void error(String message)
	{
	}
	
	/**
	 *  Debuging output -- may be ignored
	 */
	public void debug(String message)
	{
	}

	
	/*
	 *  WindowListener callbacks
	 */
	
	
	public void windowActivated(WindowEvent e)
	{
	}
	
	public void windowClosed(WindowEvent e)
	{
	}
	
	public void windowClosing(WindowEvent e)
	{
		setVisible(false);
		term.close();
		System.exit(0);
	}
	
	public void windowDeactivated(WindowEvent e)
	{
	}
	
	public void windowDeiconified(WindowEvent e)
	{
	}
	
	public void windowIconified(WindowEvent e)
	{
	}
	
	public void windowOpened(WindowEvent e)
	{
	}
}