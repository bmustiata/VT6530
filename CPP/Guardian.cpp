
#include <ctype.h>
#include "stdafx.h"
#include "Guardian.h"
#include "StringBuffer.h"

#define CHAR_ESC '\27'
#define CHAR_BELL  7
#define CHAR_BKSPACE  8
#define CHAR_HTAB  9
#define CHAR_LF  10
#define CHAR_CR  13


void Guardian::processRemoteString(char *inp, int inplen)
{		
	int pos = 0;
	int dataTypeTableCount = 0;
	StringBuffer blockBuf;

	_ASSERT(_CrtIsMemoryBlock(display, sizeof(TextDisplay), 0, 0, 0));
	_ASSERT(_CrtIsMemoryBlock(telnet, sizeof(Telnet), 0, 0, 0));
	_ASSERT(_CrtIsMemoryBlock(keys, sizeof(Keys), 0, 0, 0));

	while (pos < inplen)
	{
		char ch = (char)inp[pos++];
		
		switch (state)
		{
			case 0:
				if (ch > 31)
				{
					accum.append(ch);
					break;
				}
				if (accum.length() > 0)
				{
					if (display->getProtectMode())
					{
						display->writeBuffer(accum);
					}
					else
					{
						display->writeDisplay(accum);
					}
					accum.setLength(0);
				}
				switch (ch)
				{
					case 0x00:
						break;
					case 0x01:
						// SOH
						state = 5000;
						break;
					case 0x04:
						// reset the line
						break;
					case 0x05:
						// ENQ
						break;
					case 0x07:
						// BELL
						display->bell();
						break;
					case 0x08:
						// Backspace
						display->backspace();
						break;
					case 0x09:
						// HTab
						display->tab();
						break;
					case 0x0A:
						// NL
						display->linefeed();
						break;
					case 0x0D:
						// CR
						display->carageReturn();
						break;
					case 0x0E:
						// shift out to G1 character set
						//System.out.println("G1 char set");
						break;
					case 0x0F:
						// Shift in to G0 character set
						//System.out.println("G0 char set");
						break;
					case 0x13:
						// set cursor address
						state = 42;
						break;
					case 0x1B:
						// ESC
						state = 1;
						break;
					case 0x11:
						// set buffer address (block mode)
						state = 56;
						break;
					case 0x1D:
						// start field
						state = 59;
						break;
					default:
						//System.out.println("Unknown char " + (int)ch);
						break;
				}
				break;
			case 1:
				// ESC
				switch (ch)
				{
					case '0':
						// print screen
						state = 0;
						display->printScreen();
						break;
					case '1':
						// Set tab at cursor location
						state = 0;
						display->setTab();
						break;
					case '2':
						// Clear tab
						display->clearTab();
						state = 0;
						break;
					case '3':
						// Clear all tabs
						display->clearAllTabs();
						state = 0;
						break;
					case '6':
						// Set video attributes
						state = 44;
						break;
					case '7':
						// Set video prior condition register
						state = 46;
						break;
					case ';':
						// Display page
						state = 48;
						break;
					case '?':
						// Read terminal configuration
						if (display->getBlockMode())
						{
							char *bp = "\1!A 2B72C 0D 0E 0F 0G 0H15I 3J 0L 0M 1N 0O 0P 0X 6S 0T 0U 1V 1W 1e 1f 0i 1h10\3\0";
							telnet->_send(bp, strlen(bp));
						}
						else
						{
							char *bp = "\1!A 2B72C 0D 0E 0F 0G 0H15I 3J 0L 0M 1N 0O 0P 0X 6S 0T 0U 1V 1W 1e 1f 0i 1h10\13";
							telnet->_send(bp, strlen(bp));								
						}
						state = 0;
						break;
					case '@':
						// Delay one second
						state = 0;
						break;
					case 'A':
						// Cursor up
						display->moveCursorUp();
						state = 0;
						break;
					case 'C':
						// Cursor right
						display->moveCursorRight();
						state = 0;
						break;
					case 'F':
						// Cursor home down
						display->end();
						state = 0;
						break;
					case 'H':
						// Cursor home
						display->home();
						state = 0;
						break;
					case 'I':
						// Clear memory to spaces
						// NOTE: in protect mode, this gets a block arg
						if (display->getProtectMode())
						{
							state = 15000;
							continue;
						}
						display->clearPage();
						state = 0;
						break;
					case 'J':
						// Erase to end of page/memory
						display->clearToEnd();
						state = 0;
						break;
					case 'K':
						// Erase to end of line/field
						display->clearEOL();
						state = 0;
						break;
					case '^':
						// Read terminal status
						if (display->getBlockMode())
						{
							char status[] = {1, 63, 66, 70, 67, 67, 94, 64, 3, 0, 4};
							telnet->_send(status, 11);
						}
						else
						{
							char status[] = {1, 63, 67, 70, 67, 13}; //67, 94, 64, 3, 0};
							telnet->_send(status, 6);
						}
						state = 0;
						break;
					case '_':
						// Read firmware revision level
						if (display->getBlockMode())
						{
							//System.out.println("Read firmware revision level");
						}
						else
						{
							char status[] = {1, 35, 67, 48, 48, 84, 79, 67, 48, 48, 13};
							telnet->_send(status, 11);
						}
						state = 0;
						break;
					case 'a':
						// Read cursor address
						{
							char cursorPos[] = {1, '_', '!', 0, 0, 13};
							cursorPos[3] = display->getCursorRow();
							cursorPos[4] = display->getCursorCol();
							telnet->_send(cursorPos, 6);
						}
						state = 0;
						break;
					case 'b':
						// Unlock keyboard
						keys->unlockKeyboard();
						display->setKeysUnlocked();
						state = 10000;
						break;
					case 'c':
						// Lock keyboard
						keys->lockKeyboard();
						display->setKeysLocked();
						state = 0;
						break;
					case 'd':
						// Simulate function key
						keys->lockKeyboard();
						state = 50;
						break;
					case 'f':
						// Disconnect modem
						//System.out.println("Disconnect");
						state = 0;
						break;
					case 'o':
						// Write to message field
						state = 52;
						break;
					case 'v':
						// set terminal configuration
						state = 54;
						break;
					case 'x':
						//Set IO device configuration
						//System.out.println("ESC x recived");
						state = 0;
						break;
					case 'y':
						// Read IO device configuration
						//System.out.println("ESC y recived");
						state = 0;
						break;
					case '{':
						// Write to file or device driver
						//System.out.println("ESC { recived");
						state = 0;
						break;
					case '}':
						// Write/read to file or device driver
						//System.out.println("ESC } recived");
						state = 0;
						break;
					case '-':
						// extended CSI sequence
						state = 3;
						break;
					case ':':
						// select page
						state = 66;
						break;
					case '<':
						// read buffer
						//if (ASSERT.debug > 0)
						//	display.dumpScreen(Logger.out);
						blockBuf.setLength(0);
						display->readBufferUnprotectIgnoreMdt(&blockBuf, 0, 0, display->getNumRows()-1, display->getNumColumns()-1);
						telnet->_send(blockBuf, blockBuf.length());
						blockBuf.setLength(0);
						state = 0;
						break;
					case '=':
						// Read with address
						state = 67;
						break;
					case '>':
						// Reset modified data tags
						display->resetMdt();
						state = 0;
						break;
					case 'L':
						display->lineDown();
						state = 0;
						break;
					case 'M':
						display->deleteLine();
						state = 0;
						break;
					case 'N':
						// disable local line editing until
						// 1. ESC q
						// 2. Exit block mode
						// 3. protect to nonprotect submode
						//System.out.println("Disable local line editing");
						state = 0;
						break;
					case 'O':
						// insert char
						display->insertChar();
						state = 0;
						break;
					case 'P':
						// delete char
						display->deleteChar();
						state = 0;
						break;
					case 'S':
						// roll up
						//System.out.println("Roll up");
						state = 0;
						break;
					case 'T':
						// roll down
						state = 0;
						display->lineDown();
						break;
					case 'U':
						// page down
						//System.out.println("Page down");
						state = 0;
						break;
					case 'V':
						// page up
						//System.out.println("Page up");
						state = 0;
						break;
					case 'W':
						//  Enter protect mode
						display->setProtectMode();
						keys->setProtectMode();
						state = 0;
						break;
					case 'X':
						// exit protect mode
						display->exitProtectMode();
						keys->exitProtectMode();
						state = 0;
						break;
					case '[':
						// start field extended
						state = 71;
						break;
					case ']':
						// Read with address all
						if (display->getProtectMode())
						{
							// same as ESC =
							state = 67;
							continue;
						}
						state = 75;
						break;
					case 'i':
						// back tab
						display->backtab();
						state = 0;
						break;
					case 'p':
						// set max page num
						state = 81;
						break;
					case 'q':
						// reinitialize
						display->init();
						display->setProtectMode();
						display->exitProtectMode();
						state = 10000;
						break;
					case 'r':
						// Define data type table
						state = 84;
						break;
					case 'u':
						// define enter key function
						state = 82;
						break;
					default:
						//System.out.println("Unknown ESC " + (int)ch);
						state = 0;
				}
				break;
			case 3:
				if (isdigit(ch))
				{
					state = 24;
					accum.append(ch);
					continue;
				}
				switch (ch)
				{
					case 'c':
						strStack.addElement("7");
						state = 30;
						break;
					case 'e':
						// Get machine name 3-28
						{
							char name[] = {1, '&', 'j', 'o', 'h', 'n', 13};
							telnet->_send(name, 7);
						}
						state = 0;
						break;
					case 'V':
						state = 39;
						break;
					case 'W':
						// Report Exec code 3-34
						{
							char code[] = {1, '?', (1<<6) | 1, 'F', 'D', 13};
						}
						state = 0;
						break;
					case 'J':
						blockBuf.setLength(0);
						display->readBufferUnprotect(&blockBuf, 0, 0, display->getNumRows()-1, display->getNumColumns()-1);
						telnet->_send(blockBuf, blockBuf.length());
						state = 0;
						break;
					default:
						//System.out.println("Unknown ESC - " + (int)ch);
						break;
				}
				break;
			case 39:
				if (ch == CHAR_CR)
				{
					// Execute local program
					//System.out.println("Execute local program " + accum);
					accum.setLength(0);
					state = 0;
					continue;
				}
				accum.append(ch);
				break;
			case 24:
				if (isdigit(ch))
				{
					accum.append(ch);
					state = 25;
					continue;
				}
				switch (ch)
				{
					case ';':
						strStack.addElement(accum);
						accum.setLength(0);
						state = 34;
						break;
					case 'd':
						// Read string configuration param
						//System.out.println("Read string config param " + accum);
						accum.setLength(0);
						state = 0;
						break;
					case 'c':
						state = 30;
						break;
					default:
						//System.out.println("Unknown ESC-" + accum.toString() + " " + (int)ch);
						break;
				}
				break;
			case 34:
				if (isdigit(ch))
				{
					accum.append(ch);
					continue;
				}
				if (ch != ';')
				{
					//System.out.println("Expected ';' in state 34; Got " + (int)ch);
				}
				strStack.addElement(accum);
				accum.setLength(0);
				state = 35;
				break;
			case 35:
				if (isdigit(ch))
				{
					accum.append(ch);
					continue;
				}
				switch (ch)
				{
					case 'C':
						// set buffer address extended
						display->setBufferRowCol(atoi(strStack.elementAt(0)), atoi(accum));
						strStack.clear();
						accum.setLength(0);
						state = 0;
						break;
					case 'q':
						strStack.addElement(accum);
						strStack.addElement("q");
						accum.setLength(0);
						state = 36;
						break;
					case 'I':
						// Clear memory to spaces extended
						{
							int sr = strStack.elementAt(0)[0];
							int sc = strStack.elementAt(0)[1];
							int er = accum[0];
							int ec = accum[1];
							accum.setLength(0);
							strStack.clear();
							display->clearBlock(sr, sc, er, ec);
							state = 0;
						}
						break;
					case ';':
						strStack.addElement(accum);
						state = 64;
						break;
					default:
						//System.out.println("Unexpected char in 35: " + (int)ch);
						break;
				}
				break;
			case 36:
				switch (ch)
				{
					case '0':
					case '1':
					case '2':
					case '3':
					case '4':
					case '5':
					case '6':
					case '7':
					case '8':
					case '9':
					case 'A':
					case 'B':
					case 'C':
					case 'D':
					case 'E':
					case 'F':
						strStack.addElement((char) ch);
						state = 37;
						continue;
				}
				strStack.addElement(accum);
				accum.setLength(0);
				{
					char *p1;
					int p2, p3;

					p1 = strStack.elementAt(0);
					if (p1[0] == '0')
					{
						// reset color map
						//System.out.println("Reset color map");
					}
					else
					{
						//System.out.println("Set color map");
						p2 = atoi(strStack.elementAt(1));
						p3 = atoi(strStack.elementAt(2));
						
						if (strStack.elementAt(3)[0] != 'q')
						{
							//System.out.println("State 36 Error");
						}
						for (int x = 0; x < p3-p2; x++)
						{
							// setColorMap(p2+x, Integer.parseInt((String)strStack.elementAt(x*2+4), 16), Integer.parseInt((String)strStack.elementAt(x*2+5), 16) );
						}
					}
					strStack.clear();
				}
				break;
			case 37:
				switch (ch)
				{
					case '0':
					case '1':
					case '2':
					case '3':
					case '4':
					case '5':
					case '6':
					case '7':
					case '8':
					case '9':
					case 'A':
					case 'B':
					case 'C':
					case 'D':
					case 'E':
					case 'F':
						strStack.addElement((char) ch);
						state = 36;
						continue;
				}
				//System.out.println("Bad hex in state 37: " + (int)ch);
				break;
			case 30:
				if (ch > 31)
				{
					accum.append(ch);
					continue;
				}
				if (ch == 0x12)
				{
					strStack.addElement(accum);
					accum.setLength(0);
					continue;
				}
				if (ch != CHAR_CR)
				{
					//System.out.println("Expected CR in 30; Got " + (int)ch);
				}
				accum.setLength(0);
				{
					int count = atoi(strStack.elementAt(0));
					for (int x = 1; x < strStack.size(); x++)
					{
						//System.out.println("Parameter recived: " + (String)strStack.elementAt(x));
					}
				}
				strStack.clear();
				break;
			case 25:
				if (isdigit(ch))
				{
					accum.append(ch);
					continue;
				}
				if (ch == ';')
				{
					strStack.addElement(accum);
					accum.setLength(0);
					state = 27;
					continue;
				}
				//System.out.println("Unexpected char in 25: " + (int)ch);
				break;
			case 27:
				if (isdigit(ch))
				{
					accum.append(ch);
					continue;
				}
				if (ch == 'D')
				{
					// set cursor position
					int curx = strStack.elementAt(0)[0] - 32;
					int cury = strStack.elementAt(1)[0] - 32;
					display->setCursorRowCol(curx, cury);
				}
				else if (ch == 'O')
				{
					// write to AUX
					//System.out.println("Write to AUX");
				}
				else
				{
					//System.out.println("Unexpcted char in 27: " + (int)ch);
				}
				break;
			case 42:
				strStack.addElement((char) ch);
				state = 43;
				break;
			case 43:
				display->setCursorRowCol( strStack.elementAt(0)[0] - 0x20, (int)ch - 0x20);
				strStack.clear();
				state = 0;
				break;
			case 44:
				display->setWriteAttribute((int)ch & ~(1<<5));
				state = 0;
				break;
			case 46:
				display->setPriorWriteAttribute((int)ch & ~(1<<5));
				state = 0;
				break;
			case 48:
				display->setDisplayPage(ch - 0x20);
				state = 0;
				break;
			case 50:
				{
					char fnKey[] = {1, ch, 0, 0, 13};
					fnKey[2] = display->getCursorRow();
					fnKey[3] = display->getCursorCol();
					telnet->_send(fnKey, 5);
				}
				state = 0;
				break;
			case 52:
				if (ch == 13)
				{
					display->writeMessage(accum);
					accum.setLength(0);
					state = 0;
					continue;
				}
				accum.append(ch);
				break;
			case 54:
				if (ch == 13)
				{
					switch (accum[0])
					{
						case 'A':
							// cursor type
							break;
						case 'F':
							// language
							break;
						case 'G':
							// mode
							break;
						case 'M':
							// enter key mode
							if (accum[1] == '0')
								keys->setEnterKeyOff();
							else
								keys->setEnterKeyOn();
							break;
						case 'T':
							// normal intensity
							break;
						case 'V':
							// character size
							break;
						default:
							//System.out.println("State 54: " + accum);
							break;
					}
					accum.setLength(0);
					state = 0;
					continue;
				}
				accum.append((char) ch);
				break;
			case 56:
				strStack.addElement((char) ch);
				state = 57;
				break;
			case 57:
				display->setBufferRowCol( strStack.elementAt(0)[0] - 0x20, ch - 0x20);
				strStack.clear();
				state = 0;
				break;
			case 59:
				strStack.addElement((char) ch);
				state = 60;
				break;
			case 60:
				display->startField( strStack.elementAt(0)[0] - 0x20, ch - 0x20);
				strStack.clear();
				state = 0;
				break;
			case 64:
				if (isdigit(ch))
				{
					accum.append(ch);
					continue;
				}
				switch(ch)
				{
					case 'J':
					case 'K':
						{
							int sr = atoi(strStack.elementAt(0));
							int sc = atoi(strStack.elementAt(1));
							int er = atoi(strStack.elementAt(2));
							int ec = atoi(accum);
							strStack.clear();
							accum.setLength(0);
							blockBuf.setLength(0);
							display->readBufferAllIgnoreMdt(&blockBuf, sr, sc, er, ec);
							telnet->_send(blockBuf, blockBuf.length());
							blockBuf.setLength(0);
						}
						break;
					default:
						//System.out.println("Unexpected char in 64: " + (int)ch);
						break;
				}
				break;
			case 66:
				display->setPage(ch-0x20);
				state = 0;
				break;
			case 67:
				if (strStack.size() == 3)
				{
					int sr = strStack.elementAt(0)[0] - 0x20;
					int sc = strStack.elementAt(1)[0] - 0x20;
					int er = strStack.elementAt(2)[0] - 0x20;
					int ec = ch - 0x20;
					blockBuf.setLength(0);
					display->readBufferAllMdt(&blockBuf, sr, sc, er, ec);
					telnet->_send(blockBuf, blockBuf.length());
					blockBuf.setLength(0);
					strStack.clear();
					state = 0;
					continue;
				}
				strStack.addElement((char) ch);
				break;
			case 71:
				strStack.addElement((char) ch);
				state = 72;
				break;
			case 72:
				strStack.addElement((char) ch);
				state = 73;
				break;
			case 73:
				{
					int vidAttr = strStack.elementAt(0)[0] - 0x20;
					int dataAttr = strStack.elementAt(1)[0] - 0x20;
					int keyAttr = ch - 0x20;
					display->startField(vidAttr, dataAttr, keyAttr);
				}
				strStack.clear();
				state = 0;
				break;
			case 75:
				strStack.addElement(ch);
				state = 76;
				break;
			case 76:
				strStack.addElement(ch);
				state = 77;
				break;
			case 77:
				if (ch != ';')
				{
					//System.out.println("Expected ; in 77: " + (int)ch);
				}
				state = 78;
				break;
			case 78:
				strStack.addElement((char) ch);
				state = 79;
				break;
			case 79:
				{
					int sr = strStack.elementAt(0)[0] - 0x20;
					int sc = strStack.elementAt(1)[0] - 0x20;
					int er = strStack.elementAt(2)[0] - 0x20;
					int ec = ch - 0x20;
					blockBuf.setLength(0);
					display->readFieldsAll(&blockBuf, sr, sc, er, ec);
					telnet->_send(blockBuf, blockBuf.length());
					blockBuf.setLength(0);
				}
				strStack.clear();
				state = 0;
				break;
			case 81:
				display->setPageCount(((int)ch) - 0x30);
				state = 10000;
				break;
			case 82:
				strStack.addElement((char)(ch-0x20));
				state = 83;
				break;
			case 83:
				accum.append(ch);
				ch = strStack.elementAt(0)[0] - 1;
				strStack.clear();
				if (ch == 0)
				{
					//keys->setMap(13, 0, accum);
					accum.setLength(0);
					state = 0;
				}
				else
				{
					strStack.addElement((char)ch);
				}
				break;
			case 84:
				// datatype table add ch
				if (++dataTypeTableCount == 96)
				{
					// set data type table
					state = 0;
					//System.out.println("Set data type table");
				}
				break;
			case 10000:
				if (ch == 4)
				{
					state = 0;
					continue;
				}
				if (ch != 13) 
				{
					//System.out.println("Guardian Expected 13 in 10000: " + (int)ch);
				}
				state = 10001;
				break;
			case 10001:
				if (ch == 4)
				{
					state = 0;
					continue;
				}
				if (ch != 10)
				{
					//System.out.println("Guardian Expected 10 in 10001: " + (int)ch);
				}
				state = 0;
				break;
			case 5000:
				switch (ch)
				{
					case 'A':
						// ANSI terminal mode
						telnet->setBufferingOff();
						break;
					case 'B':
						// BLOCK mode
						display->setModeBlock();
						keys->setKeySet(KEYS_BLOCK);
						telnet->setBufferingOn();
						break;
					case 'C':
						// Conversational mode
						display->setModeConv();
						keys->setKeySet(KEYS_CONV);
						telnet->setBufferingOff();
						break;
					case '!':
						// send term config?
						state = 5050;
						break;
					default:
						//System.out.println("Unexpected char in 5000: " + (int)ch);
						break;
				}
				state = 5001;
				break;
			case 5001:
				if (ch != 3) 
				{
					//System.out.println("Expected 3 in 5000: " + (int)ch);
				}
				state = 0;
				break;
			case 5050:
				// accept chars until 3
				if (ch == 3)
				{
					//System.out.println("Send term config? " + accum.toString());
					accum.setLength(0);
					state = 0;
					continue;
				}
				accum.append((char) ch);
				break;
			case 15000:
				// ESC I (Clear Block)
				if (strStack.size() == 3)
				{
					display->clearBlock(strStack.elementAt(0)[0]-0x20, strStack.elementAt(1)[0]-0x20, strStack.elementAt(2)[0]-0x20, ch - 0x20);
					strStack.clear();
					state = 0;
					continue;
				}
				strStack.addElement((char) ch);
				break;
			default:
				//System.out.println("Unknown state " + state);
				break;
		}
	}
	if (accum.length() > 0)
	{
		if (display->getProtectMode())
		{
			display->writeBuffer(accum);
		}
		else
		{
			display->writeDisplay(accum);
		}
		accum.setLength(0);
	}
}

