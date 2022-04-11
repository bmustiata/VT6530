using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using Maximum;
using Maximum.AnsiTerm;
using Maximum.Collections;

namespace LibVt6530
{
	public struct CellAttributes
	{
		public int m_vidNormal;
		public int m_vidBlinking;
		public int m_vidReverse;
		public int m_vidInvis;
		public int m_vidUnderline;

		public int m_datMdt;
		public int m_datDataType;// : 3;
		public int m_datAutotab;
		public int m_datUnprotect;

		public int m_keyUpshift;
		public int m_keyKbOnly;
		public int m_keyAidOnly;
		public int m_keyEither;

		public volatile int m_charStartField;
		public int m_charDirtyCell;
		public int m_charColor;// : 3;
	};

	public class PageCell
	{
		protected int m_row, m_col;
		protected volatile char m_ch;
		protected CellAttributes m_attribs;

		public PageCell(int row, int col)
		{
			m_row = row;
			m_col = col;
		}

		public int Row
		{
			get { return m_row; }
		}

		public int Col
		{
			get { return m_col; }
		}

		public char Get( )
		{
			return m_ch;
		}

		public void Set( char ch )
		{
			Debug.Assert(((int)ch) < 256);

			m_ch = ch;
			m_attribs.m_charDirtyCell = (int)VideoAttribs.CHAR_CELL_DIRTY;
		}

		public void Set (PageCell cell)
		{
			m_ch = cell.m_ch;
			m_attribs = cell.m_attribs;
			m_attribs.m_charDirtyCell = (int)VideoAttribs.CHAR_CELL_DIRTY;
		}

		public void Set( char ch, int attribs )
		{
			Set(ch);
			SetAttributes(attribs);
		}

		public void SetAttributes( int attribs )
		{
			/*m_attribs.m_vidNormal = (attribs & VideoAttribs.VID_NORMAL) != 0;
			m_attribs.m_vidBlinking = (attribs & VideoAttribs.VID_BLINKING) != 0;
			m_attribs.m_vidReverse = (attribs & VideoAttribs.VID_REVERSE) != 0;
			m_attribs.m_vidInvis = (attribs & VideoAttribs.VID_INVIS) != 0;
			m_attribs.m_vidUnderline = (attribs & VideoAttribs.VID_UNDERLINE) != 0;
			m_attribs.m_datMdt = (attribs & VideoAttribs.DAT_MDT) != 0;
			m_attribs.m_datAutotab = (attribs & VideoAttribs.DAT_AUTOTAB) != 0;
			m_attribs.m_datUnprotect = (attribs & VideoAttribs.DAT_UNPROTECT) != 0;
			m_attribs.m_keyUpshift = (attribs & VideoAttribs.KEY_UPSHIFT) != 0;
			m_attribs.m_keyKbOnly = (attribs & VideoAttribs.KEY_KB_ONLY) != 0;
			m_attribs.m_charStartField = (attribs & VideoAttribs.CHAR_START_FIELD) != 0;
			m_attribs.m_charDirtyCell = (attribs & VideoAttribs.CHAR_CELL_DIRTY) != 0;
			m_attribs.m_charColor = (attribs & VideoAttribs.MASK_COLOR) >> VideoAttribs.SHIFT_COLOR;
			m_attribs.m_datDataType = (attribs & VideoAttribs.DAT_TYPE) >> VideoAttribs.SHIFT_DAT_TYPE;*/

			m_attribs.m_vidNormal = attribs & (int)VideoAttribs.VID_NORMAL;
			m_attribs.m_vidBlinking = attribs & (int)VideoAttribs.VID_BLINKING;
			m_attribs.m_vidReverse = attribs & (int)VideoAttribs.VID_REVERSE;
			m_attribs.m_vidInvis = attribs & (int)VideoAttribs.VID_INVIS;
			m_attribs.m_vidUnderline = attribs & (int)VideoAttribs.VID_UNDERLINE;
			m_attribs.m_datMdt = attribs & (int)VideoAttribs.DAT_MDT;
			m_attribs.m_datAutotab = attribs & (int)VideoAttribs.DAT_AUTOTAB;
			m_attribs.m_datUnprotect = attribs & (int)VideoAttribs.DAT_UNPROTECT;
			m_attribs.m_keyUpshift = attribs & (int)VideoAttribs.KEY_UPSHIFT;
			m_attribs.m_keyKbOnly = attribs & (int)VideoAttribs.KEY_KB_ONLY;
			m_attribs.m_charStartField = attribs & (int)VideoAttribs.CHAR_START_FIELD;
			m_attribs.m_charDirtyCell = attribs & (int)VideoAttribs.CHAR_CELL_DIRTY;
			m_attribs.m_charColor = attribs & (int)VideoAttribs.MASK_COLOR;
			m_attribs.m_datDataType = attribs & (int)VideoAttribs.DAT_TYPE;
		}

