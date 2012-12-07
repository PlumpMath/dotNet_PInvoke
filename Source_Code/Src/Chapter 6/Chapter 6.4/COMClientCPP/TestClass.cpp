#include "stdafx.h"
#include <conio.h>
#include <objbase.h>
#include <atlbase.h>
#import "mscorlib.tlb"
#import "MarshalStructure.tlb" no_namespace

void TestClass()
{
	// ����COM����ʵ��
	IMarshalClassPtr comObj(__uuidof(MarshalClassObj));
	if (comObj)
	{
		IDataClassPtr pObj = comObj->CreateInstance();
		wprintf(L"����Class����obj->IntegerValue = %d, obj->StringValue = %s\n", pObj->IntegerValue, (wchar_t*)pObj->StringValue);

		comObj->UpdateInstance(pObj);
		wprintf(L"�޸�Class����obj->IntegerValue = %d, obj->StringValue = %s\n", pObj->IntegerValue, (wchar_t*)pObj->StringValue);

		comObj.Release();
	}
}