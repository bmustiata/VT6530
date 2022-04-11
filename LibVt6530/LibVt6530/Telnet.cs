using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;

using Maximum;
using Maximum.AnsiTerm;

namespace LibVt6530
{
	enum TelnetCodeState
	{
		TERM_STATE_0 = 0,
		TERM_STATE_IAC = 1,		// 255 TELNET command seq
		TERM_STATE_DO = 2,
		TERM_STATE_DONT = 3,
		TERM_STATE_WILL = 4,
		TERM_STATE_WONT = 5,
		TERM_STATE_SB = 6,
		TERM_STATE_SB_TERM_TYPE = 7,
		TERM_STATE_SB_TERM_TYPE_IS = 8,
		TERM_STATE_SB_TERM_TYPE_SEND = 9,
		TERM_STATE_SB_SE = 10
	};

	enum TelnetControl
	{
		TN_IAC = (byte)255,

		// control
		TN_DONT = (byte)254,
		TN_DO = (byte)253,
		TN_WONT = (byte)252,
		TN_WILL = (byte)251,
		TN_SB = (byte)250,		// SuBnegotiation option
		TN_SE = (byte)240,		// Subnegotiation End 
		TN_NOP = (byte)241,
		TN_DATAMARK = (byte)242,
		TN_BREAK = (byte)243,
		TN_IP = (byte)244,		// interrupt
		TN_AO = (byte)245,		// abort output
		TN_PING = (byte)246,	// are you there
		TN_EC = (byte)247,		// erase char
		TN_EL = (byte)248,		// erase line
		TN_GA = (byte)249,		// go ahead

		TN_SB_SEND = (byte)1,
		TN_SB_IS = (byte)0
	};

	enum TelnetOptions
	{
		// options
		TN_BINARY = (byte)0,
		TN_ECHO = (byte)1,
		TN_RECONNECT = (byte)2,
		TN_STOP_GA = (byte)3,		// stop go ahead
		TN_MSG_SIZE_NEG = (byte)4,
		TN_STATUS = (byte)5,
		TN_TIMING = (byte)6,
		TN_REMOTE_CTL_TRANS_ECHO = (byte)7,
		TN_OUT_LINE_WIDTH = (byte)8,
		TN_OUT_PAGE_SIZE = (byte)9,
		TN_NAOCRD = (byte)10,
		TN_NAOHTS = (byte)11,
		TN_NAOHTD = (byte)12,
		TN_NAOFFD = (byte)13,
		TN_NAOVTS = (byte)14,
		TN_NAOVTD = (byte)15,
		TN_NAOLFD = (byte)16,
		TN_EXTEND_ASCII = (byte)17,
		TN_LOGOUT = (byte)18,
		TN_BM = (byte)19,			// byte macro
		TN_DTE = (byte)20,			// data entry terminal
		TN_SUPDUP = (byte)21,
		TN_SUPDUP_OUT = (byte)22,
		TN_SEND_LOC = (byte)23,
		TN_TERM_TYPE = (byte)24,
		TN_EOR = (byte)25,
		TN_USERID = (byte)26,
		TN_OUT_MARK = (byte)27,
		TN_TTYLOC = (byte)28,
		TN_VT3270 = (byte)29,
		TN_X3PAD = (byte)30,
		TN_NAWS = (byte)31,		// negotiate about window size
		TN_TERM_SPEED = (byte)32,
		TN_REMOTE_FLOW_CTL = (byte)33,
		TN_LINE_MODE = (byte)34,
		TN_X_LOC = (byte)35,
		TN_ENV_OPT = (byte)36,
		TN_AUTH = (byte)37,
		TN_ENCRYPT = (byte)38,
		TN_NEW_ENV_OPT = (byte)39,
		TN_XAUTH = (byte)40,
		TN_CHARSET = (byte)41,
		TN_SUPPRESS_LOCAL_ECHO = (byte)45,
		TN_EXENDS_OPTION_LIST = (byte)255
	};

