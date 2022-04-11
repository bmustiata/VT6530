using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using LibVt6530;
using DOR.TandemScreens;
using DOR.TandemScreens.CL;

namespace DOR.TandemScreens.LV
{
	public struct ApproveLeaveInfo
	{
		public int checknum;
		public string requester;
		public string from;
		public string to;
		public string leavetype;
		public string action;
	}

	public class S2410 : CommandLineScreen
	{
		internal S2410(Terminal term, Vt6530 vt)
			: base(term, vt)
		{
			m_vt = vt;
		}

		public override bool VerifyLocation()
		{
			m_term.WaitEventScreen(false);
			// Check some of the protected fields to ensure 
			// the terminal is on this screen.
			return Footer == "=< 2410 >==< AL >=============< Leave Approval >================< P >==< R200 > "
				|| Footer == "=< 2410 >==< AL >=============< Leave Approval >================< D >==< R200 > "
				|| Footer == "=< 2410 >==< AL >=============< Leave Approval >================< T >==< R200 > ";
		}

		public override void NavigateTo()
		{
			throw new NotImplementedException();
		}

		public void NavigateTo(string ssnLast4)
		{
			R002 r002 = m_term.CreateClR002();
			S2401 s2401 = m_term.CreateLvS2401();
			S2401_2 s2401_2 = m_term.CreateLvS2401_2();

			r002.NavigateTo();

			// now go to the 2401 screen (SSN)
			r002.CommandLineExec("2401");
			s2401.WaitScreenLoad();

			// send the SSN
			int count = 0;
			while (s2401.SsnCaption.Trim() == "Enter Last Four Digits of Your SSN:")
			{
				if (s2401.VerifyLocation() && (count % 2) == 0)
				{
					s2401.SsnLastFour = ssnLast4;
					s2401.Submit();
					m_term.WaitEventScreen(true);
				}
				else if (s2401.VerifyLocation())
				{
					Thread.Sleep(300);
				}
				if (s2401_2.VerifyLocation())
				{
					break;
				}
				if (s2401.Message.Trim() == "Social security number entered is incorrect. Access Denied.")
				{
					throw new Exception(s2401.Message);
				}
				if (count++ > 5)
				{
					throw new Exception("Can't load 2401");
				}
			}
			if (m_term.FieldCount < 14)
			{
				throw new Exception("Can't load 2401_2");
			}
			s2401_2.CommandLineExec("2410");
			WaitScreenLoad();
		}

		public override void WaitScreenLoad()
		{
			base.WaitScreenLoad();
		}

