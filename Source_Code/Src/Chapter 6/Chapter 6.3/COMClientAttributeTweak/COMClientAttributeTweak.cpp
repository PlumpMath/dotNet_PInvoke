// COMClientAttributeTweak.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <conio.h>
#include <locale.h>
#include <objbase.h>
#include <atlbase.h>
#import "mscorlib.tlb"
#import "AttributeTweakCOM.tlb" no_namespace

int _tmain(int argc, _TCHAR* argv[])
{
	_wsetlocale(LC_ALL, L"chs");

	wprintf(L"------------------ �������Կ������ɵ����Ϳ�Ԫ���� ------------------\n");

	::CoInitializeEx(NULL, COINIT_APARTMENTTHREADED);

	ITweakCOMPtr comObj(__uuidof(TweakCOMObj));

	if(comObj)
	{
		wprintf(L"����ExposedMethod����\n");
		comObj->ExposedMethod();

		wprintf(L"����DemoProperty���Ե�ֵ\n");
		int result = comObj->DemoProperty;

		// �����Եĸ�ֵ���ᵼ�±���������
		// ��Ϊ�����Ե�put������������ΪComVisible(false)
		//comObj->DemoProperty = 0;

		comObj.Release();
	}

	::CoUninitialize();

	wprintf(L"\n��������˳�...\n");
	_getch();

	return 0;
}

