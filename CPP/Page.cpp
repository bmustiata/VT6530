
#include "stdafx.h"
#include "Page.h"

Page::Page(int numRows, int numCols)
	: cursorPos(numRows, numCols), bufferPos(numRows, numCols)
{
	_ASSERT(numRows > 0 && numCols > 0);
	numColumns = numCols;
	this->numRows = numRows;
	mem = new int *[numRows];
	for (int x = 0; x < numRows; x++)
	{
		mem[x] = new int[numColumns];
	}
	chbuf[1] = '\0';

	writeAttr = VID_NORMAL;
	priorAttr = VID_NORMAL;
	insertMode = INSERT_INSERT;
	
	cursorBlock = true;

	init();
}

Page::~Page()
{
	_ASSERT(_CrtIsMemoryBlock(mem, numRows*sizeof(int *), 0, 0, 0));

	for (int x = 0; x < numRows; x++)
	{
		_ASSERT(_CrtIsMemoryBlock(mem[x], numColumns*sizeof(int), 0, 0, 0));
		delete[] mem[x];
	}
	delete[] mem;
}

void Page::init()
{	
	_ASSERT(_CrtIsMemoryBlock(mem, numRows * sizeof(int *), 0, 0, 0));

	writeAttr = VID_NORMAL;
	priorAttr = VID_NORMAL;
	cursorPos.clear();
	bufferPos.clear();

	for (int x = 0; x < numRows; x++)
	{
		_ASSERT(_CrtIsMemoryBlock(mem[x], numColumns * sizeof(int), 0, 0, 0));

		for (int q = 0; q < numColumns; q++)
		{
			mem[x][q] = (int)' ' | CHAR_CELL_DIRTY | writeAttr;
		}
	}
}

void Page::insertChar(PageProtocol *mode)
{
	_ASSERT(_CrtIsValidHeapPointer(mode));
	mode->insertChar(this);
	_ASSERT(bufferPos.row < numRows);
	_ASSERT(bufferPos.column < numColumns);
	_ASSERT(cursorPos.row < numRows);
	_ASSERT(cursorPos.column < numColumns);
}

void Page::deleteChar(PageProtocol *mode)
{
	_ASSERT(_CrtIsValidHeapPointer(mode));
	mode->deleteChar(this);
	_ASSERT(bufferPos.row < numRows);
	_ASSERT(bufferPos.column < numColumns);
	_ASSERT(cursorPos.row < numRows);
	_ASSERT(cursorPos.column < numColumns);
}

void Page::tab(PageProtocol *mode, int inc)
{
	_ASSERT(_CrtIsValidHeapPointer(mode));
	mode->tab(this, inc);
	_ASSERT(bufferPos.row < numRows);
	_ASSERT(bufferPos.column < numColumns);
	_ASSERT(cursorPos.row < numRows);
	_ASSERT(cursorPos.column < numColumns);
}

void Page::backspace(PageProtocol *mode)
{
	_ASSERT(_CrtIsValidHeapPointer(mode));
	mode->backspace(this);
	_ASSERT(bufferPos.row < numRows);
	_ASSERT(bufferPos.column < numColumns);
	_ASSERT(cursorPos.row < numRows);
	_ASSERT(cursorPos.column < numColumns);
}

void Page::cursorUp(PageProtocol *mode)
{
	_ASSERT(_CrtIsValidHeapPointer(mode));
	mode->cursorUp(this);
	_ASSERT(bufferPos.row < numRows);
	_ASSERT(bufferPos.column < numColumns);
	_ASSERT(cursorPos.row < numRows);
	_ASSERT(cursorPos.column < numColumns);
}

void Page::cursorDown(PageProtocol *mode)
{
	_ASSERT(_CrtIsValidHeapPointer(mode));
	mode->cursorDown(this);
	_ASSERT(bufferPos.row < numRows);
	_ASSERT(bufferPos.column < numColumns);
	_ASSERT(cursorPos.row < numRows);
	_ASSERT(cursorPos.column < numColumns);
}

