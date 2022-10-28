package gov.revenue.vt6530.ui;

import java.awt.Container;
import java.awt.event.KeyListener;
import java.awt.event.KeyEvent;
import java.util.Hashtable;

import gov.revenue.vt6530.FastVector;

/**
 *  Keys performs mapping of keystokes to terminal
 *  (Tandem) types (f.e. F13-F16).  Now that we have
 *  a better understanding of Guardian mode key 
 *  handling, this class can be much improved (Unless
 *  we want to keep the ANSI related structures).
 */
public class Keys implements KeyListener
{
	public static final int KEYS_ANSI = 0;
	public static final int KEYS_CONV = 1;
	public static final int KEYS_BLOCK = 2;
	
	public static final String NULL = "" + (char)0;
	public static final String SOH = "" + (char)1;
	public static final String ENQUIRY = "" + (char)5;
	public static final String ACK = "" + (char)6;
	public static final String BELL = "" + (char)7;
	public static final String BACKSPACE = "\b";
	public static final String NAK = "" + (char)21;
	public static final String ESC = "" + (char)27;
	public static final String CR = "" + (char)13;
	
	private static final int SPC_F1 = 0;
	private static final int SPC_F2 = 1;
	private static final int SPC_F3 = 2;
	private static final int SPC_F4 = 3;
	private static final int SPC_F5 = 4;
	private static final int SPC_F6 = 5;
	private static final int SPC_F7 = 6;
	private static final int SPC_F8 = 7;
	private static final int SPC_F9 = 11;
	private static final int SPC_F10 = 12;
	private static final int SPC_F11 = 14;
	private static final int SPC_F12 = 15;
	public static final int SPC_BREAK = 16;
	static final int SPC_PGUP = 17;
	static final int SPC_PGDN = 18;
	static final int SPC_HOME = 19;
	static final int SPC_END = 20;
	static final int SPC_INS = 21;
	static final int SPC_DEL = 22;
	static final int SPC_SCROLLOCK = 23;
	static final int SPC_UP = 24;
	static final int SPC_DOWN = 25;
	static final int SPC_LEFT = 26;
	static final int SPC_RIGHT = 28;
	public static final int SPC_PRINTSCR = 29;
	private static final int LAST_SPC = 30;
	static final int SPC_COPY = 31;
	static final int SPC_PASTE = 96;
	static final int SPC_CUT = 127;

	private boolean sendCursorWithFn = false;
	
	private char pressedKey = ' ';
	private long pressedWhen = 0;

	private String[] plain;
	private String[] crtl;
	private String[] alt;
	private String[] shift;
	
	private byte[] localCmd;
	private byte[] localCmdCtl;
	private byte[] localCmdAlt;
	
	private boolean ignoreKeys = false;
	private boolean protectMode = false;
	private boolean enterKeyOn = true;

	MappedKeyListener listener;
	
	
	public Keys()
	{
		setKeySet(KEYS_CONV);
	}

	public void setProtectMode()
	{
		lockKeyboard();
		protectMode = true;
		// disable local line editing
	}
	
	public void exitProtectMode()
	{
		unlockKeyboard();
		protectMode = false;
		// enable local line editing
	}
	
	public void enterKeyOn()
	{
		System.out.println("Enter key on");
		enterKeyOn = true;
	}
	
	public void enterKeyOff()
	{
		System.out.println("Enter key off");
		enterKeyOn = false;
	}
	
	public void setListener(MappedKeyListener listener)
	{
		this.listener = listener;
	}
	
	public void setKeySet(int keySet)
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
	
	public void setMap(int ch, int modifier, String out)
	{
		switch (modifier)
		{
			case 0:
				switch (ch)
				{
					case KeyEvent.VK_F1:
						plain[SPC_F1] = out;
						break;
					case KeyEvent.VK_F2:
						plain[SPC_F2] = out;
						break;
					case KeyEvent.VK_F3:
						plain[SPC_F3] = out;
						break;
					case KeyEvent.VK_F4:
						plain[SPC_F4] = out;
						break;
					case KeyEvent.VK_F5:
						plain[SPC_F5] = out;
						break;
					case KeyEvent.VK_F6:
						plain[SPC_F6] = out;
						break;
					case KeyEvent.VK_F7:
						plain[SPC_F7] = out;
						break;
					case KeyEvent.VK_F8:
						plain[SPC_F8] = out;
						break;
					case KeyEvent.VK_F9:
						plain[SPC_F9] = out;
						break;
					case KeyEvent.VK_F10:
						plain[SPC_F10] = out;
						break;
					case KeyEvent.VK_F11:
						plain[SPC_F11] = out;
						break;
					case KeyEvent.VK_F12:
						plain[SPC_F12] = out;
						break;
					default:
						plain[ch] = out;
				}
				break;
			case KeyEvent.VK_ALT:
				alt[ch] = out;
				break;
			case KeyEvent.VK_CONTROL:
				crtl[ch] = out;
				break;
		}
	}
	
