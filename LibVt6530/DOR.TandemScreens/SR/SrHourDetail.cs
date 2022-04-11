using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DOR.TandemScreens.SR
{
	public class SrHourDetail
	{
		protected string m_lastName;
		protected string m_firstName;
		protected int m_doruserId;
		protected int m_srnum;
		protected string m_date;
		protected string m_phase;
		protected string m_commentKey;
		protected string m_dwnldCode;
		protected decimal m_hours;

		public SrHourDetail(DataRow dr)
		{
			m_lastName = (string)dr["LastName"];
			m_firstName = (string)dr["FirstName"];
			m_doruserId = Int32.Parse((string)dr["DORUSER_ID"]);
			m_srnum = Int32.Parse((string)dr["SR"]);
			m_date = (string)dr["Date"];
			m_phase = (string)dr["Phase"];
			m_commentKey = (string)dr["CommentKey"];
			m_dwnldCode = (string)dr["DwnldCode"];
			m_hours = Decimal.Parse((string)dr["Hours"]);
		}

		public string LastName
		{
			get { return m_lastName; }
		}

		public string FirstName
		{
			get { return m_firstName; }
		}

		public int SrNum
		{
			get { return m_srnum; }
		}

		public string Phase
		{
			get { return m_phase; }
		}

		public string PhaseLit
		{
			get
			{
				switch (m_phase.Trim())
				{
					case "1000":
						return "REQUEST";
					case "2000":
						return "ANALYSIS";
					case "3000":
						return "DESIGN";
					case "4000":
						return "DEVELOPMENT";
					case "5000":
						return "IMPLEMENTATION";
					case "6000":
						return "MAINTENANCE";
					case "9000":
						return "ADMIN";
					default:
						return m_phase;
				}
			}
		}

		public string Date
		{
			get { return m_date; }
		}

		public string DateFormated
		{
			get { return m_date.Substring(4,2) + "/" + m_date.Substring(6,2) + "/" + m_date.Substring(0, 4); }
		}

		public string DwnldCode
		{
			get { return m_dwnldCode; }
		}

		public Decimal Hours
		{
			get { return m_hours; }
		}

		public static List<SrHourDetail> List(DataTable dt)
		{
			List<SrHourDetail> list = new List<SrHourDetail>();
			for (int x = 0; x < dt.Rows.Count; x++)
			{
				SrHourDetail sdt = new SrHourDetail(dt.Rows[x]);
				list.Add(sdt);
			}
			return list;
		}
	}
}
