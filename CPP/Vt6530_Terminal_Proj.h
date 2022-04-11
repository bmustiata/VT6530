/* this ALWAYS GENERATED file contains the definitions for the interfaces */


/* File created by MIDL compiler version 5.01.0164 */
/* at Wed Aug 30 11:49:23 2000
 */
/* Compiler settings for D:\Sources\vt6530\CPP\Vt6530_Terminal_Proj.idl:
    Oicf (OptLev=i2), W1, Zp8, env=Win32, ms_ext, c_ext
    error checks: allocation ref bounds_check enum stub_data 
*/
//@@MIDL_FILE_HEADING(  )


/* verify that the <rpcndr.h> version is high enough to compile this file*/
#ifndef __REQUIRED_RPCNDR_H_VERSION__
#define __REQUIRED_RPCNDR_H_VERSION__ 440
#endif

#include "rpc.h"
#include "rpcndr.h"

#ifndef __RPCNDR_H_VERSION__
#error this stub requires an updated version of <rpcndr.h>
#endif // __RPCNDR_H_VERSION__

#ifndef COM_NO_WINDOWS_H
#include "windows.h"
#include "ole2.h"
#endif /*COM_NO_WINDOWS_H*/

#ifndef __Vt6530_Terminal_Proj_h__
#define __Vt6530_Terminal_Proj_h__

