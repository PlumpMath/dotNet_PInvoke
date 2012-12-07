#include "stdafx.h"
#include <conio.h>
#include <objbase.h>
#include <atlbase.h>
#import "mscorlib.tlb"
#import "MarshalCommonType.tlb" no_namespace

void TestCommonTypes()
{
	// ����COM����ʵ��
	IMarshalCommonTypePtr comObj(__uuidof(MarshalCommonTypeObj));
	if (comObj)
	{
		// ��������
		//wprintf(L"����");
		wprintf(L"���������COM�е����ͣ�long��");
		comObj->MarshalInteger(24);
		// �����ַ�
		wprintf(L"���������COM�е����ͣ�unsign short��");
		comObj->MarshalChar('C');
		// �����ַ���
		wprintf(L"���������COM�е����ͣ�BSTR��");
		comObj->MarshalString("�ַ���");
		// ����DATEʱ��
		wprintf(L"���������COM�е����ͣ�DATE��");
		comObj->MarshalDate((DATE)39668.833333333336);
		// ����DECIMAL
		wprintf(L"���������COM�е����ͣ�DECIMAL��");
		DECIMAL dec;
		double d = 2008.88;
		// ����DECIMAL
		VarDecFromR8(d, &dec);
		comObj->MarshalDecimal(dec);
		// ����CURRENCY
		wprintf(L"���������COM�е����ͣ�CURRENCY��");
		CURRENCY cur;
		// ����CURRENCY
		VarCyFromR8(d, &cur);
		comObj->MarshalCurrecny(cur);

		comObj.Release();
	}
}