package gov.revenue.vt6530.telnet;

import java.net.Socket;
import java.io.BufferedInputStream;
import java.io.BufferedOutputStream;
import java.io.IOException;
import java.awt.Dimension;
import java.util.Vector;
import java.io.*;

import gov.revenue.ASSERT;

/**
 *  Telnet implement RFC 854.  Most of this code was "reused" from
 *  a GPL'ed VT320.
 * 
 *  The Tandem uses a nonstandard control sequence (34).  The 34
 *  command seems to control local buffering and character 
 *  attributes in conversation mode.
 */
public class Telnet implements TelnetStateListener
{
	private PrintWriter sendlog;
	private PrintWriter reclog;
	
	private byte neg_state = 0;

	/**
	 * constants for the negotiation state
	 */
	private final static byte STATE_DATA	= 0;
	private final static byte STATE_IAC		= 1;
	private final static byte STATE_IACSB	= 2;
	private final static byte STATE_IACWILL	= 3;
	private final static byte STATE_IACDO	= 4;
	private final static byte STATE_IACWONT	= 5;
	private final static byte STATE_IACDONT	= 6;
	private final static byte STATE_IACSBIAC	= 7;
	private final static byte STATE_IACSBDATA	= 8;
	private final static byte STATE_IACSBDATAIAC	= 9;

	/**
	 * What IAC SB <xx> we are handling right now
	 */
	private byte current_sb;

	/**
	 * IAC - init sequence for telnet negotiation.
	 */
	private final static byte IAC  = (byte)255;
	/**
	 * [IAC] End Of Record
	 */
	private final static byte EOR  = (byte)239;
	/**
	 * [IAC] WILL
	 */
	private final static byte WILL  = (byte)251;
	/**
	 * [IAC] WONT
	 */
	private final static byte WONT  = (byte)252;
	/**
	 * [IAC] DO
	 */
	private final static byte DO    = (byte)253;
	/**
	 * [IAC] DONT
	 */
	private final static byte DONT  = (byte)254;
	/**
	 * [IAC] Sub Begin 
	 */
	private final static byte SB  = (byte)250;
	/**
	 * [IAC] Sub End
	 */
	private final static byte SE  = (byte)240;
	/**
	 * Telnet option: echo text
	 */
	private final static byte TELOPT_ECHO  = (byte)1;  /* echo on/off */
	/**
	 * Telnet option: End Of Record
	 */
	private final static byte TELOPT_EOR   = (byte)25;  /* end of record */
	/**
	 * Telnet option: Negotiate About Window Size
	 */
	private final static byte TELOPT_NAWS  = (byte)31;  /* NA-WindowSize*/
	/**
	 * Telnet option: Terminal Type
	 */
	private final static byte TELOPT_TTYPE  = (byte)24;  /* terminal type */

	private final static byte[] IACWILL  = {IAC, WILL };
	private final static byte[] IACWONT  = {IAC, WONT };
	private final static byte[] IACDO    = { IAC, DO	};
	private final static byte[] IACDONT  = { IAC, DONT };
	private final static byte[] IACSB  = { IAC, SB };
	private final static byte[] IACSE  = {	IAC, SE };

	/** 
	 * Telnet option qualifier 'IS' 
	 */
	private final static byte TELQUAL_IS = (byte)0;

	/** 
	 * Telnet option qualifier 'SEND' 
	 */
	private final static byte TELQUAL_SEND = (byte)1;

	/**
	 * What IAC DO(NT) request do we have received already ?
	 */
	private byte[] receivedDX;
	
	/**
	 * What IAC WILL/WONT request do we have received already ?
	 */
	private byte[] receivedWX;
	/**
	 * What IAC DO/DONT request do we have sent already ?
	 */
	private byte[] sentDX;
	/**
	 * What IAC WILL/WONT request do we have sent already ?
	 */
	private byte[] sentWX;

	private Socket socket;
	private BufferedInputStream is;
	private BufferedOutputStream os;

	private TelnetStateListener peer = this;		/* peer, notified on status */

	public Telnet(TelnetStateListener p)
	{
		super();
		peer = p;

		if (ASSERT.debug > 0)
		{
			try
			{
				sendlog = new PrintWriter(new FileWriter("\\send.txt"));
				reclog  = new PrintWriter(new FileWriter("\\recv.txt"));
			}
			catch (IOException ie)
			{
				ie.printStackTrace();
			}
		}
	}
	
