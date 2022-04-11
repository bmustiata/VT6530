using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;

using Maximum;
using Maximum.AnsiTerm;
using Maximum.Collections;

namespace LibVt6530
{
	public interface Vt6530Listener
	{
		void OnConnect();
		void OnDisconnect();
		void OnError(string msg);
	};

	public struct TextWatch
	{
		public string text;
		public int commandCode;
		public bool ignoreCase;
	};

	public class Vt6530 : ITelnetListener, MappedKeyListener, IDisposable
	{
		protected string m_rhost;
		protected int m_port;
		protected TelnetConnection m_telnet;	

		protected TextDisplay m_display;
		protected Keys m_keys;
		protected Mode m_currentMode;
		protected Vector<TermEventListener> m_listeners = new Vector<TermEventListener>();
		protected Vector<TextWatch> m_textWatches = new Vector<TextWatch>();
		protected StringBuilder m_receiveBuf = new StringBuilder();
		protected Socket m_sock;

		public Vt6530(string remote_host, int port)
		{
			m_rhost = remote_host; 
			m_port = port;

			m_display = new TextDisplay(2, 80, 24);
			m_keys = new Keys();
			//container.registerKeyListener(keys);
			m_keys.SetListener(this);

			m_sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			m_sock.Connect(remote_host, m_port);
			m_telnet = new TelnetConnection(this, m_sock);
			m_currentMode = new Guardian(m_listeners, m_display, m_keys, m_telnet);	
			m_telnet.Start();
		}

		public string Host
		{
			get { return m_rhost; }
		}

		public int Port
		{
			get { return m_port; }
		}

		public void Lock()
		{
			m_telnet.Lock();
		}

		public void UnLock()
		{
			m_telnet.UnLock();
		}

		public void Stop()
		{
			m_telnet.Stop();
		}

		public void Join()
		{
			m_telnet.Join();
		}

		public void Dispose()
		{
			Stop();
		}

		public void OnTelnetRecv(byte[] data, int len)
		{
			m_currentMode.ProcessRemoteString(data, len);
			if (m_display.NeedsRepaint())
			{
				m_display.GetDisplayPage().RescanFields();
				DispatchDisplayChanged();
			}
			/*
			for ( int x = 0; x < len; x++ )
			{
				if ( !isprint(data[x]) )
				{
					data[x] = ' ';
				}
				else if ( '%' == data[x] )
				{
					data[x] = '/';
				}
			}
			data[len-1] = '\0';
			Log::WriteInfo((char *)data);*/
		}		
		
		public void OnTelnetConnect()
		{
			DispatchConnect();
		}

		public void OnTelnetClose()
		{
			DispatchDisconnect();
		}

		public void OnTelnetError(Exception se)
		{
			DispatchError(se.Message);
		}

		public void OnTelnetUnmappedOption(int command, int option)
		{
		}

		public void TelnetGetWindowSize(out int widthInChars, out int heightInChars)
		{
			widthInChars = GetNumColumns();
			heightInChars = GetNumRows();
		}

		public string TelnetGetTeminalName()
		{
			return "tn6530-8";
		}

		public TelnetConnection Connection() 
		{ 
			return m_telnet; 
		}

		public TextDisplay Display() 
		{ 
			return m_display; 
		}

		public bool IsBlockMode() 
		{ 
			return !m_currentMode.IsConvMode(); 
		}

		public void Keydown(int key, bool shift, bool ctrl, bool alt)
		{
			m_keys.KeyReleased(key, shift, ctrl, alt);
		}

		public void Keypress(int key, bool shift, bool ctrl, bool alt)
		{
			m_keys.KeyTyped(key, shift, ctrl, alt);
		}

		/*void setdisplay(PaintSurface *ps)
		{
			_ASSERT(_CrtIsValidHeapPointer(ps));

			ps.setPaintPeer(this);
			//ps.registerKeyListener(m_keys);
			container = ps;
		}*/
				
		/*void paint()
		{
			
			m_display.paint(container);
		}*/
			
		public void Close()
		{
			m_telnet.Stop();
			DispatchDisconnect();
		}
			
		public void KeyMappedKey(string s)
		{
			if (s[0] == (char)ExtKeys.SPC_PRINTSCR)
			{
				DispatchDebug("Print screen received");
				//display.dumpScreen(Logger.out);
				//Logger.out.println(display.dumpAttibutes());
				//Logger.out.println(display.toHTML(container.getForeGroundColor(), container.getBackGroundColor()));
			}
			if (s[0] != (char)1)
			{
				m_display.EchoDisplay(s);
			}
			m_telnet.Send(s);
			DispatchDisplayChanged();
		}

