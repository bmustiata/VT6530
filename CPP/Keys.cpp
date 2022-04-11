
#include "stdafx.h"
#include "Keys.h"
#include "StringBuffer.h"


Keys::~Keys()
{
}

void Keys::setKeySet(int keySet)
{
	if (keySet == KEYS_ANSI)
	{
		plain = ansiChar;
		crtl = ansiCharCtl;
		alt = ansiCharAlt;
		shift = ansiCharShift;
		localCmd = ansiLocal;
		localCmdCtl = ansiLocalCtl;
		localCmdAlt = ansiLocalAlt;
		sendCursorWithFn = false;
		protectMode = false;
		enterKeyOn = true;
	}
	else if (keySet == KEYS_CONV)
	{
		plain = convChar;
		crtl = convCharCtl;
		alt = convCharAlt;
		shift = convCharShift;
		localCmd = convLocal;
		localCmdCtl = convLocalCtl;
		localCmdAlt = convLocalAlt;
		sendCursorWithFn = true;
		protectMode = false;
		enterKeyOn = true;
	}
	else if (keySet == KEYS_BLOCK)
	{
		plain = blockChar;
		crtl = blockCharCtl;
		alt = blockCharAlt;
		shift = blockCharShift;
		localCmd = blockLocal;
		localCmdCtl = blockLocalCtl;
		localCmdAlt = blockLocalAlt;
		sendCursorWithFn = true;
		protectMode = false;
		enterKeyOn = true;
	}
}

void Keys::keyReleased( int keycode, bool shift, bool ctrl, bool alt )
{
	bool fn = false;
	
	switch (keycode)
	{
		case VK_F1:
			pressedKey = SPC_F1;
			fn = true;
			break;
		case VK_F2:
			pressedKey = SPC_F2;
			fn = true;
			break;
		case VK_F3:
			pressedKey = SPC_F3;
			fn = true;
			break;
		case VK_F4:
			pressedKey = SPC_F4;
			fn = true;
			break;
		case VK_F5:
			pressedKey = SPC_F5;
			fn = true;
			break;
		case VK_F6:
			pressedKey = SPC_F6;
			fn = true;
			break;
		case VK_F7:
			pressedKey = SPC_F7;
			fn = true;
			break;
		case VK_F8:
			pressedKey = SPC_F8;
			fn = true;
			break;
		case VK_F9:
			pressedKey = SPC_F9;
			fn = true;
			break;
		case VK_F10:
			pressedKey = SPC_F10;
			fn = true;
			break;
		case VK_F11:
			pressedKey = SPC_F11;
			fn = true;
			break;
		case VK_F12:
			pressedKey = SPC_F12;
			fn = true;
			break;
		case VK_HOME:
			keycode = SPC_HOME;
			keyAction(false, keycode, shift, ctrl, alt);
			break;
		case VK_INSERT:
			keycode = SPC_INS;
			keyAction(false, keycode, shift, ctrl, alt);
			break;
		case VK_DELETE:
			keycode = SPC_DEL;
			keyAction(false, keycode, shift, ctrl, alt);
			break;
		case VK_DOWN:
			keycode = SPC_DOWN;
			keyAction(false, keycode, shift, ctrl, alt);
			break;
		case VK_END:
			keycode = SPC_END;
			keyAction(false, keycode, shift, ctrl, alt);
			break;
		case VK_LEFT:
			keycode = SPC_LEFT;
			keyAction(false, keycode, shift, ctrl, alt);
			break;
		case VK_NEXT:
			keycode = SPC_PGDN;
			keyAction(false, keycode, shift, ctrl, alt);
			break;
		case VK_PRIOR:
			keycode = SPC_PGUP;
			keyAction(false, keycode, shift, ctrl, alt);
			break;
		case VK_SNAPSHOT:
			keycode = SPC_PRINTSCR;
			keyAction(false, keycode, shift, ctrl, alt);
			break;
		case VK_RIGHT:
			keycode = SPC_RIGHT;
			keyAction(false, keycode, shift, ctrl, alt);
			break;
		case VK_UP:
			keycode = SPC_UP;
			keyAction(false, keycode, shift, ctrl, alt);
			break;
		case VK_SCROLL:
			keycode = SPC_SCROLLOCK;
			keyAction(false, keycode, shift, ctrl, alt);
			break;
	}
	if (fn)
	{
		keyAction(fn, keycode, shift, ctrl, alt);
	}
}

