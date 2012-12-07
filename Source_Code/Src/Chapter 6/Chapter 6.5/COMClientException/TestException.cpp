#include "stdafx.h"
#include <conio.h>
#include <objbase.h>
#include <atlbase.h>
#import "mscorlib.tlb"
#import "Exception2HRESULT.tlb" no_namespace

void PrintErrorInfo(_com_error e)
{
	wprintf(L"����Դ��%s\n", (wchar_t*)e.Source());
	wprintf(L"������Ϣ��%s��HResult��0x%x\n", e.ErrorMessage(), e.Error());
	wprintf(L"����������%s\n", (wchar_t*)e.Description());
}

void TestException()
{
	// ����COM����ʵ��
	IExceptions2HResultPtr comObj(__uuidof(Exceptions2HResultObj));
	if (comObj)
	{
		wprintf(L"1. ���÷�����ThrowCommonException() ...\n");
		try
		{
			comObj->ThrowCommonException();
		}
		catch(_com_error &e)
		{
			PrintErrorInfo(e);
		}

		wprintf(L"\n");
		wprintf(L"2. ���÷�����ThrowMyHResultException() ...\n");
		try
		{
			comObj->ThrowMyHResultException();
		}
		catch(_com_error &e)
		{
			PrintErrorInfo(e);
		}

		wprintf(L"\n");
		wprintf(L"3. ���÷�����ThrowExceptionForHR() ...\n");
		try
		{
			comObj->ThrowExceptionForHR(0x80051061L);
		}
		catch(_com_error &e)
		{
			PrintErrorInfo(e);
		}

		comObj.Release();
	}
}

