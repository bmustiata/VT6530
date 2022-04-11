using System;
using System.Collections.Generic;
using System.Text;
using LibVt6530;

namespace DOR.TandemScreens.LV
{
	public struct LeaveRequestItem
	{
		public string startDate;
		public string startDayOfWeek;
		public string startTime;
		public string endDate;
		public string endDayOfWeek;
		public string endTime;
		public string hours;
		public string leaveType;
		public string status;
		public string createdBy;
	}

	public class S2411 : CommandLineScreen
	{
		internal S2411(Terminal term, Vt6530 vt)
			: base(term, vt)
		{

		}

		public override bool VerifyLocation()
		{
			m_term.WaitEventScreen(false);
			// Check some of the protected fields to ensure 
			// the terminal is on this screen.
			return Footer == "=< 2411 >==< AL >==========< View/Cancel Requests >=============< P >==< R210 > "
				|| Footer == "=< 2411 >==< AL >==========< View/Cancel Requests >=============< D >==< R210 > "
				|| Footer == "=< 2411 >==< AL >==========< View/Cancel Requests >=============< T >==< R210 > ";
		}

		public override void NavigateTo()
		{
			throw new NotImplementedException();
		}

		public void NavigateTo( string ssnLast4 )
		{
			S2401_2 s2401_2 = m_term.CreateLvS2401_2();
			s2401_2.NavigateTo(ssnLast4);
			s2401_2.ScreenNum = "2411";
			s2401_2.Submit();
			WaitScreenLoad();
		}

