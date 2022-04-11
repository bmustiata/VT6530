#ifndef _local_page_h
#define _local_page_h

#include "Attributes.h"
#include "SharedProtocol.h"
#include "Page.h"

class LocalPage : public SharedProtocol
{
	void writeCursor(Page *page, char *text)
	{
		int len = strlen(text);
		for (int x = 0; x < len; x++)
		{
			writeChar(page, text[x]);
		}
	}

	void writeChar(Page *page, int c)
	{	
		SharedProtocol::writeChar(page, &page->cursorPos, c | DAT_MDT);
	}
};

#endif