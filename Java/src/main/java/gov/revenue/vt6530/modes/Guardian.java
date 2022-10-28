package gov.revenue.vt6530.modes;

import java.io.IOException;

import gov.revenue.ASSERT;
import gov.revenue.vt6530.ui.TextDisplay;
import gov.revenue.vt6530.ui.Keys;
import gov.revenue.vt6530.telnet.*;
import gov.revenue.vt6530.FastVector;
import gov.revenue.Logger;

/**
 *  Guardian is an interpreter for VT6530 block
 *  and conversation mode commands.  Term calls
 *  Guardian with data recieved from Telnet.
 * 
 */
public final class Guardian implements Mode
{	
	private static final char CHAR_ESC = '\27';
	private static final char CHAR_BELL = 7;
	private static final char CHAR_BKSPACE = 8;
	private static final char CHAR_HTAB = 9;
	private static final char CHAR_LF = 10;
	private static final char CHAR_CR = 13;

	/**
	 *  Buffer for string elements
	 */
	private FastVector strStack = new FastVector();
	
	/**
	 *  Buffer for building strings
	 */
	private StringBuffer accum = new StringBuffer();
	
	/**
	 *  Some other buffer
	 */
	private StringBuffer keyBuffer = new StringBuffer();
	
	/**
	 *  Some command change the state of keyboard handling
	 */
	private Keys keys;
	
	/**
	 *  For sending data to tandem
	 */
	private Telnet telnet;
	
	/**
	 *  Virtual 80X25 character display
	 */
	private TextDisplay display;
	
	/**
	 *  The current state
	 */
	int state;
	
	
	public Guardian(TextDisplay display, Keys keys, Telnet telnet)
	{
		this.keys = keys;
		this.telnet = telnet;
				
		this.display = display;
	}

