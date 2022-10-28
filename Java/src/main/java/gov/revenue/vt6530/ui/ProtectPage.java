package gov.revenue.vt6530.ui;

import gov.revenue.ASSERT;

/**
 *  ProtectPage implements the features for BLOCK PROTECT
 *  mode.  This mode uses edit fields and read-only fields
 *  to control reads, clears, and cursor movement.
 */
public class ProtectPage extends SharedProtocol
{
	/**
	 *  Move to the first unprotect field on the page
	 */
	public void home(Page page)
	{		
		for (int r = 0; r < page.numRows; r++)
		{
			for (int c = 0; c < page.numColumns; c++)
			{
				// find the first unprotected cell
				if ( (page.mem[r][c] & CHAR_START_FIELD) != 0)
				{
					setCursor(page, r, c+1);
					return;
				}
			}
		}
	}
	
	/**
	 *  Move to the last unprotect field on the page
	 */
	public void end(Page page)
	{
		boolean inField = false;
		
		for (int r = page.numRows-1; r >= 0; r--)
		{
			for (int c = page.numColumns-1; c >= 0; c--)
			{
				if (inField)
				{
					// we're in the last field, find the start
					if ( (page.mem[r][c] & CHAR_START_FIELD) != 0)
					{
						setCursor(page, r, c+1);
						return;
					}
				}
				else
				{
					// find the start of an unprotected field
					if ( (page.mem[r][c] & DAT_UNPROTECT) != 0)
					{
						inField = true;
					}
				}
			}
		}
	}

	/** 0x09
	 * PROTECT
	 *  Move to the first position of the next 
	 *  unprotected field.
	 */
	public final void tab(Page page, int inc)
	{
		page.mem[page.cursorPos.row][page.cursorPos.column] |= CHAR_CELL_DIRTY;
		
		int newX = page.scanForNextField(page.cursorPos.column+inc, page.cursorPos.row, inc);
		if (newX >= 0)
		{
			newX = page.scanForUnprotectField(newX, page.cursorPos.row, inc);
			if (newX >= 0)
			{
				page.cursorPos.column = newX;
				page.mem[page.cursorPos.row][page.cursorPos.column] |= CHAR_CELL_DIRTY;
				return;
			}
		}
		int y = page.cursorPos.row + inc;
		for (int qpr = 0; qpr < page.numRows; qpr += inc)
		{
			if (inc > 0)
			{
				if (y >= page.numRows)
				{
					y = 0;
				}
			}
			else
			{
				if (y < 0)
				{
					y = page.numRows-1;
				}
			}
			newX = page.scanForUnprotectField(0, y, inc);
			if (newX >= 0)
			{
				page.cursorPos.column = newX;
				page.cursorPos.row = y;
				ASSERT.fatal(page.cursorPos.row < page.numRows, "SharedProtocol", 20, "Illegal row");
				ASSERT.fatal(page.cursorPos.column < page.numColumns, "SharedProtocol", 21, "Illegal column");
				page.mem[page.cursorPos.row][page.cursorPos.column] |= CHAR_CELL_DIRTY;
				return;
			}
			y++;
		}
		page.mem[page.cursorPos.row][page.cursorPos.column] |= CHAR_CELL_DIRTY;
		
		ASSERT.fatal(page.cursorPos.row < page.numRows, "SharedProtocol", 20, "Illegal row");
		ASSERT.fatal(page.cursorPos.column < page.numColumns, "SharedProtocol", 21, "Illegal column");
	}
	
	/**
	 *  Clear all the field data to the end of a 
	 *  line.  Do not remove the fields.
	 */
	public void clearToEOL(Page page)
	{
		int x = page.bufferPos.column;
		int y = page.bufferPos.row;
		
		int attr = page.mem[y][x] & MASK_FIELD;
		
		if ((page.mem[y][x] & CHAR_START_FIELD) != 0)
		{
			// clear the video attributes for the field
			attr = VID_NORMAL;
			page.mem[y][x] ^= MASK_VID;
			page.mem[y][x] |= CHAR_CELL_DIRTY | VID_NORMAL;
			x++;
		}
		attr |= CHAR_CELL_DIRTY | (int)' ';
		while (x < page.numColumns && (page.mem[y][x] & CHAR_START_FIELD) == 0)
		{
			page.mem[y][x] = attr;
			x++;
		}
		if (x != page.numColumns) //((page.mem[y][x] & CHAR_START_FIELD) != 0)
		{
			return;
		}
		for (y = y+1; y < page.numRows; y++)
		{
			x = 0;
			while (x < page.numColumns && (page.mem[y][x] & CHAR_START_FIELD) == 0)
			{
				page.mem[y][x] = attr;
				x++;
			}
			if (x < page.numColumns)
			{
				if ((page.mem[y][x] & CHAR_START_FIELD) != 0)
				{
					break;
				}
			}
		}
		ASSERT.fatal(page.bufferPos.row < page.numRows, "SharedProtocol", 20, "Illegal row");
		ASSERT.fatal(page.bufferPos.column < page.numColumns, "SharedProtocol", 21, "Illegal column");
	}
	
