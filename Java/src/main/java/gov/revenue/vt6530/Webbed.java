package gov.revenue.vt6530;

import java.applet.*;
import java.awt.Color;
import java.awt.GridLayout;

import gov.revenue.vt6530.ui.AwtDisplay;
import gov.revenue.vt6530.ui.PaintPeer;

/**
 *  Display the terminal on a web page (see default.html).
 */
public class Webbed extends Applet implements TermEventListener
{
	/**
	 *  Interface to an AWT canvas.
	 */
	AwtDisplay display;
	
	/**
	 *  The core terminal
	 */
	Term term;
	
	/**
	 *  Tandem host name or IP.
	 */
	String host;
	
	/**
	 *  Tandem port number
	 */
	int port;
	
	/**
	 *  Initialize the applet
	 * 
	 *		- Get the host, port, and colors from the PARAM tags
	 *		- initialize the display
	 *		- connect to the tandem
	 */
	public void init()
	{
		host = getParameter("HOST");
		String port = getParameter("PORT");
		String bgColor = getParameter("BGCOLOR");
		String fgColor = getParameter("FGCOLOR");
	
		if (host == null)
		{
			host = "is";
		}
		if (port == null)
		{
			this.port = 1016;
		}
		else
		{
			this.port = Integer.parseInt(port);
		}
		display = new AwtDisplay(24, 80);
		display.setFont("Courier", java.awt.Font.BOLD, 14);
		setLayout(new GridLayout(1, 1));
		add(display);
		
		term = new Term();
		term.setDisplay(display);
		term.setHost(host);
		term.setPort(this.port);
		term.addListener(this);
		term.connect();
		
		if (bgColor != null)
		{
			term.setBackgroundColor(bgColor);
		}
		if (fgColor != null)
		{
			term.setForegroundColor(fgColor);
		}
	}
	
	/**
	 *  Netscape calls start/stop when the applet is no
	 *  longer visible on the page.  If we want to use
	 *  NS, this will need to be handled here.
	 */
	public void start()
	{
	}
	
	public void stop()
	{
		term.close();
	}

	/*
	 *  HTML Interface
	 */
	
	/**
	 *  Open a terminal in a window
	 */
	public void openWindow()
	{
		new Windowed(host, port);
	}
	
	/**
	 *  Set the background color
	 */
	public void setBackgroundColor(String color)
	{
		term.setBackgroundColor(color);
	}
	
	/**
	 *  Set the text color
	 */
	public void setForegroundColor(String color)
	{
		term.setForegroundColor(color);
	}
	
	/** 
	 *  Get all the character data
	 */
	public String screenScrape()
	{
		return term.scrapeScreen();
	}
	
	
	/*
	 *  TermEventListener call backs
	 */
	
	
	/**
	 *  The terminal has successfully connected
	 *  to the host.
	 */
	public void connect()
	{
		this.showStatus("Connected");
	}
	
	/**
	 *  The connection to the host was lost or
	 *  closed.
	 */
	public void disconnect()
	{
		this.showStatus("Disconnected");
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
		this.showStatus(message);
	}
	
	/**
	 *  Debuging output -- may be ignored
	 */
	public void debug(String message)
	{
		this.showStatus(message);
	}
}
