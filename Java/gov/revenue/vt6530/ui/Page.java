package gov.revenue.vt6530.ui;

import java.awt.Graphics;
import java.awt.Color;

import gov.revenue.ASSERT;
import gov.revenue.Logger;

/**
 *  Page represents a memory page in the
 *  virtual character display.
 */
class Page implements Attributes
{
	/**
	 *  Character and attribute data is stored here
	 */
	int[][] mem;
	
	/**
	 *  Buffer for drawing text.
	 */
	byte[] chbuf = new byte[1];
	
	/**
	 *  number of rows and columns (should be 80X24)
	 */
	int numRows, numColumns;
	
	/**
	 *  Current cursor position.  The cursor is autotabbed
	 *  to unprocted fields in block mode.
	 */
	Cursor cursorPos;
	
	/**
	 *  Current buffer position.  The buffer position
	 *  is unaffected by block mode.
	 */
	Cursor bufferPos;

	/**
	 *  The write attribute is applied to all character
	 *  writes.
	 */
	int writeAttr = VID_NORMAL;
	
	/**
	 *  The write prior attribute is applied to all character
	 *  writes.
	 */
	int priorAttr = VID_NORMAL;
	
	/**
	 *  Insert/overwrite -- currently unimplemented
	 */
	int insertMode = INSERT_INSERT;
	
	/**
	 *  Block mode flag
	 */
	boolean cursorBlock = true;
	
	
	Page(int numRows, int numCols)
	{
		this.numColumns = numCols;
		this.numRows = numRows;
		init();
	}
	
	/**
	 *  Rebuild the page
	 */
	void init()
	{
		cursorPos = new Cursor(numRows, numColumns);
		bufferPos = new Cursor(numRows, numColumns);
		
		writeAttr = VID_NORMAL;
		priorAttr = VID_NORMAL;
		cursorPos.clear();
		bufferPos.clear();
		
		mem = new int[numRows][];
		for (int x = 0; x < numRows; x++)
		{
			mem[x] = new int[numColumns];
			for (int q = 0; q < numColumns; q++)
			{
				mem[x][q] = ' ' | CHAR_CELL_DIRTY | writeAttr;
			}
		}
	}
	
	/**
	 *  Write at the current buffer position
	 */
	void writeBuffer(PageProtocol mode, String text)
	{
		mode.writeBuffer(this, text);
		ASSERT.fatal(bufferPos.row < numRows, "Page", 58, "Illegal row");
		ASSERT.fatal(bufferPos.column < numColumns, "Page", 59, "Illegal column");
		ASSERT.fatal(cursorPos.row < numRows, "Page", 60, "Illegal row");
		ASSERT.fatal(cursorPos.column < numColumns, "Page", 61, "Illegal column");
	}
	
	/**
	 *  write at the current cursor positon
	 */
	void writeCursor(PageProtocol mode, String text)
	{
		mode.writeCursor(this, text);
		ASSERT.fatal(bufferPos.row < numRows, "Page", 67, "Illegal row");
		ASSERT.fatal(bufferPos.column < numColumns, "Page", 68, "Illegal column");
		ASSERT.fatal(cursorPos.row < numRows, "Page", 69, "Illegal row");
		ASSERT.fatal(cursorPos.column < numColumns, "Page", 70, "Illegal column");
	}

	/**
	 *  Write at the current cursor position in local edit
	 *  mode.
	 */
	public void writeCursorLocal(PageProtocol mode, String text)
	{
		for (int x = 0; x < text.length(); x++)
		{
			mode.writeChar(this, cursorPos, text.charAt(x) | DAT_MDT);
		}
	}
	
	/**
	 *  Move the cursor to column 1
	 */
	void carageReturn(PageProtocol mode)
	{
		mode.carageReturn(this);
		ASSERT.fatal(bufferPos.row < numRows, "Page", 76, "Illegal row");
		ASSERT.fatal(bufferPos.column < numColumns, "Page", 77, "Illegal column");
		ASSERT.fatal(cursorPos.row < numRows, "Page", 78, "Illegal row");
		ASSERT.fatal(cursorPos.column < numColumns, "Page", 79, "Illegal column");
	}
	