		public List<ApproveLeaveInfo> Load()
		{
			if ( ! VerifyLocation() )
			{
				return null;
			}
			ApproveLeaveInfo ali;
			List<ApproveLeaveInfo> l = new List<ApproveLeaveInfo>();

			ali.action = RequestType1.Trim();
			ali.checknum = 1;
			ali.from = DateFrom1.Trim();
			ali.leavetype = LeaveType1.Trim();
			ali.requester = NameLastFirst1.Trim();
			ali.to = DateTo1.Trim();
			if (ali.requester.Length > 0) l.Add(ali);

			ali.action = RequestType2.Trim();
			ali.checknum = 2;
			ali.from = DateFrom2.Trim();
			ali.leavetype = LeaveType2.Trim();
			ali.requester = NameLastFirst2.Trim();
			ali.to = DateTo2.Trim();
			if (ali.requester.Length > 0) l.Add(ali);

			ali.action = RequestType3.Trim();
			ali.checknum = 3;
			ali.from = DateFrom3.Trim();
			ali.leavetype = LeaveType3.Trim();
			ali.requester = NameLastFirst3.Trim();
			ali.to = DateTo3.Trim();
			if (ali.requester.Length > 0) l.Add(ali);

			ali.action = RequestType4.Trim();
			ali.checknum = 4;
			ali.from = DateFrom4.Trim();
			ali.leavetype = LeaveType4.Trim();
			ali.requester = NameLastFirst4.Trim();
			ali.to = DateTo4.Trim();
			if (ali.requester.Length > 0) l.Add(ali);

			ali.action = RequestType5.Trim();
			ali.checknum = 5;
			ali.from = DateFrom5.Trim();
			ali.leavetype = LeaveType5.Trim();
			ali.requester = NameLastFirst5.Trim();
			ali.to = DateTo5.Trim();
			if (ali.requester.Length > 0) l.Add(ali);

			ali.action = RequestType6.Trim();
			ali.checknum = 6;
			ali.from = DateFrom6.Trim();
			ali.leavetype = LeaveType6.Trim();
			ali.requester = NameLastFirst6.Trim();
			ali.to = DateTo6.Trim();
			if (ali.requester.Length > 0) l.Add(ali);

			ali.action = RequestType7.Trim();
			ali.checknum = 7;
			ali.from = DateFrom7.Trim();
			ali.leavetype = LeaveType7.Trim();
			ali.requester = NameLastFirst7.Trim();
			ali.to = DateTo7.Trim();
			if (ali.requester.Length > 0) l.Add(ali);

			ali.action = RequestType8.Trim();
			ali.checknum = 8;
			ali.from = DateFrom8.Trim();
			ali.leavetype = LeaveType8.Trim();
			ali.requester = NameLastFirst8.Trim();
			ali.to = DateTo8.Trim();
			if (ali.requester.Length > 0) l.Add(ali);

			ali.action = RequestType9.Trim();
			ali.checknum = 9;
			ali.from = DateFrom9.Trim();
			ali.leavetype = LeaveType9.Trim();
			ali.requester = NameLastFirst9.Trim();
			ali.to = DateTo9.Trim();
			if (ali.requester.Length > 0) l.Add(ali);

			ali.action = RequestType10.Trim();
			ali.checknum = 10;
			ali.from = DateFrom10.Trim();
			ali.leavetype = LeaveType10.Trim();
			ali.requester = NameLastFirst10.Trim();
			ali.to = DateTo10.Trim();
			if (ali.requester.Length > 0) l.Add(ali);

			ali.action = RequestType11.Trim();
			ali.checknum = 11;
			ali.from = DateFrom11.Trim();
			ali.leavetype = LeaveType11.Trim();
			ali.requester = NameLastFirst11.Trim();
			ali.to = DateTo11.Trim();
			if (ali.requester.Length > 0) l.Add(ali);

			ali.action = RequestType12.Trim();
			ali.checknum = 12;
			ali.from = DateFrom12.Trim();
			ali.leavetype = LeaveType12.Trim();
			ali.requester = NameLastFirst12.Trim();
			ali.to = DateTo12.Trim();
			if (ali.requester.Length > 0) l.Add(ali);

			return l;
		}

		public void Select(int index)
		{
			switch (index)
			{
				case 1:
					Check1 = "X";
					Debug.Assert(NameLastFirst1.Trim().Length > 0);
					break;
				case 2:
					Check2 = "X";
					Debug.Assert(NameLastFirst2.Trim().Length > 0);
					break;
				case 3:
					Check3 = "X";
					Debug.Assert(NameLastFirst3.Trim().Length > 0);
					break;
				case 4:
					Check4 = "X";
					Debug.Assert(NameLastFirst4.Trim().Length > 0);
					break;
				case 5:
					Check5 = "X";
					Debug.Assert(NameLastFirst5.Trim().Length > 0);
					break;
				case 6:
					Check6 = "X";
					Debug.Assert(NameLastFirst6.Trim().Length > 0);
					break;
				case 7:
					Check7 = "X";
					Debug.Assert(NameLastFirst7.Trim().Length > 0);
					break;
				case 8:
					Check8 = "X";
					Debug.Assert(NameLastFirst8.Trim().Length > 0);
					break;
				case 9:
					Check9 = "X";
					Debug.Assert(NameLastFirst9.Trim().Length > 0);
					break;
				case 10:
					Check10 = "X";
					Debug.Assert(NameLastFirst10.Trim().Length > 0);
					break;
				case 11:
					Check11 = "X";
					Debug.Assert(NameLastFirst11.Trim().Length > 0);
					break;
				case 12:
					Check12 = "X";
					Debug.Assert(NameLastFirst12.Trim().Length > 0);
					break;
				default:
					throw new Exception("Index must be in the range [1,12]");
			}
		}

