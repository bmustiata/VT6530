// Vt5630Control.cpp : Implementation of CVt6530Control

#include "stdafx.h"
#include <comdef.h>
#include "Vt6530_Terminal_Proj.h"
#include "Vt5630Control.h"

/////////////////////////////////////////////////////////////////////////////
// CVt6530Control

HRESULT CVt6530Control::FinalConstruct()
{
	ATLTRACE("FinalConstruct\n");
	HRESULT ret = CoCreateFreeThreadedMarshaler(GetControllingUnknown(), &m_pUnkMarshaler.p);
	WSADATA data;

	if (WSAStartup(2, &data) != 0)
	{
		AtlReportError(CLSID_Vt6530, "Can't init sockets", IID_IVt6530, E_FAIL);
		return E_FAIL;
	}
	tcb = new TermCallBack(this);
	term = new Term();
	term->addListener(tcb);
	pi = NULL;

	COLORREF cr;
	OleTranslateColor(m_clrBackColor, NULL, &cr);
	term->setBackgroundColor(cr);
	OleTranslateColor(m_clrForeColor, NULL, &cr);
	term->setForegroundColor(cr);
	return ret;
}

void CVt6530Control::FinalRelease()
{
	ATLTRACE("FinalRelease\n");
	term->close();
	CloseHandle(hEvent);
	WSACleanup();
	delete term;
	delete tcb;
	delete pi;
	term = NULL;
	tcb = NULL;
	pi = NULL;
	m_pUnkMarshaler.Release();
	ATLTRACE("FinalRelease Complete\n");
}

STDMETHODIMP CVt6530Control::InitNew()
{
	ATLTRACE("InitNew\n");
	if (m_bInitialized)
	{
		return E_UNEXPECTED;
	}
	m_bstrHost = "is";
	m_sPort = 1016;
	m_clrBackColor = 0;
	m_clrForeColor = RGB(0, 0xFF, 0);
	SetDirty(TRUE);
	m_bInitialized = true;
	return S_OK;
}

STDMETHODIMP CVt6530Control::Load(LPSTREAM pStm)
{
	ATLTRACE("Load\n");
	if (m_bInitialized)
	{
		return E_UNEXPECTED;
	}
	m_bInitialized = true;
	return IPersistStreamInitImpl<CVt6530Control>::Load(pStm);
}

STDMETHODIMP CVt6530Control::waitENQ()
{
	//Sleep(1000);
	bool ret = term->waitENQ();
	if (ret)
	{
		return S_OK;
	}
	else
	{
		return E_FAIL;
	}
}

STDMETHODIMP CVt6530Control::get_Host(BSTR *pVal)
{
	CComBSTR bstr;
	char *hostName;

	hostName = term->getHost();

	bstr.Append(hostName);
	*pVal = bstr.Detach();

	return S_OK;
}

STDMETHODIMP CVt6530Control::put_Host(BSTR newVal)
{
	m_bstrHost = newVal;

	SendOnDataChange();
	SetDirty(TRUE);

	_bstr_t val(newVal);
	term->setHost(val);
	return S_OK;
}

STDMETHODIMP CVt6530Control::get_Port(short *pVal)
{
	*pVal = term->getPort();
	return S_OK;
}

STDMETHODIMP CVt6530Control::put_Port(short newVal)
{
	m_sPort = newVal;

	SendOnDataChange();
	SetDirty(TRUE);

	term->setPort(newVal);
	return S_OK;
}

STDMETHODIMP CVt6530Control::Connect(int iTimeOut)
{
	ResetEvent(hEvent);

	try
	{
		term->connect();
	}
	catch (Exception e)
	{
		AtlReportError(CLSID_Vt6530Control, e.getMessage(), IID_IVt6530Control, E_FAIL);
		CComBSTR str(e.getMessage());
		Fire_Error(str.Detach());
		return E_FAIL;
	}
	catch (...)
	{
		AtlReportError(CLSID_Vt6530Control, "Unknown exception in Connect", IID_IVt6530Control, E_FAIL);
		CComBSTR str("Unknown exception in Connect");
		Fire_Error(str.Detach());
		return E_FAIL;
	}
	WaitForSingleObject(hEvent, 5000);
	return S_OK;
}

