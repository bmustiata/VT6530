#ifndef _page_h
#define _page_h

#include "Attributes.h"
#include "Cursor.h"
#include "PaintSurface.h"
#include "PageProtocol.h"


class Page
{	
protected:
	char chbuf[2];
	
	int numRows, numColumns;

	int writeAttr;
	int priorAttr;
	int insertMode;
	
	bool cursorBlock;

public:

	int **mem;

	Cursor cursorPos;
	Cursor bufferPos;

	Page(int numRows, int numCols);

	virtual ~Page();
	
	void init();

	inline int getNumRows()
	{
		return numRows;
	}

	inline int getNumColumns()
	{
		return numColumns;
	}

	void writeBuffer(PageProtocol *mode, char *text);
	
	void writeCursor(PageProtocol *mode, char *text);

	void writeCursorLocal(PageProtocol *mode, char *text);
	
	void carageReturn(PageProtocol *mode);
	
	void setCursor(PageProtocol *mode, int row, int col);
	
	void setBuffer(int row, int col);
	
	void setVideoPriorCondition(int attr)
	{
		priorAttr = attr;
	}
	
	void setWriteAttribute(int attr)
	{
		writeAttr = attr;
	}
	
	int getWriteAttr()
	{
		return writeAttr;
	}

	int getPriorAttr()
	{
		return priorAttr;
	}

	void insertChar(PageProtocol *mode);
	
	void setInsertMode(int mode)
	{
		insertMode = mode;
	}

	void deleteChar(PageProtocol *mode);
	
	void tab(PageProtocol *mode, int inc);
	
	void backspace(PageProtocol *mode);
	
	void cursorUp(PageProtocol *mode);
	
	void cursorDown(PageProtocol *mode);
	
	void cursorLeft(PageProtocol *mode);
	
	void cursorRight(PageProtocol *mode);
	
	void clearPage();
	
	void clearToEOP(PageProtocol *mode)
	{
		_ASSERT(_CrtIsValidHeapPointer(mode));
		mode->clearToEOP(this);
	}
	
	void clearToEOL(PageProtocol *mode)
	{
		_ASSERT(_CrtIsValidHeapPointer(mode));
		mode->clearToEOL(this);
	}
	
	void clearBlock(PageProtocol *mode, int startRow, int startCol, int endRow, int endCol)
	{
		_ASSERT(_CrtIsValidHeapPointer(mode));
		mode->clearBlock(this, startRow, startCol, endRow, endCol);
	}
	
	void writeField(int c);
	
	void readBuffer(StringBuffer *out, PageProtocol *mode, int reqMask, int forbidMask, int startRow, int startCol, int endRow, int endCol)
	{
		_ASSERT(_CrtIsValidHeapPointer(mode));
		mode->readBuffer(out, this, reqMask, forbidMask, startRow, startCol, endRow, endCol);
	}
	
	void resetMDTs();
	
	void getStartFieldASCII(StringBuffer *);

	void paint(PaintSurface *ps, char *statusLine);

	void forceDirty();
	
	void scrollPageUp();

	int scanForNextField(int c, int r, int inc);
	
	int scanForUnprotectField(int c, int r, int inc);

	inline void setColors(PaintSurface *ps, int ch, COLORREF fgcolor, COLORREF bgcolor, COLORREF fgbright, HBRUSH foreground, HBRUSH background, HPEN pen, HPEN revpen, int r, int c, int charWidth, int charHeight)
	{
		if ( ((ch & VID_REVERSE) != 0) && ((ch & VID_BLINKING) != 0) ) 
		{ 
			//HBRUSH color = bg; 
			//bg = fg;
			//fg = color;
			ps->setBkColor(fgbright);
			ps->setTextColor(bgcolor);
			ps->setPen(revpen);
			ps->fillRect(c * charWidth, r * charHeight, charWidth, charHeight, foreground);
		}
		else if ((ch & VID_REVERSE) != 0)
		{
			ps->setBkColor(fgcolor);
			ps->setTextColor(bgcolor);
			ps->setPen(revpen);
			ps->fillRect(c * charWidth, r * charHeight, charWidth, charHeight, foreground);
		}
		else if ((ch & VID_BLINKING) != 0)
		{
			ps->setBkColor(bgcolor);
			ps->setTextColor(fgbright);	
			ps->setPen(pen);
			ps->fillRect(c * charWidth, r * charHeight, charWidth, charHeight, background);
		}
		else
		{
			ps->setBkColor(bgcolor);
			ps->setTextColor(fgcolor);
			ps->fillRect(c * charWidth, r * charHeight, charWidth, charHeight, background);
		}
	}
};


#endif