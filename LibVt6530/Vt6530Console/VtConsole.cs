using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

using Maximum;
using Maximum.AnsiTerm;
using LibVt6530;

namespace Vt6530Console
{
	public class VtConsole : TermEventListener
	{
		protected Vt6530 m_6530;
		protected IConnection m_stdout;
		protected TerminalFrameManager m_term;
		protected TerminalWidgetStatusBar m_statusbar;
		protected TerminalWidgetTextPanel m_convText;
		protected TermCap m_caps = new TermCap();
		protected IConnection m_stdin;
		protected ThreadedConnectionListener m_inkeys;
		protected bool m_sendLogonOnEnquire;
		protected bool m_sendLogonOnEnquire2;

		public Vt6530 GetServerTerm() { return m_6530; }

		bool f1Key( TerminalWidget source, int id, object arg )
		{
			TerminalFrameManager tfm = (TerminalFrameManager)arg;
			VtConsole con = (VtConsole)tfm.GetData();
			if (con.GetServerTerm().IsBlockMode())
			{
				con.GetServerTerm().FakeF1();
			}
			return true;
		}

		bool f2Key( TerminalWidget source, int id, object arg )
		{
			TerminalFrameManager tfm = (TerminalFrameManager)arg;
			VtConsole con = (VtConsole)tfm.GetData();
			if ( con.GetServerTerm().IsBlockMode() )
			{
				con.GetServerTerm().FakeF2();
			}
			return true;
		}

		bool f3Key( TerminalWidget source, int id, object arg )
		{
			TerminalFrameManager tfm = (TerminalFrameManager)arg;
			VtConsole con = (VtConsole)tfm.GetData();
			if (con.GetServerTerm().IsBlockMode())
			{
				con.GetServerTerm().FakeF3();
			}
			return true;
		}

		bool f4Key( TerminalWidget source, int id, object arg )
		{
			TerminalFrameManager tfm = (TerminalFrameManager)arg;
			VtConsole con = (VtConsole)tfm.GetData();
			if (con.GetServerTerm().IsBlockMode())
			{
				con.GetServerTerm().FakeF4();
			}
			return true;
		}

		bool f5Key( TerminalWidget source, int id, object arg )
		{
			TerminalFrameManager tfm = (TerminalFrameManager)arg;
			VtConsole con = (VtConsole)tfm.GetData();
			if (con.GetServerTerm().IsBlockMode())
			{
				con.GetServerTerm().FakeF5();
			}
			return true;
		}

		bool f6Key( TerminalWidget source, int id, object arg )
		{
			TerminalFrameManager tfm = (TerminalFrameManager)arg;
			VtConsole con = (VtConsole)tfm.GetData();
			if (con.GetServerTerm().IsBlockMode())
			{
				con.GetServerTerm().FakeF6();
			}
			return true;
		}

		bool f7Key( TerminalWidget source, int id, object arg )
		{
			TerminalFrameManager tfm = (TerminalFrameManager)arg;
			VtConsole con = (VtConsole)tfm.GetData();
			if (con.GetServerTerm().IsBlockMode())
			{
				con.GetServerTerm().FakeF7();
			}
			return true;
		}

		bool f8Key( TerminalWidget source, int id, object arg )
		{
			TerminalFrameManager tfm = (TerminalFrameManager)arg;
			VtConsole con = (VtConsole)tfm.GetData();
			if (con.GetServerTerm().IsBlockMode())
			{
				con.GetServerTerm().FakeF8();
			}
			return true;
		}

		bool f9Key( TerminalWidget source, int id, object arg )
		{
			TerminalFrameManager tfm = (TerminalFrameManager)arg;
			VtConsole con = (VtConsole)tfm.GetData();
			if (con.GetServerTerm().IsBlockMode())
			{
				con.GetServerTerm().FakeF9();
			}
			return true;
		}

		bool f10Key( TerminalWidget source, int id, object arg )
		{
			TerminalFrameManager tfm = (TerminalFrameManager)arg;
			VtConsole con = (VtConsole)tfm.GetData();
			if (con.GetServerTerm().IsBlockMode())
			{
				con.GetServerTerm().FakeF10();
			}
			return true;
		}

