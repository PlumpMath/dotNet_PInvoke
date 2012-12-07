#include "stdafx.h"
#include <conio.h>
#include <objbase.h>
#include <atlbase.h>
#import "mscorlib.tlb"
#import "MarshalOptionalParam.tlb" no_namespace

void TestOptionalParam()
{
	// ����COM����ʵ��
	IMarshalOptionalPtr comObj(__uuidof(MarshalOptionalObj));
	if (comObj)
	{
        // �����򷽷����ݿ�ѡ������VARIANTֵ 
		VARIANT varOptional;
		varOptional.vt = VT_ERROR;
		varOptional.scode = DISP_E_PARAMNOTFOUND; 
		
		int result = comObj->AddVariantOptional(1, varOptional);
		wprintf(L"һ�����������%d\n", result);
		
		result = comObj->AddVariantOptional(1, 2);
		wprintf(L"�������������%d\n", result);

		comObj.Release();
	}
}