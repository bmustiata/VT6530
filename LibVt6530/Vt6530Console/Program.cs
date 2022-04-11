using System;
using System.Collections.Generic;
using System.Text;

using Maximum;

namespace Vt6530Console
{
	struct Config
	{
		public string serverIp;
		public int port;
		public string termname;
	};

	public class Program
	{
		static void printUsage()
		{
			Console.WriteLine("usage: vt6530 [-s serverip] [-p serverport] [-t termname (pcansi|ansi|cygwin|vt100)]");
		}

		static bool	parserArgs(ref Config config, string[] argv)
		{
			for ( int x = 0; x < argv.Length; x+=2 )
			{
				if ( argv[x][0] != '-' )
				{
					printUsage();
					return false;
				}
				switch ( argv[x][1] )
				{
				case 's':
					config.serverIp = argv[x+1];
					break;
				case 'p':
					config.port = Int32.Parse(argv[x+1]);
					break;
				case 't':
					config.termname = argv[x+1];
					break;
				default:
					printUsage();
					return false;
				}
			}
			return true;
		}

		static void Main(string[] args)
		{
			Config config;
			config.port = 1016;
			config.serverIp = "192.209.32.3";
			config.termname =  "pcansi";

			//Log.Init("vt6530_log.txt", LogLevel.INFOS);

			if ( args.Length > 0 )
			{
				if ( args.Length != 2 && args.Length != 4 && args.Length != 6 )
				{
					printUsage();
					return;
				}
				if ( !parserArgs(ref config, args) )
				{
					return;
				}
			}

			VtConsole con = new VtConsole(config.serverIp, config.port, config.termname);
			con.Join();
		}
	}
}
