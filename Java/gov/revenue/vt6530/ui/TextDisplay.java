package gov.revenue.vt6530.ui;

import java.awt.Color;

import gov.revenue.ASSERT;

/**
 *  Virtual character display
 */
public class TextDisplay implements Attributes
{
	private StringBuffer statusLine = new StringBuffer(80);
	
	private int charWidth;      /* current width of a char */
	private int charHeight;      /* current height of a char */
	private int charDescent;      /* base line descent */	
	
	private Page displayPage;
	private Page writePage;
	private Page[] pages;
	private int numPages;
	
	private boolean echoOn = true;
	private boolean blockMode = false;
	private boolean protectMode = false;
	
	private boolean requiresRepaint = true;
	
	PageProtocol ppprotectMode = new ProtectPage();
	PageProtocol ppunProtectMode = new UnprotectPage();
	PageProtocol ppconvMode = new UnprotectPage();

	PageProtocol ppRemote;
	
	boolean keysLocked;
	
	int numRows, numColumns;
		
	
	public TextDisplay(int pageCount, int cols, int rows)
	{
		numPages = pageCount;
		numRows = rows;
		numColumns = cols;
		ppRemote = ppconvMode;
		
		init();

		statusLine.insert(0, "                                                                                  ");
		statusLine.setLength(80);
		statusLine.insert(67, "CONV");
		writeDisplay("*******************************************************************************\r\n");
		writeDisplay("*\n\r*                   Washington State Department of Revenue\n\r*                             VT6530 Emulator\n\r\n\r");
		setCursorRowCol(23, 0);
		writeDisplay("*******************************************************************************");
		for (int x = 1; x < numRows; x++)
		{
			setCursorRowCol(x, 0);
			writeDisplay("*");
			setCursorRowCol(x, numColumns-1);
			writeDisplay("*");			
		}
	}

	public boolean needsRepaint()
	{
		return requiresRepaint;
	}
	
	public void setRePaint()
	{
		requiresRepaint = true;
	}
	
	public void writeBuffer(String text)
	{
		writePage.writeBuffer(ppRemote, text);
	}
	
	public void writeDisplay(String text)
	{
		displayPage.writeCursor(ppRemote, text);
		requiresRepaint = true;
	}
	
	public void writeLocal(String text)
	{
		displayPage.writeCursorLocal(ppRemote, text);
		ppRemote.validateCursorPos(displayPage);
		requiresRepaint = true;
	}
	
