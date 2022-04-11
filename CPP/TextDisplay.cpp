
#include "stdafx.h"

#include "TextDisplay.h"
#include "LocalPage.h"
#include "OutOfMemoryException.h"


TextDisplay::TextDisplay(int pageCount, int cols, int rows)
{
	if ((statusLine = new StringBuffer(80)) == NULL)
	{
		throw OutOfMemoryException("Out of memory in TextDisplay()");
	}
	echoOn = true;
	blockMode = false;
	protectMode = false;
	
	requiresRepaint = true;
	
	ppprotectMode = new ProtectPage();
	ppunProtectMode = new UnprotectPage();
	ppconvMode = new UnprotectPage();

	numPages = pageCount;
	numRows = rows;
	numColumns = cols;
	ppRemote = ppconvMode;
	
	pages = NULL;
	init();

	statusLine->insert(0, "                                                                                  ");
	statusLine->setLength(80);
	statusLine->insert(67, "CONV");
	writeDisplay("*******************************************************************************\r\n");
	writeDisplay("*\r\n*                   Washington State Department of Revenue\r\n*                             VT6530 Emulator\r\n\r\n");
	setCursorRowCol(23, 0);
	writeDisplay("*******************************************************************************");
	
	for (int x = 1; x < numRows; x++)
	{
		setCursorRowCol(x, 0);
		writeDisplay("*");
		setCursorRowCol(x, numColumns-1);
		writeDisplay("*");			
	}
}

