using System;
using System.Collections.Generic;
using System.Text;

namespace LibVt6530
{
	public interface Mode
	{
		void ProcessRemoteString(byte[] inp, int inplen);
		void ExecLocalCommand(char cmd);
		bool IsConvMode();
	}

	public enum PageMode
	{
		MODE_DISPLAY = 0,
		MODE_PROTECT = 1,
		MODE_UNPROTECT = 2
	}
}
