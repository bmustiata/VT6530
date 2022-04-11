using System;
using System.Collections.Generic;
using System.Text;

namespace LibVt6530
{
	public enum KeyMode
	{
		KEYS_ANSI = 0,
		KEYS_CONV = 1,
		KEYS_BLOCK = 2
	}

	enum ExtKeys
	{
		SPC_F1 = 0,
		SPC_F2 = 1,
		SPC_F3 = 2,
		SPC_F4 = 3,
		SPC_F5 = 4,
		SPC_F6 = 5,
		SPC_F7 = 6,
		SPC_F8 = 7,
		SPC_F9 = 11,
		SPC_F10 = 12,
		SPC_F11 = 14,
		SPC_F12 = 15,
		SPC_BREAK = 16,
		SPC_PGUP = 17,
		SPC_PGDN = 18,
		SPC_HOME = 19,
		SPC_END = 20,
		SPC_INS = 21,
		SPC_DEL = 22,
		SPC_SCROLLOCK = 23,
		SPC_UP = 24,
		SPC_DOWN = 25,
		SPC_LEFT = 26,
		SPC_RIGHT = 28,
		SPC_PRINTSCR = 29,
		LAST_SPC = 30
	}

	public class Keys
	{
		const char TDM_SOH = ((char)1);
		const char TDM_ENQUIRY = ((char)5);
		const char TDM_ACK =  ((char)6);
		const char TDM_BELL = ((char)7);
		static string TDM_BACKSPACE = "\b";
		static string TDM_NAK = ((char)21).ToString();
		static string TDM_ESC = ((char)27).ToString();
		const char TDM_CR =  ((char)13);

		protected bool m_sendCursorWithFn;

		protected char m_pressedKey;
		protected long m_pressedWhen;

		protected string[] m_plain;
		protected string[] m_crtl;
		protected string[] m_alt;
		protected string[] m_shift;

		protected int[] m_localCmd;
		protected int[] m_localCmdCtl;
		protected int[] m_localCmdAlt;

		protected bool m_ignoreKeys;
		protected bool m_protectMode;
		protected bool m_enterKeyOn;

		protected MappedKeyListener m_listener;

		public Keys()
		{
			SetKeySet(KeyMode.KEYS_CONV);
			m_ignoreKeys = false;
			m_protectMode = false;
			m_enterKeyOn = true;
			m_sendCursorWithFn = false;
			m_pressedKey = ' ';
			m_pressedWhen = 0;
		}

		public void SetProtectMode()
		{
			LockKeyboard();
			m_protectMode = true;
			// disable local line editing
		}
		
		public void ExitProtectMode()
		{
			UnlockKeyboard();
			m_protectMode = false;
			// enable local line editing
		}
		
		public void SetEnterKeyOn()
		{
			m_enterKeyOn = true;
		}
		
		public void SetEnterKeyOff()
		{
			m_enterKeyOn = false;
		}
		
		public void SetListener(MappedKeyListener listener)
		{
			this.m_listener = listener;
		}
				
		//public void setMap(int ch, int modifier, char *out);
		
		public void KeyPressed( int keycode, bool shift, bool ctrl, bool alt ) 
		{
		}
				
		public void KeyTyped(  int keycode, bool shift, bool ctrl, bool alt )
		{
			m_pressedKey = (char)keycode;
			KeyAction(false, keycode, shift, ctrl, alt);
		}
				
		public void LockKeyboard()
		{
			m_ignoreKeys = true;
		}
		
		public void UnlockKeyboard()
		{
			m_ignoreKeys = false;
		}
		
		public void SetCrLfOn()
		{
		}
		
		public void SetCrLfOff()
		{
		}