	public class OptionState
	{
		public enum OptionStateCode {
			DO = 1,
			DONT = 2,
			WILL = 3,
			WONT = 4,
			UNSET = 0
		};

		protected OptionStateCode m_state;

		public OptionState() 
		{
			m_state = OptionStateCode.UNSET; 
		}
		public OptionStateCode GetState() { return m_state; }
		public void SetState(OptionStateCode val) { m_state = val; }
		public bool IsDO() { return m_state == OptionStateCode.DO; }
		public bool IsDONT() { return m_state == OptionStateCode.DONT; }
		public bool IsWILL() { return m_state == OptionStateCode.WILL; }
		public bool IsWONT() { return m_state == OptionStateCode.WONT; }
		public void SetDO() { m_state = OptionStateCode.DO; }
		public void SetDONT() { m_state = OptionStateCode.DONT; }
		public void SetWILL() { m_state = OptionStateCode.WILL; }
		public void SetWONT() { m_state = OptionStateCode.WONT; }
		public bool IsSet() { return m_state != OptionStateCode.UNSET; }
		public void Clear() { m_state = OptionStateCode.UNSET; }

		public override string ToString()
		{
			switch( m_state )
			{
			case OptionStateCode.DO: return "DO";
			case OptionStateCode.DONT: return "DONT";
			case OptionStateCode.WILL: return "WILL";
			case OptionStateCode.WONT: return "WONT";
			default: return "???";
			}
		}
	};

	public delegate void TNACTION(TelnetFilter tn, byte option);

	public interface ITelnetListener
	{
		void OnTelnetRecv( byte[] data, int len );
		void OnTelnetConnect();
		void OnTelnetClose();
		void OnTelnetError(Exception se);
		void OnTelnetUnmappedOption(int command, int option);
		void TelnetGetWindowSize(out int widthInChars, out int heightInChars);
		string TelnetGetTeminalName();
	};


	public class TelnetFilter
	{
		private ITelnetListener m_listener;
		private IConnection m_conn;
		private TelnetCodeState m_state;
		private byte[] m_iacbuf = new byte[6];
		private StringBuilder m_termtype;
		private TermCap m_caps = new TermCap();
		private bool m_termIdentified;
		private bool m_notifiedConnect;
		private byte[] m_buf = new byte[Connection.NET_BUF_SIZE];
		private byte[] m_iacbufL = new byte[Connection.NET_BUF_SIZE];

		/**
		 * What IAC request's have we received already ?
		 */
		private OptionState[] m_received = new OptionState[256];
		
		/**
		 * What IAC request's do we have sent already ?
		 */
		private OptionState[] m_sent = new OptionState[256];

		private TNACTION[] m_willAction = new TNACTION[256];
		private TNACTION[] m_wontAction = new TNACTION[256];
		private TNACTION[] m_doAction = new TNACTION[256];
		private TNACTION[] m_dontAction = new TNACTION[256];

		private TNACTION[] m_sbIsAction = new TNACTION[256];
		private TNACTION[] m_sbSendAction = new TNACTION[256];

		protected void DoActionTermType(TelnetFilter tn, byte option)
		{
			tn.SetWill((int)TelnetOptions.TN_TERM_TYPE);
		}

		protected void SbSendActionTermType(TelnetFilter tn, byte option)
		{
			string termname = tn.m_listener.TelnetGetTeminalName();
			tn.SendSB((byte)TelnetOptions.TN_TERM_TYPE, (byte)TelnetControl.TN_SB_IS, termname);
		}

		protected void DoActionNAWS(TelnetFilter tn, byte option)
		{
			int width, height;
			tn.m_listener.TelnetGetWindowSize(out width, out height);
			tn.SendIAC((byte)TelnetControl.TN_SB, (byte)TelnetOptions.TN_NAWS, 7, (byte)TelnetControl.TN_SB_IS, (byte)(width >> 8), (byte)(width & 0xFF), (byte)(height >> 8), (byte)(height & 0xFF), (byte)TelnetControl.TN_IAC, (byte)TelnetControl.TN_SE);
		}

