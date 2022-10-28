package gov.revenue.vt6530.modes;

import java.io.IOException;

/**
 *  Mode is an interface for terminal command
 *  interpreters
 */
public interface Mode
{
	void processRemoteString(byte[] inp) throws IOException;
	void execLocalCommand(byte cmd);
}
