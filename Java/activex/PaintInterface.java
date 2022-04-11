package activex;

import com.ms.wfc.core.*;
import com.ms.wfc.ui.*;

import java.awt.event.KeyListener;

import gov.revenue.vt6530.Term;
import gov.revenue.vt6530.ui.PaintSurface;
import gov.revenue.vt6530.ui.PaintPeer;

public class PaintInterface implements PaintSurface
{
	private UserControl control;
	private Term term;
	
	private Graphics gfx;
	private Font font;
	private RasterOp currentROP;
	private Brush background = new Brush(new Color(0,0,0), BrushStyle.SOLID);
	private Brush foreground = new Brush(new Color(0, 0xFF, 0), BrushStyle.SOLID); 
	
	private PaintPeer peer;
	private java.awt.event.KeyListener keyListener;
	
	private int charWidth;      /* current width of a char */
	private int charHeight;      /* current height of a char */
	private int charDescent;      /* base line descent */	
	private String fontName = "Courier";
	private int fontSize = 40;

	PaintInterface (UserControl control, Term term)
	{
		this.control = control;
		this.term = term;
		setFont(fontName, 0, fontSize);
	}
	
	public void resize()
	{
		setFont(fontName, 0, fontSize);
	}
	
	public int getPixelWidth()
	{
		return control.getWidth();
	}
	
	public int getPixelHeight()
	{
		return control.getHeight();
	}
	
	public void setFont(String fontName, int attributes, int fontSize)
	{
		float scaleFontSize = fontSize;
		int width = control.getWidth();
		int height = control.getHeight();
		Graphics g = control.createGraphics();
		
		//font = new Font(fontName, scaleFontSize, FontSize.PIXELS);
		font = new Font(FontFamily.MODERN, FontPitch.FIXED, scaleFontSize, FontSize.PIXELS);
		FontDescriptor fd = new FontDescriptor(font, g);
		charWidth = fd.maxWidth;
		charHeight = fd.height;
		charDescent = fd.descent;

		//while ((charWidth * 80) > width)
		while ((charWidth * 80 > width || (charHeight/*+charDescent*/) * 25 > height) && fontSize > 1)
		{
			scaleFontSize *= .95;
			font = new Font(FontFamily.MODERN, FontPitch.FIXED, scaleFontSize, FontSize.PIXELS);
			fd = new FontDescriptor(font, g);
			charWidth = fd.maxWidth;
			charHeight = fd.height;
			charDescent = fd.descent;
		}
		
		control.setFont(font);		
	}
	
	public int getFontWidth()
	{
		return charWidth;
	}
	
	public int getFontHeight()
	{
		return charHeight;
	}
	
	public int getFontDescent()
	{
		return charDescent;
	}
	
	public java.awt.Color getBackGroundColor()
	{
		return new java.awt.Color(background.getColor().getRGB());
	}
	
	public java.awt.Color getForeGroundColor()
	{
		return new java.awt.Color(foreground.getColor().getRGB());
	}
	
	public void setBackGroundColor(java.awt.Color c)
	{
		Color winc = new Color(c.getRed(), c.getGreen(), c.getBlue());
		background = new Brush(winc, BrushStyle.SOLID);
		
		if (gfx == null)
			;//control.setBackColor(winc);
		else
		{
			gfx.setBackColor(winc);
		}
	}
	
	public void setForeGroundColor(java.awt.Color c)
	{
		Color winc = new Color(c.getRed(), c.getGreen(), c.getBlue());
		foreground = new Brush(winc, BrushStyle.SOLID);

		if (gfx == null)
			;//control.setForeColor(winc);
		else
		{
			gfx.setTextColor(winc);
			gfx.setPen(new Pen(winc, PenStyle.SOLID));
			gfx.setBrush(foreground);
		}
	}
	
	public void setPaintXorMode(java.awt.Color c)
	{
		currentROP = RasterOp.TARGET.xorWith(RasterOp.SOURCE);
	}
	
	public void setPaintMode()
	{
		currentROP = RasterOp.SOURCE;
	}
	
	public void fillRect(int x1, int y1, int x2, int y2)
	{
		gfx.drawRect(x1, y1, x2, y2, currentROP);
	}
	
	public void drawBytes(byte[] text, int offset, int length, int x1, int y1)
	{
		gfx.drawString(new String(text, offset, length), x1, y1);
	}
	
	public void drawLine(int x1, int y1, int x2, int y2)
	{
		gfx.drawLine(x1, y1, x2, y2, currentROP);
	}
	
	public void registerKeyListener(KeyListener listener)
	{
		keyListener = listener;
	}
	
	public void setPaintPeer(PaintPeer peer)
	{
		this.peer = peer;
	}

	public void forceRepaint()
	{
		Graphics g = control.createGraphics();
		//control.invalidate();
		onPaint(g);
	}
	
	void dispatchKey(int keyCode)
	{
	}

    void onPaint(PaintEvent p) 
	{
		onPaint(p.graphics);
	}
	
	private void onPaint(Graphics g)
	{
        gfx = g;
		gfx.setFont(font);
		gfx.setCoordinateSystem(CoordinateSystem.TEXT);
		gfx.setBrush(foreground);
		gfx.setTextColor(foreground.getColor());
		peer.paint();
    }
}
