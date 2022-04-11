#ifndef _text_display_h
#define _text_display_h

#include "Attributes.h"
#include "Page.h"
#include "PaintSurface.h"
#include "StringBuffer.h"
#include "PageProtocol.h"
#include "SharedProtocol.h"
#include "ProtectPage.h"


class TextDisplay 
{
	StringBuffer *statusLine;
	
	int charWidth;      /* current width of a char */
	int charHeight;      /* current height of a char */
	int charDescent;      /* base line descent */	
	
	Page *displayPage;
	Page *writePage;
	Page **pages;
	int numPages;
	
	bool echoOn;
	bool blockMode;
	bool protectMode;
	
	bool requiresRepaint;
	
	PageProtocol *ppprotectMode;
	PageProtocol *ppunProtectMode;
	PageProtocol *ppconvMode;

	PageProtocol *ppRemote;
	
	bool keysLocked;
	
	int numRows, numColumns;
		
public:
	
	TextDisplay(int pageCount, int cols, int rows);
	virtual ~TextDisplay();

	bool needsRepaint()
	{
		return requiresRepaint;
	}
	
	void setRePaint()
	{
		requiresRepaint = true;
	}
	
	void writeBuffer(char *text)
	{
		_ASSERT(_CrtIsMemoryBlock(writePage, sizeof(Page), 0, 0, 0));
		writePage->writeBuffer(ppRemote, text);
	}
	
	void writeDisplay(char *text)
	{
		_ASSERT(_CrtIsMemoryBlock(displayPage, sizeof(Page), 0, 0, 0));
		displayPage->writeCursor(ppRemote, text);
		requiresRepaint = true;
	}
	
	void writeLocal(char *text)
	{
		_ASSERT(_CrtIsMemoryBlock(displayPage, sizeof(Page), 0, 0, 0));
		_ASSERT(_CrtIsValidHeapPointer(ppRemote));
		displayPage->writeCursorLocal(ppRemote, text);
		ppRemote->validateCursorPos(displayPage);
		requiresRepaint = true;
	}
	
	void echoDisplay(char *text)
	{
		_ASSERT(_CrtIsMemoryBlock(displayPage, sizeof(Page), 0, 0, 0));
		if (echoOn)
		{
			displayPage->writeCursor(ppRemote, text);
			requiresRepaint = true;
			return;
		}
		char c = text[0];
		if (c == 13)
		{
			char buf[3];
			buf[0] = c;
			buf[1] = 10;
			buf[2] = 0;
			displayPage->writeCursor(ppRemote, buf);
			requiresRepaint = true;
		}
	}
	
		/** ESC W
	 *  1.  Clear all pages to blanks
	 *  2.  Set video prior condition to NORMAL for all pages
	 *  3.  Select page 1
	 *  4.  Display page 1
	 *  5.  Set the buffer addess to (1,1) for all pages
	 *  6.  Set the cursor address to (1,1) for all pages
	 *  7.  Lock the keyboard
	 *  8.  Clear the status line display
	 *  9.  Reset insert mode
	 * 10.  Initialize datatype table
	 * 11.  Disable local line editing
	 */
	void setProtectMode();
	
	
	/** ESC X
	 *  1.  Clear all pages to blanks
	 *  2.  Set the video prior conditiion registers to NORMAL for all pages
	 *  3.  Select page 1
	 *  4.  Display page 1
	 *  5.  Set the buffer address to (1,1) for all pages
	 *  6.  Set the cursor address to (1,1) for all pages
	 *  7.  Lock the keyboard
	 *  8.  Clear the status line
	 *  9.  Reset insert mode
	 * 10.  Enable local line editing
	 * 11.  Clear all horizontal tab stops
	 */
	void exitProtectMode();
	
	void setKeysLocked()
	{
		keysLocked = true;
	}
	
	void setKeysUnlocked()
	{
		keysLocked = false;
	}
	
	/** ESC :
	 */
	void setPage(int page)
	{
		_ASSERT(_CrtIsMemoryBlock(pages, (numPages + 2) * sizeof(Page *), 0, 0, 0));
		_ASSERT(_CrtIsMemoryBlock(pages[page], sizeof(Page), 0, 0, 0));

		//if (page >= numPages)
		//{
		//	return;
		//}
		writePage = pages[page];
	}
	
	/** 0x07
	 */
	void bell()
	{
		// ding, ding, ding
	}
	
	/** 0x08
	 */
	void backspace()
	{
		_ASSERT(_CrtIsMemoryBlock(writePage, sizeof(Page), 0, 0, 0));

		writePage->backspace(ppRemote);
		requiresRepaint = true;
	}
	