void Page::cursorLeft(PageProtocol *mode)
{
	_ASSERT(_CrtIsValidHeapPointer(mode));
	mode->cursorLeft(this);
	_ASSERT(bufferPos.row < numRows);
	_ASSERT(bufferPos.column < numColumns);
	_ASSERT(cursorPos.row < numRows);
	_ASSERT(cursorPos.column < numColumns);
}

void Page::cursorRight(PageProtocol *mode)
{
	_ASSERT(_CrtIsValidHeapPointer(mode));
	mode->cursorRight(this);
	_ASSERT(bufferPos.row < numRows);
	_ASSERT(bufferPos.column < numColumns);
	_ASSERT(cursorPos.row < numRows);
	_ASSERT(cursorPos.column < numColumns);
}

void Page::clearPage()
{
	_ASSERT(bufferPos.row < numRows);
	_ASSERT(bufferPos.column < numColumns);
	_ASSERT(cursorPos.row < numRows);
	_ASSERT(cursorPos.column < numColumns);
	
	_ASSERT(_CrtIsMemoryBlock(mem, numRows*sizeof(int *), 0, 0, 0));

	for (int r = 0; r < numRows; r++)
	{
		_ASSERT(_CrtIsMemoryBlock(mem[r], numColumns * sizeof(int), 0, 0, 0));

		for (int c = 0; c < numColumns; c++)
		{
			mem[r][c] = (int)' ' | CHAR_CELL_DIRTY | writeAttr | priorAttr;
		}
	}
}

void Page::writeBuffer(PageProtocol *mode, char *text)
{
	_ASSERT(_CrtIsValidHeapPointer(mode));
	mode->writeBuffer(this, text);
	_ASSERT(bufferPos.row < numRows);
	_ASSERT(bufferPos.column < numColumns);
	_ASSERT(cursorPos.row < numRows);
	_ASSERT(cursorPos.column < numColumns);
}

void Page::writeCursor(PageProtocol *mode, char *text)
{
	_ASSERT(_CrtIsValidHeapPointer(mode));
	mode->writeCursor(this, text);
	_ASSERT(bufferPos.row < numRows);
	_ASSERT(bufferPos.column < numColumns);
	_ASSERT(cursorPos.row < numRows);
	_ASSERT(cursorPos.column < numColumns);
}

void Page::writeCursorLocal(PageProtocol *mode, char *text)
{
	_ASSERT(_CrtIsValidHeapPointer(mode));

	int len = strlen(text);

	for (int x = 0; x < len; x++)
	{
		mode->writeChar(this, &cursorPos, text[x] | DAT_MDT);
	}
}


void Page::carageReturn(PageProtocol *mode)
{
	_ASSERT(_CrtIsValidHeapPointer(mode));
	mode->carageReturn(this);
	_ASSERT(bufferPos.row < numRows);
	_ASSERT(bufferPos.column < numColumns);
	_ASSERT(cursorPos.row < numRows);
	_ASSERT(cursorPos.column < numColumns);
}

void Page::setCursor(PageProtocol *mode, int row, int col)
{
	_ASSERT(_CrtIsValidHeapPointer(mode));
	mode->setCursor(this, row, col);
	_ASSERT(bufferPos.row < numRows);
	_ASSERT(bufferPos.column < numColumns);
	_ASSERT(cursorPos.row < numRows);
	_ASSERT(cursorPos.column < numColumns);
}

void Page::setBuffer(int row, int col)
{
	bufferPos.row = row;
	bufferPos.column = col;
	_ASSERT(bufferPos.row < numRows);
	_ASSERT(bufferPos.column < numColumns);
	_ASSERT(cursorPos.row < numRows);
	_ASSERT(cursorPos.column < numColumns);
}

