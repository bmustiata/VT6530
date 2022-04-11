using System;
using System.Collections.Generic;
using System.Diagnostics;
using LibVt6530;

namespace DOR.TandemScreens.BR
{
	public struct R360Activity
	{
		public string DateChanged;
		public string Description;
		public string DorUserId;
		public string Effective;
	}

	public class R360 : CommandLineScreen
	{
		internal R360(Terminal term, Vt6530 vt)
			: base(term, vt)
		{
			m_vt = vt;
		}

		public List<R360Activity> ListAll()
		{
			Debug.Assert(VerifyLocation());
			m_term.WaitEventScreen(false);
			List<R360Activity> list = new List<R360Activity>();
			while (LastPageInd2.Trim() == "")
			{
				R360Activity act;
				act.DateChanged = Date1.Trim();
				act.Description = Desc1.Trim();
				act.DorUserId = DorUserId1.Trim();
				act.Effective = Effective1.Trim();
				if (act.DateChanged.Length > 0)
				{
					list.Add(act);
				}
				act.DateChanged = Date2.Trim();
				act.Description = Desc2.Trim();
				act.DorUserId = DorUserId2.Trim();
				act.Effective = Effective2.Trim();
				if (act.DateChanged.Length > 0)
				{
					list.Add(act);
				}
				act.DateChanged = Date3.Trim();
				act.Description = Desc3.Trim();
				act.DorUserId = DorUserId3.Trim();
				act.Effective = Effective3.Trim();
				if (act.DateChanged.Length > 0)
				{
					list.Add(act);
				}
				act.DateChanged = Date4.Trim();
				act.Description = Desc4.Trim();
				act.DorUserId = DorUserId4.Trim();
				act.Effective = Effective4.Trim();
				if (act.DateChanged.Length > 0)
				{
					list.Add(act);
				}
				act.DateChanged = Date5.Trim();
				act.Description = Desc5.Trim();
				act.DorUserId = DorUserId5.Trim();
				act.Effective = Effective5.Trim();
				if (act.DateChanged.Length > 0)
				{
					list.Add(act);
				}
				act.DateChanged = Date6.Trim();
				act.Description = Desc6.Trim();
				act.DorUserId = DorUserId6.Trim();
				act.Effective = Effective6.Trim();
				if (act.DateChanged.Length > 0)
				{
					list.Add(act);
				}
				act.DateChanged = Date7.Trim();
				act.Description = Desc7.Trim();
				act.DorUserId = DorUserId7.Trim();
				act.Effective = Effective7.Trim();
				if (act.DateChanged.Length > 0)
				{
					list.Add(act);
				}
				act.DateChanged = Date8.Trim();
				act.Description = Desc8.Trim();
				act.DorUserId = DorUserId8.Trim();
				act.Effective = Effective8.Trim();
				if (act.DateChanged.Length > 0)
				{
					list.Add(act);
				}
				act.DateChanged = Date9.Trim();
				act.Description = Desc9.Trim();
				act.DorUserId = DorUserId9.Trim();
				act.Effective = Effective9.Trim();
				if (act.DateChanged.Length > 0)
				{
					list.Add(act);
				}
				act.DateChanged = Date10.Trim();
				act.Description = Desc10.Trim();
				act.DorUserId = DorUserId10.Trim();
				act.Effective = Effective10.Trim();
				if (act.DateChanged.Length > 0)
				{
					list.Add(act);
				}
				act.DateChanged = Date11.Trim();
				act.Description = Desc11.Trim();
				act.DorUserId = DorUserId11.Trim();
				act.Effective = Effective11.Trim();
				if (act.DateChanged.Length > 0)
				{
					list.Add(act);
				}
				act.DateChanged = Date12.Trim();
				act.Description = Desc12.Trim();
				act.DorUserId = DorUserId12.Trim();
				act.Effective = Effective12.Trim();
				if (act.DateChanged.Length > 0)
				{
					list.Add(act);
				}
				act.DateChanged = Date13.Trim();
				act.Description = Desc13.Trim();
				act.DorUserId = DorUserId13.Trim();
				act.Effective = Effective13.Trim();
				if (act.DateChanged.Length > 0)
				{
					list.Add(act);
				}
				m_vt.FakeF6();
				m_term.WaitEventScreen(true);
			}
			return list;
		}

		public override bool VerifyLocation()
		{
			m_term.WaitEventScreen(false);
			// Check some of the protected fields to ensure 
			// the terminal is on this screen.
			return Footer == "=< 109 >==< BR >===========< TRA Activity Inquiry >=============< P >==< R360 > "
				|| Footer == "=< 109 >==< BR >===========< TRA Activity Inquiry >=============< D >==< R360 > "
				|| Footer == "=< 109 >==< BR >===========< TRA Activity Inquiry >=============< T >==< R360 > ";
		}

		public override void NavigateTo()
		{
			throw new NotImplementedException();
		}

