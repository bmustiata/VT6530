using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Text;
using System.Net;
using System.Xml;

namespace DOR.TandemScreens.SR
{
	public class ServletException : Exception
	{
		public ServletException ( string msg ) : base( msg )
		{
		}
	}

	/// <summary>
	/// Post calls to tandem servlets
	/// </summary>
	public class JavaServletHelper
	{
		private JavaServletHelper()
		{
		}

		private static int port = 1028;

		public static int Port
		{
			get { return port; }
			set { port = value; }
		}

		public static void SetPort(string env)
		{
			if (env == "PROD" || env == "$PROD")
			{
				port = 80;
			}
			else if (env == "DEMO" || env == "$DEMO")
			{
				port = 1028;
			}
			else
			{
				port = 1027;
			}
		}

		public static DataSet SystemList(  )
		{
			Hashtable prms = new Hashtable();
			string xml = Send( "http://tandem8:" + port + "/servlet/pr.SrAgency", "syslist", prms );
			DataSet ds = new DataSet();
			ds.ReadXml( new StringReader( xml ) );
			return ds;
		}

		public static DataSet PhaseList()
		{
			Hashtable prms = new Hashtable();
			string xml = Send("http://tandem8:" + port + "/servlet/pr.SrReport", "phaselist", prms);
			DataSet ds = new DataSet();
			ds.ReadXml(new StringReader(xml));
			return ds;
		}

		public static DataSet SrDetails(int srnum)
		{
			Hashtable prms = new Hashtable();
			prms.Add( "srnum", srnum.ToString() );
			string xml = Send( "http://tandem8:" + port + "/servlet/pr.SrReport", "srdetail", prms );
			DataSet ds = new DataSet();
			ds.ReadXml( new StringReader( xml ) );
			return ds;
		}

		public static DataSet SrHourDetails(int srnum)
		{
			Hashtable prms = new Hashtable();
			prms.Add("srnum", srnum.ToString());
			string xml = Send("http://tandem8:" + port + "/servlet/pr.SrReport", "srhoursdetail", prms);
			DataSet ds = new DataSet();
			ds.ReadXml(new StringReader(xml));
			return ds;
		}

		public static DataSet Sr(int srnum)
		{
			Hashtable prms = new Hashtable();
			prms.Add( "srnum", srnum.ToString() );
			string xml = Send( "http://tandem8:" + port + "/servlet/pr.SrReport", "loadsr", prms );
			DataSet ds = new DataSet();
			ds.ReadXml( new StringReader( xml ) );
			return ds;
		}

		/// <summary>
		/// Get a dataset with hour by service request for employees
		/// </summary>
		/// <param name="supervisorId">Supervisor's numeric ID</param>
		/// <param name="from">Start Date</param>
		/// <param name="to">Stop Date</param>
		/// <returns>DataSet</returns>
		public static DataSet LoadAllSr( int supervisorId, string systemsCSV, DateTime from, DateTime to )
		{
			Hashtable prms = new Hashtable();
			prms.Add( "supervisor", supervisorId.ToString() );
			prms.Add( "from", from.ToString( "yyyyMMdd" ) );
			prms.Add( "to", to.ToString( "yyyyMMdd" ) );
			prms.Add( "systemlist", systemsCSV );

			string xml = Send( "http://tandem8:" + port + "/servlet/pr.SrReport", "load", prms );
			DataSet ds = new DataSet();
			ds.ReadXml( new StringReader( xml ) );
			return ds;
		}

		public static DataSet LoadEmp( int supervisorId, DateTime from, DateTime to )
		{
			Hashtable prms = new Hashtable();
			prms.Add( "supervisor", supervisorId.ToString() );
			prms.Add( "from", from.ToString( "yyyyMMdd" ) );
			prms.Add( "to", to.ToString( "yyyyMMdd" ) );

			string xml = Send( "http://tandem8:" + port + "/servlet/pr.SrReport", "loademp", prms );
			DataSet ds = new DataSet();
			ds.ReadXml( new StringReader( xml ) );
			return ds;
		}

		/// <summary>
		/// Get a dataset with all service requests for the listed systems.
		/// </summary>
		/// <param name="systemsCSV">(Use * for all service requests)</param>
		/// <returns></returns>
		/// <exception cref="ServletException"></exception>
		/// <exception cref="XmlException"></exception>
		public static DataSet ListServiceRequests( string systemsCSV )
		{
			Hashtable prms = new Hashtable();
			prms.Add( "syslist", systemsCSV );
			string xml = Send( "http://tandem8:" + port + "/servlet/pr.SrReport", "syssr", prms );
			DataSet ds = new DataSet();
			ds.ReadXml( new StringReader( xml ) );
			return ds;
		}

		/// <summary>
		/// Get a dataset with all service requests for the agency
		/// </summary>
		/// <param name="systemsCSV"></param>
		/// <returns></returns>
		/// <exception cref="ServletException"></exception>
		/// <exception cref="XmlException"></exception>
		public static DataSet Agency( int year )
		{
			Hashtable prms = new Hashtable();
			prms.Add( "from", new DateTime(year, 1, 1).ToString( "yyyyMMdd" ) );
			string xml = Send( "http://tandem8:" + port + "/servlet/pr.SrAgency", "load", prms );
			DataSet ds = new DataSet();
			ds.ReadXml( new StringReader( xml ) );
			return ds;
		}

		public static string Send( string url, string action, Hashtable prms )
		{
			StringBuilder xml = new StringBuilder();
			xml.Append( "xml=<?xml version=\"1.0\"?><call action='" );
			xml.Append( action );
			xml.Append( "'><parameters>" );
			
			foreach ( string key in prms.Keys )
			{
				xml.Append( "<param name='" );
				xml.Append( key );
				xml.Append( "'>" );
				xml.Append( (string)prms[key] );
				xml.Append( "</param>" );
			}
			xml.Append( "</parameters></call>" );
			
			WebRequest req = HttpWebRequest.Create( url );
			req.Timeout = 1000 * 120;
			req.Method = "POST";
			req.ContentLength = xml.Length;
			req.ContentType = "text/xml";
			Stream str = req.GetRequestStream();
			str.Write( System.Text.UTF8Encoding.ASCII.GetBytes( xml.ToString() ), 0, xml.Length );
			str.Close();
			HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

			if ( resp.StatusCode != HttpStatusCode.OK )
			{
				throw new ServletException( resp.StatusDescription );
			}
			str = resp.GetResponseStream();
			xml.Length = 0;

			StreamReader reader = new StreamReader( str, Encoding.UTF8 );
			string respxml = reader.ReadToEnd();
			str.Close();

			if ( respxml.IndexOf( "<error" ) > -1 || respxml.IndexOf("<H1>Servlet Exception Occured</H1>") > -1 )
			{
				throw new ServletException( respxml );
			}
			return respxml;
		}
	}
}
