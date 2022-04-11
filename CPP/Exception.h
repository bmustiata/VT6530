#ifndef _exception_h
#define _exception_h


class Exception
{
	char msg[255];

public:

	Exception ()
	{
		msg[0] = '\0';
	}

	Exception(char *message)
	{
		lstrcpy(msg, message);
	}

	Exception(Exception &e)
	{
		lstrcpy(msg, e.msg);
	}

	void operator = (Exception e)
	{
		lstrcpy(msg, e.msg);
	}

	~Exception()
	{
	}

	char *getMessage()
	{
		return msg;
	}
};

#endif