#include "stdafx.h"
#include <conio.h>
#include <objbase.h>
#include <atlbase.h>
#import "mscorlib.tlb"
#import "MarshalEvent.tlb" no_namespace named_guids

CComModule _Module;

// ����Sink�ӿ���
// ����ʹ���˱����ؼ��֡�interface��, ������ʹ�á�class���ؼ��ָ�ֱ��
// ��Ȼʹ��class����Գ�������κ�Ӱ��
interface CSink:public IDispEventImpl<0, CSink, &DIID_IStockPriceBeeper, &LIBID_MarshalEvent, 1, 0>
{
public:

// ����ĵ����������Ƕ������й�COM�������е�[DispId(17)]��16���Ʊ�ʾ
BEGIN_SINK_MAP(CSink)
	SINK_ENTRY_EX(0, DIID_IStockPriceBeeper, 0x11, OnPriceChanged)
END_SINK_MAP()

// ����ʹ�á�HRESULT __stdcall������
	HRESULT __stdcall OnPriceChanged(double newPrice, double oldPrice)
	{
		wprintf(L"�¹ɼۣ�%.2f��ǰһ�����չɼۣ�%.2f\n", newPrice, oldPrice);
		return S_OK;
	}
};

void TestEvent()
{
	// ��ʼ��ATL
	_Module.Init(NULL, GetModuleHandle(NULL));

	// ����COM����
	IStockPriceControlPtr comObj(__uuidof(StockPriceControlObj));
	if (comObj)
	{
		CSink sink;
		// �ҽ��¼�
		sink.DispEventAdvise(comObj);

		// �����¼�
		comObj->ChangeStockPrice(18);
		comObj->ChangeStockPrice(38);
		comObj->ChangeStockPrice(28);
		comObj->ChangeStockPrice(58);

		// ȡ���¼�����
		sink.DispEventUnadvise(comObj);

		// �ͷ�COM����
		comObj.Release();
	}

	// ��ֹATL
	_Module.Term();
	
}
