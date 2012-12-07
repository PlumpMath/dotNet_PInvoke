#include "stdafx.h"
#include <conio.h>
#include <objbase.h>
#include <atlbase.h>
#import "mscorlib.tlb"
#import "MarshalArray.tlb" no_namespace

// ��ǰ����
void PrintIntArray(SAFEARRAY* pArray);

void TestSafeArray()
{
	// ����COM����ʵ��
	IMarshalSafeArrayPtr comObj(__uuidof(MarshalSafeArrayObj));
	if (comObj)
	{
		// ����SafeArray
		SAFEARRAY* pIntArray;
		SAFEARRAYBOUND saBnd[1];
		saBnd[0].lLbound = 0;
		saBnd[0].cElements = 5;
		pIntArray = SafeArrayCreate(VT_I4, 1, saBnd);

		// ��ʼ��SafeArray
		int* pArray;
		SafeArrayAccessData(pIntArray, (void HUGEP**)&pArray);
		pArray[0] = 0;
		pArray[1] = 1;
		pArray[2] = 2;
		pArray[3] = 3;
		pArray[4] = 4;
		SafeArrayUnaccessData(pIntArray);

		wprintf(L"(0) ��ʼ������\n");
		PrintIntArray(pIntArray);

		int result = 0;
		result = comObj->IntArrayUpdateByVal(pIntArray);
		wprintf(L"Ԫ�غͣ�%d\n\n", result);
		wprintf(L"(1) ���÷��� IntArrayUpdateByVal\n");
		PrintIntArray(pIntArray);

		result = comObj->IntArrayUpdateByValInOut(pIntArray);
		wprintf(L"Ԫ�غͣ�%d\n\n", result);
		wprintf(L"(2) ���÷��� IntArrayUpdateByValInOut\n");
		PrintIntArray(pIntArray);

		result = comObj->IntArrayUpdateByRef(&pIntArray);
		wprintf(L"Ԫ�غͣ�%d\n\n", result);
		wprintf(L"(3) ���÷��� IntArrayUpdateByRef\n");
		PrintIntArray(pIntArray);

		result = comObj->IntArrayUpdateByRefInOnly(&pIntArray);
		wprintf(L"Ԫ�غͣ�%d\n\n", result);
		wprintf(L"(4) ���÷��� IntArrayUpdateByRefInOnly\n");
		PrintIntArray(pIntArray);

		// �ͷ�SafeArray
		SafeArrayDestroy(pIntArray);

		SAFEARRAY* pNewArray = comObj->IntArrayReturn(5);
		wprintf(L"\n(5) ���÷��� IntArrayReturn��\n");
		PrintIntArray(pNewArray);

		// �ͷ�SafeArray
		SafeArrayDestroy(pNewArray);

		comObj.Release();
	}
}

void PrintIntArray(SAFEARRAY* pIntArray)
{
	wprintf(L"��ʾ����Ԫ�أ�\n");
	int* pArray;
	SafeArrayAccessData(pIntArray, (void HUGEP**)&pArray);
	for(int i = 0; i < pIntArray->rgsabound->cElements; i++)
	{
		wprintf(L"Ԫ��[%d]=%d", i, pArray[i]);
		if(i < pIntArray->rgsabound->cElements -1)
		{
			wprintf(L", ");
		}
	}
	wprintf(L"\n");
	SafeArrayUnaccessData(pIntArray);
}