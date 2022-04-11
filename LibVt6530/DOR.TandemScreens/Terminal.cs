using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Principal;
using System.Text;
using System.Threading;

using Maximum;
using Maximum.AnsiTerm;
using LibVt6530;

namespace DOR.TandemScreens
{
	public interface TerminalListener
	{
		void TermOnDisconnect();
		void TermOnEnquire();
		void TermOnResetLine();
		void TermOnError(string message);
		void TermOnLockKeyboard();
		void TermOnUnlockKeyboard();
		void TermOnTextWatch(string txt, int commandCode);
	}

	public class Terminal : TermEventListener
	{
		enum ScreenLoadState
		{
			START,
			KBUNLOCK,
			RESET,
			ENQ
		}

		protected Vt6530 m_vt;
		protected TerminalListener m_listener;
		protected ManualResetEvent m_event = new ManualResetEvent(false);
		protected ManualResetEvent m_eventEnquire = new ManualResetEvent(false);
		protected ManualResetEvent m_screenEvent = new ManualResetEvent(false);
		protected ManualResetEvent m_kbUnlockEvent = new ManualResetEvent(false);
		volatile ScreenLoadState m_state;

		public Terminal(string host, int port, TerminalListener listener)
		{
			m_listener = listener;
			m_vt = new Vt6530(host, port);
			m_vt.AddListener(this);
		}

		public Terminal(TerminalListener listener)
		{
			m_listener = listener;
			m_vt = new Vt6530("192.209.32.3", 1016);
			m_vt.AddListener(this);
		}

		public Terminal()
		{
			m_listener = null;
			m_vt = new Vt6530("192.209.32.3", 1016);
			m_vt.AddListener(this);
		}

		protected void AssertConvMode()
		{
			if (m_vt.IsBlockMode())
			{
				throw new Exception("Terminal already in block mode");
			}
		}

		public void StartOTIS(string env)
		{
			StartOTIS(env, null);
		}

		public void StartOTIS(string env, IIdentity ident)
		{
			if (env == "PROD")
			{
				GoProd(ident);
			}
			else if (env == "DEMO")
			{
				GoDemo(ident);
			}
			else if (env == "TEST")
			{
				GoTest(ident);
			}
			else
			{
				throw new Exception("Unknown environment");
			}
		}

		private bool WaitDOR(string taclcmd)
		{
			AssertConvMode();
			//string screen = ScreenText;
			//if (screen.IndexOf("Enter Choice") < 0)
			//{
			//	WaitEvent();
			//}
			m_vt.FakeKeys(taclcmd + "\n");
			WaitEventEnquire();
			WaitEventEnquire();
			int count = 0;
			while (ScreenText.IndexOf("DOR") < 0 && ScreenText.IndexOf("Revenue Logon Screen") < 0)
			{
				// up to 2 second delay
				if (count++ > 19)
				{
					return false;
				}
				Thread.Sleep(100);
			}
			return true;
		}

		private void EnsureLogon(string taclcmd, IIdentity ident)
		{
			const int tries = 4;
			for (int x = 0; x < tries; x++)
			{
				CL.R002 menu = CreateClR002();
				try
				{
					int count = 0;
					while (!WaitDOR(taclcmd))
					{
						if (count++ > 3)
						{
							break;
						}
						Close();
						m_vt = new Vt6530(m_vt.Host, m_vt.Port);
						m_vt.AddListener(this);
						menu = CreateClR002();
					}
					if (null == ident)
					{
						m_vt.FakeKeys(OV_SSO.GetLogon());
					}
					else
					{
						m_vt.FakeKeys(OV_SSO.GetLogon(ident.Name));
					}
					m_vt.FakeF1();
					WaitEventScreen(true);
					count = 0;
					while (count++ < 3 && !menu.VerifyLocation() && ScreenText.IndexOf("Revenue Logon Screen") < 0 && ScreenText.IndexOf("Logon ID and/or Password required") < 0 )
					{
						WaitEvent();
					}
					if (menu.VerifyLocation())
					{
						return;
					}
					Close();
					Thread.Sleep(200);
					m_vt = new Vt6530(m_vt.Host, m_vt.Port);
					m_vt.AddListener(this);
				}
				catch (Exception ex)
				{
					if (x == tries-1)
					{
						throw ex;
					}
				}
			}
			Close();
			throw new Exception("Can't logon: " + ScreenText);
		}

