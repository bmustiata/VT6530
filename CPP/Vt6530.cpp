// Vt6530.cpp : Implementation of CVt6530
#include "stdafx.h"
#include "comdef.h"
#include "Vt6530_Terminal_Proj.h"
#include "Vt6530.h"


/////////////////////////////////////////////////////////////////////////////
// CVt6530

HRESULT CVt6530::FinalConstruct()
{
	HRESULT ret = CoCreateFreeThreadedMarshaler(GetControllingUnknown(), &m_pUnkMarshaler.p);
	WSADATA data;
	if (WSAStartup(2, &data) != 0)
	{
		AtlReportError(CLSID_Vt6530, "Can't init sockets", IID_IVt6530, E_FAIL);
		return E_FAIL;
	}
	if ((tcb = new TermCallBackB(this)) == NULL)
	{
		return E_FAIL;
	}
	if ((term = new Term()) == NULL)
	{
		return E_FAIL;
	}
	term->addListener(tcb);
	WaitForSingleObject(hEvent, 5000);
	return ret;
}

void CVt6530::FinalRelease()
{
	CloseHandle(hEvent);
	term->close();
	delete term;
	delete tcb;
	WSACleanup();
	m_pUnkMarshaler.Release();
}

STDMETHODIMP CVt6530::InterfaceSupportsErrorInfo(REFIID riid)
{
	static const IID* arr[] = 
	{
		&IID_IVt6530
	};
	for (int i=0; i < sizeof(arr) / sizeof(arr[0]); i++)
	{
		if (InlineIsEqualGUID(*arr[i],riid))
			return S_OK;
	}
	return S_FALSE;
}

STDMETHODIMP CVt6530::getScreenNumber(BSTR *sScreenNum)
{
	StringBuffer sb;
	term->getSubString(21, 3, 4, &sb);
	CComBSTR ret(sb);
	*sScreenNum = ret.Detach();
	return S_OK;
}

STDMETHODIMP CVt6530::toHTML(BSTR *sHTML)
{
	CComBSTR bstr;
	StringBuffer sb;
	term->toHTML(&sb);
	bstr.Append(sb);
	*sHTML = bstr.Detach();
	return S_OK;
}

STDMETHODIMP CVt6530::FakeDelete()
{
	term->fakeDelete();

	return S_OK;
}

STDMETHODIMP CVt6530::FakeInsert()
{
	term->fakeInsert();

	return S_OK;
}

STDMETHODIMP CVt6530::FakeHome()
{
	term->fakeHome();

	return S_OK;
}

STDMETHODIMP CVt6530::FakeEnd()
{
	term->fakeEnd();

	return S_OK;
}

STDMETHODIMP CVt6530::FakeRightArrow()
{
	term->fakeRightArrow();

	return S_OK;
}

STDMETHODIMP CVt6530::FakeLeftArrow()
{
	term->fakeLeftArrow();

	return S_OK;
}

STDMETHODIMP CVt6530::FakeDownArrow()
{
	term->fakeDownArrow();

	return S_OK;
}

STDMETHODIMP CVt6530::FakeUpArrow()
{
	term->fakeUpArrow();

	return S_OK;
}

STDMETHODIMP CVt6530::FakeTab()
{
	term->fakeTab();

	return S_OK;
}

STDMETHODIMP CVt6530::FakeBackspace()
{
	term->fakeBackspace();
	return S_OK;
}

STDMETHODIMP CVt6530::FakeEnter()
{
	term->fakeEnter();
	return S_OK;
}

STDMETHODIMP CVt6530::FakeF1()
{
	term->fakeF1();
	return S_OK;
}

STDMETHODIMP CVt6530::FakeF2()
{
	term->fakeF2();
	return S_OK;
}

STDMETHODIMP CVt6530::FakeF3()
{
	term->fakeF3();
	return S_OK;
}

STDMETHODIMP CVt6530::FakeF4()
{
	term->fakeF4();
	return S_OK;
}

STDMETHODIMP CVt6530::FakeF5()
{
	term->fakeF5();
	return S_OK;
}

STDMETHODIMP CVt6530::FakeF6()
{
	term->fakeF6();
	return S_OK;
}

STDMETHODIMP CVt6530::FakeF7()
{
	term->fakeF7();
	return S_OK;
}

STDMETHODIMP CVt6530::FakeF8()
{
	term->fakeF8();
	return S_OK;
}

STDMETHODIMP CVt6530::FakeF9()
{
	term->fakeF9();
	return S_OK;
}

STDMETHODIMP CVt6530::FakeF10()
{
	term->fakeF10();
	return S_OK;
}

STDMETHODIMP CVt6530::FakeF11()
{
	term->fakeF11();
	return S_OK;
}

STDMETHODIMP CVt6530::FakeF12()
{
	term->fakeF12();
	return S_OK;
}

STDMETHODIMP CVt6530::FakeF13()
{
	term->fakeF13();
	return S_OK;
}

STDMETHODIMP CVt6530::FakeF14()
{
	term->fakeF14();
	return S_OK;
}

