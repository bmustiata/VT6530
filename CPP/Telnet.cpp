
#include "stdafx.h"
#include "Telnet.h"
#include "IOException.h"

/**
 * constants for the negotiation state
 */
#define STATE_DATA	 ((char)0)
#define STATE_IAC	 ((char)1)
#define STATE_IACSB	 ((char)2)
#define STATE_IACWILL	 ((char)3)
#define STATE_IACDO		 ((char)4)
#define STATE_IACWONT	 ((char)5)
#define STATE_IACDONT	 ((char)6)
#define STATE_IACSBIAC	 ((char)7)
#define STATE_IACSBDATA	 ((char)8)
#define STATE_IACSBDATAIAC	 ((char)9)

/**
 * IAC - init sequence for telnet negotiation.
 */
#define IAC   ((char)255)
/**
 * [IAC] End Of Record
 */
#define EOR  ((char)239)
/**
 * [IAC] WILL
 */
#define WILL   ((char)251)
/**
 * [IAC] WONT
 */
#define WONT   ((char)252)
/**
 * [IAC] DO
 */
#define DO    ((char)253)
/**
 * [IAC] DONT
 */
#define DONT  ((char)254)
/**
 * [IAC] Sub Begin 
 */
#define SB   ((char)250)
/**
 * [IAC] Sub End
 */
#define SE   ((char)240)
/**
 * Telnet option: echo text
 */
#define TELOPT_ECHO   ((char)1)
/**
 * Telnet option: End Of Record
 */
#define TELOPT_EOR    ((char)25)
/**
 * Telnet option: Negotiate About Window Size
 */
#define TELOPT_NAWS   ((char)31)

/**
 * Telnet option: Terminal Type
 */
#define TELOPT_TTYPE  ((char)24)  

static const char IACWILL[]  = { IAC, WILL };
static const char IACWONT[]  = { IAC, WONT };
static const char IACDO[]    = { IAC, DO	};
static const char IACDONT[]  = { IAC, DONT };
static const char IACSB[]  = { IAC, SB };
static const char IACSE[]  = { IAC, SE };

/** 
 * Telnet option qualifier 'IS' 
 */
#define TELQUAL_IS  ((char)0)

/** 
 * Telnet option qualifier 'SEND' 
 */
#define TELQUAL_SEND  ((char)1)

Telnet::Telnet(TelnetStateListener *p)
{
	//if ((mysocket = WSASocket(AF_INET, SOCK_STREAM, 0, NULL, 0, WSA_FLAG_OVERLAPPED)) == INVALID_SOCKET)
	if ((mysocket = socket(AF_INET, SOCK_STREAM, 0)) == INVALID_SOCKET)
	{
		throw IOException("Couldn't create socket");
	}
	BOOL opt = TRUE;
	//if (setsockopt(mysocket, IPPROTO_TCP, TCP_NODELAY, (char *)&opt, sizeof(BOOL)) != 0)
	//{
	//	throw IOException("Can't set socket options");
	//}
	if (setsockopt(mysocket, SOL_SOCKET, SO_OOBINLINE, (char *)&opt, sizeof(BOOL)) != 0)
	{
		throw IOException("Can't set socket options");
	}
	peer = p;
	sent34Wont = false;
	neg_state = 0;
	isBuffering = false;

#ifdef _DEBUG
	sendlog = fopen("\\send.txt", "w");
	reclog  = fopen("\\recv.txt", "w");
#endif
}

Telnet::~Telnet()
{
	if (mysocket != NULL && mysocket != INVALID_SOCKET)
	{
		closesocket(mysocket);
	}
#ifdef _DEBUG
	fclose(sendlog);
	fclose(reclog);
#endif
}

/**
 * Connect to the remote host at the specified port.
 * @param address the symbolic host address
 * @param port the numeric port
 * @see #disconnect
 */
void Telnet::connect(char *address, int port)
{
	neg_state = 0;
	
	SOCKADDR_IN addr;
	addr.sin_family = AF_INET;
	addr.sin_port = htons (port);      	

	if (isdigit(address[0]))
	{
		addr.sin_addr.s_addr = inet_addr(address);
	}
	else
	{
		PHOSTENT phostent = gethostbyname(address);
		memcpy ((char FAR *)&(addr.sin_addr), phostent->h_addr, phostent->h_length);
	}
	if (::connect(mysocket, (sockaddr *)&addr, sizeof(addr)) != 0)
	{
		int err = WSAGetLastError();
		throw IOException("Can't connect to host");
	}
}