		public void KeyCommand(char c)
		{
			if (c == (char)AnsiKeyCodeCmd.TAB)
			{
				m_display.Tab();
				DispatchDisplayChanged();
				return;
			}
			else if (c == (char)ExtKeys.SPC_BREAK)
			{
				try
				{
					m_telnet.Send((byte)0);
					m_telnet.Send((byte)0);
					m_telnet.Send((byte)0);
				}
				catch (Exception ioe)
				{
					LogFile.SysWriteError(ioe);
				}
				return;
			}
			else if (c == (char)ExtKeys.SPC_PRINTSCR || c == 27)
			{
				StringBuilder sb = new StringBuilder();
				m_display.DumpScreen(sb);
				sb.Append("\r\n\r\n");
				m_display.DumpAttibutes(sb);

				LogFile.SysWriteInfo(sb.ToString());
				//FILE *out = fopen("d:\\output.txt", "w");
				//fprintf(out, "%s", (char*)sb);
				//fclose(out);

				//display.dumpScreen(Logger.out);
				//Logger.out.println(display.dumpAttibutes());
				//Logger.out.println(display.toHTML(container.getForeGroundColor(), container.getBackGroundColor()));
				return;
			}
			m_currentMode.ExecLocalCommand(c);
			DispatchDisplayChanged();
		}
		
		public int KeyGetPage()
		{
			return m_display.GetCurrentPage();
		}
		
		public int GetNumRows()
		{
			return m_display.GetNumRows();
		}

		public int GetNumColumns()
		{
			return m_display.GetNumColumns();
		}

		public int KeyGetCursorX()
		{
			return m_display.GetCursorCol();
		}
		
		public int KeyGetCursorY()
		{
			return m_display.GetCursorRow();
		}
		
		public void KeyGetStartFieldASCII(StringBuilder sb)
		{
			m_display.GetStartFieldASCII(sb);
		}
			
		public void LocalEchoOff()
		{
			m_display.SetEchoOff();
		}
		
		public void LocalEchoOn()
		{
			m_display.SetEchoOn();
		}
		
		/**
		 *  m_display a message in line 25
		 */
		public void Message(string msg)
		{
			//ASSERT_MEM(m_display, sizeof(Textm_display));
			//m_display.writeMessage(msg);
			//dispatchm_displayChanged();
			DispatchDebug(msg);
		}
		
		/**
		 *  Telnet has recived a 34 extended control sequence.  
		 *  The parameters seem to contain info about text
		 *  attributes (such as invisible) and characters that
		 *  signal a buffer send to Tandem (like CR).
		 */
		//public virtual void Set34(string operation, string parms, int len)
		//{
		//	/*if (params[0] == 4)
		//	{
		//		ATLTRACE("34 -- SetEvent\r\n");
		//		SetEvent(h34Lock);
		//	}
		//	else
		//	{
		//		ATLTRACE("34 -- Not Firing\r\n");
		//	}*/
		//	Dispatch34(operation, parms, len);
		//}

		/**
		 *  Set the major terminal mode.  The choices
		 *  are:
		 *		A	ANSI
		 *		B	Block Mode
		 *		C	Conversation mode
		 *//*
		void setMode(char mode)
		{
			switch (mode)
			{
				case 'A':
					//System.out.println("ANSI Mode");
					break;
				case 'B':
					m_keys.setKeySet(KEYS_BLOCK);
					m_display.setModeBlock();
					telnet.setBufferingOn();
					break;
				case 'C':
					m_keys.setKeySet(KEYS_CONV);
					m_display.setModeConv();
					telnet.setBufferingOff();
					break;
			}
		}*/
		
		public void SetCursorY(int row)
		{
			m_display.SetCursorRowCol(row, m_display.GetCursorCol());
		}
		
		public void SetCursorX(int col)
		{
			m_display.SetCursorRowCol(m_display.GetCursorRow(), col);
		}
		
		public void ScrapeScreen(StringBuilder sb)
		{
			m_display.DumpScreen(sb);
		}
		
		public void ScrapeAttributes(StringBuilder sb)
		{
			m_display.DumpAttibutes(sb);
		}
		
		public void GetSubString(int row, int col, int len, StringBuilder sb)
		{
			m_display.GetSubString(row, col, len, sb);
		}