void Keys::keyAction(bool fn, int keycode, bool shift, bool ctrl, bool alt)
{
	StringBuffer sb;

	if (ctrl)
	{
		if (fn)
		{
			if (sendCursorWithFn)
			{
				if (protectMode)
				{
					return;						
				}
				else
				{
					return;
				}
			}
		}
		if (localCmdCtl[pressedKey] != 0)
		{
			notifyListener(localCmdCtl[pressedKey]);
			return;
		}
		notifyListener(crtl[pressedKey], strlen(crtl[pressedKey]));
	}
	else if (shift)
	{
		if (fn)
		{
			if (sendCursorWithFn)
			{
				if (protectMode)
				{
					sb.append((char)1);
					sb.append(shiftFn[pressedKey]);
					sb.append((char)(listener->getPage() + 0x20));
					listener->getStartFieldASCII(&sb);
					sb.append(((char)3));
					sb.append(((char)0));
					notifyListener(sb, 7);
					return;						
				}
				else
				{
					sb.append((char)1);
					sb.append(shiftFn[pressedKey]);
					sb.append((char)(listener->getCursorX() + 0x20));
					sb.append((char)(listener->getCursorY() + 0x20));
					sb.append(((char)13));
					notifyListener(sb, 5);
					return;
				}
			}
		}
		if (localCmd[pressedKey] != 0)
		{
			notifyListener(localCmd[pressedKey]);
			return;
		}
		notifyListener(this->shift[pressedKey], strlen(this->shift[pressedKey]));
	}
	else if (alt)
	{
		if (fn)
		{
			if (sendCursorWithFn)
			{
				if (protectMode)
				{
					return;						
				}
				else
				{
					return;
				}
			}
		}
		if (localCmdAlt[pressedKey] != 0)
		{
			notifyListener(localCmdAlt[pressedKey]);
			return;
		}
		notifyListener(this->alt[pressedKey], strlen(this->alt[pressedKey]));
	}
	else
	{
		if (fn)
		{
			if (sendCursorWithFn)
			{
				if (protectMode)
				{
				//	notifyListener((char)1 + plainFn[pressedKey] + (char)(listener.getPage() + 0x20) + listener.getStartFieldASCII() + ((char)3) + "" + ((char)0));
				//	return;						
				//}
				//else
				//{
				//	notifyListener((char)1 + plainFn[pressedKey] + (char)(listener.getCursorX() + 0x20) + ""   + (char)(listener.getCursorY() + 0x20) + "" + ((char)13));
				//	return;
					sb.append((char)1);
					sb.append(plainFn[pressedKey]);
					sb.append((char)(listener->getPage() + 0x20));
					listener->getStartFieldASCII(&sb);
					sb.append(((char)3));
					sb.append(((char)0));
					notifyListener(sb, 7);
					return;						
				}
				else
				{
					sb.append((char)1);
					sb.append(plainFn[pressedKey]);
					sb.append((char)(listener->getCursorX() + 0x20));
					sb.append((char)(listener->getCursorY() + 0x20));
					sb.append(((char)13));
					notifyListener(sb, 5);
					return;
				}
			}
		}
		if (pressedKey == 13 && enterKeyOn && protectMode)
		{
			sb.append((char)1);
			sb.append("V");
			sb.append((char)(listener->getPage() + 0x20));
			sb.append((char)(listener->getCursorX() + 0x20));
			sb.append((char)(listener->getCursorY() + 0x20));
			sb.append((char)3);
			sb.append((char)0);
			notifyListener(sb, 7);
		}
		if (localCmd[pressedKey] != 0)
		{
			notifyListener(localCmd[pressedKey]);
			return;
		}
		notifyListener(plain[pressedKey], strlen(plain[pressedKey]));
	}
}

