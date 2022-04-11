using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Diagnostics;
using System.Threading;

using DOR.TandemScreens;
using DOR.TandemScreens.LV;
using DOR.TandemScreens.CL;

namespace DOR.TandemScreens.LV
{
	public enum LeaveType
	{
		ANNUAL = 0,
		SICK = 1,
		ACCRUE_EXC = 2,
		BEREAVE = 3,
		COMP = 4,
		DON_ANN = 5,
		DON_PER = 6,
		DON_SICK = 7,
		EXC = 8,
		FMLA = 9,
		LWOP = 10,
		LIFE = 11,
		MIL = 12,
		OTHER = 13,
		PH = 14,
		SHARED = 15
	}

	public enum LeaveSickReasons
	{
		NA = 0,
		ILLNESS = 1,
		PREVENT = 2,
		MATERN = 3,
		RELATIVE = 4,
		PREVENT_RELATIVE = 5,
		DEATH = 6,
		WEATHER = 7,
		FAMILY_CARE = 8,
		OSHA = 9
	}

	/// <summary>
	/// Summary description for Leave
	/// </summary>
	[Serializable]
	public class LeaveData
	{
		protected Date m_from;
		protected string m_fromTime;
		protected Date m_to;
		protected string m_toTime;
		protected decimal m_hours;
		protected LeaveType m_type;
		protected string m_supv;
		protected LeaveSickReasons m_sickType;
		protected string m_explaination = "";

		public Date From
		{
			get { return m_from; }
			set { m_from = value; }
		}

		public string FromTime
		{
			get { return m_fromTime; }
			set
			{
				m_fromTime = value;
				if (m_fromTime.EndsWith(" AM") || m_fromTime.EndsWith(" PM"))
				{
					string ampm = m_fromTime.Substring(m_fromTime.Length - 2);
					m_fromTime = m_fromTime.Substring(0, m_fromTime.Length - 3);
					if (ampm == "PM")
					{
						// convert to mil time
						int hour = FromHour;
						int min = FromMinute;
						m_fromTime = (hour + 12).ToString("00") + ":" + min.ToString("00");
					}
				}
			}
		}

		public int FromHour
		{
			get { return Int32.Parse(m_fromTime.Substring(0, m_fromTime.IndexOf(':'))); }
		}

		public int FromHourAMPM
		{
			get
			{
				int h = FromHour;
				return (h > 12) ? h - 12 : h;
			}
		}

		public int FromMinute
		{
			get { return Int32.Parse(m_fromTime.Substring(m_fromTime.IndexOf(':') + 1)); }
		}

		public Date To
		{
			get { return m_to; }
			set { m_to = value; }
		}

		public string ToTime
		{
			get { return m_toTime; }
			set
			{
				m_toTime = value;
				if (m_toTime.EndsWith(" AM") || m_toTime.EndsWith(" PM"))
				{
					string ampm = m_toTime.Substring(m_toTime.Length - 2);
					m_toTime = m_toTime.Substring(0, m_toTime.Length - 3);
					if (ampm == "PM")
					{
						// convert to mil time
						int hour = FromHour;
						int min = FromMinute;
						m_toTime = (hour + 12).ToString("00") + ":" + min.ToString("00");
					}
				}
			}
		}

		public int ToHour
		{
			get { return Int32.Parse(m_toTime.Substring(0, m_toTime.IndexOf(':'))); }
		}

		public int ToHourAMPM
		{
			get
			{
				int h = ToHour;
				return (h > 12) ? h - 12 : h;
			}
		}

		public int ToMinute
		{
			get { return Int32.Parse(m_toTime.Substring(m_toTime.IndexOf(':') + 1)); }
		}

		public decimal Hours
		{
			get { return m_hours; }
			set { m_hours = value; }
		}

		public LeaveType LeaveType
		{
			get { return m_type; }
			set { m_type = value; }
		}

		public LeaveSickReasons SickReason
		{
			get { return m_sickType; }
			set { m_sickType = value; }
		}

		public string Supervisor
		{
			get { return m_supv; }
			set { m_supv = value; }
		}

		public string Explaination
		{
			get { return m_explaination; }
			set { m_explaination = value; }
		}

		public LeaveData()
		{
		}

