#include "stdafx.h"
#include <conio.h>
#include <objbase.h>
#include <atlbase.h>
#import "mscorlib.tlb"
#import "MarshalString.tlb" no_namespace

void TestString()
{
	// ����COM����ʵ��
	IMarshalStringPtr comObj(__uuidof(MarshalStringObj));
	if (comObj)
	{
		BSTR strInArg = L"һ����";
		
		BSTR strRefArg = L"δ��ֵ";
		comObj->StringRef(strInArg, &strRefArg);
		wprintf(L"����: StringRef            strInArg = %s, strResult = %s\n", strInArg, strRefArg);

		BSTR strOutArg = L"δ��ֵ";
		comObj->StringOut(strInArg, &strOutArg);
		wprintf(L"����: StringOut            strInArg = %s, strResult = %s\n", strInArg, strOutArg);

		BSTR strReturn = comObj->StringReturn(strInArg);
		wprintf(L"����: StringReturn         strInArg = %s, strResult = %s\n", strInArg, strReturn);

		// ע�������ڴ治��ֻ����ղ������������йܴ�����StringBuilder��CapacityΪ0
		// Capacity�Ĵ�С�����ڴ��е����ݳ��Ⱦ����ġ������У���Ȼ����100��˫�ֽڵĿռ䣬
		// ���ڷ��͵��йܴ����е�CapacityΪ80
		wchar_t* pbuffer = new wchar_t[100];
		wmemset(pbuffer, 0, 100);
		wmemset(pbuffer, L'a', 80);
		comObj->StringStringBuilder(strInArg, pbuffer);
		wprintf(L"����: StringStringBuilder  strInArg = %s, strResult = %s\n", strInArg, pbuffer);
		delete[] pbuffer;

		wchar_t* pResult = comObj->StringCStyle(L"һ����");
		wprintf(L"����: StringCStyle         strInArg = %s, strResult = %s\n", strInArg, pResult);
		// �����ͷŷ��ص��ڴ�ռ�
		CoTaskMemFree(pResult);

		comObj.Release();
	}
}