using System;
using System.Collections.Generic;
using System.Text;
using LibVt6530;

namespace DOR.TandemScreens.SR
{
	public class S3410 : CommandLineScreen
	{
		internal S3410(Terminal term, Vt6530 vt)
			: base(term, vt)
		{

		}

		public override bool VerifyLocation()
		{
			m_term.WaitEventScreen(false);
			// Check some of the protected fields to ensure 
			// the terminal is on this screen.
			return Footer == "=< 1310 >==< SR >=============< Request Browse >================< P >==< R210 > "
				|| Footer == "=< 1310 >==< SR >=============< Request Browse >================< D >==< R210 > "
				|| Footer == "=< 1310 >==< SR >=============< Request Browse >================< T >==< R210 > ";
		}

		public override void NavigateTo()
		{
			// Navigate to the screen.
			DOR.TandemScreens.CL.R002 s000;
			s000 = m_term.CreateClR002();
			s000.NavigateTo();
			s000.WaitScreenLoad();
			// go to 000 screen
			s000.CommandLineExec("1310");
			WaitScreenLoad();
		}

		public override void WaitScreenLoad()
		{
			// Wait for the screen to finish loading.
			base.WaitScreenLoad();
		}

		// Req Num ROW:0 COL:2
		public string Field0
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(0);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     ROW:0 COL:10
		public string Field1
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(1);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// Name ROW:0 COL:14
		public string Field2
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(2);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//                           ROW:0 COL:19
		public string Field3
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(3);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// Sys ROW:0 COL:45
		public string Field4
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(4);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// Div ROW:0 COL:49
		public string Field5
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(5);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// Pri ROW:0 COL:53
		public string Field6
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(6);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// Init Date ROW:0 COL:57
		public string Field7
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(7);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// Status ROW:0 COL:68
		public string Field9
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(9);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//      ROW:0 COL:75
		public string Field10
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(10);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// REQNUM  ROW:1 COL:2
		public string SrNum
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(11);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(11, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		//                                     ROW:1 COL:10
		public string Field12
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(12);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// IN ROW:1 COL:46
		public string SystemCode
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(13);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(13, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		// DI ROW:1 COL:50
		public string DivisionCode
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

		//                ROW:1 COL:53
		public string Field16
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(16);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// STATUS     ROW:1 COL:68
		public string Status
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(17);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(17, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		//   ROW:2 COL:0
		public string Chk1
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

		//    7640 ROW:2 COL:2
		public string SrNum1
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(20);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// SR: WORKSPACE INTEGRATION         ROW:2 COL:11
		public string Title1
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(22);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// IN ROW:2 COL:46
		public string System1
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(24);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  6 ROW:2 COL:50
		public string Division1
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(26);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 99 ROW:2 COL:54
		public string Priority1
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(28);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 11/30/2006 ROW:2 COL:57
		public string Date1
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(29);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// PENDING    ROW:2 COL:68
		public string Status1
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(30);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:3 COL:0
		public string Chk2
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(32);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(32, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		//    7639 ROW:3 COL:2
		public string SrNum2
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(33);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// CHANGE HOW SEND BACKS ARE COUNTED ROW:3 COL:11
		public string Title2
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(35);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// UP ROW:3 COL:46
		public string System2
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(37);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 11 ROW:3 COL:50
		public string Division2
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(39);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 99 ROW:3 COL:54
		public string Priority2
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(41);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 11/29/2006 ROW:3 COL:57
		public string Date2
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(42);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// PENDING    ROW:3 COL:68
		public string Status2
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(43);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:4 COL:0
		public string Chk3
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(45);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(45, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		//    7638 ROW:4 COL:2
		public string SrNum3
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(46);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// DOD DOR SOFT MATCH PROJECT - COMP ROW:4 COL:11
		public string Title3
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(48);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// DW ROW:4 COL:46
		public string System3
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(50);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  6 ROW:4 COL:50
		public string Division3
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(52);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  1 ROW:4 COL:54
		public string Priority3
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(54);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 11/29/2006 ROW:4 COL:57
		public string Date3
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(55);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// PENDING    ROW:4 COL:68
		public string Status3
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(56);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:5 COL:0
		public string Chk4
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(58);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(58, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		//    7637 ROW:5 COL:2
		public string SrNum4
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(59);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// SR: REET - ENHANCEMENTS           ROW:5 COL:11
		public string Title4
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(61);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// IN ROW:5 COL:46
		public string System4
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(63);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  6 ROW:5 COL:50
		public string Division4
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(65);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 99 ROW:5 COL:54
		public string Priority4
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(67);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 11/29/2006 ROW:5 COL:57
		public string Date4
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(68);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// PENDING    ROW:5 COL:68
		public string Status4
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(69);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:6 COL:0
		public string Chk5
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(71);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(71, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		//    7636 ROW:6 COL:2
		public string SrNum5
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(72);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// REGION 11 NONREPORTING OSR DELETE ROW:6 COL:11
		public string Title5
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(74);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// AC ROW:6 COL:46
		public string System5
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(76);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  9 ROW:6 COL:50
		public string Division5
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(78);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  2 ROW:6 COL:54
		public string Priority5
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(80);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 11/28/2006 ROW:6 COL:57
		public string Date5
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(81);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// PENDING    ROW:6 COL:68
		public string Status5
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(82);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:7 COL:0
		public string Chk6
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(84);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(84, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		//    7635 ROW:7 COL:2
		public string SrNum7
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(85);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// CORRECT INVOLUNTARY PERIOD UPDATE ROW:7 COL:11
		public string Title7
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(87);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// BR ROW:7 COL:46
		public string System7
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(89);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 10 ROW:7 COL:50
		public string Division7
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(91);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  3 ROW:7 COL:54
		public string Priority7
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(93);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 11/27/2006 ROW:7 COL:57
		public string Date7
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(94);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// PENDING    ROW:7 COL:68
		public string Status7
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(95);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:8 COL:0
		public string Chk8
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(97);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(97, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		//    7634 ROW:8 COL:2
		public string SrNum8
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(98);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// DMG ADMINISTRATIVE 2006-Q4        ROW:8 COL:11
		public string Title8
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(100);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// ZU ROW:8 COL:46
		public string System8
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(102);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  6 ROW:8 COL:50
		public string Division8
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(104);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 99 ROW:8 COL:54
		public string Priority8
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(106);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 11/22/2006 ROW:8 COL:57
		public string Date8
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(107);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// IN PROGRES ROW:8 COL:68
		public string Status8
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(108);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:9 COL:0
		public string Chk9
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(110);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(110, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		//    7633 ROW:9 COL:2
		public string SrNum9
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(111);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// DMG PRODUCTION PROBLEMS 2006-Q4   ROW:9 COL:11
		public string Title9
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(113);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// ZU ROW:9 COL:46
		public string System9
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(115);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  6 ROW:9 COL:50
		public string Division9
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(117);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 99 ROW:9 COL:54
		public string Priority9
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(119);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 11/22/2006 ROW:9 COL:57
		public string Date9
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(120);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// IN PROGRES ROW:9 COL:68
		public string Status9
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(121);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:10 COL:0
		public string Chk10
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(123);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(123, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		//    7632 ROW:10 COL:2
		public string SrNum10
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(124);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// DMG SYSTEM SUPPORT 2006-Q4        ROW:10 COL:11
		public string Title10
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(126);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// ZU ROW:10 COL:46
		public string System10
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(128);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  6 ROW:10 COL:50
		public string Division10
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(130);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 99 ROW:10 COL:54
		public string Priority10
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(132);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 11/22/2006 ROW:10 COL:57
		public string Date10
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(133);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// IN PROGRES ROW:10 COL:68
		public string Status10
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(134);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:11 COL:0
		public string Chk11
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(136);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(136, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		//    7631 ROW:11 COL:2
		public string SrNum11
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(137);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// ACCOUNT INFO INDICATOR FOR SEMICO ROW:11 COL:11
		public string Title11
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(139);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// BR ROW:11 COL:46
		public string System11
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(141);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 10 ROW:11 COL:50
		public string Division11
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(143);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  1 ROW:11 COL:54
		public string Priority11
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(145);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 11/22/2006 ROW:11 COL:57
		public string Date11
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(146);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// PRE ANLYS  ROW:11 COL:68
		public string Status11
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(147);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:12 COL:0
		public string Chk12
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(149);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(149, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		//    7630 ROW:12 COL:2
		public string SrNum12
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(150);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// FIVE DAY GRACE PERIOD BEFORE PENA ROW:12 COL:11
		public string Title12
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(152);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// ET ROW:12 COL:46
		public string System12
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(154);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 10 ROW:12 COL:50
		public string Divsion12
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(156);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  4 ROW:12 COL:54
		public string Priority12
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(158);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 11/22/2006 ROW:12 COL:57
		public string Date12
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(159);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// PENDING    ROW:12 COL:68
		public string Status12
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(160);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:13 COL:0
		public string Chk13
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(162);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(162, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		//    7629 ROW:13 COL:2
		public string SrNum13
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(163);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// CORRECTION TO MAILING ADDRESS FOR ROW:13 COL:11
		public string Title13
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(165);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// UP ROW:13 COL:46
		public string System13
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(167);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 11 ROW:13 COL:50
		public string Division13
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(169);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 99 ROW:13 COL:54
		public string Priority13
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(171);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 11/22/2006 ROW:13 COL:57
		public string Date13
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(172);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// PENDING    ROW:13 COL:68
		public string Status13
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(173);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:14 COL:0
		public string Chk14
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(175);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(175, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		//    7628 ROW:14 COL:2
		public string SrNum14
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(176);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// BUSINSS OBJECTS REPORT SUPPORT -  ROW:14 COL:11
		public string Title14
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(178);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// DW ROW:14 COL:46
		public string System14
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(180);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  6 ROW:14 COL:50
		public string Division14
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(182);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  1 ROW:14 COL:54
		public string Priority14
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(184);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 11/21/2006 ROW:14 COL:57
		public string Date14
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(185);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// PENDING    ROW:14 COL:68
		public string Status14
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(186);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:15 COL:0
		public string Chk15
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(188);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(188, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		//    7627 ROW:15 COL:2
		public string SrNum15
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(189);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// UPDATE TRADIM COUNTY CITY LOC COD ROW:15 COL:11
		public string Title15
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(191);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// DW ROW:15 COL:46
		public string System15
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(193);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  6 ROW:15 COL:50
		public string Divsion15
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(195);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  2 ROW:15 COL:54
		public string Priority15
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(197);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 11/20/2006 ROW:15 COL:57
		public string Date15
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(198);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// IN PROGRES ROW:15 COL:68
		public string Status15
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(199);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:16 COL:0
		public string Chk16
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(201);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(201, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		//    7626 ROW:16 COL:2
		public string SrNum16
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(202);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// ADD RESPONSE INDICATOR TO MTGCONT ROW:16 COL:11
		public string Title16
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(204);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// ET ROW:16 COL:46
		public string System16
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(206);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 14 ROW:16 COL:50
		public string Division16
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(208);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  1 ROW:16 COL:54
		public string Priority16
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(210);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 11/21/2006 ROW:16 COL:57
		public string Date16
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(211);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// PENDING    ROW:16 COL:68
		public string Status16
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(212);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:17 COL:0
		public string Chk17
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(214);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(214, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		//    7625 ROW:17 COL:2
		public string SrNum17
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(215);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// LOCAL LODGING CHANGE FOR Q1/2007  ROW:17 COL:11
		public string Title17
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(217);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// EF ROW:17 COL:46
		public string System17
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(219);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 10 ROW:17 COL:50
		public string Division17
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(221);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  1 ROW:17 COL:54
		public string Priority17
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(223);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 11/20/2006 ROW:17 COL:57
		public string Date17
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(224);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// PENDING    ROW:17 COL:68
		public string Status17
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(225);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:18 COL:0
		public string Chk18
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(227);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(227, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		//    7624 ROW:18 COL:2
		public string SrNum18
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(228);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// LOCAL LODGING CHANGE FOR Q1/2007  ROW:18 COL:11
		public string Title18
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(230);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// ET ROW:18 COL:46
		public string System18
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(232);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 10 ROW:18 COL:50
		public string Divsion18
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(234);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  1 ROW:18 COL:54
		public string Priority18
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(236);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 11/20/2006 ROW:18 COL:57
		public string Date18
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(237);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// PENDING    ROW:18 COL:68
		public string Status18
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(238);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:19 COL:0
		public string Chk19
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(240);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(240, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		//    7623 ROW:19 COL:2
		public string SrNum19
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(241);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// FMLA LEAVE ENTRY                  ROW:19 COL:11
		public string Title19
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(243);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// AL ROW:19 COL:46
		public string System19
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(245);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  5 ROW:19 COL:50
		public string Division19
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(247);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 99 ROW:19 COL:54
		public string Priority19
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(249);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 11/16/2006 ROW:19 COL:57
		public string Date19
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(250);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// PRE ANLYS  ROW:19 COL:68
		public string Status19
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(251);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//                                                                      ROW:20 COL:0
		public override string Message
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(253);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// PROD0W    ROW:20 COL:69
		public string Environment
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(254);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// =< 1310 >==< SR >=============< Request Browse >================< P >==< R210 >  ROW:20 COL:79
		public string Footer
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(255);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// F1-List  F2-Detail  F3-Anlysis  F6-Pg Fwd SF6-Pg Bwd  ROW:22 COL:0
		public string Menu1
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(256);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// SF10-Help  F12-Menu/GoTo  ROW:22 COL:54
		public string Menu2
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(257);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//                                                                                ROW:23 COL:0
		public override string CommandLine
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(258);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(258, out col, out row);
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