		public int GetAttributes()
		{
			return m_attribs.m_vidNormal |
				m_attribs.m_vidBlinking |
				m_attribs.m_vidReverse |
				m_attribs.m_vidInvis |
				m_attribs.m_vidUnderline |
				m_attribs.m_datMdt |
				m_attribs.m_datAutotab |
				m_attribs.m_datUnprotect | 
				m_attribs.m_keyUpshift |
				m_attribs.m_keyKbOnly |
				m_attribs.m_charStartField |
				m_attribs.m_charDirtyCell |
				m_attribs.m_charColor |
				m_attribs.m_datDataType; 
		}

		public int AsInt()
		{
			return GetAttributes() | (int)m_ch;
		}

		public void Clear()
		{
			m_ch = ' ';
			SetAttributes((int)VideoAttribs.VID_NORMAL);
		}

		public void Clear( int attributes )
		{
			m_ch = ' ';
			SetAttributes(attributes);
		}

		public void ClearVideoAttribs()
		{
			m_attribs.m_vidNormal = (int)VideoAttribs.VID_NORMAL;
			m_attribs.m_vidBlinking = 0;
			m_attribs.m_vidReverse = 0;
			m_attribs.m_vidInvis = 0;
			m_attribs.m_vidUnderline = 0;
			m_attribs.m_charDirtyCell = (int)VideoAttribs.CHAR_CELL_DIRTY;
		}

		/* ClearTo doesn't set the dirty flag */
		public void ClearTo( char ch )
		{
			m_ch = ch;
			SetAttributes((int)VideoAttribs.VID_NORMAL);
		}

		public void SetDirty(bool val) { m_attribs.m_charDirtyCell = ((val)?(int)VideoAttribs.CHAR_CELL_DIRTY:0); }
		public bool IsDirty() { return m_attribs.m_charDirtyCell != 0; }

		public void SetNormal(bool val) { m_attribs.m_vidNormal = ((val)?(int)VideoAttribs.VID_NORMAL : 0); }
		public bool IsNormal() { return m_attribs.m_vidNormal != 0; }

		public void SetBlinking(bool val) { m_attribs.m_vidBlinking = ((val)?(int)VideoAttribs.VID_BLINKING : 0); }
		public bool IsBlinking() { return m_attribs.m_vidBlinking != 0; }

		public void SetReverse(bool val) { m_attribs.m_vidReverse = ((val)?(int)VideoAttribs.VID_REVERSE : 0); }
		public bool IsReverse() { return m_attribs.m_vidReverse != 0; }

		public void SetInvis(bool val) { m_attribs.m_vidInvis = ((val)?(int)VideoAttribs.VID_INVIS : 0); }
		public bool IsInvis() { return m_attribs.m_vidInvis != 0; }

		public void SetUnderline(bool val) { m_attribs.m_vidUnderline = ((val)?(int)VideoAttribs.VID_UNDERLINE : 0); }
		public bool IsUnderline() { return m_attribs.m_vidUnderline != 0; }

		public void SetUnprotect(bool val) { m_attribs.m_datUnprotect = ((val)?(int)VideoAttribs.DAT_UNPROTECT : 0); }
		public bool IsUnprotect() { return m_attribs.m_datUnprotect != 0; }

		public void SetStartField(bool val) { m_attribs.m_charStartField = ((val)?(int)VideoAttribs.CHAR_START_FIELD : 0); }
		public bool IsStartField() { return m_attribs.m_charStartField != 0; }