		public List<LeaveRequestItem> LoadAll(int maxcount)
		{
			WaitScreenLoad();
			List<LeaveRequestItem> l = new List<LeaveRequestItem>();
			while ( maxcount-- > 0 )
			{
				LeaveRequestItem lv;
				lv.createdBy = CreatedBy1;
				lv.endDate = DateTo1;
				lv.endDayOfWeek = ToDayOfWeek1;
				lv.endTime = ToTime1;
				lv.hours = Hours1;
				lv.leaveType = LeaveType1;
				lv.startDate = DateFrom1;
				lv.startDayOfWeek = FromDayOfWeek1;
				lv.startTime = FromTime1;
				lv.status = Status1;
				if (lv.hours.Trim().Length > 0) l.Add(lv);

				lv.createdBy = CreatedBy2;
				lv.endDate = DateTo2;
				lv.endDayOfWeek = ToDayOfWeek2;
				lv.endTime = ToTime2;
				lv.hours = Hours2;
				lv.leaveType = LeaveType2;
				lv.startDate = DateFrom2;
				lv.startDayOfWeek = FromDayOfWeek2;
				lv.startTime = FromTime2;
				lv.status = Status2;
				if (lv.hours.Trim().Length > 0) l.Add(lv);

				lv.createdBy = CreatedBy3;
				lv.endDate = DateTo3;
				lv.endDayOfWeek = ToDayOfWeek3;
				lv.endTime = ToTime3;
				lv.hours = Hours3;
				lv.leaveType = LeaveType3;
				lv.startDate = DateFrom3;
				lv.startDayOfWeek = FromDayOfWeek3;
				lv.startTime = FromTime3;
				lv.status = Status3;
				if (lv.hours.Trim().Length > 0) l.Add(lv);

				lv.createdBy = CreatedBy4;
				lv.endDate = DateTo4;
				lv.endDayOfWeek = ToDayOfWeek4;
				lv.endTime = ToTime4;
				lv.hours = Hours4;
				lv.leaveType = LeaveType4;
				lv.startDate = DateFrom4;
				lv.startDayOfWeek = FromDayOfWeek4;
				lv.startTime = FromTime4;
				lv.status = Status4;
				if (lv.hours.Trim().Length > 0) l.Add(lv);

				lv.createdBy = CreatedBy5;
				lv.endDate = DateTo5;
				lv.endDayOfWeek = ToDayOfWeek5;
				lv.endTime = ToTime5;
				lv.hours = Hours5;
				lv.leaveType = LeaveType5;
				lv.startDate = DateFrom5;
				lv.startDayOfWeek = FromDayOfWeek5;
				lv.startTime = FromTime5;
				lv.status = Status5;
				if (lv.hours.Trim().Length > 0) l.Add(lv);

				lv.createdBy = CreatedBy6;
				lv.endDate = DateTo6;
				lv.endDayOfWeek = ToDayOfWeek6;
				lv.endTime = ToTime6;
				lv.hours = Hours6;
				lv.leaveType = LeaveType6;
				lv.startDate = DateFrom6;
				lv.startDayOfWeek = FromDayOfWeek6;
				lv.startTime = FromTime6;
				lv.status = Status6;
				if (lv.hours.Trim().Length > 0) l.Add(lv);

				lv.createdBy = CreatedBy7;
				lv.endDate = DateTo7;
				lv.endDayOfWeek = ToDayOfWeek7;
				lv.endTime = ToTime7;
				lv.hours = Hours7;
				lv.leaveType = LeaveType7;
				lv.startDate = DateFrom7;
				lv.startDayOfWeek = FromDayOfWeek7;
				lv.startTime = FromTime7;
				lv.status = Status7;
				if (lv.hours.Trim().Length > 0) l.Add(lv);

				lv.createdBy = CreatedBy8;
				lv.endDate = DateTo8;
				lv.endDayOfWeek = ToDayOfWeek8;
				lv.endTime = ToTime8;
				lv.hours = Hours8;
				lv.leaveType = LeaveType8;
				lv.startDate = DateFrom8;
				lv.startDayOfWeek = FromDayOfWeek8;
				lv.startTime = FromTime8;
				lv.status = Status8;
				if (lv.hours.Trim().Length > 0) l.Add(lv);

				lv.createdBy = CreatedBy9;
				lv.endDate = DateTo9;
				lv.endDayOfWeek = ToDayOfWeek9;
				lv.endTime = ToTime9;
				lv.hours = Hours9;
				lv.leaveType = LeaveType9;
				lv.startDate = DateFrom9;
				lv.startDayOfWeek = FromDayOfWeek9;
				lv.startTime = FromTime9;
				lv.status = Status9;
				if (lv.hours.Trim().Length > 0) l.Add(lv);

				lv.createdBy = CreatedBy10;
				lv.endDate = DateTo10;
				lv.endDayOfWeek = ToDayOfWeek10;
				lv.endTime = ToTime10;
				lv.hours = Hours10;
				lv.leaveType = LeaveType10;
				lv.startDate = DateFrom10;
				lv.startDayOfWeek = FromDayOfWeek10;
				lv.startTime = FromTime10;
				lv.status = Status10;
				if (lv.hours.Trim().Length > 0) l.Add(lv);

				lv.createdBy = CreatedBy11;
				lv.endDate = DateTo11;
				lv.endDayOfWeek = ToDayOfWeek11;
				lv.endTime = ToTime11;
				lv.hours = Hours11;
				lv.leaveType = LeaveType11;
				lv.startDate = DateFrom11;
				lv.startDayOfWeek = FromDayOfWeek11;
				lv.startTime = FromTime11;
				lv.status = Status11;
				if (lv.hours.Trim().Length > 0) l.Add(lv);

				if ( Message.Trim() != "")
				{
					break;
				}
				m_vt.FakeF6();
				m_term.WaitEventScreen(true);

			} 
			return l;
		}

		public override void WaitScreenLoad()
		{
			// Wait for the screen to finish loading.
			base.WaitScreenLoad();
		}

		// Last Name:  ROW:0 COL:0
		public string Field0
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(0);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//                      ROW:0 COL:12
		public string LastName
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

		//   ROW:0 COL:33
		public string Field2
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(2);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// SMITH, BOB                ROW:0 COL:35
		public string NameLastFirst
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(3);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//               ROW:0 COL:66
		public string Field4
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(4);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// Start Date mm/dd/yyyy: ROW:1 COL:0
		public string Field5
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(5);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//    ROW:1 COL:23
		public string StartDateMonth
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

		// / ROW:1 COL:26
		public string Field7
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(7);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//    ROW:1 COL:28
		public string StartDateDay
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

