#include "stdafx.h"
#include "ProtectPage.h"

void ProtectPage::home(Page *page)
{
	_ASSERT(_CrtIsMemoryBlock(page, sizeof(Page), 0, 0, 0));
}

void ProtectPage::end(Page *page)
{
	_ASSERT(_CrtIsMemoryBlock(page, sizeof(Page), 0, 0, 0));
}

/** 0x09
 * PROTECT
 *  Move to the first position of the next 
 *  unprotected field.
 */
void ProtectPage::tab(Page *page, int inc)
{
	_ASSERT(_CrtIsMemoryBlock(page, sizeof(Page), 0, 0, 0));

	int newX = page->scanForNextField(page->cursorPos.column+inc, page->cursorPos.row, inc);
	if (newX >= 0)
	{
		newX = page->scanForUnprotectField(newX, page->cursorPos.row, inc);
		if (newX >= 0)
		{
			page->cursorPos.column = newX;
			return;
		}
	}
	int y = page->cursorPos.row+inc;
	for (int qpr = 0; qpr < page->getNumRows(); qpr += inc)
	{
		if (inc > 0)
		{
			if (y >= page->getNumRows())
			{
				y = 0;
			}
		}
		else
		{
			if (y < 0)
			{
				y = page->getNumRows()-1;
			}
		}
		_ASSERT(y < page->getNumRows());
		newX = page->scanForUnprotectField(0, y, inc);
		if (newX >= 0)
		{
			page->cursorPos.column = newX;
			page->cursorPos.row = y;
			_ASSERT(page->cursorPos.row < page->getNumRows());
			_ASSERT(page->cursorPos.column < page->getNumColumns());
			return;
		}
		//y++;
		y += inc;
	}
	_ASSERT(page->cursorPos.row < page->getNumRows());
	_ASSERT(page->cursorPos.column < page->getNumColumns());
}

void ProtectPage::clearToEOL(Page *page)
{
	_ASSERT(_CrtIsMemoryBlock(page, sizeof(Page), 0, 0, 0));
	_ASSERT(_CrtIsMemoryBlock(page->mem, page->getNumRows() * sizeof(int *), 0, 0, 0));

	int x = page->bufferPos.column;
	int y = page->bufferPos.row;
	
	_ASSERT(_CrtIsMemoryBlock(page->mem[y], page->getNumColumns() * sizeof(int), 0, 0, 0));

	int attr = page->mem[y][x] & MASK_FIELD;
	
	if ((page->mem[y][x] & CHAR_START_FIELD) != 0)
	{
		// clear the video attributes for the field
		attr = VID_NORMAL;
		page->mem[y][x] ^= MASK_VID;
		page->mem[y][x] |= CHAR_CELL_DIRTY | VID_NORMAL;
		x++;
	}
	attr |= CHAR_CELL_DIRTY | (int)' ';
	while (x < page->getNumColumns() && (page->mem[y][x] & CHAR_START_FIELD) == 0)
	{
		page->mem[y][x] = attr;
		x++;
	}
	if (x != page->getNumColumns()) //((page->mem[y][x] & CHAR_START_FIELD) != 0)
	{
		return;
	}
	for (y = y+1; y < page->getNumRows(); y++)
	{
		_ASSERT(_CrtIsMemoryBlock(page->mem[y], page->getNumColumns() * sizeof (int), 0, 0, 0));

		x = 0;
		while (x < page->getNumColumns() && (page->mem[y][x] & CHAR_START_FIELD) == 0)
		{
			page->mem[y][x] = attr;
			x++;
		}
		if (x < page->getNumColumns())
		{
			if ((page->mem[y][x] & CHAR_START_FIELD) != 0)
			{
				break;
			}
		}
	}
	_ASSERT(page->bufferPos.row < page->getNumRows());
	_ASSERT(page->bufferPos.column < page->getNumColumns());
}

void ProtectPage::clearToEOP(Page *page)
{
	_ASSERT(_CrtIsMemoryBlock(page, sizeof(Page), 0, 0, 0));
	_ASSERT(_CrtIsMemoryBlock(page->mem, page->getNumRows() * sizeof(int *), 0, 0, 0));

	Cursor *cursor = &page->bufferPos;
	
	_ASSERT(_CrtIsValidHeapPointer(cursor));

	int attr = page->getWriteAttr() | page->getPriorAttr();
	
	_ASSERT(_CrtIsValidHeapPointer(page->mem[cursor->row]));

	for (int c = cursor->column; c < page->getNumColumns(); c++)
	{
		if ( (page->mem[cursor->row][c] & DAT_UNPROTECT) != 0)
		{
			page->mem[cursor->row][c] = (' ' | CHAR_CELL_DIRTY);
			page->mem[cursor->row][c] |= (attr | DAT_UNPROTECT);
		}
	}
	for (int r = cursor->row+1; r < page->getNumRows(); r++)
	{
		_ASSERT(_CrtIsValidHeapPointer(page->mem[r]));

		for (int c = 0; c < page->getNumColumns(); c++)
		{
			if ( (page->mem[r][c] & DAT_UNPROTECT) != 0)
			{
				page->mem[r][c] = (' ' | CHAR_CELL_DIRTY);
				page->mem[r][c] |= (attr | DAT_UNPROTECT);
			}
		}
	}
}