		public void GoProd()
		{
			EnsureLogon("prod", null);
		}

		public void GoProd(IIdentity ident)
		{
			EnsureLogon("prod", ident);
		}

		public void GoDemo()
		{
			EnsureLogon("demo", null);
		}

		public void GoDemo(IIdentity ident)
		{
			EnsureLogon("demo", ident);
		}

		public void GoTest()
		{
			EnsureLogon("test", null);
		}

		public void GoTest(IIdentity ident)
		{
			EnsureLogon("test", ident);
		}

		public void Close()
		{
			m_vt.Stop();
		}

		public bool IsBlockMode
		{
			get { return m_vt.IsBlockMode(); }
		}

		public void TextWatchAdd(string txt, int commandNum, bool ignoreCase)
		{
			m_vt.AddOnText(txt, commandNum, ignoreCase);
		}

		public void TextWatchesClear()
		{
			m_vt.ClearOnTexts();
		}

		public string ScreenText
		{
			get
			{
				return m_vt.Display().ToString();
			}
		}

		public string ScreenHTML
		{
			get
			{
				StringBuilder buf = new StringBuilder();
				m_vt.Display().ToHTML(Color.Green, Color.Black, buf);
				return buf.ToString();
			}
		}

		public int FieldCount
		{
			get
			{
				return m_vt.Display().GetDisplayPage().GetFieldCount();
			}
		}

		public string FieldText(int fieldNum)
		{
			StringBuilder buf = new StringBuilder();
			m_vt.Display().GetField(fieldNum, buf);
			return buf.ToString();
		}

		public int FieldLength(int fieldNum)
		{
			StringBuilder buf = new StringBuilder();
			m_vt.Display().GetField(fieldNum, buf);
			return buf.Length;
		}

		public void FieldSet(int fieldNum, string text)
		{
			if (! m_vt.Display().GetDisplayPage().GetFieldStart(fieldNum).IsUnprotect())
			{
				throw new Exception("Field is protected");
			}
			m_vt.Display().CursorToField(fieldNum);
			m_vt.FakeKeys(text);
		}

		public void KeyF1()	{ m_vt.FakeF1(); }
		public void KeyF2() { m_vt.FakeF2(); }
		public void KeyF3() { m_vt.FakeF3(); }
		public void KeyF4() { m_vt.FakeF4(); }
		public void KeyF5() { m_vt.FakeF5(); }
		public void KeyF6() { m_vt.FakeF6(); }
		public void KeyF7() { m_vt.FakeF7(); }
		public void KeyF8() { m_vt.FakeF8(); }
		public void KeyF9() { m_vt.FakeF9(); }
		public void KeyF10() { m_vt.FakeF10(); }
		public void KeyF11() { m_vt.FakeF11(); }
		public void KeyF12() { m_vt.FakeF12(); }
		public void KeyBreak() { m_vt.SendBreak(); }

		public void WaitEvent()
		{
			//Log.WriteInfo("WaitEvent");
			m_event.Reset();
			m_event.WaitOne(TimeSpan.FromSeconds(1), true);
		}

		public void WaitEventEnquire()
		{
			//Log.WriteInfo("WaitEventEnquire");
			m_eventEnquire.Reset();
			m_eventEnquire.WaitOne(TimeSpan.FromSeconds(4), true);
		}

		public void WaitEventKbUnlock()
		{
			//Log.WriteInfo("WaitKbUnlock");
			m_kbUnlockEvent.WaitOne(TimeSpan.FromSeconds(5), true);
		}

		public void WaitEventScreen(bool force)
		{
			if (force)
			{
				WaitEventEnquire();
			}
			m_vt.Lock();
			m_vt.UnLock();
			if (m_state != ScreenLoadState.ENQ)
			{
				m_screenEvent.Reset();
				m_screenEvent.WaitOne(TimeSpan.FromSeconds(5), true);
			}
		}

		public void TermOnConnect()
		{
			//Log.WriteInfo("TERM ON CONNECT");
		}

		/**
		 *  The connection to the host was lost or
		 *  closed.
		 */
		public void TermOnDisconnect()
		{
			//Log.WriteInfo("TERM ON DISCONNECT");
			if (null != m_listener)
			{
				m_listener.TermOnDisconnect();
			}
			m_event.Set();
		}

