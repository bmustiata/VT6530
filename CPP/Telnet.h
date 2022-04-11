#ifndef _telnet_h
#define _telnet_h

#include <winsock2.h>

#ifdef _DEBUG
#include <stdio.h>
#endif

#include "TelnetStateListener.h"
#include "StringBuffer.h"
#include "IOException.h"

// length of the socket read buffer
#define READBUF_LEN  8190


class Telnet : public TelnetStateListener
{
	FILE *sendlog;
	FILE *reclog;
	
	char neg_state;

	/* buffer for reading from the socket */
	char readbuf[READBUF_LEN];

	/* users of this class can manually control buffering of output */
	StringBuffer sendBuffer;

	bool isBuffering;

	/**
	 * What IAC SB <xx> we are handling right now
	 */
	char current_sb;

	/**
	 * What IAC DO(NT) request do we have received already ?
	 */
	char receivedDX[256];
	
	/**
	 * What IAC WILL/WONT request do we have received already ?
	 */
	char receivedWX[256];
	/**
	 * What IAC DO/DONT request do we have sent already ?
	 */
	char sentDX[256];
	/**
	 * What IAC WILL/WONT request do we have sent already ?
	 */
	char sentWX[256];

	SOCKET mysocket;

	TelnetStateListener *peer;		/* peer, notified on status */

	bool sent34Wont;

public:

	Telnet(TelnetStateListener *p);
	
	virtual ~Telnet();

	/**
	 * Connect to the remote host at the specified port.
	 * @param address the symbolic host address
	 * @param port the numeric port
	 * @see #disconnect
	 */
	void connect(char *address, int port);

	/**
	 * Disconnect from remote host.
	 * @see #connect
	 */
	void disconnect();
	
	/**
	 * Set the object to be notified about current status.
	 * @param obj object to be notified.
	 */
	void setListener(TelnetStateListener *obj) 
	{ 
		peer = obj; 
	}	

	/**
	 * Read data from the remote host. Blocks until data is available. 
	 * Returns an array of bytes.
	 * @see #send
	 */
	void receive(StringBuffer *buf);

	inline void _send(char b)
	{
#ifdef _DEBUG
		fprintf(sendlog, "%c\t%d\n", (char)b, (int)(b&0xFF));
		fflush(sendlog);
		ATLTRACE("_send\r\n");
#endif
		if (::send(mysocket, &b, 1, 0) != 1)
		{
			throw IOException("Error writing to socket");
		}				
	}

	inline void _send(char *buf, int len)
	{
#ifdef _DEBUG
		for (int qq = 0; qq < len; qq++)
		{
			fprintf(sendlog, "%c\t%d\n", (char)buf[qq], (int)(buf[qq]&0xFF));
		}
		fflush(sendlog);
		ATLTRACE("_send\r\n");
#endif
		if (::send(mysocket, buf, len, 0) != len)
		{
			throw IOException("Error writing to socket");
		}
	}

	/**
	 * Send data to the remote host.
	 * @param buf array of bytes to send
	 * @see #receive
	 */
	inline void send(char *buf, int len)
	{
		if (isBuffering)
		{
			sendBuffer.append(buf, len);
			return;
		}
		_send(buf, len);
	}

	inline void send(char b)
	{
		if (isBuffering)
		{
			sendBuffer.append(b);
			return;
		}
		_send(b);
	}

	void flush()
	{
		int len = sendBuffer.length();
		if (len == 0)
		{
			return;
		}
#ifdef _DEBUG
		for (int qq = 0; qq < len; qq++)
		{
			fprintf(sendlog, "%c\t%d\n", (char)sendBuffer[qq], (int)(sendBuffer[qq]&0xFF));
		}
		fflush(sendlog);
#endif
		if (::send(mysocket, sendBuffer, len, 0) != len)
		{
			throw IOException("Error writing to socket");
		}
		sendBuffer.setLength(0);
	}
	
	inline void setBufferingOn()
	{
		isBuffering = true;
	}

	inline void setBufferingOff()
	{
		isBuffering = false;
	}

	/**
	 * Handle an incoming IAC SB <type> <bytes> IAC SE
	 * @param type type of SB
	 * @param sbata byte array as <bytes>
	 * @param sbcount nr of bytes. may be 0 too.
	 */
	void handle_sb(char type, char *sbdata, int sbcount, StringBuffer *sb);

	/* wo faengt buf an bei buf[0] oder bei buf[1] */
	void negotiate(StringBuffer *out, int count);

	char *ttype()
	{
		return "TN6530-8";
	}
	
	void localEchoOn()
	{
	}

	void localEchoOff()
	{
	}
	
	void naws(int *width, int *height)
	{
		*width = 0;
		*height = 0;
	}
	
	/*void setMode(char mode)
	{
	}*/
	
	void message(char *msg)
	{
	}
	
	void enquire()
	{
	}

	/**
	 *  Telnet has recived a 34 extended control sequence.  
	 *  The parameters seem to contain info about text
	 *  attributes (such as invisible) and characters that
	 *  signal a buffer send to Tandem (like CR).
	 */
	virtual void set34(char *operation, char *params, int len)
	{
	}
};

#endif