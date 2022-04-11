/* this file contains the actual definitions of */
/* the IIDs and CLSIDs */

/* link this file in with the server and any clients */


/* File created by MIDL compiler version 5.01.0164 */
/* at Wed Aug 30 11:49:23 2000
 */
/* Compiler settings for D:\Sources\vt6530\CPP\Vt6530_Terminal_Proj.idl:
    Oicf (OptLev=i2), W1, Zp8, env=Win32, ms_ext, c_ext
    error checks: allocation ref bounds_check enum stub_data 
*/
//@@MIDL_FILE_HEADING(  )
#ifdef __cplusplus
extern "C"{
#endif 


#ifndef __IID_DEFINED__
#define __IID_DEFINED__

typedef struct _IID
{
    unsigned long x;
    unsigned short s1;
    unsigned short s2;
    unsigned char  c[8];
} IID;

#endif // __IID_DEFINED__

#ifndef CLSID_DEFINED
#define CLSID_DEFINED
typedef IID CLSID;
#endif // CLSID_DEFINED

const IID IID_IVt6530Control = {0xEDA1E56F,0x6F0F,0x11D4,{0x98,0xC6,0x00,0x01,0x02,0x49,0x47,0x81}};


const IID LIBID_VT6530_TERMINAL_PROJLib = {0xEDA1E561,0x6F0F,0x11D4,{0x98,0xC6,0x00,0x01,0x02,0x49,0x47,0x81}};


const IID DIID__IVt5630ControlEvents = {0xEDA1E573,0x6F0F,0x11D4,{0x98,0xC6,0x00,0x01,0x02,0x49,0x47,0x81}};


const IID IID_IVt6530 = {0x507CA441,0x785A,0x11D4,{0x98,0xD2,0x00,0x01,0x02,0x49,0x47,0x81}};


const CLSID CLSID_Vt6530Control = {0xEDA1E572,0x6F0F,0x11D4,{0x98,0xC6,0x00,0x01,0x02,0x49,0x47,0x81}};


const CLSID CLSID_Vt6530 = {0x507CA442,0x785A,0x11D4,{0x98,0xD2,0x00,0x01,0x02,0x49,0x47,0x81}};


#ifdef __cplusplus
}
#endif