		public void SetKeySet(KeyMode keySet)
		{
			if (keySet == KeyMode.KEYS_ANSI)
			{
				m_plain = ansiChar;
				m_crtl = ansiCharCtl;
				m_alt = ansiCharAlt;
				m_shift = ansiCharShift;
				m_localCmd = ansiLocal;
				m_localCmdCtl = ansiLocalCtl;
				m_localCmdAlt = ansiLocalAlt;
				m_sendCursorWithFn = false;
				m_protectMode = false;
				m_enterKeyOn = true;
			}
			else if (keySet == KeyMode.KEYS_CONV)
			{
				m_plain = convChar;
				m_crtl = convCharCtl;
				m_alt = convCharAlt;
				m_shift = convCharShift;
				m_localCmd = convLocal;
				m_localCmdCtl = convLocalCtl;
				m_localCmdAlt = convLocalAlt;
				m_sendCursorWithFn = true;
				m_protectMode = false;
				m_enterKeyOn = true;
			}
			else if (keySet == KeyMode.KEYS_BLOCK)
			{
				m_plain = blockChar;
				m_crtl = blockCharCtl;
				m_alt = blockCharAlt;
				m_shift = blockCharShift;
				m_localCmd = blockLocal;
				m_localCmdCtl = blockLocalCtl;
				m_localCmdAlt = blockLocalAlt;
				m_sendCursorWithFn = true;
				m_protectMode = false;
				m_enterKeyOn = true;
			}
		}

		public void KeyReleased( int keycode, bool shift, bool ctrl, bool alt )
		{
			bool fn = false;
			
			switch (keycode)
			{
				case (int)ExtKeys.SPC_F1:
					m_pressedKey = (char)ExtKeys.SPC_F1;
					fn = true;
					break;
				case (int)ExtKeys.SPC_F2:
					m_pressedKey = (char)ExtKeys.SPC_F2;
					fn = true;
					break;
				case (int)ExtKeys.SPC_F3:
					m_pressedKey = (char)ExtKeys.SPC_F3;
					fn = true;
					break;
				case (int)ExtKeys.SPC_F4:
					m_pressedKey = (char)ExtKeys.SPC_F4;
					fn = true;
					break;
				case (int)ExtKeys.SPC_F5:
					m_pressedKey = (char)ExtKeys.SPC_F5;
					fn = true;
					break;
				case (int)ExtKeys.SPC_F6:
					m_pressedKey = (char)ExtKeys.SPC_F6;
					fn = true;
					break;
				case (int)ExtKeys.SPC_F7:
					m_pressedKey = (char)ExtKeys.SPC_F7;
					fn = true;
					break;
				case (int)ExtKeys.SPC_F8:
					m_pressedKey = (char)ExtKeys.SPC_F8;
					fn = true;
					break;
				case (int)ExtKeys.SPC_F9:
					m_pressedKey = (char)ExtKeys.SPC_F9;
					fn = true;
					break;
				case (int)ExtKeys.SPC_F10:
					m_pressedKey = (char)ExtKeys.SPC_F10;
					fn = true;
					break;
				case (int)ExtKeys.SPC_F11:
					m_pressedKey = (char)ExtKeys.SPC_F11;
					fn = true;
					break;
				case (int)ExtKeys.SPC_F12:
					m_pressedKey = (char)ExtKeys.SPC_F12;
					fn = true;
					break;
				case (int)ExtKeys.SPC_HOME:
					//keycode = SPC_HOME;
					KeyAction(false, keycode, shift, ctrl, alt);
					break;
				case (int)ExtKeys.SPC_INS:
					//keycode = SPC_INS;
					KeyAction(false, keycode, shift, ctrl, alt);
					break;
				case (int)ExtKeys.SPC_DEL:
					//keycode = SPC_DEL;
					KeyAction(false, keycode, shift, ctrl, alt);
					break;
				case (int)ExtKeys.SPC_DOWN:
					//keycode = SPC_DOWN;
					KeyAction(false, keycode, shift, ctrl, alt);
					break;
				case (int)ExtKeys.SPC_END:
					//keycode = SPC_END;
					KeyAction(false, keycode, shift, ctrl, alt);
					break;
				case (int)ExtKeys.SPC_LEFT:
					//keycode = SPC_LEFT;
					KeyAction(false, keycode, shift, ctrl, alt);
					break;
				case (int)ExtKeys.SPC_PGDN:
					//keycode = SPC_PGDN;
					KeyAction(false, keycode, shift, ctrl, alt);
					break;
				case (int)ExtKeys.SPC_PGUP:
					//keycode = SPC_PGUP;
					KeyAction(false, keycode, shift, ctrl, alt);
					break;
				case (int)ExtKeys.SPC_PRINTSCR:
					//keycode = SPC_PRINTSCR;
					KeyAction(false, keycode, shift, ctrl, alt);
					break;
				case (int)ExtKeys.SPC_RIGHT:
					//keycode = SPC_RIGHT;
					KeyAction(false, keycode, shift, ctrl, alt);
					break;
				case (int)ExtKeys.SPC_UP:
					//keycode = SPC_UP;
					KeyAction(false, keycode, shift, ctrl, alt);
					break;
				case (int)ExtKeys.SPC_SCROLLOCK:
					//keycode = SPC_SCROLLOCK;
					KeyAction(false, keycode, shift, ctrl, alt);
					break;
			}
			if (fn)
			{
				KeyAction(fn, keycode, shift, ctrl, alt);
			}
		}

