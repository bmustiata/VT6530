using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace LibVt6530
{
	public class SharedProtocol : PageProtocol
	{
		public virtual void WriteBuffer(Page page, byte[] text)
		{
			Write(page.m_bufferPos, page, page.GetWriteAttr() | page.GetPriorAttr(), text);
		}

		public virtual void WriteBuffer(Page page, string text)
		{
			Write(page.m_bufferPos, page, page.GetWriteAttr() | page.GetPriorAttr(), text);
		}

		public virtual void WriteCursor(Page page, byte[] text)
		{
			Write(page.m_cursorPos, page, page.GetWriteAttr() | page.GetPriorAttr(), text);
		}

		public virtual void WriteCursor(Page page, string text)
		{
			Write(page.m_cursorPos, page, page.GetWriteAttr() | page.GetPriorAttr(), text);
		}

		public virtual void WriteChar(Page page, Cursor cursor, int c)
		{
			Debug.Assert(cursor.m_row < page.GetNumRows());
			Debug.Assert(cursor.m_column < page.GetNumColumns());

			switch ((char)(c & (int)VideoAttribs.MASK_CHAR))
			{
				case '\t':
					Tab(page, 1);
					break;
				case '\r':
					Linefeed(page);
					break;
				case '\n':
					CarageReturn(page);
					break;;
				case '\b':
					Backspace(page);
					break;
				case (char)11:
					CursorUp(page);
					break;
				case (char)ExtKeys.SPC_DEL:
					DeleteChar(page);
					break;
				case (char)ExtKeys.SPC_DOWN:
					ArrowDown(page, cursor);
					break;
				case (char)ExtKeys.SPC_END:
					End(page);
					break;
				case (char)ExtKeys.SPC_HOME:
					Home(page);
					break;
				case (char)ExtKeys.SPC_INS:
					InsertChar(page);
					break;
				case (char)ExtKeys.SPC_LEFT:
					ArrowLeft(page, cursor);
					break;
				case (char)ExtKeys.SPC_PGDN:
					break;
				case (char)ExtKeys.SPC_PGUP:
					break;
				case (char)ExtKeys.SPC_PRINTSCR:
					break;
				case (char)ExtKeys.SPC_RIGHT:
					ArrowRight(page, cursor);
					break;
				case (char)ExtKeys.SPC_UP:
					ArrowUp(page, cursor);
					break;
				default:
					if (page.GetCell(cursor).IsKeyUpshift())
					{
						page.GetCell(cursor).Set(Char.ToUpper((char)c), page.GetWriteAttr() | page.GetPriorAttr() | page.GetCell(cursor).GetAttributes());
					}
					else
					{
						page.GetCell(cursor).Set((char)(c & (int)VideoAttribs.MASK_CHAR), (c & ~(int)VideoAttribs.MASK_CHAR) | page.GetCell(cursor).GetAttributes() | (int)VideoAttribs.CHAR_CELL_DIRTY | page.GetPriorAttr());
					}
					cursor.m_column++;
					cursor.AdjustCol();
					page.GetCell(cursor).SetDirty(true);
					break;
			}
			ValidateCursorPos(page);
		}
		
		public virtual void ArrowDown(Page page, Cursor cursor)
		{
			page.GetCell(cursor).SetDirty(true);
			cursor.m_row++;
			cursor.AdjustRow();
			page.GetCell(cursor).SetDirty(true);
		}
		
		public virtual void ArrowUp(Page page, Cursor cursor)
		{
			page.GetCell(cursor).SetDirty(true);
			cursor.m_row--;
			cursor.AdjustRow();
			page.GetCell(cursor).SetDirty(true);
		}

		public virtual void ArrowLeft(Page page, Cursor cursor)
		{
			page.GetCell(cursor).SetDirty(true);
			cursor.m_column--;
			cursor.AdjustCol();
			page.GetCell(cursor).SetDirty(true);
		}
		
		public virtual void ArrowRight(Page page, Cursor cursor)
		{
			page.GetCell(cursor).SetDirty(true);
			cursor.m_column++;
			cursor.AdjustCol();
			page.GetCell(cursor).SetDirty(true);
		}

		public virtual void Home(Page page)
		{
		}

		public virtual void End(Page page)
		{
		}

		public virtual void ValidateCursorPos(Page page)
		{
		}
		
		public virtual void InsertChar(Page page)
		{
		}
		
		public virtual void DeleteChar(Page page)
		{
		}
		
		public void Backspace(Page page)
		{
			page.GetCell(page.m_cursorPos).SetDirty(true);
			page.m_cursorPos.m_column--;
			page.m_cursorPos.AdjustCol();
			page.GetCell(page.m_cursorPos).Set(' ');

			Debug.Assert(page.m_cursorPos.m_row < page.GetNumRows());
			Debug.Assert(page.m_cursorPos.m_column < page.GetNumColumns());
		}
		
		public virtual void Tab(Page page, int inc)
		{
		}
		
		public virtual void CarageReturn(Page page)
		{
			page.GetCell(page.m_cursorPos).SetDirty(true);
			page.m_cursorPos.m_column = 0;
			page.GetCell(page.m_cursorPos).SetDirty(true);

			Debug.Assert(page.m_cursorPos.m_row < page.GetNumRows());
			Debug.Assert(page.m_cursorPos.m_column < page.GetNumColumns());
		}
		
		public virtual void Linefeed(Page page)
		{
			page.GetCell(page.m_cursorPos).SetDirty(true);
			
			if (page.m_cursorPos.m_row == page.GetNumRows()-1)
			{
				page.ScrollPageUp();
			}
			else
			{
				page.m_cursorPos.m_row++;
			}
			page.GetCell(page.m_cursorPos).SetDirty(true);
			
			Debug.Assert(page.m_cursorPos.m_row < page.GetNumRows());
			Debug.Assert(page.m_cursorPos.m_column < page.GetNumColumns());
		}
		
		public virtual void CursorRight(Page page)
		{
			page.GetCell(page.m_bufferPos).SetDirty(true);
			if (++page.m_cursorPos.m_column >= page.GetNumColumns())
			{
				page.m_cursorPos.m_column = 0;
				if (page.m_cursorPos.m_row == page.GetNumRows()-1)
				{
					page.ScrollPageUp();
				}
				else
				{
					page.m_cursorPos.m_row++;
					page.m_cursorPos.AdjustRow();
				}
			}
			Debug.Assert(page.m_cursorPos.m_row < page.GetNumRows());
			Debug.Assert(page.m_cursorPos.m_column < page.GetNumColumns());
		}

		public virtual void CursorLeft(Page page)
		{
			page.m_cursorPos.m_column--;
			if (page.m_cursorPos.m_column < 0)
			{
				page.m_cursorPos.m_column = 0;
				if (page.m_cursorPos.m_row == 0)
				{
					page.m_cursorPos.m_row = page.GetNumRows() -1;
				}
				else
				{
					page.m_cursorPos.m_row--;
				}
			}
			Debug.Assert(page.m_cursorPos.m_row < page.GetNumRows());
			Debug.Assert(page.m_cursorPos.m_column < page.GetNumColumns());
		}
			
		public virtual void CursorUp(Page page)
		{
			page.GetCell(page.m_cursorPos).SetDirty(true);
			page.m_cursorPos.m_row++;
			page.m_cursorPos.AdjustRow();
			page.GetCell(page.m_cursorPos).SetDirty(true);

			Debug.Assert(page.m_cursorPos.m_row < page.GetNumRows());
			Debug.Assert(page.m_cursorPos.m_column < page.GetNumColumns());
		}
		
		public virtual void CursorDown(Page page)
		{
			page.GetCell(page.m_cursorPos).SetDirty(true);
			if (page.m_cursorPos.m_row == page.GetNumRows()-1)
			{
				page.ScrollPageUp();
			}
			else
			{
				page.m_cursorPos.m_row++;
				page.GetCell(page.m_cursorPos).SetDirty(true);
			}
			Debug.Assert(page.m_cursorPos.m_row < page.GetNumRows());
			Debug.Assert(page.m_cursorPos.m_column < page.GetNumColumns());
		}
		
		public virtual void ClearToEOL(Page page)
		{
		}
		
		public virtual void ClearBlock(Page page, int startRow, int startCol, int endRow, int endCol)
		{
		}

		public virtual void ClearToEOP(Page page)
		{
			for (int c = page.m_cursorPos.m_column; c < page.GetNumColumns(); c++)
			{
				page.GetCell(c, page.m_cursorPos.m_row).Clear((int)VideoAttribs.CHAR_CELL_DIRTY | page.GetWriteAttr() | page.GetPriorAttr());
			}
			for (int r = page.m_cursorPos.m_row+1; r < page.GetNumRows(); r++)
			{
				for (int c = 0; c < page.GetNumColumns(); c++)
				{
					page.GetCell(c, r).Clear((int)VideoAttribs.CHAR_CELL_DIRTY | page.GetWriteAttr() | page.GetPriorAttr());
				}
			}
		}

		
		public virtual void ReadBuffer(StringBuilder sb, Page page, int reqMask, int forbidMask, int startRow, int startCol, int endRow, int endCol)
		{
		}

		public virtual void SetCursor(Page page, int row, int col)
		{
			page.GetCell(page.m_cursorPos).SetDirty(true);
			page.m_cursorPos.m_row = row;
			page.m_cursorPos.m_column = col;
			page.GetCell(page.m_cursorPos).SetDirty(true);

			Debug.Assert(page.m_cursorPos.m_row < page.GetNumRows());
			Debug.Assert(page.m_cursorPos.m_column < page.GetNumColumns());
		}

		protected void Write(Cursor pos, Page page, int attribute, string text)
		{
			byte[] buf = new byte[text.Length];
			for (int x = 0; x < text.Length; x++)
			{
				buf[x] = (byte)text[x];
			}
			Write(pos, page, attribute, buf);
		}

		protected void Write(Cursor pos, Page page, int attribute, byte[] text)
		{
			Debug.Assert(attribute == 0 || (attribute & (int)VideoAttribs.MASK_CHAR) == 0);

			int len = text.Length;
			for (int x = 0; x < len; x++)
			{
				//page.mem[pos.row][pos.column] = text.charAt(x) | attribute | CHAR_CELL_DIRTY;
				//c = text.charAt(x);
				//System.out.print((char)(c & MASK_CHAR));		
				//c |= attribute;
				//System.out.print((char)(c & MASK_CHAR));
				WriteChar(page, pos, text[x] | attribute);
				if (pos.m_column == page.GetNumColumns()-1)
				{
					return;
				}
			}
			Debug.Assert(pos.m_row < page.GetNumRows());
			Debug.Assert(pos.m_column < page.GetNumColumns());
		}
	};
}
