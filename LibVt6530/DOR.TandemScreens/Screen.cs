using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using LibVt6530;

namespace DOR.TandemScreens
{
	public abstract class Screen
	{
		protected Terminal m_term;
		protected Vt6530 m_vt;

		internal Screen(Terminal term, Vt6530 vt)
		{
			m_term = term;
			m_vt = vt;
		}

		public abstract bool VerifyLocation();

		public virtual void WaitScreenLoad()
		{
			m_term.WaitEventKbUnlock();
			int count = 0;
			while (!VerifyLocation())
			{
				if (count++ > 4)
				{
					throw new Exception("Screen didn't load as expected.\n" + m_term.ScreenText);
				}
				m_term.WaitEvent();
			}
		}

	}

	public class AcessDeignedException : Exception
	{
		public AcessDeignedException(string msg) : base(msg) { }
	}

	public abstract class CommandLineScreen : Screen
	{
		internal CommandLineScreen(Terminal term, Vt6530 vt)
			: base(term, vt)
		{
		}
		
		public abstract string CommandLine { get; set; }

		public abstract void NavigateTo();

		public abstract string Message { get; }

		public void CommandLineExec(string cmd)
		{
			m_term.WaitEventKbUnlock();
			CommandLine = cmd;
			m_vt.FakeF12();
			m_term.WaitEventScreen(true);
			if (VerifyLocation())
			{
				if (Message.Trim().StartsWith("Current Logon ID not authorized to access function"))
				{
					throw new AcessDeignedException(Message);
				}
				Thread.Sleep(1000);
				if (VerifyLocation())
				{
					if (Message.Trim().StartsWith("Current Logon ID not authorized to access function"))
					{
						throw new AcessDeignedException(Message);
					}
					//CommandLine = cmd;
					//m_vt.FakeF12();
					//m_term.WaitEventScreen(true);
					throw new Exception("Cannot navigate to " + cmd);
				}
			}
		}
	}
}
