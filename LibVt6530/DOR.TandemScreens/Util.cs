using System;

namespace DOR.TandemScreens.SR
{
	/// <summary>
	/// Summary description for Util.
	/// </summary>
	public class Util
	{
		private Util()
		{
		}

		internal static int CountWorkdays( DateTime start, DateTime end )
		{
			int count = 0;
			while ( start.Date <= end.Date )
			{
				if ( ! (IsWeekend( start ) || IsHoliday( start )) )
				{
					count++;
				}
				start = start.AddDays(1);
			}
			return count;
		}

		internal static bool IsWeekend(DateTime dtm)
		{
			if( dtm.DayOfWeek == DayOfWeek.Sunday || dtm.DayOfWeek == DayOfWeek.Saturday )
				return true;
			return false;
		}

		internal static bool IsHoliday(DateTime dtmDate)
		{
			int intDay;

			intDay = dtmDate.Day;

			switch ( dtmDate.Month )
			{
				case 1:
					//	New Years
					//	RULE
					//	January 1.  If on a weekend, the previous friday is off
					if (intDay == 1 && ( dtmDate.DayOfWeek != DayOfWeek.Saturday && dtmDate.DayOfWeek != DayOfWeek.Sunday) ) 
					{
						return true;
					}
				
					//	MLK Day
					//	RULE
					//	celebrated as third Monday in January, King's birthday is Jan 18.
					if ( dtmDate.DayOfWeek == DayOfWeek.Monday )
					{
						if ( dtmDate.AddDays(7).Month == 1 && dtmDate.AddDays(14).Month == 2 )
						{
							return true;
						}
					}
					break;
				case 2:
					//	Presidents' Day
					//	RULE
					//	Third Monday in February.  Replaced Lincoln and Washington's birthday in 1971.
					if ( dtmDate.DayOfWeek == DayOfWeek.Monday )
					{
						if ( dtmDate.AddDays(7).Month == 2 && dtmDate.AddDays(14).Month == 3 )
						{
							return true;
						}
					}
					break;
				case 5:
					//	Memorial Day
					//	RULE
					//	Last Monday in May. Originally May 31
					if ( dtmDate.DayOfWeek == DayOfWeek.Monday )
					{
						return dtmDate.AddDays(7).Month == 6;
					}
					break;
				case 7:
					// July 4th
					// RULE
					//  July 4.  If on a weekend, the previous friday is off
					if (intDay == 4 && ( dtmDate.DayOfWeek != DayOfWeek.Saturday && dtmDate.DayOfWeek != DayOfWeek.Sunday) )
					{
						return true;
					}
					else if ( (intDay == 2 || intDay == 3) && dtmDate.DayOfWeek == DayOfWeek.Friday )
					{
						return true;
					}
					break;
				case 9:
					//	Labor Day
					//	RULE
					//	First Monday in September. In 1882..1883 it was celebrated on September 5.
					if ( dtmDate.DayOfWeek == DayOfWeek.Monday ) 
					{
						if ( dtmDate.AddDays(-7).Month == 8 )
						{
							return true;
						}
					}
					break;
				case 10:
					//	Columbus Day
					//	RULE
					//	1905 .. 1970 -> October 12
					//	1971 .. now  -> Second Monday in October.
					if ( dtmDate.DayOfWeek == DayOfWeek.Monday )
					{
						if ( dtmDate.AddDays(-14).Month == 9 && dtmDate.AddDays(14).Month == 10 )
						{
							return true;
						}
					}
					break;
				case 11:
					// Veterans' Day
					// RULE
					// November 11.  If on a weekend, the previous friday is off
					if (intDay == 11 && dtmDate.DayOfWeek != DayOfWeek.Saturday && dtmDate.DayOfWeek != DayOfWeek.Sunday )
					{
						return true;
					}
					else if ( intDay == 9 || intDay == 10 && dtmDate.DayOfWeek == DayOfWeek.Friday )
					{
						return true;
					}
					//  Thanksgiving 
					//	RULE
					//	1621         -> first Thanksgiving, precise date unknown.
					//	1622         -> was no Thanksgiving.
					//	1623 .. 1675 -> precise date unknown.
					//	1676 .. 1862 -> June 29.
					//	1863 .. 1938 -> last Thursday of November.
					//	1939 .. 1941 -> 2nd to last Thursday of November.
					//	1942 .. now  -> 4th Thursday of November.
					if ( dtmDate.DayOfWeek == DayOfWeek.Thursday && intDay > 22 ) 
					{
						if ( dtmDate.AddDays(7).Month == 12 )
						{
							return true;
						}
					}
					// friday is also a bank holiday
					if ( dtmDate.DayOfWeek == DayOfWeek.Friday && intDay > 22 ) 
					{
						if ( dtmDate.AddDays(7).Month == 12 )
						{
							return true;
						}
					}
					break;
				case 12:
					//	Christmass
					//	RULE
					//	December 25.  If on a weekend, the previous friday is off
					if (intDay == 25 && ( dtmDate.DayOfWeek != DayOfWeek.Saturday && dtmDate.DayOfWeek != DayOfWeek.Sunday)) 
					{
						return true;
					}
					else if ( (intDay == 23 || intDay == 24) && dtmDate.DayOfWeek == DayOfWeek.Friday )
					{
						// the 25th is on the following saturday or sunday
						return true;
					}
					//	New Years
					//	RULE
					//	January 1.  If on a weekend, the previous friday is off
					if ( dtmDate.Day > 29 && dtmDate.DayOfWeek == DayOfWeek.Friday )
					{
						// the friday of Dec 30 or Dec 31 is a holiday (jan 1 is on saturday or sunday)
						return true;
					}
					break;
			}
			return false;
		}	
	}
}
