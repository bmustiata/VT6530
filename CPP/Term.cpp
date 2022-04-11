
#include "stdafx.h"
#include "Term.h"

Term::Term()
{

	//if (ASSERT.debug > 0)
	//	Logger.setOutput("\\output.txt");
	host = "is";
	port = 1016;
	hEnqLock = CreateEvent(NULL, TRUE, FALSE, "Vt6530Enq");
	h34Lock = CreateEvent(NULL, TRUE, FALSE, "Vt6530_34");

	connected = false;
	nullDisplay = new NullDisplay();
	container = nullDisplay;
	container->setPaintPeer(this);
	container->setFont("Courier", 0, 14);
	display = new TextDisplay(2, 80, 24);
	
	keys = new Keys();
	//container->registerKeyListener(keys);
	keys->setListener(this);
	
	telnet = new Telnet(this);
	currentMode = new Guardian(display, keys, telnet);	
}

Term::~Term()
{
	_ASSERT(_CrtIsMemoryBlock(nullDisplay, sizeof(NullDisplay), 0, 0, 0));
	_ASSERT(_CrtIsMemoryBlock(keys, sizeof(Keys), 0, 0, 0));
	_ASSERT(_CrtIsMemoryBlock(currentMode, sizeof(Guardian), 0, 0, 0));
	_ASSERT(_CrtIsMemoryBlock(telnet, sizeof(Telnet), 0, 0, 0));

	if (WaitForSingleObject(termThread, 500) == WAIT_TIMEOUT)
	{
		ATLTRACE("Killing Thread\n");
		TerminateThread(termThread, 0);
	}
	delete currentMode;
	delete telnet;
	delete keys;
	delete nullDisplay;
	//CloseHandle(hEnqLock);
	//CloseHandle(h34Lock);
}

bool Term::waitENQ()
{
	bool ret;
	ATLTRACE("WaitENQ\r\n");
	ret = WaitForSingleObject(hEnqLock, 5000) == WAIT_OBJECT_0;
	ATLTRACE("WaitENQ Complete\r\n");
	return ret;
}

bool Term::wait34()
{
	bool ret;
	ATLTRACE("Wait34\r\n");
	ret = WaitForSingleObject(h34Lock, 5000) == WAIT_OBJECT_0;
	ATLTRACE("Wait34 Complete\r\n");
	return ret;
}

void Term::run()
{
	_ASSERT(_CrtIsValidHeapPointer(display));
	_ASSERT(_CrtIsMemoryBlock(telnet, sizeof(Telnet), 0, 0, 0));

	CoInitializeEx(NULL, COINIT_MULTITHREADED);

	display->clearPage();
	display->echoDisplay("Connecting to ");
	display->echoDisplay(host);
	display->echoDisplay("...");
	dispatchDisplayChanged();

	telnet->connect(host, port);
	connected = true;
	display->carageReturn();
	display->linefeed();
		
	display->echoDisplay("Connected.");
	dispatchConnect();
		
	dispatchDisplayChanged();
					
	while (connected)
	{
		_ASSERT(_CrtIsValidHeapPointer(display));
		_ASSERT(_CrtIsMemoryBlock(telnet, sizeof(Telnet), 0, 0, 0));
		_ASSERT(_CrtIsMemoryBlock(currentMode, sizeof(Guardian), 0, 0, 0));
		
		receiveBuf.setLength(0);
		try
		{
			telnet->receive(&receiveBuf);
		}
		catch (IOException exc)
		{
			if (connected)
			{
				SetEvent(hEnqLock);
				SetEvent(hEnqLock);
				_ASSERT(_CrtIsMemoryBlock(this, sizeof(Term), NULL, NULL, NULL));
				this->dispatchError(exc.getMessage());
				dispatchDisconnect();
			}
			connected = false;
			continue;
		}
		if (receiveBuf.length() > 0)
		{
			currentMode->processRemoteString(receiveBuf, receiveBuf.length());
			if (display->needsRepaint())
			{
				dispatchDisplayChanged();
			}
		}
	}
	CoUninitialize();
}

