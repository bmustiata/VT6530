package gov.revenue.vt6530.ui;

/**
 *  Tandem cell attribute codes
 */
public interface Attributes
{
	static final int MASK_CHAR = 0xFF;

	/**
	 *  VIDEO attributes
	 */
	final static int VID_NORMAL		= (1<<8);
	final static int VID_BLINKING	= (1<<9);
	final static int VID_REVERSE	= (1<<10);
	final static int VID_INVIS		= (1<<11);
	final static int VID_UNDERLINE	= (1<<12);
	final static int MASK_VID = (VID_NORMAL|VID_BLINKING|VID_REVERSE|VID_INVIS|VID_UNDERLINE);

	/**
	 *  DATA attributes
	 */
	final static int DAT_MDT	 = (1<<13);
	final static int DAT_TYPE	 = ((1<<14) | (1<<15) | (1<<16));
	final static int DAT_AUTOTAB = (1<<17);
	final static int DAT_UNPROTECT = (1<<18);
	final static int SHIFT_DAT_TYPE = 14;

	/**
	 *  KEY attributes
	 */
	final static int KEY_UPSHIFT	= (1<<19);
	final static int KEY_KB_ONLY	= (1<<20);
	final static int KEY_AID_ONLY	= (1<<21);
	final static int KEY_EITHER		= (1<<22);

	/**
	 *  SPECIAL attributes used by TextDisplay
	 */
	final static int CHAR_START_FIELD = (1<<23);
	final static int CHAR_CELL_DIRTY = (1<<24);
	final static int MASK_FIELD = ((1<<8)|(1<<9)|(1<<10)|(1<<11)|(1<<12)|(1<<13)|(1<<14)|(1<<15)|(1<<16)|(1<<17)|(1<<18)|(1<<19)|(1<<20)|(1<<21)|(1<<22)|(1<<23)|(1<<25)|(1<<26)|(1<<27));

	/**
	 *  Color bits
	 */
	final static int MASK_COLOR = ((1<<25)|(1<<26)|(1<<27));
	final static int SHIFT_COLOR = 25;

	/**
	 *  Insert mode
	 */
	final static int INSERT_INSERT = 0;
	final static int INSERT_OVERWRITE = 1;

}
