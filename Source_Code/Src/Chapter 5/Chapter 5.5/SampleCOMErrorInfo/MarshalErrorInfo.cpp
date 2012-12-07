// MarshalErrorInfo.cpp : Implementation of CMarshalErrorInfo

#include "stdafx.h"
#include "MarshalErrorInfo.h"


// CMarshalErrorInfo

STDMETHODIMP CMarshalErrorInfo::InterfaceSupportsErrorInfo(REFIID riid)
{
	static const IID* arr[] = 
	{
		&IID_IMarshalErrorInfo
	};

	for (int i=0; i < sizeof(arr) / sizeof(arr[0]); i++)
	{
		if (InlineIsEqualGUID(*arr[i],riid))
			return S_OK;
	}
	return S_FALSE;
}

STDMETHODIMP CMarshalErrorInfo::GenerateCOMError(int type)
{
	HRESULT hr = S_OK;
	switch(type)
	{
	case 1:
		hr = E_POINTER;
		break;
	case 2:
		hr = E_NOTIMPL;
		break;
	case 3:
		hr = E_ACCESSDENIED;
		break;
	case 4:
		hr = E_OUTOFMEMORY;
		break;
	case 5:
		hr = CO_E_CLRNOTAVAILABLE;
		// ���case 6���ظ��йܴ�����쳣�����ǿ��ԣ�
		// 1. �Զ�������ţ�messege��
		// 2. ������ȷ�Ĵ��������λ�á���source��
		GenerateErrorInfo(_uuidof(IMarshalErrorInfo),
			"CMarshalErrorInfo::GenerateCOMError()",
			"����Ϊ5ʱ�����Ĵ���: CO_E_CLRNOTAVAILABLE");
		break;
	case 6:
		hr = CO_E_THREADPOOL_CONFIG;
		break;
	case 7:
		hr = 0x80040310L;
		GenerateErrorInfo(_uuidof(IMarshalErrorInfo),
			"CMarshalErrorInfo::GenerateCOMError()",
			"����Ϊ7ʱ�����Ĵ���");
		break;
	default:
		break;
	}

	return hr;

}

void CMarshalErrorInfo::GenerateErrorInfo(REFGUID rGUID, 
										  _bstr_t source, _bstr_t message)
{
	ICreateErrorInfo* pCreateInfo		= NULL;
	IErrorInfo* pErrorInfo				= NULL;
	// ����ICreateErrorInfo����
	CreateErrorInfo(&pCreateInfo);

	// ��������ֵ
	pCreateInfo->SetGUID(rGUID);
	pCreateInfo->SetSource(source);
	pCreateInfo->SetDescription(message);

	// ���pCreateInfo��IErrorInfo�ӿ�����
	pCreateInfo->QueryInterface(IID_IErrorInfo, (void**)&pErrorInfo);

	// ����IErrorInfo
	SetErrorInfo(0,pErrorInfo);

	// �ͷŶ���
	pErrorInfo->Release();
	pCreateInfo->Release();
}