		public void SetMDT(bool val) { m_attribs.m_datMdt = ((val)?(int)VideoAttribs.DAT_MDT : 0); }
		public bool IsMDT() { return m_attribs.m_datMdt != 0; }

		public void SetKeyUpshift(bool val) { m_attribs.m_keyUpshift = ((val)?(int)VideoAttribs.KEY_UPSHIFT : 0); }
		public bool IsKeyUpshift() { return m_attribs.m_keyUpshift != 0; }
	};

	/*
	 *  In protect mode, there's 4 pages (or buffers) to send
	 *  commands to.
	 */
	public class Page
	{	
		protected char[] m_chbuf = new char[2];
		
		protected int m_numRows, m_numColumns;

		protected int m_writeAttr;		// These attributes can be set and then effect
		protected int m_priorAttr;		// all subsequent writes.
		
		protected int m_insertMode;
		protected bool m_cursorBlock;

		protected PageCell[] m_cells;
		protected Vector<PageCell> m_fields = new Vector<PageCell>();
		protected Vector<PageCell> m_unprotectFields = new Vector<PageCell>();

		public Cursor m_cursorPos;
		public Cursor m_bufferPos;

		public Page(int numRows, int numCols)
		{
			m_cursorPos = new Cursor(numRows, numCols);
			m_bufferPos = new Cursor(numRows, numCols);

			Debug.Assert(numRows > 0 && numCols > 0);
			m_numColumns = numCols;
			m_numRows = numRows;

			m_cells = new PageCell[m_numRows * m_numColumns];
			for ( int x = 0; x < m_numRows * m_numColumns; x++ )
			{
				m_cells[x] = new PageCell(x / m_numColumns, x % m_numColumns);
			}
			m_chbuf[1] = '\0';

			m_writeAttr = (int)VideoAttribs.VID_NORMAL;
			m_priorAttr = (int)VideoAttribs.VID_NORMAL;
			m_insertMode = (int)VideoAttribs.INSERT_INSERT;
			
			m_cursorBlock = true;

			Init();
		}

		public void Init()
		{
			m_writeAttr = (int)VideoAttribs.VID_NORMAL;
			m_priorAttr = (int)VideoAttribs.VID_NORMAL;
			m_cursorPos.Clear();
			m_bufferPos.Clear();
			m_fields.Clear();
			m_unprotectFields.Clear();

			for (int x = 0; x < m_numRows; x++)
			{
				for (int q = 0; q < m_numColumns; q++)
				{
					PageCell cell = GetCell(q, x);
					cell.ClearTo(' ');// = (int)' ' | CHAR_CELL_DIRTY | m_writeAttr;
					cell.SetDirty(true);
					cell.SetNormal(true);
				}
			}
		}

		public int GetNumRows()
		{
			return m_numRows;
		}

		public int GetNumColumns()
		{
			return m_numColumns;
		}

		public PageCell GetCell(int x, int y)
		{
			Debug.Assert( x < m_numColumns && y < m_numRows );
			return m_cells[y * m_numColumns + x];
		}

		public PageCell GetCell(Cursor cursor)
		{
			return GetCell(cursor.m_column, cursor.m_row);
		}
		
		public void SetVideoPriorCondition(int attr)
		{
			m_priorAttr = attr;
		}
		
		public void SetWriteAttribute(int attr)
		{
			m_writeAttr = attr;
		}
		
		public int GetWriteAttr()
		{
			return m_writeAttr;
		}

		public int GetPriorAttr()
		{
			return m_priorAttr;
		}
		
		public void SetInsertMode(int mode)
		{
			m_insertMode = mode;
		}
		
		public void ClearToEOP(PageProtocol mode)
		{
			mode.ClearToEOP(this);
		}
		
		public void ClearToEOL(PageProtocol mode)
		{
			mode.ClearToEOL(this);
		}
		
		public void ClearBlock(PageProtocol mode, int startRow, int startCol, int endRow, int endCol)
		{
			mode.ClearBlock(this, startRow, startCol, endRow, endCol);
		}
				
