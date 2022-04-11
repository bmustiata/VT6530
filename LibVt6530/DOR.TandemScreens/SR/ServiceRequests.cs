using System;
using System.Data;
using System.Collections;
using System.Diagnostics;

namespace DOR.TandemScreens.SR
{
	/// <summary>
	/// Summary description for ServiceRequests.
	/// </summary>
	[Serializable]
	public class ServiceRequests
	{
		public ServiceRequests( DataTable srs )
		{
			for ( int x = 0; x < srs.Rows.Count; x++ )
			{
				ServiceRequest sr = new ServiceRequest( srs.Rows[x] );
				m_reqsById.Add( (Int32)sr.Id, sr );
				m_reqs.Add( sr );
			}
		}

		public ServiceRequest LoadSr( int srnum )
		{
			DataSet ds = JavaServletHelper.Sr( srnum );
			ServiceRequest sr = new ServiceRequest( ds.Tables[0].Rows[0] );
			m_reqsById.Add( (Int32)sr.Id, sr );
			m_reqs.Add( sr );
			return sr;
		}

		public ServiceRequests( DataSet dsagency )
		{
			DataTable srs = dsagency.Tables["srs"];
			DataTable hrs = dsagency.Tables["hours"];
			DataTable est = dsagency.Tables["est"];
			
			for ( int x = 0; x < srs.Rows.Count; x++ )
			{
				ServiceRequest sr = new ServiceRequest( srs.Rows[x] );
				m_reqsById.Add( (Int32)sr.Id, sr );
				m_reqs.Add( sr );
			}
			for ( int x = 0; x < hrs.Rows.Count; x++ )
			{
				int srnum = Int32.Parse( (string)hrs.Rows[x][0] );
				string shours = (string)hrs.Rows[x][1];
				if ( "null" != shours )
				{
					ServiceRequest sr = (ServiceRequest)m_reqsById[ (Int32)srnum ];
					if ( null == sr )
					{
						sr = LoadSr( srnum );
					}
					sr.Hours = (float)Double.Parse( shours );
				}
			}
			for ( int x = 0; x < est.Rows.Count; x++ )
			{
				int srnum = Int32.Parse( (string)est.Rows[x][0] );
				string shours = (string)est.Rows[x][1];
				if ( "null" != shours )
				{
					ServiceRequest sr = (ServiceRequest)m_reqsById[ (Int32)srnum ];
					if ( null == sr )
					{
						sr = LoadSr( srnum );
					}
					sr.Estimate = (float)Double.Parse( shours );
				}
			}
		}

		public DataSet CreateView( bool all, bool complete, bool priority, bool filterZeroHours )
		{
			DataSet ds = new DataSet();
			DataTable dt = ds.Tables.Add();
			dt.Columns.Add("SR");
			dt.Columns.Add("Sys");
			dt.Columns.Add("Name");
			if ( priority )
			{
				dt.Columns.Add("Priority", typeof(Int32));
			}
			dt.Columns.Add("Hours");
			dt.Columns.Add("Est");

			for ( int x = 0; x < m_reqs.Count; x++ )
			{
				ServiceRequest sr = (ServiceRequest)m_reqs[x];
				if ( filterZeroHours && sr.Hours == 0 )
				{
					continue;
				}
				if ( all || (complete && sr.Status == 6) || (! complete && sr.Status < 6) )
				{
					if ( priority )
					{
						dt.Rows.Add( new object[] { sr.Id, sr.System, sr.Name, sr.Priority, sr.Hours, sr.Estimate } );
					}
					else
					{
						dt.Rows.Add( new object[] { sr.Id, sr.System, sr.Name, sr.Hours, sr.Estimate } );
					}
				}
			}
			return ds;
		}

		public DataSet EmployeePeriodData(int sup, DateTime from, DateTime to)
		{
			if ( 0 == m_empdata.Count )
			{
				DataTable empdata = JavaServletHelper.LoadEmp( sup, from, to ).Tables[0];
				for ( int x = 0; x < empdata.Rows.Count; x++ )
				{
					Int32 uid = Int32.Parse( (string)empdata.Rows[x]["UID"] );
					Employee emp = (Employee)m_empdata[uid];
					if ( null == emp )
					{
						emp = new Employee( uid, (string)empdata.Rows[x]["Name"] );
						m_empdata.Add( (Int32)uid, emp );
					}
					emp.AddHours( Int32.Parse((string)empdata.Rows[x]["SR"]), (float)Double.Parse((string)empdata.Rows[x]["Hours"]) );
				}
			}
			DataSet ds = new DataSet();
			ds.Tables.Add();
			ds.Tables[0].Columns.Add("Employee" );
			ds.Tables[0].Columns.Add("SR #" );
			ds.Tables[0].Columns.Add("SR Name");
			ds.Tables[0].Columns.Add("Hours" );

			foreach ( Employee emp in m_empdata.Values )
			{
				emp.GetSrHours( ds.Tables[0], this );
			}
			return ds;
		}

		public DataSet EmployeePeriodTotals(int sup, DateTime from, DateTime to)
		{
			if ( 0 == m_empdata.Count )
			{
				EmployeePeriodData(sup, from, to);
			}
			// create the employee hour totals
			DataSet ds = new DataSet();
			ds.Tables.Add();
			ds.Tables[0].Columns.Add("Name" );
			ds.Tables[0].Columns.Add("Hours" );

			foreach ( Employee emp in m_empdata.Values )
			{
				DataRow row = ds.Tables[0].NewRow();
				row["Name"] = emp.Name;
				row["Hours"] = emp.TotalHours;
				ds.Tables[0].Rows.Add( row );
			}
			return ds;
		}

		public ServiceRequest this[ int index ]
		{
			get
			{
				return (ServiceRequest)m_reqsById[(Int32)index];
			}
		}

		protected Hashtable m_reqsById = new Hashtable();
		protected ArrayList m_reqs = new ArrayList();
		protected Hashtable m_empdata = new Hashtable();
	}
}
