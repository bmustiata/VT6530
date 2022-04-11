
#include <windows.h>
#include "stdafx.h"
#include "SharedProtocol.h"
#include "Keys.h"

void SharedProtocol::clearToEOP(Page *page)
{
	_ASSERT(_CrtIsMemoryBlock(page, sizeof(Page), 0, 0, 0));
	_ASSERT(_CrtIsMemoryBlock(page->mem, page->getNumRows() * sizeof(int *), 0, 0, 0));
	_ASSERT(_CrtIsMemoryBlock(page->mem[page->cursorPos.row], page->getNumColumns() * sizeof(int), 0, 0, 0));

	for (int c = page->cursorPos.column; c < page->getNumColumns(); c++)
	{
		page->mem[page->cursorPos.row][c] = (int)' ' | CHAR_CELL_DIRTY | page->getWriteAttr() | page->getPriorAttr();
	}
	for (int r = page->cursorPos.row+1; r < page->getNumRows(); r++)
	{
		_ASSERT(_CrtIsMemoryBlock(page->mem[r], page->getNumColumns() * sizeof(int), 0, 0, 0));
		for (int c = 0; c < page->getNumColumns(); c++)
		{
			page->mem[r][c] = ' ' | CHAR_CELL_DIRTY | page->getWriteAttr() | page->getPriorAttr();
		}
	}
}


void SharedProtocol::writeChar (Page *page, Cursor *cursor, int c)
{
	_ASSERT(_CrtIsMemoryBlock(page, sizeof(Page), 0, 0, 0));
	_ASSERT(_CrtIsMemoryBlock(page->mem, page->getNumRows() * sizeof(int *), 0, 0, 0));
	_ASSERT(_CrtIsValidPointer(cursor, sizeof(Cursor), TRUE));
	_ASSERT(cursor->row < page->getNumRows());
	_ASSERT(cursor->column < page->getNumColumns());

	switch ((char)(c & MASK_CHAR))
	{
		case '\t':
			tab(page, 1);
			break;
		case '\r':
			linefeed(page);
			break;
		case '\n':
			carageReturn(page);
			break;;
		case '\b':
			backspace(page);
			break;
		case (char)11:
			cursorUp(page);
			break;
		case SPC_DEL:
			deleteChar(page);
			break;
		case SPC_DOWN:
			arrowDown(page, cursor);
			break;
		case SPC_END:
			end(page);
			break;
		case SPC_HOME:
			home(page);
			break;
		case SPC_INS:
			insertChar(page);
			break;
		case SPC_LEFT:
			arrowLeft(page, cursor);
			break;
		case SPC_PGDN:
			break;
		case SPC_PGUP:
			break;
		case SPC_PRINTSCR:
			break;
		case SPC_RIGHT:
			arrowRight(page, cursor);
			break;
		case SPC_UP:
			arrowUp(page, cursor);
			break;
		default:
			_ASSERT(_CrtIsMemoryBlock(page->mem[cursor->row], page->getNumColumns() * sizeof(int), 0, 0, 0));
			if ( (page->mem[cursor->row][cursor->column] & KEY_UPSHIFT) != 0)
			{
				page->mem[cursor->row][cursor->column] = (page->getWriteAttr() | page->getPriorAttr() | (c & MASK_FIELD) | toupper((char)(c&0xFF)) | (page->mem[cursor->row][cursor->column] & MASK_FIELD) | CHAR_CELL_DIRTY);
			}
			else
			{
				page->mem[cursor->row][cursor->column] = (c | (page->mem[cursor->row][cursor->column] & MASK_FIELD)) | (CHAR_CELL_DIRTY | page->getPriorAttr());
			}
			cursor->column++;
			cursor->adjustCol();
			page->mem[cursor->row][cursor->column] |= CHAR_CELL_DIRTY;
			break;
	}
	validateCursorPos(page);
}

void SharedProtocol::write(Cursor *pos, Page *page, int attribute, char *text)
{
	_ASSERT(_CrtIsMemoryBlock(page, sizeof(Page), 0, 0, 0));
	_ASSERT(_CrtIsMemoryBlock(page->mem, page->getNumRows() * sizeof(int *), 0, 0, 0));
	_ASSERT(attribute == 0 || (attribute & MASK_CHAR) == 0);

	int len = strlen(text);
	for (int x = 0; x < len; x++)
	{
		//page->mem[pos.row][pos.column] = text.charAt(x) | attribute | CHAR_CELL_DIRTY;
		//c = text.charAt(x);
		//System.out.print((char)(c & MASK_CHAR));		
		//c |= attribute;
		//System.out.print((char)(c & MASK_CHAR));
		writeChar(page, pos, text[x] | attribute);
		if (pos->column == page->getNumColumns()-1)
		{
			return;
		}
	}
	_ASSERT(pos->row < page->getNumRows());
	_ASSERT(pos->column < page->getNumColumns());
}

