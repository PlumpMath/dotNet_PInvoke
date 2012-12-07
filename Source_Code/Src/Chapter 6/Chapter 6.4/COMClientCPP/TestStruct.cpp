#include "stdafx.h"
#include <conio.h>
#include <objbase.h>
#include <atlbase.h>
#import "mscorlib.tlb"
#import "MarshalStructure.tlb" no_namespace

void TestStruct()
{
	// ����COM����ʵ��
	IMarshalStructPtr comObj(__uuidof(MarshalStructObj));
	if (comObj)
	{
		DataStruct obj = comObj->CreateInstance();
		wprintf(L"����Struct����obj.IntegerValue = %d, obj.StringValue = %s\n", obj.IntegerValue, (wchar_t*)obj.StringValue);

		comObj->UpdateInstance(&obj);
		wprintf(L"�޸�Struct����obj.IntegerValue = %d, obj.StringValue = %s\n", obj.IntegerValue, (wchar_t*)obj.StringValue);

		comObj.Release();
	}
}