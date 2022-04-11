using System;
using System.Diagnostics;

namespace DOR.TandemScreens
{
	/// <summary>
	/// Date with no time
	/// </summary>
	public class Date
	{
		/// <summary>
		/// Initialize to current date
		/// </summary>
		public Date()
		{
			Init(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
		}

		/// <summary>
		/// Initialize
		/// </summary>
		/// <param name="dt"></param>
		public Date(DateTime dt)
		{
			Init(dt.Year, dt.Month, dt.Day);
		}

		/// <summary>
		/// Create a new date
		/// </summary>
		/// <param name="year"></param>
		/// <param name="month"></param>
		/// <param name="day"></param>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		public Date(int year, int month, int day)
		{
			Init(year, month, day);
		}

		/// <summary>
		/// year part
		/// </summary>
		public int Year
		{
			get
			{
				return m_year;
			}
		}

		/// <summary>
		/// month part
		/// </summary>
		public int Month
		{
			get
			{
				return m_month;
			}
		}

		/// <summary>
		/// day part
		/// </summary>
		public int Day
		{
			get
			{
				return m_day;
			}
		}

		/// <summary>
		/// convert to datetime
		/// </summary>
		public DateTime AsDateTime()
		{
			return new DateTime(m_year, m_month, m_day);
		}

		public int AsRevInt()
		{
			return m_year * 10000 + m_month * 100 + m_day;
		}

		/// <summary>
		/// Extremely limited format cap
		/// </summary>
		/// <param name="frmt">"MMDDYY"</param>
		/// <returns></returns>
		public string Format(string frmt)
		{
			if (frmt == "MMDDYY")
			{
				Debug.Assert(1.ToString("00") == "01", "ToString format assumption failure");
				return m_month.ToString("00") + m_day.ToString("00") + TwoDigitYear(m_year).ToString("00");
			}
			throw new ArgumentException("Can't format to " + frmt);
		}

		/// <summary>
		/// Return a four digit year from a two digit one
		/// </summary>
		/// <param name="year"></param>
		/// <returns></returns>
		public static int TwoDigitYear(int year)
		{
			Debug.Assert(Int32.Parse(year.ToString().Substring(2, 2)) == year % 100, "Conversion assumption failure");
			return year % 100;
		}

		public static Date ParseRevInt(int dtm)
		{
			int year = dtm / 10000;
			dtm -= (year * 10000);
			int mo = dtm / 100;
			Debug.Assert(mo > 0 && mo < 13);
			int dy = (dtm - mo * 100);
			Debug.Assert(dy > 0 && dy < 32);
			return new Date(year, mo, dy);
		}

		public static bool operator <(Date d1, Date d2)
		{
			return d1.AsRevInt() < d2.AsRevInt();
		}

		public static bool operator >(Date d1, Date d2)
		{
			return d1.AsRevInt() > d2.AsRevInt();
		}

		/// <summary>
		/// Are the two dates equal?
		/// </summary>
		/// <param name="d1"></param>
		/// <param name="d2"></param>
		/// <returns></returns>
		public static bool operator ==(Date d1, Date d2)
		{
			if ((object)d1 == (object)d2)
			{
				return true;
			}
			if ((object)d1 == null || (object)d2 == null)
			{
				return false;
			}
			return d1.Equals(d2);
		}

		/// <summary>
		/// Are the two date different
		/// </summary>
		/// <param name="d1"></param>
		/// <param name="d2"></param>
		/// <returns></returns>
		public static bool operator !=(Date d1, Date d2)
		{
			return !d1.Equals(d2);
		}

		/// <summary>
		/// same as ==
		/// </summary>
		/// <param name="o"></param>
		/// <returns></returns>
		public override bool Equals(object o)
		{
			Date d2 = o as Date;
			if (d2 == null)
			{
				return false;
			}
			return m_day == d2.m_day && m_month == d2.m_month && m_year == d2.m_year;
		}

		/// <summary>
		/// required override
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>
		/// Init
		/// </summary>
		/// <param name="year"></param>
		/// <param name="month"></param>
		/// <param name="day"></param>
		private void Init(int year, int month, int day)
		{
			Debug.Assert(year > 0, "Invalid year of " + year);
			Debug.Assert(month > 0 && month < 13, "Invalid month of " + month);
			Debug.Assert(day > 0 && day < 32, "Invalid day of " + day);

			if (year < 1 || month <= 0 || month > 12 || day <= 0 || day > 31)
			{
				throw new ArgumentOutOfRangeException("Date", "Invalid date (" + month + "/" + day + "/" + year + ")");
			}
			m_year = year;
			m_month = month;
			m_day = day;
		}

		public override string ToString()
		{
			return m_month.ToString("00") + "/" + m_day.ToString("00") + "/" + m_year.ToString();
		}

		protected int m_year;
		protected int m_month;
		protected int m_day;
	}
}

