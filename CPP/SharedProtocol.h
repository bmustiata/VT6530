#ifndef _shared_protocol_h
#define _shared_protocol_h

#include "Cursor.h"
#include "PageProtocol.h"
#include "Page.h"


class SharedProtocol : public PageProtocol
{
public:
	virtual void writeBuffer(Page *page, char *text)
	{
		_ASSERT(_CrtIsMemoryBlock(page, sizeof(Page), 0, 0, 0));
		_ASSERT(_CrtIsMemoryBlock(page->mem, page->getNumRows() * sizeof(int *), 0, 0, 0));

		write(&page->bufferPos, page, page->getWriteAttr() | page->getPriorAttr(), text);
	}
	
	virtual void writeCursor(Page *page, char *text)
	{
		_ASSERT(_CrtIsMemoryBlock(page, sizeof(Page), 0, 0, 0));
		_ASSERT(_CrtIsMemoryBlock(page->mem, page->getNumRows() * sizeof(int *), 0, 0, 0));

		write(&(page->cursorPos), page, page->getWriteAttr() | page->getPriorAttr(), text);
	}

	virtual void writeChar (Page *page, Cursor *cursor, int c);
	
	virtual void arrowDown(Page *page, Cursor *cursor)
	{
		_ASSERT(_CrtIsMemoryBlock(page, sizeof(Page), 0, 0, 0));
		_ASSERT(_CrtIsMemoryBlock(page->mem, page->getNumRows() * sizeof(int *), 0, 0, 0));
		_ASSERT(_CrtIsValidHeapPointer(cursor));

		page->mem[cursor->row][cursor->column] |= CHAR_CELL_DIRTY;
		cursor->row++;
		cursor->adjustRow();
		page->mem[cursor->row][cursor->column] |= CHAR_CELL_DIRTY;
	}
	
	virtual void arrowUp(Page *page, Cursor *cursor)
	{
		_ASSERT(_CrtIsMemoryBlock(page, sizeof(Page), 0, 0, 0));
		_ASSERT(_CrtIsMemoryBlock(page->mem, page->getNumRows() * sizeof(int *), 0, 0, 0));
		_ASSERT(_CrtIsValidPointer(cursor, sizeof(Cursor), TRUE));

		page->mem[cursor->row][cursor->column] |= CHAR_CELL_DIRTY;
		cursor->row--;
		cursor->adjustRow();
		page->mem[cursor->row][cursor->column] |= CHAR_CELL_DIRTY;
	}

	virtual void arrowLeft(Page *page, Cursor *cursor)
	{
		_ASSERT(_CrtIsMemoryBlock(page, sizeof(Page), 0, 0, 0));
		_ASSERT(_CrtIsMemoryBlock(page->mem, page->getNumRows() * sizeof(int *), 0, 0, 0));
		_ASSERT(_CrtIsValidHeapPointer(cursor));

		page->mem[cursor->row][cursor->column] |= CHAR_CELL_DIRTY;
		cursor->column--;
		cursor->adjustCol();
		page->mem[cursor->row][cursor->column] |= CHAR_CELL_DIRTY;
	}
	
	virtual void arrowRight(Page *page, Cursor *cursor)
	{
		_ASSERT(_CrtIsMemoryBlock(page, sizeof(Page), 0, 0, 0));
		_ASSERT(_CrtIsMemoryBlock(page->mem, page->getNumRows() * sizeof(int *), 0, 0, 0));
		_ASSERT(_CrtIsValidHeapPointer(cursor));

		page->mem[cursor->row][cursor->column] |= CHAR_CELL_DIRTY;
		cursor->column++;
		cursor->adjustCol();
		page->mem[cursor->row][cursor->column] |= CHAR_CELL_DIRTY;
	}

	virtual void home(Page *page)
	{
		_ASSERT(_CrtIsMemoryBlock(page, sizeof(Page), 0, 0, 0));
		_ASSERT(_CrtIsMemoryBlock(page->mem, page->getNumRows() * sizeof(int *), 0, 0, 0));
	}

	virtual void end(Page *page)
	{
		_ASSERT(_CrtIsMemoryBlock(page, sizeof(Page), 0, 0, 0));
		_ASSERT(_CrtIsMemoryBlock(page->mem, page->getNumRows() * sizeof(int *), 0, 0, 0));
	}

