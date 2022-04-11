#ifndef _page_protocol_h
#define _page_protocol_h

#include "Attributes.h"
#include "Cursor.h"
#include "StringBuffer.h"

class Page;

#define MODE_DISPLAY = 0
#define MODE_PROTECT = 1
#define MODE_UNPROTECT = 2


class PageProtocol
{
public:
	virtual void writeBuffer(Page *page, char *text) = 0;
	virtual void writeCursor(Page *page, char *text) = 0;
	virtual void writeChar(Page *page, Cursor *cursor, int c) = 0;
	virtual void insertChar(Page *page) = 0;
	virtual void deleteChar(Page *page) = 0;
	virtual void backspace(Page *page) = 0;
	virtual void tab(Page *page, int inc) = 0;
	virtual void carageReturn(Page *page) = 0;
	virtual void linefeed(Page *page) = 0;
	virtual void home(Page *page) = 0;
	virtual void end(Page *page) = 0;
	
	virtual void validateCursorPos(Page *page) = 0;
	virtual void setCursor(Page *page, int row, int col) = 0;
	virtual void cursorLeft(Page *page) = 0;
	virtual void cursorRight(Page *page) = 0;
	virtual void cursorDown(Page *page) = 0;
	virtual void cursorUp(Page *page) = 0;

	virtual void clearToEOL(Page *page) = 0;
	virtual void clearToEOP(Page *page) = 0;
	virtual void clearBlock(Page *page, int startRow, int startCol, int endRow, int endCol) = 0;
	
	virtual void readBuffer(StringBuffer *out, Page *page, int reqMask, int forbidMask, int startRow, int startCol, int endRow, int endCol) = 0;
};

#endif