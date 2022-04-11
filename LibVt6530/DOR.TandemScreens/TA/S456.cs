using System;
using System.Collections.Generic;
using System.Text;
using LibVt6530;

namespace DOR.TandemScreens.TA
{
	public class S456 : CommandLineScreen
	{
		internal S456(Terminal term, Vt6530 vt) : base(term, vt)
		{
			
		}

		public override bool VerifyLocation()
		{
			m_term.WaitEventScreen(false);
			// Check some of the protected fields to ensure 
			// the terminal is on this screen.
			return false;
		}

		public override void NavigateTo()
		{
			// Navigate to the screen.
			DOR.TandemScreens.CL.R002 s000;
			s000 = m_term.CreateClR002();
			s000.NavigateTo();
			s000.WaitScreenLoad();
			// go to 000 screen
			s000.CommandLineExec("456");
			WaitScreenLoad();
		}

		public override void WaitScreenLoad()
		{
			// Wait for the screen to finish loading.
			base.WaitScreenLoad();
		}

		// REG # ROW:0 COL:0
		public string Field0
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(0);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// REG       ROW:0 COL:7
		public string Tra
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(2);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(2, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		// DOC TYPE ROW:0 COL:18
		public string Field4
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(4);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     ROW:0 COL:28
		public string Field6
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(6);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     -- Account not found  --                  ROW:0 COL:33
		public string AccountNotFoundInd
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(8);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// INV # ROW:1 COL:0
		public string Field10
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(10);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// INV ROW:1 COL:7
		public string InvoiceNumber
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

		// DOC # ROW:1 COL:12
		public string Field14
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(14);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// DOC       ROW:1 COL:19
		public string DocNumber
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

		// Pd/Yr ROW:1 COL:30
		public string Field18
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(18);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//    ROW:1 COL:37
		public string PeriodNum
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(20);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//      ROW:1 COL:40
		public string PeriodYear
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(21);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// ID ROW:1 COL:46
		public string Field23
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(23);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//       ROW:1 COL:50
		public string ID
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(25);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// Bal Due ROW:1 COL:56
		public string Field26
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(26);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//                 ROW:1 COL:64
		public string BalanceDue
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(27);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//                               ROW:2 COL:0
		public string Field28
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(28);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// Issue Date: ROW:2 COL:30
		public string Field29
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(29);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//            ROW:2 COL:42
		public string IssueDate
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(30);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//                            ROW:2 COL:53
		public string Field31
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(31);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// Tran  Eff Date       Tran Amt   PmtType     Check/Note    RM?                 ROW:3 COL:0
		public string Field32
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(32);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:3 COL:78
		public string Field33
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(33);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// TRN ROW:4 COL:0
		public string TransactionCode
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

		//   ROW:4 COL:4
		public string Field35
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(35);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// EF ROW:4 COL:6
		public string AddEffectiveDateMonth
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

		// FA ROW:4 COL:9
		public string AddEffectiveDateDay
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(37);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(37, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		// DATE ROW:4 COL:12
		public string AddEffectiveDateYear
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(38);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(38, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		// TRANS AMOUNT ROW:4 COL:17
		public string TransactionAmount
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

		//      ROW:4 COL:30
		public string Field40
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(40);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// pym ROW:4 COL:35
		public string PaymentType
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(41);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(41, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		//      ROW:4 COL:39
		public string Field42
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(42);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// check note ROW:4 COL:44
		public string CheckNote
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

		//     ROW:4 COL:55
		public string Field44
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(44);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// Y ROW:4 COL:59
		public string PrintRmYN
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

		//                    ROW:4 COL:61
		public string Field46
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(46);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// Rsn ROW:5 COL:32
		public string Field47
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(47);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:5 COL:36
		public string Reason
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(48);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// Rtn ROW:5 COL:38
		public string Field49
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(49);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//               ROW:5 COL:42
		public string Return
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(50);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// PmtType ROW:5 COL:56
		public string Field51
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(51);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// Crdt ID ROW:5 COL:64
		public string Field52
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(52);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// RePrnt ROW:5 COL:72
		public string Field53
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(53);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     ROW:6 COL:0
		public string ListTranType
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(55);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:6 COL:4
		public string ListTranQual
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(56);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//            ROW:6 COL:6
		public string ListEffDate
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(57);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//              ROW:6 COL:17
		public string ListTranAmount
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(58);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:6 COL:30
		public string Field59
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(59);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     ROW:6 COL:32
		public string ListReason
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(60);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:6 COL:36
		public string Field61
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(61);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     ROW:6 COL:38
		public string ListBnkRtn
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(62);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:6 COL:42
		public string Field63
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(63);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//            ROW:6 COL:44
		public string ListCheckNote
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(64);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     ROW:6 COL:55
		public string Field65
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(65);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     ROW:6 COL:59
		public string ListPaymentType
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(66);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//         ROW:6 COL:64
		public string ListCreditId
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(68);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:6 COL:72
		public string Field69
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(69);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:6 COL:74
		public string ReprintInd
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(70);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     ROW:6 COL:76
		public string Field71
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(71);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     ROW:7 COL:0
		public string Field72
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(72);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:7 COL:4
		public string Field73
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(73);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//            ROW:7 COL:6
		public string Field74
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(74);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//              ROW:7 COL:17
		public string Field75
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(75);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:7 COL:30
		public string Field76
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(76);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     ROW:7 COL:32
		public string Field77
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(77);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:7 COL:36
		public string Field78
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(78);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     ROW:7 COL:38
		public string Field79
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(79);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:7 COL:42
		public string Field80
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(80);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//            ROW:7 COL:44
		public string Field81
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(81);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     ROW:7 COL:55
		public string Field82
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(82);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     ROW:7 COL:59
		public string Field83
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(83);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//         ROW:7 COL:64
		public string Field85
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(85);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:7 COL:72
		public string Field86
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(86);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:7 COL:74
		public string Field87
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(87);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     ROW:7 COL:76
		public string Field88
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(88);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     ROW:8 COL:0
		public string Field89
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(89);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:8 COL:4
		public string Field90
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(90);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//            ROW:8 COL:6
		public string Field91
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(91);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//              ROW:8 COL:17
		public string Field92
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(92);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:8 COL:30
		public string Field93
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(93);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     ROW:8 COL:32
		public string Field94
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(94);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:8 COL:36
		public string Field95
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(95);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     ROW:8 COL:38
		public string Field96
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(96);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:8 COL:42
		public string Field97
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(97);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//            ROW:8 COL:44
		public string Field98
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(98);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     ROW:8 COL:55
		public string Field99
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(99);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     ROW:8 COL:59
		public string Field100
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(100);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//         ROW:8 COL:64
		public string Field102
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(102);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:8 COL:72
		public string Field103
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(103);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:8 COL:74
		public string Field104
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(104);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     ROW:8 COL:76
		public string Field105
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(105);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     ROW:9 COL:0
		public string Field106
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(106);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:9 COL:4
		public string Field107
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(107);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//            ROW:9 COL:6
		public string Field108
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(108);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//              ROW:9 COL:17
		public string Field109
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(109);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:9 COL:30
		public string Field110
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(110);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     ROW:9 COL:32
		public string Field111
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(111);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:9 COL:36
		public string Field112
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(112);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     ROW:9 COL:38
		public string Field113
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(113);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:9 COL:42
		public string Field114
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(114);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//            ROW:9 COL:44
		public string Field115
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(115);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     ROW:9 COL:55
		public string Field116
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(116);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     ROW:9 COL:59
		public string Field117
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(117);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//         ROW:9 COL:64
		public string Field119
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(119);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:9 COL:72
		public string Field120
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(120);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:9 COL:74
		public string Field121
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(121);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     ROW:9 COL:76
		public string Field122
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(122);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     ROW:10 COL:0
		public string Field123
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(123);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:10 COL:4
		public string Field124
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(124);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//            ROW:10 COL:6
		public string Field125
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(125);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//              ROW:10 COL:17
		public string Field126
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(126);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:10 COL:30
		public string Field127
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(127);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     ROW:10 COL:32
		public string Field128
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(128);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:10 COL:36
		public string Field129
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(129);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     ROW:10 COL:38
		public string Field130
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(130);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:10 COL:42
		public string Field131
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(131);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//            ROW:10 COL:44
		public string Field132
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(132);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     ROW:10 COL:55
		public string Field133
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(133);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     ROW:10 COL:59
		public string Field134
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(134);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//         ROW:10 COL:64
		public string Field136
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(136);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:10 COL:72
		public string Field137
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(137);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:10 COL:74
		public string Field138
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(138);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     ROW:10 COL:76
		public string Field139
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(139);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     ROW:11 COL:0
		public string Field140
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(140);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:11 COL:4
		public string Field141
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(141);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//            ROW:11 COL:6
		public string Field142
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(142);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//              ROW:11 COL:17
		public string Field143
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(143);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:11 COL:30
		public string Field144
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(144);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     ROW:11 COL:32
		public string Field145
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(145);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:11 COL:36
		public string Field146
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(146);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     ROW:11 COL:38
		public string Field147
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(147);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:11 COL:42
		public string Field148
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(148);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//            ROW:11 COL:44
		public string Field149
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(149);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     ROW:11 COL:55
		public string Field150
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(150);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     ROW:11 COL:59
		public string Field151
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(151);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//         ROW:11 COL:64
		public string Field153
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(153);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:11 COL:72
		public string Field154
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(154);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:11 COL:74
		public string Field155
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(155);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     ROW:11 COL:76
		public string Field156
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(156);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     ROW:12 COL:0
		public string Field157
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(157);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:12 COL:4
		public string Field158
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(158);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//            ROW:12 COL:6
		public string Field159
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(159);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//              ROW:12 COL:17
		public string Field160
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(160);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:12 COL:30
		public string Field161
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(161);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     ROW:12 COL:32
		public string Field162
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(162);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:12 COL:36
		public string Field163
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(163);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     ROW:12 COL:38
		public string Field164
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(164);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:12 COL:42
		public string Field165
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(165);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//            ROW:12 COL:44
		public string Field166
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(166);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     ROW:12 COL:55
		public string Field167
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(167);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     ROW:12 COL:59
		public string Field168
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(168);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//         ROW:12 COL:64
		public string Field170
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(170);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:12 COL:72
		public string Field171
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(171);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:12 COL:74
		public string Field172
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(172);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     ROW:12 COL:76
		public string Field173
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(173);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     ROW:13 COL:0
		public string Field174
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(174);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:13 COL:4
		public string Field175
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(175);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//            ROW:13 COL:6
		public string Field176
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(176);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//              ROW:13 COL:17
		public string Field177
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(177);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:13 COL:30
		public string Field178
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(178);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     ROW:13 COL:32
		public string Field179
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(179);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:13 COL:36
		public string Field180
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(180);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     ROW:13 COL:38
		public string Field181
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(181);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:13 COL:42
		public string Field182
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(182);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//            ROW:13 COL:44
		public string Field183
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(183);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     ROW:13 COL:55
		public string Field184
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(184);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     ROW:13 COL:59
		public string Field185
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(185);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//         ROW:13 COL:64
		public string Field187
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(187);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:13 COL:72
		public string Field188
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(188);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:13 COL:74
		public string Field189
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(189);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     ROW:13 COL:76
		public string Field190
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(190);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     ROW:14 COL:0
		public string Field191
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(191);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:14 COL:4
		public string Field192
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(192);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//            ROW:14 COL:6
		public string Field193
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(193);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//              ROW:14 COL:17
		public string Field194
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(194);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:14 COL:30
		public string Field195
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(195);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     ROW:14 COL:32
		public string Field196
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(196);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:14 COL:36
		public string Field197
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(197);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     ROW:14 COL:38
		public string Field198
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(198);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:14 COL:42
		public string Field199
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(199);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//            ROW:14 COL:44
		public string Field200
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(200);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     ROW:14 COL:55
		public string Field201
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(201);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     ROW:14 COL:59
		public string Field202
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(202);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//         ROW:14 COL:64
		public string Field204
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(204);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:14 COL:72
		public string Field205
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(205);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:14 COL:74
		public string Field206
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(206);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     ROW:14 COL:76
		public string Field207
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(207);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     ROW:15 COL:0
		public string Field208
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(208);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:15 COL:4
		public string Field209
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(209);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//            ROW:15 COL:6
		public string Field210
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(210);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//              ROW:15 COL:17
		public string Field211
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(211);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:15 COL:30
		public string Field212
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(212);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     ROW:15 COL:32
		public string Field213
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(213);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:15 COL:36
		public string Field214
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(214);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     ROW:15 COL:38
		public string Field215
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(215);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:15 COL:42
		public string Field216
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(216);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//            ROW:15 COL:44
		public string Field217
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(217);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     ROW:15 COL:55
		public string Field218
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(218);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     ROW:15 COL:59
		public string Field219
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(219);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//         ROW:15 COL:64
		public string Field221
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(221);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:15 COL:72
		public string Field222
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(222);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//   ROW:15 COL:74
		public string Field223
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(223);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//     ROW:15 COL:76
		public string Field224
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(224);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// INQUIRY DATE:  ROW:18 COL:38
		public string Field225
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(225);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// in ROW:18 COL:53
		public string InquireDateMonth
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(226);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(226, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		// qd ROW:18 COL:56
		public string InquireDateDay
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

		// ate  ROW:18 COL:59
		public string InquireDatYear
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(228);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(228, out col, out row);
				m_vt.Display().SetCursorRowCol(row, col);
				m_vt.Display().WriteLocal(value);
			}
		}

		//                 ROW:18 COL:64
		public string Field229
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(229);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// Please supply a valid TRA ID & Inv Ctr or Doc Num                    ROW:19 COL:0
		public override string Message
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(230);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// TEST26    ROW:19 COL:69
		public string Environment
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(231);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// =< 456 >==< TA >==========< Transaction Add/Update >============< T >==< R456   ROW:20 COL:0
		public string Footer
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(233);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		//  F1-Enter F2-Add/Update   F4-JV    F6-Pg Frwd F10-Notes F11-Prev F12-Menu/Goto ROW:21 COL:0
		public string Menu1
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(234);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// SF1-By Date  SF2-Reprint Only     SF6-Pg Bkwd SF7-Ascending Order              ROW:22 COL:0
		public string Menu2
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(236);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
		}

		// Command LIne                                                                   ROW:23 COL:0
		public override string CommandLine
		{
			get
			{
				PageCell cell = m_vt.Display().GetDisplayPage().GetFieldStart(238);
				return m_vt.Display().GetDisplayPage().GetFieldASCII(cell);
			}
			set
			{
				int col; int row;
				m_vt.Display().GetDisplayPage().GetFieldXY(238, out col, out row);
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

/*  $TEST TRA's 
601726937
 601738625
 601862815
 601865422
 601990583
 600109969
 600402743
 600547314
 600600600
 600600600
 601262937
 601443705
 601726937
 601987299
 409008926
 600013374
 600638157
 601057183
 601057183
 601105157
 601813508
 601893023
 409010449
 600462919
 600504965
 601180624
 601529358
 601818015
 601818015
 601818015
 601914385
 602070932
 144003265
 600153804
 601914385
 800026010
 800037856
 800045370
 342007828
 600080904
 600554563
 600035896
 600662342
 409018245
 601951515
 601967410
 601998805
  48004932
 328044916
 328044916
 328044916
 601955701
 800009952
 800031999
 179023316
 601455476
 600497468
 600497468
 178001617
 601915267
 328029668
 600432711
 600528984
 600554918
 600649366
 601505469
 601701106
 601707131
 601707131
 601707131
 601785165
 601789608
 601789608
 601964689
 601988159
 601988159
 601988159
 602024272
 578037415
 600140899
 601596152
 600197729
 602011167
 602011167
 601693163
 601693163 
*/
