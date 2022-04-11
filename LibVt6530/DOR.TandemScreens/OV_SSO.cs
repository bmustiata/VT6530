using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace DOR.TandemScreens
{
	public class OV_SSO
	{
		public static string GetLogon(string logon)
		{
			return Encrypt(logon.ToUpper().Replace("DOR\\", "").Replace("REVENUE\\", ""));
		}

		public static string GetLogon()
		{
			WindowsIdentity id = WindowsIdentity.GetCurrent();
			return Encrypt(id.Name.ToUpper().Replace("DOR\\", "").Replace("REVENUE\\", ""));
		}

		private static string Encrypt(string str)
		{
			StringBuilder sb = new StringBuilder(str.Length);
			for (int x = 0; x < str.Length; x++)
			{
				switch (str[x])
				{
					case 'Z': sb.Append('%'); break; //(37)  Z
					case 'Y': sb.Append('&'); break; //(38)  Y
					case 'X': sb.Append('\''); break; //(39)  X
					case 'W': sb.Append(')'); break; //(40)  W
					case 'V': sb.Append('('); break; //(41)  V
					case 'U': sb.Append('*'); break; //(42)  U
					case 'T': sb.Append('+'); break; //(43)  T
					case 'S': sb.Append(','); break; //(44)  S
					case 'R': sb.Append('-'); break; //(45)  R
					case 'Q': sb.Append('.'); break; //(46)  Q
					case 'P': sb.Append('/'); break; //(47)  P
					case 'O': sb.Append('0'); break; //(48)  O
					case 'N': sb.Append('1'); break; //(49)  N
					case 'M': sb.Append('2'); break; //(50)  M
					case 'L': sb.Append('3'); break; //(51)  L
					case 'K': sb.Append('4'); break; //(52)  K
					case 'J': sb.Append('5'); break; //(53)  J
					case 'I': sb.Append('6'); break; //(54)  I
					case 'H': sb.Append('7'); break; //(55)  H
					case 'G': sb.Append('8'); break; //(56)  G
					case 'F': sb.Append('9'); break; //(57)
					case 'E': sb.Append(':'); break; //(58)  E
					case 'D': sb.Append(';'); break; //(59)  D
					case 'C': sb.Append('<'); break; //(60)  C
					case 'B': sb.Append('='); break; //(61)  B
					case 'A': sb.Append('>'); break; //(62)  A
					//case '': sb.Append('>'); break; //(63)  
					//case '': sb.Append('?'); break; //(64)  
					//case '': sb.Append('@'); break; //(65)  
					//case '': sb.Append('A'); break; //(66)  
					//case '': sb.Append('B'); break; //(67)  
					//case '': sb.Append('C'); break; //(68)  
					//case '': sb.Append('D'); break; //(69)
					//case '': sb.Append('E'); break; //(70)
					case '9': sb.Append('F'); break; //(70)  9
					case '8': sb.Append('G'); break; //(71)  8
					case '7': sb.Append('H'); break; //(72)  7
					case '6': sb.Append('I'); break; //(73)  6
					case '5': sb.Append('J'); break; //(74)  5
					case '4': sb.Append('K'); break; //(75)  4
					case '3': sb.Append('L'); break; //(76)  3
					case '2': sb.Append('M'); break; //(77)  2
					case '1': sb.Append('N'); break; //(78)  1
					case '0': sb.Append('O'); break; //(79)  0
				}
			}
			return sb.ToString();
		}
	}
}
