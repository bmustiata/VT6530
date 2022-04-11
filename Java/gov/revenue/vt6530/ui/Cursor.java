package gov.revenue.vt6530.ui;

import gov.revenue.ASSERT;

/**
 *  Cursor holds the cursor or buffer position
 *  for a page.  
 */
class Cursor
{
	int row, column;
	int numColumns;
	int numRows;
	
	Cursor(int rows, int cols)
	{
		numColumns = cols;
		numRows = rows;
	}
	
	final void clear()
	{
		row = 0;
		column = 0;
	}

	/**
	 *  Ensure the column is in the display
	 *  area.
	 */
	final void adjustCol()
	{
		if (column >= numColumns)
		{
			column = 0;
			row++;
			adjustRow();
		}
		if (column < 0)
		{
			column = 0;
			row--;
			adjustRow();
		}
	}

	/**
	 *  Ensure the row is in the display area.
	 */
	final void adjustRow()
	{
		if (row >= numRows)
		{
			row = 0;
		}
		if (row < 0)
		{
			row = numRows - 1;
		}
	}
}
