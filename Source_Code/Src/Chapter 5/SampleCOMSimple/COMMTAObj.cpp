// COMMTAObj.cpp : Implementation of CCOMMTAObj

#include "stdafx.h"
#include "COMMTAObj.h"
#include "comutil.h"

// CCOMMTAObj


STDMETHODIMP CCOMMTAObj::AddIntegers(int num0, int num1, int* result)
{
	*result = num0 + num1;
	return S_OK;
}

STDMETHODIMP CCOMMTAObj::ProcessString(BSTR inString, BSTR* outString)
{
	// �޸�inString�ַ������������ַ�����ӡ�_Processed����׺
	_bstr_t buffer;
	buffer.Assign(inString);
	buffer += "_Processed";
	*outString = buffer.Detach();

	return S_OK;
}
