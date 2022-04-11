#ifndef _paint_surface_h
#define _paint_surface_h

#include <windows.h>
#include "PaintPeer.h"


class PaintSurface
{
public:
	virtual int getPixelWidth() = 0;
	virtual int getPixelHeight() = 0;
	
	virtual void setFont(char *fontName, int attributes, int fontSize) = 0;
	virtual int getFontWidth() = 0;
	virtual int getFontHeight() = 0;
	virtual int getFontDescent() = 0;
	
	virtual COLORREF getBackGroundColor() = 0;
	virtual COLORREF getForeGroundColor() = 0;
	virtual void setBackGroundColor(COLORREF c) = 0;
	virtual void setForeGroundColor(COLORREF c) = 0;
	virtual HPEN setPen(HPEN pen) = 0;
	virtual void setTextColor(COLORREF c) = 0;
	virtual void setBkColor(COLORREF backcolor) = 0;

	virtual void setPaintXorMode() = 0;
	virtual void setPaintMode() = 0;
	
	virtual void fillRect(int x1, int y1, int x2, int y2, HBRUSH brush) = 0;
	virtual void drawBytes(char *text, int offset, int length, int x1, int y1) = 0;
	virtual void drawLine(int x1, int y1, int x2, int y2) = 0;
		
	virtual void setPaintPeer(PaintPeer *peer) = 0;
	virtual void forceRepaint() = 0;
};

#endif