TextDisplay::~TextDisplay()
{
	_ASSERT(_CrtIsMemoryBlock(statusLine, sizeof(StringBuffer), 0, 0, 0));
	_ASSERT(_CrtIsMemoryBlock(ppprotectMode, sizeof(ProtectPage), 0, 0, 0));
	_ASSERT(_CrtIsMemoryBlock(ppunProtectMode, sizeof(UnprotectPage), 0, 0, 0));
	_ASSERT(_CrtIsMemoryBlock(ppconvMode, sizeof(UnprotectPage), 0, 0, 0));

	delete statusLine;
	delete ppprotectMode;
	delete ppunProtectMode;
	delete ppconvMode;

	_ASSERT(_CrtIsMemoryBlock(pages, (numPages+1) * sizeof(Page *), 0, 0, 0));

	for (int x = 0; x < numPages+1; x++)
	{
		_ASSERT(_CrtIsMemoryBlock(pages[x], sizeof(Page), 0, 0, 0));
		delete pages[x];
	}
	delete[] pages;
	pages = NULL;
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
void TextDisplay::setProtectMode()
{
#ifdef _DEBUG
	_ASSERT(_CrtIsMemoryBlock(pages, (numPages+2) * sizeof(Page *), 0, 0, 0));
	for (int x = 0; x < numPages+1; x++)
	{
		_ASSERT(_CrtIsMemoryBlock(pages[x], sizeof(Page), 0, 0, 0));
	}
#endif
	protectMode = true;
	displayPage = pages[1];
	writePage = pages[1];
	ppRemote = ppprotectMode;
	clearAll();
	setVideoPriorCondition(VID_NORMAL);
	setInsertMode(INSERT_INSERT);
	initDataTypeTable();
	setKeysLocked();
	writeStatus("BLOCK PROT");
	requiresRepaint = true;
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
void TextDisplay::exitProtectMode()
{
#ifdef _DEBUG
	_ASSERT(_CrtIsMemoryBlock(pages, (numPages+2) * sizeof(Page *), 0, 0, 0));
	for (int x = 0; x < numPages+1; x++)
	{
		_ASSERT(_CrtIsMemoryBlock(pages[x], sizeof(Page), 0, 0, 0));
	}
#endif
	displayPage = pages[1];
	writePage = pages[1];
	ppRemote = ppunProtectMode;
	clearAll();
	setInsertMode(INSERT_INSERT);
	clearAllTabs();
	setKeysLocked();
	writeStatus("BLOCK");
	protectMode = false;
}


/** ESC 1
 * 
 *  Set a tab at the current cursor location
 */
void TextDisplay::setTab()
{
}

/** ESC 2
 * 
 *  Clear the tab at the current cursor location
 */
void TextDisplay::clearTab()
{
}

/** ESC 3
 */
void TextDisplay::clearAllTabs()
{
}

void TextDisplay::setModeConv()
{
#ifdef _DEBUG
	_ASSERT(_CrtIsMemoryBlock(pages, (numPages+2) * sizeof(Page *), 0, 0, 0));
	for (int x = 0; x < numPages+1; x++)
	{
		_ASSERT(_CrtIsMemoryBlock(pages[x], sizeof(Page), 0, 0, 0));
	}
#endif
	blockMode = false;
	displayPage = pages[1];
	writePage = pages[1];
	ppRemote = ppunProtectMode;
	setInsertMode(INSERT_INSERT);
	clearAllTabs();
	setKeysLocked();
	writeStatus("CONV");
	protectMode = false;
	ppRemote = ppconvMode;
	requiresRepaint = true;
}

/** ESC q
 */
void TextDisplay::init()
{
	if (pages != NULL)
	{
		//_ASSERT(_CrtIsMemoryBlock(pages, (numPages+2) * sizeof(Page *), 0, 0, 0));
		int x = 0;
		while (pages[x] != NULL)
		{
			_ASSERT(_CrtIsMemoryBlock(pages[x], sizeof(Page), 0, 0, 0));
			delete pages[x++];
		}
		delete[] pages;
	}
	pages = new Page *[numPages+2];

	for (int x = 0; x < numPages+1; x++)
	{
		if ((pages[x] = new Page(numRows, numColumns)) == NULL)
		{
			throw OutOfMemoryException("Out of memory in TextDisplay::init");
		}
	}
	pages[numPages+1] = NULL;
	displayPage = pages[0];
	writePage = pages[0];		
	requiresRepaint = true;
}

void TextDisplay::writeStatus(char *msg)
{
	for (int x = 67; x < 80; x++)
	{
		statusLine->setCharAt(x, ' ');
	}
	statusLine->insert(67, msg);
	requiresRepaint = true;
}

/** ESC o
 *  
 */
void TextDisplay::writeMessage(char *msg)
{
	for (int x = 1; x < 66; x++)
	{
		statusLine->setCharAt(x, ' ');
	}
	statusLine->insert(1, msg);
	requiresRepaint = true;
}

void TextDisplay::initDataTypeTable()
{
}

int TextDisplay::getCurrentPage()
{
	for (int x = 0; x < numPages+1; x++)
	{
		if (pages[x] == writePage)
		{
			return x;
		}
	}
	_ASSERT(false);
	return 1;
}

void TextDisplay::clearAll()
{
	for (int x = 0; x < numPages+1; x++)
	{
		if (pages[x] == NULL)
		{
			break;
		}
		_ASSERT(_CrtIsValidHeapPointer(pages[x]) == TRUE);
		pages[x]->clearPage();
		pages[x]->bufferPos.clear();
		pages[x]->cursorPos.clear();
		pages[x]->setWriteAttribute(VID_NORMAL);
		pages[x]->setVideoPriorCondition(VID_NORMAL);
	}
	requiresRepaint = true;
}
	
void TextDisplay::dumpScreen(StringBuffer *pw)
{	
	for (int r = 0; r < displayPage->getNumRows(); r++)
	{
		for (int c = 0; c < displayPage->getNumColumns(); c++)
		{
			int cell = displayPage->mem[r][c];
			pw->append((char)(cell & 0xFF));
		}
		pw->append((char)13);
		pw->append((char)10);
	}
	pw->append((char)13);
	pw->append((char)10);
}

void TextDisplay::dumpAttibutes(StringBuffer *pw)
{
	for (int r = 0; r < displayPage->getNumRows(); r++)
	{
		for (int c = 0; c < displayPage->getNumColumns(); c++)
		{
			int cell = displayPage->mem[r][c];
			pw->append((char)(cell & 0xFF));
			if ( (cell & VID_NORMAL) != 0)
			{
				pw->append("N");
			}
			else
			{
				pw->append("0");
			}
			if ( (cell & VID_BLINKING) != 0)
			{
				pw->append("B");
			}
			else
			{
				pw->append("0");
			}
			if ( (cell & VID_REVERSE) != 0)
			{
				pw->append("R");
			}
			else
			{
				pw->append("0");
			}
			if ( (cell & VID_INVIS) != 0)
			{
				pw->append("I");
			}
			else
			{
				pw->append("0");
			}
			if ( (cell & VID_UNDERLINE) != 0)
			{
				pw->append("U");
			}
			else
			{
				pw->append("0");
			}
			if ( (cell & DAT_MDT) != 0)
			{
				pw->append("M");
			}
			else
			{
				pw->append("0");
			}
			if ( (cell & DAT_TYPE) != 0)
			{
				pw->append((c>>SHIFT_DAT_TYPE) & 7);
			}
			else
			{
				pw->append("0");
			}
			if ( (cell & DAT_AUTOTAB) != 0)
			{
				pw->append("A");
			}
			else
			{
				pw->append("0");
			}
			if ( (cell & DAT_UNPROTECT) != 0)
			{
				pw->append("0");
			}
			else
			{
				pw->append("P");
			}
			if ( (cell & KEY_UPSHIFT) != 0)
			{
				pw->append("S");
			}
			else
			{
				pw->append("0");
			}
			if ( (cell & KEY_KB_ONLY) != 0)
			{
				pw->append("K");
			}
			else
			{
				pw->append("0");
			}
			if ( (cell & KEY_AID_ONLY) != 0)
			{
				pw->append("");
			}
			else
			{
				pw->append("");
			}
			if ( (cell & KEY_EITHER) != 0)
			{
				pw->append("");
			}
			else
			{
				pw->append("");
			}
			if ( (cell & CHAR_START_FIELD) != 0)
			{
				pw->append("F");
			}
			else
			{
				pw->append("0");
			}
			pw->append(",");
		}
		pw->append((char)13);
		pw->append((char)10);
	}
	pw->append((char)13);
	pw->append((char)10);
}

/**
 *  Get the 'index'nth field on the screen.
 *  The first field is index ZERO.  If the
 *  index is larger than the number of field,
 *  an empty string is returned.
 */
void TextDisplay::getField(int index, StringBuffer *accum)
{
	int count = 0;
	bool cap = false;
	
	for (int r = 0; r < numRows; r++)
	{
		for (int c = 0; c < numColumns; c++)
		{
			if ( (displayPage->mem[r][c] & CHAR_START_FIELD) != 0)
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
				accum->append ((char)(displayPage->mem[r][c] & MASK_CHAR));
			}
		}
	}
}

/**
 *  Get the video, data, and key attributes for a
 *  field.
 */
int TextDisplay::getFieldAttributes(int index)
{
	int count = 0;
	
	for (int r = 0; r < numRows; r++)
	{
		for (int c = 0; c < numColumns; c++)
		{
			if ( (displayPage->mem[r][c] & CHAR_START_FIELD) != 0)
			{
				if (count++ == index)
				{
					int r2 = r;
					int c2 = c+1;
					if (c2 >= numColumns)
					{
						r2++;
						c2 = 0;
					}
					return displayPage->mem[r2][c2] & MASK_FIELD;
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
void TextDisplay::getCurrentField(StringBuffer *accum)
{
	int count = 0;
	bool cap = false;
	int r = displayPage->cursorPos.row;
	int c = displayPage->cursorPos.column;
	
	while (c > 0)
	{
		if ( (displayPage->mem[r][c] & CHAR_START_FIELD) == 0)
		{
			break;
		}
		c--;
	}
	c++;
	while (c < numColumns)
	{
		if ( (displayPage->mem[r][c] & CHAR_START_FIELD) != 0)
		{
			break;
		}
		accum->append ((char)(displayPage->mem[r][c] & MASK_CHAR));
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
void TextDisplay::getUnprotectField(int index, StringBuffer *accum)
{
	int count = 0;
	bool cap = false;
	
	for (int r = 0; r < numRows; r++)
	{
		for (int c = 0; c < numColumns; c++)
		{
			if ( (displayPage->mem[r][c] & CHAR_START_FIELD) != 0)
			{
				if (cap)
				{
					return;
				}
				int r2 = r;
				int c2 = c+1;
				if (c2 >= numColumns)
				{
					c2 = 0;
					r2++;
				}
				if ( (displayPage->mem[r2][c2] & DAT_UNPROTECT) != 0)
				{
					if (count++ == index)
					{
						cap = true;
					}
				}
			}
			if (cap)
			{
				accum->append ((char)(displayPage->mem[r][c] & MASK_CHAR));
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
void TextDisplay::setField(int index, char *text)
{
	int count = 0;
	
	for (int r = 0; r < numRows; r++)
	{
		for (int c = 0; c < numColumns; c++)
		{
			if ( (displayPage->mem[r][c] & CHAR_START_FIELD) != 0)
			{
				int r2 = r;
				int c2 = c+1;
				if (c2 >= numColumns)
				{
					c2 = 0;
					r2++;
				}
				if ( (displayPage->mem[r2][c2] & DAT_UNPROTECT) != 0)
				{
					if (count++ == index)
					{
						setCursorRowCol(r2, c2);
						writeDisplay(text);
						requiresRepaint = true;
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
bool TextDisplay::isFieldChanged(int index)
{
	int count = 0;
	
	for (int r = 0; r < numRows; r++)
	{
		for (int c = 0; c < numColumns; c++)
		{
			if ( (displayPage->mem[r][c] & CHAR_START_FIELD) != 0)
			{
				int r2 = r;
				int c2 = c+1;
				if (c2 >= numColumns)
				{
					c2 = 0;
					r2++;
				}
				if ( (displayPage->mem[r2][c2] & DAT_UNPROTECT) != 0)
				{
					if (count++ == index)
					{
						return (displayPage->mem[r2][c2] & DAT_MDT) != 0;
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
void TextDisplay::getLine(int lineNumber, StringBuffer *sb)
{
	for (int c = 0; c < numColumns; c++)
	{
		sb->append((char)(displayPage->mem[lineNumber][c] & MASK_CHAR));
	}
}

/**
 *  Set the cursor at the start if the 
 *  'index'nth unprotected field on the screen.  
 *  The first field is index ZERO.  If the 
 *  index is larger than the number of field, 
 *  the request is ignored.
 */
void TextDisplay::cursorToField(int index)
{
	int count = 0;
	
	for (int r = 0; r < numRows; r++)
	{
		for (int c = 0; c < numColumns; c++)
		{
			if ( (displayPage->mem[r][c] & CHAR_START_FIELD) != 0)
			{
				int r2 = r;
				int c2 = c+1;
				if (c2 >= numColumns)
				{
					c2 = 0;
					r2++;
				}
				if ( (displayPage->mem[r2][c2] & DAT_UNPROTECT) != 0)
				{
					if (count++ == index)
					{
						setCursorRowCol(r2, c2);
						requiresRepaint = true;
						return;
					}
				}
			}
		}
	}
}

void TextDisplay::toHTML(COLORREF fg, COLORREF bg, StringBuffer *buf)
{
	char fgRGB[20];
	char bgRGB[20];

	wsprintf(fgRGB, "%X", fg);
	wsprintf(bgRGB, "%X", bg);

	int fieldCount = 0;
	bool inUnprot = false;
	
	StringBuffer accum;
	
	// write the style and script
	buf->append("<html>");
	buf->append("<script language='javascript'>function keys(){if (event.keyCode < 112 || event.keyCode > 123) {event.returnValue=true;return;} event.cancelBubble=true; event.returnValue=false;var k = document.forms('screen')('hdnKey'); switch(event.keyCode){case 112: k.value = 'F1'; break; case 10: k.value = 'ENTER'; break;} document.forms('screen').submit();} function canxIt(){event.cancelBubble = true;event.returnValue = false;}</script>");
	buf->append("<script language='javascript'>function loaded(){document.onkeydown=keys; document.onhelp=canxIt; var f = document.forms('screen')('F0'); if (f != null)f.focus();}</script>");
	buf->append("<script language='javascript'>function tabcheck(field){var f = document.forms('screen')('F'+field); if (f.value.length == f.maxLength()){field++; if (document.forms('screen')('F'+field) != null){document.forms('screen')('F'+field).focus();}else{document.forms('screen')('F0').focus();}}}</script>\r\n");
	buf->append("<body onload='loaded()' style='color: green; background: #3F3F3F'>\r\n");
	buf->append("<style type='text/css' >");
	buf->append(".normal { color: #");
	buf->append(fgRGB);
	buf->append("; background: #");
	buf->append(bgRGB);
	buf->append("; text-decoration: none}");
	buf->append(".reverse { color: #");
	buf->append(bgRGB);
	buf->append("; background: #");
	buf->append(fgRGB);
	buf->append("; text-decoration: none}");
	buf->append(".underline { color: #");
	buf->append(fgRGB);
	buf->append("; background: #");
	buf->append(bgRGB);
	buf->append("; text-decoration: underline} ");
	buf->append(".reverseunderline { color: #");
	buf->append(bgRGB);
	buf->append("; background: #");
	buf->append(fgRGB);
	buf->append("; text-decoration: underline}");
	buf->append(".blink { color: #");
	buf->append(fgRGB);
	buf->append("; background: #");
	buf->append(bgRGB);
	buf->append("; text-decoration: blink}");
	buf->append(".blinkreverse { color: #");
	buf->append(bgRGB);
	buf->append("; background: #");
	buf->append(fgRGB);
	buf->append("; text-decoration: blink}");
	buf->append("</style>\r\n");
	
	// write the table header
	buf->append("<form id='screen' method='post'><input type='hidden' id='hdnKey' value='' /><table cols='80' width='100%' >");
	
	for (int r = 0; r < numRows; r++)
	{
		buf->append("<tr>");
		int *row = displayPage->mem[r];
		
		for (int c = 0; c < numColumns; c++)
		{
			int ch = row[c];

			if (! inUnprot)
			{
				buf->append("<td class=");
				if ( (ch & MASK_COLOR) == 0)
				{
				}
				else
				{
				}
				if ( (ch & (VID_UNDERLINE|VID_REVERSE)) == (VID_UNDERLINE|VID_REVERSE))
				{
					buf->append("reverseunderline");
				}
				else if ( (ch & (VID_BLINKING|VID_REVERSE)) == (VID_BLINKING|VID_REVERSE))
				{
					buf->append("blinkreverse");
				}
				else if ( (ch & VID_BLINKING) != 0)
				{
					buf->append("blink");
				}
				else if ( (ch & VID_REVERSE) != 0)
				{
					buf->append("reverse");
				}
				else if ( (ch & VID_UNDERLINE) != 0)
				{
					buf->append("underline");
				}
				else
				{
					buf->append("normal");
				}
				if ( (ch & CHAR_START_FIELD) == 0)
				{
					buf->append(">");
					buf->append((char)(ch & MASK_CHAR));
					buf->append("</td>");
				}
			}
			if ( (ch & CHAR_START_FIELD) != 0)
			{
				if (inUnprot)
				{
					// end the input tag
					accum.append("' maxlength='");
					accum.append(accum.length());
					accum.append("' />");
					inUnprot = false;
					buf->append(" colspan=");
					buf->append(accum.length());
					buf->append(">");
					buf->append(accum);
					buf->append("</td>");
					accum.setLength(0);
				}
				// is the new field unprotected?
				int c2 = c + 1;
				if (c2 >= numColumns)
				{
					c2 = 0;
					row = displayPage->mem[++r];
				}
				if ( (row[c2] & DAT_UNPROTECT) != 0)
				{
					inUnprot = true;
					accum.append ("<input type='text' id='F");
					accum.append (fieldCount);
					accum.append ("' onkeypress='tabcheck(");
					accum.append ( fieldCount );
					accum.append (")' value='");
					accum.append ((char)(ch&MASK_CHAR));
					fieldCount++;
				}
			}
			else if (inUnprot)
			{
				accum.append((char)(ch & MASK_CHAR));
			}
		}
		buf->append("</tr>\r\n");
	}
	buf->append("</table></form><p/><center><a href='mailto:johnga@dor.wa.gov'>Got Bugs?</a></center></body></html>");
}