	/** 0x09
	 */
	void tab()
	{
		_ASSERT(_CrtIsMemoryBlock(writePage, sizeof(Page), 0, 0, 0));

		writePage->tab(ppRemote, 1);
		requiresRepaint = true;
	}	
	
	/** 0x0A
	 */
	void linefeed()
	{
		_ASSERT(_CrtIsMemoryBlock(writePage, sizeof(Page), 0, 0, 0));

		writePage->cursorDown(ppRemote);
		requiresRepaint = true;
	}
	
	/** 0x0D
	 */
	void carageReturn()
	{
		_ASSERT(_CrtIsMemoryBlock(writePage, sizeof(Page), 0, 0, 0));

		writePage->carageReturn(ppRemote);
		requiresRepaint = true;
	}
	
	/** ESC J
	 */
	void setCursorRowCol(int row, int col)
	{
		_ASSERT(_CrtIsMemoryBlock(writePage, sizeof(Page), 0, 0, 0));
		_ASSERT(row < numRows && col < numColumns);
		_ASSERT(row >= 0 && col >= 0);

		writePage->setCursor(ppRemote, row, col);
		requiresRepaint = true;
	}
	
	void setBufferRowCol(int row, int col)
	{
		_ASSERT(_CrtIsMemoryBlock(writePage, sizeof(Page), 0, 0, 0));
		_ASSERT(row < numRows && col < numColumns);
		_ASSERT(row >= 0 && col >= 0);

		writePage->setBuffer(row, col);
		requiresRepaint = true;
	}

	void setVideoPriorCondition(int attr)
	{
		_ASSERT(_CrtIsMemoryBlock(writePage, sizeof(Page), 0, 0, 0));
		writePage->setVideoPriorCondition(attr);
	}

	void setInsertMode(int mode)
	{
		_ASSERT(_CrtIsMemoryBlock(writePage, sizeof(Page), 0, 0, 0));
		writePage->setInsertMode( mode );
	}
		
	/** ESC 0
	 */
	void printScreen()
	{
	}
	
	/** ESC 1
	 * 
	 *  Set a tab at the current cursor location
	 */
	void setTab();
	
	/** ESC 2
	 * 
	 *  Clear the tab at the current cursor location
	 */
	void clearTab();
	
	/** ESC 3
	 */
	void clearAllTabs();
		
	/** ESC i
	 */
	void backtab()
	{
		_ASSERT(_CrtIsMemoryBlock(displayPage, sizeof(Page), 0, 0, 0));
		displayPage->tab(ppRemote, -1);
		requiresRepaint = true;
	}
	
	/** ESC 6
	 * 
	 *  All subsuquent writes use this attribute
	 */
	void setWriteAttribute(int attr)
	{
		_ASSERT(_CrtIsMemoryBlock(writePage, sizeof(Page), 0, 0, 0));
		_ASSERT ( (attr & MASK_CHAR) == 0);
		writePage->setWriteAttribute(attr);
	}

	/** ESC 7
	 *  Not sure what this is supposed to do
	 */
	void setPriorWriteAttribute(int attr)
	{
		_ASSERT(_CrtIsMemoryBlock(writePage, sizeof(Page), 0, 0, 0));
		_ASSERT ( (attr & MASK_CHAR) == 0);
		writePage->setWriteAttribute(attr);
	}
	
	/** ESC ! or ESC ' '
	 */
	void setDisplayPage(int page)
	{
		//if (page >= numPages)
		//{
		//	return;
		//}
		_ASSERT(_CrtIsMemoryBlock(pages[page], sizeof(Page), 0, 0, 0));
		displayPage = pages[page];
		displayPage->forceDirty();
		requiresRepaint = true;
	}
	
	/** ESC A
	 */
	void moveCursorUp()
	{
		_ASSERT(_CrtIsMemoryBlock(writePage, sizeof(Page), 0, 0, 0));
		writePage->cursorUp(ppRemote);
		requiresRepaint = true;
	}
	
	/** ESC C
	 */
	void moveCursorRight()
	{
		_ASSERT(_CrtIsMemoryBlock(writePage, sizeof(Page), 0, 0, 0));
		writePage->cursorRight(ppRemote);
		requiresRepaint = true;
	}
	
	/** ESC H
	 */
	void home()
	{
		_ASSERT(_CrtIsMemoryBlock(writePage, sizeof(Page), 0, 0, 0));
		writePage->setCursor(ppRemote, 0, 0);
		requiresRepaint = true;
	}
	
