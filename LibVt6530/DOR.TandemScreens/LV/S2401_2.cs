using System;
using System.Collections.Generic;
using System.Diagnostics;
using LibVt6530;

namespace DOR.TandemScreens.LV
{
	public class S2401_2 : CommandLineScreen
	{
		internal S2401_2(Terminal term, Vt6530 vt)
			: base(term, vt)
		{

		}

		public override bool VerifyLocation()
		{
			m_term.WaitEventScreen(false);
			// Check some of the protected fields to ensure 
			// the terminal is on this screen.
			return Footer == "=< 2401 >==< AL >========< AUTOMATED LEAVE Main Menu >==========< P >==< R010 > "
				|| Footer == "=< 2401 >==< AL >========< AUTOMATED LEAVE Main Menu >==========< D >==< R010 > "
				|| Footer == "=< 2401 >==< AL >========< AUTOMATED LEAVE Main Menu >==========< T >==< R010 > "
				|| Footer == "=< 2439 >==< AL >================< MAIN MENU >==================< P >==< R010 > "
				|| Footer == "=< 2439 >==< AL >================< MAIN MENU >==================< D >==< R010 > "
				|| Footer == "=< 2439 >==< AL >================< MAIN MENU >==================< T >==< R010 > "
				|| Footer == "=< 2401 >==< AL >================< Main Menu >==================< P >==< R010 > "
				|| Footer == "=< 2401 >==< AL >================< Main Menu >==================< D >==< R010 > "
				|| Footer == "=< 2401 >==< AL >================< Main Menu >==================< T >==< R010 > ";
		}

		public override void NavigateTo()
		{
			// Navigate to the screen.
			throw new NotImplementedException();
		}

		public void NavigateTo(string ssnLast4)
		{
			DOR.TandemScreens.CL.R002 s000;
			DOR.TandemScreens.LV.S2401 s2401;
			DOR.TandemScreens.LV.S2401_2 s2401_2;
			s000 = m_term.CreateClR002();
			s2401 = m_term.CreateLvS2401();
			s2401_2 = m_term.CreateLvS2401_2();
			
			s000.NavigateTo();
			s000.WaitScreenLoad();
			// go to 2401 screen
			s000.CommandLineExec("2401");
			s2401.WaitScreenLoad();
			s2401.SsnLastFour = ssnLast4;
			s2401.Submit();
			m_term.WaitEventScreen(true);
			int count = 0;
			while (!s2401_2.VerifyLocation() && s2401.Message.Trim().Length == 0)
				//s2401.SsnCaption.Trim() == "Enter Last Four Digits of Your SSN:")
			{
				// ( == "Social security number entered is incorrect. Access Denied.")
				if (count++ > 4)
				{
					throw new Exception("Can't navigate to 2401_2");
				}
				m_term.WaitEvent();
			}
		}

		// AUTOMATED LEAVE SYSTEM MAIN MENU                           ROW:0 COL:21
		public string Field0
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(0);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// EMPLOYEES:                         SUPERVISORS:                               ROW:2 COL:2
		public string Field1
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(1);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 2402  Request Leave                2410  Approve Leave                       ROW:3 COL:3
		public string Field2
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(2);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 2403  View/Cancel Requests         2411  View/Cancel Requests                ROW:4 COL:3
		public string Field3
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(3);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 2404  View Leave Bal By Month      2412  Delegate Approval Authority         ROW:5 COL:3
		public string Field4
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(4);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 2405  Projected Leave Balances     2413  View Leave Bal By Month             ROW:6 COL:3
		public string Field5
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(5);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 2406  View Rates of Accrual        2414  Request Leave For An Employee       ROW:7 COL:3
		public string Field6
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(6);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 2407  Sick Leave Buy Out           2415  View Current Bal By Attd Unit       ROW:8 COL:3
		public string Field7
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(7);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 2408  View Work Schedule           2416  View Employee Work Schedule         ROW:9 COL:3
		public string Field8
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(8);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 2417  View Leave For Attendance Unit      ROW:10 COL:38
		public string Field9
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(9);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// PAYROLL:                                                                      ROW:12 COL:2
		public string Field10
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(10);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 2420  Reports                                                                ROW:13 COL:3
		public string Field11
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(11);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 2421  Maintenance Of Leave Balances                                          ROW:14 COL:3
		public string Field12
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(12);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 2435  Employee Leave Reports                                                 ROW:15 COL:3
		public string Field13
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(13);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// ENTER SELECTION: ROW:17 COL:17
		public string Field14
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(14);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//      ROW:17 COL:34
		public string ScreenNum
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(15);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(15, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		//                                          ROW:17 COL:39
		public string Field16
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(16);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//                                                                      ROW:19 COL:0
		public override string Message
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(17);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// PROD0H    ROW:19 COL:69
		public string Environment
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(18);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// =< 2401 >==< AL >========< AUTOMATED LEAVE Main Menu >==========< P >==< R010 >  ROW:19 COL:79
		public string Footer
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(19);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// F1-Enter                                                       F12-Menu/GoTo    ROW:21 COL:0
		public string Field20
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(20);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// SF9-Pause   SF10-Help   SF12-System Menu  ROW:22 COL:38
		public string Field21
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(21);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//                                                                                ROW:23 COL:0
		public override string CommandLine
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(22);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(22, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		public void Submit()
		{
			m_vt.FakeF1();
		}
	}
}