STDMETHODIMP CVt6530Control::Disconnect()
{
	try
	{
		term->close();
	}
	catch (Exception e)
	{
		AtlReportError(CLSID_Vt6530Control, e.getMessage(), IID_IVt6530Control, E_FAIL);
		CComBSTR str(e.getMessage());
		Fire_Debug(str.Detach());
		return E_FAIL;
	}
	catch (...)
	{
		AtlReportError(CLSID_Vt6530Control, "Unknown exception in Disconnect", IID_IVt6530Control, E_FAIL);
		CComBSTR str("Unknown exception in Disconnect");
		Fire_Error(str.Detach());
		return E_FAIL;
	}
	return S_OK;
}

STDMETHODIMP CVt6530Control::getScreenDump(BSTR *asciiScreenChars)
{
	StringBuffer sb;
	CComBSTR bstr;

	term->scrapeScreen(&sb);

	bstr.Append(sb);
	*asciiScreenChars = bstr.Detach();
	return S_OK;
}

STDMETHODIMP CVt6530Control::getAttributeDump(BSTR *asciiScreenAttrs)
{
	StringBuffer sb;
	CComBSTR bstr;

	term->scrapeAttributes(&sb);

	bstr.Append(sb);
	*asciiScreenAttrs = bstr.Detach();
	return S_OK;
}

STDMETHODIMP CVt6530Control::get_Field(int index, BSTR *pVal)
{
	StringBuffer sb;
	CComBSTR bstr;

	term->getField(index, &sb);
	
	bstr.Append(sb);
	*pVal = bstr.Detach();

	return S_OK;
}

STDMETHODIMP CVt6530Control::put_Field(int index, BSTR newVal)
{
	StringBuffer sb(newVal);
	term->setField(index, sb);

	return S_OK;
}

STDMETHODIMP CVt6530Control::get_FieldAttributes(int index, int *pVal)
{
	*pVal = term->getFieldAttributes(index);
	return S_OK;
}

STDMETHODIMP CVt6530Control::put_FieldAttributes(int index, int newVal)
{
	return E_NOTIMPL;
}

STDMETHODIMP CVt6530Control::get_CurrentField(BSTR *pVal)
{
	StringBuffer sb;
	term->getCurrentField(&sb);
	return S_OK;
}

STDMETHODIMP CVt6530Control::put_CurrentField(BSTR newVal)
{
	return E_NOTIMPL;
}

STDMETHODIMP CVt6530Control::get_UnprotectField(int iIndex, BSTR *pVal)
{
	StringBuffer sb;
	CComBSTR bstr;

	term->getUnprotectField(iIndex, &sb);

	bstr.Append(sb);
	*pVal = bstr.Detach();
	return S_OK;
}

STDMETHODIMP CVt6530Control::put_UnprotectField(int iIndex, BSTR newVal)
{
	return E_NOTIMPL;
}

STDMETHODIMP CVt6530Control::get_Line(int index, BSTR *pVal)
{
	if (index <= 0 || index > term->getNumRows())
	{
		AtlReportError(CLSID_Vt6530Control, "Invalid row", IID_IVt6530Control, E_FAIL);
		return E_FAIL;
	}
	StringBuffer sb;
	CComBSTR bstr;
	term->getLine(index-1, &sb);
	bstr.Append(sb);
	*pVal = bstr.Detach();
	return S_OK;
}

STDMETHODIMP CVt6530Control::IsFieldChanged(int iIndex, BOOL *isIt)
{
	if (term->isFieldChanged(iIndex))
	{
		*isIt = TRUE;
	}
	else
	{
		*isIt = FALSE;
	}
	return S_OK;
}