		public TelnetFilter( ITelnetListener listener, IConnection conn )
		{
			for (int x = 0; x < m_received.Length; x++)
			{
				m_received[x] = new OptionState();
				m_sent[x] = new OptionState();
			}
			m_termtype = new StringBuilder();
			m_conn = conn;
			m_termIdentified = false;
			m_notifiedConnect = false;

			m_listener = listener;
			m_state = TelnetCodeState.TERM_STATE_0;
			
			//m_willAction[TN_TERM_SPEED] = willActionTermSpeed;
			//m_wontAction[TN_TERM_SPEED] = wontActionTermSpeed;
			//m_willAction[TN_TERM_TYPE] = willActionTermType;
			m_doAction[(int)TelnetOptions.TN_TERM_TYPE] = new TNACTION(DoActionTermType);
			m_sbSendAction[(int)TelnetOptions.TN_TERM_TYPE] = new TNACTION(SbSendActionTermType);
			m_doAction[(int)TelnetOptions.TN_NAWS] = new TNACTION(DoActionNAWS);

			m_iacbuf[0] = (byte)TelnetControl.TN_IAC;

			//SetBinaryModeDo();
			//SetBinaryModeWill();
			//SetTermTypeDo();
			//SetTermSpeedDo();
			//SetDteDo();
			//SetNawsWill();
			//SetCrNegDo();
			//SetStatusDo();
			//SetLineModeDo();
			//SetLineModeWill();
			//SetEchoWill();
			//SetEchoDont();
			//SetStopGaWill();
			//setStopGaDo();	
		}

		public void Lock()
		{
			m_conn.Lock();
		}

		public void UnLock()
		{
			m_conn.UnLock();
		}

		public ITelnetListener Listener
		{
			get { return m_listener; }
		}

		protected int indexofch(byte[] data, byte ch)
		{
			for (int x = 0; x < data.Length; x++ )
			{
				if ( data[x] == ch )
				{
					return x;
				}
			}
			return -1;
		}

		protected int indexofchfrom(byte[] data, byte ch, int from)
		{
			for (int x = from; x < data.Length; x++)
			{
				if (data[x] == ch)
				{
					return x;
				}
			}
			return -1;
		}

