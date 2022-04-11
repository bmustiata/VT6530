	
// Vt6530.h : Declaration of the CVt6530

#ifndef __VT6530_H_
#define __VT6530_H_

#include "resource.h"       // main symbols

#include "Term.h"

#include "TermEventListener.h"
#include "Vt6530_Terminal_ProjCP.h"

class TermCallBackB;


/////////////////////////////////////////////////////////////////////////////
// CVt6530
class ATL_NO_VTABLE CVt6530 : 
	public CComObjectRootEx<CComMultiThreadModel>,
	public CComCoClass<CVt6530, &CLSID_Vt6530>,
	public ISupportErrorInfo,
	public IConnectionPointContainerImpl<CVt6530>,
	public IDispatchImpl<IVt6530, &IID_IVt6530, &LIBID_VT6530_TERMINAL_PROJLib>
{
public:
	CVt6530()
	{
		m_pUnkMarshaler = NULL;
		hEvent = CreateEvent(NULL, TRUE, TRUE, "TermWriteSemp");
	}

DECLARE_REGISTRY_RESOURCEID(IDR_VT6530)
DECLARE_GET_CONTROLLING_UNKNOWN()

DECLARE_PROTECT_FINAL_CONSTRUCT()

BEGIN_COM_MAP(CVt6530)
	COM_INTERFACE_ENTRY(IVt6530)
	COM_INTERFACE_ENTRY(IDispatch)
	COM_INTERFACE_ENTRY(ISupportErrorInfo)
	COM_INTERFACE_ENTRY(IConnectionPointContainer)
	COM_INTERFACE_ENTRY_AGGREGATE(IID_IMarshal, m_pUnkMarshaler.p)
	COM_INTERFACE_ENTRY_IMPL(IConnectionPointContainer)
END_COM_MAP()
BEGIN_CONNECTION_POINT_MAP(CVt6530)
END_CONNECTION_POINT_MAP()


	HRESULT FinalConstruct();

	void FinalRelease();

	CComPtr<IUnknown> m_pUnkMarshaler;

// ISupportsErrorInfo
	STDMETHOD(InterfaceSupportsErrorInfo)(REFIID riid);

// IVt6530
public:
	STDMETHOD(wait34)();
	STDMETHOD(waitENQ)();
	STDMETHOD(get_Host)(/*[out, retval]*/ BSTR *pVal);
	STDMETHOD(put_Host)(/*[in]*/ BSTR newVal);
	STDMETHOD(get_Port)(/*[out, retval]*/ int *pVal);
	STDMETHOD(put_Port)(/*[in]*/ int newVal);
	STDMETHOD(Connect)(/*[in]*/ int iTimeOut);
	STDMETHOD(Disconnect)();
	STDMETHOD(getScreenDump)(/*[out, retval]*/ BSTR *sScreenChars);
	STDMETHOD(getAttributeDump)(/*[out, retval]*/ BSTR *sScreenAttrs);
	STDMETHOD(get_Field)(/*[in]*/ int iIndex, /*[out, retval]*/ BSTR *pVal);
	STDMETHOD(put_Field)(/*[in]*/ int iIndex, /*[in]*/ BSTR newVal);
	STDMETHOD(get_FieldAttributes)(/*[in]*/ int iIndex, /*[out, retval]*/ int *pVal);
	STDMETHOD(put_FieldAttributes)(/*[in]*/ int iIndex, /*[in]*/ int newVal);
	STDMETHOD(IsReverse)(/*[in]*/ int iAttribute, /*[out, retval]*/ BOOL *bIsit);
	STDMETHOD(IsInvis)(/*[in]*/ int iAttribute, /*[out, retval]*/ BOOL *bIsit);
	STDMETHOD(IsBlinking)(/*[in]*/ int iAttribute, /*[out, retval]*/ BOOL *bIsit);
	STDMETHOD(IsUnderLine)(/*[in]*/ int iAttribute, /*[out, retval]*/ BOOL *bIsit);
	STDMETHOD(IsUpshift)(/*[in]*/ int iAttribute, /*[out, retval]*/ BOOL *bIsIt);
	STDMETHOD(getRGB)(/*[in]*/ int iAttribute, /*[out, retval]*/ int *irgb);
	STDMETHOD(get_CurrentField)(/*[out, retval]*/ BSTR *pVal);
	STDMETHOD(put_CurrentField)(/*[in]*/ BSTR newVal);
	STDMETHOD(get_UnprotectField)(/*[in]*/ int iIndex, /*[out, retval]*/ BSTR *pVal);
	STDMETHOD(put_UnprotectField)(/*[in]*/ int iIndex, /*[in]*/ BSTR newVal);
	STDMETHOD(get_Line)(/*[in]*/ int iIndex, /*[out, retval]*/ BSTR *pVal);
	STDMETHOD(IsFieldChanged)(/*[in]*/int iIndex, /*[out, retval]*/ BOOL *bIsIt);
	STDMETHOD(CursorToField)(/*[in]*/ int iIndex);
	STDMETHOD(get_Row)(/*[out, retval]*/ int *pVal);
	STDMETHOD(put_Row)(/*[in]*/ int newVal);
	STDMETHOD(get_Column)(/*[out, retval]*/ int *pVal);
	STDMETHOD(put_Column)(/*[in]*/ int newVal);
	STDMETHOD(SendCommand)(/*[in]*/ BSTR sCommand);
	STDMETHOD(FakeKey)(/*[in]*/ int iKeyCode, /*[in]*/ BOOL bShift, /*[in]*/ BOOL bAlt, /*[in]*/ BOOL bCtrl);
	STDMETHOD(FakeKeys)(/*[in]*/ BSTR sKeys);
	STDMETHOD(FakeF16)();
	STDMETHOD(FakeF15)();
	STDMETHOD(FakeF14)();
	STDMETHOD(FakeF13)();
	STDMETHOD(FakeF12)();
	STDMETHOD(FakeF11)();
	STDMETHOD(FakeF10)();
	STDMETHOD(FakeF9)();
	STDMETHOD(FakeF8)();
	STDMETHOD(FakeF7)();
	STDMETHOD(FakeF6)();
	STDMETHOD(FakeF5)();
	STDMETHOD(FakeF4)();
	STDMETHOD(FakeF3)();
	STDMETHOD(FakeF2)();
	STDMETHOD(FakeF1)();
	STDMETHOD(FakeEnter)();
	STDMETHOD(FakeBackspace)();
	STDMETHOD(FakeTab)();
	STDMETHOD(FakeUpArrow)();
	STDMETHOD(FakeDownArrow)();
	STDMETHOD(FakeLeftArrow)();
	STDMETHOD(FakeRightArrow)();
	STDMETHOD(FakeEnd)();
	STDMETHOD(FakeHome)();
	STDMETHOD(FakeInsert)();
	STDMETHOD(FakeDelete)();
	STDMETHOD(toHTML)(/*[out, retval]*/ BSTR *sHTML);
	STDMETHOD(getScreenNumber)(/*[out, retval]*/ BSTR *sScreenNum);

	Term *term;
	TermCallBackB *tcb;
	HANDLE hEvent;
};


class TermCallBackB : public TermEventListener
{
	CVt6530 *control;
	bool connected;

public:	
	TermCallBackB(CVt6530 *control)
	{
		this->control = control;
	}
	
	/**
	 *  The terminal has successfully connected
	 *  to the host.
	 */
	void connect()
	{
		SetEvent(control->hEvent);
		connected = true;
		//control->Fire_Connected();
	}

	/**
	 *  The connection to the host was lost or
	 *  closed.
	 */
	void disconnect()
	{
		connected = false;
		//control->Fire_Disconnected();
	}

	/**
	 *  The host has completed rendering the
	 *  screen and is now waiting for input.
	 */
	void enquire()
	{
		//control->Fire_Enquire();
	}

	/**
	 *  Changes in the display require the container
	 *  to repaint.
	 */
	void displayChanged()
	{
	}

	/**
	 *  There has been an internal error.
	 */
	void error(char *message)
	{
		//control->Fire_Error(CComBSTR(message));
	}

	/**
	 *  Debuging output -- may be ignored
	 */
	void debug(char *message)
	{
		//control->Fire_Debug(CComBSTR(message));
	}

	void recv34(char *op, char *params, int paramLen)
	{
	}
};


#endif //__VT6530_H_
