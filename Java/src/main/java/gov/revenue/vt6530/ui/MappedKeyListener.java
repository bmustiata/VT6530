package gov.revenue.vt6530.ui;

/**
 *  Listener for key events
 */
public interface MappedKeyListener
{
	void mappedKey(String s);
	void command(byte c);
	int getPage();
	int getCursorX();
	int getCursorY();
	String getStartFieldASCII();
}
