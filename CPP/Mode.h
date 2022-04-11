#ifndef _mode_h
#define _mode_h

class Mode
{
public:
	virtual void processRemoteString(char *inp, int inplen) = 0;
	virtual void execLocalCommand(char cmd) = 0;
};


#endif