	public void keyPressed( KeyEvent e ) 
	{
	}
	
	public void keyReleased( KeyEvent e )
	{
		boolean fn = false;
		int keyCode = e.getKeyCode();
		int modifiers = e.getModifiers();
		pressedKey = e.getKeyChar();
		pressedWhen = e.getWhen();
		
		switch (keyCode)
		{
			case KeyEvent.VK_F1:
				pressedKey = SPC_F1;
				fn = true;
				break;
			case KeyEvent.VK_F2:
				pressedKey = SPC_F2;
				fn = true;
				break;
			case KeyEvent.VK_F3:
				pressedKey = SPC_F3;
				fn = true;
				break;
			case KeyEvent.VK_F4:
				pressedKey = SPC_F4;
				fn = true;
				break;
			case KeyEvent.VK_F5:
				pressedKey = SPC_F5;
				fn = true;
				break;
			case KeyEvent.VK_F6:
				pressedKey = SPC_F6;
				fn = true;
				break;
			case KeyEvent.VK_F7:
				pressedKey = SPC_F7;
				fn = true;
				break;
			case KeyEvent.VK_F8:
				pressedKey = SPC_F8;
				fn = true;
				break;
			case KeyEvent.VK_F9:
				pressedKey = SPC_F9;
				fn = true;
				break;
			case KeyEvent.VK_F10:
				pressedKey = SPC_F10;
				fn = true;
				break;
			case KeyEvent.VK_F11:
				pressedKey = SPC_F11;
				fn = true;
				break;
			case KeyEvent.VK_F12:
				pressedKey = SPC_F12;
				fn = true;
				break;
			case KeyEvent.VK_HOME:
				pressedKey = SPC_HOME;
				keyAction(false, pressedKey, keyCode, modifiers);
				break;
			case KeyEvent.VK_INSERT:
				pressedKey = SPC_INS;
				keyAction(false, pressedKey, keyCode, modifiers);
				break;
			case KeyEvent.VK_DELETE:
				pressedKey = SPC_DEL;
				keyAction(false, pressedKey, keyCode, modifiers);
				break;
			case KeyEvent.VK_DOWN:
				pressedKey = SPC_DOWN;
				keyAction(false, pressedKey, keyCode, modifiers);
				break;
			case KeyEvent.VK_END:
				pressedKey = SPC_END;
				keyAction(false, pressedKey, keyCode, modifiers);
				break;
			case KeyEvent.VK_LEFT:
				pressedKey = SPC_LEFT;
				keyAction(false, pressedKey, keyCode, modifiers);
				break;
			case KeyEvent.VK_PAGE_DOWN:
				pressedKey = SPC_PGDN;
				keyAction(false, pressedKey, keyCode, modifiers);
				break;
			case KeyEvent.VK_PAGE_UP:
				pressedKey = SPC_PGUP;
				keyAction(false, pressedKey, keyCode, modifiers);
				break;
			case KeyEvent.VK_PRINTSCREEN:
				pressedKey = SPC_PRINTSCR;
				keyAction(false, pressedKey, keyCode, modifiers);
				break;
			case KeyEvent.VK_RIGHT:
				pressedKey = SPC_RIGHT;
				keyAction(false, pressedKey, keyCode, modifiers);
				break;
			case KeyEvent.VK_UP:
				pressedKey = SPC_UP;
				keyAction(false, pressedKey, keyCode, modifiers);
				break;
			case KeyEvent.VK_SCROLL_LOCK:
				pressedKey = SPC_SCROLLOCK;
				keyAction(false, pressedKey, keyCode, modifiers);
				break;
		}
		if (fn)
		{
			keyAction(fn, pressedKey, keyCode, modifiers);
		}
	}
	
	public void keyTyped( KeyEvent e )
	{
		int keyCode = e.getKeyCode();
		int modifiers = e.getModifiers();
		pressedKey = e.getKeyChar();
		pressedWhen = e.getWhen();
		
		switch (pressedKey)
		{
			// handle CRTL V, C, X
			case 22:
				pressedKey = 'v';
				break;
			case 24:
				pressedKey = 'x';
				break;
			case 3:
				pressedKey = 'c';
				break;
		}
		keyAction(false, pressedKey, keyCode, modifiers);
	}
	
