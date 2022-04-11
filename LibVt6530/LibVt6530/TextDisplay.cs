using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Maximum.AnsiTerm;

namespace LibVt6530
{
	public class TextDisplay 
	{
		protected StringBuilder m_statusLine = new StringBuilder();
		
		//int m_charWidth;      /* current width of a char */
		//int m_charHeight;      /* current height of a char */
		//int m_charDescent;      /* base line descent */	
		
		protected Page m_displayPage;
		protected Page m_writePage;
		protected Page[] m_pages;
		protected int m_numPages;
		
		protected bool m_echoOn;
		protected bool m_blockMode;
		protected bool m_protectMode;
		
		protected bool m_requiresRepaint;
		
		protected PageProtocol m_ppprotectMode;
		protected PageProtocol m_ppunProtectMode;
		protected PageProtocol m_ppconvMode;

		protected PageProtocol m_ppRemote;
		
		protected bool m_keysLocked;
		
		protected int m_numRows, m_numColumns;

		protected Color m_foreground;
		protected Color m_background;

		public TextDisplay(int pageCount, int cols, int rows)
		{
			m_echoOn = true;
			m_blockMode = false;
			m_protectMode = false;
			
			m_requiresRepaint = true;
			
			m_ppprotectMode = new ProtectPage();
			m_ppunProtectMode = new UnprotectPage();
			m_ppconvMode = new UnprotectPage();

			m_numPages = pageCount;
			m_numRows = rows;
			m_numColumns = cols;
			m_ppRemote = m_ppconvMode;
			
			m_pages = null;
			Init();

			m_statusLine.Append("".PadLeft(80));
			m_statusLine.Insert(67, "CONV");
			m_statusLine.Length = m_numColumns;
		}

		public void SetForeGroundColor(Color color)
		{
			m_foreground = color;
		}

		public void SetBackGroundColor(Color color)
		{
			m_background = color;
		}

		public Color GetForeGroundColor()
		{
			return m_foreground;
		}

		public Color GetBackGroundColor()
		{
			return m_background;
		}

		public bool NeedsRepaint()
		{
			return m_requiresRepaint;
		}
		
		public void SetRePaint(bool val)
		{
			m_requiresRepaint = val;
		}
		
		public void WriteBuffer(byte[] text)
		{
			m_writePage.WriteBuffer(m_ppRemote, text);
		}

		public void WriteBuffer(string text)
		{
			byte[] buf = new byte[text.Length];
			for (int x = 0; x < buf.Length; x++)
			{
				buf[x] = (byte)text[x];
			}
			m_writePage.WriteBuffer(m_ppRemote, buf);
		}

		public void WriteDisplay(byte[] text)
		{
			m_displayPage.WriteCursor(m_ppRemote, text);
			m_requiresRepaint = true;
		}

		public void WriteDisplay(string text)
		{
			byte[] buf = new byte[text.Length];
			for (int x = 0; x < buf.Length; x++)
			{
				buf[x] = (byte)text[x];
			}
			m_displayPage.WriteCursor(m_ppRemote, buf);
			m_requiresRepaint = true;
		}

		public void WriteLocal(string text)
		{
			byte[] buf = new byte[text.Length];
			for (int x = 0; x < text.Length; x++)
			{
				buf[x] = (byte)text[x];
			}
			m_displayPage.WriteCursorLocal(m_ppRemote, buf);
			m_ppRemote.ValidateCursorPos(m_displayPage);
			m_requiresRepaint = true;
		}

		public void WriteLocal(byte[] data)
		{
			m_displayPage.WriteCursorLocal(m_ppRemote, data);
			m_ppRemote.ValidateCursorPos(m_displayPage);
			m_requiresRepaint = true;
		}