		public void ProcessInput( byte[] data, int len )
		{
			int nextiac = indexofch(data, (byte)TelnetControl.TN_IAC);
			if (TelnetCodeState.TERM_STATE_0 == m_state && nextiac < 0)
			{
				m_listener.OnTelnetRecv( data, len );
				return;
			}
			for ( int x = 0; x < len; x++ )
			{
				byte b = data[x];
				switch ( m_state )
				{
					case TelnetCodeState.TERM_STATE_0:
					if ( (byte)TelnetControl.TN_IAC != b )
					{
						if ( 0 > (nextiac = indexofchfrom(data, (byte)TelnetControl.TN_IAC, nextiac+1)) )
						{
							nextiac = len;
						}
						Debug.Assert( nextiac - x > 0 );
						Array.Copy(data, x, m_buf, 0, nextiac - x);
						m_listener.OnTelnetRecv(m_buf, nextiac - x);
						x = nextiac;
						Debug.Assert(x == len || (byte)TelnetControl.TN_IAC == data[x]);
						break;
					}
					m_state = TelnetCodeState.TERM_STATE_IAC;
					break;
				case TelnetCodeState.TERM_STATE_IAC:
					switch ( b )
					{
					case (byte)TelnetControl.TN_IAC:
						m_buf[0] = data[x];
						m_listener.OnTelnetRecv(m_buf, 1);
						break;
					case (byte)TelnetControl.TN_SE:
						break;
					case (byte)TelnetControl.TN_DONT:
						m_state = TelnetCodeState.TERM_STATE_DONT;
						break;
					case (byte)TelnetControl.TN_DO:
						m_state = TelnetCodeState.TERM_STATE_DO;
						break;
					case (byte)TelnetControl.TN_WONT:
						m_state = TelnetCodeState.TERM_STATE_WONT;
						break;
					case (byte)TelnetControl.TN_WILL:
						m_state = TelnetCodeState.TERM_STATE_WILL;
						break;
					case (byte)TelnetControl.TN_SB:		// SuBnegotiation option
						m_state = TelnetCodeState.TERM_STATE_SB;
						break;
					case (byte)TelnetControl.TN_DATAMARK:
					case (byte)TelnetControl.TN_BREAK:
					case (byte)TelnetControl.TN_IP:		// interrupt
					case (byte)TelnetControl.TN_AO:		// abort output
					case (byte)TelnetControl.TN_PING:	// are you there
					case (byte)TelnetControl.TN_EC:		// erase char
					case (byte)TelnetControl.TN_EL:		// erase line
					case (byte)TelnetControl.TN_GA:		// go ahead
						m_state = TelnetCodeState.TERM_STATE_0;
						break;
					default:
						LogFile.SysWriteInfo("Unknown IAC command of " + (short)data[x]);
						break;
					}
					break;
				case TelnetCodeState.TERM_STATE_DO:
					// client requests server to start doing something.
					// should respond with WILL or WONT
					LogFile.SysWriteInfo("DO " + GetTelnetOptionName(b));
					m_received[b].SetDO();
					if ( null != m_doAction[b] )
					{
						m_doAction[b](this, b);
					}
					else
					{
						m_listener.OnTelnetUnmappedOption((int)TelnetControl.TN_DO, (int)b);
					}
					m_state = TelnetCodeState.TERM_STATE_0;
					break;
				case TelnetCodeState.TERM_STATE_DONT:
					// client requests server to stop doing something
					// should not repsond
					LogFile.SysWriteInfo("DONT " + GetTelnetOptionName(b));
					m_received[b].SetDONT();
					if ( null != m_dontAction[b] )
					{
						m_dontAction[b](this, b);
					}
					else
					{
						m_listener.OnTelnetUnmappedOption((int)TelnetControl.TN_DONT, (int)b);
					}
					m_state = TelnetCodeState.TERM_STATE_0;
					break;
				case TelnetCodeState.TERM_STATE_WILL:
					// client notifies server it will do something.
					// Resond with DO to do it or DONT to not do it.
					LogFile.SysWriteInfo("WILL " + GetTelnetOptionName(b));
					m_received[b].SetWILL();
					if ( null != m_willAction[b] )
					{
						m_willAction[b](this, b);
					}
					else
					{
						m_listener.OnTelnetUnmappedOption((int)TelnetControl.TN_WILL, (int)b);
					}
					m_state = TelnetCodeState.TERM_STATE_0;
					break;
				case TelnetCodeState.TERM_STATE_WONT:
					// client notifies server it wont do something.
					// no response.
					LogFile.SysWriteInfo("WONT " + GetTelnetOptionName(b));
					m_received[b].SetWONT();
					if ( null != m_wontAction[b] )
					{
						m_wontAction[b](this, b);
					}
					else
					{
						m_listener.OnTelnetUnmappedOption((int)TelnetControl.TN_WONT, (int)b);
					}
					m_state = TelnetCodeState.TERM_STATE_0;
					break;
				case TelnetCodeState.TERM_STATE_SB:
					switch ( data[x] )
					{
					case (byte)TelnetControl.TN_IAC:
						m_state = TelnetCodeState.TERM_STATE_IAC;
						break;
					case (byte)TelnetOptions.TN_TERM_TYPE:
						m_state = TelnetCodeState.TERM_STATE_SB_TERM_TYPE;
						break;
					default:
						LogFile.SysWriteInfo("TelnetFilter: SB not handled (" + GetTelnetOptionName(data[x]) + ")");
						m_listener.OnTelnetUnmappedOption((int)TelnetControl.TN_SB, data[x]);
						break;
					}
					break;
				case TelnetCodeState.TERM_STATE_SB_TERM_TYPE:
					if( (byte)TelnetControl.TN_SB_IS == b)
					{
						m_state = TelnetCodeState.TERM_STATE_SB_TERM_TYPE_IS;
					}
					else if( (byte)TelnetControl.TN_SB_SEND == b )
					{
						m_state = TelnetCodeState.TERM_STATE_SB_TERM_TYPE_SEND;
					}
					else
					{
						LogFile.SysWriteWarn("TelnetFilter: IAC SB TERM expected IS or SEND (" + b + ")\n");
					}
					break;
				case TelnetCodeState.TERM_STATE_SB_TERM_TYPE_IS:
					if ((byte)TelnetControl.TN_IAC == b)
					{
						// no term name
						if (null != m_sbIsAction[(int)TelnetOptions.TN_TERM_TYPE])
						{
							m_sbIsAction[(int)TelnetOptions.TN_TERM_TYPE](this, 0);
						}
					}
					else if ((byte)TelnetControl.TN_SE == b)
					{
						m_state = TelnetCodeState.TERM_STATE_0;
						if ( null != m_sbIsAction[(int)TelnetOptions.TN_TERM_TYPE] )
						{
							m_sbIsAction[(int)TelnetOptions.TN_TERM_TYPE](this, 0);
						}
					}
					else
					{
						m_termtype.Append((char)b);
					}
					break;
				case TelnetCodeState.TERM_STATE_SB_TERM_TYPE_SEND:
					if ((byte)TelnetControl.TN_IAC == b)
					{
						m_state = TelnetCodeState.TERM_STATE_SB_SE;
						if (null != m_sbSendAction[(int)TelnetOptions.TN_TERM_TYPE])
						{
							m_sbSendAction[(int)TelnetOptions.TN_TERM_TYPE](this, 0);
						}
					}
					else
					{
						LogFile.SysWriteError("TelnetFilter: IAC SB TERM-TYPE expected IAC (" + b + ")\n");
					}
					break;
				case TelnetCodeState.TERM_STATE_SB_SE:
					if ((int)TelnetControl.TN_SE != b)
					{
						LogFile.SysWriteError("TelnetFilter: IAC SB ... IAC expected SE (" + b + ")\n");
					}
					m_state = TelnetCodeState.TERM_STATE_0;
					break;
				default:
					LogFile.SysWriteError("TelnetFilter: Unknown state (" + m_state + ")");
					break;
				}
			}
			if ( ! m_notifiedConnect )
			{
				m_notifiedConnect = true;
				m_listener.OnTelnetConnect();
			}
		/*	if ( m_negotiating && m_negotResponsesRemaining <= 0 )
			{
				if ( m_termtype.Length() > 0 )
				{
					m_negotiating = false;
					DumpTermFPrint();
					IndentifyTermCaps();
					m_listener.OnTelnetNegotationComplete();
				}
				else if ( m_receivedWX[TN_NAWS].IsHigh() && m_receivedDX[TN_NAWS].IsLow() )
				{
					// identify dos terminal
					m_negotResponsesRemaining = 1;
				}
			}*/
		}