void ProtectPage::cursorLeft(Page *page)
{
	_ASSERT(_CrtIsMemoryBlock(page, sizeof(Page), 0, 0, 0));
	_ASSERT(_CrtIsMemoryBlock(page->mem, page->getNumRows() * sizeof(int *), 0, 0, 0));
	_ASSERT(_CrtIsMemoryBlock(page->mem[page->cursorPos.row], page->getNumColumns() * sizeof(int), 0, 0, 0));

	page->mem[page->cursorPos.row][page->cursorPos.column] |= CHAR_CELL_DIRTY;
	
	if (page->cursorPos.column == 0)
	{
		if (page->cursorPos.row == 0)
		{
			page->cursorPos.row = page->getNumRows()-1;
		}
		else
		{
			page->cursorPos.row--;
			page->cursorPos.adjustRow();
		}
		page->cursorPos.column = page->getNumColumns()-1;
	}
	else
	{
		page->cursorPos.column--;
		page->cursorPos.adjustCol();
	}
	if ( (page->mem[page->cursorPos.row][page->cursorPos.column] & DAT_UNPROTECT) == 0)
	{
		tab(page, -1);
	}
	_ASSERT(page->cursorPos.row < page->getNumRows());
	_ASSERT(page->cursorPos.column < page->getNumColumns());
}

void ProtectPage::readBuffer(StringBuffer *accum, Page *page, int reqMask, int forbidMask, int startRow, int startCol, int endRow, int endCol)
{
	_ASSERT(_CrtIsMemoryBlock(page, sizeof(Page), 0, 0, 0));
	_ASSERT(_CrtIsMemoryBlock(page->mem, page->getNumRows() * sizeof(int *), 0, 0, 0));

	bool writeCr = false;
	StringBuffer sb;
	
	_ASSERT(startRow >= 0 && endRow < page->getNumRows());
	_ASSERT(startCol >= 0 && endCol < page->getNumColumns());
	
	accum->setLength(0);
	
	for (int y = startRow; y <= endRow; y++)
	{
		_ASSERT(_CrtIsMemoryBlock(page->mem[y], page->getNumColumns() * sizeof(int), 0, 0, 0));

		bool fieldStarted = false;
		
		for (int x = startCol; x < endCol; x++)
		{
			if ((page->mem[y][x] & CHAR_START_FIELD) != 0)
			{
				if ( (page->mem[y][x+1] & reqMask) != 0)
				{
					fieldStarted = true;
					accum->append((char)17);
					accum->append((char)(y + 0x20));
					accum->append((char)(x + 0x21));
				}
			}
			else if ((page->mem[y][x] & (reqMask)) != 0 && fieldStarted)
			{
				sb.append((char)(page->mem[y][x] & MASK_CHAR));
				writeCr = true;
			}
			else if (fieldStarted)
			{
				sb.trim();
				if (sb.length() > 0)
				{
					accum->append(sb);
				}
				sb.setLength(0);
				writeCr = false;
			}
		}
		if (writeCr == true)
		{
			sb.trim();
			if (sb.length() > 0)
			{
				accum->append(sb);
			}
			writeCr = false;
			sb.setLength(0);
		}
	}
	accum->append((char)4);
}

void ProtectPage::writeChar(Page *page, Cursor *bufferPos, int c)
{
	_ASSERT(_CrtIsMemoryBlock(page, sizeof(Page), 0, 0, 0));
	_ASSERT(_CrtIsMemoryBlock(page->mem, page->getNumRows() * sizeof(int *), 0, 0, 0));
	_ASSERT(_CrtIsMemoryBlock(page->mem[bufferPos->row], page->getNumColumns() * sizeof(int), 0, 0, 0));
	_ASSERT(bufferPos->row < page->getNumRows());
	_ASSERT(bufferPos->column < page->getNumColumns());

	if ( (c & 0xFF) < 32)
	{
		SharedProtocol::writeChar(page, bufferPos, c);
		return;
	}
	int *row = page->mem[bufferPos->row];

	_ASSERT((row[bufferPos->column] & CHAR_START_FIELD) == 0);
		
	row[bufferPos->column] = (row[bufferPos->column] & MASK_FIELD) | c | page->getWriteAttr() | CHAR_CELL_DIRTY | page->getPriorAttr();
	bufferPos->column++;
	bufferPos->adjustCol();
	
	page->mem[bufferPos->row][bufferPos->column] |= CHAR_CELL_DIRTY;
}

