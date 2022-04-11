using System;
using System.Collections.Generic;
using System.Diagnostics;
using LibVt6530;

namespace DOR.TandemScreens.LV
{
	public class S2402_2 : Screen
	{
		internal S2402_2(Terminal term, Vt6530 vt)
			: base(term, vt)
		{
			m_vt = vt;
		}

		public override bool VerifyLocation()
		{
			m_term.WaitEventScreen(false);
			// Check some of the protected fields to ensure 
			// the terminal is on this screen.
			return Foother == "=< 2402 >==< AL >==============< Request Leave >================< P >==< R100 > "
				|| Foother == "=< 2402 >==< AL >==============< Request Leave >================< D >==< R100 > "
				|| Foother == "=< 2402 >==< AL >==============< Request Leave >================< T >==< R100 > ";
		}

		//  Time   AM/PM         Date
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

		// Leave Request
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

		// Beginning:
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

		// 08
		public string BeginingHour
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

		// :
		public string Field7
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(7);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 00
		public string BeginingMin
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

		//   
		public string Field9
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(9);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// A
		public string BegingingAmPm
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(10);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int x; int y;
				m_vt.Display().GetDisplayPage().GetFieldXY(10, out x, out y);
				m_vt.Display().SetCursorRowCol(y, x);
				m_vt.Display().WriteLocal(value);
			}
		}

		//     
		public string Field11
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(11);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 11
		public string BeginingMonth
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(12);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int x; int y;
				m_vt.Display().GetDisplayPage().GetFieldXY(12, out x, out y);
				m_vt.Display().SetCursorRowCol(y, x);
				m_vt.Display().WriteLocal(value);
			}
		}

		// /
		public string Field13
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(13);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 20
		public string BeginingDay
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(14);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int x; int y;
				m_vt.Display().GetDisplayPage().GetFieldXY(14, out x, out y);
				m_vt.Display().SetCursorRowCol(y, x);
				m_vt.Display().WriteLocal(value);
			}
		}

		// /
		public string Field15
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(15);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 2006
		public string BeginingYear
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(16);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int x; int y;
				m_vt.Display().GetDisplayPage().GetFieldXY(16, out x, out y);
				m_vt.Display().SetCursorRowCol(y, x);
				m_vt.Display().WriteLocal(value);
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

		// Ending:
		public string Field18
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(18);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
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

		// 05
		public string EndingHour
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(20);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int x; int y;
				m_vt.Display().GetDisplayPage().GetFieldXY(20, out x, out y);
				m_vt.Display().SetCursorRowCol(y, x);
				m_vt.Display().WriteLocal(value);
			}
		}

		// :
		public string Field21
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(21);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 00
		public string EndingMin
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(22);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int x; int y;
				m_vt.Display().GetDisplayPage().GetFieldXY(22, out x, out y);
				m_vt.Display().SetCursorRowCol(y, x);
				m_vt.Display().WriteLocal(value);
			}
		}

		//   
		public string Field23
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(23);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// P
		public string EndingAmPm
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(24);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int x; int y;
				m_vt.Display().GetDisplayPage().GetFieldXY(24, out x, out y);
				m_vt.Display().SetCursorRowCol(y, x);
				m_vt.Display().WriteLocal(value);
			}
		}

		//     
		public string Field25
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(25);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 11
		public string EndingMonth
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(26);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int x; int y;
				m_vt.Display().GetDisplayPage().GetFieldXY(26, out x, out y);
				m_vt.Display().SetCursorRowCol(y, x);
				m_vt.Display().WriteLocal(value);
			}
		}

		// /
		public string Field27
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(27);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 20
		public string EndingDay
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(28);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int x; int y;
				m_vt.Display().GetDisplayPage().GetFieldXY(28, out x, out y);
				m_vt.Display().SetCursorRowCol(y, x);
				m_vt.Display().WriteLocal(value);
			}
		}

		// /
		public string Field29
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(29);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 2006
		public string EndingYear
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

		// hh : mm           mm / dd / yyyy
		public string Field32
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(32);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//                    
		public string Field33
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(33);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// Total Hours Requested:
		public string Field34
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(34);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 8.0  
		public string TotalHours
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(35);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int x; int y;
				m_vt.Display().GetDisplayPage().GetFieldXY(35, out x, out y);
				m_vt.Display().SetCursorRowCol(y, x);
				m_vt.Display().WriteLocal(value);
			}
		}

		// Ex. 8.0
		public string Field37
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(37);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//                                    
		public string Field38
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(38);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// Partial Hours:
		public string Field39
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(39);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// Partial                                 
		public string PartialHours
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(40);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int x; int y;
				m_vt.Display().GetDisplayPage().GetFieldXY(40, out x, out y);
				m_vt.Display().SetCursorRowCol(y, x);
				m_vt.Display().WriteLocal(value);
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
		public string Field42
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

		// Use for military leave (y/n):
		public string Field44
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(44);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  
		public string Field45
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(45);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//                                          
		public string Field46
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(46);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//                                                             
		public string Field47
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(47);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//             
		public string Field48
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(48);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//                                                             
		public string Field49
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(49);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//             
		public string Field50
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
		public string Field52
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(52);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  Leave request sent to supervisor for approval. (???)                                              
		public string Message
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(53);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// PROD0C   
		public string Environment
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(54);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// =< 2402 >==< AL >==============< Request Leave >================< P >==< R100 > 
		public string Foother
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(55);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// F1-Enter         F3-Cancel request            SF9-Pause       SF10-Help        
		public string Menu
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(56);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		public void Submit()
		{
			m_vt.FakeF1();
		}
	}
}