/**
 * Disconnect from remote host.
 * @see #connect
 */
void Telnet::disconnect()
{
	if (mysocket != NULL && mysocket != INVALID_SOCKET)
	{
		shutdown(mysocket, SD_BOTH);
		Sleep(100);
		closesocket(mysocket);
		mysocket = INVALID_SOCKET;
	}
}

/**
 * Read data from the remote host. Blocks until data is available. 
 * Returns an array of bytes.
 * @see #send
 */
void Telnet::receive(StringBuffer *buf) 
{
	_ASSERT(buf->length() == 0);

	int count = recv(mysocket, readbuf, READBUF_LEN, 0);
	if(count < 0 || count == 0) 
	{
		peer->message("Connection closed");
		throw IOException("Connection closed.");
	}
#ifdef _DEBUG
	//ATLTRACE("Recived");
	readbuf[count] = '\0';
	OutputDebugStringA(readbuf);
	//char ccbuf[2];
	//ccbuf[1] = '\0';
	//for (int q = 0; q < count; q++)
	//{
	//	if (readbuf[q] > 31)
	//	{
	//		ccbuf[0] = readbuf[q];
	//		OutputDebugStringA(ccbuf);
	//	}
	//}
	ATLTRACE("\r\n");
#endif
	negotiate(buf, count);
#ifdef _DEBUG
	static countx;
	fprintf(sendlog, "************************\nReceived %d\n************************\n", countx);
	fflush(sendlog);
	fprintf(reclog, "************************\nReceived %d\n************************\n", countx++);
	fflush(reclog);
#endif
}


/**
 * Handle an incoming IAC SB <type> <bytes> IAC SE
 * @param type type of SB
 * @param sbata byte array as <bytes>
 * @param sbcount nr of bytes. may be 0 too.
 */
void Telnet::handle_sb(char type, char *sbdata, int sbcount, StringBuffer *telBuf) 
{
	_ASSERT(_CrtIsValidPointer(sbdata, sbcount, TRUE));

	switch (type) 
	{
		case TELOPT_TTYPE:
			if (sbcount>0 && sbdata[0] == TELQUAL_SEND) 
			{
				char *ttype;
				telBuf->append((char*)IACSB, 2);
				telBuf->append(TELOPT_TTYPE);
				telBuf->append(TELQUAL_IS);
				ttype = peer->ttype();
				//if(ttype == null) 
				//{
				//	ttype = "TN6530-8";
				//}
				//byte[] bttype = new byte[ttype.length()];

				telBuf->append(ttype, strlen(ttype));
				telBuf->append((char *)IACSE, 2);
			}
			break;
		case 34:
			peer->set34("SB", sbdata, sbcount);
			// don't know what this does.
			if (!sent34Wont)
			{
				char sb[] = {(char)255, (char)252, 34};
				telBuf->append(sb, 3);
				sent34Wont = true;
			}
			break;
		default:
			{
				char msgbuf[255];
				msgbuf[0] = '\0';
				wsprintf(msgbuf, "%s%c", "Unknown IAC SB: ", type);
				peer->message(msgbuf);
			}
			break;
	}
}


