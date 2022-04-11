using System;
using System.Collections.Generic;
using System.Text;

namespace LibVt6530
{
	public class Cursor
	{
		public int m_row, m_column;
		protected int m_numColumns;
		protected int m_numRows;

		public Cursor(int rows, int cols)
		{
			m_row = 0;
			m_column = 0;
			m_numColumns = cols;
			m_numRows = rows;
		}

		public void Clear()
		{
			m_row = 0;
			m_column = 0;
		}

		public void AdjustCol()
		{
			if (m_column >= m_numColumns)
			{
				m_column = 0;
				m_row++;
				AdjustRow();
			}
			if (m_column < 0)
			{
				m_column = 0;
				m_row--;
				AdjustRow();
			}
		}

		public void AdjustRow()
		{
			if (m_row >= m_numRows)
			{
				m_row = 0;
			}
			if (m_row < 0)
			{
				m_row = m_numRows - 1;
			}
		}
	};
}
