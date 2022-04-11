#ifndef _guardian_h
#define _guardian_h

#include "StringBuffer.h"
#include "IOException.h"
#include "Telnet.h"
#include "Keys.h"
#include "Mode.h"
#include "TextDisplay.h"


#define SIZE_ELEM 100

/**
 *  Class to assist in command parsing
 */
class InputElementBuffer
{
	char *elements[SIZE_ELEM];
	int pos;

public:
	InputElementBuffer()
	{
		pos = 0;
	}

	~InputElementBuffer()
	{
		clear();
	}

	int size()
	{
		return pos;
	}

	void addElement(char *str)
	{
		char *buf = new char[strlen(str)+1];
		lstrcpy(buf, str);
		_ASSERT(pos < SIZE_ELEM);
		elements[pos++] = buf;
	}

	void addElement(char c)
	{
		char *buf = new char[2];
		buf[0] = c;
		buf[1] = '\0';
		_ASSERT(pos < SIZE_ELEM);
		elements[pos++] = buf;
	}

	char *elementAt(int index)
	{
		_ASSERT(index < SIZE_ELEM);
		_ASSERT(_CrtIsValidHeapPointer(elements[index]) == TRUE);
		return elements[index];
	}

	void clear()
	{
		for (int x = 0; x < pos; x++)
		{
			delete elements[x];
			elements[x] = NULL;
		}
		pos = 0;
	}
};

/**
 *	Tandem Guardian operating environment terminal
 *  command interpreter.
 */
class Guardian : public Mode
{	
	InputElementBuffer strStack;

	/* accumulate characters for sending to the display */
	StringBuffer accum;
	
	StringBuffer keyBuffer;
	
	/* the keyboard handler */
	Keys *keys;

	/* the socket IO and telnet line protocol handler */
	Telnet *telnet;
	
	/* the abstract display.  characters are stored here,
	 * but doesn't actually render the text on-screen     */
	TextDisplay *display;
	
	/* The command interpreter is a state machine -- this is the state */
	int state;

public:
	
	Guardian(TextDisplay *display, Keys *keys, Telnet *telnet)
	{
		this->keys = keys;
		this->telnet = telnet;
		this->display = display;
		state = 0;
	}

	/*
	 *  Process incoming text from the host.
	 */
	void processRemoteString(char *inp, int len);
	
	/*
	 *  Process text in local edit mode.
	 */
	void execLocalCommand(char cmd);
};


#endif