		public void EchoDisplay(string text)
		{
			if (m_echoOn)
			{
				m_displayPage.WriteCursor(m_ppRemote, text);
				m_requiresRepaint = true;
				return;
			}
			char c = text[0];
			if (c == 13)
			{
				byte[] buf = new byte[3];
				buf[0] = (byte)c;
				buf[1] = (byte)10;
				buf[2] = (byte)0;
				m_displayPage.WriteCursor(m_ppRemote, buf);
				m_requiresRepaint = true;
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
		public void SetProtectMode()
		{
			m_protectMode = true;
			m_displayPage = m_pages[1];
			m_writePage = m_pages[1];
			m_ppRemote = m_ppprotectMode;
			ClearAll();
			SetVideoPriorCondition((int)VideoAttribs.VID_NORMAL);
			SetInsertMode((int)VideoAttribs.INSERT_INSERT);
			InitDataTypeTable();
			SetKeysLocked();
			WriteStatus("BLOCK PROT");
			m_requiresRepaint = true;
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
		public void ExitProtectMode()
		{
			m_displayPage = m_pages[1];
			m_writePage = m_pages[1];
			m_ppRemote = m_ppunProtectMode;
			ClearAll();
			SetInsertMode((int)VideoAttribs.INSERT_INSERT);
			ClearAllTabs();
			SetKeysLocked();
			WriteStatus("BLOCK");
			m_protectMode = false;
		}
		
		public void SetKeysLocked()
		{
			m_keysLocked = true;
		}
		
		public void SetKeysUnlocked()
		{
			m_keysLocked = false;
		}
		
		/** ESC :
		 */
		public void SetPage(int page)
		{
			//if (page >= numPages)
			//{
			//	return;
			//}
			m_writePage = m_pages[page];
		}
		
		/** 0x07
		 */
		public void Bell()
		{
			// ding, ding, ding
		}
		
		/** 0x08
		 */
		public void Backspace()
		{
			m_writePage.Backspace(m_ppRemote);
			m_requiresRepaint = true;
		}
		
		/** 0x09
		 */
		public void Tab()
		{
			m_writePage.Tab(m_ppRemote, 1);
			m_requiresRepaint = true;
		}	
		
		/** 0x0A
		 */
		public void Linefeed()
		{
			m_writePage.CursorDown(m_ppRemote);
			m_requiresRepaint = true;
		}
		
		/** 0x0D
		 */
		public void CarageReturn()
		{
			m_writePage.CarageReturn(m_ppRemote);
			m_requiresRepaint = true;
		}
		
		/** ESC J
		 */
		public void SetCursorRowCol(int row, int col)
		{
			Debug.Assert(row < m_numRows && col < m_numColumns);
			Debug.Assert(row >= 0 && col >= 0);

			m_writePage.SetCursor(m_ppRemote, row, col);
			m_requiresRepaint = true;
		}
		
		public void SetBufferRowCol(int row, int col)
		{
			Debug.Assert(row < m_numRows && col < m_numColumns);
			Debug.Assert(row >= 0 && col >= 0);

			m_writePage.SetBuffer(row, col);
			m_requiresRepaint = true;
		}

		public void SetVideoPriorCondition(int attr)
		{
			m_writePage.SetVideoPriorCondition(attr);
		}

		public void SetInsertMode(int mode)
		{
			m_writePage.SetInsertMode( mode );
		}
			
		/** ESC 0
		 */
		public void PrintScreen()
		{
		}
		
		/** ESC 1
		 * 
		 *  Set a tab at the current cursor location
		 */
		public void SetTab()
		{
		}
		
		/** ESC 2
		 * 
		 *  Clear the tab at the current cursor location
		 */
		public void ClearTab()
		{
		}
		
		/** ESC 3
		 */
		public void ClearAllTabs()
		{
		}
			
		/** ESC i
		 */
		public void Backtab()
		{
			m_displayPage.Tab(m_ppRemote, -1);
			m_requiresRepaint = true;
		}
		
		/** ESC 6
		 * 
		 *  All subsuquent writes use this attribute
		 */
		public void SetWriteAttribute(int attr)
		{
			Debug.Assert((attr & (int)VideoAttribs.MASK_CHAR) == 0);
			m_writePage.SetWriteAttribute(attr);
		}

		/** ESC 7
		 *  Not sure what this is supposed to do
		 */
		public void SetPriorWriteAttribute(int attr)
		{
			Debug.Assert((attr & (int)VideoAttribs.MASK_CHAR) == 0);
			m_writePage.SetWriteAttribute(attr);
		}
		
		/** ESC ! or ESC ' '
		 */
		public void SetDisplayPage(int page)
		{
			//if (page >= numPages)
			//{
			//	return;
			//}
			m_displayPage = m_pages[page];
			m_displayPage.ForceDirty();
			m_requiresRepaint = true;
		}
		
		/** ESC A
		 */
		public void MoveCursorUp()
		{
			m_writePage.CursorUp(m_ppRemote);
			m_requiresRepaint = true;
		}
		
		/** ESC C
		 */
		public void MoveCursorRight()
		{
			m_writePage.CursorRight(m_ppRemote);
			m_requiresRepaint = true;
		}
		
		/** ESC H
		 */
		public void Home()
		{
			m_writePage.SetCursor(m_ppRemote, 0, 0);
			m_requiresRepaint = true;
		}
		
		/** ESC F
		 */
		public void End()
		{
			m_writePage.SetCursor(m_ppRemote, m_numRows-1, 0);
			m_requiresRepaint = true;
		}
		
		/** ESC I
		 */
		public void ClearPage()
		{
			m_writePage.SetWriteAttribute((int)VideoAttribs.VID_NORMAL);
			m_writePage.SetVideoPriorCondition((int)VideoAttribs.VID_NORMAL);
			m_writePage.ClearPage();
			m_requiresRepaint = true;
		}
		
		/** ESC J
		 */
		public void ClearToEnd()
		{
			m_writePage.ClearToEOP(m_ppRemote);
			m_requiresRepaint = true;
		}
		
		/** ESC I
		 */
		public void ClearBlock(int startRow, int startCol, int endRow, int endCol)
		{
			m_writePage.ClearBlock(m_ppRemote, startRow, startCol, endRow, endCol);
			m_requiresRepaint = true;
		}
		
		/** ESC K
		 *  In block mode, erase the field.  In
		 *  conversation mode, clear to end of line
		 */
		public void ClearEOL()
		{
			m_writePage.ClearToEOL(m_ppRemote);
			m_requiresRepaint = true;
		}
		
		/** 0x1D
		 */
		public void StartField(int videoAttr, int dataAttr)
		{
			WriteField(DecodeVideoAttrs(videoAttr) | DecodeDataAttrs(dataAttr) | ' ');
		}
		
		/** ESC [
		 */
		public void StartField(int videoAttr, int dataAttr, int keyAttr)
		{
			WriteField(DecodeVideoAttrs(videoAttr) | DecodeDataAttrs(dataAttr) | DecodeKeyAttrs(keyAttr) | ' ');
		}

		public void ReadBufferAllMdt(StringBuilder outp, int startRow, int startCol, int endRow, int endCol)
		{
			ReadBuffer(outp, (int)VideoAttribs.DAT_MDT, 0, startRow, startCol, endRow, endCol);
		}
		
		public void ReadBufferAllIgnoreMdt(StringBuilder outp, int startRow, int startCol, int endRow, int endCol)
		{
			ReadBuffer(outp, 0, 0, startRow, startCol, endRow, endCol);
		}
		
		/** ESC - <
		 * 
		 * PROTECT MODE
		 *  Read all the unprotected fields in the block
		 *
		 * UNPROTECT MODE 
		 *  Return raw characters in the block
		 */
		public void ReadBufferUnprotectIgnoreMdt(StringBuilder outp, int startRow, int startCol, int endRow, int endCol)
		{
			ReadBuffer(outp, (int)VideoAttribs.DAT_UNPROTECT, 0, startRow, startCol, endRow, endCol);
		}
		
		public void ReadBufferUnprotect(StringBuilder outp, int startRow, int startCol, int endRow, int endCol)
		{
			ReadBuffer(outp, (int)VideoAttribs.DAT_UNPROTECT | (int)VideoAttribs.DAT_MDT, 0, startRow, startCol, endRow, endCol);
		}

		/** ESC ]
		 * 
		 *  Read all the fields in the block (protected and unprotected)
		 */
		public void ReadFieldsAll(StringBuilder outp, int startRow, int startCol, int endRow, int endCol)
		{
			ReadBuffer(outp, 0, 0, startRow, startCol, endRow, endCol);
		}

		/** ESC >
		 *  reset all modified data tags for unprotected fields
		 */
		public void ResetMdt()
		{
			m_writePage.ResetMDTs();
		}
		
		/** ESC O
		 */
		public void InsertChar()
		{
			m_writePage.InsertChar(m_ppRemote);
			m_requiresRepaint = true;
		}
			
		/** ESC M
		 */			
		public void SetModeBlock()
		{
			m_blockMode = true;
			ExitProtectMode();
			m_ppRemote = m_ppunProtectMode;
		}
				
		public void SetModeConv()
		{
			m_blockMode = false;
			m_displayPage = m_pages[1];
			m_writePage = m_pages[1];
			m_ppRemote = m_ppunProtectMode;
			SetInsertMode((int)VideoAttribs.INSERT_INSERT);
			ClearAllTabs();
			SetKeysLocked();
			WriteStatus("CONV");
			m_protectMode = false;
			m_ppRemote = m_ppconvMode;
			m_requiresRepaint = true;
		}

		public bool IsBlockMode() 
		{ 
			return m_blockMode; 
		}

		/** ESC p
		 */
		public void SetPageCount(int count)
		{
			m_numPages = count;
		}
		
		/** ESC q
		 */
		public void Init()
		{
			m_pages = new Page[m_numPages+2];

			for (int x = 0; x < m_numPages+1; x++)
			{
				m_pages[x] = new Page(m_numRows, m_numColumns);
			}
			m_pages[m_numPages+1] = null;
			m_displayPage = m_pages[0];
			m_writePage = m_pages[0];		
			m_requiresRepaint = true;
		}
				
		public void WriteStatus(string msg)
		{
			m_statusLine.Length = 67;
			m_statusLine.Insert(67, msg);
			m_statusLine.Append("".PadLeft(80));
			m_statusLine.Length = m_numColumns;
			m_requiresRepaint = true;
		}

		/** ESC o
		 *  
		 */
		public void WriteMessage(string msg)
		{
			for (int x = 1; x < 66; x++)
			{
				m_statusLine[x] = ' ';
			}
			m_statusLine.Insert(1, msg);
			m_requiresRepaint = true;
		}

		public void InitDataTypeTable()
		{
		}
		
		public int GetNumColumns()
		{
			return m_numColumns;
		}
		
		public int GetNumRows()
		{
			return m_numRows;
		}
				
		public int GetCurrentPage()
		{
			for (int x = 0; x < m_numPages+1; x++)
			{
				if (m_pages[x] == m_writePage)
				{
					return x;
				}
			}
			Debug.Assert(false);
			return 1;
		}
		
		public Page GetDisplayPage()
		{
			return m_displayPage;
		}
		
		public int GetCursorCol()
		{
			return m_writePage.m_cursorPos.m_column + 1;
		}
		
		public int GetCursorRow()
		{
			return m_writePage.m_cursorPos.m_row + 1;
		}	

		public int GetBufferCol()
		{
			return m_writePage.m_bufferPos.m_column + 1;
		}
		
		public int GetBufferRow()
		{
			return m_writePage.m_bufferPos.m_row + 1;
		}
		
		public bool GetProtectMode()
		{
			return m_protectMode;
		}
		
		public bool GetBlockMode()
		{
			return m_blockMode;
		}
		
		public void SetEchoOn()
		{
			m_echoOn = true;
		}
		
		public void SetEchoOff()
		{
			m_echoOn = false;
		}

		/** ESC A
		 */
		public void CursorUp()
		{
			m_writePage.CursorUp(m_ppRemote);
			m_requiresRepaint = true;
		}
		
		/** 0x0A
		 */
		public void CursorDown()
		{
			m_writePage.CursorDown(m_ppRemote);
			m_requiresRepaint = true;
		}
		
		/** ESC C
		 */
		public void CursorRight()
		{
			m_writePage.CursorRight(m_ppRemote);
			m_requiresRepaint = true;
		}
		
		/** ESC L
		 */
		public void LineDown()
		{
			m_requiresRepaint = true;
		}

		/** ESC M
		 */
		public void DeleteLine()
		{
			m_requiresRepaint = true;
		}

		/** ESC O
		 * 
		 *  Insert a space
		 */
		public void Insert()
		{
			m_requiresRepaint = true;
		}
			
		public void GetStartFieldASCII(StringBuilder sb)
		{
			m_displayPage.GetStartFieldASCII(sb);
		}
			
		/** ESC P
		 * Delete a character at a given position on the screen.
		 * All characters right to the position will be moved one to the left.
		 */
		public void DeleteChar()
		{
			m_writePage.DeleteChar(m_ppRemote);
			m_requiresRepaint = true;
		}		
			
		/*public void paint(PaintSurface *ps)
		{
			ASSERT_MEMdisplayPage, sizeof(Page));
			displayPage.paint(ps, *statusLine);
			requiresRepaint = false;
		}*/

		public override string ToString()
		{
			StringBuilder buf = new StringBuilder();
			DumpScreen(buf);
			return buf.ToString();
		}
				
		public void DumpScreen(StringBuilder pw)
		{	
			for (int r = 0; r < m_displayPage.GetNumRows(); r++)
			{
				for (int c = 0; c < m_displayPage.GetNumColumns(); c++)
				{
					pw.Append(m_displayPage.GetCell(c, r).Get());
				}
				pw.Append((char)13);
				pw.Append((char)10);
			}
			pw.Append((char)13);
			pw.Append((char)10);
		}


		public void DumpAttibutes(StringBuilder pw)
		{
			for (int r = 0; r < m_displayPage.GetNumRows(); r++)
			{
				for (int c = 0; c < m_displayPage.GetNumColumns(); c++)
				{
					int cell = m_displayPage.GetCell(c, r).GetAttributes();
					pw.Append(m_displayPage.GetCell(c, r).Get());
					if ((cell & (int)VideoAttribs.VID_NORMAL) != 0)
					{
						pw.Append("N");
					}
					else
					{
						pw.Append("0");
					}
					if ((cell & (int)VideoAttribs.VID_BLINKING) != 0)
					{
						pw.Append("B");
					}
					else
					{
						pw.Append("0");
					}
					if ((cell & (int)VideoAttribs.VID_REVERSE) != 0)
					{
						pw.Append("R");
					}
					else
					{
						pw.Append("0");
					}
					if ((cell & (int)VideoAttribs.VID_INVIS) != 0)
					{
						pw.Append("I");
					}
					else
					{
						pw.Append("0");
					}
					if ((cell & (int)VideoAttribs.VID_UNDERLINE) != 0)
					{
						pw.Append("U");
					}
					else
					{
						pw.Append("0");
					}
					if ((cell & (int)VideoAttribs.DAT_MDT) != 0)
					{
						pw.Append("M");
					}
					else
					{
						pw.Append("0");
					}
					if ((cell & (int)VideoAttribs.DAT_TYPE) != 0)
					{
						pw.Append((char)((c >> (int)VideoAttribs.SHIFT_DAT_TYPE) & 7));
					}
					else
					{
						pw.Append("0");
					}
					if ((cell & (int)VideoAttribs.DAT_AUTOTAB) != 0)
					{
						pw.Append("A");
					}
					else
					{
						pw.Append("0");
					}
					if ((cell & (int)VideoAttribs.DAT_UNPROTECT) != 0)
					{
						pw.Append("0");
					}
					else
					{
						pw.Append("P");
					}
					if ((cell & (int)VideoAttribs.KEY_UPSHIFT) != 0)
					{
						pw.Append("S");
					}
					else
					{
						pw.Append("0");
					}
					if ((cell & (int)VideoAttribs.KEY_KB_ONLY) != 0)
					{
						pw.Append("K");
					}
					else
					{
						pw.Append("0");
					}
					if ((cell & (int)VideoAttribs.KEY_AID_ONLY) != 0)
					{
						pw.Append("");
					}
					else
					{
						pw.Append("");
					}
					if ((cell & (int)VideoAttribs.KEY_EITHER) != 0)
					{
						pw.Append("");
					}
					else
					{
						pw.Append("");
					}
					if ((cell & (int)VideoAttribs.CHAR_START_FIELD) != 0)
					{
						pw.Append("F");
					}
					else
					{
						pw.Append("0");
					}
					pw.Append(",");
				}
				pw.Append((char)13);
				pw.Append((char)10);
			}
			pw.Append((char)13);
			pw.Append((char)10);
		}


		/**
		 *  Get the 'index'nth field on the screen.
		 *  The first field is index ZERO.  If the
		 *  index is larger than the number of field,
		 *  an empty string is returned.
		 */
		public void GetField(int index, StringBuilder accum)
		{
			int count = 0;
			bool cap = false;
			
			for (int r = 0; r < m_numRows; r++)
			{
				for (int c = 0; c < m_numColumns; c++)
				{
					if (m_displayPage.GetCell(c, r).IsStartField())
					{
						if (cap)
						{
							return;
						}
						if (count++ == index)
						{
							cap = true;
						}
					}
					if (cap)
					{
						accum.Append (m_displayPage.GetCell(c, r).Get());
					}
				}
			}
		}
				
		/**
		 *  Get the video, data, and key attributes for a
		 *  field.
		 */
		public int GetFieldAttributes(int index)
		{
			int count = 0;
			
			for (int r = 0; r < m_numRows; r++)
			{
				for (int c = 0; c < m_numColumns; c++)
				{
					if (m_displayPage.GetCell(c, r).IsStartField())
					{
						if (count++ == index)
						{
							int r2 = r;
							int c2 = c+1;
							if (c2 >= m_numColumns)
							{
								r2++;
								c2 = 0;
							}
							return m_displayPage.GetCell(c2, r2).GetAttributes();
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
		public void GetCurrentField(StringBuilder accum)
		{
			int r = m_displayPage.m_cursorPos.m_row;
			int c = m_displayPage.m_cursorPos.m_column;
			
			while (c > 0)
			{
				if (!m_displayPage.GetCell(c, r).IsStartField())
				{
					break;
				}
				c--;
			}
			c++;
			while (c < m_numColumns)
			{
				if (m_displayPage.GetCell(c, r).IsStartField())
				{
					break;
				}
				accum.Append (m_displayPage.GetCell(c, r).Get());
				c++;
			}
		}
		
		/**
		 *  Get the 'index'nth unprotected field on 
		 *  the screen.  The first field is index 
		 *  ZERO.  If the index is larger than the 
		 *  number of field, an empty string is 
		 *  returned.
		 */
		public void GetUnprotectField(int index, StringBuilder accum)
		{
			int count = 0;
			bool cap = false;
			
			for (int r = 0; r < m_numRows; r++)
			{
				for (int c = 0; c < m_numColumns; c++)
				{
					if (m_displayPage.GetCell(c, r).IsStartField())
					{
						if (cap)
						{
							return;
						}
						int r2 = r;
						int c2 = c+1;
						if (c2 >= m_numColumns)
						{
							c2 = 0;
							r2++;
						}
						if (m_displayPage.GetCell(c2, r2).IsUnprotect())
						{
							if (count++ == index)
							{
								cap = true;
							}
						}
					}
					if (cap)
					{
						accum.Append (m_displayPage.GetCell(c, r).Get());
					}
				}
			}
		}
		
		/**
		 *  Write text into the 'index'nth 
		 *  unprotected field on the screen.  The 
		 *  first field is index ZERO.  If the 
		 *  index is larger than the number of field, 
		 *  the request is ignored.
		 */
		public void SetField(int index, string text)
		{
			int count = 0;
			
			for (int r = 0; r < m_numRows; r++)
			{
				for (int c = 0; c < m_numColumns; c++)
				{
					if (m_displayPage.GetCell(c, r).IsStartField())
					{
						int r2 = r;
						int c2 = c+1;
						if (c2 >= m_numColumns)
						{
							c2 = 0;
							r2++;
						}
						if (m_displayPage.GetCell(c2, r2).IsUnprotect())
						{
							if (count++ == index)
							{
								SetCursorRowCol(r2, c2);
								WriteDisplay(text);
								m_requiresRepaint = true;
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
		public bool IsFieldChanged(int index)
		{
			int count = 0;
			
			for (int r = 0; r < m_numRows; r++)
			{
				for (int c = 0; c < m_numColumns; c++)
				{
					if (m_displayPage.GetCell(c, r).IsStartField())
					{
						int r2 = r;
						int c2 = c+1;
						if (c2 >= m_numColumns)
						{
							c2 = 0;
							r2++;
						}
						if (m_displayPage.GetCell(c2, r2).IsUnprotect())
						{
							if (count++ == index)
							{
								return m_displayPage.GetCell(c2, r2).IsMDT();
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
		public void GetLine(int lineNumber, StringBuilder sb)
		{
			for (int c = 0; c < m_numColumns; c++)
			{
				sb.Append(m_displayPage.GetCell(c, lineNumber).Get());
			}
		}
		
		/**
		 *  Set the cursor at the start if the 
		 *  'index'nth unprotected field on the screen.  
		 *  The first field is index ZERO.  If the 
		 *  index is larger than the number of field, 
		 *  the request is ignored.
		 */
		public void CursorToField(int index)
		{
			int count = 0;
			
			for (int r = 0; r < m_numRows; r++)
			{
				for (int c = 0; c < m_numColumns; c++)
				{
					if (m_displayPage.GetCell(c, r).IsStartField())
					{
						int r2 = r;
						int c2 = c+1;
						if (c2 >= m_numColumns)
						{
							c2 = 0;
							r2++;
						}
						if (r2 >= m_numRows)
						{
							break;
						}
						if (m_displayPage.GetCell(c2, r2).IsUnprotect())
						{
							if (count++ == index)
							{
								SetCursorRowCol(r2, c2);
								m_requiresRepaint = true;
								return;
							}
						}
					}
				}
			}
		}
						
		public void ToHTML(Color fg, Color bg, StringBuilder buf)
		{
			string fgRGB;
			string bgRGB;

			fg.AsHexString(out fgRGB);
			bg.AsHexString(out bgRGB);

			int fieldCount = 0;
			bool inUnprot = false;
			
			StringBuilder accum = new StringBuilder();
			
			// write the style and script
			buf.Append("<html>");
			buf.Append("<script language='javascript'>function keys(){if (event.keyCode < 112 || event.keyCode > 123) {event.returnValue=true;return;} event.cancelBubble=true; event.returnValue=false;var k = document.forms('screen')('hdnKey'); switch(event.keyCode){case 112: k.value = 'F1'; break; case 10: k.value = 'ENTER'; break;} document.forms('screen').submit();} function canxIt(){event.cancelBubble = true;event.returnValue = false;}</script>");
			buf.Append("<script language='javascript'>function loaded(){document.onkeydown=keys; document.onhelp=canxIt; var f = document.forms('screen')('F0'); if (f != null)f.focus();}</script>");
			buf.Append("<script language='javascript'>function tabcheck(field){var f = document.forms('screen')('F'+field); if (f.value.length == f.maxLength()){field++; if (document.forms('screen')('F'+field) != null){document.forms('screen')('F'+field).focus();}else{document.forms('screen')('F0').focus();}}}</script>\r\n");
			buf.Append("<body onload='loaded()' style='color: green; background: #3F3F3F'>\r\n");
			buf.Append("<style type='text/css' >");
			buf.Append(".normal { color: #");
			buf.Append(fgRGB);
			buf.Append("; background: #");
			buf.Append(bgRGB);
			buf.Append("; text-decoration: none}");
			buf.Append(".reverse { color: #");
			buf.Append(bgRGB);
			buf.Append("; background: #");
			buf.Append(fgRGB);
			buf.Append("; text-decoration: none}");
			buf.Append(".underline { color: #");
			buf.Append(fgRGB);
			buf.Append("; background: #");
			buf.Append(bgRGB);
			buf.Append("; text-decoration: underline} ");
			buf.Append(".reverseunderline { color: #");
			buf.Append(bgRGB);
			buf.Append("; background: #");
			buf.Append(fgRGB);
			buf.Append("; text-decoration: underline}");
			buf.Append(".blink { color: #");
			buf.Append(fgRGB);
			buf.Append("; background: #");
			buf.Append(bgRGB);
			buf.Append("; text-decoration: blink}");
			buf.Append(".blinkreverse { color: #");
			buf.Append(bgRGB);
			buf.Append("; background: #");
			buf.Append(fgRGB);
			buf.Append("; text-decoration: blink}");
			buf.Append("BODY {font-family:Fixedsys;}");
			buf.Append("</style>\r\n");
			
			// write the table header
			buf.Append("<form id='screen' method='post'><input type='hidden' id='hdnKey' value='' />\n");
			buf.Append("<table cellpadding='0' cellspacing='0' cols='80' width='100%' >");
			
			for (int r = 0; r < m_numRows; r++)
			{
				buf.Append("<tr>");
				
				for (int c = 0; c < m_numColumns; c++)
				{
					PageCell cell = m_displayPage.GetCell(c, r);
					int ch = cell.GetAttributes();

					if (! inUnprot)
					{
						buf.Append("<td class=");
						if ((ch & (int)VideoAttribs.MASK_COLOR) == 0)
						{
						}
						else
						{
						}
						if ((ch & ((int)VideoAttribs.VID_UNDERLINE | (int)VideoAttribs.VID_REVERSE)) == ((int)VideoAttribs.VID_UNDERLINE | (int)VideoAttribs.VID_REVERSE))
						{
							buf.Append("reverseunderline");
						}
						else if ((ch & ((int)VideoAttribs.VID_BLINKING | (int)VideoAttribs.VID_REVERSE)) == ((int)VideoAttribs.VID_BLINKING | (int)VideoAttribs.VID_REVERSE))
						{
							buf.Append("blinkreverse");
						}
						else if ((ch & (int)VideoAttribs.VID_BLINKING) != 0)
						{
							buf.Append("blink");
						}
						else if ((ch & (int)VideoAttribs.VID_REVERSE) != 0)
						{
							buf.Append("reverse");
						}
						else if ((ch & (int)VideoAttribs.VID_UNDERLINE) != 0)
						{
							buf.Append("underline");
						}
						else
						{
							buf.Append("normal");
						}
						if ((ch & (int)VideoAttribs.CHAR_START_FIELD) == 0)
						{
							buf.Append(">");
							buf.Append(cell.Get());
							buf.Append("</td>");
						}
					}
					if ((ch & (int)VideoAttribs.CHAR_START_FIELD) != 0)
					{
						if (inUnprot)
						{
							// end the input tag
							accum.Append("' maxlength='");
							accum.Append(accum.Length);
							accum.Append("' />");
							inUnprot = false;
							buf.Append(" colspan=");
							buf.Append(accum.Length);
							buf.Append(">");
							buf.Append(accum.ToString());
							buf.Append("</td>");
							accum.Length = 0;
						}
						// is the new field unprotected?
						int c2 = c + 1;
						if (c2 >= m_numColumns && r < 23)
						{
							c2 = 0;
							cell = m_displayPage.GetCell(c2, ++r);
						}
						if (cell.IsUnprotect())
						{
							inUnprot = true;
							accum.Append ("<input type='text' id='F");
							accum.Append (fieldCount);
							accum.Append ("' onkeypress='tabcheck(");
							accum.Append ( fieldCount );
							accum.Append (")' value='");
							accum.Append (cell.Get());
							fieldCount++;
						}
					}
					else if (inUnprot)
					{
						accum.Append((char)(ch & (int)VideoAttribs.MASK_CHAR));
					}
				}
				buf.Append("</tr>\r\n");
			}
			buf.Append("</table></form><p/><center><a href='mailto:johnga@dor.wa.gov'>Got Bugs?</a></center></body></html>");
		}

		public void GetSubString(int row, int col, int len, StringBuilder sb)
		{
			Debug.Assert(row < m_numRows && col+len < m_numColumns);

			for (int c = col; c < col+len; c++)
			{
				sb.Append( m_displayPage.GetCell(c, row).Get() );
			}
		}

		public void GetText(StringBuilder sb)
		{
			for ( int row = 0; row < GetNumRows(); row++ )
			{
				for ( int col = 0; col < GetNumColumns(); col++ )
				{
					sb.Append( m_displayPage.GetCell(col, row).Get() );
				}
			}
		}

		private void ReadBuffer(StringBuilder outp, int reqMask, int forbidMask, int startRow, int startCol, int endRow, int endCol)
		{
			m_writePage.ReadBuffer(outp, m_ppRemote, reqMask, forbidMask, startRow, startCol, endRow, endCol);
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
		private void CursorLeft()
		{
			m_writePage.CursorLeft(m_ppRemote);
			m_requiresRepaint = true;
		}

		private void WriteField(int c)
		{
			m_writePage.WriteField(c);
		}

		private void ClearAll()
		{
			for (int x = 0; x < m_numPages+1; x++)
			{
				if (m_pages[x] == null)
				{
					break;
				}
				m_pages[x].ClearPage();
				m_pages[x].m_bufferPos.Clear();
				m_pages[x].m_cursorPos.Clear();
				m_pages[x].SetWriteAttribute((int)VideoAttribs.VID_NORMAL);
				m_pages[x].SetVideoPriorCondition((int)VideoAttribs.VID_NORMAL);
			}
			m_requiresRepaint = true;
		}

		private int DecodeKeyAttrs(int attr)
		{
			int ret = 0;
			if (attr == 0)
			{
				return 0;
			}		
			Debug.Assert ( (attr & (1<<6)) != 0);
			if ((attr & (1<<0)) != 0)
			{
				ret |= (int)VideoAttribs.KEY_UPSHIFT;
			}
			if ((attr & (1<<1)) != 0)
			{
				ret |= (int)VideoAttribs.KEY_KB_ONLY;
			}
			if ((attr & (1<<2)) != 0)
			{
				ret |= (int)VideoAttribs.KEY_AID_ONLY;
			}
			if ((attr & (1<<3)) != 0)
			{
				ret |= (int)VideoAttribs.KEY_EITHER;
			}
			if (ret == 0 && (ret & ~(1<<6)) != 0) 
			{
				//System.out.println("Unknown video attr " + attr);
			}
			return ret;
		}
		
		private int DecodeDataAttrs(int attr)
		{
			int ret = 0;
			if (attr == 0)
			{
				return 0;
			}
			//ASSERT.fatal ( (attr & (1<<6)) != 0, "TextDisplay", 367, "Invalid attribute format");
			if ((attr & (1<<0)) != 0)
			{
				ret |= (int)VideoAttribs.DAT_MDT;
			}
			if ((attr & (1<<4)) != 0)
			{
				ret |= (int)VideoAttribs.DAT_AUTOTAB;
			}
			if ((attr & (1<<5)) != 0)
			{
				ret |= (int)VideoAttribs.DAT_UNPROTECT;
			}
			int type = attr & ((1<<1)|(1<<2)|(1<<3));
			ret |= type << (int)VideoAttribs.SHIFT_DAT_TYPE;
			Debug.Assert( (attr & ~((1<<6)|(1<<5)|(1<<4)|(1<<0)|(1<<1)|(1<<2)|(1<<3))) == 0);
			return ret;
		}
		
		private int DecodeVideoAttrs(int attr)
		{
			int ret = 0;
			if (attr == 0 || attr == 32)
			{
				return 0;
			}
			//ASSERT.fatal ( (attr & (1<<5)) != 0, "TextDisplay", 367, "Invalid attribute format");
			
			if ((attr & (1<<0)) != 0)
			{
				ret |= (int)VideoAttribs.VID_NORMAL;
			}
			if ((attr & (1<<1)) != 0)
			{
				ret |= (int)VideoAttribs.VID_BLINKING;
			}
			if ((attr & (1<<2)) != 0)
			{
				ret |= (int)VideoAttribs.VID_REVERSE;
			}
			if ((attr & (1<<3)) != 0)
			{
				ret |= (int)VideoAttribs.VID_INVIS;
			}
			if ((attr & (1<<4)) != 0)
			{
				ret |= (int)VideoAttribs.VID_UNDERLINE;
			}
			if ( (attr & ~((1<<5)|(1<<0)|(1<<1)|(1<<2)|(1<<3)|(1<<4))) != 0) 
			{
				//System.out.println("Unknown video attr " + attr);
			}
			return ret;
		}	
	};
}