		public void SetForegroundColor(string color)
		{
			m_display.SetForeGroundColor(Color.Parse(color));
			DispatchDisplayChanged();
		}

		public void SetBackgroundColor(string color)
		{
			m_display.SetBackGroundColor(Color.Parse(color));
			//DispatchDisplayChanged();
		}
		
		public void SetForegroundColor(int c)
		{
			m_display.SetForeGroundColor(new Color(c));
			//DispatchDisplayChanged();
		}

		public void SetBackgroundColor(int c)
		{
			m_display.SetBackGroundColor(new Color(c));
			//dispatchDisplayChanged();
		}

		/**
		 *  Get the 'index'nth field on the screen.
		 *  The first field is index ZERO.  If the
		 *  index is larger than the number of field,
		 *  an empty string is returned.
		 */
		public void GetField(int index, StringBuilder sb)
		{
			m_display.GetField(index, sb);
		}
		
		/**
		 *  Get the video, data, and key attributes for a
		 *  field.
		 */
		public int GetFieldAttributes(int index)
		{
			return m_display.GetFieldAttributes(index);
		}

		/**
		 *  Get the text in the field at the cursor
		 *  position.
		 */
		public void GetCurrentField(StringBuilder sb)
		{
			m_display.GetCurrentField(sb);
		}
		
		/**
		 *  Get the 'index'nth unprotected field on 
		 *  the screen.  The first field is index 
		 *  ZERO.  If the index is larger than the 
		 *  number of field, an empty string is 
		 *  returned.
		 */
		public void GetUnprotectField(int index, StringBuilder sb)
		{
			m_display.GetUnprotectField(index, sb);
		}
		
		/**
		 *  Write text into the 'index'nth 
		 *  unprotected field on the screen.  The 
		 *  first field is index ZERO.  If the 
		 *  index is larger than the number of field, 
		 *  the request is ignored.
		 */
		public void SetField(int index, string text)
		{
			m_display.SetField(index, text);
		}
		
		/**
		 *  Returns true if the 'index'nth unprotected
		 *  field has its MDT set. The first field is 
		 *  index ZERO.  If the index is larger than 
		 *  the  number of fields, false is returned.
		 */
		public bool IsFieldChanged(int index)
		{
			return m_display.IsFieldChanged(index);
		}
		
		/**
		 *  Get a full line of m_display text.  the line
		 *  number is ranged 1-24.
		 */
		public void GetLine(int lineNumber, StringBuilder sb)
		{
			m_display.GetLine(lineNumber, sb);
		}
		
		/**
		 *  Set the cursor at the start if the 
		 *  'index'nth unprotected field on the screen.  
		 *  The first field is index ZERO.  If the 
		 *  index is larger than the number of field, 
		 *  the request is ignored.
		 */
		public void CursorToField(int index)
		{
			m_display.CursorToField(index);
		}
		
		/**
		 *  Send a break to tandem
		 */
		public void SendBreak()
		{
			m_telnet.Send((byte)4);
			m_telnet.Send((byte)0);
		}

		/**
		 *  Functions as if the user entered the
		 *  string on the command line and hit F10.
		 */
		public void SendCommandLine(string command)
		{
			m_display.SetCursorRowCol(23, 1);
			m_display.WriteDisplay(command);
			FakeF10();
			DispatchDisplayChanged();
		}
		
		/** 
		 *  Send a single keystroke to the terminal as
		 *  if it was typed at the keyboard.  Set shift,
		 *  alt, and/or ctrl to true to simulate these
		 *  keys being held down while the key was 
		 *  pressed.
		 */
		public void FakeKey(int keycode, bool shift, bool alt, bool ctrl)
		{
			//ASSERT_MEM(keys, sizeof(Keys));
			if ((keycode >= 0x30 && keycode <= 0x5B) || (keycode < 14))
			{
				m_keys.KeyTyped(keycode, shift, ctrl, alt);
			}
			else
			{
				m_keys.KeyReleased(keycode, shift, ctrl, alt);
			}
			//keypress(keycode, shift, ctrl, alt);
			DispatchDisplayChanged();
		}
		
		/**
		 *  Send a string to the terminal as if it
		 *  was typed at the keyboard.
		 */
		public void FakeKeys(string keystrokes)
		{
			int key;
			int len = keystrokes.Length;
			for (int x = 0; x < len; x++)
			{
				key = keystrokes[x];
				//keys.keyAction(false, key, false, false, false);
				Keypress(key, false, false, false);
			}
			LogFile.SysWriteInfo("FakeKeys: " + keystrokes);
			DispatchDisplayChanged();
		}
		
