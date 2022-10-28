package gov.revenue.vt6530;

/**
 *  Call-back interface for terminal events.
 */
public interface TermEventListener
{
	/**
	 *  The terminal has successfully connected
	 *  to the host.
	 */
	void connect();
	
	/**
	 *  The connection to the host was lost or
	 *  closed.
	 */
	void disconnect();
	
	/**
	 *  The host has completed rendering the
	 *  screen and is now waiting for input.
	 */
	void enquire();
	
	/**
	 *  Changes in the display require the container
	 *  to repaint.
	 */
	void displayChanged();
	
	/**
	 *  There has been an internal error.
	 */
	void error(String message);
	
	/**
	 *  Debuging output -- may be ignored
	 */
	void debug(String message);
}
