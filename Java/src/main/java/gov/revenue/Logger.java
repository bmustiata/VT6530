package gov.revenue;

import java.io.PrintStream;

/**
 *  Logger is used to log messages.  Currently,
 *  it is only used in debug builds.
 */
public class Logger
{	
	public static PrintStream out;
	static String processName;
	
	static 
	{
		out = System.out;
	}
	
	/**
	 *  Set the output file name.
	 */
	public static void setOutput(String filename)
	{
		try
		{
			out = new PrintStream(new java.io.FileOutputStream(filename));
		}
		catch(java.io.IOException ieo)
		{
			ieo.printStackTrace();
		}
	}
	
	public static void log(String msg)
	{
		out.println(/*new java.util.Date().toString() + "\n" + processName + ": " +*/ msg);
	}	
}