		public void KeyAction(bool fn, int keycode, bool shift, bool ctrl, bool alt)
		{
			StringBuilder sb = new StringBuilder();

			if (ctrl)
			{
				if (fn)
				{
					if (m_sendCursorWithFn)
					{
						if (m_protectMode)
						{
							return;						
						}
						else
						{
							return;
						}
					}
				}
				if (m_localCmdCtl[m_pressedKey] != 0)
				{
					NotifyListener((char)m_localCmdCtl[m_pressedKey]);
					return;
				}
				NotifyListener(m_crtl[m_pressedKey]);
			}
			else if (shift)
			{
				if (fn)
				{
					if (m_sendCursorWithFn)
					{
						if (m_protectMode)
						{
							sb.Append((char)1);
							sb.Append(shiftFn[m_pressedKey]);
							sb.Append((char)(m_listener.KeyGetPage() + 0x20));
							m_listener.KeyGetStartFieldASCII(sb);
							sb.Append(((char)3));
							sb.Append(((char)0));
							NotifyListener(sb.ToString());
							return;						
						}
						else
						{
							sb.Append((char)1);
							sb.Append(shiftFn[m_pressedKey]);
							sb.Append((char)(m_listener.KeyGetCursorX() + 0x20));
							sb.Append((char)(m_listener.KeyGetCursorY() + 0x20));
							sb.Append(((char)13));
							NotifyListener(sb.ToString());
							return;
						}
					}
				}
				if (m_localCmd[m_pressedKey] != 0)
				{
					NotifyListener((char)m_localCmd[m_pressedKey]);
					return;
				}
				NotifyListener(m_shift[m_pressedKey]);
			}
			else if (alt)
			{
				if (fn)
				{
					if (m_sendCursorWithFn)
					{
						if (m_protectMode)
						{
							return;						
						}
						else
						{
							return;
						}
					}
				}
				if (m_localCmdAlt[m_pressedKey] != 0)
				{
					NotifyListener((char)m_localCmdAlt[m_pressedKey]);
					return;
				}
				NotifyListener(m_alt[m_pressedKey]);
			}
			else
			{
				if (fn)
				{
					if (m_sendCursorWithFn)
					{
						if (m_protectMode)
						{
						//	notifyListener((char)1 + plainFn[pressedKey] + (char)(listener.getPage() + 0x20) + listener.getStartFieldASCII() + ((char)3) + "" + ((char)0));
						//	return;						
						//}
						//else
						//{
						//	notifyListener((char)1 + plainFn[pressedKey] + (char)(listener.getCursorX() + 0x20) + ""   + (char)(listener.getCursorY() + 0x20) + "" + ((char)13));
						//	return;
							sb.Append((char)1);
							sb.Append(plainFn[m_pressedKey]);
							sb.Append((char)(m_listener.KeyGetPage() + 0x20));
							m_listener.KeyGetStartFieldASCII(sb);
							sb.Append(((char)3));
							sb.Append(((char)0));
							NotifyListener(sb.ToString());
							return;						
						}
						else
						{
							sb.Append((char)1);
							sb.Append(plainFn[m_pressedKey]);
							sb.Append((char)(m_listener.KeyGetCursorX() + 0x20));
							sb.Append((char)(m_listener.KeyGetCursorY() + 0x20));
							sb.Append(((char)13));
							NotifyListener(sb.ToString());
							return;
						}
					}
				}
				if (m_pressedKey == 13 && m_enterKeyOn && m_protectMode)
				{
					sb.Append((char)1);
					sb.Append("V");
					sb.Append((char)(m_listener.KeyGetPage() + 0x20));
					sb.Append((char)(m_listener.KeyGetCursorX() + 0x20));
					sb.Append((char)(m_listener.KeyGetCursorY() + 0x20));
					sb.Append((char)3);
					sb.Append((char)0);
					NotifyListener(sb.ToString());
				}
				if (m_localCmd[m_pressedKey] != 0)
				{
					NotifyListener((char)m_localCmd[m_pressedKey]);
					return;
				}
				NotifyListener(m_plain[m_pressedKey]);
			}
		}

	
		private void NotifyListener(char b)
		{
			if (! m_ignoreKeys)
			{
				m_listener.KeyCommand(b);
			}
		}
		
