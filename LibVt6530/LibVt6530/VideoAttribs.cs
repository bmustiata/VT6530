using System;
using System.Collections.Generic;
using System.Text;

namespace LibVt6530
{
	public enum VideoAttribs
	{
		MASK_CHAR = (0xFF),

		/**
		 *  VIDEO attributes
		 */
		 VID_NORMAL	=	 (1<<8),
		 VID_BLINKING =	 (1<<9),
		 VID_REVERSE =	 (1<<10),
		 VID_INVIS =	 (1<<11),
		 VID_UNDERLINE = (1<<12),
		 MASK_VID =		 (VID_NORMAL|VID_BLINKING|VID_REVERSE|VID_INVIS|VID_UNDERLINE),

		/**
		 *  DATA attributes
		 */
		 DAT_MDT =		(1<<13),
		 DAT_TYPE =		((1<<14) | (1<<15) | (1<<16)),
		 DAT_AUTOTAB =  (1<<17),
		 DAT_UNPROTECT =  (1<<18),
		 SHIFT_DAT_TYPE =  14,

		/**
		 *  KEY attributes
		 */
		 KEY_UPSHIFT =	 (1<<19),
		 KEY_KB_ONLY =	 (1<<20),
		 KEY_AID_ONLY =	 (1<<21),
		 KEY_EITHER	=	 (1<<22),

		/**
		 *  SPECIAL attributes
		 */
		CHAR_START_FIELD = (1<<23),
		CHAR_CELL_DIRTY = (1<<24),
		MASK_FIELD = ((1<<8)|(1<<9)|(1<<10)|(1<<11)|(1<<12)|(1<<13)|(1<<14)|(1<<15)|(1<<16)|(1<<17)|(1<<18)|(1<<19)|(1<<20)|(1<<21)|(1<<22)|(1<<23)|(1<<25)|(1<<26)|(1<<27)),

		/**
		 *  Color bits
		 */
		MASK_COLOR = ((1<<25)|(1<<26)|(1<<27)),
		SHIFT_COLOR = 25,

		/**
		 *  Insert mode 
		 */
		INSERT_INSERT = 0,
		INSERT_OVERWRITE = 1
	};
}