	public void keyAction(boolean fn, int pressedKey, int keyCode, int modifiers)
	{
		boolean mctrl = (KeyEvent.CTRL_MASK & modifiers) != 0;
		boolean mshift = (KeyEvent.SHIFT_MASK & modifiers) != 0;
		boolean malt = (KeyEvent.ALT_MASK & modifiers) != 0;
		
		if (fn && mshift && malt)
		{
			if (sendCursorWithFn)
			{
				String fnChar;
				switch (pressedKey)
				{
					case SPC_F1:
						fnChar = "j";
						break;
					case SPC_F2:
						fnChar = "k";
						break;
					case SPC_F3:
						fnChar = "l";
						break;
					case SPC_F4:
						fnChar = "m";
						break;
					case SPC_F5:
						fnChar = "n";
						break;
					case SPC_F6:
						fnChar = "o";
						break;
					default:
						return;
				}
				if (protectMode)
				{
					notifyListener((char)1 + fnChar + (char)(listener.getPage() + 0x20) + listener.getStartFieldASCII() + ((char)3) + "" + ((char)0));
					return;						
				}
				else
				{
					notifyListener((char)1 + plainFn[pressedKey] + (char)(listener.getCursorX() + 0x20) + ""   + (char)(listener.getCursorY() + 0x20) + "" + ((char)13));
					return;
				}
			}
			return;
		}
		if (fn && malt)
		{
			if (sendCursorWithFn)
			{
				String fnChar;
				switch (pressedKey)
				{
					case SPC_F1:
						fnChar = "J";
						break;
					case SPC_F2:
						fnChar = "K";
						break;
					case SPC_F3:
						fnChar = "L";
						break;
					case SPC_F4:
						fnChar = "M";
						break;
					case SPC_F5:
						fnChar = "N";
						break;
					case SPC_F6:
						fnChar = "O";
						break;
					default:
						return;
				}
				if (protectMode)
				{
					notifyListener((char)1 + fnChar + (char)(listener.getPage() + 0x20) + listener.getStartFieldASCII() + ((char)3) + "" + ((char)0));
					return;						
				}
				else
				{
					notifyListener((char)1 + plainFn[pressedKey] + (char)(listener.getCursorX() + 0x20) + ""   + (char)(listener.getCursorY() + 0x20) + "" + ((char)13));
					return;
				}
			}
			return;
		}
		if (mctrl)
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
			notifyListener(crtl[pressedKey]);
		}
		else if (mshift)
		{
			if (fn)
			{
				if (sendCursorWithFn)
				{
					if (protectMode)
					{
						notifyListener((char)1 + shiftFn[pressedKey] + (char)(listener.getPage() + 0x20) + listener.getStartFieldASCII() + ((char)3) + "" + ((char)0));
						return;						
					}
					else
					{
						notifyListener((char)1 + shiftFn[pressedKey]  + (char)(listener.getCursorX() + 0x20) + "" + (char)(listener.getCursorY() + 0x20) + "" + ((char)13));
						return;
					}
				}
			}
			if (localCmd[pressedKey] != 0)
			{
				notifyListener(localCmd[pressedKey]);
				return;
			}
			notifyListener(shift[pressedKey]);
		}
		else if (malt)
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
			notifyListener(alt[pressedKey]);
		}
		else
		{
			if (fn)
			{
				if (sendCursorWithFn)
				{
					if (protectMode)
					{
						notifyListener((char)1 + plainFn[pressedKey] + (char)(listener.getPage() + 0x20) + listener.getStartFieldASCII() + ((char)3) + "" + ((char)0));
						return;						
					}
					else
					{
						notifyListener((char)1 + plainFn[pressedKey] + (char)(listener.getCursorX() + 0x20) + ""   + (char)(listener.getCursorY() + 0x20) + "" + ((char)13));
						return;
					}
				}
			}
			if (pressedKey == 10 && enterKeyOn && protectMode)
			{
				notifyListener((char)1 + "V" + (char)(listener.getPage() + 0x20) + (char)(listener.getCursorX() + 0x20) + "" + (char)(listener.getCursorY() + 0x20) + ((char)3) + "" + ((char)0));
			}
			if (localCmd[pressedKey] != 0)
			{
				notifyListener(localCmd[pressedKey]);
				return;
			}
			notifyListener(plain[pressedKey]);
		}
	}
	
	private void notifyListener(byte b)
	{
		if (! ignoreKeys)
		{
			listener.command(b);
		}
	}
	
	private void notifyListener(String str)
	{
		if (! ignoreKeys)
		{
			listener.mappedKey(str);
		}
	}
	
	public void lockKeyboard()
	{
		ignoreKeys = true;
	}
	
	public void unlockKeyboard()
	{
		ignoreKeys = false;
	}
	
	public void setCrLfOn()
	{
	}
	
	public void setCrLfOff()
	{
	}
	
	private String[] ansiChar = {
						ESC+"@", ESC+"A", ESC+"B", ESC+"C", ESC+"D",
						ESC+"E", ESC+"F", ESC+"G", BACKSPACE, "\t", 
						"\r", ESC+"H", ESC+"I", "\n", ESC+"J",
						ESC+"[K", "0", ESC+"[U", ESC+"[H", ESC+"[\24H", 
						ESC+"[@", ESC+"[P", "", ESC+"[A", ESC+"[B", 
						ESC+"[D", ESC+"[C", ESC, "\0", "\0", 
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
	private String[] ansiCharCtl = {
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
	private String[] ansiCharAlt = {
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
	private String[] ansiCharShift = {
						ESC+"@", ESC+"A", ESC+"B", ESC+"C", ESC+"D",
						ESC+"E", ESC+"F", ESC+"G", BACKSPACE, "\t", 
						"\r", ESC+"H", ESC+"I", "\n", ESC+"J",
						ESC+"[K", "0", ESC+"[U", ESC+"[H", ESC+"[\24H", 
						ESC+"[@", ESC+"[P", "", ESC+"[A", ESC+"[B", 
						ESC+"[D", ESC+"[C", ESC, "\0", "\0", 
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
	private byte[] ansiLocal = {
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
	private byte[] ansiLocalCtl = {
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
	private byte[] ansiLocalAlt = {
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
	
	private String[] convChar = {
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
	private String[] convCharCtl = {
						"", "", "", "", "", "", "", "", "", 
						  "", "", "", "", "", "", "", "", "",
						"", "", "", "", "", "", "", "","", 
						"", "", "", "","", " ", "", "","", "", "", 
						"", "", "", "", "", "", "", "", "", "", "", 
						"", "\0", "", "", "", "", "", "", "", "", "", 
						"", "", "", "", "\0", "\1", "\2", "" + (char)SPC_COPY, "\4", "\5", "\6", 
						"\7", "", "\t", "\10", "\11", "\12", "\13", "\14", "\15", "\16", "\17", 
						"\18", "\19", "\20", "\21", "" + (char)SPC_PASTE, "\23", "" + (char)SPC_CUT, "\25", "\26", "\27", "\28", 
						"\29", "\30", "", "", "\1", "\2", "\3", "\4", "\5", "\6", "\7", 
						"", "\t", "\10", "\11", "\12", "\13", "\14", "\15", "\16", "\17", "\18", 
						"\19", "\20", "\21", "\22", "\23", "\24", "\25", "\26", "", "", "", 
						"", "" 
						};
	private String[] convCharAlt = {
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
	private String[] convCharShift = {
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
	private byte[] convLocal = {
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
	private byte[] convLocalCtl = {
						0,0,0,0,0,0,0,0,8,0,
						0,0,0,13,0,0,16,17,18,19,
						0,0,0,0,0,0,0,0,0,0,
						0,0,0,0,0,0,0,0,0,0,
						0,0,0,0,0,0,0,0,0,0,
						0,0,0,0,0,0,0,0,0,0,
						0,0,0,0,0,0,0,SPC_COPY,0,0,
						0,0,0,0,0,0,0,0,0,0,
						0,0,0,0,0,0,SPC_PASTE,0,SPC_CUT,0,
						0,0,0,0,0,0,0,0,0,SPC_COPY,
						0,0,0,0,0,0,0,0,0,0,
						0,0,0,0,0,0,0,0,SPC_PASTE,0,
						SPC_CUT,0,0,0,0,0,0,0,0,0
						};
	private byte[] convLocalAlt = {
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
	private String[] blockChar = {
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
	private String[] blockCharCtl = {
						"", "", "", "", "", "", "", "", "", 
						  "", "", "", "", "", "", "", "", "",
						"", "", "", "", "", "", "", "","", 
						"", "", "", "","", " ", "", "","", "", "", 
						"", "", "", "", "", "", "", "", "", "", "", 
						"", "\0", "", "", "", "", "", "", "", "", "", 
						"", "", "", "", "\0", "\1", "\2", "" + (char)SPC_COPY, "\4", "\5", "\6", 
						"\7", "", "\t", "\10", "\11", "\12", "\13", "\14", "\15", "\16", "\17", 
						"\18", "\19", "\20", "\21", "" + (char)SPC_PASTE, "\23", "" + (char)SPC_CUT, "\25", "\26", "\27", "\28", 
						"\29", "\30", "", "", "\1", "\2", "\3", "\4", "\5", "\6", "\7", 
						"", "\t", "\10", "\11", "\12", "\13", "\14", "\15", "\16", "\17", "\18", 
						"\19", "\20", "\21", "\22", "\23", "\24", "\25", "\26", "", "", "", 
						"", "" 
						};
	private String[] blockCharAlt = {
						"J", "K", "L", "M", "N", "O", "", "", "", 
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
	private String[] blockCharShift = {
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
	private byte[] blockLocal = {
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
	private byte[] blockLocalCtl = {
						0,0,0,0,0,0,0,0,0,0,
						0,0,0,0,0,0,0,0,0,0,
						0,0,0,0,0,0,0,0,0,0,
						0,0,0,0,0,0,0,0,0,0,
						0,0,0,0,0,0,0,0,0,0,
						0,0,0,0,0,0,0,0,0,0,
						0,0,0,0,0,0,0,SPC_COPY,0,0,
						0,0,0,0,0,0,0,0,0,0,
						0,0,0,0,0,0,SPC_PASTE,0,SPC_CUT,0,
						0,0,0,0,0,0,0,0,0,SPC_COPY,
						0,0,0,0,0,0,0,0,0,0,
						0,0,0,0,0,0,0,0,SPC_PASTE,0,
						SPC_CUT,0,0,0,0,0,0,0,0,0
						};
	private byte[] blockLocalAlt = {
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

	private String[] plainFn = {"@", "A", "B", "C", "D", "E", "F", "G", "", "", "", "H", "I", "", "J", "K"};
	private String[] shiftFn = {"'", "a", "b", "c", "d", "e", "f", "g", "", "", "", "h", "i", "", "j", "k"};
	
	// array to store DEC Special -> Unicode mapping
	//  Unicode   DEC  Unicode name    (DEC name)
	private static char DECSPECIAL[] = 
	{
		'\u0040', //5f blank
		'\u2666', //60 black diamond
		'\u2592', //61 grey square
		'\u2409', //62 Horizontal tab  (ht) pict. for control
		'\u240c', //63 Form Feed       (ff) pict. for control
		'\u240d', //64 Carriage Return (cr) pict. for control
		'\u240a', //65 Line Feed       (lf) pict. for control
		'\u00ba', //66 Masculine ordinal indicator
		'\u00b1', //67 Plus or minus sign
		'\u2424', //68 New Line        (nl) pict. for control
		'\u240b', //69 Vertical Tab    (vt) pict. for control
		'\u2518', //6a Forms light up   and left
		'\u2510', //6b Forms light down and left
		'\u250c', //6c Forms light down and right
		'\u2514', //6d Forms light up   and right
		'\u253c', //6e Forms light vertical and horizontal
		'\u2594', //6f Upper 1/8 block                        (Scan 1)
		'\u2580', //70 Upper 1/2 block                        (Scan 3)
		'\u2500', //71 Forms light horizontal or ?em dash?    (Scan 5)
		'\u25ac', //72 \u25ac black rect. or \u2582 lower 1/4 (Scan 7)
		'\u005f', //73 \u005f underscore  or \u2581 lower 1/8 (Scan 9)
		'\u251c', //74 Forms light vertical and right
		'\u2524', //75 Forms light vertical and left
		'\u2534', //76 Forms light up   and horizontal
		'\u252c', //77 Forms light down and horizontal
		'\u2502', //78 vertical bar
		'\u2264', //79 less than or equal
		'\u2265', //7a greater than or equal
		'\u00b6', //7b paragraph
		'\u2260', //7c not equal
		'\u00a3', //7d Pound Sign (british)
		'\u00b7'  //7e Middle Dot
	};

	public final static char unimap[] = 
	{
		//#
		//#    Name:     cp437_DOSLatinUS to Unicode table
		//#    Unicode version: 1.1
		//#    Table version: 1.1
		//#    Table format:  Format A
		//#    Date:          03/31/95
		//#    Authors:       Michel Suignard <michelsu@microsoft.com>
		//#                   Lori Hoerth <lorih@microsoft.com>
		//#    General notes: none
		//#
		//#    Format: Three tab-separated columns
		//#        Column #1 is the cp1255_WinHebrew code (in hex)
		//#        Column #2 is the Unicode (in hex as 0xXXXX)
		//#        Column #3 is the Unicode name (follows a comment sign, '#')
		//#
		//#    The entries are in cp437_DOSLatinUS order
		//#

		0x0000,// #NULL
		0x0001,// #START OF HEADING
		0x0002,// #START OF TEXT
		0x0003,// #END OF TEXT
		0x0004,// #END OF TRANSMISSION
		0x0005,// #ENQUIRY
		0x0006,// #ACKNOWLEDGE
		0x0007,// #BELL
		0x0008,// #BACKSPACE
		0x0009,// #HORIZONTAL TABULATION
		0x000a,// #LINE FEED
		0x000b,// #VERTICAL TABULATION
		0x000c,// #FORM FEED
		0x000d,// #CARRIAGE RETURN
		0x000e,// #SHIFT OUT
		0x000f,// #SHIFT IN
		0x0010,// #DATA LINK ESCAPE
		0x0011,// #DEVICE CONTROL ONE
		0x0012,// #DEVICE CONTROL TWO
		0x0013,// #DEVICE CONTROL THREE
		0x0014,// #DEVICE CONTROL FOUR
		0x0015,// #NEGATIVE ACKNOWLEDGE
		0x0016,// #SYNCHRONOUS IDLE
		0x0017,// #END OF TRANSMISSION BLOCK
		0x0018,// #CANCEL
		0x0019,// #END OF MEDIUM
		0x001a,// #SUBSTITUTE
		0x001b,// #ESCAPE
		0x001c,// #FILE SEPARATOR
		0x001d,// #GROUP SEPARATOR
		0x001e,// #RECORD SEPARATOR
		0x001f,// #UNIT SEPARATOR
		0x0020,// #SPACE
		0x0021,// #EXCLAMATION MARK
		0x0022,// #QUOTATION MARK
		0x0023,// #NUMBER SIGN
		0x0024,// #DOLLAR SIGN
		0x0025,// #PERCENT SIGN
		0x0026,// #AMPERSAND
		0x0027,// #APOSTROPHE
		0x0028,// #LEFT PARENTHESIS
		0x0029,// #RIGHT PARENTHESIS
		0x002a,// #ASTERISK
		0x002b,// #PLUS SIGN
		0x002c,// #COMMA
		0x002d,// #HYPHEN-MINUS
		0x002e,// #FULL STOP
		0x002f,// #SOLIDUS
		0x0030,// #DIGIT ZERO
		0x0031,// #DIGIT ONE
		0x0032,// #DIGIT TWO
		0x0033,// #DIGIT THREE
		0x0034,// #DIGIT FOUR
		0x0035,// #DIGIT FIVE
		0x0036,// #DIGIT SIX
		0x0037,// #DIGIT SEVEN
		0x0038,// #DIGIT EIGHT
		0x0039,// #DIGIT NINE
		0x003a,// #COLON
		0x003b,// #SEMICOLON
		0x003c,// #LESS-THAN SIGN
		0x003d,// #EQUALS SIGN
		0x003e,// #GREATER-THAN SIGN
		0x003f,// #QUESTION MARK
		0x0040,// #COMMERCIAL AT
		0x0041,// #LATIN CAPITAL LETTER A
		0x0042,// #LATIN CAPITAL LETTER B
		0x0043,// #LATIN CAPITAL LETTER C
		0x0044,// #LATIN CAPITAL LETTER D
		0x0045,// #LATIN CAPITAL LETTER E
		0x0046,// #LATIN CAPITAL LETTER F
		0x0047,// #LATIN CAPITAL LETTER G
		0x0048,// #LATIN CAPITAL LETTER H
		0x0049,// #LATIN CAPITAL LETTER I
		0x004a,// #LATIN CAPITAL LETTER J
		0x004b,// #LATIN CAPITAL LETTER K
		0x004c,// #LATIN CAPITAL LETTER L
		0x004d,// #LATIN CAPITAL LETTER M
		0x004e,// #LATIN CAPITAL LETTER N
		0x004f,// #LATIN CAPITAL LETTER O
		0x0050,// #LATIN CAPITAL LETTER P
		0x0051,// #LATIN CAPITAL LETTER Q
		0x0052,// #LATIN CAPITAL LETTER R
		0x0053,// #LATIN CAPITAL LETTER S
		0x0054,// #LATIN CAPITAL LETTER T
		0x0055,// #LATIN CAPITAL LETTER U
		0x0056,// #LATIN CAPITAL LETTER V
		0x0057,// #LATIN CAPITAL LETTER W
		0x0058,// #LATIN CAPITAL LETTER X
		0x0059,// #LATIN CAPITAL LETTER Y
		0x005a,// #LATIN CAPITAL LETTER Z
		0x005b,// #LEFT SQUARE BRACKET
		0x005c,// #REVERSE SOLIDUS
		0x005d,// #RIGHT SQUARE BRACKET
		0x005e,// #CIRCUMFLEX ACCENT
		0x005f,// #LOW LINE
		0x0060,// #GRAVE ACCENT
		0x0061,// #LATIN SMALL LETTER A
		0x0062,// #LATIN SMALL LETTER B
		0x0063,// #LATIN SMALL LETTER C
		0x0064,// #LATIN SMALL LETTER D
		0x0065,// #LATIN SMALL LETTER E
		0x0066,// #LATIN SMALL LETTER F
		0x0067,// #LATIN SMALL LETTER G
		0x0068,// #LATIN SMALL LETTER H
		0x0069,// #LATIN SMALL LETTER I
		0x006a,// #LATIN SMALL LETTER J
		0x006b,// #LATIN SMALL LETTER K
		0x006c,// #LATIN SMALL LETTER L
		0x006d,// #LATIN SMALL LETTER M
		0x006e,// #LATIN SMALL LETTER N
		0x006f,// #LATIN SMALL LETTER O
		0x0070,// #LATIN SMALL LETTER P
		0x0071,// #LATIN SMALL LETTER Q
		0x0072,// #LATIN SMALL LETTER R
		0x0073,// #LATIN SMALL LETTER S
		0x0074,// #LATIN SMALL LETTER T
		0x0075,// #LATIN SMALL LETTER U
		0x0076,// #LATIN SMALL LETTER V
		0x0077,// #LATIN SMALL LETTER W
		0x0078,// #LATIN SMALL LETTER X
		0x0079,// #LATIN SMALL LETTER Y
		0x007a,// #LATIN SMALL LETTER Z
		0x007b,// #LEFT CURLY BRACKET
		0x007c,// #VERTICAL LINE
		0x007d,// #RIGHT CURLY BRACKET
		0x007e,// #TILDE
		0x007f,// #DELETE
		0x00c7,// #LATIN CAPITAL LETTER C WITH CEDILLA
		0x00fc,// #LATIN SMALL LETTER U WITH DIAERESIS
		0x00e9,// #LATIN SMALL LETTER E WITH ACUTE
		0x00e2,// #LATIN SMALL LETTER A WITH CIRCUMFLEX
		0x00e4,// #LATIN SMALL LETTER A WITH DIAERESIS
		0x00e0,// #LATIN SMALL LETTER A WITH GRAVE
		0x00e5,// #LATIN SMALL LETTER A WITH RING ABOVE
		0x00e7,// #LATIN SMALL LETTER C WITH CEDILLA
		0x00ea,// #LATIN SMALL LETTER E WITH CIRCUMFLEX
		0x00eb,// #LATIN SMALL LETTER E WITH DIAERESIS
		0x00e8,// #LATIN SMALL LETTER E WITH GRAVE
		0x00ef,// #LATIN SMALL LETTER I WITH DIAERESIS
		0x00ee,// #LATIN SMALL LETTER I WITH CIRCUMFLEX
		0x00ec,// #LATIN SMALL LETTER I WITH GRAVE
		0x00c4,// #LATIN CAPITAL LETTER A WITH DIAERESIS
		0x00c5,// #LATIN CAPITAL LETTER A WITH RING ABOVE
		0x00c9,// #LATIN CAPITAL LETTER E WITH ACUTE
		0x00e6,// #LATIN SMALL LIGATURE AE
		0x00c6,// #LATIN CAPITAL LIGATURE AE
		0x00f4,// #LATIN SMALL LETTER O WITH CIRCUMFLEX
		0x00f6,// #LATIN SMALL LETTER O WITH DIAERESIS
		0x00f2,// #LATIN SMALL LETTER O WITH GRAVE
		0x00fb,// #LATIN SMALL LETTER U WITH CIRCUMFLEX
		0x00f9,// #LATIN SMALL LETTER U WITH GRAVE
		0x00ff,// #LATIN SMALL LETTER Y WITH DIAERESIS
		0x00d6,// #LATIN CAPITAL LETTER O WITH DIAERESIS
		0x00dc,// #LATIN CAPITAL LETTER U WITH DIAERESIS
		0x00a2,// #CENT SIGN
		0x00a3,// #POUND SIGN
		0x00a5,// #YEN SIGN
		0x20a7,// #PESETA SIGN
		0x0192,// #LATIN SMALL LETTER F WITH HOOK
		0x00e1,// #LATIN SMALL LETTER A WITH ACUTE
		0x00ed,// #LATIN SMALL LETTER I WITH ACUTE
		0x00f3,// #LATIN SMALL LETTER O WITH ACUTE
		0x00fa,// #LATIN SMALL LETTER U WITH ACUTE
		0x00f1,// #LATIN SMALL LETTER N WITH TILDE
		0x00d1,// #LATIN CAPITAL LETTER N WITH TILDE
		0x00aa,// #FEMININE ORDINAL INDICATOR
		0x00ba,// #MASCULINE ORDINAL INDICATOR
		0x00bf,// #INVERTED QUESTION MARK
		0x2310,// #REVERSED NOT SIGN
		0x00ac,// #NOT SIGN
		0x00bd,// #VULGAR FRACTION ONE HALF
		0x00bc,// #VULGAR FRACTION ONE QUARTER
		0x00a1,// #INVERTED EXCLAMATION MARK
		0x00ab,// #LEFT-POINTING DOUBLE ANGLE QUOTATION MARK
		0x00bb,// #RIGHT-POINTING DOUBLE ANGLE QUOTATION MARK
		0x2591,// #LIGHT SHADE
		0x2592,// #MEDIUM SHADE
		0x2593,// #DARK SHADE
		0x2502,// #BOX DRAWINGS LIGHT VERTICAL
		0x2524,// #BOX DRAWINGS LIGHT VERTICAL AND LEFT
		0x2561,// #BOX DRAWINGS VERTICAL SINGLE AND LEFT DOUBLE
		0x2562,// #BOX DRAWINGS VERTICAL DOUBLE AND LEFT SINGLE
		0x2556,// #BOX DRAWINGS DOWN DOUBLE AND LEFT SINGLE
		0x2555,// #BOX DRAWINGS DOWN SINGLE AND LEFT DOUBLE
		0x2563,// #BOX DRAWINGS DOUBLE VERTICAL AND LEFT
		0x2551,// #BOX DRAWINGS DOUBLE VERTICAL
		0x2557,// #BOX DRAWINGS DOUBLE DOWN AND LEFT
		0x255d,// #BOX DRAWINGS DOUBLE UP AND LEFT
		0x255c,// #BOX DRAWINGS UP DOUBLE AND LEFT SINGLE
		0x255b,// #BOX DRAWINGS UP SINGLE AND LEFT DOUBLE
		0x2510,// #BOX DRAWINGS LIGHT DOWN AND LEFT
		0x2514,// #BOX DRAWINGS LIGHT UP AND RIGHT
		0x2534,// #BOX DRAWINGS LIGHT UP AND HORIZONTAL
		0x252c,// #BOX DRAWINGS LIGHT DOWN AND HORIZONTAL
		0x251c,// #BOX DRAWINGS LIGHT VERTICAL AND RIGHT
		0x2500,// #BOX DRAWINGS LIGHT HORIZONTAL
		0x253c,// #BOX DRAWINGS LIGHT VERTICAL AND HORIZONTAL
		0x255e,// #BOX DRAWINGS VERTICAL SINGLE AND RIGHT DOUBLE
		0x255f,// #BOX DRAWINGS VERTICAL DOUBLE AND RIGHT SINGLE
		0x255a,// #BOX DRAWINGS DOUBLE UP AND RIGHT
		0x2554,// #BOX DRAWINGS DOUBLE DOWN AND RIGHT
		0x2569,// #BOX DRAWINGS DOUBLE UP AND HORIZONTAL
		0x2566,// #BOX DRAWINGS DOUBLE DOWN AND HORIZONTAL
		0x2560,// #BOX DRAWINGS DOUBLE VERTICAL AND RIGHT
		0x2550,// #BOX DRAWINGS DOUBLE HORIZONTAL
		0x256c,// #BOX DRAWINGS DOUBLE VERTICAL AND HORIZONTAL
		0x2567,// #BOX DRAWINGS UP SINGLE AND HORIZONTAL DOUBLE
		0x2568,// #BOX DRAWINGS UP DOUBLE AND HORIZONTAL SINGLE
		0x2564,// #BOX DRAWINGS DOWN SINGLE AND HORIZONTAL DOUBLE
		0x2565,// #BOX DRAWINGS DOWN DOUBLE AND HORIZONTAL SINGLE
		0x2559,// #BOX DRAWINGS UP DOUBLE AND RIGHT SINGLE
		0x2558,// #BOX DRAWINGS UP SINGLE AND RIGHT DOUBLE
		0x2552,// #BOX DRAWINGS DOWN SINGLE AND RIGHT DOUBLE
		0x2553,// #BOX DRAWINGS DOWN DOUBLE AND RIGHT SINGLE
		0x256b,// #BOX DRAWINGS VERTICAL DOUBLE AND HORIZONTAL SINGLE
		0x256a,// #BOX DRAWINGS VERTICAL SINGLE AND HORIZONTAL DOUBLE
		0x2518,// #BOX DRAWINGS LIGHT UP AND LEFT
		0x250c,// #BOX DRAWINGS LIGHT DOWN AND RIGHT
		0x2588,// #FULL BLOCK
		0x2584,// #LOWER HALF BLOCK
		0x258c,// #LEFT HALF BLOCK
		0x2590,// #RIGHT HALF BLOCK
		0x2580,// #UPPER HALF BLOCK
		0x03b1,// #GREEK SMALL LETTER ALPHA
		0x00df,// #LATIN SMALL LETTER SHARP S
		0x0393,// #GREEK CAPITAL LETTER GAMMA
		0x03c0,// #GREEK SMALL LETTER PI
		0x03a3,// #GREEK CAPITAL LETTER SIGMA
		0x03c3,// #GREEK SMALL LETTER SIGMA
		0x00b5,// #MICRO SIGN
		0x03c4,// #GREEK SMALL LETTER TAU
		0x03a6,// #GREEK CAPITAL LETTER PHI
		0x0398,// #GREEK CAPITAL LETTER THETA
		0x03a9,// #GREEK CAPITAL LETTER OMEGA
		0x03b4,// #GREEK SMALL LETTER DELTA
		0x221e,// #INFINITY
		0x03c6,// #GREEK SMALL LETTER PHI
		0x03b5,// #GREEK SMALL LETTER EPSILON
		0x2229,// #INTERSECTION
		0x2261,// #IDENTICAL TO
		0x00b1,// #PLUS-MINUS SIGN
		0x2265,// #GREATER-THAN OR EQUAL TO
		0x2264,// #LESS-THAN OR EQUAL TO
		0x2320,// #TOP HALF INTEGRAL
		0x2321,// #BOTTOM HALF INTEGRAL
		0x00f7,// #DIVISION SIGN
		0x2248,// #ALMOST EQUAL TO
		0x00b0,// #DEGREE SIGN
		0x2219,// #BULLET OPERATOR
		0x00b7,// #MIDDLE DOT
		0x221a,// #SQUARE ROOT
		0x207f,// #SUPERSCRIPT LATIN SMALL LETTER N
		0x00b2,// #SUPERSCRIPT TWO
		0x25a0,// #BLACK SQUARE
		0x00a0,// #NO-BREAK SPACE
	};

	public char map_cp850_unicode(char x) 
	{
		if (x>=0x100)
			return x;
		return unimap[x];
	}
}