	/**
	 *  Clear all the field data to the end of the page.
	 *  Do not remove the fields.
	 */
	public void clearToEOP(Page page)
	{
		Cursor cursor = page.bufferPos;
		int attr = page.writeAttr | page.priorAttr;
		
		for (int c = cursor.column; c < page.numColumns; c++)
		{
			if ( (page.mem[cursor.row][c] & DAT_UNPROTECT) != 0)
			{
				page.mem[cursor.row][c] = (' ' | CHAR_CELL_DIRTY);
				page.mem[cursor.row][c] |= (attr | DAT_UNPROTECT);
			}
		}
		for (int r = cursor.row+1; r < page.numRows; r++)
		{
			for (int c = 0; c < page.numColumns; c++)
			{
				if ( (page.mem[r][c] & DAT_UNPROTECT) != 0)
				{
					page.mem[r][c] = (' ' | CHAR_CELL_DIRTY);
					page.mem[r][c] |= (attr | DAT_UNPROTECT);
				}
			}
		}
	}
	
	public void cursorLeft(Page page)
	{
		page.mem[page.cursorPos.row][page.cursorPos.column] |= CHAR_CELL_DIRTY;
		
		if (page.cursorPos.column == 0)
		{
			if (page.cursorPos.row == 0)
			{
				page.cursorPos.row = page.numRows-1;
			}
			else
			{
				page.cursorPos.row--;
				page.cursorPos.adjustRow();
			}
			page.cursorPos.column = page.numColumns-1;
		}
		else
		{
			page.cursorPos.column--;
			page.cursorPos.adjustCol();
		}
		if ( (page.mem[page.cursorPos.row][page.cursorPos.column] & DAT_UNPROTECT) == 0)
		{
			tab(page, -1);
		}
		ASSERT.fatal(page.cursorPos.row < page.numRows, "SharedProtocol", 20, "Illegal row");
		ASSERT.fatal(page.cursorPos.column < page.numColumns, "SharedProtocol", 21, "Illegal column");
	}

	/**
	 *  Read the fields in a block.
	 */
	public byte[] readBuffer(Page page, int reqMask, int forbidMask, int startRow, int startCol, int endRow, int endCol)
	{
		StringBuffer accum = new StringBuffer();
		
		boolean writeCr = false;
		StringBuffer sb = new StringBuffer();
		
		ASSERT.fatal(startRow >= 0 && endRow < page.numRows, "ProtectPage", 83, "Wrong row format");
		ASSERT.fatal(startCol >= 0 && endCol < page.numColumns, "ProtectPage", 84, "Wrong col format");
		
		accum.setLength(0);
		
		for (int y = startRow; y <= endRow; y++)
		{
			boolean fieldStarted = false;
				
			for (int x = startCol; x < endCol; x++)
			{
				if ((page.mem[y][x] & CHAR_START_FIELD) != 0)
				{
					if ( (page.mem[y][x+1] & reqMask) != 0)
					{
						fieldStarted = true;
						accum.append((char)17 + "" + (char)(y + 0x20) + "" + (char)(x + 0x21));
					}
				}
				else if ((page.mem[y][x] & (reqMask)) != 0 && fieldStarted)
				{
					sb.append((char)(page.mem[y][x] & MASK_CHAR));
					writeCr = true;
				}
				else if (fieldStarted)
				{
					String candidate = sb.toString().trim();
					sb.setLength(0);
					if (candidate.length() > 0)
					{
						accum.append(candidate);
					}
					writeCr = false;
				}
			}
			if (writeCr == true)
			{
				String candidate = sb.toString().trim();
				sb.setLength(0);
				if (candidate.length() > 0)
				{
					accum.append(candidate);
				}
				writeCr = false;
			}
		}
		accum.append("" + (char)4);
		return accum.toString().getBytes();
	}