		/**
		 *  Fake the user pressing a function key.
		 */
		public void FakeF1()
		{
			m_keys.KeyReleased((int)ExtKeys.SPC_F1, false, false, false);
		}
		
		/**
		 *  Fake the user pressing a function key.
		 */
		public void FakeF2()
		{
			m_keys.KeyReleased((int)ExtKeys.SPC_F2, false, false, false);
		}
		
		/**
		 *  Fake the user pressing a function key.
		 */
		public void FakeF3()
		{
			m_keys.KeyReleased((int)ExtKeys.SPC_F3, false, false, false);
		}
		
		/**
		 *  Fake the user pressing a function key.
		 */
		public void FakeF4()
		{
			m_keys.KeyReleased((int)ExtKeys.SPC_F4, false, false, false);
		}
		
		/**
		 *  Fake the user pressing a function key.
		 */
		public void FakeF5()
		{
			m_keys.KeyReleased((int)ExtKeys.SPC_F5, false, false, false);
		}
		
		/**
		 *  Fake the user pressing a function key.
		 */
		public void FakeF6()
		{
			m_keys.KeyReleased((int)ExtKeys.SPC_F6, false, false, false);
		}
		
		/**
		 *  Fake the user pressing a function key.
		 */
		public void FakeF7()
		{
			m_keys.KeyReleased((int)ExtKeys.SPC_F7, false, false, false);
		}

		public void FakeSF7()
		{
			m_keys.KeyReleased((int)ExtKeys.SPC_F7, true, false, false);
		}

		/**
		 *  Fake the user pressing a function key.
		 */
		public void FakeF8()
		{
			m_keys.KeyReleased((int)ExtKeys.SPC_F8, false, false, false);
		}
		
		/**
		 *  Fake the user pressing a function key.
		 */
		public void FakeF9()
		{
			m_keys.KeyReleased((int)ExtKeys.SPC_F9, false, false, false);
		}
		
		/**
		 *  Fake the user pressing a function key.
		 */
		public void FakeF10()
		{
			m_keys.KeyReleased((int)ExtKeys.SPC_F10, false, false, false);
		}
		
		/**
		 *  Fake the user pressing a function key.
		 */
		public void FakeF11()
		{
			m_keys.KeyReleased((int)ExtKeys.SPC_F11, false, false, false);
		}
		
		/**
		 *  Fake the user pressing a function key.
		 */
		public void FakeF12()
		{
			m_keys.KeyReleased((int)ExtKeys.SPC_F12, false, false, false);
		}
		
		/**
		 *  Fake the user pressing a function key.
		 */
		public void FakeF13()
		{
			throw new NotImplementedException();
		}
		
		/**
		 *  Fake the user pressing a function key.
		 */
		public void FakeF14()
		{
			throw new NotImplementedException();
		}
		
		/**
		 *  Fake the user pressing a function key.
		 */
		public void FakeF15()
		{
			throw new NotImplementedException();
		}
		
		/**
		 *  Fake the user pressing a function key.
		 */
		public void FakeF16()
		{
			throw new NotImplementedException();
		}
		
		/**
		 *  Fake the user pressing a function key.
		 */
		public void FakeEnter()
		{
			m_keys.KeyPressed(10, false, false, false);
			DispatchDisplayChanged();
		}
		
		/**
		 *  Fake the user pressing a function key.
		 */
		public void FakeBackspace()
		{
			m_keys.KeyPressed('\b', false, false, false);
			DispatchDisplayChanged();
		}
		
		/**
		 *  Fake the user pressing a function key.
		 */
		public void FakeTab()
		{
			m_keys.KeyPressed('\t', false, false, false);
			DispatchDisplayChanged();
		}
		
		/**
		 *  Fake the user pressing a function key.
		 */
		public void FakeUpArrow()
		{
			m_keys.KeyPressed((int)ExtKeys.SPC_UP, false, false, false);
			DispatchDisplayChanged();
		}
		
		/**
		 *  Fake the user pressing a function key.
		 */
		public void FakeDownArrow()
		{
			m_keys.KeyPressed((int)ExtKeys.SPC_DOWN, false, false, false);
			DispatchDisplayChanged();
		}
		
