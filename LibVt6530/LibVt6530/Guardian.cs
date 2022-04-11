using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using Maximum;
using Maximum.AnsiTerm;
using Maximum.Collections;

namespace LibVt6530
	{
	/**
	 *  Class to assist in command parsing
	 */
	class InputElementBuffer
	{
		const int SIZE_ELEM = 128;
		protected string[] m_elements = new string[SIZE_ELEM];
		protected int m_pos;

		public InputElementBuffer()
		{
			m_pos = 0;
		}

		public int Size()
		{
			return m_pos;
		}

		public void AddElement(string str)
		{
			Debug.Assert(m_pos < SIZE_ELEM);
			m_elements[m_pos++] = str;
		}

		public void AddElement(char c)
		{
			string buf = c.ToString();
			Debug.Assert(m_pos < SIZE_ELEM);
			m_elements[m_pos++] = buf;
		}

		public string ElementAt(int index)
		{
			Debug.Assert(index < SIZE_ELEM);
			return m_elements[index];
		}

		public void Clear()
		{
			for (int x = 0; x < m_pos; x++)
			{
				m_elements[x] = null;
			}
			m_pos = 0;
		}
	};

	/**
	 *	Tandem Guardian operating environment terminal
	 *  command interpreter.
	 */
	public class Guardian : Mode
	{	
		protected const char CHAR_ESC = (char)27;
		protected const char CHAR_BELL = (char)7;
		protected const char CHAR_BKSPACE = (char)8;
		protected const char CHAR_HTAB = (char)9;
		protected const char CHAR_LF = (char)10;
		protected const char CHAR_CR = (char)13;

		private InputElementBuffer m_strStack = new InputElementBuffer();

		/* accumulate characters for sending to the display */
		private StringBuilder m_accum = new StringBuilder();
		
		private StringBuilder m_keyBuffer = new StringBuilder();
		
		private StringBuilder m_blockBuf = new StringBuilder();

		/* the keyboard handler */
		private Keys m_keys;

		/* the socket IO and telnet line protocol handler */
		private TelnetConnection m_telnet;
		
		/* the abstract display.  characters are stored here,
		 * but doesn't actually render the text on-screen     */
		private TextDisplay m_display;
		
		/* The command interpreter is a state machine -- this is the state */
		private int m_state;

		/* from Vt6530 */
		Vector<TermEventListener> m_listeners = new Vector<TermEventListener>();

		public Guardian
		(
			Vector<TermEventListener> listeners, 
			TextDisplay display, 
			Keys keys, 
			TelnetConnection telnet
		) 
		{
			this.m_keys = keys;
			this.m_telnet = telnet;
			this.m_display = display;
			m_state = 0;
			m_listeners = listeners;
		}

		/*
		 *  Process incoming text from the host.
		 */
		public void ProcessRemoteString(byte[] inp, int inplen)
		{		
			int pos = 0;
			int dataTypeTableCount = 0;

			while (pos < inplen)
			{
				char ch = (char)inp[pos++];
				
				switch (m_state)
				{
					case 0:
						if (ch > 31)
						{
							m_accum.Append(ch);
							break;
						}
						if (m_accum.Length > 0)
						{
							if (m_display.GetProtectMode())
							{
								m_display.WriteBuffer(m_accum.ToString());
							}
							else
							{
								m_display.WriteDisplay(m_accum.ToString());
							}
							m_accum.Length = 0;
						}
						switch (ch)
						{
							case (char)0x00:
								break;
							case (char)0x01:
								// SOH
								m_state = 5000;
								break;
							case (char)0x04:
								// reset the line
								m_display.ResetMdt();
								DispatchResetLine();
								break;
							case (char)0x05:
								// ENQ
								DispatchEnquire();
								break;
							case (char)0x07:
								// BELL
								m_display.Bell();
								break;
							case (char)0x08:
								// Backspace
								m_display.Backspace();
								break;
							case (char)0x09:
								// HTab
								m_display.Tab();
								break;
							case (char)0x0A:
								// NL
								m_display.Linefeed();
								break;
							case (char)0x0D:
								// CR
								m_display.CarageReturn();
								break;
							case (char)0x0E:
								// shift out to G1 character set
								LogFile.SysWriteInfo("G1 char set");
								break;
							case (char)0x0F:
								// Shift in to G0 character set
								LogFile.SysWriteInfo("G0 char set");
								break;
							case (char)0x13:
								// set cursor address
								m_state = 42;
								break;
							case (char)0x1B:
								// ESC
								m_state = 1;
								break;
							case (char)0x11:
								// set buffer address (block mode)
								m_state = 56;
								break;
							case (char)0x1D:
								// start field
								m_state = 59;
								break;
							default:
								LogFile.SysWriteWarn("Unknown command char " + (int)ch);
								break;
						}
						break;
					case 1:
						// ESC
						switch (ch)
						{
							case '0':
								// print screen
								m_state = 0;
								m_display.PrintScreen();
								break;
							case '1':
								// Set tab at cursor location
								m_state = 0;
								m_display.SetTab();
								break;
							case '2':
								// Clear tab
								m_display.ClearTab();
								m_state = 0;
								break;
							case '3':
								// Clear all tabs
								m_display.ClearAllTabs();
								m_state = 0;
								break;
							case '6':
								// Set video attributes
								m_state = 44;
								break;
							case '7':
								// Set video prior condition register
								m_state = 46;
								break;
							case ';':
								// Display page
								m_state = 48;
								break;
							case '?':
								// Read terminal configuration
								if (m_display.GetBlockMode())
								{
									m_telnet.Send(((char)1).ToString() + "!A 2B72C 0D 0E 0F 0G 0H15I 3J 0L 0M 1N 0O 0P 0X 6S 0T 0U 1V 1W 1e 1f 0i 1h10" + ((char)3).ToString() + "\0");
								}
								else
								{
									m_telnet.Send(((char)1).ToString() + "!A 2B72C 0D 0E 0F 0G 0H15I 3J 0L 0M 1N 0O 0P 0X 6S 0T 0U 1V 1W 1e 1f 0i 1h10" + ((char)13).ToString());
								}
								m_state = 0;
								break;
							case '@':
								// Delay one second
								m_state = 0;
								break;
							case 'A':
								// Cursor up
								m_display.MoveCursorUp();
								m_state = 0;
								break;
							case 'C':
								// Cursor right
								m_display.MoveCursorRight();
								m_state = 0;
								break;
							case 'F':
								// Cursor home down
								m_display.End();
								m_state = 0;
								break;
							case 'H':
								// Cursor home
								m_display.Home();
								m_state = 0;
								break;
							case 'I':
								// Clear memory to spaces
								// NOTE: in protect mode, this gets a block arg
								if (m_display.GetProtectMode())
								{
									m_state = 15000;
									continue;
								}
								m_display.ClearPage();
								m_state = 0;
								break;
							case 'J':
								// Erase to end of page/memory
								m_display.ClearToEnd();
								m_state = 0;
								break;
							case 'K':
								// Erase to end of line/field
								m_display.ClearEOL();
								m_state = 0;
								break;
							case '^':
								// Read terminal status
								if (m_display.GetBlockMode())
								{
									byte[] status = {1, 63, 66, 70, 67, 67, 94, 64, 3, 0, 4};
									m_telnet.Send(status, 11);
								}
								else
								{
									byte[] status = {1, 63, 67, 70, 67, 13}; //67, 94, 64, 3, 0};
									m_telnet.Send(status, 6);
								}
								m_state = 0;
								break;
							case '_':
								// Read firmware revision level
								if (m_display.GetBlockMode())
								{
									LogFile.SysWriteInfo("Read firmware revision level");
								}
								else
								{
									byte[] status = {1, 35, 67, 48, 48, 84, 79, 67, 48, 48, 13};
									m_telnet.Send(status, 11);
								}
								m_state = 0;
								break;
							case 'a':
								// Read cursor address
								{
									byte[] cursorPos = {1, (byte)'_', (byte)'!', 0, 0, 13};
									cursorPos[3] = (byte)m_display.GetCursorRow();
									cursorPos[4] = (byte)m_display.GetCursorCol();
									m_telnet.Send(cursorPos, 6);
								}
								m_state = 0;
								break;
							case 'b':
								// Unlock keyboard
								m_keys.UnlockKeyboard();
								m_display.SetKeysUnlocked();
								m_state = 10000;
								DispatchUnlockKeyboard();
								break;
							case 'c':
								// Lock keyboard
								m_keys.LockKeyboard();
								m_display.SetKeysLocked();
								DispatchLockKeyboard();
								m_state = 0;
								break;
							case 'd':
								// Simulate function key
								m_keys.LockKeyboard();
								m_state = 50;
								break;
							case 'f':
								// Disconnect modem
								LogFile.SysWriteInfo("Disconnect modem");
								m_state = 0;
								break;
							case 'o':
								// Write to message field
								m_state = 52;
								break;
							case 'v':
								// set terminal configuration
								m_state = 54;
								break;
							case 'x':
								//Set IO device configuration
								LogFile.SysWriteWarn("Set IO device configuration");
								m_state = 0;
								break;
							case 'y':
								// Read IO device configuration
								LogFile.SysWriteWarn("Read IO device configuration");
								m_state = 0;
								break;
							case '{':
								// Write to file or device driver
								LogFile.SysWriteError("Write to file or device driver");
								m_state = 0;
								break;
							case '}':
								// Write/read to file or device driver
								LogFile.SysWriteError("Write/read to file or device driver");
								m_state = 0;
								break;
							case '-':
								// extended CSI sequence
								m_state = 3;
								break;
							case ':':
								// select page
								m_state = 66;
								break;
							case '<':
								// read buffer
								//if (ASSERT.debug > 0)
								//	display.dumpScreen(Logger.out);
								m_blockBuf.Length = 0;
								m_display.ReadBufferUnprotectIgnoreMdt(m_blockBuf, 0, 0, m_display.GetNumRows()-1, m_display.GetNumColumns()-1);
								m_telnet.Send(m_blockBuf.ToString());
								m_blockBuf.Length = 0;
								m_state = 0;
								break;
							case '=':
								// Read with address
								m_state = 67;
								break;
							case '>':
								// Reset modified data tags
								m_display.ResetMdt();
								m_state = 0;
								break;
							case 'L':
								m_display.LineDown();
								m_state = 0;
								break;
							case 'M':
								m_display.DeleteLine();
								m_state = 0;
								break;
							case 'N':
								// disable local line editing until
								// 1. ESC q
								// 2. Exit block mode
								// 3. protect to nonprotect submode
								LogFile.SysWriteError("Disable local line editing");
								m_state = 0;
								break;
							case 'O':
								// insert char
								m_display.InsertChar();
								m_state = 0;
								break;
							case 'P':
								// delete char
								m_display.DeleteChar();
								m_state = 0;
								break;
							case 'S':
								// roll up
								LogFile.SysWriteError("Roll up");
								m_state = 0;
								break;
							case 'T':
								// roll down
								m_state = 0;
								m_display.LineDown();
								break;
							case 'U':
								// page down
								LogFile.SysWriteError("Page down");
								m_state = 0;
								break;
							case 'V':
								// page up
								LogFile.SysWriteError("Page up");
								m_state = 0;
								break;
							case 'W':
								//  Enter protect mode
								m_display.SetProtectMode();
								m_keys.SetProtectMode();
								m_state = 0;
								break;
							case 'X':
								// exit protect mode
								m_display.ExitProtectMode();
								m_keys.ExitProtectMode();
								m_state = 0;
								break;
							case '[':
								// start field extended
								m_state = 71;
								break;
							case ']':
								// Read with address all
								if (m_display.GetProtectMode())
								{
									// same as ESC =
									m_state = 67;
									continue;
								}
								m_state = 75;
								break;
							case 'i':
								// back tab
								m_display.Backtab();
								m_state = 0;
								break;
							case 'p':
								// set max page num
								m_state = 81;
								break;
							case 'q':
								// reinitialize
								m_display.Init();
								m_display.SetProtectMode();
								m_display.ExitProtectMode();
								m_state = 10000;
								LogFile.SysWriteInfo("Reinitialize term");
								break;
							case 'r':
								// Define data type table
								m_state = 84;
								break;
							case 'u':
								// define enter key function
								m_state = 82;
								break;
							default:
								LogFile.SysWriteError("Unknown ESC " + (int)ch);
								m_state = 0;
								break;
						}
						break;
					case 3:
						if (Char.IsDigit(ch))
						{
							m_state = 24;
							m_accum.Append(ch);
							continue;
						}
						switch (ch)
						{
							case 'c':
								m_strStack.AddElement("7");
								m_state = 30;
								break;
							case 'e':
								// Get machine name 3-28
								{
									byte[] name = {1, (byte)'&', (byte)'j', (byte)'o', (byte)'h', (byte)'n', 13};
									m_telnet.Send(name, 7);
								}
								m_state = 0;
								break;
							case 'V':
								m_state = 39;
								break;
							case 'W':
								// Report Exec code 3-34
								{
									byte[] code = {1, (byte)'?', (1<<6) | 1, (byte)'F', (byte)'D', 13};
								}
								m_state = 0;
								break;
							case 'J':
								m_blockBuf.Length = 0;
								m_display.ReadBufferUnprotect(m_blockBuf, 0, 0, m_display.GetNumRows()-1, m_display.GetNumColumns()-1);
								m_telnet.Send(m_blockBuf.ToString());
								m_state = 0;
								break;
							default:
								LogFile.SysWriteError("Unknown ESC - " + (int)ch);
								break;
						}
						break;
					case 39:
						if (ch == CHAR_CR)
						{
							// Execute local program
							LogFile.SysWriteError("Execute local program " + m_accum);
							m_accum.Length = 0;
							m_state = 0;
							continue;
						}
						m_accum.Append(ch);
						break;
					case 24:
						if (Char.IsDigit(ch))
						{
							m_accum.Append(ch);
							m_state = 25;
							continue;
						}
						switch (ch)
						{
							case ';':
								m_strStack.AddElement(m_accum.ToString());
								m_accum.Length = 0;
								m_state = 34;
								break;
							case 'd':
								// Read string configuration param
								LogFile.SysWriteWarn("Read string config param " + m_accum);
								m_accum.Length = 0;
								m_state = 0;
								break;
							case 'c':
								m_state = 30;
								break;
							default:
								LogFile.SysWriteError("Unknown ESC-" + m_accum + " " + (int)ch);
								break;
						}
						break;
					case 34:
						if (Char.IsDigit(ch))
						{
							m_accum.Append(ch);
							continue;
						}
						if (ch != ';')
						{
							LogFile.SysWriteError("Expected ';' in state 34; Got " + (int)ch);
						}
						m_strStack.AddElement(m_accum.ToString());
						m_accum.Length = 0;
						m_state = 35;
						break;
					case 35:
						if (Char.IsDigit(ch))
						{
							m_accum.Append(ch);
							continue;
						}
						switch (ch)
						{
							case 'C':
								// set buffer address extended
								m_display.SetBufferRowCol(Int32.Parse(m_strStack.ElementAt(0)), Int32.Parse(m_accum.ToString()));
								m_strStack.Clear();
								m_accum.Length = 0;
								m_state = 0;
								break;
							case 'q':
								m_strStack.AddElement(m_accum.ToString());
								m_strStack.AddElement("q");
								m_accum.Length = 0;
								m_state = 36;
								break;
							case 'I':
								// Clear memory to spaces extended
								{
									int sr = m_strStack.ElementAt(0)[0];
									int sc = m_strStack.ElementAt(0)[1];
									int er = m_accum[0];
									int ec = m_accum[1];
									m_accum.Length = 0;
									m_strStack.Clear();
									m_display.ClearBlock(sr, sc, er, ec);
									m_state = 0;
								}
								break;
							case ';':
								m_strStack.AddElement(m_accum.ToString());
								m_state = 64;
								break;
							default:
								LogFile.SysWriteError("Unexpected char in 35: " + (int)ch);
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
								m_strStack.AddElement((char) ch);
								m_state = 37;
								continue;
						}
						m_strStack.AddElement(m_accum.ToString());
						m_accum.Length = 0;
						{
							string p1;
							int p2, p3;

							p1 = m_strStack.ElementAt(0);
							if (p1[0] == '0')
							{
								// reset color map
								LogFile.SysWriteWarn("Reset color map");
							}
							else
							{
								LogFile.SysWriteWarn("Set color map");
								p2 = Int32.Parse(m_strStack.ElementAt(1));
								p3 = Int32.Parse(m_strStack.ElementAt(2));
								
								if (m_strStack.ElementAt(3)[0] != 'q')
								{
									LogFile.SysWriteError("State 36 Error");
								}
								for (int x = 0; x < p3-p2; x++)
								{
									// setColorMap(p2+x, Integer.parseInt((String)strStack.elementAt(x*2+4), 16), Integer.parseInt((String)strStack.elementAt(x*2+5), 16) );
									LogFile.SysWriteWarn("SetColorMap(" + (p2 + x) + ", " + m_strStack.ElementAt(x * 2 + 4) + ", " + m_strStack.ElementAt(x * 2 + 5) + ");");
								}
							}
							m_strStack.Clear();
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
								m_strStack.AddElement((char) ch);
								m_state = 36;
								continue;
						}
						LogFile.SysWriteError("Bad hex in state 37: " + (int)ch);
						break;
					case 30:
						if (ch > 31)
						{
							m_accum.Append(ch);
							continue;
						}
						if (ch == 0x12)
						{
							m_strStack.AddElement(m_accum.ToString());
							m_accum.Length = 0;
							continue;
						}
						if (ch != CHAR_CR)
						{
							LogFile.SysWriteError("Expected CR in 30; Got " + (int)ch);
						}
						m_accum.Length = 0;
						{
							int count = Int32.Parse(m_strStack.ElementAt(0));
							for (int x = 1; x < m_strStack.Size(); x++)
							{
								LogFile.SysWriteWarn("Parameter recived: " + m_strStack.ElementAt(x));
							}
						}
						m_strStack.Clear();
						break;
					case 25:
						if (Char.IsDigit(ch))
						{
							m_accum.Append(ch);
							continue;
						}
						if (ch == ';')
						{
							m_strStack.AddElement(m_accum.ToString());
							m_accum.Length = 0;
							m_state = 27;
							continue;
						}
						LogFile.SysWriteError("Unexpected char in 25: " + (int)ch);
						break;
					case 27:
						if (Char.IsDigit(ch))
						{
							m_accum.Append(ch);
							continue;
						}
						if (ch == 'D')
						{
							// set cursor position
							int curx = m_strStack.ElementAt(0)[0] - 32;
							int cury = m_strStack.ElementAt(1)[0] - 32;
							m_display.SetCursorRowCol(curx, cury);
						}
						else if (ch == 'O')
						{
							// write to AUX
							LogFile.SysWriteWarn("Write to AUX");
						}
						else
						{
							LogFile.SysWriteError("Unexpcted char in 27: " + (int)ch);
						}
						break;
					case 42:
						m_strStack.AddElement((char) ch);
						m_state = 43;
						break;
					case 43:
						m_display.SetCursorRowCol( m_strStack.ElementAt(0)[0] - 0x20, (int)ch - 0x20);
						m_strStack.Clear();
						m_state = 0;
						break;
					case 44:
						m_display.SetWriteAttribute((int)ch & ~(1<<5));
						m_state = 0;
						break;
					case 46:
						m_display.SetPriorWriteAttribute((int)ch & ~(1<<5));
						m_state = 0;
						break;
					case 48:
						m_display.SetDisplayPage(ch - 0x20);
						LogFile.SysWriteInfo("SET DISPLAY PAGE " + (int)(ch - 0x20));
						m_state = 0;
						break;
					case 50:
						{
							byte[] fnKey = {1, (byte)ch, 0, 0, 13};
							fnKey[2] = (byte)m_display.GetCursorRow();
							fnKey[3] = (byte)m_display.GetCursorCol();
							m_telnet.Send(fnKey, 5);
						}
						m_state = 0;
						break;
					case 52:
						if (ch == 13)
						{
							m_display.WriteMessage(m_accum.ToString());
							m_accum.Length = 0;
							m_state = 0;
							continue;
						}
						m_accum.Append(ch);
						break;
					case 54:
						if (ch == 13)
						{
							switch (m_accum[0])
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
									if (m_accum[1] == '0')
									{
										m_keys.SetEnterKeyOff();
										LogFile.SysWriteInfo("Enter Key Mode OFF");
									}
									else
									{
										m_keys.SetEnterKeyOn();
										LogFile.SysWriteInfo("Enter Key Mode ON");
									}
									break;
								case 'T':
									// normal intensity
									break;
								case 'V':
									// character size
									break;
								default:
									LogFile.SysWriteError("State 54: " + m_accum);
									break;
							}
							m_accum.Length = 0;
							m_state = 0;
							continue;
						}
						m_accum.Append((char) ch);
						break;
					case 56:
						m_strStack.AddElement((char) ch);
						m_state = 57;
						break;
					case 57:
						m_display.SetBufferRowCol( m_strStack.ElementAt(0)[0] - 0x20, ch - 0x20);
						m_strStack.Clear();
						m_state = 0;
						break;
					case 59:
						m_strStack.AddElement((char) ch);
						m_state = 60;
						break;
					case 60:
						m_display.StartField( m_strStack.ElementAt(0)[0] - 0x20, ch - 0x20);
						m_strStack.Clear();
						m_state = 0;
						break;
					case 64:
						if (Char.IsDigit(ch))
						{
							m_accum.Append(ch);
							continue;
						}
						switch(ch)
						{
							case 'J':
							case 'K':
								{
									int sr = Int32.Parse(m_strStack.ElementAt(0));
									int sc = Int32.Parse(m_strStack.ElementAt(1));
									int er = Int32.Parse(m_strStack.ElementAt(2));
									int ec = Int32.Parse(m_accum.ToString());
									m_strStack.Clear();
									m_accum.Length = 0;
									m_blockBuf.Length = 0;
									m_display.ReadBufferAllIgnoreMdt(m_blockBuf, sr, sc, er, ec);
									m_telnet.Send(m_blockBuf.ToString());
									m_blockBuf.Length = 0;
								}
								break;
							default:
								LogFile.SysWriteError("Unexpected char in 64: " + (int)ch);
								break;
						}
						break;
					case 66:
						m_display.SetPage(ch-0x20);
						m_state = 0;
						break;
					case 67:
						if (m_strStack.Size() == 3)
						{
							int sr = m_strStack.ElementAt(0)[0] - 0x20;
							int sc = m_strStack.ElementAt(1)[0] - 0x20;
							int er = m_strStack.ElementAt(2)[0] - 0x20;
							int ec = ch - 0x20;
							m_blockBuf.Length = 0;
							m_display.ReadBufferAllMdt(m_blockBuf, sr, sc, er, ec);
							m_telnet.Send(m_blockBuf.ToString());
							m_blockBuf.Length = 0;
							m_strStack.Clear();
							m_state = 0;
							continue;
						}
						m_strStack.AddElement((char) ch);
						break;
					case 71:
						m_strStack.AddElement((char) ch);
						m_state = 72;
						break;
					case 72:
						m_strStack.AddElement((char) ch);
						m_state = 73;
						break;
					case 73:
						{
							int vidAttr = m_strStack.ElementAt(0)[0] - 0x20;
							int dataAttr = m_strStack.ElementAt(1)[0] - 0x20;
							int keyAttr = ch - 0x20;
							m_display.StartField(vidAttr, dataAttr, keyAttr);
						}
						m_strStack.Clear();
						m_state = 0;
						break;
					case 75:
						m_strStack.AddElement(ch);
						m_state = 76;
						break;
					case 76:
						m_strStack.AddElement(ch);
						m_state = 77;
						break;
					case 77:
						if (ch != ';')
						{
							LogFile.SysWriteError("Expected ; in 77: " + (int)ch);
						}
						m_state = 78;
						break;
					case 78:
						m_strStack.AddElement((char) ch);
						m_state = 79;
						break;
					case 79:
						{
							int sr = m_strStack.ElementAt(0)[0] - 0x20;
							int sc = m_strStack.ElementAt(1)[0] - 0x20;
							int er = m_strStack.ElementAt(2)[0] - 0x20;
							int ec = ch - 0x20;
							m_blockBuf.Length = 0;
							m_display.ReadFieldsAll(m_blockBuf, sr, sc, er, ec);
							m_telnet.Send(m_blockBuf.ToString());
							m_blockBuf.Length = 0;
						}
						m_strStack.Clear();
						m_state = 0;
						break;
					case 81:
						m_display.SetPageCount(((int)ch) - 0x30);
						m_state = 10000;
						break;
					case 82:
						m_strStack.AddElement((char)(ch-0x20));
						m_state = 83;
						break;
					case 83:
						m_accum.Append(ch);
						ch = (char)(((int)(m_strStack.ElementAt(0)[0])) - 1);
						m_strStack.Clear();
						if (ch == 0)
						{
							//keys.setMap(13, 0, accum);
							LogFile.SysWriteWarn("keys.setMap(13, 0, accum);");
							m_accum.Length = 0;
							m_state = 0;
						}
						else
						{
							m_strStack.AddElement((char)ch);
						}
						break;
					case 84:
						// datatype table add ch
						if (++dataTypeTableCount == 96)
						{
							// set data type table
							m_state = 0;
							LogFile.SysWriteError("Set data type table");
						}
						break;
					case 10000:
						if (ch == 4)
						{
							m_state = 0;
							continue;
						}
						if (ch != 13) 
						{
							LogFile.SysWriteError("Guardian Expected 13 in 10000: " + (int)ch);
						}
						m_state = 10001;
						break;
					case 10001:
						if (ch == 4)
						{
							m_state = 0;
							continue;
						}
						if (ch != 10)
						{
							LogFile.SysWriteError("Guardian Expected 10 in 10001: " + (int)ch);
						}
						m_state = 0;
						break;
					case 5000:
						switch (ch)
						{
							case 'A':
								// ANSI terminal mode
								//telnet.setBufferingOff();
								LogFile.SysWriteError("ANSI MODE");
								break;
							case 'B':
								// BLOCK mode
								m_display.SetModeBlock();
								m_keys.SetKeySet(KeyMode.KEYS_BLOCK);
								//telnet.setBufferingOn();
								LogFile.SysWriteInfo("BLOCK MODE");
								break;
							case 'C':
								// Conversational mode
								m_display.SetModeConv();
								m_keys.SetKeySet(KeyMode.KEYS_CONV);
								//telnet.setBufferingOff();
								LogFile.SysWriteWarn("CONVERSATIONAL MODE");
								break;
							case '!':
								// send term config?
								m_state = 5050;
								LogFile.SysWriteWarn("send term config?");
								break;
							default:
								LogFile.SysWriteError("Unexpected char in 5000: " + (int)ch);
								break;
						}
						m_state = 5001;
						break;
					case 5001:
						if (ch != 3) 
						{
							LogFile.SysWriteError("Expected 3 in 5000: " + (int)ch);
						}
						m_state = 0;
						break;
					case 5050:
						// accept chars until 3
						if (ch == 3)
						{
							LogFile.SysWriteWarn("Send term config? " + m_accum);
							m_accum.Length = 0;
							m_state = 0;
							continue;
						}
						m_accum.Append((char) ch);
						break;
					case 15000:
						// ESC I (Clear Block)
						if (m_strStack.Size() == 3)
						{
							m_display.ClearBlock(m_strStack.ElementAt(0)[0]-0x20, m_strStack.ElementAt(1)[0]-0x20, m_strStack.ElementAt(2)[0]-0x20, ch - 0x20);
							m_strStack.Clear();
							m_state = 0;
							continue;
						}
						m_strStack.AddElement((char) ch);
						break;
					default:
						LogFile.SysWriteError("Unknown state " + (int)m_state);
						break;
				}
			}
			if (m_accum.Length > 0)
			{
				if (m_display.GetProtectMode())
				{
					m_display.WriteBuffer(m_accum.ToString());
				}
				else
				{
					m_display.WriteDisplay(m_accum.ToString());
				}
				m_accum.Length = 0;
			}
		}

		/*
		 *  Process text in local edit mode.
		 */

		public void ExecLocalCommand(char cmd)
		{
			m_display.WriteLocal(cmd.ToString());

			if (! m_display.GetBlockMode())
			{
				if (cmd == 13 || cmd == 10)
				{
					m_display.WriteLocal(((char)10).ToString());
					m_keyBuffer.Append((char)13);
					m_telnet.Send(m_keyBuffer.ToString());
					m_keyBuffer.Length = 0;
				}
				else if (cmd == '\b' && m_keyBuffer.Length > 0)
				{
					m_keyBuffer.Length = m_keyBuffer.Length-1;
				}
				else
				{
					m_keyBuffer.Append(cmd);
				}
			}
			m_display.SetRePaint(true);
		}

		public virtual bool IsConvMode() 
		{ 
			return !m_display.IsBlockMode(); 
		}

		/**
		 *  The host has completed transmision and is
		 *  now waiting for input.  This can be used
		 *  to buffer keystrokes until the screen is
		 *  fully m_displayed.
		 */
		public void DispatchEnquire()
		{
			for (int x = 0; x < m_listeners.Count; x++)
			{
				m_listeners.ElementAt(x).TermOnEnquire();
			}
		}

		public void DispatchResetLine()
		{
			for (int x = 0; x < m_listeners.Count; x++)
			{
				m_listeners.ElementAt(x).TermOnResetLine();
			}
		}

		protected void DispatchUnlockKeyboard()
		{
			for (int x = 0; x < m_listeners.Count; x++)
			{
				m_listeners.ElementAt(x).TermOnUnlockKeyboard();
			}
		}

		protected void DispatchLockKeyboard()
		{
			for (int x = 0; x < m_listeners.Count; x++)
			{
				m_listeners.ElementAt(x).TermOnLockKeyboard();
			}
		}
	}
}