		private void NotifyListener(string str)
		{
			if (! m_ignoreKeys)
			{
				m_listener.KeyMappedKey(str);
			}
		}

		protected static string[] ansiChar = {
							TDM_ESC + "@", TDM_ESC + "A", TDM_ESC + "B", TDM_ESC + "C", TDM_ESC + "D",
							TDM_ESC + "E", TDM_ESC + "F", TDM_ESC + "G", "\b", "\t", 
							"\r", TDM_ESC + "H", TDM_ESC + "I", "\n", TDM_ESC + "J",
							TDM_ESC + "[K", "0", TDM_ESC + "[U", TDM_ESC + "[H", TDM_ESC + "[" + ((char)24).ToString() + "H", 
							TDM_ESC + "[@", TDM_ESC + "[P", "", TDM_ESC + "[A", TDM_ESC + "[B", 
							TDM_ESC + "[D", TDM_ESC + "[C", TDM_ESC, "\0", "\0", 
							((char)30).ToString(), ((char)31).ToString(), " ", "!", "\"", 
							"#", "$", "%", "&", "\"", "(", ")", "*", "+", ",", 
							"-", ".", "/", "0", "1", "2", "3", "4", "5", "6", 
							"7", "8", "9", ":", ";", "<", "=", ">", "?", "@", 
							"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", 
							"K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", 
							"U", "V", "W", "X", "Y", "Z", "[", "\\", "]", "^", 
							"_", "`", "a", "b", "c", "d", "e", "f", "g", "h", 
							"i", "j", "k", "l", "m", "n", "o", "p", "q", "r", 
							"s", "t", "u", "v", "w", "x", "y", "z", "{", "|", 
							"}", "~"
							};