	/**
	 * Connect to the remote host at the specified port.
	 * @param address the symbolic host address
	 * @param port the numeric port
	 * @see #disconnect
	 */
	public void connect(String address, int port) throws IOException 
	{
		if(ASSERT.debug > 0)
		{
			System.out.println("Telnet.connect(" + address + "," + port + ")");
		}
		socket = new Socket(address, port);
		is = new BufferedInputStream(socket.getInputStream());
		os = new BufferedOutputStream(socket.getOutputStream());
		neg_state = 0;
		receivedDX = new byte[256]; 
		sentDX = new byte[256];
		receivedWX = new byte[256]; 
		sentWX = new byte[256];
	}

	/**
	 * Disconnect from remote host.
	 * @see #connect
	 */
	public void disconnect() throws IOException 
	{
		if(ASSERT.debug > 0)
		{
			System.out.println("TelnetIO.disconnect()");
		}
		if(socket !=null) 
		{
			socket.close();
		}
		if (ASSERT.debug > 0)
		{
			reclog.close();
			sendlog.close();
		}
	}
	
	/**
	 * Connect to the remote host at the default telnet port (23).
	 * @param address the symbolic host address
	 */
	public void connect(String address) throws IOException 
	{
		connect(address, 23);
	}

	/**
	 * Set the object to be notified about current status.
	 * @param obj object to be notified.
	 */
	public void setListener(TelnetStateListener obj) 
	{ 
		peer = obj; 
	}

	/** Returns bytes available to be read.  Since they haven't been
	 * negotiated over, this could be misleading.
	 * Most useful as a boolean value - "are any bytes available" -
	 * rather than as an exact count of "how many ara available."
	 *
	 * @exception IOException on problems with the socket connection
	 */
	public int available() throws IOException
	{
		return is.available();
	}
	
	int countx;
	/**
	 * Read data from the remote host. Blocks until data is available. 
	 * Returns an array of bytes.
	 * @see #send
	 */
	public byte[] receive() throws IOException 
	{
		int count = is.available();
		byte buf[] = new byte[count];
		count = is.read(buf);
		if(count < 0) 
		{
			peer.message("Connection closed");
			throw new IOException("Connection closed.");
		}
		buf = negotiate(buf, count);
		if (count == 0 && ASSERT.debug != 0)
		{
			sendlog.println("***********************");
			sendlog.println("Recieved " + countx);
			sendlog.println("***********************");
			reclog.println("***********************");
			reclog.println("Recieved " + countx++);
			reclog.println("***********************");
		}
		else if (ASSERT.debug != 0)
		{
			sendlog.println("***********************");
			sendlog.println("Recieved Empty Packet");
			sendlog.println("***********************");
			reclog.println("***********************");
			reclog.println("Recieved Empty Packet");
			reclog.println("***********************");
		}
		return buf;
	}

	/**
	 * Send data to the remote host.
	 * @param buf array of bytes to send
	 * @see #receive
	 */
	public void send(byte[] buf) throws IOException 
	{
		if (ASSERT.debug > 0)
		{
			for (int qq = 0; qq < buf.length; qq++)
			{
				sendlog.println((char)buf[qq] + "\t" + (int)(buf[qq]&0xFF));
			}
			sendlog.flush();
		}
		if(ASSERT.debug > 1) 
		{
			System.out.println("TelnetIO.send(" + buf + ")");
		}
		os.write(buf);
		os.flush();
	}

	public void send(byte b) throws IOException 
	{
		if (ASSERT.debug > 0)
		{
			sendlog.println((char)b + "\t" + (int)(b&0xFF));
			sendlog.flush();
		}
		if(ASSERT.debug > 1) 
		{
			System.out.println("TelnetIO.send(" + b + ")");
		}
		os.write(b);
		os.flush();
	}

	boolean sent34Wont = false;
	
	/**
	 * Handle an incoming IAC SB <type> <bytes> IAC SE
	 * @param type type of SB
	 * @param sbata byte array as <bytes>
	 * @param sbcount nr of bytes. may be 0 too.
	 */
	private void handle_sb(byte type, byte[] sbdata, int sbcount) 
		throws IOException 
	{
		if (ASSERT.debug > 1) 
		{
			System.out.println("TelnetIO.handle_sb("+type+")");
		}
		switch (type) 
		{
			case TELOPT_TTYPE:
				if (sbcount>0 && sbdata[0]==TELQUAL_SEND) 
				{
					String ttype;
					send(IACSB);
					send(TELOPT_TTYPE);
					send(TELQUAL_IS);
					ttype = peer.ttype();
					if(ttype == null) 
					{
						ttype = "TN6530-8";
					}
					byte[] bttype = new byte[ttype.length()];

					ttype.getBytes(0,ttype.length(), bttype, 0);
					send(bttype);
					send(IACSE);
				}
				break;
			case 34:
				// don't know what this does.
				if (!sent34Wont)
				{
					byte[] sb = {(byte)255, (byte)252, 34};
					send(sb);
					sent34Wont = true;
				}
				break;
			default:
				System.out.println("Unknown IAC SB: " + type);
				break;
		}
	}

