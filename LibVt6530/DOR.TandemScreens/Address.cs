using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Xml;

using Maximum;

namespace DOR.TandemScreens
{
	public class AddressFormatException : Exception
	{
		public AddressFormatException(string msg) : base(msg)
		{
		}
	}

	/// <summary>
	/// Represents an address, mailing or shipping, that may be associated with an account
	/// or a user.
	/// </summary>
	[Serializable]
	public class Address
	{
		protected int m_countryCode = 1;
		protected string m_country = "United States";
		protected string m_addressLine1 = "";
		protected string m_addressLine2 = "";
		protected string m_city = ""; 
		protected string m_state = "";
		protected string m_province = "";
		protected string m_zip5 = "00000";
		protected string m_zipPlus4 = "";
		protected bool m_standardizedExactMatch;

		#region Properites
		/// <summary>
		/// Gets or sets the first line of the address.
		/// </summary>
		public string AddressLine1
		{
			get { return m_addressLine1; }
			set { m_addressLine1 = value.Trim(); }
		}
		/// <summary>
		/// Gets or sets the second line of the address.
		/// </summary>
		public string AddressLine2
		{
			get { return m_addressLine2; }
			set { m_addressLine2 = value.Trim(); }
		}
		/// <summary>
		/// Gets or sets the city of the address.
		/// </summary>
		public string City
		{
			get {return m_city;}
			set { m_city = value.Trim(); }
		}
		/// <summary>
		/// Gets or sets the state of the address.
		/// </summary>
		public string State
		{
			get {return m_state;}
			set 
			{
				if (value.Trim().Length > 2)
				{
					throw new AddressFormatException("Invalid two-digit state code of " + value);
				}
				m_state = value.Trim().ToUpper();
			}
		}

		/// <summary>
		/// Gets or sets the province of the address.
		/// </summary>
		public string Province
		{
			get {return m_province;}
			set { m_province = value.Trim(); }
		}

		public string Zip
		{
			get
			{
				if (m_zipPlus4.Length > 0)
				{
					return m_zip5 + "-" + m_zipPlus4;
				}
				else
				{
					return m_zip5;
				}
			}
			set
			{
				string svalue = StringHelper.RemoveNonNumerics(value);
				if (svalue.Length >= 9)
				{
					ZipPlus4 = svalue.Substring(5, 4);
					m_zip5 = svalue.Substring(0, 5);
				}
				else if (svalue.Length >= 0)
				{
					ZipPlus4 = "";
					m_zip5 = value.PadLeft(5, '0').Substring(0, svalue.Length);
				}
				else
				{
					m_zip5 = value;
				}
			}
		}

		/// <summary>
		/// Gets or sets the zip of the address.
		/// </summary>
		public string Zip5
		{
			get
			{
				return m_zip5;
			}
			set 
			{
				if (value.Length != 5)
				{
					throw new AddressFormatException("Invalid five digit zip of " + value);
				}
				m_zip5 = value;
			}
		}
		/// <summary>
		/// Gets or sets the zip code suffix of the address.
		/// </summary>
		public string ZipPlus4
		{
			get {return m_zipPlus4;}
			set 
			{
				m_zipPlus4 = value.Trim();
				if (m_zipPlus4.Length > 4)
				{
					throw new AddressFormatException("Invalid zip plus4 of " + value);
				}
				if (m_zipPlus4.Length != 0)
				{
					m_zipPlus4 = m_zipPlus4.PadLeft(4, '0').Substring(0, 4);
				}
				else
				{
					m_zipPlus4 = "";
				}
			}
		}

		/// <summary>
		/// Gets or sets the country of the address.
		/// </summary>
		public string Country
		{
			get 
			{
				return m_country;
			}
		}
		
		/// <summary>
		/// Gets or sets the integer ID of the address's country.
		/// </summary>
		public int CountryId
		{
			get
			{
				return m_countryCode;
			}
			set
			{
				m_countryCode = value;
				if (m_countryCode == 1)
					m_country = "United States";
				else if (m_countryCode == 2)
					m_country = "Canada";
				else if (m_countryCode == 3)
					m_country = "";
                else
                {
					m_country = "";
                }
			}
		}
		
		#endregion
		
		/// <summary>
		/// Creates a new instance of Address using default values.
		/// </summary>
		public Address()
		{
		}

		/// <summary>
		/// Determines if two instances of Address are logically equal.
		/// </summary>
		/// <param name="o"></param>
		/// <returns></returns>
		public override bool Equals(object o)
		{
			return GetHashCode() == o.GetHashCode()
				&& o.GetType() == typeof(Address);
		}

		/// <summary>
		/// Gets a uniques hash code for this Address.
		/// </summary>
		/// <returns>An integer.</returns>
		public override int GetHashCode()
		{
			return m_addressLine1.GetHashCode() ^
				m_addressLine2.GetHashCode() ^
				m_city.GetHashCode() ^
				m_state.GetHashCode() ^
				Zip.GetHashCode();
		}
		