		bool f11Key( TerminalWidget source, int id, object arg )
		{
			TerminalFrameManager tfm = (TerminalFrameManager)arg;
			VtConsole con = (VtConsole)tfm.GetData();
			if (con.GetServerTerm().IsBlockMode())
			{
				con.GetServerTerm().FakeF11();
			}
			return true;
		}

		bool f12Key( TerminalWidget source, int id, object arg )
		{
			TerminalFrameManager tfm = (TerminalFrameManager)arg;
			VtConsole con = (VtConsole)tfm.GetData();
			if (con.GetServerTerm().IsBlockMode())
			{
				con.GetServerTerm().FakeF12();
			}
			return true;
		}

		bool keyKey( TerminalWidget source, int id, object arg )
		{
			//GC *gc = (GC *)arg;
			TerminalFrameManager tfm = (TerminalFrameManager)arg;
			VtConsole con = (VtConsole)tfm.GetData();
			con.GetServerTerm().KeyCommand((char)id);
			return true;
		}

		bool ctlKey( TerminalWidget source, int id, object arg )
		{
			TerminalFrameManager tfm = (TerminalFrameManager)arg;
			VtConsole con = (VtConsole)tfm.GetData();
			if (id == 29)
			{
				//m_6530.Display().GetDisplayPage().RescanFields();
				DumpTemplate();
				return true;
			}
			con.GetServerTerm().KeyCommand((char)id);
			return true;
		}

		bool cmdKey( TerminalWidget source, int id, object arg )
		{
			TerminalFrameManager tfm = (TerminalFrameManager)arg;
			VtConsole con = (VtConsole)tfm.GetData();
			con.GetServerTerm().KeyCommand((char)id);
			return true;
		}

		public VtConsole(string tandemIP, int tandemPort, string termName)
		{
			m_caps.SetTerm(termName);
			m_stdout = new AnsiConsoleOutConnection(null);
			m_term = new TerminalFrameManager(m_stdout, m_caps);
			m_term.SetData( this );

			TerminalFrame frame = new TerminalFrame(m_term, 80, 25);
			m_convText = new TerminalWidgetTextPanel(frame, 0,  AnsiAttribute.TERM_COLOR_FORE_GREEN, AnsiAttribute.TERM_COLOR_BACK_BLACK, 0, 0, 80, 24);
			m_convText.SetKeyHandler( keyKey );
			m_convText.SetCmdHandler( cmdKey );
			m_convText.SetCtlHandler( ctlKey );
			frame.AddWidget(m_convText);
			
			m_statusbar = new TerminalWidgetStatusBar(frame, 0, AnsiAttribute.TERM_COLOR_FORE_GREEN, AnsiAttribute.TERM_COLOR_BACK_BLACK, 0, 24, 80);
			m_statusbar.AddPanel( 80 );
			frame.AddWidget( m_statusbar );
			m_term.AddFrame( frame, "conv" );
			m_term.SetFrame( "conv" );

			frame.AddFnHandler((char)1, f1Key);
			frame.AddFnHandler((char)2, f2Key);
			frame.AddFnHandler((char)3, f3Key);
			frame.AddFnHandler((char)4, f4Key);
			frame.AddFnHandler((char)5, f5Key);
			frame.AddFnHandler((char)6, f6Key);
			frame.AddFnHandler((char)7, f7Key);
			frame.AddFnHandler((char)8, f8Key);
			frame.AddFnHandler((char)9, f9Key);
			frame.AddFnHandler((char)10, f10Key);
			frame.AddFnHandler((char)11, f11Key);
			frame.AddFnHandler((char)12, f12Key);

			m_6530 = new Vt6530(tandemIP, tandemPort);
			m_6530.AddListener( this );

			m_stdin = null;
			m_inkeys = new ThreadedConnectionListener(m_stdout, m_term);
			m_stdout.SetListener(m_term);

			if (tandemIP == "192.209.32.3")
			{
				m_6530.AddOnText("DOR", 0, false);
			}
		}