	/**
	 *  Set the cursor position.  In block mode, the cursor
	 *  will be tabbed if the requested position is protected.
	 */
	void setCursor(PageProtocol mode, int row, int col)
	{
		mode.setCursor(this, row, col);
		ASSERT.fatal(bufferPos.row < numRows, "Page", 85, "Illegal row");
		ASSERT.fatal(bufferPos.column < numColumns, "Page", 86, "Illegal column");
		ASSERT.fatal(cursorPos.row < numRows, "Page", 87, "Illegal row");
		ASSERT.fatal(cursorPos.column < numColumns, "Page", 88, "Illegal column");
	}
	
	/**
	 *  Set the buffer position
	 */
	void setBuffer(int row, int col)
	{
		bufferPos.row = row;
		bufferPos.column = col;
		ASSERT.fatal(bufferPos.row < numRows, "Page", 95, "Illegal row");
		ASSERT.fatal(bufferPos.column < numColumns, "Page", 96, "Illegal column");
		ASSERT.fatal(cursorPos.row < numRows, "Page", 97, "Illegal row");
		ASSERT.fatal(cursorPos.column < numColumns, "Page", 98, "Illegal column");
	}
	
	/**
	 *  Set the video prior condition register
	 */
	void setVideoPriorCondition(int attr)
	{
		priorAttr = attr;
	}
	
	/**
	 *  Set the write attribute register
	 */
	void setWriteAttribute(int attr)
	{
		writeAttr = attr;
	}
	
	/**
	 *  Insert a character
	 */
	void insertChar(PageProtocol mode)
	{
		mode.insertChar(this);
		ASSERT.fatal(bufferPos.row < numRows, "Page", 114, "Illegal row");
		ASSERT.fatal(bufferPos.column < numColumns, "Page", 115, "Illegal column");
		ASSERT.fatal(cursorPos.row < numRows, "Page", 116, "Illegal row");
		ASSERT.fatal(cursorPos.column < numColumns, "Page", 117, "Illegal column");
	}
	
	/**
	 *  Delete a character
	 */
	void deleteChar(PageProtocol mode)
	{
		mode.deleteChar(this);
		ASSERT.fatal(bufferPos.row < numRows, "Page", 123, "Illegal row");
		ASSERT.fatal(bufferPos.column < numColumns, "Page", 124, "Illegal column");
		ASSERT.fatal(cursorPos.row < numRows, "Page", 125, "Illegal row");
		ASSERT.fatal(cursorPos.column < numColumns, "Page", 126, "Illegal column");
	}
	
	/**
	 *  Tab.  If inc is 1, tab forward.  If inc is -1, back tab
	 */
	void tab(PageProtocol mode, int inc)
	{
		mode.tab(this, inc);
		ASSERT.fatal(bufferPos.row < numRows, "Page", 132, "Illegal row");
		ASSERT.fatal(bufferPos.column < numColumns, "Page", 133, "Illegal column");
		ASSERT.fatal(cursorPos.row < numRows, "Page", 134, "Illegal row");
		ASSERT.fatal(cursorPos.column < numColumns, "Page", 135, "Illegal column");
	}
	
	/**
	 *  Backspace.  The new cursor position is set to a space.
	 */
	void backspace(PageProtocol mode)
	{
		mode.backspace(this);
		ASSERT.fatal(bufferPos.row < numRows, "Page", 141, "Illegal row");
		ASSERT.fatal(bufferPos.column < numColumns, "Page", 142, "Illegal column");
		ASSERT.fatal(cursorPos.row < numRows, "Page", 143, "Illegal row");
		ASSERT.fatal(cursorPos.column < numColumns, "Page", 144, "Illegal column");
	}
	
	void cursorUp(PageProtocol mode)
	{
		mode.cursorUp(this);
		ASSERT.fatal(bufferPos.row < numRows, "Page", 150, "Illegal row");
		ASSERT.fatal(bufferPos.column < numColumns, "Page", 151, "Illegal column");
		ASSERT.fatal(cursorPos.row < numRows, "Page", 152, "Illegal row");
		ASSERT.fatal(cursorPos.column < numColumns, "Page", 153, "Illegal column");
	}
	