	/** ESC F
	 */
	void end()
	{
		_ASSERT(_CrtIsMemoryBlock(writePage, sizeof(Page), 0, 0, 0));
		writePage->setCursor(ppRemote, numRows-1, 0);
		requiresRepaint = true;
	}
	
	/** ESC I
	 */
	void clearPage()
	{
		_ASSERT(_CrtIsMemoryBlock(writePage, sizeof(Page), 0, 0, 0));
		writePage->setWriteAttribute(VID_NORMAL);
		writePage->setVideoPriorCondition(VID_NORMAL);
		writePage->clearPage();
		requiresRepaint = true;
	}
	
	/** ESC J
	 */
	void clearToEnd()
	{
		_ASSERT(_CrtIsMemoryBlock(writePage, sizeof(Page), 0, 0, 0));
		writePage->clearToEOP(ppRemote);
		requiresRepaint = true;
	}
	
	/** ESC I
	 */
	void clearBlock(int startRow, int startCol, int endRow, int endCol)
	{
		_ASSERT(_CrtIsMemoryBlock(writePage, sizeof(Page), 0, 0, 0));
		writePage->clearBlock(ppRemote, startRow, startCol, endRow, endCol);
		requiresRepaint = true;
	}
	
	/** ESC K
	 *  In block mode, erase the field.  In
	 *  conversation mode, clear to end of line
	 */
	void clearEOL()
	{
		_ASSERT(_CrtIsMemoryBlock(writePage, sizeof(Page), 0, 0, 0));
		writePage->clearToEOL(ppRemote);
		requiresRepaint = true;
	}
	
	/** 0x1D
	 */
	void startField(int videoAttr, int dataAttr)
	{
		writeField(decodeVideoAttrs(videoAttr) | decodeDataAttrs(dataAttr) | ' ');
	}
	
	/** ESC [
	 */
	void startField(int videoAttr, int dataAttr, int keyAttr)
	{
		writeField(decodeVideoAttrs(videoAttr) | decodeDataAttrs(dataAttr) | decodeKeyAttrs(keyAttr) | ' ');
	}

	void readBufferAllMdt(StringBuffer *out, int startRow, int startCol, int endRow, int endCol)
	{
		readBuffer(out, DAT_MDT, 0, startRow, startCol, endRow, endCol);
	}
	
	void readBufferAllIgnoreMdt(StringBuffer *out, int startRow, int startCol, int endRow, int endCol)
	{
		readBuffer(out, 0, 0, startRow, startCol, endRow, endCol);
	}
	
	/** ESC - <
	 * 
	 * PROTECT MODE
	 *  Read all the unprotected fields in the block
	 *
	 * UNPROTECT MODE 
	 *  Return raw characters in the block
	 */
	void readBufferUnprotectIgnoreMdt(StringBuffer *out, int startRow, int startCol, int endRow, int endCol)
	{
		readBuffer(out, DAT_UNPROTECT, 0, startRow, startCol, endRow, endCol);
	}
	
	void readBufferUnprotect(StringBuffer *out, int startRow, int startCol, int endRow, int endCol)
	{
		readBuffer(out, DAT_UNPROTECT | DAT_MDT, 0, startRow, startCol, endRow, endCol);
	}

	/** ESC ]
	 * 
	 *  Read all the fields in the block (protected and unprotected)
	 */
	void readFieldsAll(StringBuffer *out, int startRow, int startCol, int endRow, int endCol)
	{
		readBuffer(out, 0, 0, startRow, startCol, endRow, endCol);
	}

	/** ESC >
	 *  reset all modified data tags for unprotected fields
	 */
	void resetMdt()
	{
		_ASSERT(_CrtIsMemoryBlock(writePage, sizeof(Page), 0, 0, 0));
		writePage->resetMDTs();
	}
	
	/** ESC O
	 */
	void insertChar()
	{
		_ASSERT(_CrtIsMemoryBlock(writePage, sizeof(Page), 0, 0, 0));
		writePage->insertChar(ppRemote);
		requiresRepaint = true;
	}
		
	/** ESC M
	 */			
	void setModeBlock()
	{
		blockMode = true;
		exitProtectMode();
		ppRemote = ppunProtectMode;
	}
	
	void setModeConv();

	/** ESC p
	 */
	void setPageCount(int count)
	{
		numPages = count;
	}
	
	/** ESC q
	 */
	void init();
	
	void writeStatus(char *msg);

	/** ESC o
	 *  
	 */
	void writeMessage(char *msg);

	void initDataTypeTable();
	
	int getNumColumns()
	{
		return numColumns;
	}
	
	int getNumRows()
	{
		return numRows;
	}
	
	int getCurrentPage();
	