		public void Join()
		{
			m_6530.Join();
		}

		public void TermOnConnect()
		{
		}

		public void TermOnDisconnect()
		{
		}

		public void TermOnEnquire()
		{
			//Log.WriteInfo("ENQUIRE\n" + m_6530.ToString());

			m_6530.CursorToField(0);
			UpdateTerm();
			UpdateClient();
			Debug.Assert( m_6530.IsBlockMode() );
			if (m_sendLogonOnEnquire)
			{
				m_sendLogonOnEnquire2 = true;
				m_sendLogonOnEnquire = false;
			}
			else if (m_sendLogonOnEnquire2)
			{
				m_sendLogonOnEnquire2 = false;
				while (m_6530.ToString().Trim() != "DOR")
				{
					Thread.Sleep(100);
				}
				//m_6530.FakeKeys(DOR.TandemScreens.OV_SSO.GetLogon());
				//m_6530.FakeF1();
			}
		}

		public void TermOnResetLine()
		{
			//Log.WriteInfo("LINE RESET\n" + m_6530.ToString());
		}

		public void TermOnDisplayChanged()
		{
			UpdateTerm();
			UpdateClient();
		}

		public void TermOnError(string message)
		{
			LogFile.SysWriteInfo(message);
			m_statusbar.SetPanelText(0, message);
		}

		public void TermOnDebug(string message)
		{
			m_statusbar.SetPanelText(0, message);
		}

		public void TermOnTextWatch(string txt, int commandCode)
		{
			if (commandCode == 0)
			{
				m_sendLogonOnEnquire = true;
				m_6530.ClearOnTexts();
			}
		}

		public void TermOnLockKeyboard()
		{
			//Log.WriteInfo("LOCK Keyboard\n" + m_6530.ToString());
		}

		public void TermOnUnlockKeyboard()
		{
			//Log.WriteInfo("UNLOCK Keyboard\n" + m_6530.ToString());
		}

		protected void UpdateTerm()
		{
			TextDisplay tdsp = m_6530.Display();
			
			if ( tdsp.NeedsRepaint() )
			{
				Page page = tdsp.GetDisplayPage();
				for ( int y = 0; y < page.GetNumRows(); y++ )
				{
					for ( int x = 0; x < page.GetNumColumns(); x++ )
					{
						PageCell cell = page.GetCell(x, y);
						if ( cell.IsDirty() )
						{
							int attribs = 0;
							if ( cell.IsUnderline() )
							{
								attribs |= TerminalCell.TA_UNDER;
							}
							if ( cell.IsBlinking() )
							{
								attribs |= TerminalCell.TA_BLINK;
							}
							if ( cell.IsInvis() )
							{
								attribs |= TerminalCell.TA_INVIS;
							}
							if ( cell.IsReverse() )
							{
								attribs |= TerminalCell.TA_REVRS;
							}
							m_convText.SetText( cell.Get(), x, y, attribs );
							cell.SetDirty(false);
						}
					}
				}
				m_convText.SetCursor( page.m_cursorPos.m_column, page.m_cursorPos.m_row );
			}
		}

		protected void UpdateClient()
		{
			m_term.CurrentFrame().UpdateTerm(m_stdout);
		}