		protected void IndentifyTermCaps()
		{
			string term = m_termtype.ToString().ToLower();
			if ( term == "ansi" && m_received[(int)TelnetOptions.TN_NAWS].IsWILL() )
			{
				if ( TermCap.HasTerminalDef( "pcansi" /*"ansi.sys"*/) )
				{
					m_caps.SetTerm( "pcansi" );
				}
				else
				{
					m_caps.SetTerm( "ansi" );
				}
			}
			else
			{
				if ( TermCap.HasTerminalDef(term) )
				{
					m_caps.SetTerm( term );
				}
				else
				{
					m_caps.SetTerm( "ansi" );
				}
			}

			string sb = null;
			if (m_caps.GetCapString(TemCapCode.TCAP_ti, ref sb))
			{
				m_conn.Send(sb);
			}
			if (m_caps.GetCapString(TemCapCode.TCAP_is, ref sb))
			{
				m_conn.Send(sb);
			}
			m_termIdentified = true;
		}

		protected void DumpTermFPrint()
		{
			LogFile.SysWriteInfo("TERM_TYPE=" + m_termtype.ToString());

			for ( int x = 0; x < 255; x++ )
			{
				if ( m_received[x].IsSet() )
				{
					LogFile.SysWriteInfo(m_received[x].ToString() + "=" + GetTelnetOptionName(x));
				}
			}
		}

