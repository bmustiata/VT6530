#include "stdafx.h"
#include "StringBuffer.h"

StringBuffer::StringBuffer(BSTR str)
{
	len = SysStringLen(str) +1;

	if ((buf = new char[len]) == NULL)
	{
		throw OutOfMemoryException("Out of memory in StringBuffer");
	}
	for (used = 0; used < len; used++)
	{
		buf[used] = (char)str[used];
	}
	buf[used] = '\0';
}

void StringBuffer::trim()
{
	_ASSERT(_CrtIsMemoryBlock(buf, len, 0, 0, 0));
	for (int x = used-1; x >= 0; x--)
	{
		if (buf[x] != ' ')
		{
			break;
		}
		buf[x] = '\0';
	}
	used = x+1;

	_ASSERT(buf[used] == '\0');
}

void StringBuffer::append(char *str, int len)
{
	_ASSERT(_CrtIsMemoryBlock(buf, StringBuffer::len, 0, 0, 0));
	for (int x = 0; x < len; x++)
	{
		append(str[x]);
	}
}

/*
 *  Append characters to the end of the buffer.  Extend
 *  the buffer if needed.
 *  Throws OutOfMemoryException
 */
void StringBuffer::append(char *str)
{
	_ASSERT(_CrtIsMemoryBlock(buf, len, 0, 0, 0));

	int alen = strlen(str);

	while (alen + used + 1 > len)
	{
		extend();
	}
	for (int x = 0; x < alen; x++)
	{
		buf[used + x] = str[x];
	}
	buf[used + x] = '\0';
	used += alen;

	_ASSERT(buf[used] == '\0');
}

/**
 *  Append a string representation of the integer
 */
void StringBuffer::append(int i)
{
	_ASSERT(_CrtIsMemoryBlock(buf, len, 0, 0, 0));
	_ASSERT(buf[used] == '\0');

	char ibuf[50];
	wsprintf(ibuf, "%d", i);
	append(ibuf);

	_ASSERT(buf[used] == '\0');
}

/**
 *  Insert a string at the index.  If the index is
 *  after EOS, the string is extended as necessary.
 */
void StringBuffer::insert(int index, char *str)
{
	_ASSERT(buf[used] == '\0');
	_ASSERT(_CrtIsMemoryBlock(buf, len, 0, 0, 0));

	extendTo(index);
	int alen = strlen(str);

	for (int x = 0; x < alen; x++)
	{
		if (index + x + 1 >= len)
		{
			extend();
		}
		buf[index + x] = str[x];
	}
	if (index + alen >= used)
	{
		used = index+alen+1;
		buf[used] = '\0';
	}
	_ASSERT(buf[used] == '\0');
}

bool StringBuffer::isNumeric()
{
	_ASSERT(buf[used] == '\0');
	_ASSERT(_CrtIsMemoryBlock(buf, len, 0, 0, 0));

	for (int x = 0; x < used; x++)
	{
		if (! isdigit(buf[x]))
		{
			return false;
		}
	}
	return true;
}

void StringBuffer::extendTo(int index)
{
	_ASSERT(_CrtIsMemoryBlock(buf, len, 0, 0, 0));

	while (index > len)
	{
		extend();
		_ASSERT(_CrtIsMemoryBlock(buf, len, 0, 0, 0));
	}
	if (index > used)
	{
		for (int x = used; x < index; x++)
		{
			buf[x] = ' ';
		}
		buf[x] = '\0';
	}
	_ASSERT(buf[used] == '\0');
	_ASSERT(_CrtIsMemoryBlock(buf, len, 0, 0, 0));
}

void StringBuffer::extend()
{
	_ASSERT(_CrtIsMemoryBlock(buf, len, 0, 0, 0));

	int newlen = len*2;
	char *newbuf;

	if ((newbuf = new char[newlen]) == NULL)
	{
		throw OutOfMemoryException("Out of memory in StringBuffer");
	}
	CopyMemory(newbuf, buf, len);
	len = newlen;
	delete[] buf;
	buf = newbuf;
	_ASSERT(_CrtIsMemoryBlock(buf, len, 0, 0, 0));
}
