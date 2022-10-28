package gov.revenue.vt6530.ui;

/**
 *  Handles unprotect mode stuff.  Unprotect mode
 *  probably doesn't work right since we don't 
 *  have any unprotect screen.
 */
public class UnprotectPage extends SharedProtocol
{
	void tab(int inc)
	{
		/** 0x09
		 * 
		 * UNPROTECT MODE
		 *  Move the the next tab stop on the row.  If the
		 *  cursor is past the last tab stop, move to 
		 *  column 1 of the next row.
		 */
	}
	
	public void clearToEOL(Page page)
	{
		for (int x = page.cursorPos.column; x < page.numColumns; x++)
		{
			page.mem[page.cursorPos.row][x] = ' ';
		}
	}

	public byte[] readBuffer(Page page, int reqMask, int forbidMask, int startRow, int startCol, int endRow, int endCol)
	{
		StringBuffer accum = new StringBuffer();
		
		for (int y = startRow-1; y < endRow-1; y++)
		{
			for (int x = startCol-1; x < endCol-1; x++)
			{
				accum.append("" + (char)(page.mem[y][x] & 0xFF) );
			}
			accum.append("" + (char)13);
		}
		return accum.toString().getBytes();
	}

}
