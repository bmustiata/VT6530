package gov.revenue.vt6530.ui;

import gov.revenue.ASSERT;
import java.awt.datatransfer.Clipboard;
import java.awt.datatransfer.ClipboardOwner;
import java.awt.datatransfer.Transferable;
import java.awt.datatransfer.StringSelection;
import java.awt.datatransfer.DataFlavor;
import java.awt.datatransfer.UnsupportedFlavorException;
import java.awt.Toolkit;

/**
 *  SharedProtocol has the operations common to protect, unprotect,
 *  and conversation mode.  Behavior specific to a mode is added
 *  by subclassing SharedProtocol.
 */
public class SharedProtocol implements PageProtocol, ClipboardOwner
{
	Clipboard clipboard;

	/**
	 *  Write to the buffer position
	 */
	public void writeBuffer(Page page, String text)
	{
		write(page.bufferPos, page, page.writeAttr | page.priorAttr, text);
	}
	
	/**
	 *  Write to the cursor position
	 */
	public void writeCursor(Page page, String text)
	{
		write(page.cursorPos, page, page.writeAttr | page.priorAttr, text);
	}
	
	/**
	 *  Write a character at the specified cursor
	 */
	public void writeChar (Page page, Cursor cursor, int c)
	{
		ASSERT.fatal(page.cursorPos.row < page.numRows, "SharedProtocol", 20, "Illegal row");
		ASSERT.fatal(page.cursorPos.column < page.numColumns, "SharedProtocol", 21, "Illegal column");

		int ch = c & MASK_CHAR;
		if (ch < 32 || ch == Keys.SPC_COPY || ch == Keys.SPC_PASTE || ch == Keys.SPC_CUT)
		{
			cursorOp(page, cursor, ch);
			return;
		}
		if ( (page.mem[cursor.row][cursor.column] & KEY_UPSHIFT) != 0)
		{
			page.mem[cursor.row][cursor.column] = (page.writeAttr | page.priorAttr | (c & MASK_FIELD) | Character.toUpperCase((char)(c&0xFF)) | (page.mem[cursor.row][cursor.column] & MASK_FIELD) | CHAR_CELL_DIRTY);
		}
		else
		{
			page.mem[cursor.row][cursor.column] = (c | (page.mem[cursor.row][cursor.column] & MASK_FIELD));
			page.mem[cursor.row][cursor.column] |= (CHAR_CELL_DIRTY | page.priorAttr);
		}
		cursor.column++;
		cursor.adjustCol();
		page.mem[cursor.row][cursor.column] |= CHAR_CELL_DIRTY;

		validateCursorPos(page);
	}
	
	/**
	 *  Handles non-printing characters.  This
	 *  should probably be in Text display or
	 *  Guardian.
	 */
	private final void cursorOp(Page page, Cursor cursor, int ch)
	{
		switch (ch & MASK_CHAR)
		{
			case '\t':
				tab(page, 1);
				break;
			case '\r':
				linefeed(page);
				break;
			case '\n':
				carageReturn(page);
				//page.mem[cursor.row][cursor.column] |= CHAR_CELL_DIRTY;
				//cursor.column = 0;
				//page.mem[cursor.row][cursor.column] |= CHAR_CELL_DIRTY;
				break;
			case '\b':
				backspace(page);
				break;
			case (char)11:
				cursorUp(page);
				break;
			case Keys.SPC_DEL:
				deleteChar(page);
				break;
			case Keys.SPC_DOWN:
				arrowDown(page, cursor);
				break;
			case Keys.SPC_END:
				end(page);
				break;
			case Keys.SPC_HOME:
				home(page);
				break;
			case Keys.SPC_INS:
				insertChar(page);
				break;
			case Keys.SPC_LEFT:
				arrowLeft(page, cursor);
				break;
			case Keys.SPC_PGDN:
				break;
			case Keys.SPC_PGUP:
				break;
			case Keys.SPC_PRINTSCR:
				break;
			case Keys.SPC_RIGHT:
				arrowRight(page, cursor);
				break;
			case Keys.SPC_UP:
				arrowUp(page, cursor);
				break;
			case Keys.SPC_COPY:
				if (clipboard == null)
					clipboard = Toolkit.getDefaultToolkit().getSystemClipboard();
				clipboard.setContents(new StringSelection(page.getCurrentField()), this);
				break;
			case Keys.SPC_CUT:
				if (clipboard == null)
					clipboard = Toolkit.getDefaultToolkit().getSystemClipboard();
				break;
			case Keys.SPC_PASTE:
				if (clipboard == null)
					clipboard = Toolkit.getDefaultToolkit().getSystemClipboard();				
				StringSelection ss = (StringSelection)clipboard.getContents(this);
				try
				{
					String s = (String)ss.getTransferData(DataFlavor.stringFlavor);
					for (int x = 0; x < s.length(); x++)
					{
						writeChar(page, cursor, s.charAt(x));
					}
				}
				catch (UnsupportedFlavorException ufe)
				{
				}
				catch (java.io.IOException ioe)
				{
				}
				
				break;
		}
	}
	