void Page::writeField(int c)
{
	_ASSERT(_CrtIsMemoryBlock(mem, numRows*sizeof(int *), 0, 0, 0));
	_ASSERT(_CrtIsMemoryBlock(mem[bufferPos.row], numColumns*sizeof(int), 0, 0, 0));

	// mark the field
	mem[bufferPos.row][bufferPos.column] = c | CHAR_START_FIELD | CHAR_CELL_DIRTY | writeAttr | priorAttr;
	mem[bufferPos.row][bufferPos.column] &= ~DAT_UNPROTECT;
	
	_ASSERT((mem[bufferPos.row][bufferPos.column] & DAT_UNPROTECT) == 0);

	bufferPos.column++;
	bufferPos.adjustCol();
			
	int col;
	
	// detect any previous field on the line and extend
	// its attributes upto this one
	for (col = bufferPos.column; col < numColumns; col++)
	{
		if ( (mem[bufferPos.row][col] & CHAR_START_FIELD) != 0)
		{
			return;
		}
		mem[bufferPos.row][col] = (c & MASK_FIELD) | (mem[bufferPos.row][col] & MASK_CHAR) | (CHAR_CELL_DIRTY | writeAttr | priorAttr);
		_ASSERT( (mem[bufferPos.row][col] & CHAR_START_FIELD) == 0);
	}
	for (int y = bufferPos.row + 1; y < numRows; y++)
	{
		_ASSERT(_CrtIsMemoryBlock(mem[y], numColumns*sizeof(int), 0, 0, 0));

		for (int x = 0; x < numColumns; x++)
		{
			if ((mem[y][x] & CHAR_START_FIELD) != 0)
			{
				return;
			}
			mem[y][x] = ((c & MASK_FIELD) | (mem[y][x] & MASK_CHAR));
			mem[y][x] |= (CHAR_CELL_DIRTY | writeAttr | priorAttr);
			_ASSERT( (mem[y][x] & CHAR_START_FIELD) == 0);
		}
	}
}

void Page::resetMDTs()
{
	_ASSERT(_CrtIsMemoryBlock(mem, numRows*sizeof(int *), 0, 0, 0));

	for (int r = 0; r < numRows; r++)
	{
		_ASSERT(_CrtIsMemoryBlock(mem[r], numColumns*sizeof(int), 0, 0, 0));
		for (int c = 0; c < numColumns; c++)
		{
			mem[r][c] &= ~DAT_MDT;
		}
	}
}

void Page::getStartFieldASCII(StringBuffer *buf)
{
	_ASSERT(_CrtIsMemoryBlock(mem, numRows*sizeof(int *), 0, 0, 0));

	for (int r = 0; r < numRows; r++)
	{
		_ASSERT(_CrtIsMemoryBlock(mem[r], numColumns*sizeof(int), 0, 0, 0));

		for (int c = 0; c < numColumns; c++)
		{
			int c2 = c+1;
			int r2 = r;
			if (c2 >= numColumns)
			{
				c2 = 0;
				r2++;
				if (r2 >= numRows)
				{
					r2 = 0;
				}
			}
			if ((mem[r][c] & CHAR_START_FIELD) != 0 && (mem[r2][c2] & DAT_UNPROTECT) != 0)
			{
				buf->append((char)(r + 0x20));
				buf->append((char)(c + 0x21));
				return;
			}
		}
	}
	buf->append("  ");
}

void Page::forceDirty()
{
	_ASSERT(_CrtIsMemoryBlock(mem, numRows*sizeof(int *), 0, 0, 0));
	
	for (int r = 0; r < numRows; r++)
	{
		_ASSERT(_CrtIsMemoryBlock(mem[r], numColumns*sizeof(int), 0, 0, 0));
		for (int c = 0; c < numColumns; c++)
		{
			mem[r][c] |= CHAR_CELL_DIRTY;
		}
	}
}