		public bool SetWill( int command )
		{
			m_sent[command].SetWILL();
			m_iacbuf[1] = (byte)TelnetControl.TN_WILL;
			m_iacbuf[2] = (byte)command;
			return m_conn.Send(m_iacbuf, 3);
		}

		public bool SetWont( int command )
		{
			m_sent[command].SetWONT();
			m_iacbuf[1] = (byte)TelnetControl.TN_WONT;
			m_iacbuf[2] = (byte)command;
			return m_conn.Send(m_iacbuf, 3);
		}

		public bool SetDo( int command )
		{
			m_sent[command].SetDO();
			m_iacbuf[1] = (byte)TelnetControl.TN_DO;
			m_iacbuf[2] = (byte)command;
			return m_conn.Send(m_iacbuf, 3);
		}

		public bool SetDont( int command )
		{
			m_sent[command].SetDONT();
			m_iacbuf[1] = (byte)TelnetControl.TN_DONT;
			m_iacbuf[2] = (byte)command;
			return m_conn.Send(m_iacbuf, 3);
		}

		public bool SendIAC(byte command, byte option, params byte[] b)
		{
			m_iacbufL[0] = (byte)TelnetControl.TN_IAC;
			m_iacbufL[1] = command;
			m_iacbufL[2] = option;
			for (int x = 0; x < b.Length; x++)
			{
				m_iacbufL[x+3] = b[x];
			}
			return m_conn.Send(m_iacbufL, b.Length + 3);
		}

		public bool SendSB(byte option, byte isOrSend, string txt)
		{
			Debug.Assert(txt.Length < m_iacbufL.Length);
			int pos = 0;

			m_iacbufL[pos++] = (byte)TelnetControl.TN_IAC;
			m_iacbufL[pos++] = (byte)TelnetControl.TN_SB;
			m_iacbufL[pos++] = option;
			m_iacbufL[pos++] = isOrSend;
			for (int x = 0; x < txt.Length; x++)
			{
				m_iacbufL[pos++] = (byte)txt[x];
			}
			m_iacbufL[pos++] = (byte)TelnetControl.TN_IAC;
			m_iacbufL[pos++] = (byte)TelnetControl.TN_SE;
			return m_conn.Send(m_iacbufL, pos);
		}

		public bool SendSB(byte option, byte isOrSend, StringBuilder txt)
		{
			Debug.Assert(txt.Length < m_iacbufL.Length);
			int pos = 0;

			m_iacbufL[pos++] = (byte)TelnetControl.TN_IAC;
			m_iacbufL[pos++] = (byte)TelnetControl.TN_SB;
			m_iacbufL[pos++] = option;
			m_iacbufL[pos++] = isOrSend;
			for (int x = 0; x < txt.Length; x++)
			{
				m_iacbufL[pos++] = (byte)txt[x];
			}
			m_iacbufL[pos++] = (byte)TelnetControl.TN_IAC;
			m_iacbufL[pos++] = (byte)TelnetControl.TN_SE;
			return m_conn.Send(m_iacbufL, pos);
		}