		 protected static string[] ansiCharCtl = {
							"", "", "", "", "", "", "", "", "", 
							  "", "", "", "", "", "", "", "", "",
							"", "", "", "", "", "", "", "","", 
							"", "", "", "","", " ", "", "","", "", "", 
							"", "", "", "", "", "", "", "", "", "", "", 
							"", "\0", "", "", "", "", "", "", "", "", "", 
							"", "", "", "", "\0", ((char)1).ToString(), ((char)2).ToString(), ((char)3).ToString(), ((char)4).ToString(), ((char)5).ToString(), ((char)6).ToString(), 
							((char)7).ToString(), "", "\t", ((char)10).ToString(), ((char)11).ToString(), ((char)12).ToString(), ((char)13).ToString(), ((char)14).ToString(), ((char)15).ToString(), ((char)16).ToString(), ((char)17).ToString(), 
							((char)18).ToString(), ((char)19).ToString(), ((char)20).ToString(), ((char)21).ToString(), ((char)22).ToString(), ((char)23).ToString(), ((char)24).ToString(), ((char)25).ToString(), ((char)26).ToString(), ((char)27).ToString(), ((char)28).ToString(), 
							((char)29).ToString(), ((char)30).ToString(), "", "", ((char)1).ToString(), ((char)2).ToString(), ((char)3).ToString(), ((char)4).ToString(), ((char)5).ToString(), ((char)6).ToString(), ((char)7).ToString(), 
							"", "\t", ((char)10).ToString(), ((char)11).ToString(), ((char)12).ToString(), ((char)13).ToString(), ((char)14).ToString(), ((char)15).ToString(), ((char)16).ToString(), ((char)17).ToString(), ((char)18).ToString(), 
							((char)19).ToString(), ((char)20).ToString(), ((char)21).ToString(), ((char)22).ToString(), ((char)23).ToString(), ((char)24).ToString(), ((char)25).ToString(), ((char)26).ToString(), "", "", "", 
							"", "" 
							};
		protected static string[] ansiCharAlt = {
							"", "", "", "", "", "", "", "", "", 
							  "", "", "", "", "", "", "", "", "",
							"", "", "", "", "", "", "", "","", 
							"", "", "", "","", " ", "", "","", "", "", 
							"", "", "", "", "", "", "", "", "", "", "", 
							"", "", "", "", "", "", "", "", "", "", "", 
							"", "", "", "", "", "", "", "", "", "", "", 
							"", "", "", "", "", "", "", "", "", "", "", 
							"", "", "", "", "", "", "", "", "", "", "", 
							"", "", "", "", "", "", "", "", "", "", "", 
							"", "", "", "", "", "", "", "", "", "", "", 
							"", "", "", "", "", "", "", "", "", "", "", 
							"", "" 
							};
		protected static string[] ansiCharShift = {
							TDM_ESC + "@", TDM_ESC + "A", TDM_ESC + "B", TDM_ESC + "C", TDM_ESC + "D",
							TDM_ESC + "E", TDM_ESC + "F", TDM_ESC + "G", "\b", "\t", 
							"\r", TDM_ESC + "H", TDM_ESC + "I", "\n", TDM_ESC + "J",
							TDM_ESC + "[K", "0", TDM_ESC + "[U", TDM_ESC + "[H", TDM_ESC + "[" + ((char)24).ToString() + "H", 
							TDM_ESC + "[@", TDM_ESC + "[P", "", TDM_ESC + "[A", TDM_ESC + "[B", 
							TDM_ESC + "[D", TDM_ESC + "[C", TDM_ESC, "\0", "\0", 
							((char)30).ToString(), ((char)31).ToString(), " ", "!", "\"", 
							"#", "$", "%", "&", "\"", "(", ")", "*", "+", ",", 
							"-", ".", "/", "0", "1", "2", "3", "4", "5", "6", 
							"7", "8", "9", ":", ";", "<", "=", ">", "?", "@", 
							"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", 
							"K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", 
							"U", "V", "W", "X", "Y", "Z", "[", "\\", "]", "^", 
							"_", "`", "a", "b", "c", "d", "e", "f", "g", "h", 
							"i", "j", "k", "l", "m", "n", "o", "p", "q", "r", 
							"s", "t", "u", "v", "w", "x", "y", "z", "{", "|", 
							"}", "~"
							};
		protected static int[] ansiLocal = {
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0
							};
		protected static int[] ansiLocalCtl = {
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0
							};
		protected static int[] ansiLocalAlt = {
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0
							};

