// COMClientException.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <conio.h>
#include <objbase.h>
#include <atlbase.h>
#include <locale.h>
#import "mscorlib.tlb"

int _tmain(int argc, _TCHAR* argv[])
{
	_wsetlocale(LC_ALL, L"chs");

	::CoInitializeEx(NULL, COINIT_APARTMENTTHREADED);

	// �����쳣
	wprintf(L"------------------ �����쳣 ---------------------\n");
	TestException();
	wprintf(L"\n");

	// ���Է���HRESULT
	wprintf(L"------------------ ���Է���HRESULT ---------------------\n");
	TestHResult();
	wprintf(L"\n");
	
	::CoUninitialize();

	wprintf(L"\n��������˳�...\n");
	_getch();

	return 0;
}

