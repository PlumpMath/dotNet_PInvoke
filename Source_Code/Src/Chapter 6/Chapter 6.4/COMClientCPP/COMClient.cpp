// COMClient.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <conio.h>
#include <objbase.h>
#include <atlbase.h>
#include <locale.h>
#import "mscorlib.tlb"
#import "MarshalCommonType.tlb" no_namespace

int _tmain(int argc, _TCHAR* argv[])
{
	_wsetlocale(LC_ALL, L"chs");

	::CoInitializeEx(NULL, COINIT_APARTMENTTHREADED);
	 
	// ������ͨ��������
	wprintf(L"------------------ ������ͨ�������� ---------------------\n");
	TestCommonTypes();
	wprintf(L"\n");

	// ���Բ�������
	wprintf(L"------------------ ���Բ����������� ---------------------\n");
	TestDirection();
	wprintf(L"\n");

	// �����ַ���
	wprintf(L"------------------ �����ַ��� ---------------------\n");
	TestString();
	wprintf(L"\n");

	// ����SafeArray
	wprintf(L"------------------ ����SafeArray ---------------------\n");
	TestSafeArray();
	wprintf(L"\n");

	// ����Variant
	wprintf(L"------------------ ����Variant ---------------------\n");
	TestVariant();
	wprintf(L"\n");

	// ����class
	wprintf(L"------------------ ����class ---------------------\n");
	TestClass();
	wprintf(L"\n");

	// ����struct
	wprintf(L"------------------ ����struct ---------------------\n");
	TestStruct();
	wprintf(L"\n");

	// �����¼�
	wprintf(L"------------------ �����¼� ---------------------\n");
	TestEvent();
	wprintf(L"\n");

	// ���Կ�ѡ����
	wprintf(L"------------------ ���Կ�ѡ���� ---------------------\n");
	TestOptionalParam();
	wprintf(L"\n");
	
	// ���Լ���
	wprintf(L"------------------ ���Լ��� ---------------------\n");
	TestCollection();
	wprintf(L"\n");

	::CoUninitialize();

	wprintf(L"\n��������˳�...\n");
	_getch();

	return 0;
}