	int getCursorCol()
	{
		_ASSERT(_CrtIsMemoryBlock(writePage, sizeof(Page), 0, 0, 0));
		return writePage->cursorPos.column + 1;
	}
	
	int getCursorRow()
	{
		_ASSERT(_CrtIsMemoryBlock(writePage, sizeof(Page), 0, 0, 0));
		return writePage->cursorPos.row + 1;
	}	

	int getBufferCol()
	{
		_ASSERT(_CrtIsMemoryBlock(writePage, sizeof(Page), 0, 0, 0));
		return writePage->bufferPos.column + 1;
	}
	
	int getBufferRow()
	{
		_ASSERT(_CrtIsMemoryBlock(writePage, sizeof(Page), 0, 0, 0));
		return writePage->bufferPos.row + 1;
	}
	
	bool getProtectMode()
	{
		return protectMode;
	}
	
	bool getBlockMode()
	{
		return blockMode;
	}
	
	void setEchoOn()
	{
		echoOn = true;
	}
	
	void setEchoOff()
	{
		echoOn = false;
	}

	
	/** ESC A
	 */
	void cursorUp()
	{
		_ASSERT(_CrtIsMemoryBlock(writePage, sizeof(Page), 0, 0, 0));
		writePage->cursorUp(ppRemote);
		requiresRepaint = true;
	}
	
	/** 0x0A
	 */
	void cursorDown()
	{
		_ASSERT(_CrtIsMemoryBlock(writePage, sizeof(Page), 0, 0, 0));
		writePage->cursorDown(ppRemote);
		requiresRepaint = true;
	}
	
	/** ESC C
	 */
	void cursorRight()
	{
		_ASSERT(_CrtIsMemoryBlock(writePage, sizeof(Page), 0, 0, 0));
		writePage->cursorRight(ppRemote);
		requiresRepaint = true;
	}
	
	/** ESC L
	 */
	void lineDown()
	{
		requiresRepaint = true;
	}

	/** ESC M
	 */
	void deleteLine()
	{
		requiresRepaint = true;
	}

	/** ESC O
	 * 
	 *  Insert a space
	 */
	void insert()
	{
		requiresRepaint = true;
	}
		
	void getStartFieldASCII(StringBuffer *sb)
	{
		_ASSERT(_CrtIsMemoryBlock(displayPage, sizeof(Page), 0, 0, 0));
		displayPage->getStartFieldASCII(sb);
	}
		
	/** ESC P
	 * Delete a character at a given position on the screen.
	 * All characters right to the position will be moved one to the left.
	 */
	void deleteChar()
	{
		_ASSERT(_CrtIsMemoryBlock(writePage, sizeof(Page), 0, 0, 0));
		writePage->deleteChar(ppRemote);
		requiresRepaint = true;
	}		
		
	void paint(PaintSurface *ps)
	{
		_ASSERT(_CrtIsMemoryBlock(displayPage, sizeof(Page), 0, 0, 0));
		displayPage->paint(ps, *statusLine);
		requiresRepaint = false;
	}
	
	void dumpScreen(StringBuffer *pw);
	
	void dumpAttibutes(StringBuffer *pw);

	/**
	 *  Get the 'index'nth field on the screen.
	 *  The first field is index ZERO.  If the
	 *  index is larger than the number of field,
	 *  an empty string is returned.
	 */
	void getField(int index, StringBuffer *accum);
	
	/**
	 *  Get the video, data, and key attributes for a
	 *  field.
	 */
	int getFieldAttributes(int index);

	/**
	 *  Get the text in the field at the cursor
	 *  position.
	 */
	void getCurrentField(StringBuffer *accum);
	
	/**
	 *  Get the 'index'nth unprotected field on 
	 *  the screen.  The first field is index 
	 *  ZERO.  If the index is larger than the 
	 *  number of field, an empty string is 
	 *  returned.
	 */
	void getUnprotectField(int index, StringBuffer *accum);
	
	/**
	 *  Write text into the 'index'nth 
	 *  unprotected field on the screen.  The 
	 *  first field is index ZERO.  If the 
	 *  index is larger than the number of field, 
	 *  the request is ignored.
	 */
	void setField(int index, char *text);
	
	/**
	 *  Returns true if the 'index'nth unprotected
	 *  field has its MDT set. The first field is 
	 *  index ZERO.  If the index is larger than 
	 *  the  number of fields, false is returned.
	 */
	bool isFieldChanged(int index);

	/**
	 *  Get a full line of display text.  
	 */
	void getLine(int lineNumber, StringBuffer *line);
	