char *Keys::ansiChar[] = {
					ESC "@", ESC "A", ESC "B", ESC "C", ESC "D",
					ESC "E", ESC "F", ESC "G", "\b", "\t", 
					"\r", ESC "H", ESC "I", "\n", ESC "J",
					ESC "[K", "0", ESC "[U", ESC "[H", ESC "[\24H", 
					ESC "[@", ESC "[P", "", ESC "[A", ESC "[B", 
					ESC "[D", ESC "[C", ESC, "\0", "\0", 
					"\30", "\31", " ", "!", "\"", 
					"#", "$", "%", "&", "\"", "(", ")", "*", "+", ",", 
					"-", ".", "/", "0", "1", "2", "3", "4", "5", "6", 
					"7", "8", "9", ":", ";", "<", "=", ">", "?", "@", 
					"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", 
					"K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", 
					"U", "V", "W", "X", "Y", "Z", "[", "\\", "]", "^", 
					"_", "`", "a", "b", "c", "d", "e", "f", "g", "h", 
					"i", "j", "k", "l", "m", "n", "o", "p", "q", "r", 
					"s", "t", "u", "v", "w", "x", "y", "z", "{", "|", 
					"}", "~"
					};

 char *Keys::ansiCharCtl[] = {
					"", "", "", "", "", "", "", "", "", 
					  "", "", "", "", "", "", "", "", "",
					"", "", "", "", "", "", "", "","", 
					"", "", "", "","", " ", "", "","", "", "", 
					"", "", "", "", "", "", "", "", "", "", "", 
					"", "\0", "", "", "", "", "", "", "", "", "", 
					"", "", "", "", "\0", "\1", "\2", "\3", "\4", "\5", "\6", 
					"\7", "", "\t", "\10", "\11", "\12", "\13", "\14", "\15", "\16", "\17", 
					"\18", "\19", "\20", "\21", "\22", "\23", "\24", "\25", "\26", "\27", "\28", 
					"\29", "\30", "", "", "\1", "\2", "\3", "\4", "\5", "\6", "\7", 
					"", "\t", "\10", "\11", "\12", "\13", "\14", "\15", "\16", "\17", "\18", 
					"\19", "\20", "\21", "\22", "\23", "\24", "\25", "\26", "", "", "", 
					"", "" 
					};
char *Keys::ansiCharAlt[] = {
					"", "", "", "", "", "", "", "", "", 
					  "", "", "", "", "", "", "", "", "",
					"", "", "", "", "", "", "", "","", 
					"", "", "", "","", " ", "", "","", "", "", 
					"", "", "", "", "", "", "", "", "", "", "", 
					"", "", "", "", "", "", "", "", "", "", "", 
					"", "", "", "", "", "", "", "", "", "", "", 
					"", "", "", "", "", "", "", "", "", "", "", 
					"", "", "", "", "", "", "", "", "", "", "", 
					"", "", "", "", "", "", "", "", "", "", "", 
					"", "", "", "", "", "", "", "", "", "", "", 
					"", "", "", "", "", "", "", "", "", "", "", 
					"", "" 
					};
char *Keys::ansiCharShift[] = {
					ESC "@", ESC "A", ESC "B", ESC "C", ESC "D",
					ESC "E", ESC "F", ESC "G", "\b", "\t", 
					"\r", ESC "H", ESC "I", "\n", ESC "J",
					ESC "[K", "0", ESC "[U", ESC "[H", ESC "[\24H", 
					ESC "[@", ESC "[P", "", ESC "[A", ESC "[B", 
					ESC "[D", ESC "[C", ESC, "\0", "\0", 
					"\30", "\31", " ", "!", "\"", 
					"#", "$", "%", "&", "\"", "(", ")", "*", "+", ",", 
					"-", ".", "/", "0", "1", "2", "3", "4", "5", "6", 
					"7", "8", "9", ":", ";", "<", "=", ">", "?", "@", 
					"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", 
					"K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", 
					"U", "V", "W", "X", "Y", "Z", "[", "\\", "]", "^", 
					"_", "`", "a", "b", "c", "d", "e", "f", "g", "h", 
					"i", "j", "k", "l", "m", "n", "o", "p", "q", "r", 
					"s", "t", "u", "v", "w", "x", "y", "z", "{", "|", 
					"}", "~"
					};
int Keys::ansiLocal[] = {
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0
					};
int Keys::ansiLocalCtl[] = {
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0
					};
int Keys::ansiLocalAlt[] = {
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0
					};

char *Keys::convChar[] = {
					"@", "A", "B", "C", "D",
					"E", "F", "G",  "\b", "\t",
					"\r", "H", "I", "\n", "J", 
					"K", "\16", "\17", "\0", "\0", 
					"\20", NAK, "\22", "\23", "\11", "\r", "\b", ESC, "\t", "\0", 
					"\30", "\31", " ", "!", "\"", "#", "$", "%", "&", "\"", 
					"(", ")", "*", "+", ",", "-", ".", "/", "0", "1", 
					"2", "3", "4", "5", "6", "7", "8", "9", ":", ";", "<", 
					"=", ">", "?", "@", "A", "B", "C", "D", "E", "F", "G", 
					"H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", 
					"S", "T", "U", "V", "W", "X", "Y", "Z", "[", "\\", "]", 
					"^", "_", "`", "a", "b", "c", "d", "e", "f", "g", "h", 
					"i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", 
					"t", "u", "v", "w", "x", "y", "z", "{", "|", "}", "~", 
					"\127" 
					};
