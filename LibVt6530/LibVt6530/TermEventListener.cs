using System;
using System.Collections.Generic;
using System.Text;

namespace LibVt6530
{
	public interface TermEventListener
	{
		/**
		 *  The terminal has successfully connected
		 *  to the host.
		 */
		void TermOnConnect();
		
		/**
		 *  The connection to the host was lost or
		 *  closed.
		 */
		void TermOnDisconnect();
		
		/**
		 *  The host has completed rendering the
		 *  screen and is now waiting for input.
		 */
		void TermOnEnquire();

		/**
		 * Host has request line reset.  Usually, when a block mode
		 * screen has finished rendering, the host sends UNLOCK KB,
		 * RESET LINE, and then ENQUIRE.  For some reason, this sequence 
		 * is often sent twice.
		 */
		void TermOnResetLine();

		/**
		 *  Changes in the display require the container
		 *  to repaint.
		 */
		void TermOnDisplayChanged();
		
		/**
		 *  There has been an internal error.
		 */
		void TermOnError(string message);
		
		/**
		 *  Debuging output -- may be ignored
		 */
		void TermOnDebug(string message);

		void TermOnTextWatch(string txt, int commandCode);

		void TermOnUnlockKeyboard();
		void TermOnLockKeyboard();
	};
}