	/**
	 *  Set the cursor at the start if the 
	 *  'index'nth unprotected field on the screen.  
	 *  The first field is index ZERO.  If the 
	 *  index is larger than the number of field, 
	 *  the request is ignored.
	 */
	void cursorToField(int index);
	
	void toHTML(COLORREF fg, COLORREF bg, StringBuffer *);

	void getSubString(int row, int col, int len, StringBuffer *sb)
	{
		_ASSERT(row < numRows && col+len < numColumns);

		for (int c = col; c < col+len; c++)
		{
			sb->append( (char)(displayPage->mem[row][c] & MASK_CHAR) );
		}
	}

private:

	void readBuffer(StringBuffer *out, int reqMask, int forbidMask, int startRow, int startCol, int endRow, int endCol)
	{
		_ASSERT(_CrtIsMemoryBlock(writePage, sizeof(Page), 0, 0, 0));
		writePage->readBuffer(out, ppRemote, reqMask, forbidMask, startRow, startCol, endRow, endCol);
	}
	
	/** 0x08
	 * PROTECT MODE
	 *  Move to the start of the field.  If the cursor
	 *  is already at the start, move the first position
	 *  of the previous unprotected field.
	 *
	 * If the new cursor position is protected,
	 * move to the last position of the previous
	 * unprotected field.
	 *
	 * UNPROTECT MODE
	 *  Move to previous tab.  If no prev tab exists
	 *  on the current row, move to first column.  If
	 *  already on first column, move to last tab on 
	 *  previous row.  If the cursor is in (1,1), move
	 *  to the right most tab of the last row
	 */
	void cursorLeft()
	{
		_ASSERT(_CrtIsMemoryBlock(writePage, sizeof(Page), 0, 0, 0));
		writePage->cursorLeft(ppRemote);
		requiresRepaint = true;
	}

	void writeField(int c)
	{
		_ASSERT(_CrtIsMemoryBlock(writePage, sizeof(Page), 0, 0, 0));
		writePage->writeField(c);
	}

	void clearAll();

	int decodeKeyAttrs(int attr)
	{
		int ret = 0;
		if (attr == 0)
		{
			return 0;
		}		
		_ASSERT ( (attr & (1<<6)) != 0);
		if ((attr & (1<<0)) != 0)
		{
			ret |= KEY_UPSHIFT;
		}
		if ((attr & (1<<1)) != 0)
		{
			ret |= KEY_KB_ONLY;
		}
		if ((attr & (1<<2)) != 0)
		{
			ret |= KEY_AID_ONLY;
		}
		if ((attr & (1<<3)) != 0)
		{
			ret |= KEY_EITHER;
		}
		if (ret == 0 && (ret & ~(1<<6)) != 0) 
		{
			//System.out.println("Unknown video attr " + attr);
		}
		return ret;
	}
	
	int decodeDataAttrs(int attr)
	{
		int ret = 0;
		if (attr == 0)
		{
			return 0;
		}
		//ASSERT.fatal ( (attr & (1<<6)) != 0, "TextDisplay", 367, "Invalid attribute format");
		if ((attr & (1<<0)) != 0)
		{
			ret |= DAT_MDT;
		}
		if ((attr & (1<<4)) != 0)
		{
			ret |= DAT_AUTOTAB;
		}
		if ((attr & (1<<5)) != 0)
		{
			ret |= DAT_UNPROTECT;
		}
		int type = attr & ((1<<1)|(1<<2)|(1<<3));
		ret |= type<<SHIFT_DAT_TYPE;
		_ASSERT( (attr & ~((1<<6)|(1<<5)|(1<<4)|(1<<0)|(1<<1)|(1<<2)|(1<<3))) == 0);
		return ret;
	}
	
	int decodeVideoAttrs(int attr)
	{
		int ret = 0;
		if (attr == 0 || attr == 32)
		{
			return 0;
		}
		//ASSERT.fatal ( (attr & (1<<5)) != 0, "TextDisplay", 367, "Invalid attribute format");
		
		if ((attr & (1<<0)) != 0)
		{
			ret |= VID_NORMAL;
		}
		if ((attr & (1<<1)) != 0)
		{
			ret |= VID_BLINKING;
		}
		if ((attr & (1<<2)) != 0)
		{
			ret |= VID_REVERSE;
		}
		if ((attr & (1<<3)) != 0)
		{
			ret |= VID_INVIS;
		}
		if ((attr & (1<<4)) != 0)
		{
			ret |= VID_UNDERLINE;
		}
		if ( (attr & ~((1<<5)|(1<<0)|(1<<1)|(1<<2)|(1<<3)|(1<<4))) != 0) 
		{
			//System.out.println("Unknown video attr " + attr);
		}
		return ret;
	}	
};

#endif
