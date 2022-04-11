using System;
using System.Collections.Generic;
using System.Text;

namespace LibVt6530
{
	public interface PageProtocol
	{
		void WriteBuffer(Page page, byte[] text);
		void WriteCursor(Page page, byte[] text);
		void WriteBuffer(Page page, string text);
		void WriteCursor(Page page, string text);
		void WriteChar(Page page, Cursor cursor, int c);
		void InsertChar(Page page);
		void DeleteChar(Page page);
		void Backspace(Page page);
		void Tab(Page page, int inc);
		void CarageReturn(Page page);
		void Linefeed(Page page);
		void Home(Page page);
		void End(Page page);
		
		void ValidateCursorPos(Page page);
		void SetCursor(Page page, int row, int col);
		void CursorLeft(Page page);
		void CursorRight(Page page);
		void CursorDown(Page page);
		void CursorUp(Page page);

		void ClearToEOL(Page page);
		void ClearToEOP(Page page);
		void ClearBlock(Page page, int startRow, int startCol, int endRow, int endCol);
		
		void ReadBuffer(StringBuilder outp, Page page, int reqMask, int forbidMask, int startRow, int startCol, int endRow, int endCol);
	};
}
