#ifndef _cursor_h
#define _cursor_h


class Cursor
{
public:
	int row, column;
	int numColumns;
	int numRows;

public:

	Cursor(int rows, int cols)
	{
		row = 0;
		column = 0;
		numColumns = cols;
		numRows = rows;
	}
	
	void clear()
	{
		row = 0;
		column = 0;
	}

	void adjustCol()
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

	void adjustRow()
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
};

#endif