#ifndef _protect_page_h
#define _protect_page_h


#include "SharedProtocol.h"
#include "Page.h"


class ProtectPage : public SharedProtocol
{
	virtual void home(Page *page);

	virtual void end(Page *page);

	/** 0x09
	 * PROTECT
	 *  Move to the first position of the next 
	 *  unprotected field.
	 */
	virtual void tab(Page *page, int inc);
	
	virtual void clearToEOL(Page *page);
	
	virtual void clearToEOP(Page *page);
	
	virtual void cursorLeft(Page *page);

	virtual void readBuffer(StringBuffer *accum, Page *page, int reqMask, int forbidMask, int startRow, int startCol, int endRow, int endCol);

	virtual void writeChar(Page *page, Cursor *bufferPos, int c);

	virtual void linefeed(Page *page);

	virtual void validateCursorPos(Page *page);
	
	virtual void setCursor(Page *page, int row, int col);

	virtual void clearBlock(Page *page, int startRow, int startCol, int endRow, int endCol);

	virtual void arrowDown(Page *page, Cursor *cursor);
	
	virtual void arrowUp(Page *page, Cursor *cursor);

	virtual void arrowLeft(Page *page, Cursor *cursor);
	
	virtual void arrowRight(Page *page, Cursor *cursor);
};

class UnprotectPage : public SharedProtocol
{
	virtual void tab(int inc)
	{
		/** 0x09
		 * 
		 * UNPROTECT MODE
		 *  Move the the next tab stop on the row.  If the
		 *  cursor is past the last tab stop, move to 
		 *  column 1 of the next row.
		 */
	}
	
	virtual void clearToEOL(Page *page)
	{
		_ASSERT(_CrtIsMemoryBlock(page, sizeof(Page), 0, 0, 0));
		_ASSERT(_CrtIsMemoryBlock(page->mem, page->getNumRows() * sizeof(int *), 0, 0, 0));

		for (int x = page->cursorPos.column; x < page->getNumColumns(); x++)
		{
			page->mem[page->cursorPos.row][x] = (int)' ';
		}
	}

	virtual void readBuffer(StringBuffer *accum, Page *page, int reqMask, int forbidMask, int startRow, int startCol, int endRow, int endCol)
	{
		_ASSERT(_CrtIsMemoryBlock(page, sizeof(Page), 0, 0, 0));
		_ASSERT(_CrtIsMemoryBlock(page->mem, page->getNumRows() * sizeof(int *), 0, 0, 0));

		for (int y = startRow-1; y < endRow-1; y++)
		{
			_ASSERT(_CrtIsMemoryBlock(page->mem[y], page->getNumColumns() * sizeof(int), 0, 0, 0));

			for (int x = startCol-1; x < endCol-1; x++)
			{
				accum->append( (char)(page->mem[y][x] & 0xFF) );
			}
			accum->append((char)13);
		}
	}

};

#endif