#include "stdafx.h"
#include "PaintInterface.h"

PaintInterface::PaintInterface (HWND contr, Term *trm, COLORREF bk, COLORREF fg)
{
	backcolor = bk;
	forecolor = fg;
	fontName = "Courier";
	fontSize = 40;
	control = contr;
	term = trm;
	setFont(fontName, 0, fontSize);
}

PaintInterface::~PaintInterface()
{
	DeleteObject(font);
}

int PaintInterface::getPixelWidth()
{
	RECT rect;
	if (GetClientRect(control, &rect) == 0)
	{
		throw IOException ("Can't get client rect");
	}
	return rect.right; 
}

int PaintInterface::getPixelHeight()
{
	RECT rect;
	int e;
	if ( (e = GetClientRect(control, &rect)) == 0)
	{
		throw IOException ("Can't get client rect");
	}
	return rect.bottom;
}

void PaintInterface::recalcFont()
{
	fontSize = 40;
	HFONT oldfont;
	
	int width = getPixelWidth();
	int height = getPixelHeight();

	HDC g = GetDC(control);
	SetMapMode(gfx, MM_TEXT);

	font = CreateFont(fontSize*2, fontSize, 0, 0, FW_NORMAL, FALSE, FALSE, FALSE, ANSI_CHARSET, 0, CLIP_DEFAULT_PRECIS, DEFAULT_QUALITY, FIXED_PITCH, "Modern");
	oldfont = (HFONT)SelectObject(g, font);
	TEXTMETRIC metrics;
	GetTextMetrics(g, &metrics);
	charWidth = metrics.tmAveCharWidth;
	charHeight = metrics.tmHeight;
	charDescent = metrics.tmDescent;
	
	while ((charWidth * 80 > width || (charHeight/*+charDescent*/) * 25 > height) && fontSize > 1)
	{
		DeleteObject(SelectObject(g, oldfont));
		fontSize--;
		font = CreateFont(fontSize*2, fontSize, 0, 0, FW_NORMAL, FALSE, FALSE, FALSE, ANSI_CHARSET, 0, CLIP_DEFAULT_PRECIS, DEFAULT_QUALITY, FIXED_PITCH, "Modern");
		SelectObject(g, font);
		GetTextMetrics(g, &metrics);
		charWidth = metrics.tmAveCharWidth;
		charHeight = metrics.tmHeight;
		charDescent = metrics.tmDescent;
	}

	SelectObject(g, oldfont);
	ReleaseDC(control, g);
}

