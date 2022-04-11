using System;
using System.Data;

namespace DOR.TandemScreens.SR
{
	[Serializable]
	public class ServiceRequest
	{
		public ServiceRequest( DataRow row )  
		{
			m_id = Int32.Parse((string)row["SR"]);
			m_name = (string)row["Name"];
			m_system = (string)row["System"];
			m_priority = Int32.Parse((string)row["Priority"]);
			if ( null != row.Table.Columns["Status"] )
			{
				m_status = Int32.Parse((string)row["Status"]);
			}
			if ( null != row.Table.Columns["Hours"] )
			{
				m_hours = (float)Double.Parse( (string)row["Hours"] );
			}
			if ( null != row.Table.Columns["Est"] )
			{
				if ( "null" != (string)row["Est"] )
				{
					m_estimate = (float)Double.Parse( (string)row["Est"] );
				}
			}
		}

		public ServiceRequest( int id, string name, string system, int priority, int status )
		{
			m_id = id;
			m_name = name;
			m_system = system;
			m_priority = priority;
			m_status = status;
		}

		public int Id
		{
			get
			{
				return m_id;
			}
		}

		public string Name
		{
			get
			{
				return m_name;
			}
		}

		public string System
		{
			get
			{
				return m_system;
			}
		}

		public int Priority
		{
			get
			{
				return m_priority;
			}
		}

		public int Status
		{
			get
			{
				return m_status;
			}
		}

		public float Hours
		{
			get
			{
				return m_hours;
			}
			set
			{
				m_hours = value;
			}
		}

		public float PeriodHours
		{
			get
			{
				return m_phours;
			}
			set
			{
				m_phours = value;
			}
		}

		public float Estimate
		{
			get
			{
				return m_estimate;
			}
			set
			{
				m_estimate = value;
			}
		}

		protected int m_id;
		protected string m_name;
		protected string m_system;
		protected int m_priority;
		protected int m_status;
		protected float m_hours;
		protected float m_phours;
		protected float m_estimate;
	}
}