	public void echoDisplay(String text)
	{
		if (echoOn)
		{
			displayPage.writeCursor(ppRemote, text);
			requiresRepaint = true;
			return;
		}
		char c = text.charAt(0);
		if (c == 13)
		{
			displayPage.writeCursor(ppRemote, "" + c + (char)10);
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
	public void setProtectMode()
	{
		protectMode = true;
		displayPage = pages[1];
		writePage = pages[1];
		ppRemote = ppprotectMode;
		clearAll();
		setVideoPriorCondition(VID_NORMAL);
		setInsertMode(INSERT_INSERT);
		initDataTypeTable();
		keysLocked();
		writeStatus("BLOCK PROT");
		requiresRepaint = true;
	}
	
	
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
	public void exitProtectMode()
	{
		displayPage = pages[1];
		writePage = pages[1];
		ppRemote = ppunProtectMode;
		clearAll();
		setInsertMode(INSERT_INSERT);
		clearAllTabs();
		keysLocked();
		writeStatus("BLOCK");
		protectMode = false;
	}
	
	public void keysLocked()
	{
		keysLocked = true;
	}
	
	public void keysUnlocked()
	{
		keysLocked = false;
	}
	
	/** ESC :
	 */
	public void setPage(int page)
	{
		//if (page >= numPages)
		//{
		//	return;
		//}
		writePage = pages[page];
	}
	
	/** 0x07
	 */
	public void bell()
	{
		// ding, ding, ding
	}
	
	/** 0x08
	 */
	public void backspace()
	{
		writePage.backspace(ppRemote);
		requiresRepaint = true;
	}
	
	/** 0x09
	 */
	public void tab()
	{
		writePage.tab(ppRemote, 1);
		requiresRepaint = true;
	}	
	
	/** 0x0A
	 */
	public void linefeed()
	{
		writePage.cursorDown(ppRemote);
		requiresRepaint = true;
	}
	
	/** 0x0D
	 */
	public void carageReturn()
	{
		writePage.carageReturn(ppRemote);
		requiresRepaint = true;
	}
	
	/** ESC J
	 */
	public void setCursorRowCol(int row, int col)
	{
		ASSERT.fatal(row < numRows && col < numColumns, "", 2, "");
		ASSERT.fatal(row >= 0 && col >= 0, "", 2, "");
		writePage.setCursor(ppRemote, row, col);
		requiresRepaint = true;
	}
	
	public void setBufferRowCol(int row, int col)
	{
		ASSERT.fatal(row < numRows && col < numColumns, "", 2, "");
		ASSERT.fatal(row >= 0 && col >= 0, "", 2, "");
		writePage.setBuffer(row, col);
		requiresRepaint = true;
	}

	public void setVideoPriorCondition(int attr)
	{
		writePage.setVideoPriorCondition(attr);
	}

	public void setInsertMode(int mode)
	{
		writePage.insertMode = mode;
	}
		
	/** ESC 0
	 */
	public void printScreen()
	{
	}
	
	/** ESC 1
	 * 
	 *  Set a tab at the current cursor location
	 */
	public void setTab()
	{
	}
	
	/** ESC 2
	 * 
	 *  Clear the tab at the current cursor location
	 */
	public void clearTab()
	{
	}
	
	/** ESC 3
	 */
	public void clearAllTabs()
	{
	}
		
	/** ESC i
	 */
	public void backtab()
	{
		displayPage.tab(ppRemote, -1);
		requiresRepaint = true;
	}
	
	/** ESC 6
	 * 
	 *  All subsuquent writes use this attribute
	 */
	public void setWriteAttribute(int attr)
	{
		ASSERT.fatal ( (attr & MASK_CHAR) == 0, "TextDisplay", 277, "Illegal attribute");
		writePage.setWriteAttribute(attr);
	}

	/** ESC 7
	 *  Not sure what this is supposed to do
	 */
	public void setPriorWriteAttribute(int attr)
	{
		ASSERT.fatal ( (attr & MASK_CHAR) == 0, "TextDisplay", 286, "Illegal attribute");
		writePage.setWriteAttribute(attr);
	}
	
	/** ESC ! or ESC ' '
	 */
	public void setDisplayPage(int page)
	{
		//if (page >= numPages)
		//{
		//	return;
		//}
		displayPage = pages[page];
		displayPage.forceDirty();
		requiresRepaint = true;
	}
	
	/** ESC A
	 */
	public void moveCursorUp()
	{
		writePage.cursorUp(ppRemote);
		requiresRepaint = true;
	}
	
	/** ESC C
	 */
	public void moveCursorRight()
	{
		writePage.cursorRight(ppRemote);
		requiresRepaint = true;
	}
	
	/** ESC H
	 */
	public void home()
	{
		writePage.home(ppRemote);
		requiresRepaint = true;
	}
	
	/** ESC F
	 */
	public void end()
	{
		writePage.end(ppRemote);
		requiresRepaint = true;
	}
	
	/** ESC I
	 */
	public void clearPage()
	{
		writePage.setWriteAttribute(VID_NORMAL);
		writePage.setVideoPriorCondition(VID_NORMAL);
		writePage.clearPage();
		requiresRepaint = true;
	}
	
	/** ESC J
	 */
	public void clearToEnd()
	{
		writePage.clearToEOP(ppRemote);
		requiresRepaint = true;
	}
	
	/** ESC I
	 */
	public void clearBlock(int startRow, int startCol, int endRow, int endCol)
	{
		writePage.clearBlock(ppRemote, startRow, startCol, endRow, endCol);
		requiresRepaint = true;
	}
	
	/** ESC K
	 *  In block mode, erase the field.  In
	 *  conversation mode, clear to end of line
	 */
	public void clearEOL()
	{
		writePage.clearToEOL(ppRemote);
		requiresRepaint = true;
	}
	
	/** 0x1D
	 */
	public void startField(int videoAttr, int dataAttr)
	{
		writeField(decodeVideoAttrs(videoAttr) | decodeDataAttrs(dataAttr) | ' ');
	}
	
	/** ESC [
	 */
	public void startField(int videoAttr, int dataAttr, int keyAttr)
	{
		writeField(decodeVideoAttrs(videoAttr) | decodeDataAttrs(dataAttr) | decodeKeyAttrs(keyAttr) | ' ');
	}

	public byte[] readBufferAllMdt(int startRow, int startCol, int endRow, int endCol)
	{
		return readBuffer(DAT_MDT, 0, startRow, startCol, endRow, endCol);
	}
	
	public byte[] readBufferAllIgnoreMdt(int startRow, int startCol, int endRow, int endCol)
	{
		return readBuffer(0, 0, startRow, startCol, endRow, endCol);
	}
	
	/** ESC - <
	 * 
	 * PROTECT MODE
	 *  Read all the unprotected fields in the block
	 *
	 * UNPROTECT MODE 
	 *  Return raw characters in the block
	 */
	public byte[] readBufferUnprotectIgnoreMdt(int startRow, int startCol, int endRow, int endCol)
	{
		return readBuffer(DAT_UNPROTECT, 0, startRow, startCol, endRow, endCol);
	}
	
	public byte[] readBufferUnprotect(int startRow, int startCol, int endRow, int endCol)
	{
		return readBuffer(DAT_UNPROTECT | DAT_MDT, 0, startRow, startCol, endRow, endCol);
	}
	
	private byte[] readBuffer(int reqMask, int forbidMask, int startRow, int startCol, int endRow, int endCol)
	{
		return writePage.readBuffer(ppRemote, reqMask, forbidMask, startRow, startCol, endRow, endCol);
	}
	
	/** ESC ]
	 * 
	 *  Read all the fields in the block (protected and unprotected)
	 */
	public byte[] readFieldsAll(int startRow, int startCol, int endRow, int endCol)
	{
		return readBuffer(0, 0, startRow, startCol, endRow, endCol);
	}

	/** ESC >
	 *  reset all modified data tags for unprotected fields
	 */
	public void resetMdt()
	{
		writePage.resetMDTs();
	}
	
	/** ESC O
	 */
	public void insertChar()
	{
		writePage.insertChar(ppRemote);
		requiresRepaint = true;
	}
		
	/** ESC M
	 */			
	public void setModeBlock()
	{
		blockMode = true;
		exitProtectMode();
		ppRemote = ppunProtectMode;
	}
	
	public void setModeConv()
	{
		blockMode = false;
		displayPage = pages[1];
		writePage = pages[1];
		ppRemote = ppunProtectMode;
		setInsertMode(INSERT_INSERT);
		clearAllTabs();
		keysLocked();
		clearAll();
		writeStatus("CONV");
		protectMode = false;
		ppRemote = ppconvMode;
		requiresRepaint = true;
	}

	/** ESC p
	 */
	public void setPageCount(int count)
	{
		numPages = count;
	}
	
	/** ESC q
	 */
	public void init()
	{
		pages = new Page[numPages+1];

		for (int x = 0; x < numPages+1; x++)
		{
			pages[x] = new Page(numRows, numColumns);
		}
		displayPage = pages[0];
		writePage = pages[0];		
		requiresRepaint = true;
	}
	
	public void writeStatus(String msg)
	{
		for (int x = 67; x < 80; x++)
		{
			statusLine.setCharAt(x, ' ');
		}
		statusLine.insert(67, msg);
		requiresRepaint = true;
	}

	/** ESC o
	 *  
	 */
	public void writeMessage(String msg)
	{
		for (int x = 1; x < 66; x++)
		{
			statusLine.setCharAt(x, ' ');
		}
		statusLine.insert(1, msg);
		requiresRepaint = true;
	}

	public void initDataTypeTable()
	{
	}
	
	public int getNumColumns()
	{
		return numColumns;
	}
	
	public int getNumRows()
	{
		return numRows;
	}
	
	public int getCurrentPage()
	{
		for (int x = 0; x < pages.length; x++)
		{
			if (pages[x] == writePage)
			{
				return x;
			}
		}
		ASSERT.fatal(false, "TextDisplay", 713, "Shouldn't get here");
		return 1;
	}
	
	public int getCursorCol()
	{
		return writePage.cursorPos.column + 1;
	}
	
	public int getCursorRow()
	{
		return writePage.cursorPos.row + 1;
	}	

	public int getBufferCol()
	{
		return writePage.bufferPos.column + 1;
	}
	
	public int getBufferRow()
	{
		return writePage.bufferPos.row + 1;
	}
	
	public boolean getProtectMode()
	{
		return protectMode;
	}
	
	public boolean getBlockMode()
	{
		return blockMode;
	}
	
	public void setEchoOn()
	{
		echoOn = true;
	}
	
	public void setEchoOff()
	{
		echoOn = false;
	}

	
	/** ESC A
	 */
	void cursorUp()
	{
		writePage.cursorUp(ppRemote);
		requiresRepaint = true;
	}
	
	/** 0x0A
	 */
	void cursorDown()
	{
		writePage.cursorDown(ppRemote);
		requiresRepaint = true;
	}
	
	/** ESC C
	 */
	void cursorRight()
	{
		writePage.cursorRight(ppRemote);
		requiresRepaint = true;
	}
	
	/** ESC L
	 */
	public void lineDown()
	{
		requiresRepaint = true;
	}

	/** ESC M
	 */
	public void deleteLine()
	{
		requiresRepaint = true;
	}

	/** ESC O
	 * 
	 *  Insert a space
	 */
	public void insert()
	{
		requiresRepaint = true;
	}
		
	public String getStartFieldASCII()
	{
		return displayPage.getStartFieldASCII();
	}
	
	private void writeField(int c)
	{
		writePage.writeField(c);
	}
	
	/** ESC P
	 * Delete a character at a given position on the screen.
	 * All characters right to the position will be moved one to the left.
	 */
	public void deleteChar()
	{
		writePage.deleteChar(ppRemote);
		requiresRepaint = true;
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
	private final void cursorLeft()
	{
		writePage.cursorLeft(ppRemote);
		requiresRepaint = true;
	}
	
	
	private final void clearAll()
	{
		for (int x = 0; x < pages.length; x++)
		{
			pages[x].clearPage();
			pages[x].bufferPos.clear();
			pages[x].cursorPos.clear();
			pages[x].writeAttr = VID_NORMAL;
			pages[x].priorAttr = VID_NORMAL;
		}
		requiresRepaint = true;
	}
	
	
		
	public final void paint(PaintSurface ps)
	{
		displayPage.paint(ps, statusLine.toString());
		requiresRepaint = false;
	}
			
	private int decodeKeyAttrs(int attr)
	{
		int ret = 0;
		if (attr == 0)
		{
			return 0;
		}		
		ASSERT.fatal ( (attr & (1<<6)) != 0, "TextDisplay", 367, "Invalid attribute format");
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
			System.out.println("Unknown video attr " + attr);
		return ret;
	}
	
	private int decodeDataAttrs(int attr)
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
		ASSERT.fatal( (attr & ~((1<<6)|(1<<5)|(1<<4)|(1<<0)|(1<<1)|(1<<2)|(1<<3))) == 0, "TextDisplay", 737, "Unknown DAT attr");
		return ret;
	}
	
	private int decodeVideoAttrs(int attr)
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
			System.out.println("Unknown video attr " + attr);
		return ret;
	}	

	public void dumpScreen(java.io.PrintStream pw)
	{
		if (pw == System.out)
			return;
		pw.println(dumpScreen());
	}
	
	public String dumpScreen()
	{
		StringBuffer pw = new StringBuffer();
		
		for (int r = 0; r < displayPage.numRows; r++)
		{
			for (int c = 0; c < displayPage.numColumns; c++)
			{
				int cell = displayPage.mem[r][c];
				pw.append((char)(cell & 0xFF));
			}
			pw.append("" + (char)13 + "" + (char)10);
		}
		pw.append("" + (char)13 + "" + (char)10);
		return pw.toString();
	}
	
	public String dumpAttibutes()
	{
		StringBuffer pw = new StringBuffer();
		for (int r = 0; r < displayPage.numRows; r++)
		{
			for (int c = 0; c < displayPage.numColumns; c++)
			{
				int cell = displayPage.mem[r][c];
				pw.append((char)(cell & 0xFF));
				if ( (cell & VID_NORMAL) != 0)
				{
					pw.append("N");
				}
				else
				{
					pw.append("0");
				}
				if ( (cell & VID_BLINKING) != 0)
				{
					pw.append("B");
				}
				else
				{
					pw.append("0");
				}
				if ( (cell & VID_REVERSE) != 0)
				{
					pw.append("R");
				}
				else
				{
					pw.append("0");
				}
				if ( (cell & VID_INVIS) != 0)
				{
					pw.append("I");
				}
				else
				{
					pw.append("0");
				}
				if ( (cell & VID_UNDERLINE) != 0)
				{
					pw.append("U");
				}
				else
				{
					pw.append("0");
				}
				if ( (cell & DAT_MDT) != 0)
				{
					pw.append("M");
				}
				else
				{
					pw.append("0");
				}
				if ( (cell & DAT_TYPE) != 0)
				{
					pw.append((c>>SHIFT_DAT_TYPE) & 7);
				}
				else
				{
					pw.append("0");
				}
				if ( (cell & DAT_AUTOTAB) != 0)
				{
					pw.append("A");
				}
				else
				{
					pw.append("0");
				}
				if ( (cell & DAT_UNPROTECT) != 0)
				{
					pw.append("0");
				}
				else
				{
					pw.append("P");
				}
				if ( (cell & KEY_UPSHIFT) != 0)
				{
					pw.append("S");
				}
				else
				{
					pw.append("0");
				}
				if ( (cell & KEY_KB_ONLY) != 0)
				{
					pw.append("K");
				}
				else
				{
					pw.append("0");
				}
				if ( (cell & KEY_AID_ONLY) != 0)
				{
					pw.append("");
				}
				else
				{
					pw.append("");
				}
				if ( (cell & KEY_EITHER) != 0)
				{
					pw.append("");
				}
				else
				{
					pw.append("");
				}
				if ( (cell & CHAR_START_FIELD) != 0)
				{
					pw.append("F");
				}
				else
				{
					pw.append("0");
				}
				pw.append(",");
			}
			pw.append("" + (char)13 + "" + (char)10);
		}
		pw.append("" + (char)13 + "" + (char)10);
		return pw.toString();
	}

	/**
	 *  Get the 'index'nth field on the screen.
	 *  The first field is index ZERO.  If the
	 *  index is larger than the number of field,
	 *  an empty string is returned.
	 */
	public String getField(int index)
	{
		int count = 0;
		boolean cap = false;
		StringBuffer accum = new StringBuffer();
		
		for (int r = 0; r < numRows; r++)
		{
			for (int c = 0; c < numColumns; c++)
			{
				if ( (displayPage.mem[r][c] & CHAR_START_FIELD) != 0)
				{
					if (cap)
					{
						return accum.toString();
					}
					if (count++ == index)
					{
						cap = true;
					}
				}
				if (cap)
				{
					accum.append ((char)(displayPage.mem[r][c] & MASK_CHAR));
				}
			}
		}
		return accum.toString();
	}
	
	/**
	 *  Get the video, data, and key attributes for a
	 *  field.
	 */
	public int getFieldAttributes(int index)
	{
		int count = 0;
		
		for (int r = 0; r < numRows; r++)
		{
			for (int c = 0; c < numColumns; c++)
			{
				if ( (displayPage.mem[r][c] & CHAR_START_FIELD) != 0)
				{
					if (count++ == index)
					{
						int r2 = r;
						int c2 = c+1;
						if (c2 >= numColumns)
						{
							r2++;
							c2 = 0;
						}
						return displayPage.mem[r2][c2] & MASK_FIELD;
					}
				}
			}
		}
		return 0;
	}

	/**
	 *  Get the text in the field at the cursor
	 *  position.
	 */
	public String getCurrentField()
	{
		return displayPage.getCurrentField();
	}
	
	/**
	 *  Get the 'index'nth unprotected field on 
	 *  the screen.  The first field is index 
	 *  ZERO.  If the index is larger than the 
	 *  number of field, an empty string is 
	 *  returned.
	 */
	public String getUnprotectField(int index)
	{
		int count = 0;
		boolean cap = false;
		StringBuffer accum = new StringBuffer();
		
		for (int r = 0; r < numRows; r++)
		{
			for (int c = 0; c < numColumns; c++)
			{
				if ( (displayPage.mem[r][c] & CHAR_START_FIELD) != 0)
				{
					if (cap)
					{
						return accum.toString();
					}
					int r2 = r;
					int c2 = c+1;
					if (c2 >= numColumns)
					{
						c2 = 0;
						r2++;
					}
					if ( (displayPage.mem[r2][c2] & DAT_UNPROTECT) != 0)
					{
						if (count++ == index)
						{
							cap = true;
						}
					}
				}
				if (cap)
				{
					accum.append ((char)(displayPage.mem[r][c] & MASK_CHAR));
				}
			}
		}
		return accum.toString();
	}
	
	/**
	 *  Write text into the 'index'nth 
	 *  unprotected field on the screen.  The 
	 *  first field is index ZERO.  If the 
	 *  index is larger than the number of field, 
	 *  the request is ignored.
	 */
	public void setField(int index, String text)
	{
		int count = 0;
		
		for (int r = 0; r < numRows; r++)
		{
			for (int c = 0; c < numColumns; c++)
			{
				if ( (displayPage.mem[r][c] & CHAR_START_FIELD) != 0)
				{
					int r2 = r;
					int c2 = c+1;
					if (c2 >= numColumns)
					{
						c2 = 0;
						r2++;
					}
					if ( (displayPage.mem[r2][c2] & DAT_UNPROTECT) != 0)
					{
						if (count++ == index)
						{
							setCursorRowCol(r2, c2);
							writeDisplay(text);
							requiresRepaint = true;
							return;
						}
					}
				}
			}
		}
	}
	
	/**
	 *  Returns true if the 'index'nth unprotected
	 *  field has its MDT set. The first field is 
	 *  index ZERO.  If the index is larger than 
	 *  the  number of fields, false is returned.
	 */
	public boolean isFieldChanged(int index)
	{
		int count = 0;
		
		for (int r = 0; r < numRows; r++)
		{
			for (int c = 0; c < numColumns; c++)
			{
				if ( (displayPage.mem[r][c] & CHAR_START_FIELD) != 0)
				{
					int r2 = r;
					int c2 = c+1;
					if (c2 >= numColumns)
					{
						c2 = 0;
						r2++;
					}
					if ( (displayPage.mem[r2][c2] & DAT_UNPROTECT) != 0)
					{
						if (count++ == index)
						{
							return (displayPage.mem[r2][c2] & DAT_MDT) != 0;
						}
					}
				}
			}
		}
		return false;
	}
	
	/**
	 *  Get a full line of display text.  
	 */
	public String getLine(int lineNumber)
	{
		StringBuffer sb = new StringBuffer();
		for (int c = 0; c < numColumns; c++)
		{
			sb.append((char)(displayPage.mem[lineNumber][c] & MASK_CHAR));
		}
		return sb.toString();
	}
	
	/**
	 *  Set the cursor at the start if the 
	 *  'index'nth unprotected field on the screen.  
	 *  The first field is index ZERO.  If the 
	 *  index is larger than the number of field, 
	 *  the request is ignored.
	 */
	public void cursorToField(int index)
	{
		int count = 0;
		
		for (int r = 0; r < numRows; r++)
		{
			for (int c = 0; c < numColumns; c++)
			{
				if ( (displayPage.mem[r][c] & CHAR_START_FIELD) != 0)
				{
					int r2 = r;
					int c2 = c+1;
					if (c2 >= numColumns)
					{
						c2 = 0;
						r2++;
					}
					if ( (displayPage.mem[r2][c2] & DAT_UNPROTECT) != 0)
					{
						if (count++ == index)
						{
							setCursorRowCol(r2, c2);
							requiresRepaint = true;
							return;
						}
					}
				}
			}
		}
	}
	
	/**
	 *  ASP fun!! Not quite working yet.
	 */
	public String toHTML(Color fg, Color bg)
	{
		String fgRGB = Integer.toHexString(fg.getRGB()).substring(2);
		String bgRGB = Integer.toHexString(bg.getRGB()).substring(2);
		int fieldCount = 0;
		boolean inUnprot = false;
		
		StringBuffer buf = new StringBuffer();
		StringBuffer accum = new StringBuffer();
		
		// write the style and script
		buf.append("<html>");
		buf.append("<script language='javascript'>function keys(){if (event.keyCode < 112 || event.keyCode > 123) {event.returnValue=true;return;} event.cancelBubble=true; event.returnValue=false;var k = document.forms('screen')('hdnKey'); switch(event.keyCode){case 112: k.value = 'F1'; break; case 10: k.value = 'ENTER'; break;} document.forms('screen').submit();} function canxIt(){event.cancelBubble = true;event.returnValue = false;}</script>");
		buf.append("<script language='javascript'>function loaded(){document.onkeydown=keys; document.onhelp=canxIt; var f = document.forms('screen')('F0'); if (f != null)f.focus();}</script>");
		buf.append("<script language='javascript'>function tabcheck(field){var f = document.forms('screen')('F'+field); if (f.value.length == f.maxLength()){field++; if (document.forms('screen')('F'+field) != null){document.forms('screen')('F'+field).focus();}else{document.forms('screen')('F0').focus();}}}</script>\r\n");
		buf.append("<body onload='loaded()' style='color: green; background: #3F3F3F'>\r\n");
		buf.append("<style type='text/css' >");
		buf.append(".normal { color: #" + fgRGB + "; background: #" + bgRGB + "; text-decoration: none}");
		buf.append(".reverse { color: #" + bgRGB + "; background: #" + fgRGB + "; text-decoration: none}");
		buf.append(".underline { color: #" + fgRGB + "; background: #" + bgRGB + "; text-decoration: underline} ");
		buf.append(".reverseunderline { color: #" + bgRGB + "; background: #" + fgRGB + "; text-decoration: underline}");
		buf.append(".blink { color: #" + fgRGB + "; background: #" + bgRGB + "; text-decoration: blink}");
		buf.append(".blinkreverse { color: #" + bgRGB + "; background: #" + fgRGB + "; text-decoration: blink}");
		buf.append("</style>\r\n");
		
		// write the table header
		buf.append("<form id='screen' method='post'><input type='hidden' id='hdnKey' value='' /><table cols='80' width='100%' >");
		
		for (int r = 0; r < numRows; r++)
		{
			buf.append("<tr>");
			int[] row = displayPage.mem[r];
			
			for (int c = 0; c < numColumns; c++)
			{
				int ch = row[c];

				if (! inUnprot)
				{
					buf.append("<td class=");
					if ( (ch & MASK_COLOR) == 0)
					{
					}
					else
					{
					}
					if ( (ch & (VID_UNDERLINE|VID_REVERSE)) == (VID_UNDERLINE|VID_REVERSE))
					{
						buf.append("reverseunderline");
					}
					else if ( (ch & (VID_BLINKING|VID_REVERSE)) == (VID_BLINKING|VID_REVERSE))
					{
						buf.append("blinkreverse");
					}
					else if ( (ch & VID_BLINKING) != 0)
					{
						buf.append("blink");
					}
					else if ( (ch & VID_REVERSE) != 0)
					{
						buf.append("reverse");
					}
					else if ( (ch & VID_UNDERLINE) != 0)
					{
						buf.append("underline");
					}
					else
					{
						buf.append("normal");
					}
					if ( (ch & CHAR_START_FIELD) == 0)
					{
						buf.append(">" + (char)(ch & MASK_CHAR) + "</td>");
					}
				}
				if ( (ch & CHAR_START_FIELD) != 0)
				{
					if (inUnprot)
					{
						// end the input tag
						accum.append("' maxlength='" + accum.length() + "' />");
						inUnprot = false;
						buf.append(" colspan=" + accum.length() + ">" + accum.toString() + "</td>");
						accum.setLength(0);
					}
					// is the new field unprotected?
					int c2 = c + 1;
					if (c2 >= numColumns)
					{
						c2 = 0;
						row = displayPage.mem[++r];
					}
					if ( (row[c2] & DAT_UNPROTECT) != 0)
					{
						inUnprot = true;
						accum.append ("<input type='text' id='F" + fieldCount + "' onkeypress='tabcheck(" + fieldCount + ")' value='" + (char)(ch&MASK_CHAR));
						fieldCount++;
					}
				}
				else if (inUnprot)
				{
					accum.append((char)(ch & MASK_CHAR));
				}
			}
			buf.append("</tr>\r\n");
		}
		buf.append("</table></form><p/><center><a href='mailto:johnga@dor.wa.gov'>Got Bugs?</a></center></body></html>");
		return buf.toString();
	}
}