STDMETHODIMP CVt6530::FakeF15()
{
	term->fakeF15();
	return S_OK;
}

STDMETHODIMP CVt6530::FakeF16()
{
	term->fakeF16();
	return S_OK;
}

STDMETHODIMP CVt6530::FakeKeys(BSTR sKeys)
{
	StringBuffer sb(sKeys);

	try
	{
		term->fakeKeys(sb);
	}
	catch (Exception e)
	{
		AtlReportError(CLSID_Vt6530, e.getMessage(), IID_IVt6530, E_FAIL);
		//CComBSTR str(e.getMessage());
		//Fire_Debug(str.Detach());
		return E_FAIL;
	}
	catch (...)
	{
		AtlReportError(CLSID_Vt6530, "Unknown exception in FakeKeys", IID_IVt6530, E_FAIL);
		//CComBSTR str("Unknown exception in FakeKeys");
		//Fire_Error(str.Detach());
		return E_FAIL;
	}
	return S_OK;
}

STDMETHODIMP CVt6530::FakeKey(int iKeyCode, BOOL bShift, BOOL bAlt, BOOL bCtrl)
{
	try
	{
		term->fakeKey(iKeyCode, bShift == TRUE, bAlt == TRUE, bCtrl == TRUE);
	}
	catch (Exception e)
	{
		AtlReportError(CLSID_Vt6530, e.getMessage(), IID_IVt6530, E_FAIL);
		//CComBSTR str(e.getMessage());
		//Fire_Debug(str.Detach());
		return E_FAIL;
	}
	catch (...)
	{
		AtlReportError(CLSID_Vt6530, "Unknown exception in FakeKey", IID_IVt6530, E_FAIL);
		//CComBSTR str("Unknown exception in FakeKey");
		//Fire_Error(str.Detach());
		return E_FAIL;
	}
	return S_OK;
}

STDMETHODIMP CVt6530::SendCommand(BSTR sCommand)
{
	StringBuffer sb(sCommand);
	try
	{
		term->sendCommandLine(sb);
	}
	catch (Exception e)
	{
		AtlReportError(CLSID_Vt6530, e.getMessage(), IID_IVt6530, E_FAIL);
		//CComBSTR str(e.getMessage());
		//Fire_Debug(str.Detach());
		return E_FAIL;
	}
	catch (...)
	{
		AtlReportError(CLSID_Vt6530, "Unknown exception in SendCommandLine", IID_IVt6530, E_FAIL);
		//CComBSTR str("Unknown exception in SendCommandLine");
		//Fire_Error(str.Detach());
		return E_FAIL;
	}
	return S_OK;
}

STDMETHODIMP CVt6530::get_Column(int *pVal)
{
	*pVal = term->getCursorX() + 1;
	return S_OK;
}

STDMETHODIMP CVt6530::put_Column(int newVal)
{
	term->setCursorX(newVal - 1);
	return S_OK;
}

STDMETHODIMP CVt6530::get_Row(int *pVal)
{
	*pVal = term->getCursorY() + 1;
	return S_OK;
}

STDMETHODIMP CVt6530::put_Row(int newVal)
{
	term->setCursorY(newVal - 1);
	return S_OK;
}

STDMETHODIMP CVt6530::CursorToField(int iIndex)
{
	term->cursorToField(iIndex);
	return S_OK;
}

STDMETHODIMP CVt6530::IsFieldChanged(int iIndex, BOOL *bIsIt)
{
	if (term->isFieldChanged(iIndex))
	{
		*bIsIt = TRUE;
	}
	else
	{
		*bIsIt = FALSE;
	}
	return S_OK;
}

STDMETHODIMP CVt6530::get_Line(int iIndex, BSTR *pVal)
{
	if (iIndex <= 0 || iIndex > term->getNumRows())
	{
		AtlReportError(CLSID_Vt6530, "Invalid row", IID_IVt6530, E_FAIL);
		return E_FAIL;
	}
	StringBuffer sb;
	CComBSTR bstr;
	term->getLine(iIndex-1, &sb);
	bstr.Append(sb);
	*pVal = bstr.Detach();
	return S_OK;
}

STDMETHODIMP CVt6530::get_UnprotectField(int iIndex, BSTR *pVal)
{
	StringBuffer sb;
	CComBSTR bstr;

	term->getUnprotectField(iIndex, &sb);

	bstr.Append(sb);
	*pVal = bstr.Detach();
	return S_OK;
}

STDMETHODIMP CVt6530::put_UnprotectField(int iIndex, BSTR newVal)
{
	return E_NOTIMPL;
}

STDMETHODIMP CVt6530::get_CurrentField(BSTR *pVal)
{
	StringBuffer sb;
	term->getCurrentField(&sb);
	return S_OK;
}

STDMETHODIMP CVt6530::put_CurrentField(BSTR newVal)
{
	return E_NOTIMPL;
}

STDMETHODIMP CVt6530::getRGB(int iAttribute, int *irgb)
{
	return E_NOTIMPL;
}