void ProtectPage::linefeed(Page *page)
{
	tab(page, 1);
}

void ProtectPage::validateCursorPos(Page *page)
{
	_ASSERT(_CrtIsMemoryBlock(page, sizeof(Page), 0, 0, 0));
	_ASSERT(_CrtIsMemoryBlock(page->mem, page->getNumRows() * sizeof(int *), 0, 0, 0));
	_ASSERT(_CrtIsMemoryBlock(page->mem[page->cursorPos.row], page->getNumColumns() * sizeof(int), 0, 0, 0));
	_ASSERT(page->cursorPos.row < page->getNumRows());
	_ASSERT(page->cursorPos.column < page->getNumColumns());
	
	if ( (page->mem[page->cursorPos.row][page->cursorPos.column] & DAT_UNPROTECT) == 0)
	{
		tab(page, 1);
	}
	_ASSERT(page->cursorPos.row < page->getNumRows());
	_ASSERT(page->cursorPos.column < page->getNumColumns());
}

void ProtectPage::setCursor(Page *page, int row, int col)
{
	_ASSERT(_CrtIsMemoryBlock(page, sizeof(Page), 0, 0, 0));
	_ASSERT(_CrtIsMemoryBlock(page->mem, page->getNumRows() * sizeof(int *), 0, 0, 0));

	page->mem[page->cursorPos.row][page->cursorPos.column] |= CHAR_CELL_DIRTY;
	page->cursorPos.row = row;
	page->cursorPos.column = col;
	validateCursorPos(page);
	page->mem[page->cursorPos.row][page->cursorPos.column] |= CHAR_CELL_DIRTY;

	_ASSERT(page->cursorPos.row < page->getNumRows());
	_ASSERT(page->cursorPos.column < page->getNumColumns());
}

void ProtectPage::clearBlock(Page *page, int startRow, int startCol, int endRow, int endCol)
{
	_ASSERT(_CrtIsMemoryBlock(page, sizeof(Page), 0, 0, 0));
	_ASSERT(_CrtIsMemoryBlock(page->mem, page->getNumRows() * sizeof(int *), 0, 0, 0));

	int value = CHAR_CELL_DIRTY | ' ';
	int mask = MASK_FIELD ^ CHAR_START_FIELD;
	
	for (int y = startRow; y <= endRow; y++)
	{
		_ASSERT(_CrtIsValidHeapPointer(page->mem[y]));
		for (int x = startCol; x <= endCol; x++)
		{
			page->mem[y][x] = (page->mem[y][x] & mask) | value;
		}
	}
	validateCursorPos(page);
}

void ProtectPage::arrowDown(Page *page, Cursor *cursor)
{
	_ASSERT(_CrtIsMemoryBlock(page, sizeof(Page), 0, 0, 0));
	_ASSERT(_CrtIsMemoryBlock(page->mem, page->getNumRows() * sizeof(int *), 0, 0, 0));
	tab(page, 1);
}

void ProtectPage::arrowUp(Page *page, Cursor *cursor)
{
	_ASSERT(_CrtIsMemoryBlock(page, sizeof(Page), 0, 0, 0));
	_ASSERT(_CrtIsMemoryBlock(page->mem, page->getNumRows() * sizeof(int *), 0, 0, 0));
	tab(page, -1);
}

void ProtectPage::arrowLeft(Page *page, Cursor *cursor)
{
	_ASSERT(_CrtIsMemoryBlock(page, sizeof(Page), 0, 0, 0));
	_ASSERT(_CrtIsMemoryBlock(page->mem, page->getNumRows() * sizeof(int *), 0, 0, 0));
	_ASSERT(_CrtIsMemoryBlock(page->mem[cursor->row], page->getNumColumns() * sizeof(int), 0, 0, 0));

	page->mem[cursor->row][cursor->column] |= CHAR_CELL_DIRTY;
	cursor->column--;
	cursor->adjustCol();
	if ( (page->mem[cursor->row][cursor->column] & DAT_UNPROTECT) == 0)
	{
		tab(page, -1);
	}
	page->mem[cursor->row][cursor->column] |= CHAR_CELL_DIRTY;
}

void ProtectPage::arrowRight(Page *page, Cursor *cursor)
{
	_ASSERT(_CrtIsMemoryBlock(page, sizeof(Page), 0, 0, 0));
	_ASSERT(_CrtIsMemoryBlock(page->mem, page->getNumRows() * sizeof(int *), 0, 0, 0));

	page->mem[cursor->row][cursor->column] |= CHAR_CELL_DIRTY;
	cursor->column++;
	cursor->adjustCol();
	if ( (page->mem[cursor->row][cursor->column] & DAT_UNPROTECT) == 0)
	{
		tab(page, 1);
	}
	page->mem[cursor->row][cursor->column] |= CHAR_CELL_DIRTY;
}