		public bool SendSB(byte option, byte isOrSend, byte[] txt, int len)
		{
			Debug.Assert(txt.Length < m_iacbufL.Length);
			int pos = 0;

			m_iacbufL[pos++] = (byte)TelnetControl.TN_IAC;
			m_iacbufL[pos++] = (byte)TelnetControl.TN_SB;
			m_iacbufL[pos++] = option;
			m_iacbufL[pos++] = isOrSend;
			for ( int x = 0; x < len; x++ )
			{
				m_iacbufL[pos++] = txt[x];
			}
			m_iacbufL[pos++] = (byte)TelnetControl.TN_IAC;
			m_iacbufL[pos++] = (byte)TelnetControl.TN_SE;
			return m_conn.Send(m_iacbufL, pos);
		}

		public void SetDoAction( TNACTION fn, byte option ) { m_doAction[option] = fn; }
		public void SetDontAction( TNACTION fn, byte option ) { m_dontAction[option] = fn; }
		public void SetWillAction( TNACTION fn, byte option ) { m_willAction[option] = fn; }
		public void SetWontAction(TNACTION fn, byte option) { m_wontAction[option] = fn; }

		public void SetBinaryModeWill() { SetWill((byte)TelnetOptions.TN_BINARY); }
		public void SetBinaryModeWont() { SetWont((byte)TelnetOptions.TN_BINARY); }
		public void SetBinaryModeDo() { SetDo((byte)TelnetOptions.TN_BINARY); }

		public void SetEchoWill() { SetWill((byte)TelnetOptions.TN_ECHO); }
		public void SetEchoWont() { SetWont((byte)TelnetOptions.TN_ECHO); }
		public void SetEchoDo() { SetDo((byte)TelnetOptions.TN_ECHO); }
		public void SetEchoDont() { SetDont((byte)TelnetOptions.TN_ECHO); }

		public void SetStopGaWill() { SetWill((byte)TelnetOptions.TN_STOP_GA); }
		public void SetStopGaWont() { SetWont((byte)TelnetOptions.TN_STOP_GA); }
		public void SetStopGaDo() { SetDo((byte)TelnetOptions.TN_STOP_GA); }
		public void SetStopGaDont() { SetDont((byte)TelnetOptions.TN_STOP_GA); }

		public void SetTermTypeDo() { SetDo((byte)TelnetOptions.TN_TERM_TYPE); }
		public void SetTermSpeedDo() { SetDo((byte)TelnetOptions.TN_TERM_SPEED); }
		public void SetDteDo() { SetDo((byte)TelnetOptions.TN_DTE); }
		public void SetNawsWill() { SetWill((byte)TelnetOptions.TN_NAWS); }
		public void SetStatusDo() { SetDo((byte)TelnetOptions.TN_STATUS); }
		public void SetCrNegDo() { SetWill((byte)TelnetOptions.TN_NAOCRD); }
		public void SetLineModeWill() { SetWill((byte)TelnetOptions.TN_LINE_MODE); }
		public void SetLineModeDo() { SetDo((byte)TelnetOptions.TN_LINE_MODE); }

		public string TerminalName() { return m_termtype.ToString(); }

		public TermCap GetTermCaps() { if(! m_termIdentified ) IndentifyTermCaps(); return m_caps; }
		public IConnection GetConnection() { return m_conn; }

		protected string GetTelnetOptionName(int opt)
		{
			if ( opt > 49 )
			{
				if ( opt == 255 )
				{
					return "Extended-Options-List                               [RFC861]";
				}
				return "Unassigned					                        [IANA]";
			}
			return m_telnetOptionNames[opt];
		}