STDMETHODIMP CVt6530::IsUpshift(int iAttribute, BOOL *bIsIt)
{
	return E_NOTIMPL;
}

STDMETHODIMP CVt6530::IsUnderLine(int iAttribute, BOOL *bIsit)
{
	return E_NOTIMPL;
}

STDMETHODIMP CVt6530::IsBlinking(int iAttribute, BOOL *bIsit)
{
	return E_NOTIMPL;
}

STDMETHODIMP CVt6530::IsInvis(int iAttribute, BOOL *bIsit)
{
	return E_NOTIMPL;
}

STDMETHODIMP CVt6530::IsReverse(int iAttribute, BOOL *bIsit)
{
	return E_NOTIMPL;
}

STDMETHODIMP CVt6530::get_FieldAttributes(int iIndex, int *pVal)
{
	*pVal = term->getFieldAttributes(iIndex);
	return S_OK;
}

STDMETHODIMP CVt6530::put_FieldAttributes(int iIndex, int newVal)
{
	return E_NOTIMPL;
}

STDMETHODIMP CVt6530::get_Field(int iIndex, BSTR *pVal)
{
	StringBuffer sb;
	CComBSTR bstr;

	term->getField(iIndex, &sb);
	
	bstr.Append(sb);
	*pVal = bstr.Detach();

	return S_OK;
}

STDMETHODIMP CVt6530::put_Field(int iIndex, BSTR newVal)
{
	StringBuffer sb(newVal);
	term->setField(iIndex, sb);

	return S_OK;
}

STDMETHODIMP CVt6530::getAttributeDump(BSTR *sScreenAttrs)
{
	StringBuffer sb;
	CComBSTR bstr;

	term->scrapeAttributes(&sb);

	bstr.Append(sb);
	*sScreenAttrs = bstr.Detach();
	return S_OK;
}

STDMETHODIMP CVt6530::getScreenDump(BSTR *sScreenChars)
{
	StringBuffer sb;
	CComBSTR bstr;

	term->scrapeScreen(&sb);

	bstr.Append(sb);
	*sScreenChars = bstr.Detach();
	return S_OK;
	return S_OK;
}

STDMETHODIMP CVt6530::Disconnect()
{
	try
	{
		term->close();
	}
	catch (Exception e)
	{
		AtlReportError(CLSID_Vt6530Control, e.getMessage(), IID_IVt6530Control, E_FAIL);
		//CComBSTR str(e.getMessage());
		//Fire_Debug(str.Detach());
		return E_FAIL;
	}
	catch (...)
	{
		AtlReportError(CLSID_Vt6530Control, "Unknown exception in Disconnect", IID_IVt6530Control, E_FAIL);
		//CComBSTR str("Unknown exception in Disconnect");
		//Fire_Error(str.Detach());
		return E_FAIL;
	}
	return S_OK;
}

STDMETHODIMP CVt6530::Connect(int iTimeOut)
{
	ResetEvent(hEvent);

	try
	{
		term->connect();
	}
	catch (Exception e)
	{
		AtlReportError(CLSID_Vt6530Control, e.getMessage(), IID_IVt6530Control, E_FAIL);
		//CComBSTR str(e.getMessage());
		//Fire_Error(str.Detach());
		return E_FAIL;
	}
	catch (...)
	{
		AtlReportError(CLSID_Vt6530Control, "Unknown exception in Connect", IID_IVt6530Control, E_FAIL);
		//CComBSTR str("Unknown exception in Connect");
		//Fire_Error(str.Detach());
		return E_FAIL;
	}
	WaitForSingleObject(hEvent, 5000);
	return S_OK;
}

STDMETHODIMP CVt6530::get_Port(int *pVal)
{
	*pVal = term->getPort();
	return S_OK;
}

STDMETHODIMP CVt6530::put_Port(int newVal)
{
	term->setPort(newVal);
	return S_OK;
}

STDMETHODIMP CVt6530::get_Host(BSTR *pVal)
{
	CComBSTR bstr;
	char *hostName;

	hostName = term->getHost();

	bstr.Append(hostName);
	*pVal = bstr.Detach();

	return S_OK;
}

STDMETHODIMP CVt6530::put_Host(BSTR newVal)
{
	_bstr_t val(newVal);
	term->setHost(val);
	return S_OK;
}

STDMETHODIMP CVt6530::waitENQ()
{
	Sleep(1000);
	bool ret = term->waitENQ();
	if (ret)
	{
		return S_OK;
	}
	else
	{
		AtlReportError(CLSID_Vt6530, "Timeout", IID_IVt6530, E_FAIL);
		return E_FAIL;
	}
	return S_OK;
}

STDMETHODIMP CVt6530::wait34()
{
	Sleep(1000);
	bool ret = term->wait34();
	if (ret)
	{
		return S_OK;
	}
	else
	{
		AtlReportError(CLSID_Vt6530, "Timeout", IID_IVt6530, E_FAIL);
		return E_FAIL;
	}
	return S_OK;	
}
