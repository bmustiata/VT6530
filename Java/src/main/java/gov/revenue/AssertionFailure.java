package gov.revenue;

/**
 *  AssertionFailure is thown when an ASSERT
 *  fails.  Since it extends Error, it isn't
 *  normally expected to be caught by try/catch.
 * 
 *  In production code, you might want to call
 *  System.exit() instead of throwing this.
 */
public class AssertionFailure extends Error
{
	public AssertionFailure(String file, int line, String msg)
	{
		super("ASSERTion failure!\nFile:" + file + "\nLine: " + Integer.toString(line) + "\n" + msg);
	}
}
