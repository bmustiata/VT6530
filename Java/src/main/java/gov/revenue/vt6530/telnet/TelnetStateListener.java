package gov.revenue.vt6530.telnet;

import java.awt.Dimension;

public interface TelnetStateListener
{
	public String ttype();
	public void localEchoOff();
	public void localEchoOn();
	public Dimension naws();
	public void setMode(char mode);
	public void message(String msg);
	public void enquire();
}
