package gov.revenue.vt6530.ui;

import java.awt.Font;
import java.awt.FontMetrics;
import java.awt.Container;
import java.awt.Panel;
import java.awt.Graphics;
import java.awt.Dimension;
import java.awt.event.ComponentListener;
import java.awt.event.ComponentEvent;
import java.awt.Color;
import java.awt.event.KeyListener;

/**
 *  AwtDisplay implement the paint interface for
 *  a canvas or panel.  Users of this class must
 *  add the AwtDisplay instance to their container.
 */
public class AwtDisplay	extends Panel implements PaintSurface
{
	private Font normalFont;      /* normal font */
	private FontMetrics fm;      /* current font metrics */
	private int charWidth;      /* current width of a char */
	private int charHeight;      /* current height of a char */
	private int charDescent;      /* base line descent */	
	private String fontName = "Courier";
	private int fontSize = 14;
	
	private int numRows, numCols;
	
	private Graphics gfx;
	private PaintPeer peer;
	
	
	public AwtDisplay(int numRows, int numCols)
	{
		this.numRows = numRows;
		this.numCols = numCols;
		
		normalFont = new Font(fontName, Font.BOLD, fontSize);
		setFont(normalFont);
		fm = getFontMetrics(normalFont);
		
		if(fm != null)
		{
			charWidth = fm.charWidth('@');
			charHeight = fm.getHeight();
			charDescent = fm.getDescent();
		}
	}
	
	public void setPaintPeer(PaintPeer peer)
	{
		this.peer = peer;
	}
	
	public void update(Graphics g)
	{
		paint(g);
	}
	
	public void paint(Graphics g)
	{
		gfx = g;
		peer.paint();
		gfx = null;
	}
	
	public void repaint()
	{
		super.repaint();
	}
	
	public Dimension getPreferredSize() 
	{
		return new Dimension(charWidth*numCols, charHeight*(numRows+1));
	}

	public Dimension getMinimumSize() 
	{
		return new Dimension(charWidth*numCols, charHeight*(numRows+1));
	}
	
	public void setSize(int width, int height)
	{
	}
	
	public void setSize(Dimension d)
	{
		setSize(d.width, d.height);
	}
	
	public void componentHidden(ComponentEvent e)    
	{
	}
	
	public void componentMoved(ComponentEvent e)    
	{
	}
	
	public void componentResized(ComponentEvent e) 
	{
	}
	
	public void componentShown(ComponentEvent e)
	{
	}

	public int getPixelWidth()
	{
		return getSize().width;
	}
	
	public int getPixelHeight()
	{
		return getSize().height;
	}
	
	public void setFont(String fontName, int attributes, int fontSize)
	{
		Font font = new Font(fontName, attributes, fontSize);
		normalFont = font;
		super.setFont(font);

		fm = getFontMetrics(font);
		
		if(fm != null)
		{
			charWidth = fm.charWidth('@');
			charHeight = fm.getHeight();
			charDescent = fm.getDescent();
		}
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
	
	public Color getBackGroundColor()
	{
		return getBackground();
	}
	
	public Color getForeGroundColor()
	{
		return getForeground();
	}
	
	public void setBackGroundColor(Color c)
	{
		setBackground(c);
	}
	
	public void setForeGroundColor(Color c)
	{
		if (gfx == null)
		{
			setForeground(c);
			return;
		}
		gfx.setColor(c);
	}
	
	public void setPaintXorMode(Color c)
	{
		gfx.setXORMode(c);
	}
	
	public void setPaintMode()
	{
		gfx.setPaintMode();
	}
	
	public void fillRect(int x1, int y1, int x2, int y2)
	{
		gfx.fillRect(x1, y1, x2, y2);
	}
	
	public void drawBytes(byte[] text, int offset, int length, int x1, int y1)
	{
		gfx.drawBytes(text, offset, length, x1, y1);
	}
	
	public void drawLine(int x1, int y1, int x2, int y2)
	{
		gfx.drawLine(x1, y1, x2, y2);
	}
	
	public void registerKeyListener(KeyListener listener)
	{
		addKeyListener(listener);
	}

	public void forceRepaint()
	{
		repaint();
	}
}