		public void ReadBuffer(StringBuilder outp, PageProtocol mode, int reqMask, int forbidMask, int startRow, int startCol, int endRow, int endCol)
		{
			mode.ReadBuffer(outp, this, reqMask, forbidMask, startRow, startCol, endRow, endCol);
		}
		
		public int GetFieldCount() { return m_fields.Count; }
		public int GetUnprotectFieldCount() { return m_unprotectFields.Count; }
		public PageCell GetFieldStart(int x) { return (x >= m_fields.Count) ? null : m_fields.ElementAt(x); }
		public PageCell GetUnprotectFieldStart(int x) { return (x >= m_unprotectFields.Count) ? null : m_unprotectFields.ElementAt(x); }

		/*inline void setColors(PaintSurface *ps, int ch, COLORREF fgcolor, COLORREF bgcolor, COLORREF fgbright, HBRUSH foreground, HBRUSH background, HPEN pen, HPEN revpen, int r, int c, int charWidth, int charHeight)
		{
			if ( ((ch & VID_REVERSE) != 0) && ((ch & VID_BLINKING) != 0) ) 
			{ 
				//HBRUSH color = bg; 
				//bg = fg;
				//fg = color;
				ps.setBkColor(fgbright);
				ps.setTextColor(bgcolor);
				ps.setPen(revpen);
				ps.fillRect(c * charWidth, r * charHeight, charWidth, charHeight, foreground);
			}
			else if ((ch & VID_REVERSE) != 0)
			{
				ps.setBkColor(fgcolor);
				ps.setTextColor(bgcolor);
				ps.setPen(revpen);
				ps.fillRect(c * charWidth, r * charHeight, charWidth, charHeight, foreground);
			}
			else if ((ch & VID_BLINKING) != 0)
			{
				ps.setBkColor(bgcolor);
				ps.setTextColor(fgbright);	
				ps.setPen(pen);
				ps.fillRect(c * charWidth, r * charHeight, charWidth, charHeight, background);
			}
			else
			{
				ps.setBkColor(bgcolor);
				ps.setTextColor(fgcolor);
				ps.fillRect(c * charWidth, r * charHeight, charWidth, charHeight, background);
			}
		}*/

		public void InsertChar(PageProtocol mode)
		{
			mode.InsertChar(this);
			Debug.Assert(m_bufferPos.m_row < m_numRows);
			Debug.Assert(m_bufferPos.m_column < m_numColumns);
			Debug.Assert(m_cursorPos.m_row < m_numRows);
			Debug.Assert(m_cursorPos.m_column < m_numColumns);
		}

		public void DeleteChar(PageProtocol mode)
		{
			mode.DeleteChar(this);
			Debug.Assert(m_bufferPos.m_row < m_numRows);
			Debug.Assert(m_bufferPos.m_column < m_numColumns);
			Debug.Assert(m_cursorPos.m_row < m_numRows);
			Debug.Assert(m_cursorPos.m_column < m_numColumns);
		}

		public void Tab(PageProtocol mode, int inc)
		{
			mode.Tab(this, inc);
			Debug.Assert(m_bufferPos.m_row < m_numRows);
			Debug.Assert(m_bufferPos.m_column < m_numColumns);
			Debug.Assert(m_cursorPos.m_row < m_numRows);
			Debug.Assert(m_cursorPos.m_column < m_numColumns);
		}

		public void Backspace(PageProtocol mode)
		{
			mode.Backspace(this);
			Debug.Assert(m_bufferPos.m_row < m_numRows);
			Debug.Assert(m_bufferPos.m_column < m_numColumns);
			Debug.Assert(m_cursorPos.m_row < m_numRows);
			Debug.Assert(m_cursorPos.m_column < m_numColumns);
		}

		public void CursorUp(PageProtocol mode)
		{
			mode.CursorUp(this);
			Debug.Assert(m_bufferPos.m_row < m_numRows);
			Debug.Assert(m_bufferPos.m_column < m_numColumns);
			Debug.Assert(m_cursorPos.m_row < m_numRows);
			Debug.Assert(m_cursorPos.m_column < m_numColumns);
		}

