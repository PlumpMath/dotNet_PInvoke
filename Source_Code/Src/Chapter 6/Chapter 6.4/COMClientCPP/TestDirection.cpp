#include "stdafx.h"
#include <conio.h>
#include <objbase.h>
#include <atlbase.h>
#import "mscorlib.tlb"
#import "MarshalDirection.tlb" no_namespace

void TestDirection()
{
	// ����COM����ʵ��
	IMarshalDirectionPtr comObj(__uuidof(MarshalDirectionObj));
	if (comObj)
	{
		long intInArg = 10;
		long intOutArg = 0;

		comObj->IntegerDefault(intInArg, intOutArg);
		wprintf(L"����: IntegerDefault  intInArg = %d intOutArg = %d\n", intInArg, intOutArg);

		intOutArg = 0;
		comObj->IntegerInOut(intInArg, intOutArg);
		wprintf(L"����: IntegerInOut    intInArg = %d intOutArg = %d\n", intInArg, intOutArg);

		intOutArg = 0;
		comObj->IntegerRef(intInArg, &intOutArg);
		wprintf(L"����: IntegerRef      intInArg = %d intOutArg = %d\n", intInArg, intOutArg); 

		intOutArg = 0;
		comObj->IntegerRefIn(intInArg, &intOutArg);
		wprintf(L"����: IntegerRefIn    intInArg = %d, intOutArg = %d\n", intInArg, intOutArg);

		intOutArg = 0;
		comObj->IntegerOut(intInArg, &intOutArg);
		wprintf(L"����: IntegerOut      intInArg = %d, intOutArg = %d\n", intInArg, intOutArg);


		BSTR strInArg = L"һ����";
		BSTR strOutArg = L"δ��ֵ";

		comObj->StringDefault(strInArg, strOutArg);
		wprintf(L"����: StringDefault   strInArg = %s, strOutArg = %s\n", strInArg, strOutArg);

		strOutArg = L"δ��ֵ";
		comObj->StringInOut(strInArg, strOutArg);
		wprintf(L"����: StringInOut     strInArg = %s, strOutArg = %s\n", strInArg, strOutArg);

		strOutArg = L"δ��ֵ";
		comObj->StringRef(strInArg, &strOutArg);
		wprintf(L"����: StringRef       strInArg = %s, strOutArg = %s\n", strInArg, strOutArg); 

		strOutArg = L"δ��ֵ";
		comObj->StringRefIn(strInArg, &strOutArg);
		wprintf(L"����: StringRefIn     strInArg = %s, strOutArg = %s\n", strInArg, strOutArg);

		strOutArg = L"δ��ֵ";
		comObj->StringOut(strInArg, &strOutArg);
		wprintf(L"����: StringOut       strInArg = %s, strOutArg = %s\n", strInArg, strOutArg);
		
		comObj.Release();
	}
}