		// Type 'X' next to the request you want to take action on.
		public string Field0
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(0);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//              
		public string Field1
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(1);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// REQUESTED                    LEAVE    FROM         TO
		public string Field2
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(2);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//                   
		public string Field3
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(3);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// BY                          TYPE    DATE        DATE         ACTION
		public string Field4
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(4);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   
		public string Field5
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(5);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// x
		public string Check1
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(6);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int x; int y;
				m_vt.Display().GetDisplayPage().GetFieldXY(6, out x, out y);
				m_vt.Display().SetCursorRowCol(y, x);
				m_vt.Display().WriteLocal(value);
			}
		}

		//   
		public string Field7
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(7);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// DUNNAGAN JASON                
		public string NameLastFirst1
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(8);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// SICK
		public string LeaveType1
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(10);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 11/13/2006
		public string DateFrom1
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(12);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 11/13/2006
		public string DateTo1
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(14);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  
		public string Field15
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(15);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// REQ      
		public string RequestType1
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(16);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  
		public string Field17
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(17);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// x
		public string Check2
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(18);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int x; int y;
				m_vt.Display().GetDisplayPage().GetFieldXY(18, out x, out y);
				m_vt.Display().SetCursorRowCol(y, x);
				m_vt.Display().WriteLocal(value);
			}
		}

		//   
		public string Field19
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(19);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// DUNNAGAN JASON                
		public string NameLastFirst2
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(20);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// ANNL
		public string LeaveType2
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(22);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 11/17/2006
		public string DateFrom2
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(24);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 11/17/2006
		public string DateTo2
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(26);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  
		public string Field27
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(27);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// REQ      
		public string RequestType2
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(28);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  
		public string Field29
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(29);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  
		public string Check3
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(30);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int x; int y;
				m_vt.Display().GetDisplayPage().GetFieldXY(30, out x, out y);
				m_vt.Display().SetCursorRowCol(y, x);
				m_vt.Display().WriteLocal(value);
			}
		}

		//   
		public string Field31
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(31);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//                               
		public string NameLastFirst3
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(32);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     
		public string LeaveType3
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(34);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//           
		public string DateFrom3
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(36);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//           
		public string DateTo3
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(38);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  
		public string Field39
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(39);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//          
		public string RequestType3
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(40);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  
		public string Field41
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(41);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  
		public string Check4
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(42);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int x; int y;
				m_vt.Display().GetDisplayPage().GetFieldXY(42, out x, out y);
				m_vt.Display().SetCursorRowCol(y, x);
				m_vt.Display().WriteLocal(value);
			}
		}

		//   
		public string Field43
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(43);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//                               
		public string NameLastFirst4
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(44);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     
		public string LeaveType4
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(46);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//           
		public string DateFrom4
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(48);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//           
		public string DateTo4
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(50);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  
		public string Field51
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(51);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//          
		public string RequestType4
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(52);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  
		public string Field53
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(53);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  
		public string Check5
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(54);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int x; int y;
				m_vt.Display().GetDisplayPage().GetFieldXY(54, out x, out y);
				m_vt.Display().SetCursorRowCol(y, x);
				m_vt.Display().WriteLocal(value);
			}
		}

		//   
		public string Field55
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(55);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//                               
		public string NameLastFirst5
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(56);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     
		public string LeaveType5
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(58);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//           
		public string DateFrom5
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(60);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//           
		public string DateTo5
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(62);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  
		public string Field63
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(63);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//          
		public string RequestType5
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(64);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  
		public string Field65
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(65);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  
		public string Check6
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(66);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int x; int y;
				m_vt.Display().GetDisplayPage().GetFieldXY(66, out x, out y);
				m_vt.Display().SetCursorRowCol(y, x);
				m_vt.Display().WriteLocal(value);
			}
		}

		//   
		public string Field67
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(67);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//                               
		public string NameLastFirst6
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(68);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     
		public string LeaveType6
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(70);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//           
		public string DateFrom6
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(72);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//           
		public string DateTo6
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(74);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  
		public string Field75
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(75);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//          
		public string RequestType6
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(76);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  
		public string Field77
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(77);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  
		public string Check7
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(78);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int x; int y;
				m_vt.Display().GetDisplayPage().GetFieldXY(78, out x, out y);
				m_vt.Display().SetCursorRowCol(y, x);
				m_vt.Display().WriteLocal(value);
			}
		}

		//   
		public string Field79
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(79);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//                               
		public string NameLastFirst7
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(80);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     
		public string LeaveType7
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(82);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//           
		public string DateFrom7
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(84);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//           
		public string DateTo7
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(86);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  
		public string Field87
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(87);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//          
		public string RequestType7
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(88);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  
		public string Field89
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(89);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  
		public string Check8
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(90);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int x; int y;
				m_vt.Display().GetDisplayPage().GetFieldXY(90, out x, out y);
				m_vt.Display().SetCursorRowCol(y, x);
				m_vt.Display().WriteLocal(value);
			}
		}

		//   
		public string Field91
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(91);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//                               
		public string NameLastFirst8
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(92);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     
		public string LeaveType8
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(94);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//           
		public string DateFrom8
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(96);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//           
		public string DateTo8
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(98);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  
		public string Field99
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(99);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//          
		public string RequestType8
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(100);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  
		public string Field101
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(101);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  
		public string Check9
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(102);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int x; int y;
				m_vt.Display().GetDisplayPage().GetFieldXY(102, out x, out y);
				m_vt.Display().SetCursorRowCol(y, x);
				m_vt.Display().WriteLocal(value);
			}
		}

		//   
		public string Field103
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(103);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//                               
		public string NameLastFirst9
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(104);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     
		public string LeaveType9
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(106);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//           
		public string DateFrom9
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(108);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//           
		public string DateTo9
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(110);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  
		public string Field111
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(111);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//          
		public string RequestType9
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(112);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  
		public string Field113
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(113);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  
		public string Check10
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(114);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int x; int y;
				m_vt.Display().GetDisplayPage().GetFieldXY(114, out x, out y);
				m_vt.Display().SetCursorRowCol(y, x);
				m_vt.Display().WriteLocal(value);
			}
		}

		//   
		public string Field115
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(115);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//                               
		public string NameLastFirst10
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(116);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     
		public string LeaveType10
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(118);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//           
		public string DateFrom10
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(120);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//           
		public string DateTo10
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(122);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  
		public string Field123
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(123);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//          
		public string RequestType10
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(124);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  
		public string Field125
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(125);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  
		public string Check11
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(126);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int x; int y;
				m_vt.Display().GetDisplayPage().GetFieldXY(126, out x, out y);
				m_vt.Display().SetCursorRowCol(y, x);
				m_vt.Display().WriteLocal(value);
			}
		}

		//   
		public string Field127
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(127);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//                               
		public string NameLastFirst11
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(128);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     
		public string LeaveType11
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(130);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//           
		public string DateFrom11
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(132);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//           
		public string DateTo11
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(134);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  
		public string Field135
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(135);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//          
		public string RequestType11
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(136);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  
		public string Field137
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(137);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  
		public string Check12
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(138);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int x; int y;
				m_vt.Display().GetDisplayPage().GetFieldXY(138, out x, out y);
				m_vt.Display().SetCursorRowCol(y, x);
				m_vt.Display().WriteLocal(value);
			}
		}

		//   
		public string Field139
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(139);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//                               
		public string NameLastFirst12
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(140);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     
		public string LeaveType12
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(142);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//           
		public string DateFrom12
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(144);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//           
		public string DateTo12
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(146);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  
		public string Field147
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(147);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//          
		public string RequestType12
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(148);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  
		public string Field149
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(149);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		public override string Message
		{
			get { return LastPageInd;  }
		}

		// This is the last page.                                              
		public string LastPageInd
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(150);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// PROD09   
		public string Environment
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(151);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// =< 2410 >==< AL >=============< Leave Approval >================< P >==< R200 > 
		public string Footer
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(152);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// F1-Enter                                           SF9-Pause    F12-Menu/GoTo  
		public string Menu1
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(153);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// F6-Page Frwd           SF6-Page Bkwd               SF10-Help   SF12-System Men 
		public string Menu2
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(154);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// Command Line                                                                  
		public override string CommandLine
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(155);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int x; int y;
				m_vt.Display().GetDisplayPage().GetFieldXY(155, out x, out y);
				m_vt.Display().SetCursorRowCol(y, x);
				m_vt.Display().WriteLocal(value);
			}
		}

		public void Submit()
		{
			m_vt.FakeF1();
		}
	}
}