STDMETHODIMP CVt6530Control::CursorToField(int iIndex)
{
	term->cursorToField(iIndex);
	return S_OK;
}

STDMETHODIMP CVt6530Control::IsReverse(int iAttribute, BOOL *isIt)
{
	return E_NOTIMPL;
}

STDMETHODIMP CVt6530Control::IsInvis(int iAttribute, BOOL *isIt)
{
	return E_NOTIMPL;
}

STDMETHODIMP CVt6530Control::IsBlinking(int iAttribute, BOOL *isIt)
{
	return E_NOTIMPL;
}

STDMETHODIMP CVt6530Control::IsUnderLine(int iAttribute, BOOL *isIt)
{
	return E_NOTIMPL;
}

STDMETHODIMP CVt6530Control::IsUpshift(int iAttribute, BOOL *isIt)
{
	return E_NOTIMPL;
}

STDMETHODIMP CVt6530Control::getRGB(int iAttribute, int *irgb)
{
	return E_NOTIMPL;
}

STDMETHODIMP CVt6530Control::get_Row(int *pVal)
{
	*pVal = term->getCursorY() + 1;
	return S_OK;
}

STDMETHODIMP CVt6530Control::put_Row(int newVal)
{
	term->setCursorY(newVal - 1);
	return S_OK;
}

STDMETHODIMP CVt6530Control::get_Column(int *pVal)
{
	*pVal = term->getCursorX() + 1;
	return S_OK;
}

STDMETHODIMP CVt6530Control::put_Column(int newVal)
{
	term->setCursorX(newVal - 1);
	return S_OK;
}

STDMETHODIMP CVt6530Control::SendCommandLine(BSTR sCommand)
{
	StringBuffer sb(sCommand);
	try
	{
		term->sendCommandLine(sb);
	}
	catch (Exception e)
	{
		AtlReportError(CLSID_Vt6530Control, e.getMessage(), IID_IVt6530Control, E_FAIL);
		CComBSTR str(e.getMessage());
		Fire_Debug(str.Detach());
		return E_FAIL;
	}
	catch (...)
	{
		AtlReportError(CLSID_Vt6530Control, "Unknown exception in SendCommandLine", IID_IVt6530Control, E_FAIL);
		CComBSTR str("Unknown exception in SendCommandLine");
		Fire_Error(str.Detach());
		return E_FAIL;
	}
	return S_OK;
}

STDMETHODIMP CVt6530Control::FakeKey(int iKeyCode, BOOL bShift, BOOL bAlt, BOOL bCtrl)
{
	try
	{
		term->fakeKey(iKeyCode, bShift == TRUE, bAlt == TRUE, bCtrl == TRUE);
	}
	catch (Exception e)
	{
		AtlReportError(CLSID_Vt6530Control, e.getMessage(), IID_IVt6530Control, E_FAIL);
		CComBSTR str(e.getMessage());
		Fire_Debug(str.Detach());
		return E_FAIL;
	}
	catch (...)
	{
		AtlReportError(CLSID_Vt6530Control, "Unknown exception in FakeKey", IID_IVt6530Control, E_FAIL);
		CComBSTR str("Unknown exception in FakeKey");
		Fire_Error(str.Detach());
		return E_FAIL;
	}
	return S_OK;
}

STDMETHODIMP CVt6530Control::FakeKeys(BSTR sKeys)
{
	StringBuffer sb(sKeys);

	try
	{
		term->fakeKeys(sb);
	}
	catch (Exception e)
	{
		AtlReportError(CLSID_Vt6530Control, e.getMessage(), IID_IVt6530Control, E_FAIL);
		CComBSTR str(e.getMessage());
		Fire_Debug(str.Detach());
		return E_FAIL;
	}
	catch (...)
	{
		AtlReportError(CLSID_Vt6530Control, "Unknown exception in FakeKeys", IID_IVt6530Control, E_FAIL);
		CComBSTR str("Unknown exception in FakeKeys");
		Fire_Error(str.Detach());
		return E_FAIL;
	}
	return S_OK;
}

