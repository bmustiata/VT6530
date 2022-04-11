#ifndef _telnet_state_listener_h
#define _telnet_state_listener_h

class TelnetStateListener
{
public:
	virtual char *ttype() = 0;
	virtual void localEchoOff() = 0;
	virtual void localEchoOn() = 0;
	virtual void naws(int *width, int *height) = 0;
	virtual void message(char *msg) = 0;
	virtual void enquire() = 0;
	virtual void set34(char *op, char *params, int len) = 0;
};

#endif