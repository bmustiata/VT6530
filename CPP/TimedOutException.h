#ifndef _time_out_exception_h
#define _time_out_exception_h


#include "IOException.h"


class TimedOutException : public IOException
{
	TimedOutException()
		: IOException("")
	{
	}

	TimedOutException(char *message)
		: IOException(message)
	{
	}
}


#endif