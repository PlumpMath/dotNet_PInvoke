#include "stdafx.h"
#include <conio.h>
#include <objbase.h>
#include <atlbase.h>
#import "mscorlib.tlb"
#import "Exception2HRESULT.tlb" no_namespace

void TestHResult()
{
	// ����COM����ʵ��
	IExceptions2HResultPtr comObj(__uuidof(Exceptions2HResultObj));
	if (comObj)
	{
		HRESULT hr;
		wprintf(L"1. ���÷�����ThrowExceptionForHR() - ����errorCode = S_FALSE (1) ...\n");
		hr = comObj->ThrowExceptionForHR(1);
		wprintf(L"HRESULT����ֵ��0x%x\n", hr);
		wprintf(L"\n");

		wprintf(L"2. ���÷�����ReturnHResult() - ����Ϊ(1) ...\n");
		hr = comObj->ReturnHResult(1);
		wprintf(L"HRESULT����ֵ��0x%x\n", hr);
		wprintf(L"\n");

		wprintf(L"3. ���÷�����ReturnHResult() - ����Ϊ(-1) ...\n");
		hr = comObj->ReturnHResult(-1);
		wprintf(L"HRESULT����ֵ��0x%x\n", hr);
		wprintf(L"\n");

		comObj.Release();
	}
}