void Page::scrollPageUp()
{
	_ASSERT(_CrtIsMemoryBlock(mem, numRows*sizeof(int *), 0, 0, 0));

	for (int r = 0; r < numRows -1; r++)
	{
		_ASSERT(_CrtIsMemoryBlock(mem[r], numColumns*sizeof(int), 0, 0, 0));
		for (int c = 0; c < numColumns; c++)
		{
			mem[r][c] = (mem[r+1][c] | CHAR_CELL_DIRTY);
		}
	}
	for (int c = 0; c < numColumns; c++)
	{
		mem[numRows-1][c] = (' ' | CHAR_CELL_DIRTY);
		mem[numRows-1][c] |= (writeAttr | priorAttr);
	}
	_ASSERT(cursorPos.row < numRows);
	_ASSERT(cursorPos.column < numColumns);
}

int Page::scanForNextField(int c, int r, int inc)
{
	_ASSERT(r < numRows);
	_ASSERT(c < numColumns);
	_ASSERT(_CrtIsMemoryBlock(mem, numRows*sizeof(int *), 0, 0, 0));
	_ASSERT(_CrtIsMemoryBlock(mem[r], numColumns*sizeof(int), 0, 0, 0));

	if (inc > 0)
	{
		for (int x = c; x < numColumns; x++)
		{
			if ((mem[r][x] & CHAR_START_FIELD) != 0)
			{
				_ASSERT((mem[r][x] & DAT_UNPROTECT) == 0);
				return x;
			}
		}
	}
	else
	{
		if (c == 0)
		{
			c = numColumns-1;
			if (r == 0)
			{
				r = numRows-1;
			}
			else
			{
				r--;
			}
		}
		for (int x = c; x >= 0; x--)
		{
			if ((mem[r][x] & CHAR_START_FIELD) != 0)
			{
				_ASSERT((mem[r][x] & DAT_UNPROTECT) == 0);
				return x;
			}
		}
	}
	return -1;
}

int Page::scanForUnprotectField(int c, int r, int inc)
{
	_ASSERT(r < numRows);
	_ASSERT(c < numColumns);
	_ASSERT(_CrtIsMemoryBlock(mem, numRows*sizeof(int), 0, 0, 0));
	_ASSERT(_CrtIsMemoryBlock(mem[r], numColumns*sizeof(int), 0, 0, 0));

	if (inc > 0)
	{
		for (int x = c; x < numColumns; x++)
		{
			if ((mem[r][x] & DAT_UNPROTECT) != 0)
			{
				_ASSERT((mem[r][x] & CHAR_START_FIELD) == 0);
				return x;
			}
		}
	}
	else
	{
		if (c == 0)
		{
			c = numColumns-1;
			if (r == 0)
			{
				r = numRows-1;
			}
			else
			{
				r--;
			}
		}
		for (int x = c; x >= 0; x--)
		{
			if ((mem[r][x] & DAT_UNPROTECT) != 0)
			{
				_ASSERT((mem[r][x] & CHAR_START_FIELD) == 0);
				return x;
			}
		}			
	}
	return -1;
}

static COLORREF brighter(COLORREF c)
{
	int r = GetRValue(c);
	r += (int)(r * .1F);
	int g = GetGValue(c);
	g += (int)(g * .1F);
	int b = GetBValue(c);
	b += (int)(b * .1F);
	return RGB((r>0xFF)?0xFF:r, (g>0xFF)?0xFF:g, (b>0xFF)?0xFF:b);
}