		protected static string[] convChar = {
							"@", "A", "B", "C", "D",
							"E", "F", "G",  "\b", "\t",
							"\r", "H", "I", "\n", "J", 
							"K", ((char)16).ToString(), ((char)17).ToString(), "\0", "\0", 
							((char)20).ToString(), TDM_NAK, ((char)22).ToString(), ((char)23).ToString(), ((char)11).ToString(), "\r", "\b", TDM_ESC, "\t", "\0", 
							((char)30).ToString(), ((char)31).ToString(), " ", "!", "\"", "#", "$", "%", "&", "\"", 
							"(", ")", "*", "+", ",", "-", ".", "/", "0", "1", 
							"2", "3", "4", "5", "6", "7", "8", "9", ":", ";", "<", 
							"=", ">", "?", "@", "A", "B", "C", "D", "E", "F", "G", 
							"H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", 
							"S", "T", "U", "V", "W", "X", "Y", "Z", "[", "\\", "]", 
							"^", "_", "`", "a", "b", "c", "d", "e", "f", "g", "h", 
							"i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", 
							"t", "u", "v", "w", "x", "y", "z", "{", "|", "}", "~", 
							((char)127).ToString()
							};
		protected static string[] convCharCtl = {
							"", "", "", "", "", "", "", "", "", 
							  "", "", "", "", "", "", "", "", "",
							"", "", "", "", "", "", "", "","", 
							"", "", "", "","", " ", "", "","", "", "", 
							"", "", "", "", "", "", "", "", "", "", "", 
							"", "\0", "", "", "", "", "", "", "", "", "", 
							"", "", "", "", "\0", ((char)1).ToString(), ((char)2).ToString(), ((char)3).ToString(), ((char)4).ToString(), ((char)5).ToString(), ((char)6).ToString(), 
							((char)7).ToString(), "", "\t", ((char)10).ToString(), ((char)11).ToString(), ((char)12).ToString(), ((char)13).ToString(), ((char)14).ToString(), ((char)15).ToString(), ((char)16).ToString(), ((char)17).ToString(), 
							((char)18).ToString(), ((char)19).ToString(), ((char)20).ToString(), ((char)21).ToString(), ((char)22).ToString(), ((char)23).ToString(), ((char)24).ToString(), ((char)25).ToString(), ((char)26).ToString(), ((char)27).ToString(), ((char)28).ToString(), 
							((char)29).ToString(), ((char)30).ToString(), "", "", ((char)1).ToString(), ((char)2).ToString(), ((char)3).ToString(), ((char)4).ToString(), ((char)5).ToString(), ((char)6).ToString(), ((char)7).ToString(), 
							"", "\t", ((char)10).ToString(), ((char)11).ToString(), ((char)12).ToString(), ((char)13).ToString(), ((char)14).ToString(), ((char)15).ToString(), ((char)16).ToString(), ((char)17).ToString(), ((char)18).ToString(), 
							((char)19).ToString(), ((char)20).ToString(), ((char)21).ToString(), ((char)22).ToString(), ((char)23).ToString(), ((char)24).ToString(), ((char)25).ToString(), ((char)26).ToString(), "", "", "", 
							"", "" 
							};
		protected string[] convCharAlt = {
							"", "", "", "", "", "", "", "", "", 
							  "", "", "", "", "", "", "", "", "",
							"", "", "", "", "", "", "", "","", 
							"", "", "", "","", " ", "", "","", "", "", 
							"", "", "", "", "", "", "", "", "", "", "", 
							"", "", "", "", "", "", "", "", "", "", "", 
							"", "", "", "", "", "", "", "", "", "", "", 
							"", "", "", "", "", "", "", "", "", "", "", 
							"", "", "", "", "", "", "", "", "", "", "", 
							"", "", "", "", "", "", "", "", "", "", "", 
							"", "", "", "", "", "", "", "", "", "", "", 
							"", "", "", "", "", "", "", "", "", "", "", 
							"", "" 
							};
		protected static string[] convCharShift = {
							"'", "a", "b", "c", "e",
							"e", "f", "g",  "", "",
							"\r", "h", "i", "\n", "j", 
							"k", "", "", "", "", 
							"", "", "", "", "", "", "", "", "", "", 
							"", "", " ", "!", "\"", "#", "$", "%", "&", "\"", 
							"(", ")", "*", "+", ",", "-", ".", "/", "0", "1", 
							"2", "3", "4", "5", "6", "7", "8", "9", ":", ";", "<", 
							"=", ">", "?", "@", "A", "B", "C", "D", "E", "F", "G", 
							"H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", 
							"S", "T", "U", "V", "W", "X", "Y", "Z", "[", "\\", "]", 
							"^", "_", "`", "A", "B", "C", "D", "E", "F", "G", "H", 
							"I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", 
							"T", "U", "V", "W", "X", "Y", "Z", "{", "|", "}", "~", 
							((char)127).ToString()
							};
		protected static int[] convLocal = {
							0,0,0,0,0,0,0,7,8,9,
							10,0,0,13,0,0,16,17,18,19,
							20,21,22,23,24,25,26,27,28,29,
							30,31,32,33,34,35,36,37,38,39,
							40,41,42,43,44,45,46,47,48,49,
							50,51,52,53,54,55,56,57,58,59,
							60,61,62,63,64,65,66,67,68,69,
							70,71,72,73,74,75,76,77,78,79,
							80,81,82,83,84,85,86,87,88,89,
							90,91,92,93,94,95,96,97,98,99,
							100,101,102,103,104,105,106,107,108,109,
							110,111,112,113,114,115,116,117,118,119,
							120,121,122,123,124,125,126,127
							};
		/*						0,0,0,0,0,0,0,0,0,9,
							0,0,0,0,0,0,16,17,18,19,
							0,0,0,23,24,0,26,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0
							};*/
		protected static int[] convLocalCtl = {
							0,0,0,0,0,0,0,0,8,0,
							0,0,0,13,0,0,16,17,18,19,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0
							};
		protected static int[] convLocalAlt = {
							0,0,0,0,0,0,0,0,8,0,
							0,0,0,13,0,0,0,0,0,19,
							0,0,0,23,24,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,49,
							50,51,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0
							};
		protected static string[] blockChar = {
							"@", "A", "B", "C", "D",
							"E", "F", "G",  TDM_BACKSPACE, "\t",
							"\r", "H", "I", "\n", "J", 
							"K", ((char)16).ToString(), ((char)17).ToString(), ((char)0).ToString(), ((char)0).ToString(), 
							((char)20).ToString(), TDM_NAK, ((char)22).ToString(), ((char)23).ToString(), ((char)11).ToString(), "\r", TDM_BACKSPACE, TDM_ESC, "\t", "\0", 
							((char)30).ToString(), ((char)31).ToString(), " ", "!", "\"", "#", "$", "%", "&", "\"", 
							"(", ")", "*", "+", ",", "-", ".", "/", "0", "1", 
							"2", "3", "4", "5", "6", "7", "8", "9", ":", ";", "<", 
							"=", ">", "?", "@", "A", "B", "C", "D", "E", "F", "G", 
							"H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", 
							"S", "T", "U", "V", "W", "X", "Y", "Z", "[", "\\", "]", 
							"^", "_", "`", "a", "b", "c", "d", "e", "f", "g", "h", 
							"i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", 
							"t", "u", "v", "w", "x", "y", "z", "{", "|", "}", "~", 
							((char)127).ToString()
							};
		protected static string[] blockCharCtl = {
							"", "", "", "", "", "", "", "", "", 
							  "", "", "", "", "", "", "", "", "",
							"", "", "", "", "", "", "", "","", 
							"", "", "", "","", " ", "", "","", "", "", 
							"", "", "", "", "", "", "", "", "", "", "", 
							"", "\0", "", "", "", "", "", "", "", "", "", 
							"", "", "", "", "\0", ((char)1).ToString(), ((char)2).ToString(), ((char)3).ToString(), ((char)4).ToString(), ((char)5).ToString(), ((char)6).ToString(), 
							((char)7).ToString(), "", "\t", ((char)10).ToString(), ((char)11).ToString(), ((char)12).ToString(), ((char)13).ToString(), ((char)14).ToString(), ((char)15).ToString(), ((char)16).ToString(), ((char)17).ToString(), 
							((char)18).ToString(), ((char)19).ToString(), ((char)20).ToString(), ((char)21).ToString(), ((char)22).ToString(), ((char)23).ToString(), ((char)24).ToString(), ((char)25).ToString(), ((char)26).ToString(), ((char)27).ToString(), ((char)28).ToString(), 
							((char)29).ToString(), ((char)30).ToString(), "", "", ((char)1).ToString(), ((char)2).ToString(), ((char)3).ToString(), ((char)4).ToString(), ((char)5).ToString(), ((char)6).ToString(), ((char)7).ToString(), 
							"", "\t", ((char)10).ToString(), ((char)11).ToString(), ((char)12).ToString(), ((char)13).ToString(), ((char)14).ToString(), ((char)15).ToString(), ((char)16).ToString(), ((char)17).ToString(), ((char)18).ToString(), 
							((char)19).ToString(), ((char)20).ToString(), ((char)21).ToString(), ((char)22).ToString(), ((char)23).ToString(), ((char)24).ToString(), ((char)25).ToString(), ((char)26).ToString(), "", "", "", 
							"", "" 
							};
		protected static string[] blockCharAlt = {
							"", "", "", "", "", "", "", "", "", 
							  "", "", "", "", "", "", "", "", "",
							"", "", "", "", "", "", "", "","", 
							"", "", "", "","", " ", "", "","", "", "", 
							"", "", "", "", "", "", "", "", "", "", "", 
							"", "", "", "", "", "", "", "", "", "", "", 
							"", "", "", "", "", "", "", "", "", "", "", 
							"", "", "", "", "", "", "", "", "", "", "", 
							"", "", "", "", "", "", "", "", "", "", "", 
							"", "", "", "", "", "", "", "", "", "", "", 
							"", "", "", "", "", "", "", "", "", "", "", 
							"", "", "", "", "", "", "", "", "", "", "", 
							"", "" 
							};
		protected static string[] blockCharShift = {
							"'", "a", "b", "c", "e",
							"e", "f", "g",  "", "",
							"\r", "h", "i", "\n", "j", 
							"k", "", "", "", "", 
							"", "", "", "", "", "", "", "", "", "", 
							"", "", " ", "!", "\"", "#", "$", "%", "&", "\"", 
							"(", ")", "*", "+", ",", "-", ".", "/", "0", "1", 
							"2", "3", "4", "5", "6", "7", "8", "9", ":", ";", "<", 
							"=", ">", "?", "@", "A", "B", "C", "D", "E", "F", "G", 
							"H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", 
							"S", "T", "U", "V", "W", "X", "Y", "Z", "[", "\\", "]", 
							"^", "_", "`", "A", "B", "C", "D", "E", "F", "G", "H", 
							"I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", 
							"T", "U", "V", "W", "X", "Y", "Z", "{", "|", "}", "~", 
							((char)127).ToString()
							};
		protected static int[] blockLocal = {
							0,0,0,0,0,0,0,7,8,9,
							10,0,0,13,0,0,16,17,18,19,
							20,21,22,23,24,25,26,27,28,29,
							30,31,32,33,34,35,36,37,38,39,
							40,41,42,43,44,45,46,47,48,49,
							50,51,52,53,54,55,56,57,58,59,
							60,61,62,63,64,65,66,67,68,69,
							70,71,72,73,74,75,76,77,78,79,
							80,81,82,83,84,85,86,87,88,89,
							90,91,92,93,94,95,96,97,98,99,
							100,101,102,103,104,105,106,107,108,109,
							110,111,112,113,114,115,116,117,118,119,
							120,121,122,123,124,125,126,127
							};
		protected static int[] blockLocalCtl = {
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0
							};
		protected static int[] blockLocalAlt = {
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0,
							0,0,0,0,0,0,0,0,0,0
							};

		protected static string[] plainFn = {"@", "A", "B", "C", "D", "E", "F", "G", "", "", "", "H", "I", "", "J", "K"};
		protected static string[] shiftFn = {"'", "a", "b", "c", "d", "e", "f", "g", "", "", "", "h", "i", "", "j", "k"};
	}
}
