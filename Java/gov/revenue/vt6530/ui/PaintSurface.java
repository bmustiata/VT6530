package gov.revenue.vt6530.ui;

import java.awt.Color;
import java.awt.event.KeyListener;

/**
 *  Interface to a physical rendering surface.
 */
public interface PaintSurface
{
	int getPixelWidth();
	int getPixelHeight();
	
	void setFont(String fontName, int attributes, int fontSize);
	int getFontWidth();
	int getFontHeight();
	int getFontDescent();
	
	Color getBackGroundColor();
	Color getForeGroundColor();
	void setBackGroundColor(Color c);
	void setForeGroundColor(Color c);
	
	void setPaintXorMode(Color c);
	void setPaintMode();
	
	void fillRect(int x1, int y1, int x2, int y2);
	void drawBytes(byte[] text, int offset, int length, int x1, int y1);
	void drawLine(int x1, int y1, int x2, int y2);
	
	void registerKeyListener(KeyListener listener);
	
	void setPaintPeer(PaintPeer peer);
	void forceRepaint();
}