STDMETHODIMP CVt6530Control::FakeF1()
{
	term->fakeF1();
	return S_OK;
}

STDMETHODIMP CVt6530Control::FakeF2()
{
	term->fakeF2();
	return S_OK;
}

STDMETHODIMP CVt6530Control::FakeF3()
{
	term->fakeF3();
	return S_OK;
}

STDMETHODIMP CVt6530Control::FakeF4()
{
	term->fakeF4();
	return S_OK;
}

STDMETHODIMP CVt6530Control::FakeF5()
{
	term->fakeF5();
	return S_OK;
}

STDMETHODIMP CVt6530Control::FakeF6()
{
	term->fakeF6();
	return S_OK;
}

STDMETHODIMP CVt6530Control::FakeF7()
{
	term->fakeF7();
	return S_OK;
}

STDMETHODIMP CVt6530Control::FakeF8()
{
	term->fakeF8();
	return S_OK;
}

STDMETHODIMP CVt6530Control::FakeF9()
{
	term->fakeF9();
	return S_OK;
}

STDMETHODIMP CVt6530Control::FakeF10()
{
	term->fakeF10();
	return S_OK;
}

STDMETHODIMP CVt6530Control::FakeF11()
{
	term->fakeF11();
	return S_OK;
}

STDMETHODIMP CVt6530Control::FakeF12()
{
	term->fakeF12();
	return S_OK;
}

STDMETHODIMP CVt6530Control::FakeF13()
{
	term->fakeF13();
	return S_OK;
}

STDMETHODIMP CVt6530Control::FakeF14()
{
	term->fakeF14();
	return S_OK;
}

STDMETHODIMP CVt6530Control::FakeF15()
{
	term->fakeF15();
	return S_OK;
}

STDMETHODIMP CVt6530Control::FakeF16()
{
	term->fakeF16();
	return S_OK;
}

STDMETHODIMP CVt6530Control::FakeEnter()
{
	term->fakeEnter();
	return S_OK;
}

STDMETHODIMP CVt6530Control::FakeBackspace()
{
	term->fakeBackspace();
	return S_OK;
}

STDMETHODIMP CVt6530Control::FakeTab()
{
	term->fakeTab();
	return S_OK;
}

STDMETHODIMP CVt6530Control::FakeUpArrow()
{
	term->fakeUpArrow();
	return S_OK;
}

STDMETHODIMP CVt6530Control::FakeDownArrow()
{
	term->fakeDownArrow();
	return S_OK;
}

STDMETHODIMP CVt6530Control::FakeLeftArrow()
{
	term->fakeLeftArrow();
	return S_OK;
}

STDMETHODIMP CVt6530Control::FakeRightArrow()
{
	term->fakeRightArrow();
	return S_OK;
}

STDMETHODIMP CVt6530Control::FakeHome()
{
	term->fakeHome();
	return S_OK;
}

STDMETHODIMP CVt6530Control::FakeEnd()
{
	term->fakeEnd();
	return S_OK;
}

STDMETHODIMP CVt6530Control::FakeInsert()
{
	term->fakeInsert();
	return S_OK;
}

STDMETHODIMP CVt6530Control::FakeDelete()
{
	term->fakeDelete();
	return S_OK;
}

STDMETHODIMP CVt6530Control::toHTML(BSTR *sHTML)
{
	CComBSTR bstr;
	StringBuffer sb;
	term->toHTML(&sb);
	bstr.Append(sb);
	*sHTML = bstr.Detach();
	return S_OK;
}

STDMETHODIMP CVt6530Control::getScreenNumber(BSTR *sScreenNum)
{
	StringBuffer sb;
	term->getSubString(21, 3, 4, &sb);
	CComBSTR ret(sb);
	*sScreenNum = ret.Detach();
	return S_OK;
}

STDMETHODIMP CVt6530Control::wait34()
{
	Sleep(1000);
	return S_OK;
}

