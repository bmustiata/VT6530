using System;
using System.Collections.Generic;
using System.Text;
using LibVt6530;

namespace DOR.TandemScreens.BR
{
	public class S1750_1 : Screen
	{
		internal S1750_1(Terminal term, Vt6530 vt) : base(term, vt)
		{
			
		}

		public override bool VerifyLocation()
		{
			m_term.WaitEventScreen(false);
			// Check some of the protected fields to ensure 
			// the terminal is on this screen.
			return Footer.Trim().StartsWith("=< 1750 >==< BI >===========< UBI New Account Add >=============<")
				&& Message.Trim() == "Press F1 to add UBI or F2 to CANCEL";
		}

		public override void WaitScreenLoad()
		{
			// Wait for the screen to finish loading.
			base.WaitScreenLoad();
		}

		// ENTER PARTNER NAME(S)                                                           ROW:0 COL:0
		public string Field0
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(0);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 1.   ROW:2 COL:1
		public string Field1
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(1);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// TESTER_______________________________________ ROW:2 COL:6
		public string Field2
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(2);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:2 COL:52
		public string Field3
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(3);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// ** OWNER'S NAME           ROW:2 COL:54
		public string Field4
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(4);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// SSN:  ROW:3 COL:3
		public string Field5
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(5);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 111 ROW:3 COL:9
		public string Field6
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(6);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(6, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		// 11 ROW:3 COL:13
		public string Field7
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(7);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(7, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		// 1111 ROW:3 COL:16
		public string Field8
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(8);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(8, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		//                                                            ROW:3 COL:21
		public string Field9
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(9);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 2.   ROW:4 COL:1
		public string Field10
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(10);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// TEST DBA_____________________________________ ROW:4 COL:6
		public string Field11
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(11);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:4 COL:52
		public string Field12
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(12);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// ** DOING BUSINESS AS      ROW:4 COL:54
		public string Field13
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(13);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 3.   ROW:6 COL:1
		public string Field14
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(14);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// _____________________________________________ ROW:6 COL:6
		public string Partner1
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

		//                             ROW:6 COL:52
		public string Field16
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(16);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// SSN:  ROW:7 COL:3
		public string Field17
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(17);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 000 ROW:7 COL:9
		public string Partner1SsnFirst3
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(18);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(18, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		// 00 ROW:7 COL:13
		public string Partner1SsnMiddle2
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(19);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(19, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		// 0000 ROW:7 COL:16
		public string Partner1SsnLast4
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(20);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(20, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		//                                                            ROW:7 COL:21
		public string Field21
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(21);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 4.   ROW:8 COL:1
		public string Field22
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(22);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// _____________________________________________ ROW:8 COL:6
		public string Partner2
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(23);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(23, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		//                             ROW:8 COL:52
		public string Field24
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(24);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// SSN:  ROW:9 COL:3
		public string Field25
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(25);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 000 ROW:9 COL:9
		public string Partner2SsnFirst3
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(26);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(26, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		// 00 ROW:9 COL:13
		public string Partner2SsnMiddle2
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(27);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(27, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		// 0000 ROW:9 COL:16
		public string Partner2SsnLast4
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(28);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(28, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		//                                                            ROW:9 COL:21
		public string Field29
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(29);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 5.   ROW:10 COL:1
		public string Field30
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(30);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// _____________________________________________ ROW:10 COL:6
		public string Partner3
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(31);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(31, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		//                             ROW:10 COL:52
		public string Field32
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(32);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// SSN:  ROW:11 COL:3
		public string Field33
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(33);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 000 ROW:11 COL:9
		public string Partner3SsnFirst3
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(34);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(34, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		// 00 ROW:11 COL:13
		public string Partner3SsnMiddle2
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(35);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(35, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		// 0000 ROW:11 COL:16
		public string Partner3SsnLast4
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(36);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(36, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		//                                                            ROW:11 COL:21
		public string Field37
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(37);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 6.   ROW:12 COL:1
		public string Field38
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(38);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// _____________________________________________ ROW:12 COL:6
		public string Partner4
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(39);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(39, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		//                             ROW:12 COL:52
		public string Field40
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(40);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// SSN:  ROW:13 COL:3
		public string Field41
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(41);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 000 ROW:13 COL:9
		public string Partner4SsnFirst3
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(42);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(42, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		// 00 ROW:13 COL:13
		public string Partner4SsnMiddle2
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(43);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(43, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		// 0000 ROW:13 COL:16
		public string Partner4SsnLast4
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(44);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(44, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		//                                                            ROW:13 COL:21
		public string Field45
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(45);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 7.   ROW:14 COL:1
		public string Field46
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(46);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// _____________________________________________ ROW:14 COL:6
		public string Partner5
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(47);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(47, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		//                             ROW:14 COL:52
		public string Field48
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(48);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// SSN:  ROW:15 COL:3
		public string Field49
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(49);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 000 ROW:15 COL:9
		public string Partner5SsnFirst3
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(50);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(50, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		// 00 ROW:15 COL:13
		public string Partner5SsnMiddle2
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(51);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(51, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		// 0000 ROW:15 COL:16
		public string Partner5SsnLast4
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(52);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(52, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		//                                                            ROW:15 COL:21
		public string Field53
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(53);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// GENERAL INFORMATION                                                              ROW:16 COL:79
		public string Field54
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(54);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// ______________________________________________________________________________ ROW:18 COL:0
		public string GeneralInformation1
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(55);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(55, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		// ______________________________________________________________________________ ROW:19 COL:0
		public string GeneralInformation2
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(57);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(57, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		//                                                                                  ROW:19 COL:79
		public string Field58
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(58);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// Press F1 to add UBI or F2 to CANCEL                                  ROW:21 COL:0
		public string Message
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(59);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// TEST19    ROW:21 COL:69
		public string Environment
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(60);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// =< 1750 >==< BI >===========< UBI New Account Add >=============< T >==< R110   ROW:22 COL:0
		public string Footer
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(62);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// F1-Add new UBI    F2-Cancel     F5-SCN 1      SF10-Help                        ROW:23 COL:1
		public string Field64
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(64);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// Press F1 to add UBI or F2 to CANCEL                                  ROW:21 COL:0
		public string Field65
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(65);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		public void Submit()
		{
			m_vt.FakeF1();
		}
	}
}