	/**
	 *  Process an incomming command string from the Tandem
	 */
	public void processRemoteString(byte[] inp) throws IOException
	{		
		int pos = 0;
		int dataTypeTableCount = 0;

		while (pos < inp.length)
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
						if (display.getProtectMode())
						{
							display.writeBuffer(accum.toString());
						}
						else
						{
							display.writeDisplay(accum.toString());
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
							display.bell();
							break;
						case 0x08:
							// Backspace
							display.backspace();
							break;
						case 0x09:
							// HTab
							display.tab();
							break;
						case 0x0A:
							// NL
							display.linefeed();
							break;
						case 0x0D:
							// CR
							display.carageReturn();
							break;
						case 0x0E:
							// shift out to G1 character set
							System.out.println("G1 char set");
							break;
						case 0x0F:
							// Shift in to G0 character set
							System.out.println("G0 char set");
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
							System.out.println("Unknown char " + (int)ch);
					}
					break;
				case 1:
					// ESC
					switch (ch)
					{
						case '0':
							// print screen
							state = 0;
							display.printScreen();
							break;
						case '1':
							// Set tab at cursor location
							state = 0;
							display.setTab();
							break;
						case '2':
							// Clear tab
							display.clearTab();
							state = 0;
							break;
						case '3':
							// Clear all tabs
							display.clearAllTabs();
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
							if (display.getBlockMode())
							{
								String bp = "\1!A 2B72C 0D 0E 0F 0G 0H15I 3J 0L 0M 1N 0O 0P 0X 6S 0T 0U 1V 1W 1e 1f 0i 1h10\3\0";
								telnet.send(bp.getBytes());
							}
							else
							{
								String bp = ((char)1) + "!A 2B72C 0D 0E 0F 0G 0H15I 3J 0L 0M 1N 0O 0P 0X 6S 0T 0U 1V 1W 1e 1f 0i 1h10" + (char)13;
								telnet.send(bp.getBytes());								
							}
							state = 0;
							break;
						case '@':
							// Delay one second
							state = 0;
							break;
						case 'A':
							// Cursor up
							display.moveCursorUp();
							state = 0;
							break;
						case 'C':
							// Cursor right
							display.moveCursorRight();
							state = 0;
							break;
						case 'F':
							// Cursor home down
							display.end();
							state = 0;
							break;
						case 'H':
							// Cursor home
							display.home();
							state = 0;
							break;
						case 'I':
							// Clear memory to spaces
							// NOTE: in protect mode, this gets a block arg
							if (display.getProtectMode())
							{
								state = 15000;
								continue;
							}
							display.clearPage();
							state = 0;
							break;
						case 'J':
							// Erase to end of page/memory
							display.clearToEnd();
							state = 0;
							break;
						case 'K':
							// Erase to end of line/field
							display.clearEOL();
							state = 0;
							break;
						case '^':
							// Read terminal status
							if (display.getBlockMode())
							{
								byte[] status = {1, 63, 66, 70, 67, 67, 94, 64, 3, 0};
								telnet.send(status);
							}
							else
							{
								byte[] status = {1, 63, 67, 70, 67, 13}; //67, 94, 64, 3, 0};
								telnet.send(status);
							}
							state = 0;
							break;
						case '_':
							// Read firmware revision level
							if (display.getBlockMode())
							{
								System.out.println("Read firmware revision level");
							}
							else
							{
								byte[] status = {1, 35, 67, 48, 48, 84, 79, 67, 48, 48, 13};
								telnet.send(status);
							}
							state = 0;
							break;
						case 'a':
							// Read cursor address
							{
								byte[] cursorPos = {1, (byte)'_', (byte)'!', 0, 0, 13};
								cursorPos[3] = (byte)display.getCursorRow();
								cursorPos[4] = (byte)display.getCursorCol();
								telnet.send(cursorPos);
							}
							state = 0;
							break;
						case 'b':
							// Unlock keyboard
							keys.unlockKeyboard();
							display.keysUnlocked();
							state = 10000;
							break;
						case 'c':
							// Lock keyboard
							keys.lockKeyboard();
							display.keysLocked();
							state = 0;
							break;
						case 'd':
							// Simulate function key
							keys.lockKeyboard();
							state = 50;
							break;
						case 'f':
							// Disconnect modem
							System.out.println("Disconnect");
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
							System.out.println("ESC x recived");
							state = 0;
							break;
						case 'y':
							// Read IO device configuration
							System.out.println("ESC y recived");
							state = 0;
							break;
						case '{':
							// Write to file or device driver
							System.out.println("ESC { recived");
							state = 0;
							break;
						case '}':
							// Write/read to file or device driver
							System.out.println("ESC } recived");
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
							if (ASSERT.debug > 0)
								display.dumpScreen(Logger.out);
							telnet.send(display.readBufferUnprotectIgnoreMdt(0, 0, display.getNumRows()-1, display.getNumColumns()-1));
							state = 0;
							break;
						case '=':
							// Read with address
							state = 67;
							break;
						case '>':
							// Reset modified data tags
							display.resetMdt();
							state = 0;
							break;
						case 'L':
							display.lineDown();
							state = 0;
							break;
						case 'M':
							display.deleteLine();
							state = 0;
							break;
						case 'N':
							// disable local line editing until
							// 1. ESC q
							// 2. Exit block mode
							// 3. protect to nonprotect submode
							System.out.println("Disable local line editing");
							state = 0;
							break;
						case 'O':
							// insert char
							display.insertChar();
							state = 0;
							break;
						case 'P':
							// delete char
							display.deleteChar();
							state = 0;
							break;
						case 'S':
							// roll up
							System.out.println("Roll up");
							state = 0;
							break;
						case 'T':
							// roll down
							state = 0;
							display.lineDown();
							break;
						case 'U':
							// page down
							System.out.println("Page down");
							state = 0;
							break;
						case 'V':
							// page up
							System.out.println("Page up");
							state = 0;
							break;
						case 'W':
							//  Enter protect mode
							display.setProtectMode();
							keys.setProtectMode();
							state = 0;
							break;
						case 'X':
							// exit protect mode
							display.exitProtectMode();
							keys.exitProtectMode();
							state = 0;
							break;
						case '[':
							// start field extended
							state = 71;
							break;
						case ']':
							// Read with address all
							if (display.getProtectMode())
							{
								// same as ESC =
								state = 67;
								continue;
							}
							state = 75;
							break;
						case 'i':
							// back tab
							display.backtab();
							state = 0;
							break;
						case 'p':
							// set max page num
							state = 81;
							break;
						case 'q':
							// reinitialize
							display.init();
							display.setProtectMode();
							display.exitProtectMode();
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
							System.out.println("Unknown ESC " + (int)ch);
							state = 0;
					}
					break;
				case 3:
					if (Character.isDigit(ch))
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
								byte[] name = {1, (byte)'&', (byte)'j', (byte)'o', (byte)'h', (byte)'n', 13};
								telnet.send(name);
							}
							state = 0;
							break;
						case 'V':
							state = 39;
							break;
						case 'W':
							// Report Exec code 3-34
							{
								byte[] code = {1, (byte)'?', (1<<6) | 1, (byte)'F', (byte)'D', 13};
							}
							state = 0;
							break;
						case 'J':
							telnet.send(display.readBufferUnprotect(0, 0, display.getNumRows()-1, display.getNumColumns()-1));
							state = 0;
							break;
						default:
							System.out.println("Unknown ESC - " + (int)ch);
					}
					break;
				case 39:
					if (ch == CHAR_CR)
					{
						// Execute local program
						System.out.println("Execute local program " + accum);
						accum.setLength(0);
						state = 0;
						continue;
					}
					accum.append(ch);
					break;
				case 24:
					if (Character.isDigit(ch))
					{
						accum.append(ch);
						state = 25;
						continue;
					}
					switch (ch)
					{
						case ';':
							strStack.addElement(accum.toString());
							accum.setLength(0);
							state = 34;
							break;
						case 'd':
							// Read string configuration param
							System.out.println("Read string config param " + accum);
							accum.setLength(0);
							state = 0;
							break;
						case 'c':
							state = 30;
							break;
						default:
							System.out.println("Unknown ESC-" + accum.toString() + " " + (int)ch);
					}
					break;
				case 34:
					if (Character.isDigit(ch))
					{
						accum.append(ch);
						continue;
					}
					if (ch != ';')
					{
						System.out.println("Expected ';' in state 34; Got " + (int)ch);
					}
					strStack.addElement(accum.toString());
					accum.setLength(0);
					state = 35;
					break;
				case 35:
					if (Character.isDigit(ch))
					{
						accum.append(ch);
						continue;
					}
					switch (ch)
					{
						case 'C':
							// set buffer address extended
							display.setBufferRowCol(Integer.parseInt((String)strStack.elementAt(0)), Integer.parseInt(accum.toString()));
							strStack.clear();
							accum.setLength(0);
							state = 0;
							break;
						case 'q':
							strStack.addElement(accum.toString());
							strStack.addElement("q");
							accum.setLength(0);
							state = 36;
							break;
						case 'I':
							// Clear memory to spaces extended
							{
								int sr = ((String)strStack.elementAt(0)).charAt(0);
								int sc = ((String)strStack.elementAt(0)).charAt(1);
								int er = accum.charAt(0);
								int ec = accum.charAt(1);
								accum.setLength(0);
								strStack.clear();
								display.clearBlock(sr, sc, er, ec);
								state = 0;
							}
							break;
						case ';':
							strStack.addElement(accum.toString());
							state = 64;
							break;
						default:
							System.out.println("Unexpected char in 35: " + (int)ch);
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
							strStack.addElement("" + ch);
							state = 37;
							continue;
					}
					strStack.addElement(accum.toString());
					accum.setLength(0);
					{
						String p1;
						int p2, p3;
						p1 = (String)strStack.elementAt(0);
						if (p1.equals("0"))
						{
							// reset color map
							System.out.println("Reset color map");
						}
						else
						{
							System.out.println("Set color map");
							p2 = Integer.parseInt((String)strStack.elementAt(1));
							p3 = Integer.parseInt((String)strStack.elementAt(2));
							
							if (! ((String)strStack.elementAt(3)).equals("q"))
							{
								System.out.println("State 36 Error");
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
							strStack.addElement("" + ch);
							state = 36;
							continue;
					}
					System.out.println("Bad hex in state 37: " + (int)ch);
					break;
				case 30:
					if (ch > 31)
					{
						accum.append(ch);
						continue;
					}
					if (ch == 0x12)
					{
						strStack.addElement(accum.toString());
						accum.setLength(0);
						continue;
					}
					if (ch != CHAR_CR)
					{
						System.out.println("Expected CR in 30; Got " + (int)ch);
					}
					strStack.addElement(accum.toString());
					accum.setLength(0);
					{
						String count = (String)strStack.elementAt(0);
						for (int x = 1; x < strStack.size(); x++)
						{
							System.out.println("Parameter recived: " + (String)strStack.elementAt(x));
						}
					}
					strStack.clear();
					break;
				case 25:
					if (Character.isDigit(ch))
					{
						accum.append(ch);
						continue;
					}
					if (ch == ';')
					{
						strStack.addElement(accum.toString());
						accum.setLength(0);
						state = 27;
						continue;
					}
					System.out.println("Unexpected char in 25: " + (int)ch);
					break;
				case 27:
					if (Character.isDigit(ch))
					{
						accum.append(ch);
						continue;
					}
					if (ch == 'D')
					{
						// set cursor position
						int curx = Integer.parseInt((String)strStack.elementAt(0)) - 32;
						int cury = Integer.parseInt((String)strStack.elementAt(1)) - 32;
						display.setCursorRowCol(curx, cury);
					}
					else if (ch == 'O')
					{
						// write to AUX
						System.out.println("Write to AUX");
					}
					else
					{
						System.out.println("Unexpcted char in 27: " + (int)ch);
					}
					break;
				case 42:
					strStack.addElement("" + ch);
					state = 43;
					break;
				case 43:
					display.setCursorRowCol( (int)(((String)strStack.elementAt(0)).charAt(0)) - 0x20, (int)ch - 0x20);
					strStack.clear();
					state = 0;
					break;
				case 44:
					display.setWriteAttribute((int)ch & ~(1<<5));
					state = 0;
					break;
				case 46:
					display.setPriorWriteAttribute((int)ch & ~(1<<5));
					state = 0;
					break;
				case 48:
					display.setDisplayPage(ch - 0x20);
					state = 0;
					break;
				case 50:
					{
						byte[] fnKey = {1, (byte)ch, 0, 0, 13};
						fnKey[2] = (byte)display.getCursorRow();
						fnKey[3] = (byte)display.getCursorCol();
						telnet.send(fnKey);
					}
					state = 0;
					break;
				case 52:
					if (ch == 13)
					{
						display.writeMessage(accum.toString());
						accum.setLength(0);
						state = 0;
						continue;
					}
					accum.append(ch);
					break;
				case 54:
					if (ch == 13)
					{
						switch (accum.charAt(0))
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
								if (accum.charAt(1) == '0')
									keys.enterKeyOff();
								else
									keys.enterKeyOn();
								break;
							case 'T':
								// normal intensity
								break;
							case 'V':
								// character size
								break;
							default:
								System.out.println("State 54: " + accum);
								break;
						}
						accum.setLength(0);
						state = 0;
						continue;
					}
					accum.append("" + ch);
					break;
				case 56:
					strStack.addElement("" + ch);
					state = 57;
					break;
				case 57:
					display.setBufferRowCol( (int)((String)strStack.elementAt(0)).charAt(0) - 0x20, (int)ch - 0x20);
					strStack.clear();
					state = 0;
					break;
				case 59:
					strStack.addElement("" + ch);
					state = 60;
					break;
				case 60:
					display.startField( (int)((String)strStack.elementAt(0)).charAt(0) - 0x20, (int)ch - 0x20);
					strStack.clear();
					state = 0;
					break;
				case 64:
					if (Character.isDigit(ch))
					{
						accum.append(ch);
						continue;
					}
					switch(ch)
					{
						case 'J':
						case 'K':
							int sr = Integer.parseInt((String)strStack.elementAt(0));
							int sc = Integer.parseInt((String)strStack.elementAt(1));
							int er = Integer.parseInt((String)strStack.elementAt(2));
							int ec = Integer.parseInt(accum.toString());
							strStack.clear();
							accum.setLength(0);
							if (ASSERT.debug > 0)
								display.dumpScreen(Logger.out);
							telnet.send(display.readBufferAllIgnoreMdt(sr, sc, er, ec));
							break;
						default:
							System.out.println("Unexpected char in 64: " + (int)ch);
							break;
					}
					break;
				case 66:
					display.setPage(ch-0x20);
					state = 0;
					break;
				case 67:
					if (strStack.size() == 3)
					{
						int sr = ((String)strStack.elementAt(0)).charAt(0) - 0x20;
						int sc = ((String)strStack.elementAt(1)).charAt(0) - 0x20;
						int er = ((String)strStack.elementAt(2)).charAt(0) - 0x20;
						int ec = ch - 0x20;
						if (ASSERT.debug > 0)
							display.dumpScreen(Logger.out);
						telnet.send(display.readBufferAllMdt(sr, sc, er, ec));
						strStack.clear();
						state = 0;
						continue;
					}
					strStack.addElement("" + ch);
					break;
				case 71:
					strStack.addElement("" + ch);
					state = 72;
					break;
				case 72:
					strStack.addElement("" + ch);
					state = 73;
					break;
				case 73:
					{
						int vidAttr = ((String)strStack.elementAt(0)).charAt(0) - 0x20;
						int dataAttr = ((String)strStack.elementAt(1)).charAt(0) - 0x20;
						int keyAttr = ch - 0x20;
						display.startField(vidAttr, dataAttr, keyAttr);
					}
					strStack.clear();
					state = 0;
					break;
				case 75:
					strStack.addElement("" + ch);
					state = 76;
					break;
				case 76:
					strStack.addElement("" + ch);
					state = 77;
					break;
				case 77:
					if (ch != ';')
					{
						System.out.println("Expected ; in 77: " + (int)ch);
					}
					state = 78;
					break;
				case 78:
					strStack.addElement("" + ch);
					state = 79;
					break;
				case 79:
					{
						int sr = ((String)strStack.elementAt(0)).charAt(0) - 0x20;
						int sc = ((String)strStack.elementAt(1)).charAt(0) - 0x20;
						int er = ((String)strStack.elementAt(2)).charAt(0) - 0x20;
						int ec = ch - 0x20;
						telnet.send(display.readFieldsAll(sr, sc, er, ec));
					}
					strStack.clear();
					state = 0;
					break;
				case 81:
					display.setPageCount(((int)ch) - 0x30);
					state = 10000;
					break;
				case 82:
					strStack.addElement("" + (ch-0x20));
					state = 83;
					break;
				case 83:
					accum.append(ch);
					ch = (char)(((String)strStack.elementAt(0)).charAt(0) - 1);
					strStack.clear();
					if (ch == 0)
					{
						keys.setMap(13, 0, accum.toString());
						accum.setLength(0);
						state = 0;
					}
					else
					{
						strStack.addElement("" + ch);
					}
					break;
				case 84:
					// datatype table add ch
					if (++dataTypeTableCount == 96)
					{
						// set data type table
						state = 0;
						System.out.println("Set data type table");
					}
					break;
				case 10000:
					if (ch == 4)
					{
						state = 0;
						continue;
					}
					if (ch != 13) System.out.println("Guardian Expected 13 in 10000: " + (int)ch);
					state = 10001;
					break;
				case 10001:
					if (ch == 4)
					{
						state = 0;
						continue;
					}
					if (ch != 10) System.out.println("Guardian Expected 10 in 10001: " + (int)ch);
					state = 0;
					break;
				case 5000:
					switch (ch)
					{
						case 'A':
							// ANSI terminal mode
							break;
						case 'B':
							// BLOCK mode
							display.setModeBlock();
							keys.setKeySet(Keys.KEYS_BLOCK);
							break;
						case 'C':
							// Conversational mode
							display.setModeConv();
							keys.setKeySet(Keys.KEYS_CONV);
							break;
						case '!':
							// send term config?
							state = 5050;
							break;
						default:
							System.out.println("Unexpected char in 5000: " + (int)ch);
							break;
					}
					state = 5001;
					break;
				case 5001:
					if (ch != 3) System.out.println("Expected 3 in 5000: " + (int)ch);
					state = 0;
					break;
				case 5050:
					// accept chars until 3
					if (ch == 3)
					{
						System.out.println("Send term config? " + accum.toString());
						accum.setLength(0);
						state = 0;
						continue;
					}
					accum.append("" + ch);
					break;
				case 15000:
					// ESC I (Clear Block)
					if (strStack.size() == 3)
					{
						display.clearBlock(((String)strStack.elementAt(0)).charAt(0)-0x20, ((String)strStack.elementAt(1)).charAt(0)-0x20, ((String)strStack.elementAt(2)).charAt(0)-0x20, ch - 0x20);
						strStack.clear();
						state = 0;
						continue;
					}
					strStack.addElement("" + ch);
					break;
				default:
					System.out.println("Unknown state " + state);
					break;
			}
		}
		if (accum.length() > 0)
		{
			if (display.getProtectMode())
			{
				display.writeBuffer(accum.toString());
			}
			else
			{
				display.writeDisplay(accum.toString());
			}
			accum.setLength(0);
		}
	}
	
	/**
	 *  Process a key typed at the keyboard
	 */
	public void execLocalCommand(byte cmd)
	{
		display.writeLocal("" + (char)cmd);
		if (! display.getBlockMode())
		{
			if (cmd == 10)
			{
				display.writeLocal("" + (char)13);
				keyBuffer.append("" + (char)13);
				try
				{
					telnet.send(keyBuffer.toString().getBytes());
				}
				catch (IOException ioe)
				{
					Logger.log(ioe.toString());
				}
				keyBuffer.setLength(0);
			}
			else if (cmd == '\b')
			{
				keyBuffer.setLength(keyBuffer.length()-1);
			}
			else
			{
				keyBuffer.append("" + (char)cmd);
			}
		}
		display.setRePaint();
	}
	
	/**
	 *  This is an obsolete and unused checksum
	 */
	private void LRC(byte[] b)
	{
		int x;
		int ltx = 0;
		ASSERT.fatal(b[0] == 1, "Guardian", 868, "LRC Error");
		for (x = 2; b[x] != 3; x++)
		{
			ltx ^= b[x];
		}
		b[x+1] = (byte)(0xFF & ltx);
	}
}