		public void CursorDown(PageProtocol mode)
		{
			mode.CursorDown(this);
			Debug.Assert(m_bufferPos.m_row < m_numRows);
			Debug.Assert(m_bufferPos.m_column < m_numColumns);
			Debug.Assert(m_cursorPos.m_row < m_numRows);
			Debug.Assert(m_cursorPos.m_column < m_numColumns);
		}

		public void CursorLeft(PageProtocol mode)
		{
			mode.CursorLeft(this);
			Debug.Assert(m_bufferPos.m_row < m_numRows);
			Debug.Assert(m_bufferPos.m_column < m_numColumns);
			Debug.Assert(m_cursorPos.m_row < m_numRows);
			Debug.Assert(m_cursorPos.m_column < m_numColumns);
		}

		public void CursorRight(PageProtocol mode)
		{
			mode.CursorRight(this);
			Debug.Assert(m_bufferPos.m_row < m_numRows);
			Debug.Assert(m_bufferPos.m_column < m_numColumns);
			Debug.Assert(m_cursorPos.m_row < m_numRows);
			Debug.Assert(m_cursorPos.m_column < m_numColumns);
		}

		public void ClearPage()
		{
			Debug.Assert(m_bufferPos.m_row < m_numRows);
			Debug.Assert(m_bufferPos.m_column < m_numColumns);
			Debug.Assert(m_cursorPos.m_row < m_numRows);
			Debug.Assert(m_cursorPos.m_column < m_numColumns);
			m_fields.Clear();
			m_unprotectFields.Clear();

			for (int r = 0; r < m_numRows * m_numColumns; r++)
			{
				m_cells[r].Clear();
				m_cells[r].SetAttributes(m_writeAttr | m_priorAttr);
			}
		}

		public void WriteBuffer(PageProtocol mode, byte[] text)
		{
			mode.WriteBuffer(this, text);
			Debug.Assert(m_bufferPos.m_row < m_numRows);
			Debug.Assert(m_bufferPos.m_column < m_numColumns);
			Debug.Assert(m_cursorPos.m_row < m_numRows);
			Debug.Assert(m_cursorPos.m_column < m_numColumns);
		}

		public void WriteCursor(PageProtocol mode, string text)
		{
			mode.WriteCursor(this, text);
			Debug.Assert(m_bufferPos.m_row < m_numRows);
			Debug.Assert(m_bufferPos.m_column < m_numColumns);
			Debug.Assert(m_cursorPos.m_row < m_numRows);
			Debug.Assert(m_cursorPos.m_column < m_numColumns);
		}

		public void WriteCursor(PageProtocol mode, byte[] text)
		{
			mode.WriteCursor(this, text);
			Debug.Assert(m_bufferPos.m_row < m_numRows);
			Debug.Assert(m_bufferPos.m_column < m_numColumns);
			Debug.Assert(m_cursorPos.m_row < m_numRows);
			Debug.Assert(m_cursorPos.m_column < m_numColumns);
		}

		public void WriteCursorLocal(PageProtocol mode, byte[] text)
		{
			for (int x = 0; x < text.Length; x++)
			{
				mode.WriteChar(this, m_cursorPos, text[x] | (int)VideoAttribs.DAT_MDT);
			}
		}

		public void CarageReturn(PageProtocol mode)
		{
			mode.CarageReturn(this);
			Debug.Assert(m_bufferPos.m_row < m_numRows);
			Debug.Assert(m_bufferPos.m_column < m_numColumns);
			Debug.Assert(m_cursorPos.m_row < m_numRows);
			Debug.Assert(m_cursorPos.m_column < m_numColumns);
		}

		public void SetCursor(PageProtocol mode, int row, int col)
		{
			mode.SetCursor(this, row, col);
			Debug.Assert(m_bufferPos.m_row < m_numRows);
			Debug.Assert(m_bufferPos.m_column < m_numColumns);
			Debug.Assert(m_cursorPos.m_row < m_numRows);
			Debug.Assert(m_cursorPos.m_column < m_numColumns);
		}

		public void SetBuffer(int row, int col)
		{
			m_bufferPos.m_row = row;
			m_bufferPos.m_column = col;
			Debug.Assert(m_bufferPos.m_row < m_numRows);
			Debug.Assert(m_bufferPos.m_column < m_numColumns);
			Debug.Assert(m_cursorPos.m_row < m_numRows);
			Debug.Assert(m_cursorPos.m_column < m_numColumns);
		}

