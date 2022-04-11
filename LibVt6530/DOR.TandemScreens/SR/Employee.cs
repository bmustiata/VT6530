using System;
using System.Collections;
using System.Data;
using System.Diagnostics;

namespace DOR.TandemScreens.SR
{
	public class EmpSr
	{
		public int srnum;
		public float hours;

		public EmpSr()
		{
		}
	}

	public class Employee
	{
		public Employee( int id, string name )
		{
			m_id = id;
			m_name = name;
		}

		public void AddHours( int sr, float hours )
		{
			EmpSr es = (EmpSr)m_srhs[(Int32)sr];
			if ( null == es )
			{
				es = new EmpSr();
				es.srnum = sr;
				es.hours = hours;
				m_srhs.Add( (Int32)es.srnum, es );
			}
			else
			{
				es.hours += hours;
			}
			m_total += hours;
		}

		public void GetSrHours( DataTable tbl, ServiceRequests srs )
		{
			foreach ( EmpSr es in m_srhs.Values )
			{
				DataRow row = tbl.NewRow();
				row[0] = m_name;
				row[1] = es.srnum;
				ServiceRequest sr = srs[(Int32)es.srnum];
				if ( null == sr )
				{
					sr = srs.LoadSr( es.srnum );
				}
				row[2] = sr.Name;
				row[3] = es.hours;
				tbl.Rows.Add( row );
			}
		}

		public string Name
		{
			get
			{
				return m_name;
			}
		}

		public float TotalHours
		{
			get
			{
				return m_total;
			}
		}

		protected int m_id;
		protected string m_name;
		protected float m_total;
		protected ArrayList m_srms = new ArrayList();
		protected Hashtable m_srhs = new Hashtable();
	}
}
