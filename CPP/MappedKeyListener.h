#ifndef _mappedkeylistener_h
#define _mappedkeylistener_h

#include "StringBuffer.h"

class MappedKeyListener
{
public:
	virtual void mappedKey(char *s, int len) = 0;
	virtual void command(char c) = 0;
	virtual int getPage() = 0;
	virtual int getCursorX() = 0;
	virtual int getCursorY() = 0;
	virtual void getStartFieldASCII(StringBuffer *sb) = 0;
};

#endif