	void cursorDown(PageProtocol mode)
	{
		mode.cursorDown(this);
		ASSERT.fatal(bufferPos.row < numRows, "Page", 159, "Illegal row");
		ASSERT.fatal(bufferPos.column < numColumns, "Page", 160, "Illegal column");
		ASSERT.fatal(cursorPos.row < numRows, "Page", 161, "Illegal row");
		ASSERT.fatal(cursorPos.column < numColumns, "Page", 162, "Illegal column");
	}
	
	void cursorLeft(PageProtocol mode)
	{
		mode.cursorLeft(this);
		ASSERT.fatal(bufferPos.row < numRows, "Page", 163, "Illegal row");
		ASSERT.fatal(bufferPos.column < numColumns, "Page", 164, "Illegal column");
		ASSERT.fatal(cursorPos.row < numRows, "Page", 165, "Illegal row");
		ASSERT.fatal(cursorPos.column < numColumns, "Page", 166, "Illegal column");
	}
	
	void cursorRight(PageProtocol mode)
	{
		mode.cursorRight(this);
		ASSERT.fatal(bufferPos.row < numRows, "Page", 177, "Illegal row");
		ASSERT.fatal(bufferPos.column < numColumns, "Page", 178, "Illegal column");
		ASSERT.fatal(cursorPos.row < numRows, "Page", 179, "Illegal row");
		ASSERT.fatal(cursorPos.column < numColumns, "Page", 180, "Illegal column");
	}
	
	/**
	 *  Move to the first unprotect field in block mode
	 */
	void home(PageProtocol mode)
	{
		mode.home(this);
	}
	
	/**
	 *  Move to the last unprotect field in block mode
	 */
	void end(PageProtocol mode)
	{
		mode.end(this);
	}
	
	/**
	 *  Clear the page.  All fields are cleared.
	 */
	void clearPage()
	{
		ASSERT.fatal(bufferPos.row < numRows, "Page", 185, "Illegal row");
		ASSERT.fatal(bufferPos.column < numColumns, "Page", 186, "Illegal column");
		ASSERT.fatal(cursorPos.row < numRows, "Page", 187, "Illegal row");
		ASSERT.fatal(cursorPos.column < numColumns, "Page", 188, "Illegal column");
		
		for (int r = 0; r < numRows; r++)
		{
			for (int c = 0; c < numColumns; c++)
			{
				mem[r][c] = ' ' | CHAR_CELL_DIRTY;
				mem[r][c] |= writeAttr | priorAttr;
			}
		}
	}
	
	/**
	 *  Clear to the end of the page.
	 */
	void clearToEOP(PageProtocol mode)
	{
		mode.clearToEOP(this);
	}
	
	/**
	 *  Clear to the end of line
	 */
	void clearToEOL(PageProtocol mode)
	{
		mode.clearToEOL(this);
	}
	
	/**
	 *  clear the specified block
	 */
	public void clearBlock(PageProtocol mode, int startRow, int startCol, int endRow, int endCol)
	{
		mode.clearBlock(this, startRow, startCol, endRow, endCol);
	}
	
	/**
	 *  Start a new field
	 */
	void writeField(int c)
	{		
		// mark the field
		mem[bufferPos.row][bufferPos.column] = (CHAR_START_FIELD | c);
		mem[bufferPos.row][bufferPos.column] |= ((writeAttr | priorAttr) | CHAR_CELL_DIRTY); 
		mem[bufferPos.row][bufferPos.column] &= ~DAT_UNPROTECT;
		
		ASSERT.fatal((mem[bufferPos.row][bufferPos.column] & (DAT_UNPROTECT)) == 0, "Linear", 782, "Bad right up top");

		bufferPos.column++;
		bufferPos.adjustCol();
				
		int col;
		
		// detect any previous field on the line and extend
		// its attributes upto this one
		for (col = bufferPos.column; col < numColumns; col++)
		{
			if ((mem[bufferPos.row][col] & CHAR_START_FIELD) != 0)
			{
				return;
			}
			mem[bufferPos.row][col] = (c & MASK_FIELD) | (mem[bufferPos.row][col] & MASK_CHAR);
			mem[bufferPos.row][col] |= (CHAR_CELL_DIRTY | writeAttr | priorAttr);
			ASSERT.fatal( (mem[bufferPos.row][col] & CHAR_START_FIELD) == 0, "Page", 185, "Illegal field attrib");
		}
		for (int y = bufferPos.row + 1; y < numRows; y++)
		{
			for (int x = 0; x < numColumns; x++)
			{
				if ((mem[y][x] & CHAR_START_FIELD) != 0)
				{
					return;
				}
				mem[y][x] = (c & MASK_FIELD) | (mem[y][x] & MASK_CHAR);
				mem[y][x] |= (CHAR_CELL_DIRTY | writeAttr | priorAttr);
				ASSERT.fatal( (mem[y][x] & CHAR_START_FIELD) == 0, "Page", 195, "Illegal field attrib");
			}
		}
	}
	