		/// <summary>
		/// Gets a string representation of an address.
		/// </summary>
		/// <returns>A string.</returns>
		public override string ToString()
		{
			return m_addressLine1 + "\n" +
				m_addressLine1 + "\n" +
				m_city + ", " + m_state + " " + Zip + "\n";
		}

		/// <summary>
		/// Gets a string representation of an address.
		/// </summary>
		/// <returns>A string.</returns>
		public string ToHTMLString()
		{
			string htmlFormatted = "";

			if(m_addressLine1.Length > 0)
				htmlFormatted += m_addressLine1 + "<br>";
			if (m_addressLine2.Length > 0)
				htmlFormatted += m_addressLine2 + "<br>";
			htmlFormatted += m_city;
			htmlFormatted += ", " + m_state + " " + Zip;
			return htmlFormatted;
		}

        /// <summary>
        /// Standardizes the address using the USPS web service. The standardized version of the address
        /// is available through the AddressStandardized property.
        /// Requires the following web.config setting:
        ///     <add key="DOR.Web.Authentication.USPSAddressStandardization.USPSAddressStandardization" value="http://dortestapps/USPSAddressStandardization/USPSAddressStandardization.asmx"/>
        /// </summary>
        public Address Standardize()
        {
            USPSAddressStandardization.USPSAddressStandardization usps = new USPSAddressStandardization.USPSAddressStandardization();

			XmlNode standardizeAddressResult = usps.StandardizeAddress(m_addressLine1 + m_addressLine2, "", m_city, m_state, Zip, false);

            if (standardizeAddressResult.SelectSingleNode("addressStandardized") != null)
            {
				Address addr = new Address();
				
				XmlNode standardizedAddress = standardizeAddressResult.SelectSingleNode("addressStandardized");
				addr.m_addressLine2 = standardizedAddress.SelectSingleNode("street").InnerText;
				addr.City = standardizedAddress.SelectSingleNode("city").InnerText;
				addr.State = standardizedAddress.SelectSingleNode("state").InnerText;
				addr.Zip5 = standardizedAddress.SelectSingleNode("zip").InnerText;
				addr.ZipPlus4 = standardizedAddress.SelectSingleNode("plus4").InnerText;

                //XmlNode county = standardizedAddress.SelectSingleNode("county");
                //if (county != null)
                //{
                //    this._addressStandardized.County = county.Attributes["name"].Value;
                //}

                XmlNode matchType = standardizeAddressResult.SelectSingleNode("result/addressMatchType");
				if (matchType != null)
				{
					addr.m_standardizedExactMatch = (matchType.Attributes["code"].Value == "1");
				}
				return addr;
			}
            else if (standardizeAddressResult.FirstChild.SelectSingleNode("error") != null)
            {
				throw new Exception(standardizeAddressResult.FirstChild.SelectSingleNode("error").InnerText);
            }
			return null;
        }

        /// <summary>
        /// Determines if the zip code is a valid zip code using the USPS Address Standardization web service.
        /// Requires the following web.config setting:
        ///     <add key="DOR.Web.Authentication.USPSAddressStandardization.USPSAddressStandardization" value="http://dortestapps/USPSAddressStandardization/USPSAddressStandardization.asmx"/>
        /// </summary>
        /// <param name="zip">5 digit US zip code</param>
        /// <returns>Boolean indicating the validity of the US zip code.</returns>
        public static bool IsValidZip(string zip)
        {
            bool validZip = false;

            if (zip.Length > 0)
            {
                USPSAddressStandardization.USPSAddressStandardization usps = new USPSAddressStandardization.USPSAddressStandardization();

                XmlNode validZipResult = usps.IsValidZip(zip);

                XmlNode result = validZipResult.SelectSingleNode("result");
                if (result != null)
                    validZip = (result.Attributes["isValid"].Value == "true");
            }
            return validZip;
        }

        /// <summary>
        /// Determines if the zip code is a valid zip code for the city and state provided using the 
        /// USPS Address Standardization web service.
        /// Requires the following web.config setting:
        ///     <add key="DOR.Web.Authentication.USPSAddressStandardization.USPSAddressStandardization" value="http://dortestapps/USPSAddressStandardization/USPSAddressStandardization.asmx"/>
        /// </summary>
        /// <param name="city">A US city name.</param>
        /// <param name="state">A US state name (Abbeviation of full name)</param>
        /// <param name="zip">A 5 digit US zip code</param>
        /// <returns></returns>
        public static bool IsValidCityZip(string city, string state, string zip)
        {
            if (city.Trim().Length > 0 && state.Trim().Length > 0 && zip.Trim().Length > 0)
            {
                USPSAddressStandardization.USPSAddressStandardization usps = new USPSAddressStandardization.USPSAddressStandardization();
                return usps.IsValidCityZip(city, state, zip);
            }
            else
                return false;
        }

	}
}