	/**
	 *  Write a character in a field
	 */
	public final void writeChar(Page page, Cursor bufferPos, int c)
	{
		int ch = c & 0xFF;
		if (ch < 32 || ch == Keys.SPC_COPY || ch == Keys.SPC_PASTE || ch == Keys.SPC_CUT)
		{
			super.writeChar(page, bufferPos, c);
			return;
		}
		ASSERT.fatal((page.mem[bufferPos.row][bufferPos.column] & CHAR_START_FIELD) == 0, "LinearPageTextDisplay", 964, "Invalid write pos");
		ASSERT.fatal(bufferPos.row < page.numRows, "SharedProtocol", 20, "Illegal row");
		ASSERT.fatal(bufferPos.column < page.numColumns, "SharedProtocol", 21, "Illegal column");
			
		page.mem[bufferPos.row][bufferPos.column] = c | (page.mem[bufferPos.row][bufferPos.column] & MASK_FIELD);
		page.mem[bufferPos.row][bufferPos.column] |= page.writeAttr | CHAR_CELL_DIRTY | page.priorAttr;
		bufferPos.column++;
		bufferPos.adjustCol();
		
		page.mem[bufferPos.row][bufferPos.column] |= CHAR_CELL_DIRTY;

		ASSERT.fatal(bufferPos.row < page.numRows, "SharedProtocol", 20, "Illegal row");
		ASSERT.fatal(bufferPos.column < page.numColumns, "SharedProtocol", 21, "Illegal column");
	}

	/**
	 *  Ensure the cursor is in an unprotect field
	 */
	public void validateCursorPos(Page page)
	{
		ASSERT.fatal(page.cursorPos.row < page.numRows, "ProtectPage", 179, "Illegal row");
		ASSERT.fatal(page.cursorPos.column < page.numColumns, "ProtectPage", 180, "Illegal column");

		page.mem[page.cursorPos.row][page.cursorPos.column] |= CHAR_CELL_DIRTY;
		
		if ( (page.mem[page.cursorPos.row][page.cursorPos.column] & DAT_UNPROTECT) == 0)
		{
			tab(page, 1);
		}
		ASSERT.fatal(page.cursorPos.row < page.numRows, "ProtectPage", 183, "Illegal row");
		ASSERT.fatal(page.cursorPos.column < page.numColumns, "ProtectPage", 184, "Illegal column");
	}
	
	/**
	 *  Set the cursor position
	 */
	public void setCursor(Page page, int row, int col)
	{
		page.mem[page.cursorPos.row][page.cursorPos.column] |= CHAR_CELL_DIRTY;
		page.cursorPos.row = row;
		page.cursorPos.column = col;
		validateCursorPos(page);
		page.mem[page.cursorPos.row][page.cursorPos.column] |= CHAR_CELL_DIRTY;
		
		ASSERT.fatal(page.cursorPos.row < page.numRows, "SharedProtocol", 20, "Illegal row");
		ASSERT.fatal(page.cursorPos.column < page.numColumns, "SharedProtocol", 21, "Illegal column");
	}

	/**
	 *  Clear the field data in a block.  Do
	 *  not remove the fields.
	 */
	public void clearBlock(Page page, int startRow, int startCol, int endRow, int endCol)
	{	
		int value = CHAR_CELL_DIRTY | ' ';
		int mask = MASK_FIELD ^ CHAR_START_FIELD;
		
		for (int y = startRow; y <= endRow; y++)
		{
			for (int x = startCol; x <= endCol; x++)
			{
				page.mem[y][x] = (page.mem[y][x] & mask) | value;
			}
		}
		validateCursorPos(page);
	}

	/**
	 *  Down arrow key
	 */
	public void arrowDown(Page page, Cursor cursor)
	{
		//tab(page, 1);
		super.arrowDown(page, cursor);
		validateCursorPos(page);
	}
	
	/**
	 *  Up arrow key
	 */
	public void arrowUp(Page page, Cursor cursor)
	{
		//tab(page, -1);
		super.arrowUp(page, cursor);
		validateCursorPos(page);		
	}

	/**
	 *  Left arrow key
	 */
	public void arrowLeft(Page page, Cursor cursor)
	{
		page.mem[cursor.row][cursor.column] |= CHAR_CELL_DIRTY;
		cursor.column--;
		cursor.adjustCol();
		if ( (page.mem[cursor.row][cursor.column] & DAT_UNPROTECT) == 0)
		{
			tab(page, -1);
		}
		page.mem[cursor.row][cursor.column] |= CHAR_CELL_DIRTY;
	}
	
	/**
	 *  Right arrow key
	 */
	public void arrowRight(Page page, Cursor cursor)
	{
		page.mem[cursor.row][cursor.column] |= CHAR_CELL_DIRTY;
		cursor.column++;
		cursor.adjustCol();
		if ( (page.mem[cursor.row][cursor.column] & DAT_UNPROTECT) == 0)
		{
			tab(page, 1);
		}
		page.mem[cursor.row][cursor.column] |= CHAR_CELL_DIRTY;
	}

}
