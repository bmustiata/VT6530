using System;
using System.Collections.Generic;
using System.Diagnostics;
using LibVt6530;

namespace DOR.TandemScreens.SS
{
	public class R001 : Screen
	{
		public R001(Terminal term, Vt6530 vt) : base(term, vt)
		{
		}

		public override bool VerifyLocation()
		{
			m_term.WaitEventScreen(false);
			return Footer == "=< SS >====================< Revenue Logon Screen >====================< R001 > ";
		}

		// W A S H I N G T O N    S T A T E                         
		public string Header1
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(0);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// D E P A R T M E N T    O F    R E V E N U E                   
		public string Header2
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(1);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 11/13/06
		public string TodaysDate
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(2);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// Logon ID: 
		public string LblLogonId
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(4);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// Logon   
		public string LogonId
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(5);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int x; int y;
				m_vt.Display().GetDisplayPage().GetFieldXY(5, out x, out y);
				m_vt.Display().SetCursorRowCol(y, x);
				m_vt.Display().WriteLocal(value);
			}
		}

		// Password: 
		public string LblPassword
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(7);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// Password                        
		public string Password
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(8);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int x; int y;
				m_vt.Display().GetDisplayPage().GetFieldXY(8, out x, out y);
				m_vt.Display().SetCursorRowCol(y, x);
				m_vt.Display().WriteLocal(value);
			}
		}

		// PROD0F  
		public string Environment
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(14);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// =< SS >====================< Revenue Logon Screen >====================< R001 > 
		public string Footer
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(15);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// F1-Logon                     F2-Change Password                       F12-Exit 
		public string Menu
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(16);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		public void Submit()
		{
			m_vt.FakeF1();
		}
	}
}
