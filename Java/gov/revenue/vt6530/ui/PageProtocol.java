package gov.revenue.vt6530.ui;

import java.awt.Graphics;

/**
 *  Interface to the shared, unprotect, and
 *  protect mode virtual character display
 *  modes.
 */
public interface PageProtocol extends Attributes
{
	static final int MODE_DISPLAY = 0;
	static final int MODE_PROTECT = 1;
	static final int MODE_UNPROTECT = 2;

	void writeBuffer(Page page, String text);
	void writeCursor(Page page, String text);
	void writeChar(Page page, Cursor cursor, int c);
	
	void insertChar(Page page);
	void deleteChar(Page page);
	void backspace(Page page);
	void tab(Page page, int inc);
	void carageReturn(Page page);
	void linefeed(Page page);
	
	void validateCursorPos(Page page);
	void setCursor(Page page, int row, int col);
	void cursorLeft(Page page);
	void cursorRight(Page page);
	void cursorDown(Page page);
	void cursorUp(Page page);

	void home(Page page);
	void end(Page page);
	
	void clearToEOL(Page page);
	void clearToEOP(Page page);
	void clearBlock(Page page, int startRow, int startCol, int endRow, int endCol);
	
	byte[] readBuffer(Page page, int reqMask, int forbidMask, int startRow, int startCol, int endRow, int endCol);
}