#ifdef __cplusplus
extern "C"{
#endif 

/* Forward Declarations */ 

#ifndef __IVt6530Control_FWD_DEFINED__
#define __IVt6530Control_FWD_DEFINED__
typedef interface IVt6530Control IVt6530Control;
#endif 	/* __IVt6530Control_FWD_DEFINED__ */


#ifndef ___IVt5630ControlEvents_FWD_DEFINED__
#define ___IVt5630ControlEvents_FWD_DEFINED__
typedef interface _IVt5630ControlEvents _IVt5630ControlEvents;
#endif 	/* ___IVt5630ControlEvents_FWD_DEFINED__ */


#ifndef __IVt6530_FWD_DEFINED__
#define __IVt6530_FWD_DEFINED__
typedef interface IVt6530 IVt6530;
#endif 	/* __IVt6530_FWD_DEFINED__ */


#ifndef __Vt6530Control_FWD_DEFINED__
#define __Vt6530Control_FWD_DEFINED__

#ifdef __cplusplus
typedef class Vt6530Control Vt6530Control;
#else
typedef struct Vt6530Control Vt6530Control;
#endif /* __cplusplus */

#endif 	/* __Vt6530Control_FWD_DEFINED__ */


#ifndef __Vt6530_FWD_DEFINED__
#define __Vt6530_FWD_DEFINED__

#ifdef __cplusplus
typedef class Vt6530 Vt6530;
#else
typedef struct Vt6530 Vt6530;
#endif /* __cplusplus */

#endif 	/* __Vt6530_FWD_DEFINED__ */


/* header files for imported files */
#include "oaidl.h"
#include "ocidl.h"

void __RPC_FAR * __RPC_USER MIDL_user_allocate(size_t);
void __RPC_USER MIDL_user_free( void __RPC_FAR * ); 

#ifndef __IVt6530Control_INTERFACE_DEFINED__
#define __IVt6530Control_INTERFACE_DEFINED__

/* interface IVt6530Control */
/* [unique][helpstring][dual][uuid][object] */ 


EXTERN_C const IID IID_IVt6530Control;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("EDA1E56F-6F0F-11D4-98C6-000102494781")
    IVt6530Control : public IDispatch
    {
    public:
        virtual /* [id][propput] */ HRESULT STDMETHODCALLTYPE put_BackColor( 
            /* [in] */ OLE_COLOR clr) = 0;
        
        virtual /* [id][propget] */ HRESULT STDMETHODCALLTYPE get_BackColor( 
            /* [retval][out] */ OLE_COLOR __RPC_FAR *pclr) = 0;
        
        virtual /* [id][propput] */ HRESULT STDMETHODCALLTYPE put_BorderColor( 
            /* [in] */ OLE_COLOR clr) = 0;
        
        virtual /* [id][propget] */ HRESULT STDMETHODCALLTYPE get_BorderColor( 
            /* [retval][out] */ OLE_COLOR __RPC_FAR *pclr) = 0;
        
        virtual /* [id][propput] */ HRESULT STDMETHODCALLTYPE put_BorderWidth( 
            /* [in] */ long width) = 0;
        
        virtual /* [id][propget] */ HRESULT STDMETHODCALLTYPE get_BorderWidth( 
            /* [retval][out] */ long __RPC_FAR *width) = 0;
        
        virtual /* [id][propput] */ HRESULT STDMETHODCALLTYPE put_ForeColor( 
            /* [in] */ OLE_COLOR clr) = 0;
        
        virtual /* [id][propget] */ HRESULT STDMETHODCALLTYPE get_ForeColor( 
            /* [retval][out] */ OLE_COLOR __RPC_FAR *pclr) = 0;
        
        virtual /* [id][propput] */ HRESULT STDMETHODCALLTYPE put_BorderVisible( 
            /* [in] */ VARIANT_BOOL vbool) = 0;
        
        virtual /* [id][propget] */ HRESULT STDMETHODCALLTYPE get_BorderVisible( 
            /* [retval][out] */ VARIANT_BOOL __RPC_FAR *pbool) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE waitENQ( void) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_Host( 
            /* [retval][out] */ BSTR __RPC_FAR *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_Host( 
            /* [in] */ BSTR newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_Port( 
            /* [retval][out] */ short __RPC_FAR *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_Port( 
            /* [in] */ short newVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE Connect( 
            /* [in] */ int iTimeOut) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE Disconnect( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE getScreenDump( 
            /* [retval][out] */ BSTR __RPC_FAR *asciiScreenChars) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE getAttributeDump( 
            /* [retval][out] */ BSTR __RPC_FAR *asciiScreenAttrs) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_Field( 
            int index,
            /* [retval][out] */ BSTR __RPC_FAR *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_Field( 
            int index,
            /* [in] */ BSTR newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_FieldAttributes( 
            int index,
            /* [retval][out] */ int __RPC_FAR *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_FieldAttributes( 
            int index,
            /* [in] */ int newVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IsReverse( 
            /* [in] */ int iAttribute,
            /* [retval][out] */ BOOL __RPC_FAR *isIt) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IsInvis( 
            /* [in] */ int iAttribute,
            /* [retval][out] */ BOOL __RPC_FAR *isIt) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IsBlinking( 
            /* [in] */ int iAttribute,
            /* [retval][out] */ BOOL __RPC_FAR *isIt) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IsUnderLine( 
            /* [in] */ int iAttribute,
            /* [retval][out] */ BOOL __RPC_FAR *isIt) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IsUpshift( 
            /* [in] */ int iAttribute,
            /* [retval][out] */ BOOL __RPC_FAR *isIt) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE getRGB( 
            /* [in] */ int iAttribute,
            /* [retval][out] */ int __RPC_FAR *irgb) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_CurrentField( 
            /* [retval][out] */ BSTR __RPC_FAR *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_CurrentField( 
            /* [in] */ BSTR newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_UnprotectField( 
            /* [in] */ int iIndex,
            /* [retval][out] */ BSTR __RPC_FAR *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_UnprotectField( 
            /* [in] */ int iIndex,
            /* [in] */ BSTR newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_Line( 
            /* [in] */ int index,
            /* [retval][out] */ BSTR __RPC_FAR *pVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IsFieldChanged( 
            /* [in] */ int iIndex,
            /* [retval][out] */ BOOL __RPC_FAR *isIt) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE CursorToField( 
            /* [in] */ int iIndex) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_Row( 
            /* [retval][out] */ int __RPC_FAR *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_Row( 
            /* [in] */ int newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_Column( 
            /* [retval][out] */ int __RPC_FAR *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_Column( 
            /* [in] */ int newVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE SendCommandLine( 
            /* [in] */ BSTR sCommand) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE FakeKey( 
            /* [in] */ int iKeyCode,
            /* [in] */ BOOL bShift,
            /* [in] */ BOOL bAlt,
            /* [in] */ BOOL bCtrl) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE FakeKeys( 
            /* [in] */ BSTR sKeys) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE FakeF1( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE FakeF2( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE FakeF3( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE FakeF4( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE FakeF5( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE FakeF6( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE FakeF7( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE FakeF8( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE FakeF9( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE FakeF10( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE FakeF11( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE FakeF12( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE FakeF13( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE FakeF14( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE FakeF15( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE FakeF16( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE FakeEnter( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE FakeBackspace( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE FakeTab( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE FakeUpArrow( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE FakeDownArrow( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE FakeLeftArrow( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE FakeRightArrow( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE FakeHome( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE FakeEnd( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE FakeInsert( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE FakeDelete( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE toHTML( 
            /* [retval][out] */ BSTR __RPC_FAR *sHTML) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE getScreenNumber( 
            /* [retval][out] */ BSTR __RPC_FAR *sScreenNum) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE wait34( void) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct IVt6530ControlVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *QueryInterface )( 
            IVt6530Control __RPC_FAR * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ void __RPC_FAR *__RPC_FAR *ppvObject);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *AddRef )( 
            IVt6530Control __RPC_FAR * This);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *Release )( 
            IVt6530Control __RPC_FAR * This);
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *GetTypeInfoCount )( 
            IVt6530Control __RPC_FAR * This,
            /* [out] */ UINT __RPC_FAR *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *GetTypeInfo )( 
            IVt6530Control __RPC_FAR * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo __RPC_FAR *__RPC_FAR *ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *GetIDsOfNames )( 
            IVt6530Control __RPC_FAR * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR __RPC_FAR *rgszNames,
            /* [in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID __RPC_FAR *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *Invoke )( 
            IVt6530Control __RPC_FAR * This,
            /* [in] */ DISPID dispIdMember,
            /* [in] */ REFIID riid,
            /* [in] */ LCID lcid,
            /* [in] */ WORD wFlags,
            /* [out][in] */ DISPPARAMS __RPC_FAR *pDispParams,
            /* [out] */ VARIANT __RPC_FAR *pVarResult,
            /* [out] */ EXCEPINFO __RPC_FAR *pExcepInfo,
            /* [out] */ UINT __RPC_FAR *puArgErr);
        
        /* [id][propput] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *put_BackColor )( 
            IVt6530Control __RPC_FAR * This,
            /* [in] */ OLE_COLOR clr);
        
        /* [id][propget] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *get_BackColor )( 
            IVt6530Control __RPC_FAR * This,
            /* [retval][out] */ OLE_COLOR __RPC_FAR *pclr);
        
        /* [id][propput] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *put_BorderColor )( 
            IVt6530Control __RPC_FAR * This,
            /* [in] */ OLE_COLOR clr);
        
        /* [id][propget] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *get_BorderColor )( 
            IVt6530Control __RPC_FAR * This,
            /* [retval][out] */ OLE_COLOR __RPC_FAR *pclr);
        
        /* [id][propput] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *put_BorderWidth )( 
            IVt6530Control __RPC_FAR * This,
            /* [in] */ long width);
        
        /* [id][propget] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *get_BorderWidth )( 
            IVt6530Control __RPC_FAR * This,
            /* [retval][out] */ long __RPC_FAR *width);
        
        /* [id][propput] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *put_ForeColor )( 
            IVt6530Control __RPC_FAR * This,
            /* [in] */ OLE_COLOR clr);
        
        /* [id][propget] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *get_ForeColor )( 
            IVt6530Control __RPC_FAR * This,
            /* [retval][out] */ OLE_COLOR __RPC_FAR *pclr);
        
        /* [id][propput] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *put_BorderVisible )( 
            IVt6530Control __RPC_FAR * This,
            /* [in] */ VARIANT_BOOL vbool);
        
        /* [id][propget] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *get_BorderVisible )( 
            IVt6530Control __RPC_FAR * This,
            /* [retval][out] */ VARIANT_BOOL __RPC_FAR *pbool);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *waitENQ )( 
            IVt6530Control __RPC_FAR * This);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *get_Host )( 
            IVt6530Control __RPC_FAR * This,
            /* [retval][out] */ BSTR __RPC_FAR *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *put_Host )( 
            IVt6530Control __RPC_FAR * This,
            /* [in] */ BSTR newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *get_Port )( 
            IVt6530Control __RPC_FAR * This,
            /* [retval][out] */ short __RPC_FAR *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *put_Port )( 
            IVt6530Control __RPC_FAR * This,
            /* [in] */ short newVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *Connect )( 
            IVt6530Control __RPC_FAR * This,
            /* [in] */ int iTimeOut);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *Disconnect )( 
            IVt6530Control __RPC_FAR * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *getScreenDump )( 
            IVt6530Control __RPC_FAR * This,
            /* [retval][out] */ BSTR __RPC_FAR *asciiScreenChars);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *getAttributeDump )( 
            IVt6530Control __RPC_FAR * This,
            /* [retval][out] */ BSTR __RPC_FAR *asciiScreenAttrs);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *get_Field )( 
            IVt6530Control __RPC_FAR * This,
            int index,
            /* [retval][out] */ BSTR __RPC_FAR *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *put_Field )( 
            IVt6530Control __RPC_FAR * This,
            int index,
            /* [in] */ BSTR newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *get_FieldAttributes )( 
            IVt6530Control __RPC_FAR * This,
            int index,
            /* [retval][out] */ int __RPC_FAR *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *put_FieldAttributes )( 
            IVt6530Control __RPC_FAR * This,
            int index,
            /* [in] */ int newVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *IsReverse )( 
            IVt6530Control __RPC_FAR * This,
            /* [in] */ int iAttribute,
            /* [retval][out] */ BOOL __RPC_FAR *isIt);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *IsInvis )( 
            IVt6530Control __RPC_FAR * This,
            /* [in] */ int iAttribute,
            /* [retval][out] */ BOOL __RPC_FAR *isIt);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *IsBlinking )( 
            IVt6530Control __RPC_FAR * This,
            /* [in] */ int iAttribute,
            /* [retval][out] */ BOOL __RPC_FAR *isIt);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *IsUnderLine )( 
            IVt6530Control __RPC_FAR * This,
            /* [in] */ int iAttribute,
            /* [retval][out] */ BOOL __RPC_FAR *isIt);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *IsUpshift )( 
            IVt6530Control __RPC_FAR * This,
            /* [in] */ int iAttribute,
            /* [retval][out] */ BOOL __RPC_FAR *isIt);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *getRGB )( 
            IVt6530Control __RPC_FAR * This,
            /* [in] */ int iAttribute,
            /* [retval][out] */ int __RPC_FAR *irgb);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *get_CurrentField )( 
            IVt6530Control __RPC_FAR * This,
            /* [retval][out] */ BSTR __RPC_FAR *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *put_CurrentField )( 
            IVt6530Control __RPC_FAR * This,
            /* [in] */ BSTR newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *get_UnprotectField )( 
            IVt6530Control __RPC_FAR * This,
            /* [in] */ int iIndex,
            /* [retval][out] */ BSTR __RPC_FAR *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *put_UnprotectField )( 
            IVt6530Control __RPC_FAR * This,
            /* [in] */ int iIndex,
            /* [in] */ BSTR newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *get_Line )( 
            IVt6530Control __RPC_FAR * This,
            /* [in] */ int index,
            /* [retval][out] */ BSTR __RPC_FAR *pVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *IsFieldChanged )( 
            IVt6530Control __RPC_FAR * This,
            /* [in] */ int iIndex,
            /* [retval][out] */ BOOL __RPC_FAR *isIt);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *CursorToField )( 
            IVt6530Control __RPC_FAR * This,
            /* [in] */ int iIndex);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *get_Row )( 
            IVt6530Control __RPC_FAR * This,
            /* [retval][out] */ int __RPC_FAR *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *put_Row )( 
            IVt6530Control __RPC_FAR * This,
            /* [in] */ int newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *get_Column )( 
            IVt6530Control __RPC_FAR * This,
            /* [retval][out] */ int __RPC_FAR *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *put_Column )( 
            IVt6530Control __RPC_FAR * This,
            /* [in] */ int newVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *SendCommandLine )( 
            IVt6530Control __RPC_FAR * This,
            /* [in] */ BSTR sCommand);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *FakeKey )( 
            IVt6530Control __RPC_FAR * This,
            /* [in] */ int iKeyCode,
            /* [in] */ BOOL bShift,
            /* [in] */ BOOL bAlt,
            /* [in] */ BOOL bCtrl);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *FakeKeys )( 
            IVt6530Control __RPC_FAR * This,
            /* [in] */ BSTR sKeys);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *FakeF1 )( 
            IVt6530Control __RPC_FAR * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *FakeF2 )( 
            IVt6530Control __RPC_FAR * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *FakeF3 )( 
            IVt6530Control __RPC_FAR * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *FakeF4 )( 
            IVt6530Control __RPC_FAR * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *FakeF5 )( 
            IVt6530Control __RPC_FAR * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *FakeF6 )( 
            IVt6530Control __RPC_FAR * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *FakeF7 )( 
            IVt6530Control __RPC_FAR * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *FakeF8 )( 
            IVt6530Control __RPC_FAR * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *FakeF9 )( 
            IVt6530Control __RPC_FAR * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *FakeF10 )( 
            IVt6530Control __RPC_FAR * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *FakeF11 )( 
            IVt6530Control __RPC_FAR * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *FakeF12 )( 
            IVt6530Control __RPC_FAR * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *FakeF13 )( 
            IVt6530Control __RPC_FAR * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *FakeF14 )( 
            IVt6530Control __RPC_FAR * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *FakeF15 )( 
            IVt6530Control __RPC_FAR * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *FakeF16 )( 
            IVt6530Control __RPC_FAR * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *FakeEnter )( 
            IVt6530Control __RPC_FAR * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *FakeBackspace )( 
            IVt6530Control __RPC_FAR * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *FakeTab )( 
            IVt6530Control __RPC_FAR * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *FakeUpArrow )( 
            IVt6530Control __RPC_FAR * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *FakeDownArrow )( 
            IVt6530Control __RPC_FAR * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *FakeLeftArrow )( 
            IVt6530Control __RPC_FAR * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *FakeRightArrow )( 
            IVt6530Control __RPC_FAR * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *FakeHome )( 
            IVt6530Control __RPC_FAR * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *FakeEnd )( 
            IVt6530Control __RPC_FAR * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *FakeInsert )( 
            IVt6530Control __RPC_FAR * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *FakeDelete )( 
            IVt6530Control __RPC_FAR * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *toHTML )( 
            IVt6530Control __RPC_FAR * This,
            /* [retval][out] */ BSTR __RPC_FAR *sHTML);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *getScreenNumber )( 
            IVt6530Control __RPC_FAR * This,
            /* [retval][out] */ BSTR __RPC_FAR *sScreenNum);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *wait34 )( 
            IVt6530Control __RPC_FAR * This);
        
        END_INTERFACE
    } IVt6530ControlVtbl;

    interface IVt6530Control
    {
        CONST_VTBL struct IVt6530ControlVtbl __RPC_FAR *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IVt6530Control_QueryInterface(This,riid,ppvObject)	\
    (This)->lpVtbl -> QueryInterface(This,riid,ppvObject)

#define IVt6530Control_AddRef(This)	\
    (This)->lpVtbl -> AddRef(This)

#define IVt6530Control_Release(This)	\
    (This)->lpVtbl -> Release(This)


#define IVt6530Control_GetTypeInfoCount(This,pctinfo)	\
    (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo)

#define IVt6530Control_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo)

#define IVt6530Control_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)

#define IVt6530Control_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)


#define IVt6530Control_put_BackColor(This,clr)	\
    (This)->lpVtbl -> put_BackColor(This,clr)

#define IVt6530Control_get_BackColor(This,pclr)	\
    (This)->lpVtbl -> get_BackColor(This,pclr)

#define IVt6530Control_put_BorderColor(This,clr)	\
    (This)->lpVtbl -> put_BorderColor(This,clr)

#define IVt6530Control_get_BorderColor(This,pclr)	\
    (This)->lpVtbl -> get_BorderColor(This,pclr)

#define IVt6530Control_put_BorderWidth(This,width)	\
    (This)->lpVtbl -> put_BorderWidth(This,width)

#define IVt6530Control_get_BorderWidth(This,width)	\
    (This)->lpVtbl -> get_BorderWidth(This,width)

#define IVt6530Control_put_ForeColor(This,clr)	\
    (This)->lpVtbl -> put_ForeColor(This,clr)

#define IVt6530Control_get_ForeColor(This,pclr)	\
    (This)->lpVtbl -> get_ForeColor(This,pclr)

#define IVt6530Control_put_BorderVisible(This,vbool)	\
    (This)->lpVtbl -> put_BorderVisible(This,vbool)

#define IVt6530Control_get_BorderVisible(This,pbool)	\
    (This)->lpVtbl -> get_BorderVisible(This,pbool)

#define IVt6530Control_waitENQ(This)	\
    (This)->lpVtbl -> waitENQ(This)

#define IVt6530Control_get_Host(This,pVal)	\
    (This)->lpVtbl -> get_Host(This,pVal)

#define IVt6530Control_put_Host(This,newVal)	\
    (This)->lpVtbl -> put_Host(This,newVal)

#define IVt6530Control_get_Port(This,pVal)	\
    (This)->lpVtbl -> get_Port(This,pVal)

#define IVt6530Control_put_Port(This,newVal)	\
    (This)->lpVtbl -> put_Port(This,newVal)

#define IVt6530Control_Connect(This,iTimeOut)	\
    (This)->lpVtbl -> Connect(This,iTimeOut)

#define IVt6530Control_Disconnect(This)	\
    (This)->lpVtbl -> Disconnect(This)

#define IVt6530Control_getScreenDump(This,asciiScreenChars)	\
    (This)->lpVtbl -> getScreenDump(This,asciiScreenChars)

#define IVt6530Control_getAttributeDump(This,asciiScreenAttrs)	\
    (This)->lpVtbl -> getAttributeDump(This,asciiScreenAttrs)

#define IVt6530Control_get_Field(This,index,pVal)	\
    (This)->lpVtbl -> get_Field(This,index,pVal)

#define IVt6530Control_put_Field(This,index,newVal)	\
    (This)->lpVtbl -> put_Field(This,index,newVal)

#define IVt6530Control_get_FieldAttributes(This,index,pVal)	\
    (This)->lpVtbl -> get_FieldAttributes(This,index,pVal)

#define IVt6530Control_put_FieldAttributes(This,index,newVal)	\
    (This)->lpVtbl -> put_FieldAttributes(This,index,newVal)

#define IVt6530Control_IsReverse(This,iAttribute,isIt)	\
    (This)->lpVtbl -> IsReverse(This,iAttribute,isIt)

#define IVt6530Control_IsInvis(This,iAttribute,isIt)	\
    (This)->lpVtbl -> IsInvis(This,iAttribute,isIt)

#define IVt6530Control_IsBlinking(This,iAttribute,isIt)	\
    (This)->lpVtbl -> IsBlinking(This,iAttribute,isIt)

#define IVt6530Control_IsUnderLine(This,iAttribute,isIt)	\
    (This)->lpVtbl -> IsUnderLine(This,iAttribute,isIt)

#define IVt6530Control_IsUpshift(This,iAttribute,isIt)	\
    (This)->lpVtbl -> IsUpshift(This,iAttribute,isIt)

#define IVt6530Control_getRGB(This,iAttribute,irgb)	\
    (This)->lpVtbl -> getRGB(This,iAttribute,irgb)

#define IVt6530Control_get_CurrentField(This,pVal)	\
    (This)->lpVtbl -> get_CurrentField(This,pVal)

#define IVt6530Control_put_CurrentField(This,newVal)	\
    (This)->lpVtbl -> put_CurrentField(This,newVal)

#define IVt6530Control_get_UnprotectField(This,iIndex,pVal)	\
    (This)->lpVtbl -> get_UnprotectField(This,iIndex,pVal)

#define IVt6530Control_put_UnprotectField(This,iIndex,newVal)	\
    (This)->lpVtbl -> put_UnprotectField(This,iIndex,newVal)

#define IVt6530Control_get_Line(This,index,pVal)	\
    (This)->lpVtbl -> get_Line(This,index,pVal)

#define IVt6530Control_IsFieldChanged(This,iIndex,isIt)	\
    (This)->lpVtbl -> IsFieldChanged(This,iIndex,isIt)

#define IVt6530Control_CursorToField(This,iIndex)	\
    (This)->lpVtbl -> CursorToField(This,iIndex)

#define IVt6530Control_get_Row(This,pVal)	\
    (This)->lpVtbl -> get_Row(This,pVal)

#define IVt6530Control_put_Row(This,newVal)	\
    (This)->lpVtbl -> put_Row(This,newVal)

#define IVt6530Control_get_Column(This,pVal)	\
    (This)->lpVtbl -> get_Column(This,pVal)

#define IVt6530Control_put_Column(This,newVal)	\
    (This)->lpVtbl -> put_Column(This,newVal)

#define IVt6530Control_SendCommandLine(This,sCommand)	\
    (This)->lpVtbl -> SendCommandLine(This,sCommand)

#define IVt6530Control_FakeKey(This,iKeyCode,bShift,bAlt,bCtrl)	\
    (This)->lpVtbl -> FakeKey(This,iKeyCode,bShift,bAlt,bCtrl)

#define IVt6530Control_FakeKeys(This,sKeys)	\
    (This)->lpVtbl -> FakeKeys(This,sKeys)

#define IVt6530Control_FakeF1(This)	\
    (This)->lpVtbl -> FakeF1(This)

#define IVt6530Control_FakeF2(This)	\
    (This)->lpVtbl -> FakeF2(This)

#define IVt6530Control_FakeF3(This)	\
    (This)->lpVtbl -> FakeF3(This)

#define IVt6530Control_FakeF4(This)	\
    (This)->lpVtbl -> FakeF4(This)

#define IVt6530Control_FakeF5(This)	\
    (This)->lpVtbl -> FakeF5(This)

#define IVt6530Control_FakeF6(This)	\
    (This)->lpVtbl -> FakeF6(This)

#define IVt6530Control_FakeF7(This)	\
    (This)->lpVtbl -> FakeF7(This)

#define IVt6530Control_FakeF8(This)	\
    (This)->lpVtbl -> FakeF8(This)

#define IVt6530Control_FakeF9(This)	\
    (This)->lpVtbl -> FakeF9(This)

#define IVt6530Control_FakeF10(This)	\
    (This)->lpVtbl -> FakeF10(This)

#define IVt6530Control_FakeF11(This)	\
    (This)->lpVtbl -> FakeF11(This)

#define IVt6530Control_FakeF12(This)	\
    (This)->lpVtbl -> FakeF12(This)

#define IVt6530Control_FakeF13(This)	\
    (This)->lpVtbl -> FakeF13(This)

#define IVt6530Control_FakeF14(This)	\
    (This)->lpVtbl -> FakeF14(This)

#define IVt6530Control_FakeF15(This)	\
    (This)->lpVtbl -> FakeF15(This)

#define IVt6530Control_FakeF16(This)	\
    (This)->lpVtbl -> FakeF16(This)

#define IVt6530Control_FakeEnter(This)	\
    (This)->lpVtbl -> FakeEnter(This)

#define IVt6530Control_FakeBackspace(This)	\
    (This)->lpVtbl -> FakeBackspace(This)

#define IVt6530Control_FakeTab(This)	\
    (This)->lpVtbl -> FakeTab(This)

#define IVt6530Control_FakeUpArrow(This)	\
    (This)->lpVtbl -> FakeUpArrow(This)

#define IVt6530Control_FakeDownArrow(This)	\
    (This)->lpVtbl -> FakeDownArrow(This)

#define IVt6530Control_FakeLeftArrow(This)	\
    (This)->lpVtbl -> FakeLeftArrow(This)

#define IVt6530Control_FakeRightArrow(This)	\
    (This)->lpVtbl -> FakeRightArrow(This)

#define IVt6530Control_FakeHome(This)	\
    (This)->lpVtbl -> FakeHome(This)

#define IVt6530Control_FakeEnd(This)	\
    (This)->lpVtbl -> FakeEnd(This)

#define IVt6530Control_FakeInsert(This)	\
    (This)->lpVtbl -> FakeInsert(This)

#define IVt6530Control_FakeDelete(This)	\
    (This)->lpVtbl -> FakeDelete(This)

#define IVt6530Control_toHTML(This,sHTML)	\
    (This)->lpVtbl -> toHTML(This,sHTML)

#define IVt6530Control_getScreenNumber(This,sScreenNum)	\
    (This)->lpVtbl -> getScreenNumber(This,sScreenNum)

#define IVt6530Control_wait34(This)	\
    (This)->lpVtbl -> wait34(This)

#endif /* COBJMACROS */


#endif 	/* C style interface */



/* [id][propput] */ HRESULT STDMETHODCALLTYPE IVt6530Control_put_BackColor_Proxy( 
    IVt6530Control __RPC_FAR * This,
    /* [in] */ OLE_COLOR clr);


void __RPC_STUB IVt6530Control_put_BackColor_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [id][propget] */ HRESULT STDMETHODCALLTYPE IVt6530Control_get_BackColor_Proxy( 
    IVt6530Control __RPC_FAR * This,
    /* [retval][out] */ OLE_COLOR __RPC_FAR *pclr);


void __RPC_STUB IVt6530Control_get_BackColor_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [id][propput] */ HRESULT STDMETHODCALLTYPE IVt6530Control_put_BorderColor_Proxy( 
    IVt6530Control __RPC_FAR * This,
    /* [in] */ OLE_COLOR clr);


void __RPC_STUB IVt6530Control_put_BorderColor_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [id][propget] */ HRESULT STDMETHODCALLTYPE IVt6530Control_get_BorderColor_Proxy( 
    IVt6530Control __RPC_FAR * This,
    /* [retval][out] */ OLE_COLOR __RPC_FAR *pclr);


void __RPC_STUB IVt6530Control_get_BorderColor_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [id][propput] */ HRESULT STDMETHODCALLTYPE IVt6530Control_put_BorderWidth_Proxy( 
    IVt6530Control __RPC_FAR * This,
    /* [in] */ long width);


void __RPC_STUB IVt6530Control_put_BorderWidth_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [id][propget] */ HRESULT STDMETHODCALLTYPE IVt6530Control_get_BorderWidth_Proxy( 
    IVt6530Control __RPC_FAR * This,
    /* [retval][out] */ long __RPC_FAR *width);


void __RPC_STUB IVt6530Control_get_BorderWidth_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [id][propput] */ HRESULT STDMETHODCALLTYPE IVt6530Control_put_ForeColor_Proxy( 
    IVt6530Control __RPC_FAR * This,
    /* [in] */ OLE_COLOR clr);


void __RPC_STUB IVt6530Control_put_ForeColor_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [id][propget] */ HRESULT STDMETHODCALLTYPE IVt6530Control_get_ForeColor_Proxy( 
    IVt6530Control __RPC_FAR * This,
    /* [retval][out] */ OLE_COLOR __RPC_FAR *pclr);


void __RPC_STUB IVt6530Control_get_ForeColor_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [id][propput] */ HRESULT STDMETHODCALLTYPE IVt6530Control_put_BorderVisible_Proxy( 
    IVt6530Control __RPC_FAR * This,
    /* [in] */ VARIANT_BOOL vbool);


void __RPC_STUB IVt6530Control_put_BorderVisible_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [id][propget] */ HRESULT STDMETHODCALLTYPE IVt6530Control_get_BorderVisible_Proxy( 
    IVt6530Control __RPC_FAR * This,
    /* [retval][out] */ VARIANT_BOOL __RPC_FAR *pbool);


void __RPC_STUB IVt6530Control_get_BorderVisible_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530Control_waitENQ_Proxy( 
    IVt6530Control __RPC_FAR * This);


void __RPC_STUB IVt6530Control_waitENQ_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE IVt6530Control_get_Host_Proxy( 
    IVt6530Control __RPC_FAR * This,
    /* [retval][out] */ BSTR __RPC_FAR *pVal);


void __RPC_STUB IVt6530Control_get_Host_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE IVt6530Control_put_Host_Proxy( 
    IVt6530Control __RPC_FAR * This,
    /* [in] */ BSTR newVal);


void __RPC_STUB IVt6530Control_put_Host_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE IVt6530Control_get_Port_Proxy( 
    IVt6530Control __RPC_FAR * This,
    /* [retval][out] */ short __RPC_FAR *pVal);


void __RPC_STUB IVt6530Control_get_Port_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE IVt6530Control_put_Port_Proxy( 
    IVt6530Control __RPC_FAR * This,
    /* [in] */ short newVal);


void __RPC_STUB IVt6530Control_put_Port_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530Control_Connect_Proxy( 
    IVt6530Control __RPC_FAR * This,
    /* [in] */ int iTimeOut);


void __RPC_STUB IVt6530Control_Connect_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530Control_Disconnect_Proxy( 
    IVt6530Control __RPC_FAR * This);


void __RPC_STUB IVt6530Control_Disconnect_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530Control_getScreenDump_Proxy( 
    IVt6530Control __RPC_FAR * This,
    /* [retval][out] */ BSTR __RPC_FAR *asciiScreenChars);


void __RPC_STUB IVt6530Control_getScreenDump_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530Control_getAttributeDump_Proxy( 
    IVt6530Control __RPC_FAR * This,
    /* [retval][out] */ BSTR __RPC_FAR *asciiScreenAttrs);


void __RPC_STUB IVt6530Control_getAttributeDump_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE IVt6530Control_get_Field_Proxy( 
    IVt6530Control __RPC_FAR * This,
    int index,
    /* [retval][out] */ BSTR __RPC_FAR *pVal);


void __RPC_STUB IVt6530Control_get_Field_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE IVt6530Control_put_Field_Proxy( 
    IVt6530Control __RPC_FAR * This,
    int index,
    /* [in] */ BSTR newVal);


void __RPC_STUB IVt6530Control_put_Field_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE IVt6530Control_get_FieldAttributes_Proxy( 
    IVt6530Control __RPC_FAR * This,
    int index,
    /* [retval][out] */ int __RPC_FAR *pVal);


void __RPC_STUB IVt6530Control_get_FieldAttributes_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE IVt6530Control_put_FieldAttributes_Proxy( 
    IVt6530Control __RPC_FAR * This,
    int index,
    /* [in] */ int newVal);


void __RPC_STUB IVt6530Control_put_FieldAttributes_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530Control_IsReverse_Proxy( 
    IVt6530Control __RPC_FAR * This,
    /* [in] */ int iAttribute,
    /* [retval][out] */ BOOL __RPC_FAR *isIt);


void __RPC_STUB IVt6530Control_IsReverse_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530Control_IsInvis_Proxy( 
    IVt6530Control __RPC_FAR * This,
    /* [in] */ int iAttribute,
    /* [retval][out] */ BOOL __RPC_FAR *isIt);


void __RPC_STUB IVt6530Control_IsInvis_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530Control_IsBlinking_Proxy( 
    IVt6530Control __RPC_FAR * This,
    /* [in] */ int iAttribute,
    /* [retval][out] */ BOOL __RPC_FAR *isIt);


void __RPC_STUB IVt6530Control_IsBlinking_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530Control_IsUnderLine_Proxy( 
    IVt6530Control __RPC_FAR * This,
    /* [in] */ int iAttribute,
    /* [retval][out] */ BOOL __RPC_FAR *isIt);


void __RPC_STUB IVt6530Control_IsUnderLine_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530Control_IsUpshift_Proxy( 
    IVt6530Control __RPC_FAR * This,
    /* [in] */ int iAttribute,
    /* [retval][out] */ BOOL __RPC_FAR *isIt);


void __RPC_STUB IVt6530Control_IsUpshift_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530Control_getRGB_Proxy( 
    IVt6530Control __RPC_FAR * This,
    /* [in] */ int iAttribute,
    /* [retval][out] */ int __RPC_FAR *irgb);


void __RPC_STUB IVt6530Control_getRGB_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE IVt6530Control_get_CurrentField_Proxy( 
    IVt6530Control __RPC_FAR * This,
    /* [retval][out] */ BSTR __RPC_FAR *pVal);


void __RPC_STUB IVt6530Control_get_CurrentField_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE IVt6530Control_put_CurrentField_Proxy( 
    IVt6530Control __RPC_FAR * This,
    /* [in] */ BSTR newVal);


void __RPC_STUB IVt6530Control_put_CurrentField_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE IVt6530Control_get_UnprotectField_Proxy( 
    IVt6530Control __RPC_FAR * This,
    /* [in] */ int iIndex,
    /* [retval][out] */ BSTR __RPC_FAR *pVal);


void __RPC_STUB IVt6530Control_get_UnprotectField_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE IVt6530Control_put_UnprotectField_Proxy( 
    IVt6530Control __RPC_FAR * This,
    /* [in] */ int iIndex,
    /* [in] */ BSTR newVal);


void __RPC_STUB IVt6530Control_put_UnprotectField_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE IVt6530Control_get_Line_Proxy( 
    IVt6530Control __RPC_FAR * This,
    /* [in] */ int index,
    /* [retval][out] */ BSTR __RPC_FAR *pVal);


void __RPC_STUB IVt6530Control_get_Line_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530Control_IsFieldChanged_Proxy( 
    IVt6530Control __RPC_FAR * This,
    /* [in] */ int iIndex,
    /* [retval][out] */ BOOL __RPC_FAR *isIt);


void __RPC_STUB IVt6530Control_IsFieldChanged_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530Control_CursorToField_Proxy( 
    IVt6530Control __RPC_FAR * This,
    /* [in] */ int iIndex);


void __RPC_STUB IVt6530Control_CursorToField_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE IVt6530Control_get_Row_Proxy( 
    IVt6530Control __RPC_FAR * This,
    /* [retval][out] */ int __RPC_FAR *pVal);


void __RPC_STUB IVt6530Control_get_Row_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE IVt6530Control_put_Row_Proxy( 
    IVt6530Control __RPC_FAR * This,
    /* [in] */ int newVal);


void __RPC_STUB IVt6530Control_put_Row_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE IVt6530Control_get_Column_Proxy( 
    IVt6530Control __RPC_FAR * This,
    /* [retval][out] */ int __RPC_FAR *pVal);


void __RPC_STUB IVt6530Control_get_Column_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE IVt6530Control_put_Column_Proxy( 
    IVt6530Control __RPC_FAR * This,
    /* [in] */ int newVal);


void __RPC_STUB IVt6530Control_put_Column_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530Control_SendCommandLine_Proxy( 
    IVt6530Control __RPC_FAR * This,
    /* [in] */ BSTR sCommand);


void __RPC_STUB IVt6530Control_SendCommandLine_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530Control_FakeKey_Proxy( 
    IVt6530Control __RPC_FAR * This,
    /* [in] */ int iKeyCode,
    /* [in] */ BOOL bShift,
    /* [in] */ BOOL bAlt,
    /* [in] */ BOOL bCtrl);


void __RPC_STUB IVt6530Control_FakeKey_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530Control_FakeKeys_Proxy( 
    IVt6530Control __RPC_FAR * This,
    /* [in] */ BSTR sKeys);


void __RPC_STUB IVt6530Control_FakeKeys_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530Control_FakeF1_Proxy( 
    IVt6530Control __RPC_FAR * This);


void __RPC_STUB IVt6530Control_FakeF1_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530Control_FakeF2_Proxy( 
    IVt6530Control __RPC_FAR * This);


void __RPC_STUB IVt6530Control_FakeF2_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530Control_FakeF3_Proxy( 
    IVt6530Control __RPC_FAR * This);


void __RPC_STUB IVt6530Control_FakeF3_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530Control_FakeF4_Proxy( 
    IVt6530Control __RPC_FAR * This);


void __RPC_STUB IVt6530Control_FakeF4_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530Control_FakeF5_Proxy( 
    IVt6530Control __RPC_FAR * This);


void __RPC_STUB IVt6530Control_FakeF5_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530Control_FakeF6_Proxy( 
    IVt6530Control __RPC_FAR * This);


void __RPC_STUB IVt6530Control_FakeF6_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530Control_FakeF7_Proxy( 
    IVt6530Control __RPC_FAR * This);


void __RPC_STUB IVt6530Control_FakeF7_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530Control_FakeF8_Proxy( 
    IVt6530Control __RPC_FAR * This);


void __RPC_STUB IVt6530Control_FakeF8_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530Control_FakeF9_Proxy( 
    IVt6530Control __RPC_FAR * This);


void __RPC_STUB IVt6530Control_FakeF9_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530Control_FakeF10_Proxy( 
    IVt6530Control __RPC_FAR * This);


void __RPC_STUB IVt6530Control_FakeF10_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530Control_FakeF11_Proxy( 
    IVt6530Control __RPC_FAR * This);


void __RPC_STUB IVt6530Control_FakeF11_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530Control_FakeF12_Proxy( 
    IVt6530Control __RPC_FAR * This);


void __RPC_STUB IVt6530Control_FakeF12_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530Control_FakeF13_Proxy( 
    IVt6530Control __RPC_FAR * This);


void __RPC_STUB IVt6530Control_FakeF13_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530Control_FakeF14_Proxy( 
    IVt6530Control __RPC_FAR * This);


void __RPC_STUB IVt6530Control_FakeF14_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530Control_FakeF15_Proxy( 
    IVt6530Control __RPC_FAR * This);


void __RPC_STUB IVt6530Control_FakeF15_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530Control_FakeF16_Proxy( 
    IVt6530Control __RPC_FAR * This);


void __RPC_STUB IVt6530Control_FakeF16_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530Control_FakeEnter_Proxy( 
    IVt6530Control __RPC_FAR * This);


void __RPC_STUB IVt6530Control_FakeEnter_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530Control_FakeBackspace_Proxy( 
    IVt6530Control __RPC_FAR * This);


void __RPC_STUB IVt6530Control_FakeBackspace_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530Control_FakeTab_Proxy( 
    IVt6530Control __RPC_FAR * This);


void __RPC_STUB IVt6530Control_FakeTab_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530Control_FakeUpArrow_Proxy( 
    IVt6530Control __RPC_FAR * This);


void __RPC_STUB IVt6530Control_FakeUpArrow_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530Control_FakeDownArrow_Proxy( 
    IVt6530Control __RPC_FAR * This);


void __RPC_STUB IVt6530Control_FakeDownArrow_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530Control_FakeLeftArrow_Proxy( 
    IVt6530Control __RPC_FAR * This);


void __RPC_STUB IVt6530Control_FakeLeftArrow_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530Control_FakeRightArrow_Proxy( 
    IVt6530Control __RPC_FAR * This);


void __RPC_STUB IVt6530Control_FakeRightArrow_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530Control_FakeHome_Proxy( 
    IVt6530Control __RPC_FAR * This);


void __RPC_STUB IVt6530Control_FakeHome_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530Control_FakeEnd_Proxy( 
    IVt6530Control __RPC_FAR * This);


void __RPC_STUB IVt6530Control_FakeEnd_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530Control_FakeInsert_Proxy( 
    IVt6530Control __RPC_FAR * This);


void __RPC_STUB IVt6530Control_FakeInsert_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530Control_FakeDelete_Proxy( 
    IVt6530Control __RPC_FAR * This);


void __RPC_STUB IVt6530Control_FakeDelete_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530Control_toHTML_Proxy( 
    IVt6530Control __RPC_FAR * This,
    /* [retval][out] */ BSTR __RPC_FAR *sHTML);


void __RPC_STUB IVt6530Control_toHTML_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530Control_getScreenNumber_Proxy( 
    IVt6530Control __RPC_FAR * This,
    /* [retval][out] */ BSTR __RPC_FAR *sScreenNum);


void __RPC_STUB IVt6530Control_getScreenNumber_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530Control_wait34_Proxy( 
    IVt6530Control __RPC_FAR * This);


void __RPC_STUB IVt6530Control_wait34_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);



#endif 	/* __IVt6530Control_INTERFACE_DEFINED__ */



#ifndef __VT6530_TERMINAL_PROJLib_LIBRARY_DEFINED__
#define __VT6530_TERMINAL_PROJLib_LIBRARY_DEFINED__

/* library VT6530_TERMINAL_PROJLib */
/* [helpstring][version][uuid] */ 


EXTERN_C const IID LIBID_VT6530_TERMINAL_PROJLib;

#ifndef ___IVt5630ControlEvents_DISPINTERFACE_DEFINED__
#define ___IVt5630ControlEvents_DISPINTERFACE_DEFINED__

/* dispinterface _IVt5630ControlEvents */
/* [helpstring][uuid] */ 


EXTERN_C const IID DIID__IVt5630ControlEvents;

#if defined(__cplusplus) && !defined(CINTERFACE)

    MIDL_INTERFACE("EDA1E573-6F0F-11D4-98C6-000102494781")
    _IVt5630ControlEvents : public IDispatch
    {
    };
    
#else 	/* C style interface */

    typedef struct _IVt5630ControlEventsVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *QueryInterface )( 
            _IVt5630ControlEvents __RPC_FAR * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ void __RPC_FAR *__RPC_FAR *ppvObject);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *AddRef )( 
            _IVt5630ControlEvents __RPC_FAR * This);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *Release )( 
            _IVt5630ControlEvents __RPC_FAR * This);
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *GetTypeInfoCount )( 
            _IVt5630ControlEvents __RPC_FAR * This,
            /* [out] */ UINT __RPC_FAR *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *GetTypeInfo )( 
            _IVt5630ControlEvents __RPC_FAR * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo __RPC_FAR *__RPC_FAR *ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *GetIDsOfNames )( 
            _IVt5630ControlEvents __RPC_FAR * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR __RPC_FAR *rgszNames,
            /* [in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID __RPC_FAR *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *Invoke )( 
            _IVt5630ControlEvents __RPC_FAR * This,
            /* [in] */ DISPID dispIdMember,
            /* [in] */ REFIID riid,
            /* [in] */ LCID lcid,
            /* [in] */ WORD wFlags,
            /* [out][in] */ DISPPARAMS __RPC_FAR *pDispParams,
            /* [out] */ VARIANT __RPC_FAR *pVarResult,
            /* [out] */ EXCEPINFO __RPC_FAR *pExcepInfo,
            /* [out] */ UINT __RPC_FAR *puArgErr);
        
        END_INTERFACE
    } _IVt5630ControlEventsVtbl;

    interface _IVt5630ControlEvents
    {
        CONST_VTBL struct _IVt5630ControlEventsVtbl __RPC_FAR *lpVtbl;
    };

    

#ifdef COBJMACROS


#define _IVt5630ControlEvents_QueryInterface(This,riid,ppvObject)	\
    (This)->lpVtbl -> QueryInterface(This,riid,ppvObject)

#define _IVt5630ControlEvents_AddRef(This)	\
    (This)->lpVtbl -> AddRef(This)

#define _IVt5630ControlEvents_Release(This)	\
    (This)->lpVtbl -> Release(This)


#define _IVt5630ControlEvents_GetTypeInfoCount(This,pctinfo)	\
    (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo)

#define _IVt5630ControlEvents_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo)

#define _IVt5630ControlEvents_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)

#define _IVt5630ControlEvents_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)

#endif /* COBJMACROS */


#endif 	/* C style interface */


#endif 	/* ___IVt5630ControlEvents_DISPINTERFACE_DEFINED__ */


#ifndef __IVt6530_INTERFACE_DEFINED__
#define __IVt6530_INTERFACE_DEFINED__

/* interface IVt6530 */
/* [unique][helpstring][dual][uuid][object] */ 


EXTERN_C const IID IID_IVt6530;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("507CA441-785A-11D4-98D2-000102494781")
    IVt6530 : public IDispatch
    {
    public:
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE getScreenNumber( 
            /* [retval][out] */ BSTR __RPC_FAR *sScreenNum) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE toHTML( 
            /* [retval][out] */ BSTR __RPC_FAR *sHTML) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE FakeDelete( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE FakeInsert( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE FakeHome( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE FakeEnd( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE FakeRightArrow( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE FakeLeftArrow( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE FakeDownArrow( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE FakeUpArrow( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE FakeTab( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE FakeBackspace( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE FakeEnter( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE FakeF1( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE FakeF2( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE FakeF3( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE FakeF4( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE FakeF5( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE FakeF6( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE FakeF7( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE FakeF8( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE FakeF9( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE FakeF10( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE FakeF11( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE FakeF12( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE FakeF13( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE FakeF14( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE FakeF15( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE FakeF16( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE FakeKeys( 
            /* [in] */ BSTR sKeys) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE FakeKey( 
            /* [in] */ int iKeyCode,
            /* [in] */ BOOL bShift,
            /* [in] */ BOOL bAlt,
            /* [in] */ BOOL bCtrl) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE SendCommand( 
            /* [in] */ BSTR sCommand) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_Column( 
            /* [retval][out] */ int __RPC_FAR *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_Column( 
            /* [in] */ int newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_Row( 
            /* [retval][out] */ int __RPC_FAR *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_Row( 
            /* [in] */ int newVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE CursorToField( 
            /* [in] */ int iIndex) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IsFieldChanged( 
            /* [in] */ int iIndex,
            /* [retval][out] */ BOOL __RPC_FAR *bIsIt) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_Line( 
            /* [in] */ int iIndex,
            /* [retval][out] */ BSTR __RPC_FAR *pVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_UnprotectField( 
            /* [in] */ int iIndex,
            /* [retval][out] */ BSTR __RPC_FAR *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_UnprotectField( 
            /* [in] */ int iIndex,
            /* [in] */ BSTR newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_CurrentField( 
            /* [retval][out] */ BSTR __RPC_FAR *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_CurrentField( 
            /* [in] */ BSTR newVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE getRGB( 
            /* [in] */ int iAttribute,
            /* [retval][out] */ int __RPC_FAR *irgb) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IsUpshift( 
            /* [in] */ int iAttribute,
            /* [retval][out] */ BOOL __RPC_FAR *bIsIt) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IsUnderLine( 
            /* [in] */ int iAttribute,
            /* [retval][out] */ BOOL __RPC_FAR *bIsit) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IsBlinking( 
            /* [in] */ int iAttribute,
            /* [retval][out] */ BOOL __RPC_FAR *bIsit) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IsInvis( 
            /* [in] */ int iAttribute,
            /* [retval][out] */ BOOL __RPC_FAR *bIsit) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IsReverse( 
            /* [in] */ int iAttribute,
            /* [retval][out] */ BOOL __RPC_FAR *bIsit) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_FieldAttributes( 
            /* [in] */ int iIndex,
            /* [retval][out] */ int __RPC_FAR *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_FieldAttributes( 
            /* [in] */ int iIndex,
            /* [in] */ int newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_Field( 
            /* [in] */ int iIndex,
            /* [retval][out] */ BSTR __RPC_FAR *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_Field( 
            /* [in] */ int iIndex,
            /* [in] */ BSTR newVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE getAttributeDump( 
            /* [retval][out] */ BSTR __RPC_FAR *sScreenAttrs) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE getScreenDump( 
            /* [retval][out] */ BSTR __RPC_FAR *sScreenChars) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE Disconnect( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE Connect( 
            /* [in] */ int iTimeOut) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_Port( 
            /* [retval][out] */ int __RPC_FAR *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_Port( 
            /* [in] */ int newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_Host( 
            /* [retval][out] */ BSTR __RPC_FAR *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_Host( 
            /* [in] */ BSTR newVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE waitENQ( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE wait34( void) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct IVt6530Vtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *QueryInterface )( 
            IVt6530 __RPC_FAR * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ void __RPC_FAR *__RPC_FAR *ppvObject);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *AddRef )( 
            IVt6530 __RPC_FAR * This);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *Release )( 
            IVt6530 __RPC_FAR * This);
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *GetTypeInfoCount )( 
            IVt6530 __RPC_FAR * This,
            /* [out] */ UINT __RPC_FAR *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *GetTypeInfo )( 
            IVt6530 __RPC_FAR * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo __RPC_FAR *__RPC_FAR *ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *GetIDsOfNames )( 
            IVt6530 __RPC_FAR * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR __RPC_FAR *rgszNames,
            /* [in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID __RPC_FAR *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *Invoke )( 
            IVt6530 __RPC_FAR * This,
            /* [in] */ DISPID dispIdMember,
            /* [in] */ REFIID riid,
            /* [in] */ LCID lcid,
            /* [in] */ WORD wFlags,
            /* [out][in] */ DISPPARAMS __RPC_FAR *pDispParams,
            /* [out] */ VARIANT __RPC_FAR *pVarResult,
            /* [out] */ EXCEPINFO __RPC_FAR *pExcepInfo,
            /* [out] */ UINT __RPC_FAR *puArgErr);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *getScreenNumber )( 
            IVt6530 __RPC_FAR * This,
            /* [retval][out] */ BSTR __RPC_FAR *sScreenNum);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *toHTML )( 
            IVt6530 __RPC_FAR * This,
            /* [retval][out] */ BSTR __RPC_FAR *sHTML);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *FakeDelete )( 
            IVt6530 __RPC_FAR * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *FakeInsert )( 
            IVt6530 __RPC_FAR * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *FakeHome )( 
            IVt6530 __RPC_FAR * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *FakeEnd )( 
            IVt6530 __RPC_FAR * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *FakeRightArrow )( 
            IVt6530 __RPC_FAR * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *FakeLeftArrow )( 
            IVt6530 __RPC_FAR * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *FakeDownArrow )( 
            IVt6530 __RPC_FAR * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *FakeUpArrow )( 
            IVt6530 __RPC_FAR * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *FakeTab )( 
            IVt6530 __RPC_FAR * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *FakeBackspace )( 
            IVt6530 __RPC_FAR * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *FakeEnter )( 
            IVt6530 __RPC_FAR * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *FakeF1 )( 
            IVt6530 __RPC_FAR * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *FakeF2 )( 
            IVt6530 __RPC_FAR * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *FakeF3 )( 
            IVt6530 __RPC_FAR * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *FakeF4 )( 
            IVt6530 __RPC_FAR * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *FakeF5 )( 
            IVt6530 __RPC_FAR * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *FakeF6 )( 
            IVt6530 __RPC_FAR * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *FakeF7 )( 
            IVt6530 __RPC_FAR * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *FakeF8 )( 
            IVt6530 __RPC_FAR * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *FakeF9 )( 
            IVt6530 __RPC_FAR * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *FakeF10 )( 
            IVt6530 __RPC_FAR * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *FakeF11 )( 
            IVt6530 __RPC_FAR * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *FakeF12 )( 
            IVt6530 __RPC_FAR * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *FakeF13 )( 
            IVt6530 __RPC_FAR * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *FakeF14 )( 
            IVt6530 __RPC_FAR * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *FakeF15 )( 
            IVt6530 __RPC_FAR * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *FakeF16 )( 
            IVt6530 __RPC_FAR * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *FakeKeys )( 
            IVt6530 __RPC_FAR * This,
            /* [in] */ BSTR sKeys);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *FakeKey )( 
            IVt6530 __RPC_FAR * This,
            /* [in] */ int iKeyCode,
            /* [in] */ BOOL bShift,
            /* [in] */ BOOL bAlt,
            /* [in] */ BOOL bCtrl);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *SendCommand )( 
            IVt6530 __RPC_FAR * This,
            /* [in] */ BSTR sCommand);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *get_Column )( 
            IVt6530 __RPC_FAR * This,
            /* [retval][out] */ int __RPC_FAR *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *put_Column )( 
            IVt6530 __RPC_FAR * This,
            /* [in] */ int newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *get_Row )( 
            IVt6530 __RPC_FAR * This,
            /* [retval][out] */ int __RPC_FAR *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *put_Row )( 
            IVt6530 __RPC_FAR * This,
            /* [in] */ int newVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *CursorToField )( 
            IVt6530 __RPC_FAR * This,
            /* [in] */ int iIndex);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *IsFieldChanged )( 
            IVt6530 __RPC_FAR * This,
            /* [in] */ int iIndex,
            /* [retval][out] */ BOOL __RPC_FAR *bIsIt);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *get_Line )( 
            IVt6530 __RPC_FAR * This,
            /* [in] */ int iIndex,
            /* [retval][out] */ BSTR __RPC_FAR *pVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *get_UnprotectField )( 
            IVt6530 __RPC_FAR * This,
            /* [in] */ int iIndex,
            /* [retval][out] */ BSTR __RPC_FAR *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *put_UnprotectField )( 
            IVt6530 __RPC_FAR * This,
            /* [in] */ int iIndex,
            /* [in] */ BSTR newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *get_CurrentField )( 
            IVt6530 __RPC_FAR * This,
            /* [retval][out] */ BSTR __RPC_FAR *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *put_CurrentField )( 
            IVt6530 __RPC_FAR * This,
            /* [in] */ BSTR newVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *getRGB )( 
            IVt6530 __RPC_FAR * This,
            /* [in] */ int iAttribute,
            /* [retval][out] */ int __RPC_FAR *irgb);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *IsUpshift )( 
            IVt6530 __RPC_FAR * This,
            /* [in] */ int iAttribute,
            /* [retval][out] */ BOOL __RPC_FAR *bIsIt);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *IsUnderLine )( 
            IVt6530 __RPC_FAR * This,
            /* [in] */ int iAttribute,
            /* [retval][out] */ BOOL __RPC_FAR *bIsit);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *IsBlinking )( 
            IVt6530 __RPC_FAR * This,
            /* [in] */ int iAttribute,
            /* [retval][out] */ BOOL __RPC_FAR *bIsit);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *IsInvis )( 
            IVt6530 __RPC_FAR * This,
            /* [in] */ int iAttribute,
            /* [retval][out] */ BOOL __RPC_FAR *bIsit);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *IsReverse )( 
            IVt6530 __RPC_FAR * This,
            /* [in] */ int iAttribute,
            /* [retval][out] */ BOOL __RPC_FAR *bIsit);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *get_FieldAttributes )( 
            IVt6530 __RPC_FAR * This,
            /* [in] */ int iIndex,
            /* [retval][out] */ int __RPC_FAR *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *put_FieldAttributes )( 
            IVt6530 __RPC_FAR * This,
            /* [in] */ int iIndex,
            /* [in] */ int newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *get_Field )( 
            IVt6530 __RPC_FAR * This,
            /* [in] */ int iIndex,
            /* [retval][out] */ BSTR __RPC_FAR *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *put_Field )( 
            IVt6530 __RPC_FAR * This,
            /* [in] */ int iIndex,
            /* [in] */ BSTR newVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *getAttributeDump )( 
            IVt6530 __RPC_FAR * This,
            /* [retval][out] */ BSTR __RPC_FAR *sScreenAttrs);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *getScreenDump )( 
            IVt6530 __RPC_FAR * This,
            /* [retval][out] */ BSTR __RPC_FAR *sScreenChars);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *Disconnect )( 
            IVt6530 __RPC_FAR * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *Connect )( 
            IVt6530 __RPC_FAR * This,
            /* [in] */ int iTimeOut);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *get_Port )( 
            IVt6530 __RPC_FAR * This,
            /* [retval][out] */ int __RPC_FAR *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *put_Port )( 
            IVt6530 __RPC_FAR * This,
            /* [in] */ int newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *get_Host )( 
            IVt6530 __RPC_FAR * This,
            /* [retval][out] */ BSTR __RPC_FAR *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *put_Host )( 
            IVt6530 __RPC_FAR * This,
            /* [in] */ BSTR newVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *waitENQ )( 
            IVt6530 __RPC_FAR * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *wait34 )( 
            IVt6530 __RPC_FAR * This);
        
        END_INTERFACE
    } IVt6530Vtbl;

    interface IVt6530
    {
        CONST_VTBL struct IVt6530Vtbl __RPC_FAR *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IVt6530_QueryInterface(This,riid,ppvObject)	\
    (This)->lpVtbl -> QueryInterface(This,riid,ppvObject)

#define IVt6530_AddRef(This)	\
    (This)->lpVtbl -> AddRef(This)

#define IVt6530_Release(This)	\
    (This)->lpVtbl -> Release(This)


#define IVt6530_GetTypeInfoCount(This,pctinfo)	\
    (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo)

#define IVt6530_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo)

#define IVt6530_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)

#define IVt6530_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)


#define IVt6530_getScreenNumber(This,sScreenNum)	\
    (This)->lpVtbl -> getScreenNumber(This,sScreenNum)

#define IVt6530_toHTML(This,sHTML)	\
    (This)->lpVtbl -> toHTML(This,sHTML)

#define IVt6530_FakeDelete(This)	\
    (This)->lpVtbl -> FakeDelete(This)

#define IVt6530_FakeInsert(This)	\
    (This)->lpVtbl -> FakeInsert(This)

#define IVt6530_FakeHome(This)	\
    (This)->lpVtbl -> FakeHome(This)

#define IVt6530_FakeEnd(This)	\
    (This)->lpVtbl -> FakeEnd(This)

#define IVt6530_FakeRightArrow(This)	\
    (This)->lpVtbl -> FakeRightArrow(This)

#define IVt6530_FakeLeftArrow(This)	\
    (This)->lpVtbl -> FakeLeftArrow(This)

#define IVt6530_FakeDownArrow(This)	\
    (This)->lpVtbl -> FakeDownArrow(This)

#define IVt6530_FakeUpArrow(This)	\
    (This)->lpVtbl -> FakeUpArrow(This)

#define IVt6530_FakeTab(This)	\
    (This)->lpVtbl -> FakeTab(This)

#define IVt6530_FakeBackspace(This)	\
    (This)->lpVtbl -> FakeBackspace(This)

#define IVt6530_FakeEnter(This)	\
    (This)->lpVtbl -> FakeEnter(This)

#define IVt6530_FakeF1(This)	\
    (This)->lpVtbl -> FakeF1(This)

#define IVt6530_FakeF2(This)	\
    (This)->lpVtbl -> FakeF2(This)

#define IVt6530_FakeF3(This)	\
    (This)->lpVtbl -> FakeF3(This)

#define IVt6530_FakeF4(This)	\
    (This)->lpVtbl -> FakeF4(This)

#define IVt6530_FakeF5(This)	\
    (This)->lpVtbl -> FakeF5(This)

#define IVt6530_FakeF6(This)	\
    (This)->lpVtbl -> FakeF6(This)

#define IVt6530_FakeF7(This)	\
    (This)->lpVtbl -> FakeF7(This)

#define IVt6530_FakeF8(This)	\
    (This)->lpVtbl -> FakeF8(This)

#define IVt6530_FakeF9(This)	\
    (This)->lpVtbl -> FakeF9(This)

#define IVt6530_FakeF10(This)	\
    (This)->lpVtbl -> FakeF10(This)

#define IVt6530_FakeF11(This)	\
    (This)->lpVtbl -> FakeF11(This)

#define IVt6530_FakeF12(This)	\
    (This)->lpVtbl -> FakeF12(This)

#define IVt6530_FakeF13(This)	\
    (This)->lpVtbl -> FakeF13(This)

#define IVt6530_FakeF14(This)	\
    (This)->lpVtbl -> FakeF14(This)

#define IVt6530_FakeF15(This)	\
    (This)->lpVtbl -> FakeF15(This)

#define IVt6530_FakeF16(This)	\
    (This)->lpVtbl -> FakeF16(This)

#define IVt6530_FakeKeys(This,sKeys)	\
    (This)->lpVtbl -> FakeKeys(This,sKeys)

#define IVt6530_FakeKey(This,iKeyCode,bShift,bAlt,bCtrl)	\
    (This)->lpVtbl -> FakeKey(This,iKeyCode,bShift,bAlt,bCtrl)

#define IVt6530_SendCommand(This,sCommand)	\
    (This)->lpVtbl -> SendCommand(This,sCommand)

#define IVt6530_get_Column(This,pVal)	\
    (This)->lpVtbl -> get_Column(This,pVal)

#define IVt6530_put_Column(This,newVal)	\
    (This)->lpVtbl -> put_Column(This,newVal)

#define IVt6530_get_Row(This,pVal)	\
    (This)->lpVtbl -> get_Row(This,pVal)

#define IVt6530_put_Row(This,newVal)	\
    (This)->lpVtbl -> put_Row(This,newVal)

#define IVt6530_CursorToField(This,iIndex)	\
    (This)->lpVtbl -> CursorToField(This,iIndex)

#define IVt6530_IsFieldChanged(This,iIndex,bIsIt)	\
    (This)->lpVtbl -> IsFieldChanged(This,iIndex,bIsIt)

#define IVt6530_get_Line(This,iIndex,pVal)	\
    (This)->lpVtbl -> get_Line(This,iIndex,pVal)

#define IVt6530_get_UnprotectField(This,iIndex,pVal)	\
    (This)->lpVtbl -> get_UnprotectField(This,iIndex,pVal)

#define IVt6530_put_UnprotectField(This,iIndex,newVal)	\
    (This)->lpVtbl -> put_UnprotectField(This,iIndex,newVal)

#define IVt6530_get_CurrentField(This,pVal)	\
    (This)->lpVtbl -> get_CurrentField(This,pVal)

#define IVt6530_put_CurrentField(This,newVal)	\
    (This)->lpVtbl -> put_CurrentField(This,newVal)

#define IVt6530_getRGB(This,iAttribute,irgb)	\
    (This)->lpVtbl -> getRGB(This,iAttribute,irgb)

#define IVt6530_IsUpshift(This,iAttribute,bIsIt)	\
    (This)->lpVtbl -> IsUpshift(This,iAttribute,bIsIt)

#define IVt6530_IsUnderLine(This,iAttribute,bIsit)	\
    (This)->lpVtbl -> IsUnderLine(This,iAttribute,bIsit)

#define IVt6530_IsBlinking(This,iAttribute,bIsit)	\
    (This)->lpVtbl -> IsBlinking(This,iAttribute,bIsit)

#define IVt6530_IsInvis(This,iAttribute,bIsit)	\
    (This)->lpVtbl -> IsInvis(This,iAttribute,bIsit)

#define IVt6530_IsReverse(This,iAttribute,bIsit)	\
    (This)->lpVtbl -> IsReverse(This,iAttribute,bIsit)

#define IVt6530_get_FieldAttributes(This,iIndex,pVal)	\
    (This)->lpVtbl -> get_FieldAttributes(This,iIndex,pVal)

#define IVt6530_put_FieldAttributes(This,iIndex,newVal)	\
    (This)->lpVtbl -> put_FieldAttributes(This,iIndex,newVal)

#define IVt6530_get_Field(This,iIndex,pVal)	\
    (This)->lpVtbl -> get_Field(This,iIndex,pVal)

#define IVt6530_put_Field(This,iIndex,newVal)	\
    (This)->lpVtbl -> put_Field(This,iIndex,newVal)

#define IVt6530_getAttributeDump(This,sScreenAttrs)	\
    (This)->lpVtbl -> getAttributeDump(This,sScreenAttrs)

#define IVt6530_getScreenDump(This,sScreenChars)	\
    (This)->lpVtbl -> getScreenDump(This,sScreenChars)

#define IVt6530_Disconnect(This)	\
    (This)->lpVtbl -> Disconnect(This)

#define IVt6530_Connect(This,iTimeOut)	\
    (This)->lpVtbl -> Connect(This,iTimeOut)

#define IVt6530_get_Port(This,pVal)	\
    (This)->lpVtbl -> get_Port(This,pVal)

#define IVt6530_put_Port(This,newVal)	\
    (This)->lpVtbl -> put_Port(This,newVal)

#define IVt6530_get_Host(This,pVal)	\
    (This)->lpVtbl -> get_Host(This,pVal)

#define IVt6530_put_Host(This,newVal)	\
    (This)->lpVtbl -> put_Host(This,newVal)

#define IVt6530_waitENQ(This)	\
    (This)->lpVtbl -> waitENQ(This)

#define IVt6530_wait34(This)	\
    (This)->lpVtbl -> wait34(This)

#endif /* COBJMACROS */


#endif 	/* C style interface */



/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530_getScreenNumber_Proxy( 
    IVt6530 __RPC_FAR * This,
    /* [retval][out] */ BSTR __RPC_FAR *sScreenNum);


void __RPC_STUB IVt6530_getScreenNumber_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530_toHTML_Proxy( 
    IVt6530 __RPC_FAR * This,
    /* [retval][out] */ BSTR __RPC_FAR *sHTML);


void __RPC_STUB IVt6530_toHTML_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530_FakeDelete_Proxy( 
    IVt6530 __RPC_FAR * This);


void __RPC_STUB IVt6530_FakeDelete_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530_FakeInsert_Proxy( 
    IVt6530 __RPC_FAR * This);


void __RPC_STUB IVt6530_FakeInsert_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530_FakeHome_Proxy( 
    IVt6530 __RPC_FAR * This);


void __RPC_STUB IVt6530_FakeHome_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530_FakeEnd_Proxy( 
    IVt6530 __RPC_FAR * This);


void __RPC_STUB IVt6530_FakeEnd_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530_FakeRightArrow_Proxy( 
    IVt6530 __RPC_FAR * This);


void __RPC_STUB IVt6530_FakeRightArrow_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530_FakeLeftArrow_Proxy( 
    IVt6530 __RPC_FAR * This);


void __RPC_STUB IVt6530_FakeLeftArrow_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530_FakeDownArrow_Proxy( 
    IVt6530 __RPC_FAR * This);


void __RPC_STUB IVt6530_FakeDownArrow_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530_FakeUpArrow_Proxy( 
    IVt6530 __RPC_FAR * This);


void __RPC_STUB IVt6530_FakeUpArrow_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530_FakeTab_Proxy( 
    IVt6530 __RPC_FAR * This);


void __RPC_STUB IVt6530_FakeTab_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530_FakeBackspace_Proxy( 
    IVt6530 __RPC_FAR * This);


void __RPC_STUB IVt6530_FakeBackspace_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530_FakeEnter_Proxy( 
    IVt6530 __RPC_FAR * This);


void __RPC_STUB IVt6530_FakeEnter_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530_FakeF1_Proxy( 
    IVt6530 __RPC_FAR * This);


void __RPC_STUB IVt6530_FakeF1_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530_FakeF2_Proxy( 
    IVt6530 __RPC_FAR * This);


void __RPC_STUB IVt6530_FakeF2_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530_FakeF3_Proxy( 
    IVt6530 __RPC_FAR * This);


void __RPC_STUB IVt6530_FakeF3_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530_FakeF4_Proxy( 
    IVt6530 __RPC_FAR * This);


void __RPC_STUB IVt6530_FakeF4_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530_FakeF5_Proxy( 
    IVt6530 __RPC_FAR * This);


void __RPC_STUB IVt6530_FakeF5_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530_FakeF6_Proxy( 
    IVt6530 __RPC_FAR * This);


void __RPC_STUB IVt6530_FakeF6_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530_FakeF7_Proxy( 
    IVt6530 __RPC_FAR * This);


void __RPC_STUB IVt6530_FakeF7_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530_FakeF8_Proxy( 
    IVt6530 __RPC_FAR * This);


void __RPC_STUB IVt6530_FakeF8_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530_FakeF9_Proxy( 
    IVt6530 __RPC_FAR * This);


void __RPC_STUB IVt6530_FakeF9_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530_FakeF10_Proxy( 
    IVt6530 __RPC_FAR * This);


void __RPC_STUB IVt6530_FakeF10_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530_FakeF11_Proxy( 
    IVt6530 __RPC_FAR * This);


void __RPC_STUB IVt6530_FakeF11_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530_FakeF12_Proxy( 
    IVt6530 __RPC_FAR * This);


void __RPC_STUB IVt6530_FakeF12_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530_FakeF13_Proxy( 
    IVt6530 __RPC_FAR * This);


void __RPC_STUB IVt6530_FakeF13_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530_FakeF14_Proxy( 
    IVt6530 __RPC_FAR * This);


void __RPC_STUB IVt6530_FakeF14_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530_FakeF15_Proxy( 
    IVt6530 __RPC_FAR * This);


void __RPC_STUB IVt6530_FakeF15_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530_FakeF16_Proxy( 
    IVt6530 __RPC_FAR * This);


void __RPC_STUB IVt6530_FakeF16_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530_FakeKeys_Proxy( 
    IVt6530 __RPC_FAR * This,
    /* [in] */ BSTR sKeys);


void __RPC_STUB IVt6530_FakeKeys_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530_FakeKey_Proxy( 
    IVt6530 __RPC_FAR * This,
    /* [in] */ int iKeyCode,
    /* [in] */ BOOL bShift,
    /* [in] */ BOOL bAlt,
    /* [in] */ BOOL bCtrl);


void __RPC_STUB IVt6530_FakeKey_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530_SendCommand_Proxy( 
    IVt6530 __RPC_FAR * This,
    /* [in] */ BSTR sCommand);


void __RPC_STUB IVt6530_SendCommand_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE IVt6530_get_Column_Proxy( 
    IVt6530 __RPC_FAR * This,
    /* [retval][out] */ int __RPC_FAR *pVal);


void __RPC_STUB IVt6530_get_Column_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE IVt6530_put_Column_Proxy( 
    IVt6530 __RPC_FAR * This,
    /* [in] */ int newVal);


void __RPC_STUB IVt6530_put_Column_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE IVt6530_get_Row_Proxy( 
    IVt6530 __RPC_FAR * This,
    /* [retval][out] */ int __RPC_FAR *pVal);


void __RPC_STUB IVt6530_get_Row_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE IVt6530_put_Row_Proxy( 
    IVt6530 __RPC_FAR * This,
    /* [in] */ int newVal);


void __RPC_STUB IVt6530_put_Row_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530_CursorToField_Proxy( 
    IVt6530 __RPC_FAR * This,
    /* [in] */ int iIndex);


void __RPC_STUB IVt6530_CursorToField_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530_IsFieldChanged_Proxy( 
    IVt6530 __RPC_FAR * This,
    /* [in] */ int iIndex,
    /* [retval][out] */ BOOL __RPC_FAR *bIsIt);


void __RPC_STUB IVt6530_IsFieldChanged_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE IVt6530_get_Line_Proxy( 
    IVt6530 __RPC_FAR * This,
    /* [in] */ int iIndex,
    /* [retval][out] */ BSTR __RPC_FAR *pVal);


void __RPC_STUB IVt6530_get_Line_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE IVt6530_get_UnprotectField_Proxy( 
    IVt6530 __RPC_FAR * This,
    /* [in] */ int iIndex,
    /* [retval][out] */ BSTR __RPC_FAR *pVal);


void __RPC_STUB IVt6530_get_UnprotectField_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE IVt6530_put_UnprotectField_Proxy( 
    IVt6530 __RPC_FAR * This,
    /* [in] */ int iIndex,
    /* [in] */ BSTR newVal);


void __RPC_STUB IVt6530_put_UnprotectField_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE IVt6530_get_CurrentField_Proxy( 
    IVt6530 __RPC_FAR * This,
    /* [retval][out] */ BSTR __RPC_FAR *pVal);


void __RPC_STUB IVt6530_get_CurrentField_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE IVt6530_put_CurrentField_Proxy( 
    IVt6530 __RPC_FAR * This,
    /* [in] */ BSTR newVal);


void __RPC_STUB IVt6530_put_CurrentField_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530_getRGB_Proxy( 
    IVt6530 __RPC_FAR * This,
    /* [in] */ int iAttribute,
    /* [retval][out] */ int __RPC_FAR *irgb);


void __RPC_STUB IVt6530_getRGB_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530_IsUpshift_Proxy( 
    IVt6530 __RPC_FAR * This,
    /* [in] */ int iAttribute,
    /* [retval][out] */ BOOL __RPC_FAR *bIsIt);


void __RPC_STUB IVt6530_IsUpshift_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530_IsUnderLine_Proxy( 
    IVt6530 __RPC_FAR * This,
    /* [in] */ int iAttribute,
    /* [retval][out] */ BOOL __RPC_FAR *bIsit);


void __RPC_STUB IVt6530_IsUnderLine_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530_IsBlinking_Proxy( 
    IVt6530 __RPC_FAR * This,
    /* [in] */ int iAttribute,
    /* [retval][out] */ BOOL __RPC_FAR *bIsit);


void __RPC_STUB IVt6530_IsBlinking_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530_IsInvis_Proxy( 
    IVt6530 __RPC_FAR * This,
    /* [in] */ int iAttribute,
    /* [retval][out] */ BOOL __RPC_FAR *bIsit);


void __RPC_STUB IVt6530_IsInvis_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530_IsReverse_Proxy( 
    IVt6530 __RPC_FAR * This,
    /* [in] */ int iAttribute,
    /* [retval][out] */ BOOL __RPC_FAR *bIsit);


void __RPC_STUB IVt6530_IsReverse_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE IVt6530_get_FieldAttributes_Proxy( 
    IVt6530 __RPC_FAR * This,
    /* [in] */ int iIndex,
    /* [retval][out] */ int __RPC_FAR *pVal);


void __RPC_STUB IVt6530_get_FieldAttributes_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE IVt6530_put_FieldAttributes_Proxy( 
    IVt6530 __RPC_FAR * This,
    /* [in] */ int iIndex,
    /* [in] */ int newVal);


void __RPC_STUB IVt6530_put_FieldAttributes_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE IVt6530_get_Field_Proxy( 
    IVt6530 __RPC_FAR * This,
    /* [in] */ int iIndex,
    /* [retval][out] */ BSTR __RPC_FAR *pVal);


void __RPC_STUB IVt6530_get_Field_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE IVt6530_put_Field_Proxy( 
    IVt6530 __RPC_FAR * This,
    /* [in] */ int iIndex,
    /* [in] */ BSTR newVal);


void __RPC_STUB IVt6530_put_Field_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530_getAttributeDump_Proxy( 
    IVt6530 __RPC_FAR * This,
    /* [retval][out] */ BSTR __RPC_FAR *sScreenAttrs);


void __RPC_STUB IVt6530_getAttributeDump_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530_getScreenDump_Proxy( 
    IVt6530 __RPC_FAR * This,
    /* [retval][out] */ BSTR __RPC_FAR *sScreenChars);


void __RPC_STUB IVt6530_getScreenDump_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530_Disconnect_Proxy( 
    IVt6530 __RPC_FAR * This);


void __RPC_STUB IVt6530_Disconnect_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530_Connect_Proxy( 
    IVt6530 __RPC_FAR * This,
    /* [in] */ int iTimeOut);


void __RPC_STUB IVt6530_Connect_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE IVt6530_get_Port_Proxy( 
    IVt6530 __RPC_FAR * This,
    /* [retval][out] */ int __RPC_FAR *pVal);


void __RPC_STUB IVt6530_get_Port_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE IVt6530_put_Port_Proxy( 
    IVt6530 __RPC_FAR * This,
    /* [in] */ int newVal);


void __RPC_STUB IVt6530_put_Port_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE IVt6530_get_Host_Proxy( 
    IVt6530 __RPC_FAR * This,
    /* [retval][out] */ BSTR __RPC_FAR *pVal);


void __RPC_STUB IVt6530_get_Host_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE IVt6530_put_Host_Proxy( 
    IVt6530 __RPC_FAR * This,
    /* [in] */ BSTR newVal);


void __RPC_STUB IVt6530_put_Host_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530_waitENQ_Proxy( 
    IVt6530 __RPC_FAR * This);


void __RPC_STUB IVt6530_waitENQ_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IVt6530_wait34_Proxy( 
    IVt6530 __RPC_FAR * This);


void __RPC_STUB IVt6530_wait34_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);



#endif 	/* __IVt6530_INTERFACE_DEFINED__ */


EXTERN_C const CLSID CLSID_Vt6530Control;

#ifdef __cplusplus

class DECLSPEC_UUID("EDA1E572-6F0F-11D4-98C6-000102494781")
Vt6530Control;
#endif

EXTERN_C const CLSID CLSID_Vt6530;

#ifdef __cplusplus

class DECLSPEC_UUID("507CA442-785A-11D4-98D2-000102494781")
Vt6530;
#endif
#endif /* __VT6530_TERMINAL_PROJLib_LIBRARY_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

unsigned long             __RPC_USER  BSTR_UserSize(     unsigned long __RPC_FAR *, unsigned long            , BSTR __RPC_FAR * ); 
unsigned char __RPC_FAR * __RPC_USER  BSTR_UserMarshal(  unsigned long __RPC_FAR *, unsigned char __RPC_FAR *, BSTR __RPC_FAR * ); 
unsigned char __RPC_FAR * __RPC_USER  BSTR_UserUnmarshal(unsigned long __RPC_FAR *, unsigned char __RPC_FAR *, BSTR __RPC_FAR * ); 
void                      __RPC_USER  BSTR_UserFree(     unsigned long __RPC_FAR *, BSTR __RPC_FAR * ); 

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif
