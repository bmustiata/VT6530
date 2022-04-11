using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace LibVt6530
{
	public class ProtectPage : SharedProtocol
	{
		public override void Home(Page page)
		{
		}

		public override void End(Page page)
		{
		}

		/** 0x09
		 * PROTECT
		 *  Move to the first position of the next 
		 *  unprotected field.
		 */
		public override void Tab(Page page, int inc)
		{
			int newX = page.ScanForNextField(page.m_cursorPos.m_column, page.m_cursorPos.m_row, inc);
			if (newX >= 0)
			{
				newX = page.ScanForUnprotectField(newX, page.m_cursorPos.m_row, inc);
				if (newX >= 0)
				{
					page.m_cursorPos.m_column = newX;
					return;
				}
			}
			int y = page.m_cursorPos.m_row+inc;
			for (int qpr = 0; qpr < page.GetNumRows(); qpr += inc)
			{
				if (inc > 0)
				{
					if (y >= page.GetNumRows())
					{
						y = 0;
					}
				}
				else
				{
					if (y < 0)
					{
						y = page.GetNumRows()-1;
					}
				}
				Debug.Assert(y < page.GetNumRows());
				newX = page.ScanForUnprotectField(0, y, inc);
				if (newX >= 0)
				{
					page.m_cursorPos.m_column = newX;
					page.m_cursorPos.m_row = y;
					Debug.Assert(page.m_cursorPos.m_row < page.GetNumRows());
					Debug.Assert(page.m_cursorPos.m_column < page.GetNumColumns());
					return;
				}
				//y++;
				y += inc;
			}
			Debug.Assert(page.m_cursorPos.m_row < page.GetNumRows());
			Debug.Assert(page.m_cursorPos.m_column < page.GetNumColumns());
		}

		public override void ClearToEOL(Page page)
		{
			int x = page.m_bufferPos.m_column;
			int y = page.m_bufferPos.m_row;
			int attr = page.GetCell(x, y).GetAttributes();
			
			if (page.GetCell(x, y).IsStartField())
			{
				// clear the video attributes for the field
				attr = (int)VideoAttribs.VID_NORMAL;
				page.GetCell(x, y).ClearVideoAttribs();
				x++;
			}
			attr |= (int)VideoAttribs.CHAR_CELL_DIRTY /*| (int)' '*/;
			while (x < page.GetNumColumns() && (!page.GetCell(x, y).IsStartField()))
			{
				page.GetCell(x, y).Clear(attr);
				x++;
			}
			if (x != page.GetNumColumns()) //((page.mem[y][x] & CHAR_START_FIELD) != 0)
			{
				return;
			}
			for (y = y+1; y < page.GetNumRows(); y++)
			{
				x = 0;
				while (x < page.GetNumColumns() && (!page.GetCell(x, y).IsStartField()))
				{
					page.GetCell(x, y).Clear(attr);
					x++;
				}
				if (x < page.GetNumColumns())
				{
					if (page.GetCell(x, y).IsStartField())
					{
						break;
					}
				}
			}
			Debug.Assert(page.m_bufferPos.m_row < page.GetNumRows());
			Debug.Assert(page.m_bufferPos.m_column < page.GetNumColumns());
		}

		public override void ClearToEOP(Page page)
		{
			Cursor cursor = page.m_bufferPos;
			
			int attr = page.GetWriteAttr() | page.GetPriorAttr();
			
			for (int c = cursor.m_column; c < page.GetNumColumns(); c++)
			{
				PageCell cell = page.GetCell(c, cursor.m_row);
				if (cell.IsUnprotect())
				{
					cell.Clear((int)VideoAttribs.CHAR_CELL_DIRTY | attr | (int)VideoAttribs.DAT_UNPROTECT);
				}
			}
			for (int r = cursor.m_row+1; r < page.GetNumRows(); r++)
			{
				for (int c = 0; c < page.GetNumColumns(); c++)
				{
					PageCell cell = page.GetCell(c, r);
					if (cell.IsUnprotect())
					{
						cell.Clear((int)VideoAttribs.CHAR_CELL_DIRTY | attr | (int)VideoAttribs.DAT_UNPROTECT);
					}
				}
			}
		}

		public override void CursorLeft(Page page)
		{
			page.GetCell(page.m_cursorPos).SetDirty(true);
			
			if (page.m_cursorPos.m_column == 0)
			{
				if (page.m_cursorPos.m_row == 0)
				{
					page.m_cursorPos.m_row = page.GetNumRows()-1;
				}
				else
				{
					page.m_cursorPos.m_row--;
					page.m_cursorPos.AdjustRow();
				}
				page.m_cursorPos.m_column = page.GetNumColumns()-1;
			}
			else
			{
				page.m_cursorPos.m_column--;
				page.m_cursorPos.AdjustCol();
			}
			if (!page.GetCell(page.m_cursorPos).IsUnprotect())
			{
				Tab(page, -1);
			}
			Debug.Assert(page.m_cursorPos.m_row < page.GetNumRows());
			Debug.Assert(page.m_cursorPos.m_column < page.GetNumColumns());
		}

		public override void ReadBuffer(StringBuilder accum, Page page, int reqMask, int forbidMask, int startRow, int startCol, int endRow, int endCol)
		{
			bool writeCr = false;
			StringBuilder sb = new StringBuilder();
			
			Debug.Assert(startRow >= 0 && endRow < page.GetNumRows());
			Debug.Assert(startCol >= 0 && endCol < page.GetNumColumns());
			
			accum.Length = 0;
			
			for (int y = startRow; y <= endRow; y++)
			{
				bool fieldStarted = false;
				
				for (int x = startCol; x < endCol; x++)
				{
					if (page.GetCell(x, y).IsStartField())
					{
						if (fieldStarted)
						{
							string trimmed = sb.ToString().Trim();
							if (trimmed.Length > 0)
							{
								accum.Append(trimmed);
							}
							sb.Length = 0;
							writeCr = false;
						}
						if ((page.GetCell(x + 1, y).AsInt() & reqMask) != 0)
						{
							fieldStarted = true;
							accum.Append((char)17);
							accum.Append((char)(y + 0x20));
							accum.Append((char)(x + 0x21));
							writeCr = true;
						}
						else
						{
							fieldStarted = false;
							writeCr = false;
						}
					}
					else if ((page.GetCell(x, y).AsInt() & (reqMask)) != 0 && fieldStarted)
					{
						sb.Append(page.GetCell(x, y).Get());
						writeCr = true;
					}
					else if (fieldStarted)
					{
						string trimmed = sb.ToString().Trim();
						if (trimmed.Length > 0)
						{
							accum.Append(trimmed);
						}
						sb.Length = 0;
						writeCr = false;
						fieldStarted = false;
					}
				}
				if (writeCr == true)
				{
					string trimmed = sb.ToString().Trim();
					if (trimmed.Length > 0)
					{
						accum.Append(trimmed);
					}
					writeCr = false;
					sb.Length = 0;
				}
			}
			accum.Append((char)4);
		}

		public override void WriteChar(Page page, Cursor bufferPos, int c)
		{
			Debug.Assert(bufferPos.m_row < page.GetNumRows());
			Debug.Assert(bufferPos.m_column < page.GetNumColumns());

			if ( (c & 0xFF) < 32)
			{
				base.WriteChar(page, bufferPos, c);
				return;
			}
			PageCell cell = page.GetCell(bufferPos);
			Debug.Assert(!cell.IsStartField());
			cell.Set((char)(0xFF & c));
			cell.SetAttributes((c & ~0xFF) | cell.GetAttributes() | page.GetWriteAttr() | (int)VideoAttribs.CHAR_CELL_DIRTY | page.GetPriorAttr());
			
			bufferPos.m_column++;
			bufferPos.AdjustCol();
			
			page.GetCell(bufferPos).SetDirty(true);
		}

		public override void Linefeed(Page page)
		{
			Tab(page, 1);
		}

		public override void ValidateCursorPos(Page page)
		{
			Debug.Assert(page.m_cursorPos.m_row < page.GetNumRows());
			Debug.Assert(page.m_cursorPos.m_column < page.GetNumColumns());
			
			if ( !page.GetCell(page.m_cursorPos).IsUnprotect() )
			{
				Tab(page, 1);
			}
			Debug.Assert(page.m_cursorPos.m_row < page.GetNumRows());
			Debug.Assert(page.m_cursorPos.m_column < page.GetNumColumns());
		}

		public override void SetCursor(Page page, int row, int col)
		{
			page.GetCell(page.m_cursorPos).SetDirty(true);
			page.m_cursorPos.m_row = row;
			page.m_cursorPos.m_column = col;
			if (page.GetCell(page.m_cursorPos).IsStartField())
			{
				CursorRight(page);
			}
			ValidateCursorPos(page);
			page.GetCell(page.m_cursorPos).SetDirty(true);

			Debug.Assert(page.m_cursorPos.m_row < page.GetNumRows());
			Debug.Assert(page.m_cursorPos.m_column < page.GetNumColumns());
		}

		public override void ClearBlock(Page page, int startRow, int startCol, int endRow, int endCol)
		{
			//int value = (int)VideoAttribs.CHAR_CELL_DIRTY | ' ';
			int mask = (int)VideoAttribs.MASK_FIELD ^ (int)VideoAttribs.CHAR_START_FIELD;
			PageCell cell;

			for (int y = startRow; y <= endRow; y++)
			{
				for (int x = startCol; x <= endCol; x++)
				{
					cell = page.GetCell(x, y);
					cell.Clear(cell.GetAttributes() & mask | (int)VideoAttribs.CHAR_CELL_DIRTY);
				}
			}
			ValidateCursorPos(page);
		}

		public override void ArrowDown(Page page, Cursor cursor)
		{
			Tab(page, 1);
		}

		public override void ArrowUp(Page page, Cursor cursor)
		{
			Tab(page, -1);
		}

		public override void ArrowLeft(Page page, Cursor cursor)
		{
			page.GetCell(cursor).SetDirty(true);
			cursor.m_column--;
			cursor.AdjustCol();
			if (!page.GetCell(cursor).IsUnprotect())
			{
				Tab(page, -1);
			}
			page.GetCell(cursor).SetDirty(true);
		}

		public override void ArrowRight(Page page, Cursor cursor)
		{
			page.GetCell(cursor).SetDirty(true);
			cursor.m_column++;
			cursor.AdjustCol();
			if (!page.GetCell(cursor).IsUnprotect())
			{
				Tab(page, 1);
			}
			page.GetCell(cursor).SetDirty(true);
		}
	};

	public class UnprotectPage : SharedProtocol
	{
		public virtual void Tab(int inc)
		{
			/** 0x09
			 * 
			 * UNPROTECT MODE
			 *  Move the the next tab stop on the row.  If the
			 *  cursor is past the last tab stop, move to 
			 *  column 1 of the next row.
			 */
		}

		public override void ClearToEOL(Page page)
		{
			for (int x = page.m_cursorPos.m_column; x < page.GetNumColumns(); x++)
			{
				page.GetCell(x, page.m_cursorPos.m_row).ClearTo(' ');
			}
		}

		public override void ReadBuffer(StringBuilder accum, Page page, int reqMask, int forbidMask, int startRow, int startCol, int endRow, int endCol)
		{
			for (int y = startRow-1; y < endRow-1; y++)
			{
				for (int x = startCol-1; x < endCol-1; x++)
				{
					accum.Append( page.GetCell(x, y).Get() );
				}
				accum.Append((char)13);
			}
		}
	};
}
