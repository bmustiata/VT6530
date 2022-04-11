using System;
using System.Collections.Generic;
using System.Text;
using LibVt6530;

namespace DOR.TandemScreens.SR
{
	public class S1304 : CommandLineScreen
	{
		internal S1304(Terminal term, Vt6530 vt)
			: base(term, vt)
		{

		}

		public override bool VerifyLocation()
		{
			m_term.WaitEventScreen(false);
			// Check some of the protected fields to ensure 
			// the terminal is on this screen.
			return Footer == "=< 1304 >==< SR >=============< Time Log Browse >===============< P >==< R104 > "
				|| Footer == "=< 1304 >==< SR >=============< Time Log Browse >===============< D >==< R104 > "
				|| Footer == "=< 1304 >==< SR >=============< Time Log Browse >===============< T >==< R104 > ";
		}

		public override void NavigateTo()
		{
			// Navigate to the screen.
			DOR.TandemScreens.CL.R002 s000;
			s000 = m_term.CreateClR002();
			s000.NavigateTo();
			s000.WaitScreenLoad();
			// go to 000 screen
			s000.CommandLineExec("1304");
			WaitScreenLoad();
		}

		public override void WaitScreenLoad()
		{
			// Wait for the screen to finish loading.
			base.WaitScreenLoad();
		}