		/**
		 *  Fake the user pressing a function key.
		 */
		public void FakeLeftArrow()
		{
			m_keys.KeyPressed((int)ExtKeys.SPC_LEFT, false, false, false);
			DispatchDisplayChanged();
		}
		
		/**
		 *  Fake the user pressing a function key.
		 */
		public void FakeRightArrow()
		{
			m_keys.KeyPressed((int)ExtKeys.SPC_RIGHT, false, false, false);
			DispatchDisplayChanged();
		}
		
		/**
		 *  Fake the user pressing a function key.
		 */
		public void FakeHome()
		{
			m_keys.KeyPressed((int)ExtKeys.SPC_HOME, false, false, false);
			DispatchDisplayChanged();
		}
		
		/**
		 *  Fake the user pressing a function key.
		 */
		public void FakeEnd()
		{
			m_keys.KeyPressed((int)ExtKeys.SPC_END, false, false, false);
			DispatchDisplayChanged();
		}
		
		/**
		 *  Fake the user pressing a function key.
		 */
		public void FakeInsert()
		{
			m_keys.KeyPressed((int)ExtKeys.SPC_INS, false, false, false);
			DispatchDisplayChanged();
		}
		
		/**
		 *  Fake the user pressing a function key.
		 */
		public void FakeDelete()
		{
			m_keys.KeyPressed((int)ExtKeys.SPC_DEL, false, false, false);
			DispatchDisplayChanged();
		}

		public void ToHTML(StringBuilder sb)
		{
			m_display.ToHTML(m_display.GetForeGroundColor(), m_display.GetBackGroundColor(), sb);
		}

		public override string ToString()
		{
			return m_display.ToString();
		}

		public void AddListener(TermEventListener tel)
		{
			m_listeners.Add(tel);
		}

		public void ClearListeners()
		{
			m_listeners.Clear();
		}

		public void AddOnText(string txt, int commandCode, bool ignoreCase)
		{
			TextWatch tw;
			tw.text = txt;
			tw.commandCode = commandCode;
			tw.ignoreCase = ignoreCase;
			m_textWatches.Add(tw);
		}

		public void ClearOnTexts()
		{
			m_textWatches.Clear();
		}

		private void DispatchTextWatch(string txt, int commandCode)
		{
			for (int x = 0; x < m_listeners.Count; x++)
			{
				m_listeners.ElementAt(x).TermOnTextWatch(txt, commandCode);
			}
		}

		protected void DispatchTextWatch()
		{
			int count = m_textWatches.Count;
			if ( count == 0 )
			{
				return;
			}
			StringBuilder screenTxt = new StringBuilder(m_display.GetNumRows() * m_display.GetNumColumns());
			m_display.GetText(screenTxt);
			for ( int x = 0; x < count; x++ )
			{
				int diff;
				string txt = m_textWatches.ElementAt(x).text;
				if ( m_textWatches.ElementAt(x).ignoreCase )
				{
					diff = screenTxt.ToString().IndexOf( txt, 0, StringComparison.CurrentCultureIgnoreCase );
				}
				else
				{
					diff = screenTxt.ToString().IndexOf( txt, 0 );
				}
				if ( diff > -1 )
				{
					DispatchTextWatch( txt, m_textWatches.ElementAt(x).commandCode );
					return;
				}
			}
		}

		protected void DispatchConnect()
		{
			for (int x = 0; x < m_listeners.Count; x++)
			{
				m_listeners.ElementAt(x).TermOnConnect();
			}
		}

		protected void DispatchDisconnect()
		{
			for (int x = 0; x < m_listeners.Count; x++)
			{
				m_listeners.ElementAt(x).TermOnDisconnect();
			}
		}

		protected void DispatchDisplayChanged()
		{
			for (int x = 0; x < m_listeners.Count; x++)
			{
				m_listeners.ElementAt(x).TermOnDisplayChanged();
			}
			DispatchTextWatch();
		}

		protected void DispatchError(string msg)
		{
			for (int x = 0; x < m_listeners.Count; x++)
			{
				m_listeners.ElementAt(x).TermOnError(msg);
			}
		}

		protected void DispatchDebug(string msg)
		{
			for (int x = 0; x < m_listeners.Count; x++)
			{
				m_listeners.ElementAt(x).TermOnDebug(msg);
			}
		}

		/*protected void Dispatch34(string operation, string parms, int len)
		{
			for (int x = 0; x < m_listeners.Count(); x++)
			{
				m_listeners.ElementAt(x).TermOnRecv34(operation, parms, len);
			}
		}*/
	};
}