		public void WriteField(int c)
		{
			// mark the field
			PageCell cell = GetCell(m_bufferPos);
			m_fields.Add(cell);

			cell.Set((char)(c & (int)VideoAttribs.MASK_CHAR));
			cell.SetAttributes((c & (int)VideoAttribs.MASK_FIELD) | (int)VideoAttribs.CHAR_START_FIELD | (int)VideoAttribs.CHAR_CELL_DIRTY | m_writeAttr | m_priorAttr);
			if ( cell.IsUnprotect() )
			{
				// wrong.  the start cells are not unprotect, just the cells between the starts.
				m_unprotectFields.Add( cell );
			}
			// The field start char is not updatable.  For unprotect fields, the char to the
			// right is the first updatable char.
			cell.SetUnprotect(false);

			m_bufferPos.m_column++;
			m_bufferPos.AdjustCol();
								
			// detect any previous field on the line and extend
			// its attributes upto this one
			for (int col = m_bufferPos.m_column; col < m_numColumns; col++)
			{
				PageCell cellp = GetCell(col, m_bufferPos.m_row);
				if ( cellp.IsStartField() )
				{
					return;
				}
				cellp.SetAttributes((c & (int)VideoAttribs.MASK_FIELD) | (int)VideoAttribs.CHAR_CELL_DIRTY | m_writeAttr | m_priorAttr);
				Debug.Assert( !cellp.IsStartField() );
			}
			for (int y = m_bufferPos.m_row + 1; y < m_numRows; y++)
			{
				for (int x = 0; x < m_numColumns; x++)
				{
					cell = GetCell(x, y);
					if (cell.IsStartField())
					{
						return;
					}
					cell.SetAttributes((c & (int)VideoAttribs.MASK_FIELD) | (int)VideoAttribs.CHAR_CELL_DIRTY | m_writeAttr | m_priorAttr);
					Debug.Assert( !cell.IsStartField() );
				}
			}
		}

		public void ResetMDTs()
		{
			for (int r = 0; r < m_numRows * m_numColumns; r++)
			{
				m_cells[r].SetMDT(false);
			}
		}

		public void RescanFields()
		{
			m_fields.Clear();
			m_unprotectFields.Clear();
			for (int y = 0; y < m_numRows; y++)
			{
				for (int x = 0; x < m_numColumns; x++)
				{
					PageCell cell = GetCell(x, y);
					if (cell.IsStartField())
					{
						m_fields.Add(cell);
						if (x == m_numColumns - 1)
						{
							if (y < m_numRows - 1)
							{
								if (GetCell(0, y + 1).IsUnprotect())
								{
									m_unprotectFields.Add(cell);
								}
							}
						}
						else
						{
							if (GetCell(x + 1, y).IsUnprotect())
							{
								m_unprotectFields.Add(cell);
							}
						}
					}
				}
			}
		}

		public bool GetFieldXY(int field, out int x, out int y)
		{
			x = 0; y = 0;
			if (field >= m_fields.Count)
			{
				return false;
			}
			PageCell cell = m_fields.ElementAt(field);
			x = cell.Col;
			y = cell.Row;
			return true;
			//x = 0;
			//for (y = 0; y < m_numRows; y++)
			//{
			//    for (x = 0; x < m_numColumns; x++)
			//    {
			//        if (GetCell(x, y) == cell)
			//        {
			//            return true;
			//        }
			//    }
			//}
			//return false;
		}

		public string GetFieldASCII(PageCell cell)
		{
			StringBuilder buf = new StringBuilder();
			bool isUnProtect;
			GetFieldASCII(cell, buf, out isUnProtect);
			return buf.ToString();
		}

		public bool ValidField(int field)
		{
			int x; int y;
			return GetFieldXY(field, out x, out y);
		}