void Guardian::execLocalCommand(char cmd)
{
	_ASSERT(_CrtIsMemoryBlock(display, sizeof(TextDisplay), 0, 0, 0));
	_ASSERT(_CrtIsMemoryBlock(telnet, sizeof(Telnet), 0, 0, 0));

	char buf[2];
	buf[0] = cmd;
	buf[1] = '\0';

	display->writeLocal(buf);

	if (! display->getBlockMode())
	{
		if (cmd == 13)
		{
			buf[0] = 10;
			display->writeLocal(buf);
			keyBuffer.append((char)13);
			telnet->send(keyBuffer, keyBuffer.length());
			keyBuffer.setLength(0);
		}
		else if (cmd == '\b' && keyBuffer.length() > 0)
		{
			keyBuffer.setLength(keyBuffer.length()-1);
		}
		else
		{
			keyBuffer.append(cmd);
		}
	}
	display->setRePaint();
}

/*void Guardian::execLocalCommand(char cmd)
{
	char buf[2];
	buf[0] = cmd;
	buf[1] = '\0';

	display->writeLocal(buf);

	if (cmd == 10)
	{
		buf[0] = 13;
		display->writeLocal(buf);
	}
	if (display->getProtectMode())
	{
		if (cmd == '\t')
		{
			display->tab();
		}
	}
	else
	{
		if (cmd == 10)
		{
			keyBuffer.append((char) (char)13);
			telnet->send(keyBuffer, keyBuffer.length());
			keyBuffer.setLength(0);
		}
		else if (cmd == '\b')
		{
			keyBuffer.setLength(keyBuffer.length()-1);
		}
		else
		{
			keyBuffer.append((char)cmd);
		}
	}
	display->setRePaint();
}*/