	virtual void validateCursorPos(Page *page)
	{
		_ASSERT(_CrtIsMemoryBlock(page, sizeof(Page), 0, 0, 0));
		_ASSERT(_CrtIsMemoryBlock(page->mem, page->getNumRows() * sizeof(int *), 0, 0, 0));
	}
	
	virtual void insertChar(Page *page)
	{
		_ASSERT(_CrtIsMemoryBlock(page, sizeof(Page), 0, 0, 0));
		_ASSERT(_CrtIsMemoryBlock(page->mem, page->getNumRows() * sizeof(int *), 0, 0, 0));
	}
	
	virtual void deleteChar(Page *page)
	{
		_ASSERT(_CrtIsMemoryBlock(page, sizeof(Page), 0, 0, 0));
		_ASSERT(_CrtIsMemoryBlock(page->mem, page->getNumRows() * sizeof(int *), 0, 0, 0));
	}
	
	void backspace(Page *page)
	{
		_ASSERT(_CrtIsMemoryBlock(page, sizeof(Page), 0, 0, 0));
		_ASSERT(_CrtIsMemoryBlock(page->mem, page->getNumRows() * sizeof(int *), 0, 0, 0));

		page->mem[page->cursorPos.row][page->cursorPos.column] |= CHAR_CELL_DIRTY;
		page->cursorPos.column--;
		page->cursorPos.adjustCol();
		page->mem[page->cursorPos.row][page->cursorPos.column] = ' ' | (page->mem[page->cursorPos.row][page->cursorPos.column] & MASK_FIELD) | CHAR_CELL_DIRTY;		

		_ASSERT(page->cursorPos.row < page->getNumRows());
		_ASSERT(page->cursorPos.column < page->getNumColumns());
	}
	
	virtual void tab(Page *page, int inc)
	{
		_ASSERT(_CrtIsMemoryBlock(page, sizeof(Page), 0, 0, 0));
		_ASSERT(_CrtIsMemoryBlock(page->mem, page->getNumRows() * sizeof(int *), 0, 0, 0));
	}
	
	virtual void carageReturn(Page *page)
	{
		_ASSERT(_CrtIsMemoryBlock(page, sizeof(Page), 0, 0, 0));
		_ASSERT(_CrtIsMemoryBlock(page->mem, page->getNumRows() * sizeof(int *), 0, 0, 0));

		page->mem[page->cursorPos.row][page->cursorPos.column] |= CHAR_CELL_DIRTY;
		page->cursorPos.column = 0;
		page->mem[page->cursorPos.row][page->cursorPos.column] |= CHAR_CELL_DIRTY;

		_ASSERT(page->cursorPos.row < page->getNumRows());
		_ASSERT(page->cursorPos.column < page->getNumColumns());
	}
	
	virtual void linefeed(Page *page)
	{
		_ASSERT(_CrtIsMemoryBlock(page, sizeof(Page), 0, 0, 0));
		_ASSERT(_CrtIsMemoryBlock(page->mem, page->getNumRows() * sizeof(int *), 0, 0, 0));

		page->mem[page->cursorPos.row][page->cursorPos.column] |= CHAR_CELL_DIRTY;
		
		if (page->cursorPos.row == page->getNumRows()-1)
		{
			page->scrollPageUp();
		}
		else
		{
			page->cursorPos.row++;
		}
		page->mem[page->cursorPos.row][page->cursorPos.column] |= CHAR_CELL_DIRTY;
		
		_ASSERT(page->cursorPos.row < page->getNumRows());
		_ASSERT(page->cursorPos.column < page->getNumColumns());
	}
	
	virtual void cursorRight(Page *page)
	{
		_ASSERT(_CrtIsMemoryBlock(page, sizeof(Page), 0, 0, 0));
		_ASSERT(_CrtIsMemoryBlock(page->mem, page->getNumRows() * sizeof(int *), 0, 0, 0));

		page->mem[page->bufferPos.row][page->bufferPos.column] |= CHAR_CELL_DIRTY;
		if (++page->cursorPos.column >= page->getNumColumns())
		{
			page->cursorPos.column = 0;
			if (page->cursorPos.row == page->getNumRows()-1)
			{
				page->scrollPageUp();
			}
			else
			{
				page->cursorPos.row++;
				page->cursorPos.adjustRow();
			}
		}
		_ASSERT(page->cursorPos.row < page->getNumRows());
		_ASSERT(page->cursorPos.column < page->getNumColumns());
	}

