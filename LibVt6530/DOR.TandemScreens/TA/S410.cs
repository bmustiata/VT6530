using System;
using System.Collections.Generic;
using System.Text;
using LibVt6530;

namespace DOR.TandemScreens.TA
{
	public class S410 : CommandLineScreen
	{
		internal S410(Terminal term, Vt6530 vt)
			: base(term, vt)
		{

		}

		public override bool VerifyLocation()
		{
			m_term.WaitEventScreen(false);
			// Check some of the protected fields to ensure 
			// the terminal is on this screen.
			return Footer.StartsWith("=< 410 >==< TA >===============< Invoice List >");
		}

		public override void NavigateTo()
		{
			// Navigate to the screen.
			DOR.TandemScreens.CL.R002 s000;
			s000 = m_term.CreateClR002();
			s000.NavigateTo();
			s000.WaitScreenLoad();
			// go to 000 screen
			s000.CommandLineExec("410");
			WaitScreenLoad();
		}

		public override void WaitScreenLoad()
		{
			// Wait for the screen to finish loading.
			base.WaitScreenLoad();
		}

		// REG # ROW:1 COL:3
		public string Field0
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(0);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 600406846 ROW:1 COL:9
		public string Field1
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(1);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(1, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		//               ROW:1 COL:19
		public string Field2
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(2);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// VANHOLLEBEKE KEVIN J                          ROW:1 COL:33
		public string Field3
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(3);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:1 COL:79
		public string Field4
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(4);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// INV # ROW:2 COL:3
		public string Field5
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(5);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     ROW:2 COL:9
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

		//                     ROW:2 COL:13
		public string Field7
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(7);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// EMERALD PACIFIC LAWN CARE                     ROW:2 COL:33
		public string Field8
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(8);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:2 COL:79
		public string Field9
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(9);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// RDS ROW:3 COL:5
		public string Field10
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(10);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  1  2   3 ROW:3 COL:9
		public string Field11
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(11);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//               ROW:3 COL:19
		public string Field12
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(12);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 20610 RICHMOND RD                             ROW:3 COL:33
		public string Field13
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(13);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:3 COL:79
		public string Field14
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(14);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// Post Pd ROW:4 COL:1
		public string Field15
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(15);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//    ROW:4 COL:9
		public string Field16
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(16);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(16, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		//      ROW:4 COL:12
		public string Field17
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

		//                 ROW:4 COL:17
		public string Field18
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(18);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// BOTHELL  WA    98012-9632                     ROW:4 COL:33
		public string Field19
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(19);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:4 COL:79
		public string Field20
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(20);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//      ROW:5 COL:15
		public string Field21
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(21);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:5 COL:21
		public string Field23
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(23);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//    ROW:5 COL:24
		public string Field25
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(25);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:5 COL:28
		public string Field27
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(27);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//    ROW:5 COL:30
		public string Field28
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(28);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//                                               ROW:5 COL:33
		public string Field29
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(29);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:5 COL:79
		public string Field30
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(30);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// Inv Doc   Doc #   Post Pd   Issued      Due    LSU H/S     Balance Due     ID  ROW:7 COL:1
		public string Field31
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(31);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   1 ROW:8 COL:1
		public string Field33
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(33);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// BAS ROW:8 COL:5
		public string Field34
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(34);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// B0395049D ROW:8 COL:9
		public string Field35
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(35);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// Q3 ROW:8 COL:19
		public string Field36
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(36);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 1994 ROW:8 COL:22
		public string Field37
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(37);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 01/23/1995 ROW:8 COL:27
		public string Field38
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(38);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 02/22/1995 ROW:8 COL:38
		public string Field39
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(39);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// N ROW:8 COL:49
		public string Field40
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(40);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:8 COL:51
		public string Field41
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(41);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:8 COL:53
		public string Field42
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(42);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//      PAID        ROW:8 COL:55
		public string Field43
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(43);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     1 ROW:8 COL:73
		public string Field45
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(45);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:8 COL:79
		public string Field46
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(46);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   2 ROW:9 COL:1
		public string Field47
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(47);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// BAS ROW:9 COL:5
		public string Field48
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(48);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// B0480511D ROW:9 COL:9
		public string Field49
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(49);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// Q4 ROW:9 COL:19
		public string Field50
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(50);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 1994 ROW:9 COL:22
		public string Field51
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(51);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 01/02/1996 ROW:9 COL:27
		public string Field52
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(52);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 02/01/1996 ROW:9 COL:38
		public string Field53
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(53);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// N ROW:9 COL:49
		public string Field54
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(54);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:9 COL:51
		public string Field55
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(55);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:9 COL:53
		public string Field56
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(56);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//      ASSUMED-004 ROW:9 COL:55
		public string Field57
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(57);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  2475 ROW:9 COL:73
		public string Field59
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(59);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:9 COL:79
		public string Field60
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(60);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   3 ROW:10 COL:1
		public string Field61
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(61);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// UTR ROW:10 COL:5
		public string Field62
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(62);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// B0483960D ROW:10 COL:9
		public string Field63
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(63);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// Q2 ROW:10 COL:19
		public string Field64
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(64);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 1995 ROW:10 COL:22
		public string Field65
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(65);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 01/16/1996 ROW:10 COL:27
		public string Field66
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(66);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 02/15/1996 ROW:10 COL:38
		public string Field67
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(67);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// N ROW:10 COL:49
		public string Field68
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(68);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:10 COL:51
		public string Field69
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(69);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:10 COL:53
		public string Field70
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(70);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//      ASSUMED-004 ROW:10 COL:55
		public string Field71
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(71);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  2475 ROW:10 COL:73
		public string Field73
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(73);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:10 COL:79
		public string Field74
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(74);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   4 ROW:11 COL:1
		public string Field75
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(75);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// WAR ROW:11 COL:5
		public string Field76
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(76);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 284053    ROW:11 COL:9
		public string Field77
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(77);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// Q4 ROW:11 COL:19
		public string Field78
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(78);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 1995 ROW:11 COL:22
		public string Field79
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(79);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 03/05/1996 ROW:11 COL:27
		public string Field80
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(80);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 03/05/1996 ROW:11 COL:38
		public string Field81
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(81);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// P ROW:11 COL:49
		public string Field82
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(82);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:11 COL:51
		public string Field83
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(83);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// S ROW:11 COL:53
		public string Field84
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(84);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//      PAID        ROW:11 COL:55
		public string Field85
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(85);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  2489 ROW:11 COL:73
		public string Field87
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(87);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:11 COL:79
		public string Field88
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(88);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   5 ROW:12 COL:1
		public string Field89
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(89);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// UTR ROW:12 COL:5
		public string Field90
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(90);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// B0578992D ROW:12 COL:9
		public string Field91
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(91);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// Q2 ROW:12 COL:19
		public string Field92
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(92);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 1996 ROW:12 COL:22
		public string Field93
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(93);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 12/12/1996 ROW:12 COL:27
		public string Field94
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(94);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 01/13/1997 ROW:12 COL:38
		public string Field95
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(95);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// N ROW:12 COL:49
		public string Field96
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(96);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:12 COL:51
		public string Field97
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(97);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:12 COL:53
		public string Field98
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(98);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//      PAID        ROW:12 COL:55
		public string Field99
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(99);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  2908 ROW:12 COL:73
		public string Field101
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(101);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:12 COL:79
		public string Field102
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(102);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   6 ROW:13 COL:1
		public string Field103
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(103);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// UTR ROW:13 COL:5
		public string Field104
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(104);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// B0683576D ROW:13 COL:9
		public string Field105
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(105);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 08 ROW:13 COL:19
		public string Field106
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(106);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 1997 ROW:13 COL:22
		public string Field107
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(107);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 12/22/1997 ROW:13 COL:27
		public string Field108
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(108);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 01/21/1998 ROW:13 COL:38
		public string Field109
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(109);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// Y ROW:13 COL:49
		public string Field110
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(110);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:13 COL:51
		public string Field111
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(111);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:13 COL:53
		public string Field112
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(112);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//      ASSUMED-012 ROW:13 COL:55
		public string Field113
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(113);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     1 ROW:13 COL:73
		public string Field115
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(115);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:13 COL:79
		public string Field116
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(116);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   7 ROW:14 COL:1
		public string Field117
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(117);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// UTR ROW:14 COL:5
		public string Field118
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(118);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// B0683577D ROW:14 COL:9
		public string Field119
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(119);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 09 ROW:14 COL:19
		public string Field120
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(120);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 1997 ROW:14 COL:22
		public string Field121
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(121);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 12/22/1997 ROW:14 COL:27
		public string Field122
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(122);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 01/21/1998 ROW:14 COL:38
		public string Field123
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(123);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// Y ROW:14 COL:49
		public string Field124
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(124);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:14 COL:51
		public string Field125
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(125);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:14 COL:53
		public string Field126
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(126);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//      ASSUMED-012 ROW:14 COL:55
		public string Field127
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(127);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     1 ROW:14 COL:73
		public string Field129
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(129);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:14 COL:79
		public string Field130
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(130);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   8 ROW:15 COL:1
		public string Field131
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(131);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// UTR ROW:15 COL:5
		public string Field132
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(132);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// B0683578D ROW:15 COL:9
		public string Field133
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(133);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 10 ROW:15 COL:19
		public string Field134
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(134);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 1997 ROW:15 COL:22
		public string Field135
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(135);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 12/22/1997 ROW:15 COL:27
		public string Field136
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(136);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 01/21/1998 ROW:15 COL:38
		public string Field137
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(137);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// Y ROW:15 COL:49
		public string Field138
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(138);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:15 COL:51
		public string Field139
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(139);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:15 COL:53
		public string Field140
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(140);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//      ASSUMED-012 ROW:15 COL:55
		public string Field141
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(141);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     1 ROW:15 COL:73
		public string Field143
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(143);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:15 COL:79
		public string Field144
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(144);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   9 ROW:16 COL:1
		public string Field145
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(145);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// UTR ROW:16 COL:5
		public string Field146
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(146);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// B0686518D ROW:16 COL:9
		public string Field147
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(147);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 11 ROW:16 COL:19
		public string Field148
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(148);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 1997 ROW:16 COL:22
		public string Field149
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(149);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 01/06/1998 ROW:16 COL:27
		public string Field150
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(150);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 02/05/1998 ROW:16 COL:38
		public string Field151
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(151);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// P ROW:16 COL:49
		public string Field152
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(152);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:16 COL:51
		public string Field153
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(153);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:16 COL:53
		public string Field154
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(154);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//      PAID        ROW:16 COL:55
		public string Field155
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(155);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     1 ROW:16 COL:73
		public string Field157
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(157);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:16 COL:79
		public string Field158
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(158);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  10 ROW:17 COL:1
		public string Field159
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(159);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// UTR ROW:17 COL:5
		public string Field160
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(160);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// B0708997D ROW:17 COL:9
		public string Field161
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(161);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 12 ROW:17 COL:19
		public string Field162
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(162);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 1997 ROW:17 COL:22
		public string Field163
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(163);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 03/24/1998 ROW:17 COL:27
		public string Field164
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(164);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 04/23/1998 ROW:17 COL:38
		public string Field165
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(165);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// Y ROW:17 COL:49
		public string Field166
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(166);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:17 COL:51
		public string Field167
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(167);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:17 COL:53
		public string Field168
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(168);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//      ASSUMED-012 ROW:17 COL:55
		public string Field169
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(169);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     1 ROW:17 COL:73
		public string Field171
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(171);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:17 COL:79
		public string Field172
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(172);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  11 ROW:18 COL:1
		public string Field173
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(173);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// UTR ROW:18 COL:5
		public string Field174
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(174);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// B0708998D ROW:18 COL:9
		public string Field175
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(175);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 01 ROW:18 COL:19
		public string Field176
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(176);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 1998 ROW:18 COL:22
		public string Field177
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(177);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 03/24/1998 ROW:18 COL:27
		public string Field178
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(178);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 04/23/1998 ROW:18 COL:38
		public string Field179
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(179);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// Y ROW:18 COL:49
		public string Field180
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(180);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:18 COL:51
		public string Field181
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(181);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:18 COL:53
		public string Field182
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(182);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//      ASSUMED-012 ROW:18 COL:55
		public string Field183
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(183);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     1 ROW:18 COL:73
		public string Field185
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(185);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:18 COL:79
		public string Field186
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(186);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  12 ROW:19 COL:1
		public string Field187
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(187);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// WAR ROW:19 COL:5
		public string Field188
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(188);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 299535    ROW:19 COL:9
		public string Field189
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(189);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 01 ROW:19 COL:19
		public string Field190
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(190);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 1998 ROW:19 COL:22
		public string Field191
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(191);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 04/01/1998 ROW:19 COL:27
		public string Field192
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(192);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 04/01/1998 ROW:19 COL:38
		public string Field193
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(193);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// P ROW:19 COL:49
		public string Field194
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(194);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:19 COL:51
		public string Field195
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(195);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// S ROW:19 COL:53
		public string Field196
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(196);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//      PAID        ROW:19 COL:55
		public string Field197
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(197);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  2155 ROW:19 COL:73
		public string Field199
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(199);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:19 COL:79
		public string Field200
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(200);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// F6-Pg Frwd...To view more                                            ROW:20 COL:1
		public override string Message
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(201);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// TEST26    ROW:20 COL:70
		public string Environment
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(202);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// =< 410 >==< TA >===============< Invoice List >=================< T >==< R410   ROW:21 COL:1
		public string Footer
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(204);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  F1-Enter F2-WithBal F3-PostPdOnly  F5-Invoice F6-PgFd  F11-Prev F12-Menu/Goto  ROW:22 COL:1
		public string Menu1
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(205);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// SF1-Desc Order      SF3-ListByPostPd          SF6-PgBk                          ROW:23 COL:1
		public string Menu2
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(206);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//                                                                                ROW:24 COL:1
		public override string CommandLine
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(207);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(207, out col, out row);
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