		protected void DumpTemplate()
		{
			if ( File.Exists("Screen.cs") )
			{
				File.Delete("Screen.cs");
			}
			StreamWriter writer = File.CreateText("Screen.cs");
			writer.WriteLine("using System;");
			writer.WriteLine("using System.Collections.Generic;");
			writer.WriteLine("using System.Diagnostics;");
			writer.WriteLine("using LibVt6530;");
			writer.WriteLine();
			writer.WriteLine("namespace ZZ");
			writer.WriteLine("{");

			writer.WriteLine("\tpublic class R000 : CommandLineScreen");
			writer.WriteLine("\t{");

			writer.WriteLine("\t\tinternal R000(Terminal term, Vt6530 vt) : base(term, vt)");
			writer.WriteLine("\t\t{");
			writer.WriteLine("\t\t\t");
			writer.WriteLine("\t\t}");
			writer.WriteLine();

			writer.WriteLine("\t\tpublic override bool VerifyLocation()");
			writer.WriteLine("\t\t{");
			writer.WriteLine("\t\t\tm_term.WaitEventScreen(false);");
			writer.WriteLine("\t\t\t// Check some of the protected fields to ensure ");
			writer.WriteLine("\t\t\t// the terminal is on this screen.");
			writer.WriteLine("\t\t\treturn false;");
			writer.WriteLine("\t\t}");
			writer.WriteLine();

			writer.WriteLine("\t\tpublic override void NavigateTo()");
			writer.WriteLine("\t\t{");
			writer.WriteLine("\t\t\t// Navigate to the screen.");
			writer.WriteLine("\t\t\tDOR.TandemScreens.CL.R002 s000;");
			writer.WriteLine("\t\t\ts000 = m_term.CreateClR002();");
			writer.WriteLine("\t\t\ts000.NavigateTo();");
			writer.WriteLine("\t\t\ts000.WaitScreenLoad();");
			writer.WriteLine("\t\t\t// go to 000 screen");
			writer.WriteLine("\t\t\ts000.CommandLineExec(\"000\");");
			writer.WriteLine("\t\t\tWaitScreenLoad();");
			writer.WriteLine("\t\t\tthrow new NotImplementedException();");
			writer.WriteLine("\t\t}");
			writer.WriteLine();

			writer.WriteLine("\t\tpublic override void WaitScreenLoad()");
			writer.WriteLine("\t\t{");
			writer.WriteLine("\t\t\t// Wait for the screen to finish loading.");
			writer.WriteLine("\t\t\tbase.WaitScreenLoad();");
			writer.WriteLine("\t\t}");
			writer.WriteLine();

			// write all the fields as properties
			bool isUnprotect;
			int col, row;
			StringBuilder buf = new StringBuilder();
			int count = m_6530.Display().GetDisplayPage().GetFieldCount();
			for (int x = 0; x < count; x++)
			{
				Page page = m_6530.Display().GetDisplayPage();
				if (!page.ValidField(x))
				{
					continue;
				}
				PageCell cell = page.GetFieldStart(x);
				page.GetFieldASCII(cell, buf, out isUnprotect);
				if (!isUnprotect && buf.Length == 0)
				{
					// no data and not updatable.
					continue;
				}
				page.GetFieldXY(x, out col, out row);
				writer.WriteLine("\t\t// {0} ROW:{1} COL:{2}", buf.ToString(), row+1, col+2);
				buf.Length = 0;
				writer.WriteLine("\t\tpublic string Field{0}", x);
				writer.WriteLine("\t\t{");
				writer.WriteLine("\t\t\tget");
				writer.WriteLine("\t\t\t{");
				writer.WriteLine("\t\t\t\tPageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart({0});", x);
				writer.WriteLine("\t\t\t\treturn m_vt.Display().GetDisplayPage().GetFieldASCII(cell);");
				writer.WriteLine("\t\t\t}");
				if (isUnprotect)
				{
					writer.WriteLine("\t\t\tset");
					writer.WriteLine("\t\t\t{");
					writer.WriteLine("\t\t\t\tint col; int row;");
					writer.WriteLine("\t\t\t\tm_vt.Display().GetDisplayPage().GetFieldXY({0}, out col, out row);", x);
					writer.WriteLine("\t\t\t\tm_vt.Display().SetCursorRowCol(row, col);");
					writer.WriteLine("\t\t\t\tm_vt.Display().WriteLocal(value);");
					writer.WriteLine("\t\t\t}");
				}
				writer.WriteLine("\t\t}");
				writer.WriteLine();
			}
			
			// submit page
			writer.WriteLine("\t\tpublic void Submit()");
			writer.WriteLine("\t\t{");
			writer.WriteLine("\t\t\tm_vt.FakeF1();");
			writer.WriteLine("\t\t}");

			writer.WriteLine("\t}");

			writer.WriteLine("}");
			writer.Close(); 
		}
	};
}
