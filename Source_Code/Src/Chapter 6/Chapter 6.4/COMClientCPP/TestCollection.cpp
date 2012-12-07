#include "stdafx.h"
#include <conio.h>
#include <objbase.h>
#include <atlbase.h>
#import "mscorlib.tlb"
#import "MarshalCollection.tlb" no_namespace named_guids

void TestCollection()
{
	// ����COM����ʵ��
	IMarshalCollectionPtr comObj(__uuidof(MarshalCollectionObj));
	if (comObj)
	{
		// ������һ������
		IDemoItemPtr value1(__uuidof(DemoItemObj));
		value1->IntValue = 1;
		value1->StringValue = L"�Ƽ���";

		VARIANT* var1 = new VARIANT();
		// �������ﴫ�ݵ��йܴ����еĶ�����COM�ӿڶ���
		// ���Ҫ����ΪVT_UNKNOWN����VT_DISPATCH
		// �����н�ʹ��VT_DISPATCH�������ö�Ӧ��pdispVal�ֶδ������
		// ���ʹ��VT_UNKNOWN����Ӧ�������Ӧ��punkVal�ֶδ������
		var1->vt = VT_DISPATCH;
		var1->pdispVal = value1;

		// �����ڶ�������
		IDemoItemPtr value2(__uuidof(DemoItemObj));
		value2->IntValue = 2;
		value2->StringValue = L"����Դ";

		VARIANT* var2 = new VARIANT();
		var2->vt = VT_DISPATCH;
		var2->pdispVal = value2;

		// ���뼯��
		comObj->Add(_variant_t(var1));
		comObj->Add(_variant_t(var2));
		
		// ö�ټ����е�Ԫ��
		IEnumVARIANTPtr pEnum = comObj->GetEnumerator();
		VARIANT item;
		// ÿ��ȡ��1�����
		ULONG expectedItemCount = 1;
		ULONG returnedItemCount;
		while(S_OK == pEnum->Next(expectedItemCount, &item, &returnedItemCount))
		{
			if(expectedItemCount == returnedItemCount)	
			{
				IDemoItemPtr pItem;
				HRESULT hr = item.pdispVal->QueryInterface(IID_IDemoItem, (LPVOID*)&pItem);
				wprintf(L"����%d ������%s\n", pItem->IntValue, (wchar_t*)pItem->StringValue);
			}
		}

		// �ͷŶ���
		value1.Release();
		delete var1;

		value2.Release();
		delete var2;

		comObj.Release();
	}
}