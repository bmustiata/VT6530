package gov.revenue.vt6530.telnet;

import java.io.IOException;

/** Exception thrown when a Telnet connection takes too long
 * before receiving a specified String token.
 * @author George Ruban
 * @version 0.1 7/30/97 */
public class TimedOutException extends IOException
{
  public TimedOutException()
  {
  }

  public TimedOutException(String message)
  {
    super(message);
  }
}