		public bool Submit(Terminal term, string ssnLast4, LeaveSickReasons rsn, string explain, out string msg)
		{
			S2402 s2402 = term.CreateLvS2402();
			msg = "";
			s2402.NavigateTo(ssnLast4);
			switch (LeaveType)
			{
				case LeaveType.ANNUAL:
					s2402.ChkLeaveTypeAnnual = "X";
					break;
				case LeaveType.SICK:
					s2402.ChkLeaveTypeSick = "X";
					break;
				case LeaveType.PH:
					s2402.ChkLeaveTypePeronalHoliday = "X";
					break;
				default:
					throw new Exception("Unsupported leave type");
			}
			s2402.Submit();
			S2402_2 s2402_2 = term.CreateLvS2402_2();
			s2402_2.WaitScreenLoad();

			s2402_2.BeginingHour = FromHourAMPM.ToString("00");
			s2402_2.BeginingMin = FromMinute.ToString("00");
			s2402_2.BegingingAmPm = FromHour >= 12 ? "P" : "A";
			s2402_2.BeginingDay = From.Day.ToString("00");
			s2402_2.BeginingMonth = From.Month.ToString("00");
			s2402_2.BeginingYear = From.Year.ToString();

			s2402_2.EndingAmPm = ToHour >= 12 ? "P" : "A";
			s2402_2.EndingDay = To.Day.ToString("00");
			s2402_2.EndingHour = ToHourAMPM.ToString("00");
			s2402_2.EndingMin = ToMinute.ToString("00");
			s2402_2.EndingMonth = To.Month.ToString("00");
			s2402_2.EndingYear = To.Year.ToString();

			s2402_2.TotalHours = Hours.ToString("0.0");

			s2402_2.Submit();
			Thread.Sleep(100);
			term.WaitEventKbUnlock();

			if (rsn != LeaveSickReasons.NA)
			{
				if (LeaveType != LeaveType.SICK)
				{
					throw new Exception("Sick leave reason should be NA");
				}
				S2402_3 st = term.CreateLvS2402_3();
				st.WaitScreenLoad();

				if (explain.Length > st.ExplainationLine1.Length)
				{
					int len = st.ExplainationLine1.Length;
					st.ExplainationLine1 = explain.Substring(0, len);
					st.ExplainationLine2 = explain.Substring(len);
				}
				else
				{
					st.ExplainationLine1 = explain;
				}

				switch (rsn)
				{
					case LeaveSickReasons.DEATH:
						st.ChkBereavement = "X";
						break;
					case LeaveSickReasons.FAMILY_CARE:
						st.ChkUnforseenFamilyCare = "X";
						break;
					case LeaveSickReasons.ILLNESS:
						st.ChkIllness = "X";
						break;
					case LeaveSickReasons.MATERN:
						st.ChkMaternity = "X";
						break;
					case LeaveSickReasons.OSHA:
						st.ChkJobRelatedInjury = "X";
						break;
					case LeaveSickReasons.PREVENT:
						st.ChkPreventiveCare = "X";
						break;
					case LeaveSickReasons.PREVENT_RELATIVE:
						st.ChkPreventiveCareForRelative = "X";
						break;
					case LeaveSickReasons.RELATIVE:
						st.ChkIllnessOfRelative = "X";
						break;
					case LeaveSickReasons.WEATHER:
						st.ChkInclementWeather = "X";
						break;
					default:
						throw new Exception("Unknown sick leave reason");
				}
				st.Submit();
				term.WaitEventKbUnlock();
			}
			while (s2402_2.Message.Trim() == "" && s2402.Message.Trim() == "")
			{
				term.WaitEvent();
			}
			if (s2402.Message.Trim().StartsWith("Leave request sent to supervisor for approval"))
			{
				msg = s2402.Message.Trim();
				Supervisor = s2402.ToLastFirst;
				return true;
			}
			msg = s2402_2.Message.Trim() + s2402.Message.Trim();
			return false;
		}

		//public static bool GoTo2402(Terminal term, string ssnLast4, out S2402 s2402)
		//{
		//    R002 r002 = term.CreateClR002();
		//    S2401 s2401 = term.CreateLvS2401();
		//    S2401_2 s2401_2 = term.CreateLvS2401_2();
		//    s2402 = term.CreateLvS2402();

		//    r002.NavigateTo();

		//    // now go to the 2401 screen (SSN)
		//    r002.CommandLineExec("2401");
		//    s2401.WaitScreenLoad();