char *Keys::convCharCtl[] = {
					"", "", "", "", "", "", "", "", "", 
					  "", "", "", "", "", "", "", "", "",
					"", "", "", "", "", "", "", "","", 
					"", "", "", "","", " ", "", "","", "", "", 
					"", "", "", "", "", "", "", "", "", "", "", 
					"", "\0", "", "", "", "", "", "", "", "", "", 
					"", "", "", "", "\0", "\1", "\2", "\3", "\4", "\5", "\6", 
					"\7", "", "\t", "\10", "\11", "\12", "\13", "\14", "\15", "\16", "\17", 
					"\18", "\19", "\20", "\21", "\22", "\23", "\24", "\25", "\26", "\27", "\28", 
					"\29", "\30", "", "", "\1", "\2", "\3", "\4", "\5", "\6", "\7", 
					"", "\t", "\10", "\11", "\12", "\13", "\14", "\15", "\16", "\17", "\18", 
					"\19", "\20", "\21", "\22", "\23", "\24", "\25", "\26", "", "", "", 
					"", "" 
					};
char *Keys::convCharAlt[] = {
					"", "", "", "", "", "", "", "", "", 
					  "", "", "", "", "", "", "", "", "",
					"", "", "", "", "", "", "", "","", 
					"", "", "", "","", " ", "", "","", "", "", 
					"", "", "", "", "", "", "", "", "", "", "", 
					"", "", "", "", "", "", "", "", "", "", "", 
					"", "", "", "", "", "", "", "", "", "", "", 
					"", "", "", "", "", "", "", "", "", "", "", 
					"", "", "", "", "", "", "", "", "", "", "", 
					"", "", "", "", "", "", "", "", "", "", "", 
					"", "", "", "", "", "", "", "", "", "", "", 
					"", "", "", "", "", "", "", "", "", "", "", 
					"", "" 
					};
char *Keys::convCharShift[] = {
					"'", "a", "b", "c", "e",
					"e", "f", "g",  "", "",
					"\r", "h", "i", "\n", "j", 
					"k", "", "", "", "", 
					"", "", "", "", "", "", "", "", "", "", 
					"", "", " ", "!", "\"", "#", "$", "%", "&", "\"", 
					"(", ")", "*", "+", ",", "-", ".", "/", "0", "1", 
					"2", "3", "4", "5", "6", "7", "8", "9", ":", ";", "<", 
					"=", ">", "?", "@", "A", "B", "C", "D", "E", "F", "G", 
					"H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", 
					"S", "T", "U", "V", "W", "X", "Y", "Z", "[", "\\", "]", 
					"^", "_", "`", "A", "B", "C", "D", "E", "F", "G", "H", 
					"I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", 
					"T", "U", "V", "W", "X", "Y", "Z", "{", "|", "}", "~", 
					"\127" 
					};
int Keys::convLocal[] = {
					0,0,0,0,0,0,0,7,8,9,
					10,0,0,13,0,0,16,17,18,19,
					20,21,22,23,24,25,26,27,28,29,
					30,31,32,33,34,35,36,37,38,39,
					40,41,42,43,44,45,46,47,48,49,
					50,51,52,53,54,55,56,57,58,59,
					60,61,62,63,64,65,66,67,68,69,
					70,71,72,73,74,75,76,77,78,79,
					80,81,82,83,84,85,86,87,88,89,
					90,91,92,93,94,95,96,97,98,99,
					100,101,102,103,104,105,106,107,108,109,
					110,111,112,113,114,115,116,117,118,119,
					120,121,122,123,124,125,126,127
					};
/*						0,0,0,0,0,0,0,0,0,9,
					0,0,0,0,0,0,16,17,18,19,
					0,0,0,23,24,0,26,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0
					};*/
int Keys::convLocalCtl[] = {
					0,0,0,0,0,0,0,0,8,0,
					0,0,0,13,0,0,16,17,18,19,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0
					};
int Keys::convLocalAlt[] = {
					0,0,0,0,0,0,0,0,8,0,
					0,0,0,13,0,0,0,0,0,19,
					0,0,0,23,24,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,49,
					50,51,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0
					};
char *Keys::blockChar[] = {
					"@", "A", "B", "C", "D",
					"E", "F", "G",  BACKSPACE, "\t",
					"\r", "H", "I", "\n", "J", 
					"K", "\16", "\17", "\0", "\0", 
					"\20", NAK, "\22", "\23", ""+(char)11, "\r", BACKSPACE, ESC, "\t", "\0", 
					"\30", "\31", " ", "!", "\"", "#", "$", "%", "&", "\"", 
					"(", ")", "*", "+", ",", "-", ".", "/", "0", "1", 
					"2", "3", "4", "5", "6", "7", "8", "9", ":", ";", "<", 
					"=", ">", "?", "@", "A", "B", "C", "D", "E", "F", "G", 
					"H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", 
					"S", "T", "U", "V", "W", "X", "Y", "Z", "[", "\\", "]", 
					"^", "_", "`", "a", "b", "c", "d", "e", "f", "g", "h", 
					"i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", 
					"t", "u", "v", "w", "x", "y", "z", "{", "|", "}", "~", 
					"\127" 
					};