void Telnet::negotiate(StringBuffer *out, int count) 
{
	StringBuffer telBuf; 

#ifdef DEBUG
	for (int qq = 0; qq < count; qq++)
	{
		fprintf(reclog, "%c\t%d\n", (char)readbuf[qq], (int)(readbuf[qq]&0xFF));
	}
	fflush(reclog);
#endif

	//char nbuf[count];
	char *sbbuf = new char[count];
	char sendbuf[3];
	char b, reply = 0;
	int  sbcount = 0;
	int boffset = 0, noffset = 0;

	while(boffset < count) 
	{
		b = readbuf[boffset++];
		/* of course, byte is a signed entity (-128 -> 127)
		* but apparently the SGI Netscape 3.0 doesn't seem
		* to care and provides happily values up to 255
		*/
		if (b >= 128)
			b = (char)((int)b-256);
		switch (neg_state) 
		{
			case STATE_DATA:
				if (b == IAC) 
				{
					neg_state = STATE_IAC;
				}
				else if (b == 5)
				{
					peer->enquire();
				}
				else 
				{
					out->append(b);
				}
				break;
			case STATE_IAC:
				switch (b) 
				{
					case IAC:
						neg_state = STATE_DATA;
						//nbuf[noffset++] = IAC;
						out->append(IAC);
						break;
					case WILL:
						neg_state = STATE_IACWILL;
						break;
					case WONT:
						neg_state = STATE_IACWONT;
						break;
					case DONT:
						neg_state = STATE_IACDONT;
						break;
					case DO:
						neg_state = STATE_IACDO;
						break;
					case EOR:
						neg_state = STATE_DATA;
						break;
					case SB:
						neg_state = STATE_IACSB;
						sbcount = 0;
						break;
					default:
						{
							StringBuffer msg;
							msg.append("<UNKNOWN STATE_IAC=");
							msg.append((int)b);
							peer->message(msg);
						}
						neg_state = STATE_DATA;
						break;
				}
				break;
			case STATE_IACWILL:
				switch(b) 
				{
					case TELOPT_ECHO:
						//reply = DO;
						reply = WILL;
						b = 3;
						//vec = new Vector(2);
						//vec.addElement("NOLOCALECHO");
						peer->localEchoOff();
						break;
					case TELOPT_EOR:
						reply = DO;
						break;
					case 3:
						reply = WILL;
						b = 34;
						break;
					default:
						{
							StringBuffer msg;
							msg.append("UNKNOWN STATE_IACWILL=");
							msg.append(b);
							peer->message(msg);
						}
						reply = DONT;
						break;
				}
				if (	reply != sentDX[b+128] ||
						WILL != receivedWX[b+128]
					) 
				{
					sendbuf[0]=IAC;
					sendbuf[1]=reply;
					sendbuf[2]=b;
					telBuf.append(sendbuf, 3);
					sentDX[b+128] = reply;
					receivedWX[b+128] = WILL;
				}
				neg_state = STATE_DATA;
				break;
			case STATE_IACWONT:
				switch(b) 
				{
					case TELOPT_ECHO:

						//vec = new Vector(2);
						//vec.addElement("LOCALECHO");
						peer->localEchoOn();
						reply = DONT;
						break;
					case TELOPT_EOR:
						reply = DONT;
						break;
					case 3:
						reply = DONT;
						b = 34;
						break;
					default:
						{
							StringBuffer msg;
							msg.append("UNKNOWN STATE_IACWONT=");
							msg.append(b);
							peer->message(msg);
						}
						reply = DONT;
						break;
				}
				if (	reply != sentDX[b+128] ||
						WONT != receivedWX[b+128]
					) 
				{
					sendbuf[0]=IAC;
					sendbuf[1]=reply;
					sendbuf[2]=b;
					telBuf.append(sendbuf, 3);
					sentDX[b+128] = reply;
					receivedWX[b+128] = WILL;
				}
				neg_state = STATE_DATA;
				break;
			case STATE_IACDO:
				switch (b) 
				{
					case TELOPT_ECHO:
						reply = WILL;
						//vec = new Vector(2);
						//vec.addElement("LOCALECHO");
						peer->localEchoOn();
						break;
					case TELOPT_TTYPE:
						reply = WILL;
						break;
					case TELOPT_NAWS:
						//vec = new Vector(2);
						//vec.addElement("NAWS");
						//Dimension size = peer.naws();
						//receivedDX[b] = DO;
						//if(size == null)
						//{
							/* this shouldn't happen */
						//send(IAC);
						//sendbuf[1]=WILL;
						//reply = DO;
						//b = TELOPT_ECHO;
						b = TELOPT_NAWS;
						reply = WONT;
						//sentWX[b] = WONT;
							//break;
						//}
						//reply = WILL;
						//sentWX[b] = WILL;
						//sendbuf[0]=IAC;
						//sendbuf[1]=WILL;
						//sendbuf[2]=TELOPT_NAWS;
						//send(sendbuf);
						//send(IAC);
						//send(SB);
						//send(TELOPT_NAWS);
						//send((byte) (size.width >> 8));
						//send((byte) (size.width & 0xff));
						//send((byte) (size.height >> 8));
						//send((byte) (size.height & 0xff));
						//send(IAC);
						//send(SE);
						break;
					case 3:
						reply = DO;
						b = TELOPT_ECHO;
						break;
					case 34:
						peer->set34("DO", "", 0);
						reply = DO;
						{
							char bp[] = {(char)255, (char)250, 34, 1, 1, (char)255, (char)240};
							telBuf.append(bp, 7);
						}
						neg_state = STATE_DATA;
						continue;
					default:
						{
							StringBuffer msg;
							msg.append("UNKNOWN STATE_IACDO=");
							msg.append(b);
							peer->message(msg);
						}
						reply = WONT;
						break;
				}
				if (	reply != sentWX[128+b] ||
						DO != receivedDX[128+b]
					) 
				{
					sendbuf[0]=IAC;
					sendbuf[1]=reply;
					sendbuf[2]=b;
					telBuf.append(sendbuf, 3);
					sentWX[b+128] = reply;
					receivedDX[b+128] = DO;
				}
				neg_state = STATE_DATA;
				break;
			case STATE_IACDONT:
				switch (b) 
				{
					case TELOPT_ECHO:
						reply	= WONT;
						//vec = new Vector(2);
						//vec.addElement("NOLOCALECHO");
						peer->localEchoOff();
						break;
					case TELOPT_NAWS:
						reply	= WONT;
						break;
					case 34:
						peer->set34("DONT", "", 0);
						reply = WONT;
						break;
					default:
						{
							StringBuffer msg;
							msg.append("UNKNOWN STATE_IACDONT=");
							msg.append(b);
							peer->message(msg);
						}
						reply	= WONT;
						break;
				}
				if (	reply	!= sentWX[b+128] ||
						DONT	!= receivedDX[b+128]
					) 
				{
					telBuf.append(IAC);
					telBuf.append(reply);
					telBuf.append(b);
					sentWX[b+128]		= reply;
					receivedDX[b+128]	= DONT;
				}
				neg_state = STATE_DATA;
				break;
			case STATE_IACSBIAC:
				if (b == IAC) 
				{
					sbcount		= 0;
					current_sb	= b;
					neg_state	= STATE_IACSBDATA;
				}
				else 
				{
					peer->message("(bad)");//"+b+" ");
					neg_state	= STATE_DATA;
				}
				break;
			case STATE_IACSB:
				switch (b) 
				{
					case IAC:
						neg_state = STATE_IACSBIAC;
						break;
					case 24:
						{
							char sb[] = {IAC, SB, TELOPT_TTYPE, 0, 'T', 'N', '6', '5', '3', '0', '-', '8'};
							telBuf.append(sb, 12);
							reply = SE;
							b = 0;
							telBuf.append(IAC);
							telBuf.append(SE);
							sentWX[b+128] = reply;
							receivedDX[b+128] = DO;
						}
						break;
					default:
						current_sb	= b;
						sbcount		= 0;
						neg_state	= STATE_IACSBDATA;
						break;
				}
				break;
			case STATE_IACSBDATA:
				switch (b) 
				{
					case IAC:
						neg_state = STATE_IACSBDATAIAC;
						break;
					default:
						sbbuf[sbcount++] = b;
						break;
				}
				break;
			case STATE_IACSBDATAIAC:
				switch (b) 
				{
					case IAC:
						neg_state = STATE_IACSBDATA;
						sbbuf[sbcount++] = IAC;
						break;
					case SE:
						handle_sb(current_sb, sbbuf, sbcount, &telBuf);
						current_sb	= 0;
						neg_state	= STATE_DATA;
						break;
					case SB:
						handle_sb(current_sb, sbbuf, sbcount, &telBuf);
						neg_state	= STATE_IACSB;
						break;
					default:
						neg_state	= STATE_DATA;
						break;
				}
				break;
			default:
				{
					StringBuffer msg;
					msg.append("Shouldn't happen in Telnet.cpp");
					msg.append(b);
					peer->message(msg);
				}
				neg_state = STATE_DATA;
				break;
		}
	}
	_send(telBuf, telBuf.length());

	_ASSERT(_CrtIsMemoryBlock(sbbuf, count * sizeof(char), 0, 0, 0));
	delete[] sbbuf;
}