	public void arrowDown(Page page, Cursor cursor)
	{
		page.mem[cursor.row][cursor.column] |= CHAR_CELL_DIRTY;
		cursor.row++;
		cursor.adjustRow();
		page.mem[cursor.row][cursor.column] |= CHAR_CELL_DIRTY;
	}
	
	public void arrowUp(Page page, Cursor cursor)
	{
		page.mem[cursor.row][cursor.column] |= CHAR_CELL_DIRTY;
		cursor.row--;
		cursor.adjustRow();
		page.mem[cursor.row][cursor.column] |= CHAR_CELL_DIRTY;
	}

	public void arrowLeft(Page page, Cursor cursor)
	{
		page.mem[cursor.row][cursor.column] |= CHAR_CELL_DIRTY;
		cursor.column--;
		cursor.adjustCol();
		page.mem[cursor.row][cursor.column] |= CHAR_CELL_DIRTY;
	}
	
	public void arrowRight(Page page, Cursor cursor)
	{
		page.mem[cursor.row][cursor.column] |= CHAR_CELL_DIRTY;
		cursor.column++;
		cursor.adjustCol();
		page.mem[cursor.row][cursor.column] |= CHAR_CELL_DIRTY;
	}

	public void validateCursorPos(Page page)
	{
	}
	
	public void insertChar(Page page)
	{
	}
	
	public void deleteChar(Page page)
	{
	}
	
	public void backspace(Page page)
	{
		page.mem[page.cursorPos.row][page.cursorPos.column] |= CHAR_CELL_DIRTY;
		page.cursorPos.column--;
		page.cursorPos.adjustCol();
		page.mem[page.cursorPos.row][page.cursorPos.column] = ' ' | (page.mem[page.cursorPos.row][page.cursorPos.column] & MASK_FIELD) | CHAR_CELL_DIRTY;		

		ASSERT.fatal(page.cursorPos.row < page.numRows, "SharedProtocol", 20, "Illegal row");
		ASSERT.fatal(page.cursorPos.column < page.numColumns, "SharedProtocol", 21, "Illegal column");
	}
	
	public void tab(Page page, int inc)
	{
	}
	
	public void home(Page page)
	{
	}
	
	public void end(Page page)
	{
	}

	public void carageReturn(Page page)
	{
		page.mem[page.cursorPos.row][page.cursorPos.column] |= CHAR_CELL_DIRTY;
		page.cursorPos.column = 0;
		page.mem[page.cursorPos.row][page.cursorPos.column] |= CHAR_CELL_DIRTY;

		ASSERT.fatal(page.cursorPos.row < page.numRows, "SharedProtocol", 20, "Illegal row");
		ASSERT.fatal(page.cursorPos.column < page.numColumns, "SharedProtocol", 21, "Illegal column");
	}
	
	public void linefeed(Page page)
	{
		page.mem[page.cursorPos.row][page.cursorPos.column] |= CHAR_CELL_DIRTY;
		
		if (page.cursorPos.row == page.numRows-1)
		{
			page.scrollPageUp();
		}
		else
		{
			page.cursorPos.row++;
		}
		page.mem[page.cursorPos.row][page.cursorPos.column] |= CHAR_CELL_DIRTY;
		
		ASSERT.fatal(page.cursorPos.row < page.numRows, "SharedProtocol", 20, "Illegal row");
		ASSERT.fatal(page.cursorPos.column < page.numColumns, "SharedProtocol", 21, "Illegal column");
	}
	
	public void cursorRight(Page page)
	{
		page.mem[page.bufferPos.row][page.bufferPos.column] |= CHAR_CELL_DIRTY;
		if (++page.cursorPos.column >= page.numColumns)
		{
			page.cursorPos.column = 0;
			if (page.cursorPos.row == page.numRows-1)
			{
				page.scrollPageUp();
			}
			else
			{
				page.cursorPos.row++;
				page.cursorPos.adjustRow();
			}
		}
		ASSERT.fatal(page.cursorPos.row < page.numRows, "SharedProtocol", 20, "Illegal row");
		ASSERT.fatal(page.cursorPos.column < page.numColumns, "SharedProtocol", 21, "Illegal column");
	}