		public void GetFieldASCII(PageCell cell, StringBuilder buf, out bool isUnProtect)
		{
			isUnProtect = false;
			if (null == cell)
			{
				return;
			}
			Debug.Assert(cell.IsStartField());
			int row = cell.Row;
			int col = cell.Col;
			//bool found = false;
			//for (row = 0; row < m_numRows; row++)
			//{
			//    for (col = 0; col < m_numColumns; col++)
			//    {
			//        if (GetCell(col, row) == cell)
			//        {
			//            found = true;
			//            break;
			//        }
			//    }
			//    if (found)
			//    {
			//        break;
			//    }
			//}
			//if (!found)
			//{
			//    return;
			//}
			col++;
			if (col >= m_numColumns)
			{
				// go to the next row
				col = 0;
				row++;
			}
			if (row >= m_numRows || col >= m_numColumns)
			{
				return;
			}
			while (col < m_numColumns)
			{
				PageCell cell2 = GetCell(col, row);
				if (cell2.IsStartField())
				{
					break;
				}
				if (cell2.IsUnprotect())
				{
					isUnProtect = true;
				}
				buf.Append(cell2.Get());
				col++;
			}
		}

		public void GetStartFieldASCII(StringBuilder buf)
		{
			for (int r = 0; r < m_numRows; r++)
			{
				for (int c = 0; c < m_numColumns; c++)
				{
					int c2 = c+1;
					int r2 = r;
					if (c2 >= m_numColumns)
					{
						c2 = 0;
						r2++;
						if (r2 >= m_numRows)
						{
							r2 = 0;
						}
					}
					if (GetCell(c, r).IsStartField() && GetCell(c2, r2).IsUnprotect())
					{
						buf.Append((char)(r + 0x20));
						buf.Append((char)(c + 0x21));
						return;
					}
				}
			}
			buf.Append("  ");
		}

		public void ForceDirty()
		{
			for (int r = 0; r < m_numRows * m_numColumns; r++)
			{
				m_cells[r].SetDirty(true);
			}
		}

		public void ScrollPageUp()
		{
			for (int r = 0; r < m_numRows -1; r++)
			{
				for (int c = 0; c < m_numColumns; c++)
				{
					GetCell(c, r).Set(GetCell(c, r+1));
				}
			}
			for (int c = 0; c < m_numColumns; c++)
			{
				GetCell(c, m_numRows-1).Set(' ');
				GetCell(c, m_numRows-1).SetAttributes(m_writeAttr | m_priorAttr);
			}
			Debug.Assert(m_cursorPos.m_row < m_numRows);
			Debug.Assert(m_cursorPos.m_column < m_numColumns);
		}

		public int ScanForNextField(int c, int r, int inc)
		{
			Debug.Assert(r < m_numRows);
			Debug.Assert(c < m_numColumns);

			if (inc > 0)
			{
				for (int x = c; x < m_numColumns; x++)
				{
					if (GetCell(x, r).IsStartField())
					{
						Debug.Assert(!GetCell(x, r).IsUnprotect());
						return x;
					}
				}
			}
			else
			{
				if (c == 0)
				{
					c = m_numColumns-1;
					if (r == 0)
					{
						r = m_numRows-1;
					}
					else
					{
						r--;
					}
				}
				for (int x = c; x >= 0; x--)
				{
					if (GetCell(x, r).IsStartField())
					{
						Debug.Assert(!GetCell(x, r).IsUnprotect());
						return x;
					}
				}
			}
			return -1;
		}

		public int ScanForUnprotectField(int c, int r, int inc)
		{
			Debug.Assert(r < m_numRows);
			Debug.Assert(c < m_numColumns);

			if (inc > 0)
			{
				for (int x = c; x < m_numColumns; x++)
				{
					if (GetCell(x, r).IsUnprotect())
					{
						Debug.Assert(!GetCell(x, r).IsStartField());
						return x;
					}
				}
			}
			else
			{
				if (c == 0)
				{
					c = m_numColumns-1;
					if (r == 0)
					{
						r = m_numRows-1;
					}
					else
					{
						r--;
					}
				}
				for (int x = c; x >= 0; x--)
				{
					if (GetCell(x, r).IsUnprotect())
					{
						Debug.Assert(!GetCell(x, r).IsStartField());
						return x;
					}
				}			
			}
			return -1;
		}
	};
}
