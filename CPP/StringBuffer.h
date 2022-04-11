#ifndef _string_buffer_h
#define _string_buffer_h

#include <ctype.h>
#include "OutOfMemoryException.h"


class StringBuffer
{
	char *buf;
	int used;
	int len;

public:

	StringBuffer()
	{
		if ((buf = new char[10]) == NULL)
		{
			throw OutOfMemoryException("Out of memory in StringBuffer");
		}
		used = 0;
		len = 10;
		buf[0] = '\0';
	}

	StringBuffer(int size)
	{
		if ((buf = new char[size]) == NULL)
		{
			throw OutOfMemoryException("Out of memory in StringBuffer");
		}
		used = 0;
		len = size;
		buf[0] = '\0';
	}

	StringBuffer(BSTR str);

	void trim();

	void setLength(int l)
	{
		if (l < 0)
		{
			return;
		}
		_ASSERT(_CrtIsMemoryBlock(buf, len, 0, 0, 0));
		if (l > len)
		{
			extendTo(l);
		}
		used = l;
		buf[used] = '\0';
	}

	void append(char *str, int len);

	/*
	 *  Append characters to the end of the buffer.  Extend
	 *  the buffer if needed.
	 *  Throws OutOfMemoryException
	 */
	void append(char *str);

	void append(char c)
	{
		_ASSERT(_CrtIsMemoryBlock(buf, len, 0, 0, 0));

		if (used + 1 >= len)
		{
			extend();
		}
		buf[used] = c;
		buf[used + 1] = '\0';
		used++;

		_ASSERT(buf[used] == '\0');
	}

	/**
	 *  Append a string representation of the integer
	 */
	void append(int i);

	inline int length()
	{
		_ASSERT(_CrtIsMemoryBlock(buf, len, 0, 0, 0));
		return used;
	}

	inline operator char *()
	{
		_ASSERT(_CrtIsMemoryBlock(buf, len, 0, 0, 0));
		return buf;
	}

	char& operator[](int index)
	{
		_ASSERT(_CrtIsMemoryBlock(buf, len, 0, 0, 0));

		if (index > used)
		{
			return buf[0];
		}
		return buf[index];
	}

	void operator = (char *str)
	{
		_ASSERT(_CrtIsMemoryBlock(buf, len, 0, 0, 0));
		used = 0;
		append(str);
	}

	void setCharAt(int index, char c)
	{
		_ASSERT(_CrtIsMemoryBlock(buf, len, 0, 0, 0));
		_ASSERT(buf[used] == '\0');

		extendTo(index);
		buf[index] = c;

		if (index == used)
		{
			if (used == len)
			{
				extend();
			}
			buf[index+1] = '\0';
			used++;
		}
		_ASSERT(buf[used] == '\0');
		_ASSERT(_CrtIsMemoryBlock(buf, len, 0, 0, 0));
	}

	/**
	 *  Insert a string at the index.  If the index is
	 *  after EOS, the string is extended as necessary.
	 */
	void insert(int index, char *str);

	bool isNumeric();

private:

	void extendTo(int index);

	void extend();
};

#endif