	
// Vt5630Control.h : Declaration of the CVt5630Control

#ifndef __VT5630CONTROL_H_
#define __VT5630CONTROL_H_

#include "resource.h"       // main symbols
#include <atlctl.h>

#include "Term.h"
#include "TermCallBack.h"
#include "PaintInterface.h"
#include "Vt6530_Terminal_ProjCP.h"

#define WM_USER_CONNECT		(WM_USER + 1)
#define WM_USER_DISCONNECT	(WM_USER + 2)
#define WM_USER_ENQ			(WM_USER + 3)
#define WM_USER_ERROR		(WM_USER + 4)
#define WM_USER_DEBUG		(WM_USER + 5)

class TermCallBack;

/////////////////////////////////////////////////////////////////////////////
// CVt5630Control
class ATL_NO_VTABLE CVt6530Control :
	public CComObjectRootEx<CComSingleThreadModel>,
	public CStockPropImpl<CVt6530Control, IVt6530Control, &IID_IVt6530Control, &LIBID_VT6530_TERMINAL_PROJLib>,
	public CComControl<CVt6530Control>,
	public IPersistStreamInitImpl<CVt6530Control>,
	public IOleControlImpl<CVt6530Control>,
	public IOleObjectImpl<CVt6530Control>,
	public IOleInPlaceActiveObjectImpl<CVt6530Control>,
	public IViewObjectExImpl<CVt6530Control>,
	public IOleInPlaceObjectWindowlessImpl<CVt6530Control>,
	public ISupportErrorInfo,
	public IConnectionPointContainerImpl<CVt6530Control>,
	public IPersistStorageImpl<CVt6530Control>,
	public ISpecifyPropertyPagesImpl<CVt6530Control>,
	public IQuickActivateImpl<CVt6530Control>,
	public IDataObjectImpl<CVt6530Control>,
	public IProvideClassInfo2Impl<&CLSID_Vt6530Control, &DIID__IVt5630ControlEvents, &LIBID_VT6530_TERMINAL_PROJLib>,
	public IPropertyNotifySinkCP<CVt6530Control>,
	public CComCoClass<CVt6530Control, &CLSID_Vt6530Control>,
	public CProxy_IVt5630ControlEvents< CVt6530Control >
{
public:
	CVt6530Control() 
	{
		m_pUnkMarshaler = NULL;
		m_bResizeNatural = TRUE;
		m_bWindowOnly = TRUE;
		bCtrlDown = false;
		bShiftDown = false;
		bAltDown = false;
		m_bInitialized = false;
		m_clrBackColor = 0;
		m_clrForeColor = RGB(0, 0xFF, 0);
		hEvent = CreateEvent(NULL, TRUE, TRUE, "TermWriteSemp");
	}

DECLARE_GET_CONTROLLING_UNKNOWN()
DECLARE_REGISTRY_RESOURCEID(IDR_VT5630CONTROL)

DECLARE_PROTECT_FINAL_CONSTRUCT()

BEGIN_COM_MAP(CVt6530Control)
	COM_INTERFACE_ENTRY(IVt6530Control)
	COM_INTERFACE_ENTRY(IDispatch)
	COM_INTERFACE_ENTRY(IViewObjectEx)
	COM_INTERFACE_ENTRY(IViewObject2)
	COM_INTERFACE_ENTRY(IViewObject)
	COM_INTERFACE_ENTRY(IOleInPlaceObjectWindowless)
	COM_INTERFACE_ENTRY(IOleInPlaceObject)
	COM_INTERFACE_ENTRY2(IOleWindow, IOleInPlaceObjectWindowless)
	COM_INTERFACE_ENTRY(IOleInPlaceActiveObject)
	COM_INTERFACE_ENTRY(IOleControl)
	COM_INTERFACE_ENTRY(IOleObject)
	COM_INTERFACE_ENTRY(IPersistStreamInit)
	COM_INTERFACE_ENTRY2(IPersist, IPersistStreamInit)
	COM_INTERFACE_ENTRY(ISupportErrorInfo)
	COM_INTERFACE_ENTRY(IConnectionPointContainer)
	COM_INTERFACE_ENTRY(ISpecifyPropertyPages)
	COM_INTERFACE_ENTRY(IQuickActivate)
	COM_INTERFACE_ENTRY(IPersistStorage)
	COM_INTERFACE_ENTRY(IDataObject)
	COM_INTERFACE_ENTRY(IProvideClassInfo)
	COM_INTERFACE_ENTRY(IProvideClassInfo2)
	COM_INTERFACE_ENTRY_AGGREGATE(IID_IMarshal, m_pUnkMarshaler.p)
	COM_INTERFACE_ENTRY_IMPL(IConnectionPointContainer)
END_COM_MAP()

BEGIN_PROP_MAP(CVt6530Control)
	PROP_DATA_ENTRY("_cx", m_sizeExtent.cx, VT_UI4)
	PROP_DATA_ENTRY("_cy", m_sizeExtent.cy, VT_UI4)
	PROP_ENTRY("BackColor", DISPID_BACKCOLOR, CLSID_StockColorPage)
	PROP_ENTRY("BorderColor", DISPID_BORDERCOLOR, CLSID_StockColorPage)
	PROP_ENTRY("BorderVisible", DISPID_BORDERVISIBLE, CLSID_NULL)
	PROP_ENTRY("BorderWidth", DISPID_BORDERWIDTH, CLSID_NULL)
	PROP_ENTRY("ForeColor", DISPID_FORECOLOR, CLSID_StockColorPage)
	PROP_ENTRY("Host", 2, CLSID_NULL)
	PROP_ENTRY("Port", 3, CLSID_NULL)
	// Example entries
	// PROP_ENTRY("Property Description", dispid, clsid)
	// PROP_PAGE(CLSID_StockColorPage)
END_PROP_MAP()

BEGIN_CONNECTION_POINT_MAP(CVt6530Control)
	CONNECTION_POINT_ENTRY(IID_IPropertyNotifySink)
	CONNECTION_POINT_ENTRY(DIID__IVt5630ControlEvents)
END_CONNECTION_POINT_MAP()

BEGIN_MSG_MAP(CVt6530Control)
	CHAIN_MSG_MAP(CComControl<CVt6530Control>)
	DEFAULT_REFLECTION_HANDLER()
	MESSAGE_HANDLER(WM_SIZE, OnSize)
	MESSAGE_HANDLER(WM_CHAR, OnChar)
	MESSAGE_HANDLER(WM_KEYDOWN, OnKeyDown)
	MESSAGE_HANDLER(WM_SYSKEYDOWN, OnSysKeyDown)
	MESSAGE_HANDLER(WM_SYSKEYUP, OnSysKeyUp)
	MESSAGE_HANDLER(WM_KEYUP, OnKeyUp)
	MESSAGE_HANDLER(WM_HELP, OnHelp)
	MESSAGE_HANDLER(WM_USER_CONNECT, OnUserConnect)
	MESSAGE_HANDLER(WM_USER_DISCONNECT, OnUserDisConnect)
	MESSAGE_HANDLER(WM_USER_ENQ, OnUserEnq)
	MESSAGE_HANDLER(WM_USER_ERROR, OnUserError)
	MESSAGE_HANDLER(WM_USER_DEBUG, OnUserDebug)

END_MSG_MAP()
// Handler prototypes:
//  LRESULT MessageHandler(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled);
//  LRESULT CommandHandler(WORD wNotifyCode, WORD wID, HWND hWndCtl, BOOL& bHandled);
//  LRESULT NotifyHandler(int idCtrl, LPNMHDR pnmh, BOOL& bHandled);

	//IPersistStreamInit
	STDMETHOD(InitNew)();
	STDMETHOD(Load)(LPSTREAM pStm);
	bool m_bInitialized;

	STDMETHOD( get_BackColor )(OLE_COLOR *clrBackColor)
	{
		*clrBackColor = m_clrBackColor;
		return S_OK;
	}

	STDMETHOD( get_ForeColor )(OLE_COLOR *clrForeColor)
	{
		*clrForeColor = m_clrForeColor;
		return S_OK;
	}

	STDMETHOD( put_BackColor )(OLE_COLOR clrBackColor)
	{
		m_clrBackColor = clrBackColor;
		COLORREF cr;
		OleTranslateColor(m_clrBackColor, NULL, &cr);
		if (pi != NULL)
		{
			_ASSERT(_CrtIsMemoryBlock(pi, sizeof(PaintInterface), NULL, NULL, NULL));
			pi->setBackGroundColor(cr);
		}
		if (term != NULL)
		{
			_ASSERT(_CrtIsMemoryBlock(term, sizeof(Term), NULL, NULL, NULL));
			term->setBackgroundColor(cr);
		}
		SendOnDataChange();
		SetDirty(TRUE);
		return S_OK;
	}

	STDMETHOD( put_ForeColor )(OLE_COLOR clrForeColor)
	{
		m_clrForeColor = clrForeColor;
		COLORREF cr;
		OleTranslateColor(m_clrForeColor, NULL, &cr);
		if (pi != NULL)
		{
			_ASSERT(_CrtIsMemoryBlock(pi, sizeof(PaintInterface), NULL, NULL, NULL));
			pi->setForeGroundColor(cr);
		}
		if (term != NULL)
		{
			_ASSERT(_CrtIsMemoryBlock(term, sizeof(Term), NULL, NULL, NULL));
			term->setForegroundColor(cr);
		}
		SendOnDataChange();
		SetDirty(TRUE);
		return S_OK;
	}

	STDMETHOD(TranslateAccelerator)( LPMSG lpmsg )
	{
		if (lpmsg->message == VK_TAB)
		{
			term->keydown(VK_TAB, bShiftDown, bCtrlDown, bAltDown);
			return S_OK;
		}
		else if (lpmsg->message == VK_F1)
		{
			term->keydown(VK_F1, bShiftDown, bCtrlDown, bAltDown);
			return S_OK;
		}
		return S_FALSE;
	}

	bool bCtrlDown;
	bool bShiftDown;
	bool bAltDown;

	LRESULT OnHelp(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled)
	{
		//term->keydown(VK_F1, bShiftDown, bCtrlDown, bAltDown);
		bHandled = TRUE;
		return 0;
	}

	LRESULT OnSysKeyUp(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled)
	{
		switch (wParam)
		{
			case VK_MENU:
				bAltDown = false;
				break;
		}
		return 0;
	}

	LRESULT OnSysKeyDown(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled)
	{
		switch (wParam)
		{
			case VK_F10:
				try
				{
					term->keydown(wParam, bShiftDown, bCtrlDown, bAltDown);
				}
				catch (Exception e)
				{
					CComBSTR str(e.getMessage());
					Fire_Error(str.Detach());
				}
				catch (...)
				{
					CComBSTR str("Unknown exception in OnSysKeyDown");
					Fire_Error(str.Detach());
				}
				bHandled = TRUE;
				break;
			case VK_MENU:
				bAltDown = true;
				break;
		}
		return 0;
	}
	
	LRESULT OnKeyDown(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled)
	{
		switch (wParam)
		{
			case VK_CONTROL:
				bCtrlDown = true;
				break;
			case VK_SHIFT:
				bShiftDown = true;
				break;
			case VK_MENU:
				bAltDown = true;
				break;
			case VK_F1:
			case VK_F2:
			case VK_F3:
			case VK_F4:
			case VK_F5:
			case VK_F6:
			case VK_F7:
			case VK_F8:
			case VK_F9:
			case VK_F10:
			case VK_F11:
			case VK_F12:
			case VK_F13:
			case VK_F14:
			case VK_F15:
			case VK_F16:
				try
				{
					_ASSERT(_CrtIsMemoryBlock(term, sizeof(Term), NULL, NULL, NULL));
					term->keydown(wParam, bShiftDown, bCtrlDown, bAltDown);
				}
				catch (Exception e)
				{
					ATLTRACE(e.getMessage());
					CComBSTR str(e.getMessage());
					Fire_Error(str.Detach());
				}
				catch (...)
				{
					ATLTRACE("Unknown exception in OnKeyDown");
					CComBSTR str("Unknown exception in OnKeyDown");
					Fire_Error(str.Detach());
				}
				bHandled = TRUE;
				break;
			default:
				bHandled = FALSE;
				break;
		}
		return 0;
	}

	LRESULT OnChar(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled)
	{
		try
		{
			//BYTE keystate[256];
			//GetKeyboardState(&keystate[0]);
			//WORD key = ;
			//if (ToAscii(wParam, (lParam>>16)&0xFF, &keystate[0], &key, 0) == 0)
			//{
			//}
			term->keypress(wParam, bShiftDown, bCtrlDown, bAltDown);
		}
		catch (Exception e)
		{
			ATLTRACE(e.getMessage());
			CComBSTR str(e.getMessage());
			Fire_Error(str.Detach());
		}
		catch (...)
		{
			ATLTRACE("Unknown exception in OnChar");
			CComBSTR str("Unknown exception in OnChar");
			Fire_Error(str.Detach());
		}
		bHandled = TRUE;
		return 0;
	}

	LRESULT OnKeyUp(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled)
	{
		switch (wParam)
		{
			case VK_CONTROL:
				bCtrlDown = false;
				break;
			case VK_SHIFT:
				bShiftDown = false;
				break;
		}
		bHandled = TRUE;
		return 0;
	}

	LRESULT OnSize(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled)
	{
		int width = LOWORD(lParam);
		int height = HIWORD(lParam);
		if (pi != NULL)
		{
			_ASSERT(_CrtIsMemoryBlock(pi, sizeof(PaintInterface), NULL, NULL, NULL));
			pi->recalcFont();
		}
		bHandled = TRUE;
		return 0;
	}

	LRESULT OnUserConnect(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled)
	{
		Fire_Connected();
		return 0;
	}

	LRESULT OnUserDisConnect(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled)
	{
		Fire_Disconnected();
		return 0;
	}

	LRESULT OnUserEnq(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled)
	{
		Fire_Enquire();
		return 0;
	}

	LRESULT OnUserError(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled)
	{
		Fire_Error(m_bstrMessage);
		return 0;
	}

	LRESULT OnUserDebug(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled)
	{
		Fire_Debug(m_bstrMessage);
		return 0;
	}

	HRESULT FinalConstruct();

	void FinalRelease();

	CComPtr<IUnknown> m_pUnkMarshaler;

// ISupportsErrorInfo
	STDMETHOD(InterfaceSupportsErrorInfo)(REFIID riid)
	{
		static const IID* arr[] = 
		{
			&IID_IVt6530Control,
		};
		for (int i=0; i<sizeof(arr)/sizeof(arr[0]); i++)
		{
			if (InlineIsEqualGUID(*arr[i], riid))
				return S_OK;
		}
		return S_FALSE;
	}

// IViewObjectEx
	DECLARE_VIEW_STATUS(VIEWSTATUS_SOLIDBKGND | VIEWSTATUS_OPAQUE)

// IVt5630Control
public:
	STDMETHOD(wait34)();
	STDMETHOD(getScreenNumber)(/*[out, retval]*/ BSTR *sScreenNum);
	STDMETHOD(toHTML)(/*[out, retval]*/ BSTR *sHTML);
	STDMETHOD(FakeDelete)();
	STDMETHOD(FakeInsert)();
	STDMETHOD(FakeEnd)();
	STDMETHOD(FakeHome)();
	STDMETHOD(FakeRightArrow)();
	STDMETHOD(FakeLeftArrow)();
	STDMETHOD(FakeDownArrow)();
	STDMETHOD(FakeUpArrow)();
	STDMETHOD(FakeTab)();
	STDMETHOD(FakeBackspace)();
	STDMETHOD(FakeEnter)();
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
	STDMETHOD(FakeKeys)(/*[in]*/ BSTR sKeys);
	STDMETHOD(FakeKey)(/*[in]*/ int iKeyCode, /*[in]*/ BOOL bShift, /*[in]*/ BOOL bAlt, /*[in]*/ BOOL bCtrl);
	STDMETHOD(SendCommandLine)(/*[in]*/ BSTR sCommand);
	STDMETHOD(get_Column)(/*[out, retval]*/ int *pVal);
	STDMETHOD(put_Column)(/*[in]*/ int newVal);
	STDMETHOD(get_Row)(/*[out, retval]*/ int *pVal);
	STDMETHOD(put_Row)(/*[in]*/ int newVal);
	STDMETHOD(CursorToField)(/*[in]*/ int iIndex);
	STDMETHOD(IsFieldChanged)(/*[in]*/ int iIndex, /*[out, retval]*/ BOOL *isIt);
	STDMETHOD(get_Line)(/*[in]*/ int index, /*[out, retval]*/ BSTR *pVal);
	STDMETHOD(get_UnprotectField)(/*[in]*/ int iIndex, /*[out, retval]*/ BSTR *pVal);
	STDMETHOD(put_UnprotectField)(/*[in]*/ int iIndex, /*[in]*/ BSTR newVal);
	STDMETHOD(get_CurrentField)(/*[out, retval]*/ BSTR *pVal);
	STDMETHOD(put_CurrentField)(/*[in]*/ BSTR newVal);
	STDMETHOD(getRGB)(/*[in]*/ int iAttribute, /*[out, retval]*/ int *irgb);
	STDMETHOD(IsUpshift)(/*[in]*/ int iAttribute, /*[out, retval]*/ BOOL *isIt);
	STDMETHOD(IsUnderLine)(/*[in]*/ int iAttribute, /*[out, retval]*/ BOOL *isIt);
	STDMETHOD(IsBlinking)(/*[in]*/ int iAttribute, /*[out, retval]*/ BOOL *isIt);
	STDMETHOD(IsInvis)(/*[in]*/ int iAttribute, /*[out, retval]*/ BOOL *isIt);
	STDMETHOD(IsReverse)(/*[in]*/ int iAttribute, /*[out, retval]*/ BOOL *isIt);
	STDMETHOD(get_FieldAttributes)(int index, /*[out, retval]*/ int *pVal);
	STDMETHOD(put_FieldAttributes)(int index, /*[in]*/ int newVal);
	STDMETHOD(get_Field)(int index, /*[out, retval]*/ BSTR *pVal);
	STDMETHOD(put_Field)(int index, /*[in]*/ BSTR newVal);
	STDMETHOD(getAttributeDump)(/*[out, retval]*/ BSTR *asciiScreenAttrs);
	STDMETHOD(getScreenDump)(/*[out, retval]*/ BSTR *asciiScreenChars);
	STDMETHOD(Disconnect)();
	STDMETHOD(Connect)(/*[in]*/ int iTimeOut);
	STDMETHOD(get_Port)(/*[out, retval]*/ short *pVal);
	STDMETHOD(put_Port)(/*[in]*/ short newVal);
	STDMETHOD(get_Host)(/*[out, retval]*/ BSTR *pVal);
	STDMETHOD(put_Host)(/*[in]*/ BSTR newVal);
	STDMETHOD(waitENQ)();

	HRESULT OnDraw(ATL_DRAWINFO& di)
	{
		RECT& rc = *(RECT*)di.prcBounds;
		Rectangle(di.hdcDraw, rc.left, rc.top, rc.right, rc.bottom);

		if (pi == NULL)
		{
			pi = new PaintInterface(m_hWnd, term, m_clrBackColor, m_clrForeColor);
			term->setDisplay(pi);
		}
		//SetTextAlign(di.hdcDraw, TA_CENTER|TA_BASELINE);
		//LPCTSTR pszText = _T("ATL 3.0 : Vt5630Control");
		//TextOut(di.hdcDraw, 
		//	(rc.left + rc.right) / 2, 
		//	(rc.top + rc.bottom) / 2, 
		//	pszText, 
		//	lstrlen(pszText));

		pi->onPaint(di.hdcDraw);
		return S_OK;
	}
	CComBSTR m_bstrMessage;
	CComBSTR m_bstrHost;
	short m_sPort;
	OLE_COLOR m_clrBackColor;
	OLE_COLOR m_clrBorderColor;
	BOOL m_bBorderVisible;
	LONG m_nBorderWidth;
	OLE_COLOR m_clrForeColor;

	Term *term;
	TermCallBack *tcb;
	PaintInterface *pi;
	HANDLE hEvent;
};