		//    // send the SSN
		//    int count = 0;
		//    while (s2401.SsnCaption.Trim() == "Enter Last Four Digits of Your SSN:")
		//    {
		//        if (s2401.VerifyLocation() && (count % 2) == 0)
		//        {
		//            s2401.SsnLastFour = ssnLast4;
		//            s2401.Submit();
		//            term.WaitEventScreen(true);
		//        }
		//        else if (s2401.VerifyLocation())
		//        {
		//            Thread.Sleep(300);
		//        }
		//        if (s2401_2.VerifyLocation())
		//        {
		//            break;
		//        }
		//        if (s2401.Message.Trim() == "Social security number entered is incorrect. Access Denied.")
		//        {
		//            return false;
		//        }
		//        if (count++ > 5)
		//        {
		//            throw new Exception("Can't load 2401");
		//        }
		//    }
		//    Debug.Assert(term.FieldCount > 14);

		//    // load the leave data/start screen (2402)
		//    s2401_2.CommandLineExec("2402");

		//    return s2402.VerifyLocation();
		//}

		private static void GoTo2410_2(Terminal term, S2402 s2402, int index, out S2410_2 s2410_2)
		{
			Debug.Assert(index > 0);
			s2410_2 = term.CreateLvS2410_2();
			if (!s2402.VerifyLocation())
			{
				throw new Exception("Should be on the 2402 screen before calling GetLeaveToApprove");
			}
			S2401 s2401 = term.CreateLvS2401();
			S2401_2 s2401_2 = term.CreateLvS2401_2();

			term.KeyF12();
			term.WaitEventScreen(true);
			s2401_2.WaitScreenLoad();
			s2401_2.CommandLineExec("2410");
			S2410 s2410 = term.CreateLvS2410();
			int count = 0;
			while
			(
				!s2401.Message.Trim().StartsWith("Current Logon ID not authorized to access function")
				&& !s2410.VerifyLocation()
			)
			{
				if (count++ > 4)
				{
					throw new Exception("Can't navigate to 2410");
				}
				term.WaitEvent();
			}
			s2410.Select(index);
			s2410.Submit();
			term.WaitEventScreen(true);
			s2410_2 = term.CreateLvS2410_2();
			s2410_2.WaitScreenLoad();
		}

		public static LeaveData GetLeaveToApprove(Terminal term, S2402 s2402, int index, out S2410_2 s2410_2)
		{
			GoTo2410_2(term, s2402, index, out s2410_2);

			LeaveData lv = new LeaveData();
			lv.From = new Date(DateTime.Parse(s2410_2.StateDate));
			lv.FromTime = s2410_2.StartTime;
			lv.Hours = Decimal.Parse(s2410_2.TotalHours);
			lv.Supervisor = "You";
			lv.To = new Date(DateTime.Parse(s2410_2.EndDate));
			lv.ToTime = s2410_2.EndTime;
			lv.Explaination = s2410_2.ReasonText1.Trim() + s2410_2.ReasonText2.Trim();

			switch (s2410_2.LeaveType.Trim())
			{
				case "ANNL":
					lv.LeaveType = LeaveType.ANNUAL;
					break;
				case "SICK":
					lv.LeaveType = LeaveType.SICK;
					switch (s2410_2.Reason.Trim())
					{
						case "PERSONAL ILLNESS":
							lv.SickReason = LeaveSickReasons.ILLNESS;
							break;
						case "PERSONAL PREVENTIVE CARE":
							lv.SickReason = LeaveSickReasons.PREVENT;
							break;
						default:
							throw new Exception("Unsupported sick leave reason of " + s2410_2.Reason);
					}
					break;
				default:
					throw new Exception("Unsupported leave type of " + s2410_2.LeaveType);
			}
			return lv;
		}

		public bool Approve(Terminal term, S2402 s2402, int index, out string msg)
		{
			msg = "";

			S2410_2 s2410_2;
			GoTo2410_2(term, s2402, index, out s2410_2);
			if (m_explaination.Length > 0)
			{
				if (m_explaination.Length > s2410_2.ReasonText1.Length)
				{
					int len = s2410_2.ReasonText1.Length;
					s2410_2.ReasonText1 = m_explaination.Substring(0, len);
					s2410_2.ReasonText2 = m_explaination.Substring(len);
				}
				else
				{
					s2410_2.ReasonText1 = m_explaination;
				}
			}
			s2410_2.Approve();
			msg = s2410_2.ApproveCompleteInd;
			return true;
		}
	}
}
