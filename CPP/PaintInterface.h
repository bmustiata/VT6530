
#include "stdafx.h"
#include <windows.h>
#include "Term.h"
#include "PaintSurface.h"
#include "PaintPeer.h"


class PaintInterface : public PaintSurface
{
	HWND control;
	Term *term;
	
	HDC gfx;
	HFONT font;
	int currentROP;

	COLORREF backcolor, forecolor;
	
	PaintPeer *peer;
	//KeyListener *keyListener;

	int charWidth;      /* current width of a char */
	int charHeight;      /* current height of a char */
	int charDescent;      /* base line descent */	
	StringBuffer fontName;
	int fontSize;

public:

	PaintInterface (HWND contr, Term *trm, COLORREF bk, COLORREF fg);
	
	virtual ~PaintInterface();

	int getPixelWidth();
	
	int getPixelHeight();
	
	void setFont(char *fontName, int attributes, int fontSize)
	{
		//this->fontSize = fontSize;
		recalcFont();
	}

	void recalcFont();
	
	int getFontWidth()
	{
		return charWidth;
	}
	
	int getFontHeight()
	{
		return charHeight;
	}
	
	int getFontDescent()
	{
		return charDescent;
	}
	
	COLORREF getBackGroundColor()
	{
		return backcolor;
	}
	
	COLORREF getForeGroundColor()
	{
		return forecolor;
	}
	
	void setBackGroundColor(COLORREF c)
	{
		backcolor = c;
	}
	
	void setForeGroundColor(COLORREF c)
	{
		forecolor = c;
	}
	
	HPEN setPen(HPEN pen)
	{
		_ASSERT(gfx != NULL);
		return (HPEN)SelectObject(gfx, pen);
	}

	HBRUSH setBrush(HBRUSH brush)
	{
		_ASSERT(gfx != NULL);
		return (HBRUSH) SelectObject(gfx, brush);
	}

	virtual void setBkColor(COLORREF backcolor)
	{
		SetBkColor(gfx, backcolor);
	}

	virtual void setTextColor(COLORREF c)
	{
		_ASSERT(gfx != NULL);
		SetTextColor(gfx, c);
	}

	void setPaintXorMode()
	{
		SetROP2(gfx, R2_WHITE);
	}
	
	void setPaintMode()
	{
		SetROP2(gfx, R2_COPYPEN);
	}
	
	void fillRect(int x1, int y1, int width, int height, HBRUSH brush)
	{
		RECT rect;
		rect.left = x1;
		rect.right = x1 + width;
		rect.top = y1;
		rect.bottom = y1 + height;
		FillRect(gfx, &rect, brush);
	}
	
	void drawBytes(char *text, int offset, int length, int x1, int y1)
	{
		ExtTextOut(gfx, x1, y1, 0, NULL, text, strlen(text), NULL);
	}
	
	void drawLine(int x1, int y1, int x2, int y2)
	{
		_ASSERT(gfx != NULL);
		MoveToEx(gfx, x1, y1, NULL);
		LineTo(gfx, x2, y2);
	}
		
	void setPaintPeer(PaintPeer *peer)
	{
		this->peer = peer;
	}

	void forceRepaint()
	{
		HDC hdc = GetDC(control);
		onPaint(hdc);
		ReleaseDC(control, hdc);
	}
	
	void dispatchKey(int keyCode)
	{
	}

    void onPaint(HDC hdc) 
	{
       	gfx = hdc;
		SetMapMode(gfx, MM_TEXT);
		HFONT oldfont = (HFONT)SelectObject(gfx, font);
		SetTextAlign(gfx, TA_BOTTOM);
		SetBkMode(gfx, TRANSPARENT); 
		SetBkColor(gfx, backcolor);
		//NT/2000 SetDCPenColor(gfx, forecolor);
		SetTextColor(gfx, forecolor);
		peer->paint();
		SelectObject(gfx, oldfont);
	}
};
