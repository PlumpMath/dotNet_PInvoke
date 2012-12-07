// COMClientLateBinding.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <conio.h>
#include <locale.h>
#include <objbase.h>
#include <atlbase.h>
#import "mscorlib.tlb"
#import "LateBinding.tlb" no_namespace

int _tmain(int argc, _TCHAR* argv[])
{
	_wsetlocale(LC_ALL, L"chs");

	wprintf(L"------------------ ����ClassInterface����COM - ���ڰ� ------------------\n");

	::CoInitializeEx(NULL, COINIT_APARTMENTTHREADED);

	// ����progID��ѯCLSID
	CLSID clsid;
	CLSIDFromProgID(L"LateBinding.WeatherManager", &clsid);

	// ��������ʵ��
	IDispatch* pIDispatch = NULL;
	CoCreateInstance(clsid, NULL, CLSCTX_INPROC_SERVER,
		IID_IDispatch, (void**)&pIDispatch);

	// 1. ����GetTemperatureCELSIUS����
	// ��ȡ������dispID
	DISPID dispId;
	OLECHAR* methodName = L"GetTemperatureCELSIUS";
	pIDispatch->GetIDsOfNames(IID_NULL, &methodName, 1,
		GetUserDefaultLCID(), &dispId);

	// ����GetTemperatureCELSIUS�����Ĳ���
	DISPPARAMS param;	
	param.cArgs = 0;
	param.rgvarg = NULL;
	param.cNamedArgs = 0;
	param.rgdispidNamedArgs = NULL;

	// ��Ž���ı���
	VARIANT varCTemp;
	VariantInit(&varCTemp);
	
	// ���÷���
	HRESULT hr = pIDispatch->Invoke(dispId, IID_NULL,
		GetUserDefaultLCID(), DISPATCH_METHOD, &param,
		&varCTemp, NULL, NULL);

	wprintf(L"��ȡ�����¶ȣ�%.2f\n", varCTemp.dblVal);

	// 2. ����ConvertCelsius2Fahrenheit����
	methodName = L"ConvertCelsius2Fahrenheit";
	pIDispatch->GetIDsOfNames(IID_NULL, &methodName, 1,
		GetUserDefaultLCID(), &dispId);

	// ����ConvertCelsius2Fahrenheit�����Ĳ���
	VARIANTARG cTemp[1];
	VariantInit(&cTemp[0]);
	cTemp[0].vt = VT_R8;
	cTemp[0].dblVal = varCTemp.dblVal;
	
	param.cArgs = 1;
	param.rgvarg = (VARIANTARG*)&cTemp;
	param.cNamedArgs = 0;
	param.rgdispidNamedArgs = NULL;

	// ��Ž���ı���
	VARIANT varFTemp;
	VariantInit(&varFTemp);

	// ���÷���
	hr = pIDispatch->Invoke(dispId, IID_NULL,
		GetUserDefaultLCID(), DISPATCH_METHOD, &param,
		&varFTemp, NULL, NULL);

	wprintf(L"���㻪���¶ȣ�%.2f\n", varFTemp.dblVal);

	// 3. ����OperationCount����
	methodName = L"OperationCount";
	pIDispatch->GetIDsOfNames(IID_NULL, &methodName, 1,
		GetUserDefaultLCID(), &dispId);

	// ����get_OperationCount�����Ĳ���
	param.cArgs = 0;
	param.rgvarg = NULL;
	param.cNamedArgs = 0;
	param.rgdispidNamedArgs = NULL;

	// ��Ž���ı���
	VARIANT varCount;
	VariantInit(&varCount);

	// ���÷���
	hr = pIDispatch->Invoke(dispId, IID_NULL,
		GetUserDefaultLCID(), DISPATCH_METHOD, &param,
		&varCount, NULL, NULL);

	wprintf(L"��ִ�к�������%d�Ρ�\n", varCount.lVal);

	// �ͷŶ���
	pIDispatch->Release();
	::CoUninitialize();

	wprintf(L"\n��������˳�...\n");
	_getch();
	return 0;

}