	public void cursorLeft(Page page)
	{
		page.cursorPos.column--;
		if (page.cursorPos.column < 0)
		{
			page.cursorPos.column = 0;
			if (page.cursorPos.row == 0)
			{
				page.cursorPos.row = page.numRows -1;
			}
			else
			{
				page.cursorPos.row--;
			}
		}
		ASSERT.fatal(page.cursorPos.row < page.numRows, "SharedProtocol", 20, "Illegal row");
		ASSERT.fatal(page.cursorPos.column < page.numColumns, "SharedProtocol", 21, "Illegal column");
	}
		
	public void cursorUp(Page page)
	{
		page.mem[page.cursorPos.row][page.cursorPos.column] |= CHAR_CELL_DIRTY;
		page.cursorPos.row++;
		page.cursorPos.adjustRow();
		page.mem[page.cursorPos.row][page.cursorPos.column] |= CHAR_CELL_DIRTY;

		ASSERT.fatal(page.cursorPos.row < page.numRows, "SharedProtocol", 20, "Illegal row");
		ASSERT.fatal(page.cursorPos.column < page.numColumns, "SharedProtocol", 21, "Illegal column");
	}
	
	public void cursorDown(Page page)
	{
		page.mem[page.cursorPos.row][page.cursorPos.column] |= CHAR_CELL_DIRTY;
		if (page.cursorPos.row == page.numRows-1)
		{
			page.scrollPageUp();
		}
		else
		{
			page.cursorPos.row++;
			page.mem[page.cursorPos.row][page.cursorPos.column] |= CHAR_CELL_DIRTY;
		}
		ASSERT.fatal(page.cursorPos.row < page.numRows, "SharedProtocol", 20, "Illegal row");
		ASSERT.fatal(page.cursorPos.column < page.numColumns, "SharedProtocol", 21, "Illegal column");
	}
	
	public void clearToEOL(Page page)
	{
	}
	
	public void clearBlock(Page page, int startRow, int startCol, int endRow, int endCol)
	{
	}

	public void clearToEOP(Page page)
	{
		for (int c = page.cursorPos.column; c < page.numColumns; c++)
		{
			page.mem[page.cursorPos.row][c] = ' ' | CHAR_CELL_DIRTY;
			page.mem[page.cursorPos.row][c] |= page.writeAttr | page.priorAttr;
		}
		for (int r = page.cursorPos.row+1; r < page.numRows; r++)
		{
			for (int c = 0; c < page.numColumns; c++)
			{
				page.mem[r][c] = ' ' | CHAR_CELL_DIRTY;
				page.mem[r][c] |= page.writeAttr | page.priorAttr;
			}
		}
		ASSERT.fatal(page.bufferPos.row < page.numRows, "Page", 212, "Illegal row");
		ASSERT.fatal(page.bufferPos.column < page.numColumns, "Page", 213, "Illegal column");
		ASSERT.fatal(page.cursorPos.row < page.numRows, "Page", 214, "Illegal row");
		ASSERT.fatal(page.cursorPos.column < page.numColumns, "Page", 125, "Illegal column");
	}
	
	public byte[] readBuffer(Page page, int reqMask, int forbidMask, int startRow, int startCol, int endRow, int endCol)
	{
		return null;
	}

	public void setCursor(Page page, int row, int col)
	{
		page.mem[page.cursorPos.row][page.cursorPos.column] |= CHAR_CELL_DIRTY;
		page.cursorPos.row = row;
		page.cursorPos.column = col;
		page.mem[page.cursorPos.row][page.cursorPos.column] |= CHAR_CELL_DIRTY;

		ASSERT.fatal(page.cursorPos.row < page.numRows, "SharedProtocol", 20, "Illegal row");
		ASSERT.fatal(page.cursorPos.column < page.numColumns, "SharedProtocol", 21, "Illegal column");
	}

	private void write(Cursor pos, Page page, int attribute, String text)
	{
		ASSERT.fatal(attribute == 0 || (attribute & MASK_CHAR) == 0, "", 9, "");
		//int c;
		for (int x = 0; x < text.length(); x++)
		{
			//page.mem[pos.row][pos.column] = text.charAt(x) | attribute | CHAR_CELL_DIRTY;
			//c = text.charAt(x);
			//System.out.print((char)(c & MASK_CHAR));		
			//c |= attribute;
			//System.out.print((char)(c & MASK_CHAR));
			writeChar(page, pos, text.charAt(x) | attribute);
			if (pos.column == page.numColumns-1)
			{
				return;
			}
		}
		ASSERT.fatal(page.cursorPos.row < page.numRows, "SharedProtocol", 20, "Illegal row");
		ASSERT.fatal(page.cursorPos.column < page.numColumns, "SharedProtocol", 21, "Illegal column");
	}
	
	/**
	 *  Another application has placed data in the clipboard
	 *  before our data was read.
	 */
	public void lostOwnership(Clipboard cb, Transferable data)
	{
	}
}
