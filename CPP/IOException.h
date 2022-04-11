#ifndef _ioexception_
#define _ioexception_

#include "Exception.h"

class IOException : public Exception
{
public:

	IOException(char *message)
		: Exception(message)
	{
	}
};

#endif