	/* wo faengt buf an bei buf[0] oder bei buf[1] */
	private byte[] negotiate(byte buf[], int count) throws IOException 
	{
		if (ASSERT.debug > 0)
		{
			for (int qq = 0; qq < buf.length; qq++)
			{
				reclog.println((char)buf[qq] + "\t" + (int)(buf[qq]&0xFF));
			}
			reclog.flush();
		}
		if(ASSERT.debug > 1) 
		{
			System.out.println("TelnetIO.negotiate("+buf+","+count+")");
		}
		byte nbuf[] = new byte[count];
		byte sbbuf[] = new byte[count];
		byte sendbuf[] = new byte[3];
		byte b,reply = 0;
		int  sbcount = 0;
		int boffset = 0, noffset = 0;
		//Vector	vec = new Vector(2);

		while(boffset < count) 
		{
			b = buf[boffset++];
			/* of course, byte is a signed entity (-128 -> 127)
			* but apparently the SGI Netscape 3.0 doesn't seem
			* to care and provides happily values up to 255
			*/
			if (b >= 128)
				b = (byte)((int)b-256);
			switch (neg_state) 
			{
				case STATE_DATA:
					if (b == IAC) 
					{
						neg_state = STATE_IAC;
					}
					else if (b == 5)
					{
						peer.enquire();
					}
					else 
					{
						nbuf[noffset++] = b;
					}
					break;
				case STATE_IAC:
					switch (b) 
					{
						case IAC:
							if(ASSERT.debug > 2) 
								System.out.print("IAC ");
							neg_state = STATE_DATA;
							nbuf[noffset++] = IAC;
							break;
						case WILL:
							if(ASSERT.debug > 2)
								System.out.print("WILL ");
							neg_state = STATE_IACWILL;
							break;
						case WONT:
							if(ASSERT.debug > 2)
								System.out.print("WONT ");
							neg_state = STATE_IACWONT;
							break;
						case DONT:
							if(ASSERT.debug > 2)
								System.out.print("DONT ");
							neg_state = STATE_IACDONT;
							break;
						case DO:
							if(ASSERT.debug > 2)
								System.out.print("DO ");
							neg_state = STATE_IACDO;
							break;
						case EOR:
							if(ASSERT.debug > 2)
								System.out.print("EOR ");
							neg_state = STATE_DATA;
							break;
						case SB:
							if(ASSERT.debug > 2)
								System.out.print("SB ");
							neg_state = STATE_IACSB;
							sbcount = 0;
							break;
						default:
							if(ASSERT.debug > 0)
								System.out.print("<UNKNOWN STATE_IAC="+b+" > ");
							neg_state = STATE_DATA;
							break;
					}
					break;
				case STATE_IACWILL:
					switch(b) 
					{
						case TELOPT_ECHO:
							if(ASSERT.debug > 2) 
								System.out.println("ECHO");
							//reply = DO;
							reply = WILL;
							b = 3;
							//vec = new Vector(2);
							//vec.addElement("NOLOCALECHO");
							peer.localEchoOff();
							break;
						case TELOPT_EOR:
							if(ASSERT.debug > 2) 
								System.out.println("EOR");
							reply = DO;
							break;
						case 3:
							reply = WILL;
							b = 34;
							break;
						default:
							if(ASSERT.debug > 0)
							{
								System.out.println("<UNKNOWN STATE_IACWILL="+b+">");
							}
							reply = DONT;
							break;
					}
					if(ASSERT.debug > 1)
						System.out.println("<"+b+", WILL ="+WILL+">");
					if (	reply != sentDX[b+128] ||
							WILL != receivedWX[b+128]
						) 
					{
						sendbuf[0]=IAC;
						sendbuf[1]=reply;
						sendbuf[2]=b;
						send(sendbuf);
						sentDX[b+128] = reply;
						receivedWX[b+128] = WILL;
					}
					neg_state = STATE_DATA;
					break;
				case STATE_IACWONT:
					switch(b) 
					{
						case TELOPT_ECHO:
							if(ASSERT.debug > 2) 
								System.out.println("ECHO");

							//vec = new Vector(2);
							//vec.addElement("LOCALECHO");
							peer.localEchoOn();
							reply = DONT;
							break;
						case TELOPT_EOR:
							if(ASSERT.debug > 2) 
								System.out.println("EOR");
							reply = DONT;
							break;
						case 3:
							reply = DONT;
							b = 34;
							break;
						default:
							if(ASSERT.debug > 0) 
							{
								System.out.println("<UNKNOWN STATE_IACWONT="+b+">");
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
						send(sendbuf);
						sentDX[b+128] = reply;
						receivedWX[b+128] = WILL;
					}
					neg_state = STATE_DATA;
					break;
				case STATE_IACDO:
					switch (b) 
					{
						case TELOPT_ECHO:
							if(ASSERT.debug > 2) 
								System.out.println("ECHO");
							reply = WILL;
							//vec = new Vector(2);
							//vec.addElement("LOCALECHO");
							peer.localEchoOn();
							break;
						case TELOPT_TTYPE:
							if(ASSERT.debug > 2) 
								System.out.println("TTYPE");
							reply = WILL;
							break;
						case TELOPT_NAWS:
							if(ASSERT.debug > 2) 
								System.out.println("NAWS");
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
							//send(IAC);send(SB);send(TELOPT_NAWS);
							//send((byte) (size.width >> 8));
							//send((byte) (size.width & 0xff));
							//send((byte) (size.height >> 8));
							//send((byte) (size.height & 0xff));
							//send(IAC);send(SE);
							break;
						case 3:
							reply = DO;
							b = TELOPT_ECHO;
							break;
						case 34:
							reply = DO;
							{
								byte[] bp = {(byte)255, (byte)250, 34, 1, 1, (byte)255, (byte)240};
								send(bp);
							}
							neg_state = STATE_DATA;
							continue;
						default:
							if(ASSERT.debug > 0) 
								System.out.println("<UNKNOWN STATE_IACDO="+b+">");
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
						send(sendbuf);
						sentWX[b+128] = reply;
						receivedDX[b+128] = DO;
					}
					neg_state = STATE_DATA;
					break;
				case STATE_IACDONT:
					switch (b) 
					{
						case TELOPT_ECHO:
							if(ASSERT.debug > 2) 
								System.out.println("ECHO");
							reply	= WONT;
							//vec = new Vector(2);
							//vec.addElement("NOLOCALECHO");
							peer.localEchoOff();
							break;
						case TELOPT_NAWS:
							if(ASSERT.debug > 2) 
								System.out.println("NAWS");
							reply	= WONT;
							break;
						case 34:
							reply = WONT;
							break;
						default:
							if(ASSERT.debug > 0) 
								System.out.println("<UNKNOWN STATE_IACDONT="+b+">");
							reply	= WONT;
							break;
					}
					if (	reply	!= sentWX[b+128] ||
							DONT	!= receivedDX[b+128]
						) 
					{
						send(IAC);send(reply);send(b);
						sentWX[b+128]		= reply;
						receivedDX[b+128]	= DONT;
					}
					neg_state = STATE_DATA;
					break;
				case STATE_IACSBIAC:
					if(ASSERT.debug > 2) System.out.println(""+b+" ");
					if (b == IAC) 
					{
						sbcount		= 0;
						current_sb	= b;
						neg_state	= STATE_IACSBDATA;
					}
					else 
					{
						System.out.println("(bad) "+b+" ");
						neg_state	= STATE_DATA;
					}
					break;
				case STATE_IACSB:
					if(ASSERT.debug > 2) System.out.println(""+b+" ");
					switch (b) 
					{
						case IAC:
							neg_state = STATE_IACSBIAC;
							break;
						case 24:
							{
								byte sb[] = {IAC, SB, TELOPT_TTYPE, 0, (byte)'T',(byte)'N',(byte)'6',(byte)'5',(byte)'3',(byte)'0',(byte)'-',(byte)'8'};
								send(sb);
								reply = SE;
								b = 0;
								send(IAC);
								send(SE);
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
					if (ASSERT.debug > 2) System.out.println(""+b+" ");
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
					if (ASSERT.debug > 2) System.out.println(""+b+" ");
					switch (b) 
					{
						case IAC:
							neg_state = STATE_IACSBDATA;
							sbbuf[sbcount++] = IAC;
							break;
						case SE:
							handle_sb(current_sb,sbbuf,sbcount);
							current_sb	= 0;
							neg_state	= STATE_DATA;
							break;
						case SB:
							handle_sb(current_sb,sbbuf,sbcount);
							neg_state	= STATE_IACSB;
							break;
						default:
							neg_state	= STATE_DATA;
							break;
					}
					break;
				default:
					if (ASSERT.debug > 0) 
						System.out.println("This should not happen: "+neg_state+" ");
					neg_state = STATE_DATA;
					break;
			}
		}
		buf	= new byte[noffset];
		System.arraycopy(nbuf, 0, buf, 0, noffset);
		return buf;
	}

	public String ttype()
	{
		return "TN6530-8";
	}
	
	public void localEchoOn()
	{
	}

	public void localEchoOff()
	{
	}
	
	public Dimension naws()
	{
		return null;
	}
	
	public void setMode(char mode)
	{
	}
	
	public void message(String msg)
	{
	}
	
	public void enquire()
	{
	}
}