		public void NavigateTo(string tra)
		{
			DOR.TandemScreens.CL.R002 m_000;
			DOR.TandemScreens.BR.S101 m_101;
			m_000 = m_term.CreateClR002();
			m_101 = m_term.CreateBrS101();
			m_000.NavigateTo();
			m_000.WaitScreenLoad();
			// go to 101 screen
			m_000.CommandLineExec("101");
			m_101.WaitScreenLoad();
			TRA = tra;
			m_vt.FakeF6();
			m_term.WaitEventScreen(true);
			Debug.Assert(VerifyLocation());
		}

		// Enter Reg #:
		public string Field0
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(0);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//          
		public string TRA
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(1);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int x; int y;
				m_vt.Display().GetDisplayPage().GetFieldXY(1, out x, out y);
				m_vt.Display().SetCursorRowCol(y, x);
				m_vt.Display().WriteLocal(value);
			}
		}

		//                                                         
		public string Field2
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(2);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// Reg #:
		public string Field3
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(3);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 601 601 601
		public string TraFormated
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

		//  Name:
		public string Field6
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(6);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// JOHNSON GREGGORY G                           
		public string BusinessName
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(7);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//                           
		public string Field8
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(8);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   DBA:
		public string Field9
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(9);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// GREGG JOHNSONS AUTOMOTIVE                    
		public string DBA
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(10);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
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

		// Date Changed
		public string Field12
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(12);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  
		public string Field13
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(13);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// Description
		public string Field14
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

		// User#
		public string Field16
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(16);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// Effective Date
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

		// 11/02/2006
		public string Date1
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(20);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// ADDRESS CHANGE    
		public string Desc1
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(22);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 03373
		public string DorUserId1
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(24);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//           
		public string Effective1
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

		// 10/06/2006
		public string Date2
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(28);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// PAC ASSIGNED      
		public string Desc2
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(30);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 03781
		public string DorUserId2
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(32);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//           
		public string Effective2
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(34);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//                        
		public string Field35
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(35);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 08/28/2006
		public string Date3
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(36);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// ELF END DATE      
		public string Desc3
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(38);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 08026
		public string DorUserId3
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(40);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 08/28/2006
		public string Effective3
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(42);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
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

		// 01/11/2006
		public string Date4
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(44);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// ELF SET UP        
		public string Desc4
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(46);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 01170
		public string DorUserId4
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(48);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//           
		public string Effective4
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

		// 08/12/2005
		public string Date5
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(52);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// AUTO RTL RTN OFF  
		public string Desc5
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(54);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 00001
		public string DorUserId5
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(56);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 08/12/2005
		public string Effective5
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(58);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//                        
		public string Field59
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(59);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 06/24/2005
		public string Date6
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(60);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// EFT PMT TO TOUCHT 
		public string Desc6
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(62);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 00184
		public string DorUserId6
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(64);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 06/24/2005
		public string Effective6
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(66);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
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

		// 06/24/2005
		public string Date7
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(68);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// EFT BANK ACCT# CH 
		public string Desc7
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(70);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 00184
		public string DorUserId7
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(72);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 06/24/2005
		public string Effective7
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

		// 06/24/2005
		public string Date8
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(76);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// EFT BANK TRS RT CH
		public string Desc8
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(78);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 00184
		public string DorUserId8
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(80);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 06/24/2005
		public string Effective8
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(82);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//                        
		public string Field83
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(83);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 05/04/2005
		public string Date9
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(84);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// EFT BANK LOC CHG  
		public string Desc9
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(86);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 00184
		public string DorUserId9
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(88);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 05/04/2005
		public string Effective9
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(90);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
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

		// 05/04/2005
		public string Date10
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(92);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// EFT PMT TO VOICE  
		public string Desc10
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(94);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 00184
		public string DorUserId10
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(96);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 05/04/2005
		public string Effective10
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

		// 05/04/2005
		public string Date11
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(100);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// EFT BANK ACCT#    
		public string Desc11
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(102);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 00184
		public string DorUserId11
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(104);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 05/04/2005
		public string Effective11
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(106);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//                        
		public string Field107
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(107);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 05/04/2005
		public string Date12
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(108);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// EFT BANK TRS RT   
		public string Desc12
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(110);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 00184
		public string DorUserId12
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(112);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 05/04/2005
		public string Effective12
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(114);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
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

		// 05/04/2005
		public string Date13
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(116);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// EFT BANK ACCT TYPE
		public string Desc13
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(118);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 00184
		public string DorUserId13
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(120);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// 05/04/2005
		public string Effective13
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(122);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		public override string Message
		{
			get { return LastPageInd1 + LastPageInd2; }
		}

		//                        
		public string LastPageInd1
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(123);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//                                                                     
		public string LastPageInd2
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(124);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// PROD1C   
		public string Environment
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(125);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// =< 109 >==< BR >===========< TRA Activity Inquiry >=============< P >==< R360 > 
		public string Footer
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(126);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// F1-Enter 
		public string Field127
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(127);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// F6-Pg Fwd 
		public string Field128
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(128);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// SF6-Pg Bwd   
		public string Field129
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(129);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// F9-Addr Hist      
		public string Field130
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(130);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// SF10-Help 
		public string Field131
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(131);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// F12-Menu/GoTo 
		public string Field132
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(132);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//                                                                               
		public override string CommandLine
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(133);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int x; int y;
				m_vt.Display().GetDisplayPage().GetFieldXY(133, out x, out y);
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
