#include "stdafx.h"
#include <conio.h>
#include <objbase.h>
#include <atlbase.h>
#import "mscorlib.tlb"
#import "MarshalVariant.tlb" no_namespace named_guids

void TestVariant()
{
	// ����COM����ʵ��
	IMarshalVariantPtr comObj(__uuidof(MarshalVariantObj));
	if (comObj)
	{
		VARIANT* varOutArg = new VARIANT();
		
		VARIANT varInt;
		varInt.vt = VT_I4;
		varInt.lVal = 10;
		_bstr_t typeName = comObj->MarshalVariant(varInt, varOutArg);
		wprintf(L"Variant���ͣ�VT_I4, �й����ͣ�%s\n", (wchar_t*)typeName);

		VARIANT varShort;
		varShort.vt = VT_I2;
		varShort.lVal = 10;
		typeName = comObj->MarshalVariant(varShort, varOutArg);
		wprintf(L"Variant���ͣ�VT_I2, �й����ͣ�%s\n", (wchar_t*)typeName);

		VARIANT varSingle;
		varSingle.vt = VT_R4;
		varSingle.fltVal = 10.0f;
		typeName = comObj->MarshalVariant(varSingle, varOutArg);
		wprintf(L"Variant���ͣ�VT_R4, �й����ͣ�%s\n", (wchar_t*)typeName);

		VARIANT varDouble;
		varDouble.vt = VT_R8;
		varDouble.dblVal = 10.0f;
		typeName = comObj->MarshalVariant(varDouble, varOutArg);
		wprintf(L"Variant���ͣ�VT_R4, �й����ͣ�%s\n", (wchar_t*)typeName);

		VARIANT varDate;
		varDate.vt = VT_DATE;
		varDate.date = (DATE)39668.833333333336;
		typeName = comObj->MarshalVariant(varDate, varOutArg);
		wprintf(L"Variant���ͣ�VT_DATE, �й����ͣ�%s\n", (wchar_t*)typeName);

		// ����DECIMAL
		VARIANT varDecimal;
		varDecimal.vt = VT_DECIMAL;
		DECIMAL dec;
		VarDecFromI4(2008, &dec);
		dec.wReserved = VT_DECIMAL;
		varDecimal.decVal = dec;
		typeName = comObj->MarshalVariant(varDecimal, varOutArg);
		wprintf(L"Variant���ͣ�VT_DECIMAL, �й����ͣ�%s\n", (wchar_t*)typeName);

		VARIANT varString;
		varString.vt = VT_BSTR;
		varString.bstrVal = L"abc";
		typeName = comObj->MarshalVariant(varString, varOutArg);
		wprintf(L"Variant���ͣ�VT_BSTR, �й����ͣ�%s\n", (wchar_t*)typeName);
		
		// .NET������Variant���͵�֧��
		// 1. CurrencyWrapper
		// ����CURRENCY
		VARIANT varCurrency;
		varCurrency.vt = VT_CY;
		CY cy;
		VarCyFromI4(2008, &cy);
		varCurrency.cyVal = cy;
		typeName = comObj->MarshalVariantCurrency(varDecimal, varOutArg);
		wprintf(L"Variant���ͣ�VT_CY, �й����ͣ�%s, �����������ͣ�%d(VT_DECIMAL)\n", (wchar_t*)typeName, varOutArg->vt);

		// ʹ��CurrencyWrapper
		typeName = comObj->MarshalVariantCurrencyWrapper(varDecimal, varOutArg);
		wprintf(L"Variant���ͣ�VT_CY, �й����ͣ�%s, ������������(CurrencyWrapper)��%d(VT_CY)\n", (wchar_t*)typeName, varOutArg->vt);

		// 2. BSTRWrapper
		_variant_t ret = comObj->MarshalVariantStringNull();
		wprintf(L"����Null�ַ�����vt����Ϊ��%d(VT_EMPTY)\n", ret.vt);

		// ʹ��BSTRWrapper
		ret = comObj->MarshalVariantStringNullWrapper();
		wprintf(L"����Null�ַ���(BSTRWrapper)��vt����Ϊ��%d(VT_BSTR)\n", ret.vt);

		ret = comObj->MarshalVariantStringNonNullWrapper();
		wprintf(L"������ͨ�ַ���(BSTRWrapper)��vt����Ϊ��%d(VT_BSTR)\n", ret.vt);

		// 3. ʹ��VARIANT������
		IDemoItemExPtr inValue(__uuidof(DemoItemExObj));
		inValue->IntValue = 1;
		inValue->StringValue = L"�Ƽ���";

		VARIANT* inArg = new VARIANT();
		// �������ﴫ�ݵ��йܴ����еĶ�����COM�ӿڶ���
		// ���Ҫ����ΪVT_UNKNOWN����VT_DISPATCH
		// �����н�ʹ��VT_DISPATCH�������ö�Ӧ��pdispVal�ֶδ������
		// ���ʹ��VT_UNKNOWN����Ӧ�������Ӧ��punkVal�ֶδ������
		inArg->vt = VT_UNKNOWN;
		inArg->punkVal = inValue;

		wprintf(L"����%d ������%s\n", inValue->IntValue, (wchar_t*)inValue->StringValue);

		VARIANT *outArg = new VARIANT();
		outArg->vt = VT_UNKNOWN;
		outArg->punkVal = NULL;

		comObj->MarshalVariantClass(inArg, outArg);
		
		// ʹ��QueryInterface��ѯ�ӿ��һص�VARIANT�еĶ���
		IDemoItemExPtr outPtr;
		HRESULT hr = outArg->punkVal->QueryInterface(IID_IDemoItemEx, (LPVOID*)&outPtr);
		if(SUCCEEDED(hr))
		{
			wprintf(L"����%d ������%s\n", outPtr->IntValue, (wchar_t*)outPtr->StringValue);
		}

		delete inArg;
		delete outArg;

		delete varOutArg;

		comObj.Release();
	}
}