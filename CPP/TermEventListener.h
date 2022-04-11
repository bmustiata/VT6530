#ifndef _term_event_listener_h
#define _term_event_listener_h

#include <comdef.h>


class TermEventListener
{
public:
	/**
	 *  The terminal has successfully connected
	 *  to the host.
	 */
	virtual void connect() = 0;
	
	/**
	 *  The connection to the host was lost or
	 *  closed.
	 */
	virtual void disconnect() = 0;
	
	/**
	 *  The host has completed rendering the
	 *  screen and is now waiting for input.
	 */
	virtual void enquire() = 0;
	
	/**
	 *  Changes in the display require the container
	 *  to repaint.
	 */
	virtual void displayChanged() = 0;
	
	/**
	 *  There has been an internal error.
	 */
	virtual void error(char *message) = 0;
	
	/**
	 *  Debuging output -- may be ignored
	 */
	virtual void debug(char *message) = 0;

	virtual void recv34(char *op, char *params, int paramLen) = 0;
};

#endif