void Term::mappedKey(char *s, int len)
{
	_ASSERT(_CrtIsMemoryBlock(telnet, sizeof(Telnet), 0, 0, 0));

	if (s[0] == SPC_PRINTSCR)
	{
		//display.dumpScreen(Logger.out);
		//Logger.out.println(display.dumpAttibutes());
		//Logger.out.println(display.toHTML(container.getForeGroundColor(), container.getBackGroundColor()));
	}
	if (s[0] != (char)1)
	{
		display->echoDisplay(s);
	}
	try
	{
		ATLTRACE("mappedKey -- ResetEvent\r\n");
		ResetEvent(hEnqLock);
		ResetEvent(h34Lock);
		telnet->flush();
		telnet->_send(s, len);
	}
	catch (IOException ioe)
	{
		connected = false;
	}
	dispatchDisplayChanged();
}

void Term::command(char c)
{
	_ASSERT(_CrtIsMemoryBlock(telnet, sizeof(Telnet), 0, 0, 0));
	_ASSERT(_CrtIsValidHeapPointer(display));
	_ASSERT(_CrtIsMemoryBlock(currentMode, sizeof(Guardian), 0, 0, 0));

	if (c == SPC_BREAK)
	{
		try
		{
			telnet->send(0);
			telnet->send(0);
			telnet->send(0);
		}
		catch (IOException ioe)
		{
		}
		return;
	}
	else if (c == SPC_PRINTSCR || c == 27)
	{
		StringBuffer sb;
		display->dumpScreen(&sb);
		sb.append("\r\n\r\n");
		display->dumpAttibutes(&sb);
		FILE *out = fopen("d:\\output.txt", "w");
		fprintf(out, "%s", (char*)sb);
		fclose(out);
		//display.dumpScreen(Logger.out);
		//Logger.out.println(display.dumpAttibutes());
		//Logger.out.println(display.toHTML(container.getForeGroundColor(), container.getBackGroundColor()));
		return;
	}
 	currentMode->execLocalCommand(c);
	dispatchDisplayChanged();
}

/**
 *  Send a string to the terminal as if it
 *  was typed at the keyboard.
 */
void Term::fakeKeys(char *keystrokes)
{
	_ASSERT(_CrtIsMemoryBlock(keys, sizeof(Keys), 0, 0, 0));

	int key;
	int len = strlen(keystrokes);
	for (int x = 0; x < len; x++)
	{
		key = keystrokes[x];
		//keys->keyAction(false, key, false, false, false);
		keypress(key, false, false, false);
	}
	dispatchDisplayChanged();
}


COLORREF Term::getColor(char *color)
{
	if (strcmp("GREEN", color) == 0)
		return RGB(0, 240, 0);
		
	else if (strcmp("BLACK", color) == 0)
		return RGB(0,0,0);
		
	else if (strcmp("BLUE", color) == 0)
		return RGB(0, 0, 240);
		
	else if (strcmp("LTGRAY", color) == 0)
		return RGB(200, 200, 200);
			
	else if (strcmp("GRAY", color) == 0)
		return RGB(140, 140, 140);
		
	else if (strcmp("WHITE", color) == 0)
		return RGB(255, 255, 255);
			
	else if (strcmp("YELLOW", color) == 0)
		return RGB(0, 255, 255);
			
	else if (strcmp("RED", color) == 0)
		return RGB(255, 0, 0);
			
	else if (strcmp("PINK", color) == 0)
		return RGB(230, 100, 100);
			
	else if (strcmp("MAGENTA", color) == 0)
		return RGB(230, 0, 230);
			
	else if (strcmp("ORANGE", color) == 0)
		return RGB(255, 200, 0);
			
	else
		return RGB(80, 80, 80);
}


DWORD WINAPI TermProc(LPVOID lpParameter)
{
	((Term *)lpParameter)->run();
	return 0;
}

