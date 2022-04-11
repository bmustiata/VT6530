using System;
using System.Diagnostics;
using Maximum;

namespace DOR.TandemScreens
{
	/// <summary>
	/// A Phone Number
	/// </summary>
	public class PhoneNumber
	{
		protected int m_area;
		protected int m_prefix;
		protected int m_suffix;
		protected string m_ext;

		public PhoneNumber()
		{
		}

		public PhoneNumber( int area, int prefix, int suffix ) : this( area, prefix, suffix, "" )
		{
		}

		public PhoneNumber( int area, int prefix, int suffix, string ext )
		{
			m_area = area;
			m_prefix = prefix;
			m_suffix = suffix;
			m_ext = ext;
		}

		public PhoneNumber( long phoneNum )
		{
			ParseNumeric( phoneNum );
		}

		public PhoneNumber( string phoneNum )
		{
			string stripped = phoneNum;
			if ( stripped.IndexOf('(') > -1 )
			{
				stripped = stripped.Replace("(", "").Replace(")", "");
			}
			if ( stripped.IndexOf(' ') > -1 )
			{
				stripped = stripped.Replace(" ", "");
			}
			if ( stripped.IndexOf('-') > -1 )
			{
				stripped = stripped.Replace("-", "");
			}
			if ( 0 == stripped.Length )
			{
				return;
			}
			if ( stripped.Length != 10 )
			{
				throw new Exception( phoneNum + " is an invalid phone number" );
			}
			long iphoneNum = 0;
			if ( StringHelper.IsNumeric( stripped ) )
			{
				iphoneNum = Int64.Parse( stripped );
				ParseNumeric( iphoneNum );
			}
			else
			{
				throw new Exception( phoneNum + " is an invalid phone number" );
			}
		}

		private void ParseNumeric( long phoneNum )
		{
			m_area = (int)(phoneNum / 10000000L);
			phoneNum -= m_area * 10000000L;
			Debug.Assert(phoneNum.ToString().Length == 7);
			m_prefix = (int)(phoneNum / 10000);
			m_suffix = (int)(int)(phoneNum - m_prefix * 10000);
		}

		public int AreaCode
		{
			get
			{
				return m_area;
			}
		}

		public int Prefix
		{
			get
			{
				return m_prefix;
			}
		}

		public int Suffix
		{
			get
			{
				return m_suffix;
			}
		}

		public string Extension
		{
			get
			{
				return m_ext;
			}
		}

		public override string ToString()
		{
			if ( 0 == m_area && 0 == m_prefix && 0 == m_suffix )
			{
				return String.Empty;
			}
			if ( m_ext.Length > 0 )
			{
				return "(" + m_area.ToString("000") + ") " + m_prefix.ToString("000") + "-" + m_suffix.ToString("0000") + " EXT: " + m_ext;
			}
			else
			{
				return "(" + m_area.ToString("000") + ") " + m_prefix.ToString("000") + "-" + m_suffix.ToString("0000");
			}
		}

		public long ToInt()
		{
			long ret = m_area * 10000000L + m_prefix * 10000 + m_suffix;
			Debug.Assert( ret == Int64.Parse(m_area.ToString() + m_prefix.ToString("000") + m_suffix.ToString("0000")) );

			return ret;
		}
	}
}