	virtual void cursorLeft(Page *page)
	{
		_ASSERT(_CrtIsMemoryBlock(page, sizeof(Page), 0, 0, 0));
		_ASSERT(_CrtIsMemoryBlock(page->mem, page->getNumRows() * sizeof(int *), 0, 0, 0));

		page->cursorPos.column--;
		if (page->cursorPos.column < 0)
		{
			page->cursorPos.column = 0;
			if (page->cursorPos.row == 0)
			{
				page->cursorPos.row = page->getNumRows() -1;
			}
			else
			{
				page->cursorPos.row--;
			}
		}
		_ASSERT(page->cursorPos.row < page->getNumRows());
		_ASSERT(page->cursorPos.column < page->getNumColumns());
	}
		
	virtual void cursorUp(Page *page)
	{
		_ASSERT(_CrtIsMemoryBlock(page, sizeof(Page), 0, 0, 0));
		_ASSERT(_CrtIsMemoryBlock(page->mem, page->getNumRows() * sizeof(int *), 0, 0, 0));

		page->mem[page->cursorPos.row][page->cursorPos.column] |= CHAR_CELL_DIRTY;
		page->cursorPos.row++;
		page->cursorPos.adjustRow();
		page->mem[page->cursorPos.row][page->cursorPos.column] |= CHAR_CELL_DIRTY;

		_ASSERT(page->cursorPos.row < page->getNumRows());
		_ASSERT(page->cursorPos.column < page->getNumColumns());
	}
	
	virtual void cursorDown(Page *page)
	{
		_ASSERT(_CrtIsMemoryBlock(page, sizeof(Page), 0, 0, 0));
		_ASSERT(_CrtIsMemoryBlock(page->mem, page->getNumRows() * sizeof(int *), 0, 0, 0));

		page->mem[page->cursorPos.row][page->cursorPos.column] |= CHAR_CELL_DIRTY;
		if (page->cursorPos.row == page->getNumRows()-1)
		{
			page->scrollPageUp();
		}
		else
		{
			page->cursorPos.row++;
			page->mem[page->cursorPos.row][page->cursorPos.column] |= CHAR_CELL_DIRTY;
		}
		_ASSERT(page->cursorPos.row < page->getNumRows());
		_ASSERT(page->cursorPos.column < page->getNumColumns());
	}
	
	virtual void clearToEOL(Page *page)
	{
		_ASSERT(_CrtIsMemoryBlock(page, sizeof(Page), 0, 0, 0));
		_ASSERT(_CrtIsMemoryBlock(page->mem, page->getNumRows() * sizeof(int *), 0, 0, 0));
	}
	
	virtual void clearBlock(Page *page, int startRow, int startCol, int endRow, int endCol)
	{
		_ASSERT(_CrtIsMemoryBlock(page, sizeof(Page), 0, 0, 0));
		_ASSERT(_CrtIsMemoryBlock(page->mem, page->getNumRows() * sizeof(int *), 0, 0, 0));
	}

	virtual void clearToEOP(Page *page);
	
	virtual void readBuffer(StringBuffer *sb, Page *page, int reqMask, int forbidMask, int startRow, int startCol, int endRow, int endCol)
	{
	}

	virtual void setCursor(Page *page, int row, int col)
	{
		_ASSERT(_CrtIsMemoryBlock(page, sizeof(Page), 0, 0, 0));
		_ASSERT(_CrtIsMemoryBlock(page->mem, page->getNumRows() * sizeof(int *), 0, 0, 0));

		page->mem[page->cursorPos.row][page->cursorPos.column] |= CHAR_CELL_DIRTY;
		page->cursorPos.row = row;
		page->cursorPos.column = col;
		page->mem[page->cursorPos.row][page->cursorPos.column] |= CHAR_CELL_DIRTY;

		_ASSERT(page->cursorPos.row < page->getNumRows());
		_ASSERT(page->cursorPos.column < page->getNumColumns());
	}

protected:
	
	void write(Cursor *pos, Page *page, int attribute, char *text);		
};

#endif