#include "TermEventListener.h"

class TermCallBack : public TermEventListener
{
	CVt6530Control *control;
	bool connected;

public:	
	TermCallBack(CVt6530Control *control)
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
		PostMessage(control->m_hWnd, WM_USER_CONNECT, 0, 0);
	}

	/**
	 *  The connection to the host was lost or
	 *  closed.
	 */
	void disconnect()
	{
		connected = false;
		//control->Fire_Disconnected();
		PostMessage(control->m_hWnd, WM_USER_DISCONNECT, 0, 0);
	}

	/**
	 *  The host has completed rendering the
	 *  screen and is now waiting for input.
	 */
	void enquire()
	{
		//control->Fire_Enquire();
		PostMessage(control->m_hWnd, WM_USER_ENQ, 0, 0);
	}

	/**
	 *  Changes in the display require the container
	 *  to repaint.
	 */
	void displayChanged()
	{
		if (control->pi != NULL)
		{
			control->pi->forceRepaint();
		}
	}

	/**
	 *  There has been an internal error.
	 */
	void error(char *message)
	{
		//control->Fire_Error(CComBSTR(message));
		control->m_bstrMessage = message;
		PostMessage(control->m_hWnd, WM_USER_DEBUG, 0, 0);
	}

	/**
	 *  Debuging output -- may be ignored
	 */
	void debug(char *message)
	{
		//control->Fire_Debug(CComBSTR(message));
		control->m_bstrMessage = message;
		PostMessage(control->m_hWnd, WM_USER_DEBUG, 0, 0);
	}

	void recv34(char *op, char *params, int paramLen)
	{
	}
};


#endif //__VT5630CONTROL_H_