		// / ROW:1 COL:31
		public string Field9
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(9);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//      ROW:1 COL:33
		public string StartDateYear
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(10);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(10, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		//            ROW:1 COL:38
		public string Field11
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(11);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// Phone: ROW:1 COL:49
		public string Field12
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(12);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 586-7954 ROW:1 COL:56
		public string Phone
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(13);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//                ROW:1 COL:65
		public string Field14
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(14);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// Type "C" next to a request to cancel, "V" to view ROW:2 COL:0
		public string Field15
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(15);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//                               ROW:2 COL:50
		public string Field16
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(16);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// * Not Included in Balance ROW:3 COL:52
		public string Field17
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(17);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:3 COL:78
		public string Field18
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(18);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// FROM          TO         FROM        TO                          CREATED ROW:4 COL:5
		public string Field19
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(19);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:4 COL:78
		public string Field20
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(20);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// DATE         DATE        TIME       TIME     HRS   TYPE  ACTION     BY ROW:5 COL:5
		public string Field21
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(21);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     ROW:5 COL:76
		public string Field22
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(22);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:6 COL:79
		public string Chk1CV
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(23);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				if (value != "C" && value != "V")
				{
					throw new Exception("Chk1CV must be a C or V");
				}
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(23, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		// 12/06/2006 ROW:7 COL:1
		public string DateFrom1
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(24);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// WE ROW:7 COL:12
		public string FromDayOfWeek1
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(25);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 12/11/2006 ROW:7 COL:15
		public string DateTo1
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(26);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// MO ROW:7 COL:26
		public string ToDayOfWeek1
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(27);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  7:00 A ROW:7 COL:29
		public string FromTime1
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(28);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//    ROW:7 COL:37
		public string Field29
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(29);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  3:30 P ROW:7 COL:40
		public string ToTime1
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(30);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  -32.0 ROW:7 COL:48
		public string Hours1
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(31);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// ANNL ROW:7 COL:56
		public string LeaveType1
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(33);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// APPR      ROW:7 COL:62
		public string Status1
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(35);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// EMP ROW:7 COL:73
		public string CreatedBy1
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(37);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// * ROW:7 COL:77
		public string Field38
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(38);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:7 COL:79
		public string Chk2CV
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

		// 11/28/2006 ROW:8 COL:1
		public string DateFrom2
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(40);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// TU ROW:8 COL:12
		public string FromDayOfWeek2
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(41);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 11/28/2006 ROW:8 COL:15
		public string DateTo2
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(42);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// TU ROW:8 COL:26
		public string ToDayOfWeek2
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(43);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  8:00 A ROW:8 COL:29
		public string FromTime2
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(44);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//    ROW:8 COL:37
		public string Field45
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(45);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 10:00 A ROW:8 COL:40
		public string ToTime2
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(46);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   -2.0 ROW:8 COL:48
		public string Hours2
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(47);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// ANNL ROW:8 COL:56
		public string LeaveType2
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(49);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// APPR      ROW:8 COL:62
		public string Status2
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(51);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// EMP ROW:8 COL:73
		public string CreatedBy2
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(53);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:8 COL:77
		public string Field54
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(54);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:8 COL:79
		public string Chk3CV
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

		// 11/27/2006 ROW:9 COL:1
		public string DateFrom3
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(56);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// MO ROW:9 COL:12
		public string FromDayOfWeek3
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(57);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 11/27/2006 ROW:9 COL:15
		public string DateTo3
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(58);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// MO ROW:9 COL:26
		public string ToDayOfWeek3
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(59);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  7:00 A ROW:9 COL:29
		public string FromTime3
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(60);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//    ROW:9 COL:37
		public string Field61
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(61);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  3:30 P ROW:9 COL:40
		public string ToTime3
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(62);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   -8.0 ROW:9 COL:48
		public string Hours3
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(63);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// SICK ROW:9 COL:56
		public string LeaveType3
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(65);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// APPR      ROW:9 COL:62
		public string Status3
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(67);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// EMP ROW:9 COL:73
		public string CreatedBy3
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(69);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:9 COL:77
		public string Field70
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(70);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:9 COL:79
		public string Chk4CV
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

		// 11/17/2006 ROW:10 COL:1
		public string DateFrom4
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(72);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// FR ROW:10 COL:12
		public string FromDayOfWeek4
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(73);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 11/17/2006 ROW:10 COL:15
		public string DateTo4
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(74);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// FR ROW:10 COL:26
		public string ToDayOfWeek4
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(75);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  1:00 P ROW:10 COL:29
		public string FromTime4
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(76);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//    ROW:10 COL:37
		public string Field77
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(77);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  3:30 P ROW:10 COL:40
		public string ToTime4
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(78);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   -2.5 ROW:10 COL:48
		public string Hours4
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(79);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// ANNL ROW:10 COL:56
		public string LeaveType4
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(81);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// APPR      ROW:10 COL:62
		public string Status4
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(83);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// EMP ROW:10 COL:73
		public string CreatedBy4
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(85);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:10 COL:77
		public string Field86
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(86);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:10 COL:79
		public string Chk5CV
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(87);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(87, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		// 11/16/2006 ROW:11 COL:1
		public string DateFrom5
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(88);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// TH ROW:11 COL:12
		public string FromDayOfWeek5
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(89);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 11/16/2006 ROW:11 COL:15
		public string DateTo5
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(90);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// TH ROW:11 COL:26
		public string ToDayOfWeek5
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(91);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  1:00 P ROW:11 COL:29
		public string FromTime5
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(92);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//    ROW:11 COL:37
		public string Field93
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(93);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  3:30 P ROW:11 COL:40
		public string ToTime5
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(94);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   -2.5 ROW:11 COL:48
		public string Hours5
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(95);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// SICK ROW:11 COL:56
		public string LeaveType5
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(97);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// APPR      ROW:11 COL:62
		public string Status5
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(99);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// EMP ROW:11 COL:73
		public string CreatedBy5
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(101);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:11 COL:77
		public string Field102
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(102);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:11 COL:79
		public string Chk6CV
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

		//            ROW:12 COL:1
		public string DateFrom6
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(104);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//    ROW:12 COL:12
		public string FromDayOfWeek6
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(105);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 11/15/2006 ROW:12 COL:15
		public string DateTo6
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(106);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// WE ROW:12 COL:26
		public string ToDayOfWeek6
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(107);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//         ROW:12 COL:29
		public string FromTime6
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(108);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//    ROW:12 COL:37
		public string Field109
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(109);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 12:15 A ROW:12 COL:40
		public string ToTime6
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(110);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   +8.0 ROW:12 COL:48
		public string Hours6
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(111);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// SICK ROW:12 COL:56
		public string LeaveType6
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(113);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// ACCR      ROW:12 COL:62
		public string Status6
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(115);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// SYS ROW:12 COL:73
		public string CreatedBy6
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(117);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:12 COL:77
		public string Field118
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(118);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:12 COL:79
		public string Chk7CV
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(119);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(119, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		//            ROW:13 COL:1
		public string DateFrom7
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(120);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//    ROW:13 COL:12
		public string FromDayOfWeek7
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(121);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 11/15/2006 ROW:13 COL:15
		public string DateTo7
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(122);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// WE ROW:13 COL:26
		public string ToDayOfWeek7
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(123);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//         ROW:13 COL:29
		public string FromTime7
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(124);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//    ROW:13 COL:37
		public string Field125
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(125);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 12:15 A ROW:13 COL:40
		public string ToTime7
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(126);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  +10.0 ROW:13 COL:48
		public string Hours7
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(127);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// ANNL ROW:13 COL:56
		public string LeaveType7
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(129);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// ACCR      ROW:13 COL:62
		public string Status7
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(131);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// SYS ROW:13 COL:73
		public string CreatedBy7
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(133);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:13 COL:77
		public string Field134
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(134);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:13 COL:79
		public string Chk8CV
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(135);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(135, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		// 11/13/2006 ROW:14 COL:1
		public string DateFrom8
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(136);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// MO ROW:14 COL:12
		public string FromDayOfWeek8
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(137);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 11/13/2006 ROW:14 COL:15
		public string DateTo8
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(138);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// MO ROW:14 COL:26
		public string ToDayOfWeek8
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(139);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  7:00 A ROW:14 COL:29
		public string FromTime8
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(140);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//    ROW:14 COL:37
		public string Field141
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(141);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  3:30 P ROW:14 COL:40
		public string ToTime8
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(142);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   -8.0 ROW:14 COL:48
		public string Hours8
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(143);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// SICK ROW:14 COL:56
		public string LeaveType8
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(145);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// APPR      ROW:14 COL:62
		public string Status8
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(147);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// EMP ROW:14 COL:73
		public string CreatedBy8
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(149);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:14 COL:77
		public string Field150
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(150);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:14 COL:79
		public string Chk9CV
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(151);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(151, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		// 11/03/2006 ROW:15 COL:1
		public string DateFrom9
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(152);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// FR ROW:15 COL:12
		public string FromDayOfWeek9
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(153);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 11/03/2006 ROW:15 COL:15
		public string DateTo9
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(154);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// FR ROW:15 COL:26
		public string ToDayOfWeek9
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(155);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  7:00 A ROW:15 COL:29
		public string FromTime9
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(156);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//    ROW:15 COL:37
		public string Field157
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(157);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  3:30 P ROW:15 COL:40
		public string ToTime9
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(158);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   -8.0 ROW:15 COL:48
		public string Hours9
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(159);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// SICK ROW:15 COL:56
		public string LeaveType9
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(161);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// APPR      ROW:15 COL:62
		public string Status9
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(163);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// EMP ROW:15 COL:73
		public string CreatedBy9
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(165);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:15 COL:77
		public string Field166
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(166);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:15 COL:79
		public string Chk10CV
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(167);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(167, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		// 10/24/2006 ROW:16 COL:1
		public string DateFrom10
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(168);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// TU ROW:16 COL:12
		public string FromDayOfWeek10
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(169);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 10/24/2006 ROW:16 COL:15
		public string DateTo10
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(170);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// TU ROW:16 COL:26
		public string ToDayOfWeek10
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(171);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  2:00 P ROW:16 COL:29
		public string FromTime10
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(172);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//    ROW:16 COL:37
		public string Field173
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(173);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  3:30 P ROW:16 COL:40
		public string ToTime10
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(174);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   -1.5 ROW:16 COL:48
		public string Hours10
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(175);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// SICK ROW:16 COL:56
		public string LeaveType10
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(177);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// APPR      ROW:16 COL:62
		public string Status10
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(179);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// EMP ROW:16 COL:73
		public string CreatedBy10
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(181);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:16 COL:77
		public string Field182
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(182);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:16 COL:79
		public string Chk11CV
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(183);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(183, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		// 10/19/2006 ROW:17 COL:1
		public string DateFrom11
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(184);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// TH ROW:17 COL:12
		public string FromDayOfWeek11
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(185);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 10/19/2006 ROW:17 COL:15
		public string DateTo11
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(186);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// TH ROW:17 COL:26
		public string ToDayOfWeek11
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(187);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  2:00 P ROW:17 COL:29
		public string FromTime11
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(188);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//    ROW:17 COL:37
		public string Field189
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(189);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  3:30 P ROW:17 COL:40
		public string ToTime11
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(190);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   -1.5 ROW:17 COL:48
		public string Hours11
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(191);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// ANNL ROW:17 COL:56
		public string LeaveType11
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(193);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// APPR      ROW:17 COL:62
		public string Status11
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(195);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// EMP ROW:17 COL:73
		public string CreatedBy11
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(197);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:17 COL:77
		public string Field198
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(198);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//                                                                                  ROW:17 COL:79
		public string Field199
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(199);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// You must type a "V" or a "C" next to a leave request.                ROW:19 COL:0
		public override string Message
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(200);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// PROD1G    ROW:19 COL:69
		public string Environment
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(201);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// =< 2411 >==< AL >==========< View/Cancel Requests >=============< P >==< R210 >  ROW:19 COL:79
		public string Footer
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(202);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// F1-Enter                F6-Page Frwd           SF9-Pause       F12-Menu/GoTo    ROW:21 COL:0
		public string Menu1
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(203);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// F3-Search Name         ROW:22 COL:0
		public string Menu2
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(204);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// SF6-Page Bkwd           SF10-Help      SF12-System Menu  ROW:22 COL:23
		public string Menu3
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(205);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//                                                                                ROW:23 COL:0
		public override string CommandLine
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(206);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(206, out col, out row);
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
