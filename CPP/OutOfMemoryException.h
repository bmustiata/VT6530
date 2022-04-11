#ifndef _out_of_memory_exception_h
#define _out_of_memory_exception_h

#include "Exception.h"

class OutOfMemoryException : public Exception
{
public:

	OutOfMemoryException(char *message)
	:	Exception(message)
	{
	}

};

#endif