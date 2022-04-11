using System;
using System.Collections.Generic;
using System.Text;

namespace LibVt6530
{
	public interface MappedKeyListener
	{
		void KeyMappedKey(string s);
		void KeyCommand(char c);
		int KeyGetPage();
		int KeyGetCursorX();
		int KeyGetCursorY();
		void KeyGetStartFieldASCII(StringBuilder sb);
	};
}