		static private string[] m_telnetOptionNames = {
		   /*0     */"Binary Transmission                                 [RFC856]",
		   /*1     */"Echo                                                [RFC857]",
		   /*2     */"Reconnection                                      [NIC50005]",
		   /*3     */"Suppress Go Ahead                                   [RFC858]",
		   /*4     */"Approx Message Size Negotiation                   [ETHERNET]",
		   /*5     */"Status                                              [RFC859]",
		   /*6     */"Timing Mark                                         [RFC860]",
		   /*7     */"Remote Controlled Trans and Echo                    [RFC726]",
		   /*8     */"Output Line Width                                 [NIC50005]",
		   /*9     */"Output Page Size                                  [NIC50005]",
		  /*10     */"Output Carriage-Return Disposition                  [RFC652]",
		  /*11     */"Output Horizontal Tab Stops                         [RFC653]",
		  /*12     */"Output Horizontal Tab Disposition                   [RFC654]",
		  /*13     */"Output Formfeed Disposition                         [RFC655]",
		  /*14     */"Output Vertical Tabstops                            [RFC656]",
		  /*15     */"Output Vertical Tab Disposition                     [RFC657]",
		  /*16     */"Output Linefeed Disposition                         [RFC658]",
		  /*17     */"Extended ASCII                                      [RFC698]",
		  /*18     */"Logout                                              [RFC727]",
		  /*19     */"Byte Macro                                          [RFC735]",
		  /*20     */"Data Entry Terminal                         [RFC1043,RFC732]",
		  /*21     */"SUPDUP                                       [RFC736,RFC734]",
		  /*22     */"SUPDUP Output                                       [RFC749]",
		  /*23     */"Send Location                                       [RFC779]",
		  /*24     */"Terminal Type                                      [RFC1091]",
		  /*25     */"End of Record                                       [RFC885]",
		  /*26     */"TACACS User Identification                          [RFC927]",
		  /*27     */"Output Marking                                      [RFC933]",
		  /*28     */"Terminal Location Number                            [RFC946]",
		  /*29     */"Telnet 3270 Regime                                 [RFC1041]",
		  /*30     */"X.3 PAD                                            [RFC1053]",
		  /*31     */"Negotiate About Window Size                        [RFC1073]",
		  /*32     */"Terminal Speed                                     [RFC1079]",
		  /*33     */"Remote Flow Control                                [RFC1372]",
		  /*34     */"Linemode                                           [RFC1184]",
		  /*35     */"X Display Location                                 [RFC1096]",
		  /*36     */"Environment Option                                 [RFC1408]",
		  /*37     */"Authentication Option                              [RFC2941]",
		  /*38     */"Encryption Option                                  [RFC2946]",
		  /*39     */"New Environment Option                             [RFC1572]",
		  /*40     */"TN3270E                                            [RFC1647]",
		  /*41     */"XAUTH                                              [Earhart]",
		  /*42     */"CHARSET                                            [RFC2066]",
		  /*43	  */"Telnet Remote Serial Port (RSP)                      [Barnes]",
		  /*44     */"Com Port Control Option                            [RFC2217]",
		  /*45     */"Telnet Suppress Local Echo                           [Atmar]",
		  /*46     */"Telnet Start TLS                                       [Boe]",
		  /*47     */"KERMIT                                             [RFC2840]",
		  /*48     */"SEND-URL                                             [Croft]",
		  /*49	   */"FORWARD_X					                          [Altman]",
		};
	};

	public class TelnetConnection : ThreadedConnection
	{
		protected TelnetFilter m_tnet;

		public TelnetConnection( ITelnetListener listener, Socket sp )
			: base(sp)
		{
			m_tnet = new TelnetFilter(listener, this);
		}

		public override void OnOpen()
		{
		}

		public override void OnClose()
		{
			m_tnet.Listener.OnTelnetClose();
		}

		public override void OnError(Exception se)
		{
			m_tnet.Listener.OnTelnetError(se);
		}

		public override void OnRecv(byte[] buf, int len)
		{
			m_tnet.ProcessInput(buf, len);
		}
	};
}
