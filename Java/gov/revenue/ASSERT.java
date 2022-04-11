package gov.revenue;

/**
 *  ASSERT is used to add debug checks in the
 *  code.  In production releases, ASSERT code
 *  can be remove.  However, Java doesn't
 *  provide any built-in method for doing
 *  this.
 */
public class ASSERT
{
	/**
	 *  Set debug to one to enable debuging
	 *  code in other classes.
	 */
	public static final int debug = 0;
	
	/**
	 *  Check for a fatal error.  If the condition is 
	 *  true, display a message and halt execution.
	 */
	public static final void fatal(boolean cond, String file, int line, String msg)
	{
		if (! cond) 
		{
			System.out.println(msg);
			throw new AssertionFailure(file, line, msg);
		}
	}
}