void Page::paint(PaintSurface *ps, char *statusLine)
{
	_ASSERT(_CrtIsMemoryBlock(mem, numRows*sizeof(int *), 0, 0, 0));

	COLORREF fgcolor = ps->getForeGroundColor();
	COLORREF bgcolor = ps->getBackGroundColor();
	COLORREF fgbright = brighter(fgcolor);
	HBRUSH foreground = CreateSolidBrush(fgcolor);
	HPEN pen = CreatePen(PS_SOLID, 1, ps->getForeGroundColor());
	HPEN revpen = CreatePen(PS_SOLID, 1, bgcolor);
	HBRUSH background = CreateSolidBrush(bgcolor);

	int charWidth = ps->getFontWidth();
	int charDescent = ps->getFontDescent();
	int charHeight = ps->getFontHeight()/*+charDescent*/;
	
	//HBRUSH fg, bg;
	boolean allClean = true;
	
	HPEN oldpen = ps->setPen(pen);
	ps->setPaintMode();

	for (int r = 0; r < numRows; r++)
	{
		_ASSERT(_CrtIsMemoryBlock(mem[r], numColumns*sizeof(int), 0, 0, 0));

		for(int c = 0; c < numColumns; c++)
		{
			int ch = mem[r][c];
			if ( (ch & CHAR_CELL_DIRTY) == 0)
			{
				continue;
			}
			mem[r][c] &= ~CHAR_CELL_DIRTY;
			allClean = false;
			//fg = foreground;
			//bg = background;

			setColors(ps, ch, fgcolor, bgcolor, fgbright, foreground, background, pen, revpen, r, c, charWidth, charHeight);
			// clear the part of the screen we want to change (fill rectangle)
			//ps->fillRect(c * charWidth, r * charHeight, charWidth, charHeight, bg);
			
			// draw the characters
			if ( (ch & VID_INVIS) == 0)
			{
				chbuf[0] = (char)(ch & MASK_CHAR);
				if (chbuf[0] != ' ')
				{
					ps->drawBytes(chbuf, 0, 1, c * charWidth, (r+1) * charHeight /*- charDescent*/);
				}
			}
			if(((ch & VID_UNDERLINE) != 0) && ((ch & CHAR_START_FIELD) == 0))
			{
				int liney = (r+1) * charHeight - charDescent/3;
				int linex = c * charWidth;
				ps->drawLine(linex, liney, linex + charWidth, liney);
			}
		}
	}
	if (allClean)
	{
		for (int y = 0; y < numRows; y++)
		{
			_ASSERT(_CrtIsMemoryBlock(mem[y], numColumns*sizeof(int), 0, 0, 0));
			for (int x = 0; x < numColumns; x++)
			{
				mem[y][x] |= CHAR_CELL_DIRTY;
			}
		}
		paint(ps, statusLine);
		return;
	}
	else
	{
		// draw cursor
		ps->setPaintXorMode();

		if (cursorBlock)
		{
			int ch = mem[cursorPos.row][cursorPos.column];
			//ps->fillRect( cursorPos.column * charWidth, 
			//			cursorPos.row * charHeight,
			//			charWidth, charHeight, foreground);
			setColors(ps, ch, bgcolor, fgcolor, bgcolor, background, foreground, revpen, pen, cursorPos.row, cursorPos.column, charWidth, charHeight);

			chbuf[0] = (char)(ch & MASK_CHAR);
			if (chbuf[0] != ' ')
			{
				ps->drawBytes(chbuf, 0, 1, cursorPos.column * charWidth, (cursorPos.row+1) * charHeight /*- charDescent*/);
			}
		}
		else
		{
			ps->drawLine(cursorPos.column * charWidth ,
					   (cursorPos.row) * charHeight + charHeight,
						cursorPos.column * charWidth + charWidth,
					   (cursorPos.row) * charHeight + charHeight); 
		}
		ps->setPaintMode();
		
		// draw the status line
		ps->setBkColor(bgcolor);
		ps->setTextColor(fgcolor);
		ps->fillRect(0, numRows * charHeight, charWidth*numColumns, charHeight, background);
		
		_ASSERT(strlen(statusLine) >= numColumns);
		for (int x = 0; x < numColumns; x++)
		{
			int c = statusLine[x];
			if (c == 27)
			{
				int attr = statusLine[++x];
				c = statusLine[++x];
			}
			chbuf[0] = (char)(c & MASK_CHAR);
			ps->drawBytes(chbuf, 0, 1, x*charWidth, (numRows+1) * charHeight /*- charDescent*/);			
		}
	}
	ps->setPen( oldpen );
	DeleteObject( foreground );
	DeleteObject( pen );
	DeleteObject( revpen );
	DeleteObject( background );
}