		public void TermOnResetLine()
		{
			//Log.WriteInfo("TERM ON RESET LINE");
			if (null != m_listener)
			{
				m_listener.TermOnResetLine();
			}
			if (m_state == ScreenLoadState.KBUNLOCK)
			{
				m_state = ScreenLoadState.RESET;
			}
			m_event.Set();
		}

		/**
		 *  The host has completed rendering the
		 *  screen and is now waiting for input.
		 */
		public void TermOnEnquire()
		{
			//Log.WriteInfo("TERM ON ENQUIRE");
			m_vt.Display().GetDisplayPage().RescanFields();
			if (null != m_listener)
			{
				m_listener.TermOnEnquire();
			}
			m_eventEnquire.Set();
			m_event.Set();
			if (m_state == ScreenLoadState.RESET)
			{
				m_state = ScreenLoadState.ENQ;
				m_screenEvent.Set();
			}
		}

		/**
		 *  Changes in the display require the container
		 *  to repaint.
		 */
		public void TermOnDisplayChanged()
		{
			//Log.WriteInfo("TERM ON DISPLAY CHANGED");
		}

		/**
		 *  There has been an internal error.
		 */
		public void TermOnError(string message)
		{
			LogFile.SysWriteInfo("TERM ON ERROR " + message);
			if (null != m_listener)
			{
				m_listener.TermOnError(message);
			}
			m_event.Set();
		}

		/**
		 *  Debuging output -- may be ignored
		 */
		public void TermOnDebug(string message)
		{
			LogFile.SysWriteInfo("TERM ON DEBUG " + message);
			m_event.Set();
		}

		public void TermOnTextWatch(string txt, int commandCode)
		{
			if (null != m_listener)
			{
				m_listener.TermOnTextWatch(txt, commandCode);
			}
			m_event.Set();
		}

		public void TermOnLockKeyboard()
		{
			//Log.WriteInfo("TERM ON KB LOCK");
			if (null != m_listener)
			{
				m_listener.TermOnLockKeyboard();
			}
			m_state = ScreenLoadState.START;
			m_kbUnlockEvent.Reset();
			m_event.Set();
		}

		public void TermOnUnlockKeyboard()
		{
			//Log.WriteInfo("TERM ON KB UNLOCK");
			if (null != m_listener)
			{
				m_listener.TermOnUnlockKeyboard();
			}
			m_kbUnlockEvent.Set();
			m_event.Set();
			if (m_state == ScreenLoadState.START)
			{
				m_state = ScreenLoadState.KBUNLOCK;
			}
		}

		public BR.R360 CreateBrR360() {	return new BR.R360(this, m_vt); }
		public BR.S101 CreateBrS101() { return new BR.S101(this, m_vt); }
		public BR.S102 CreateBrS102() { return new BR.S102(this, m_vt); }
		public BR.S103 CreateBrS103() { return new BR.S103(this, m_vt); }
		public BR.S105 CreateBrS105() { return new BR.S105(this, m_vt); }

		public CL.R002 CreateClR002() { return new CL.R002(this, m_vt); }

		public LV.S2401 CreateLvS2401() { return new LV.S2401(this, m_vt); }
		public LV.S2401_2 CreateLvS2401_2() { return new LV.S2401_2(this, m_vt); }
		public LV.S2402 CreateLvS2402() { return new LV.S2402(this, m_vt); }
		public LV.S2402_2 CreateLvS2402_2() { return new LV.S2402_2(this, m_vt); }
		public LV.S2402_3 CreateLvS2402_3() { return new LV.S2402_3(this, m_vt); }
		public LV.S2405 CreateLvS2405() { return new LV.S2405(this, m_vt); }
		public LV.S2410 CreateLvS2410() { return new LV.S2410(this, m_vt); }
		public LV.S2410_2 CreateLvS2410_2() { return new LV.S2410_2(this, m_vt); }
		public LV.S2408 CreateLvS2408() { return new LV.S2408(this, m_vt); }

		public SR.S1301 CreateSrS1301() { return new SR.S1301(this, m_vt); }
		public SR.S1301_2 CreateSrS1301_2() { return new SR.S1301_2(this, m_vt); }
		public SR.S1352 CreateSrS1352() { return new SR.S1352(this, m_vt); }

		public SS.R001 CreateLogonScreen() { return new SS.R001(this, m_vt); }
	}
}