		// Date ROW:0 COL:5
		public string Field0
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(0);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//      ROW:0 COL:10
		public string Field1
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(1);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// Req # ROW:0 COL:15
		public string Field2
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(2);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// Phase ROW:0 COL:22
		public string Field4
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(4);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:0 COL:28
		public string Field5
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(5);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// Hours ROW:0 COL:30
		public string Field6
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(6);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:0 COL:36
		public string Field7
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(7);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// Comments ROW:0 COL:38
		public string Field8
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(8);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//                      ROW:0 COL:47
		public string Field9
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(9);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// User ID ROW:0 COL:68
		public string Field10
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(10);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     ROW:0 COL:76
		public string Field11
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(11);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// DATE       ROW:1 COL:2
		public string FilterDate
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(12);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(12, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		// REQNUM  ROW:1 COL:13
		public string FilterSrNum
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

		// PH?A ROW:1 COL:22
		public string FilterPhase
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

		//                                        ROW:1 COL:27
		public string Field16
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(16);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 000006247 ROW:1 COL:66
		public string UserIdPad9Digits
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

		//     ROW:1 COL:76
		public string Field18
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(18);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// JOHN                 ROW:2 COL:36
		public string FirstName
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(19);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// GARRISON             ROW:2 COL:57
		public string LastName
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(20);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:2 COL:78
		public string Field21
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(21);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// X ROW:3 COL:0
		public string Chk1
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

		// 10/28/2003 ROW:3 COL:2
		public string Date1
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(23);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//    5267 ROW:3 COL:13
		public string SrNum1
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(24);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 6000 ROW:3 COL:22
		public string Phase1
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(26);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     1.50 ROW:3 COL:27
		public string Hours1
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(27);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// Dentist                                   ROW:3 COL:37
		public string Note1
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(29);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// X ROW:4 COL:0
		public string Chk2
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

		// 10/28/2003 ROW:4 COL:2
		public string Date2
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(32);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//    5269 ROW:4 COL:13
		public string SrNum2
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(33);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 6000 ROW:4 COL:22
		public string Phase2
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(35);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     2.00 ROW:4 COL:27
		public string Hours2
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(36);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//                                           ROW:4 COL:37
		public string Note2
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(38);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// X ROW:5 COL:0
		public string Chk3
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(40);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(40, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		// 10/28/2003 ROW:5 COL:2
		public string Date3
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(41);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//    5348 ROW:5 COL:13
		public string SrNum3
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(42);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 6000 ROW:5 COL:22
		public string Phase3
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(44);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     2.50 ROW:5 COL:27
		public string Hours3
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(45);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// FW: Rescheduled HR Reform Meeting         ROW:5 COL:37
		public string Note3
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(47);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// X ROW:6 COL:0
		public string Chk4
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(49);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(49, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		// 10/27/2003 ROW:6 COL:2
		public string Date4
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(50);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//    5267 ROW:6 COL:13
		public string SrNum4
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(51);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 6000 ROW:6 COL:22
		public string Phase4
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(53);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     1.00 ROW:6 COL:27
		public string Hours4
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(54);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//                                           ROW:6 COL:37
		public string Note4
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(56);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// X ROW:7 COL:0
		public string Chk5
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

		// 10/27/2003 ROW:7 COL:2
		public string Date5
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(59);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//    5269 ROW:7 COL:13
		public string SrNum5
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(60);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 6000 ROW:7 COL:22
		public string Phase5
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(62);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     1.00 ROW:7 COL:27
		public string Hours5
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(63);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//                                           ROW:7 COL:37
		public string Note5
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(65);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// X ROW:8 COL:0
		public string Chk6
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(67);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(67, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		// 10/27/2003 ROW:8 COL:2
		public string Date6
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(68);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//    5269 ROW:8 COL:13
		public string SrNum6
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(69);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 6000 ROW:8 COL:22
		public string Phase6
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(71);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     1.00 ROW:8 COL:27
		public string Hours6
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(72);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//                                           ROW:8 COL:37
		public string Note6
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(74);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// X ROW:9 COL:0
		public string Chk7
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(76);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(76, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		// 10/27/2003 ROW:9 COL:2
		public string Date7
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(77);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//    5384 ROW:9 COL:13
		public string SrNum7
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(78);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 6000 ROW:9 COL:22
		public string Phase7
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(80);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     3.00 ROW:9 COL:27
		public string Hours7
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(81);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//                                           ROW:9 COL:37
		public string Note7
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(83);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// X ROW:10 COL:0
		public string Chk8
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(85);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(85, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		// 10/24/2003 ROW:10 COL:2
		public string Date8
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(86);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//    5268 ROW:10 COL:13
		public string SrNum8
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(87);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 6000 ROW:10 COL:22
		public string Phase8
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(89);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     1.00 ROW:10 COL:27
		public string Hours8
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(90);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// Review complaint tracking and list serve  ROW:10 COL:37
		public string Note8
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(92);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// X ROW:11 COL:0
		public string Chk9
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(94);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(94, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		// 10/24/2003 ROW:11 COL:2
		public string Date9
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(95);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//    5390 ROW:11 COL:13
		public string SrNum9
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(96);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 6000 ROW:11 COL:22
		public string Phase9
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(98);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     1.00 ROW:11 COL:27
		public string Hours9
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(99);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// EFT Authorization Meeting                 ROW:11 COL:37
		public string Note9
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(101);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// X ROW:12 COL:0
		public string Chk10
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(103);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(103, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		// 10/23/2003 ROW:12 COL:2
		public string Date10
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(104);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//    5267 ROW:12 COL:13
		public string SrNum10
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(105);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 6000 ROW:12 COL:22
		public string Phase10
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(107);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     1.50 ROW:12 COL:27
		public string Hours10
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(108);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// All Staff Meeting                         ROW:12 COL:37
		public string Note10
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(110);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// X ROW:13 COL:0
		public string Chk11
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(112);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(112, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		// 10/23/2003 ROW:13 COL:2
		public string Date11
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(113);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//    5268 ROW:13 COL:13
		public string SrNum11
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(114);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 6000 ROW:13 COL:22
		public string Phase11
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(116);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     1.50 ROW:13 COL:27
		public string Hours11
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(117);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// ELF Design Team                           ROW:13 COL:37
		public string Note11
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(119);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// X ROW:14 COL:0
		public string Chk12
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(121);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(121, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		// 10/22/2003 ROW:14 COL:2
		public string Date12
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(122);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//    5268 ROW:14 COL:13
		public string SrNum12
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(123);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 6000 ROW:14 COL:22
		public string Phase12
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(125);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     1.00 ROW:14 COL:27
		public string Hours12
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(126);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// Systems Discussion                        ROW:14 COL:37
		public string Note12
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(128);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// X ROW:15 COL:0
		public string Chk13
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(130);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(130, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		// 10/22/2003 ROW:15 COL:2
		public string Date13
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(131);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//    5268 ROW:15 COL:13
		public string SrNum13
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(132);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 6000 ROW:15 COL:22
		public string Phase13
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(134);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     1.00 ROW:15 COL:27
		public string Hours13
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(135);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// Moving elf behind dor firewall and away f ROW:15 COL:37
		public string Note13
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(137);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// X ROW:16 COL:0
		public string Chk14
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(139);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(139, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		// 10/21/2003 ROW:16 COL:2
		public string Date14
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(140);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//    5267 ROW:16 COL:13
		public string SrNum14
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(141);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 6000 ROW:16 COL:22
		public string Phase14
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(143);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     2.00 ROW:16 COL:27
		public string Hours14
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(144);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// Dentist                                   ROW:16 COL:37
		public string Note14
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(146);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// X ROW:17 COL:0
		public string Chk15
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(148);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(148, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		// 10/21/2003 ROW:17 COL:2
		public string Date15
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(149);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//    5269 ROW:17 COL:13
		public string SrNum15
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(150);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 6000 ROW:17 COL:22
		public string Phase15
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(152);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     4.00 ROW:17 COL:27
		public string Hours15
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(153);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//                                           ROW:17 COL:37
		public string Note15
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(155);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// X ROW:18 COL:0
		public string Chk16
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(157);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(157, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		// 10/20/2003 ROW:18 COL:2
		public string Date16
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(158);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//    5267 ROW:18 COL:13
		public string SrNum16
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(159);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 6000 ROW:18 COL:22
		public string Phase16
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(161);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//      .50 ROW:18 COL:27
		public string Hours16
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(162);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// Team Meeting                              ROW:18 COL:37
		public string Note16
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(164);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// X ROW:19 COL:0
		public string Chk17
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(166);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(166, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		// 10/20/2003 ROW:19 COL:2
		public string Date17
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(167);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//    5267 ROW:19 COL:13
		public string SrNum17
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(168);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 6000 ROW:19 COL:22
		public string Phase17
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(170);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     1.50 ROW:19 COL:27
		public string Hours17
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(171);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//                                           ROW:19 COL:37
		public string Note17
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(173);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//                                                                      ROW:20 COL:0
		public override string Message
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(175);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// PROD0W    ROW:20 COL:69
		public string Environment
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(176);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// =< 1304 >==< SR >=============< Time Log Browse >===============< P >==< R104 >  ROW:20 COL:79
		public string Footer
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(177);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// F1-List    F3-Inq/Upd    F6-Pg Frwd   SF6-Pg Bkwd    SF10-Help   F12-Menu/GoTo ROW:22 COL:0
		public string Menu
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(178);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// CommandLine                                                                    ROW:23 COL:0
		public override string CommandLine
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(180);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(180, out col, out row);
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
