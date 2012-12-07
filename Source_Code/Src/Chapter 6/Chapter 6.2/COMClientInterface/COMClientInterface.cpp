// COMClientInterface.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <conio.h>
#include <locale.h>
#include <objbase.h>
#include <atlbase.h>
#import "mscorlib.tlb"
#import "ExposeInterface.tlb" no_namespace

int _tmain(int argc, _TCHAR* argv[])
{
	_wsetlocale(LC_ALL, L"chs");

	wprintf(L"------------------ ���ýӿڶ���COM ------------------\n");

	::CoInitializeEx(NULL, COINIT_APARTMENTTHREADED);

	IWeatherManagerPtr comObj(__uuidof(WeatherManagerObj));

	if(comObj)
	{
		double cTemp = comObj->GetTemperatureCELSIUS();
		wprintf(L"��ȡ�����¶ȣ�%.2f\n", cTemp);

		double fTemp = comObj->ConvertCelsius2Fahrenheit(cTemp);
		wprintf(L"���㻪���¶ȣ�%.2f\n", fTemp);

		int count = comObj->OperationCount;
		wprintf(L"��ִ�к�������%d�Ρ�\n", count);

		comObj.Release();
	}

	::CoUninitialize();

	wprintf(L"\n��������˳�...\n");
	_getch();

	return 0;
}