	/**
	 *  Read data from a block
	 */
	byte[] readBuffer(PageProtocol mode, int reqMask, int forbidMask, int startRow, int startCol, int endRow, int endCol)
	{
		return mode.readBuffer(this, reqMask, forbidMask, startRow, startCol, endRow, endCol);
	}
	
	/**
	 *  Clear all the data changed flags
	 */
	void resetMDTs()
	{
		for (int r = 0; r < numRows; r++)
		{
			for (int c = 0; c < numColumns; c++)
			{
				mem[r][c] &= ~DAT_MDT;
			}
		}
	}
	
	/**
	 *  Get the first field on the display, in Guardian encoded
	 *  ASCII
	 */
	String getStartFieldASCII()
	{
		for (int r = 0; r < numRows; r++)
		{
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
					return (char)(r + 0x20) + "" + (char)(c + 0x21);
				}
			}
		}
		return "  ";
	}

	/**
	 *  Render the virtual character display on a 
	 *  physical display.
	 */
	final void paint(PaintSurface ps, String statusLine)
	{
		Color foreground = ps.getForeGroundColor();
		Color background = ps.getBackGroundColor();
		Color blink = new Color(~foreground.getRGB() & 0xFFFFFF);
		int charWidth = ps.getFontWidth();
		int charHeight = ps.getFontHeight();
		int charDescent = ps.getFontDescent();
		
		Color fg, bg;
		boolean allClean = true;
		
		ps.setPaintMode();

		for (int r = 0; r < numRows; r++)
		{
			for(int c = 0; c < numColumns; c++)
			{
				int ch = mem[r][c];
				if ( (ch & CHAR_CELL_DIRTY) == 0)
				{
					continue;
				}
				mem[r][c] &= ~CHAR_CELL_DIRTY;
				allClean = false;
				fg = foreground;
				bg = background;

				if ((ch & VID_REVERSE) != 0) 
				{ 
					Color color = bg; 
					bg = fg;
					fg = color; 
				}
				if ((ch & VID_BLINKING) != 0)
				{
					fg = blink;
				}
				// clear the part of the screen we want to change (fill rectangle)
				ps.setForeGroundColor(bg);
				ps.fillRect(c * charWidth /*+ xoffset*/, r * charHeight /*+ yoffset*/, charWidth, charHeight);
				ps.setForeGroundColor(fg);
				
				// draw the characters
				if ( (ch & VID_INVIS) == 0)
				{
					chbuf[0] = (byte)(ch & MASK_CHAR);
					ps.drawBytes(chbuf, 0, 1, c * charWidth, (r+1) * charHeight - charDescent /*+ yoffset*/);
				}
				if(((ch & VID_UNDERLINE) != 0) && ((ch & CHAR_START_FIELD) == 0))
				{
					ps.drawLine(c * charWidth, (r+1) * charHeight - charDescent / 2 /*+ yoffset*/, c * charWidth + 1 * charWidth /*+ xoffset*/, (r+1) * charHeight - charDescent / 2 /*+ yoffset*/);
				}
			}
		}
		if (allClean)
		{
			for (int y = 0; y < numRows; y++)
			{
				for (int x = 0; x < numColumns; x++)
				{
					mem[y][x] |= CHAR_CELL_DIRTY;
				}
			}
			paint(ps, statusLine);
		}
		else
		{
			// draw cursor
			ps.setForeGroundColor(foreground);
			ps.setPaintXorMode(background);
			if (cursorBlock)
			{
				ps.fillRect( cursorPos.column * charWidth, 
							cursorPos.row * charHeight,
							charWidth, charHeight);
			}
			else
			{
				ps.drawLine(cursorPos.column * charWidth ,
						   (cursorPos.row) * charHeight + charHeight,
							cursorPos.column * charWidth + charWidth,
						   (cursorPos.row) * charHeight + charHeight); 
			}
			ps.setPaintMode();
			
			// draw the status line
			ps.setForeGroundColor(background);
			ps.fillRect(0, numRows * charHeight, charWidth*numColumns, charHeight);
			ps.setForeGroundColor(foreground);
			
			for (int x = 0; x < numColumns; x++)
			{
				int c = statusLine.charAt(x);
				if (c == 27)
				{
					int attr = statusLine.charAt(++x);
					c = statusLine.charAt(++x);
				}
				chbuf[0] = (byte)(c&MASK_CHAR);
				ps.drawBytes(chbuf, 0, 1, x*charWidth, (numRows+1) * charHeight - charDescent);			
			}
		}
	}

	/**
	 *  Set all the cells to dirty so they will be repainted
	 */
	final void forceDirty()
	{
		for (int r = 0; r < numRows; r++)
		{
			for (int c = 0; c < numColumns; c++)
			{
				mem[r][c] |= CHAR_CELL_DIRTY;
			}
		}
	}
	
	/**
	 *  Scroll the page up on line and fill the bottom
	 *  line with spaces.
	 */
	final void scrollPageUp()
	{
		for (int r = 0; r < numRows -1; r++)
		{
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
		ASSERT.fatal(cursorPos.row < numRows, "SharedProtocol", 20, "Illegal row");
		ASSERT.fatal(cursorPos.column < numColumns, "SharedProtocol", 21, "Illegal column");
	}

	/**
	 *  Get the contents of the field the cursor is in.
	 */
	final String getCurrentField()
	{
		int count = 0;
		boolean cap = false;
		StringBuffer accum = new StringBuffer();
		int r = cursorPos.row;
		int c = cursorPos.column;
		
		while (c > 0)
		{
			if ( (mem[r][c] & CHAR_START_FIELD) == 0)
			{
				break;
			}
			c--;
		}
		while (c < numColumns)
		{
			if ( (mem[r][c] & CHAR_START_FIELD) != 0)
			{
				break;
			}
			accum.append ((char)(mem[r][c] & MASK_CHAR));
			c++;
		}
		return accum.toString();
	}
	
	/**
	 *  Find the next field from the specified start
	 */
	final int scanForNextField(int c, int r, int inc)
	{
		ASSERT.fatal(r < numRows, "Page", 20, "Illegal row");
		ASSERT.fatal(c < numColumns, "Page", 21, "Illegal column");
		if (inc > 0)
		{
			for (int x = c; x < numColumns; x++)
			{
				if ((mem[r][x] & CHAR_START_FIELD) != 0)
				{
					ASSERT.fatal((mem[r][x] & DAT_UNPROTECT) == 0, "Linear", 999, "Bad Scan");
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
					ASSERT.fatal((mem[r][x] & DAT_UNPROTECT) == 0, "Linear", 999, "Bad Scan");
					return x;
				}
			}
		}
		return -1;
	}
	
	/**
	 *  Find the next unprotected field from the specified start
	 */
	final int scanForUnprotectField(int c, int r, int inc)
	{
		ASSERT.fatal(r < numRows, "SharedProtocol", 20, "Illegal row");
		ASSERT.fatal(c < numColumns, "SharedProtocol", 21, "Illegal column");
		if (inc > 0)
		{
			for (int x = c; x < numColumns; x++)
			{
				if ((mem[r][x] & DAT_UNPROTECT) != 0)
				{
					ASSERT.fatal((mem[r][x] & CHAR_START_FIELD) == 0, "Linear", 999, "Bad Scan");
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
					ASSERT.fatal((mem[r][x] & CHAR_START_FIELD) == 0, "Linear", 999, "Bad Scan");
					return x;
				}
			}			
		}
		return -1;
	}
}
