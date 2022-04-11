using System;
using System.Collections.Generic;
using System.Diagnostics;
using LibVt6530;

namespace DOR.TandemScreens.LV
{
	public class S2408 : CommandLineScreen
	{
		internal S2408(Terminal term, Vt6530 vt)
			: base(term, vt)
		{
			m_vt = vt;
		}

		public override bool VerifyLocation()
		{
			m_term.WaitEventScreen(false);
			// Check some of the protected fields to ensure 
			// the terminal is on this screen.
			return Footer == "=< 2408 >==< AL >=======< Employee view work schedule >=========< P >==< R170 > "
				|| Footer == "=< 2408 >==< AL >=======< Employee view work schedule >=========< D >==< R170 > "
				|| Footer == "=< 2408 >==< AL >=======< Employee view work schedule >=========< T >==< R170 > ";
		}

		public override void NavigateTo()
		{
			// Navigate to the screen.
			throw new NotImplementedException();
		}

		public override void WaitScreenLoad()
		{
			base.WaitScreenLoad();
		}


		// GARRISON JOHN                  ROW:0 COL:35
		public string NameLastFirst
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(0);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//               ROW:0 COL:66
		public string Field1
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(1);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// Attend Unit: ROW:1 COL:2
		public string Field2
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(2);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 37 ROW:1 COL:15
		public string AttenanceUnit
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(3);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//                        ROW:1 COL:18
		public string Field4
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(4);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// Schedule Effective Date: ROW:1 COL:41
		public string Field5
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(5);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 01/07/1999 ROW:1 COL:66
		public string ScheduleEffectiveDate
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(6);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//    ROW:1 COL:77
		public string Field7
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(7);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// Overtime Eligible: ROW:2 COL:2
		public string Field8
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(8);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// No  ROW:2 COL:21
		public string OvertimeEligibleYN
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(9);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//                                                        ROW:2 COL:25
		public string Field10
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(10);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// Begin Work Time     End Work Time       Begin Lunch Time     End Lunch Time ROW:3 COL:2
		public string Field11
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(11);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:3 COL:78
		public string Field12
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(12);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// Week 1 ROW:4 COL:37
		public string Field13
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(13);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//                                    ROW:4 COL:44
		public string Field14
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(14);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// M ROW:4 COL:79
		public string Field15
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(15);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//       ROW:5 COL:1
		public string Field16
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(16);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  8 ROW:5 COL:7
		public string Week1MondayStartHour
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(17);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// : ROW:5 COL:10
		public string Field18
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(18);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 00 ROW:5 COL:12
		public string Week1MondayStartMinute
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(19);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//           ROW:5 COL:15
		public string Field20
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(20);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  5 ROW:5 COL:25
		public string Week1MondayEndHour
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(21);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// : ROW:5 COL:28
		public string Field22
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(22);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 00 ROW:5 COL:30
		public string Week1MondayEndMinute
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(23);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//              ROW:5 COL:33
		public string Field24
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(24);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 12 ROW:5 COL:46
		public string Week1MondayLunchStartHour
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(25);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// : ROW:5 COL:49
		public string Field26
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(26);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 00 ROW:5 COL:51
		public string Week1MondayLunchStartMinute
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(27);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//            ROW:5 COL:54
		public string Field28
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(28);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  1 ROW:5 COL:65
		public string Week1MondayLunchEndHour
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(29);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// : ROW:5 COL:68
		public string Field30
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(30);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 00 ROW:5 COL:70
		public string Week1MondayLunchEndMinute
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(31);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//    ROW:5 COL:73
		public string Field32
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(32);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// M ROW:5 COL:76
		public string Field33
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(33);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// T ROW:5 COL:79
		public string Field35
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(35);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//       ROW:6 COL:1
		public string Field36
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(36);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  8 ROW:6 COL:7
		public string Field37
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(37);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// : ROW:6 COL:10
		public string Field38
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(38);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 00 ROW:6 COL:12
		public string Field39
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(39);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//           ROW:6 COL:15
		public string Field40
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(40);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  5 ROW:6 COL:25
		public string Field41
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(41);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// : ROW:6 COL:28
		public string Field42
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(42);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 00 ROW:6 COL:30
		public string Field43
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(43);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//              ROW:6 COL:33
		public string Field44
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(44);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 12 ROW:6 COL:46
		public string Field45
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(45);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// : ROW:6 COL:49
		public string Field46
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(46);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 00 ROW:6 COL:51
		public string Field47
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(47);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//            ROW:6 COL:54
		public string Field48
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(48);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  1 ROW:6 COL:65
		public string Field49
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(49);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// : ROW:6 COL:68
		public string Field50
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(50);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 00 ROW:6 COL:70
		public string Field51
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(51);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//    ROW:6 COL:73
		public string Field52
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(52);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// T ROW:6 COL:76
		public string Field53
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(53);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// W ROW:6 COL:79
		public string Field55
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(55);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//       ROW:7 COL:1
		public string Field56
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(56);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  8 ROW:7 COL:7
		public string Field57
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(57);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// : ROW:7 COL:10
		public string Field58
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(58);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 00 ROW:7 COL:12
		public string Field59
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(59);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//           ROW:7 COL:15
		public string Field60
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(60);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  5 ROW:7 COL:25
		public string Field61
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(61);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// : ROW:7 COL:28
		public string Field62
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(62);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 00 ROW:7 COL:30
		public string Field63
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(63);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//              ROW:7 COL:33
		public string Field64
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(64);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 12 ROW:7 COL:46
		public string Field65
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(65);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// : ROW:7 COL:49
		public string Field66
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(66);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 00 ROW:7 COL:51
		public string Field67
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(67);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//            ROW:7 COL:54
		public string Field68
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(68);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  1 ROW:7 COL:65
		public string Field69
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(69);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// : ROW:7 COL:68
		public string Field70
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(70);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 00 ROW:7 COL:70
		public string Field71
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(71);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//    ROW:7 COL:73
		public string Field72
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(72);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// W ROW:7 COL:76
		public string Field73
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(73);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// TH ROW:7 COL:79
		public string Field75
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(75);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//      ROW:8 COL:2
		public string Field76
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(76);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  8 ROW:8 COL:7
		public string Field77
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(77);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// : ROW:8 COL:10
		public string Field78
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(78);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 00 ROW:8 COL:12
		public string Field79
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(79);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//           ROW:8 COL:15
		public string Field80
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(80);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  5 ROW:8 COL:25
		public string Field81
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(81);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// : ROW:8 COL:28
		public string Field82
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(82);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 00 ROW:8 COL:30
		public string Field83
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(83);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//              ROW:8 COL:33
		public string Field84
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(84);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 12 ROW:8 COL:46
		public string Field85
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(85);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// : ROW:8 COL:49
		public string Field86
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(86);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 00 ROW:8 COL:51
		public string Field87
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(87);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//            ROW:8 COL:54
		public string Field88
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(88);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  1 ROW:8 COL:65
		public string Field89
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(89);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// : ROW:8 COL:68
		public string Field90
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(90);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 00 ROW:8 COL:70
		public string Field91
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(91);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//    ROW:8 COL:73
		public string Field92
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(92);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// TH ROW:8 COL:76
		public string Field93
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(93);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// F ROW:8 COL:79
		public string Field94
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(94);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//       ROW:9 COL:1
		public string Field95
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(95);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  8 ROW:9 COL:7
		public string Field96
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(96);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// : ROW:9 COL:10
		public string Field97
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(97);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 00 ROW:9 COL:12
		public string Field98
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(98);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//           ROW:9 COL:15
		public string Field99
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(99);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  5 ROW:9 COL:25
		public string Field100
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(100);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// : ROW:9 COL:28
		public string Field101
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(101);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 00 ROW:9 COL:30
		public string Field102
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(102);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//              ROW:9 COL:33
		public string Field103
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(103);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 12 ROW:9 COL:46
		public string Field104
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(104);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// : ROW:9 COL:49
		public string Field105
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(105);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 00 ROW:9 COL:51
		public string Field106
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(106);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//            ROW:9 COL:54
		public string Field107
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(107);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  1 ROW:9 COL:65
		public string Field108
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(108);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// : ROW:9 COL:68
		public string Field109
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(109);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 00 ROW:9 COL:70
		public string Field110
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(110);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//    ROW:9 COL:73
		public string Field111
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(111);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// F ROW:9 COL:76
		public string Field112
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(112);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:9 COL:78
		public string Field113
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(113);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// Week 2 ROW:10 COL:37
		public string Field114
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(114);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//                                    ROW:10 COL:44
		public string Field115
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(115);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// M ROW:10 COL:79
		public string Field116
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(116);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//       ROW:11 COL:1
		public string Field117
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(117);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  8 ROW:11 COL:7
		public string Field118
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(118);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// : ROW:11 COL:10
		public string Field119
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(119);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 00 ROW:11 COL:12
		public string Field120
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(120);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//           ROW:11 COL:15
		public string Field121
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(121);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  5 ROW:11 COL:25
		public string Field122
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(122);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// : ROW:11 COL:28
		public string Field123
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(123);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 00 ROW:11 COL:30
		public string Field124
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(124);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//              ROW:11 COL:33
		public string Field125
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(125);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 12 ROW:11 COL:46
		public string Field126
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(126);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// : ROW:11 COL:49
		public string Field127
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(127);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 00 ROW:11 COL:51
		public string Field128
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(128);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//            ROW:11 COL:54
		public string Field129
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(129);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  1 ROW:11 COL:65
		public string Field130
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(130);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// : ROW:11 COL:68
		public string Field131
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(131);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 00 ROW:11 COL:70
		public string Field132
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(132);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//    ROW:11 COL:73
		public string Field133
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(133);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// M ROW:11 COL:76
		public string Field134
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(134);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// T ROW:11 COL:79
		public string Field136
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(136);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//       ROW:12 COL:1
		public string Field137
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(137);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  8 ROW:12 COL:7
		public string Field138
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(138);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// : ROW:12 COL:10
		public string Field139
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(139);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 00 ROW:12 COL:12
		public string Field140
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(140);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//           ROW:12 COL:15
		public string Field141
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(141);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  5 ROW:12 COL:25
		public string Field142
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(142);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// : ROW:12 COL:28
		public string Field143
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(143);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 00 ROW:12 COL:30
		public string Field144
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(144);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//              ROW:12 COL:33
		public string Field145
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(145);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 12 ROW:12 COL:46
		public string Field146
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(146);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// : ROW:12 COL:49
		public string Field147
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(147);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 00 ROW:12 COL:51
		public string Field148
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(148);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//            ROW:12 COL:54
		public string Field149
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(149);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  1 ROW:12 COL:65
		public string Field150
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(150);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// : ROW:12 COL:68
		public string Field151
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(151);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 00 ROW:12 COL:70
		public string Field152
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(152);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//    ROW:12 COL:73
		public string Field153
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(153);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// T ROW:12 COL:76
		public string Field154
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(154);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// W ROW:12 COL:79
		public string Field156
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(156);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//       ROW:13 COL:1
		public string Field157
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(157);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  8 ROW:13 COL:7
		public string Field158
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(158);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// : ROW:13 COL:10
		public string Field159
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(159);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 00 ROW:13 COL:12
		public string Field160
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(160);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//           ROW:13 COL:15
		public string Field161
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(161);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  5 ROW:13 COL:25
		public string Field162
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(162);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// : ROW:13 COL:28
		public string Field163
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(163);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 00 ROW:13 COL:30
		public string Field164
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(164);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//              ROW:13 COL:33
		public string Field165
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(165);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 12 ROW:13 COL:46
		public string Field166
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(166);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// : ROW:13 COL:49
		public string Field167
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(167);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 00 ROW:13 COL:51
		public string Field168
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(168);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//            ROW:13 COL:54
		public string Field169
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(169);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  1 ROW:13 COL:65
		public string Field170
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(170);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// : ROW:13 COL:68
		public string Field171
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(171);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 00 ROW:13 COL:70
		public string Field172
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(172);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//    ROW:13 COL:73
		public string Field173
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(173);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// W ROW:13 COL:76
		public string Field174
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(174);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// TH ROW:13 COL:79
		public string Field176
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(176);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//      ROW:14 COL:2
		public string Field177
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(177);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  8 ROW:14 COL:7
		public string Field178
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(178);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// : ROW:14 COL:10
		public string Field179
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(179);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 00 ROW:14 COL:12
		public string Field180
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(180);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//           ROW:14 COL:15
		public string Field181
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(181);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  5 ROW:14 COL:25
		public string Field182
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(182);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// : ROW:14 COL:28
		public string Field183
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(183);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 00 ROW:14 COL:30
		public string Field184
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(184);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//              ROW:14 COL:33
		public string Field185
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(185);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 12 ROW:14 COL:46
		public string Field186
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(186);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// : ROW:14 COL:49
		public string Field187
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(187);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 00 ROW:14 COL:51
		public string Field188
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(188);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//            ROW:14 COL:54
		public string Field189
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(189);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  1 ROW:14 COL:65
		public string Field190
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(190);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// : ROW:14 COL:68
		public string Field191
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(191);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 00 ROW:14 COL:70
		public string Field192
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(192);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//    ROW:14 COL:73
		public string Field193
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(193);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// TH ROW:14 COL:76
		public string Field194
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(194);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// F ROW:14 COL:79
		public string Field195
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(195);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//       ROW:15 COL:1
		public string Field196
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(196);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  8 ROW:15 COL:7
		public string Field197
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(197);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// : ROW:15 COL:10
		public string Field198
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(198);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 00 ROW:15 COL:12
		public string Field199
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(199);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//           ROW:15 COL:15
		public string Field200
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(200);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  5 ROW:15 COL:25
		public string Field201
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(201);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// : ROW:15 COL:28
		public string Field202
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(202);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 00 ROW:15 COL:30
		public string Field203
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(203);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//              ROW:15 COL:33
		public string Field204
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(204);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 12 ROW:15 COL:46
		public string Field205
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(205);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// : ROW:15 COL:49
		public string Field206
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(206);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 00 ROW:15 COL:51
		public string Field207
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(207);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//            ROW:15 COL:54
		public string Field208
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(208);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  1 ROW:15 COL:65
		public string Field209
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(209);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// : ROW:15 COL:68
		public string Field210
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(210);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 00 ROW:15 COL:70
		public string Field211
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(211);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//    ROW:15 COL:73
		public string Field212
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(212);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// F ROW:15 COL:76
		public string Field213
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(213);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:15 COL:78
		public string Field214
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(214);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// Work Week: ROW:17 COL:9
		public string Field215
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(215);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// S   ROW:17 COL:20
		public string Field216
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(216);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//      ROW:17 COL:24
		public string Field217
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(217);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// Work Week Effective Date: ROW:17 COL:29
		public string Field218
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(218);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 01/07/1999 ROW:17 COL:55
		public string Field219
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(219);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//               ROW:17 COL:66
		public string Field220
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(220);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//                                                                      ROW:19 COL:0
		public override string Message
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(221);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// PROD0M    ROW:19 COL:69
		public string Environment
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(222);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// =< 2408 >==< AL >=======< Employee view work schedule >=========< P >==< R170 >  ROW:19 COL:79
		public string Footer
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(223);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// F9-Pause     SF10-Help    F12-Menu/GoTo    SF12-System Menu                     ROW:21 COL:0
		public string Menu
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(224);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//                                                                                ROW:23 COL:0
		public override string CommandLine
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(225);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(225, out col, out row);
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