char *Keys::blockCharCtl[] = {
					"", "", "", "", "", "", "", "", "", 
					  "", "", "", "", "", "", "", "", "",
					"", "", "", "", "", "", "", "","", 
					"", "", "", "","", " ", "", "","", "", "", 
					"", "", "", "", "", "", "", "", "", "", "", 
					"", "\0", "", "", "", "", "", "", "", "", "", 
					"", "", "", "", "\0", "\1", "\2", "\3", "\4", "\5", "\6", 
					"\7", "", "\t", "\10", "\11", "\12", "\13", "\14", "\15", "\16", "\17", 
					"\18", "\19", "\20", "\21", "\22", "\23", "\24", "\25", "\26", "\27", "\28", 
					"\29", "\30", "", "", "\1", "\2", "\3", "\4", "\5", "\6", "\7", 
					"", "\t", "\10", "\11", "\12", "\13", "\14", "\15", "\16", "\17", "\18", 
					"\19", "\20", "\21", "\22", "\23", "\24", "\25", "\26", "", "", "", 
					"", "" 
					};
char *Keys::blockCharAlt[] = {
					"", "", "", "", "", "", "", "", "", 
					  "", "", "", "", "", "", "", "", "",
					"", "", "", "", "", "", "", "","", 
					"", "", "", "","", " ", "", "","", "", "", 
					"", "", "", "", "", "", "", "", "", "", "", 
					"", "", "", "", "", "", "", "", "", "", "", 
					"", "", "", "", "", "", "", "", "", "", "", 
					"", "", "", "", "", "", "", "", "", "", "", 
					"", "", "", "", "", "", "", "", "", "", "", 
					"", "", "", "", "", "", "", "", "", "", "", 
					"", "", "", "", "", "", "", "", "", "", "", 
					"", "", "", "", "", "", "", "", "", "", "", 
					"", "" 
					};
char *Keys::blockCharShift[] = {
					"'", "a", "b", "c", "e",
					"e", "f", "g",  "", "",
					"\r", "h", "i", "\n", "j", 
					"k", "", "", "", "", 
					"", "", "", "", "", "", "", "", "", "", 
					"", "", " ", "!", "\"", "#", "$", "%", "&", "\"", 
					"(", ")", "*", "+", ",", "-", ".", "/", "0", "1", 
					"2", "3", "4", "5", "6", "7", "8", "9", ":", ";", "<", 
					"=", ">", "?", "@", "A", "B", "C", "D", "E", "F", "G", 
					"H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", 
					"S", "T", "U", "V", "W", "X", "Y", "Z", "[", "\\", "]", 
					"^", "_", "`", "A", "B", "C", "D", "E", "F", "G", "H", 
					"I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", 
					"T", "U", "V", "W", "X", "Y", "Z", "{", "|", "}", "~", 
					"\127" 
					};
int Keys::blockLocal[] = {
					0,0,0,0,0,0,0,7,8,9,
					10,0,0,13,0,0,16,17,18,19,
					20,21,22,23,24,25,26,27,28,29,
					30,31,32,33,34,35,36,37,38,39,
					40,41,42,43,44,45,46,47,48,49,
					50,51,52,53,54,55,56,57,58,59,
					60,61,62,63,64,65,66,67,68,69,
					70,71,72,73,74,75,76,77,78,79,
					80,81,82,83,84,85,86,87,88,89,
					90,91,92,93,94,95,96,97,98,99,
					100,101,102,103,104,105,106,107,108,109,
					110,111,112,113,114,115,116,117,118,119,
					120,121,122,123,124,125,126,127
					};
int Keys::blockLocalCtl[] = {
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0
					};
int Keys::blockLocalAlt[] = {
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0,
					0,0,0,0,0,0,0,0,0,0
					};

char *Keys::plainFn[] = {"@", "A", "B", "C", "D", "E", "F", "G", "", "", "", "H", "I", "", "J", "K"};
char *Keys::shiftFn[] = {"'", "a", "b", "c", "d", "e", "f", "g", "", "", "", "h", "i", "", "j", "k"};




