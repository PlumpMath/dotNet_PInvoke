#include "StdAfx.h"
#include "NameEntityFinder.h"

CNameEntityFinder::CNameEntityFinder(NameEntityType type)
{
	_type = type;
	wprintf(L"*Native*: NameEntity finder object is created.\n\n");
}

CNameEntityFinder::~CNameEntityFinder(void)
{
	wprintf(L"\n*Native*: NameEntity finder object is deleted.\n\n");
}

bool CNameEntityFinder::Initialize(const wchar_t* resourcePath)
{
	// ��ʼ������
	wprintf(L"\n*Native*: NameEntity finder object is initialized.\n\n");
	return true;
}

void CNameEntityFinder::UnInitialize()
{
	// �ͷŶ���ģ����Դ
	wprintf(L"\n*Native*: NameEntity finder object is uninitialized.\n\n");
}

bool CNameEntityFinder::FindNames(const wchar_t* text, PNAMEENTITY* ppNameEntity, UINT* nameCount)
{
	wprintf(L"\n*Native*: NameEntity finder object is processing text.\n\n");
	// ���ú����㷨������������֣������ؽ��
	// �������ǽ�����ʾ��������
	*nameCount = PopulateResult(text, ppNameEntity);
	return true;
}

int CNameEntityFinder::PopulateResult(const wchar_t* text, PNAMEENTITY* ppNameEntity)
{
	int nameCount = 5;
	*ppNameEntity = (PNAMEENTITY)CoTaskMemAlloc(nameCount * sizeof(NAMEENTITY));
	memset(*ppNameEntity, 0, nameCount * sizeof(NAMEENTITY));
	PNAMEENTITY pCurNameEntity = *ppNameEntity;

	AddNameEntity(L"����", 17, PlaceName, 0.8, pCurNameEntity);
	pCurNameEntity++;
	AddNameEntity(L"΢�������о�Ժ", 21, OrganizationName, 0.9, pCurNameEntity);
	pCurNameEntity++;
	AddNameEntity(L"��С��", 31, PersonName, 0.8, pCurNameEntity);
	pCurNameEntity++;
	AddNameEntity(L"����", 37, PersonName, 0.78, pCurNameEntity);
	pCurNameEntity++;
	AddNameEntity(L"������", 42, PersonName, 0.78, pCurNameEntity);

	return nameCount;
}


void CNameEntityFinder::AddNameEntity(const wchar_t* name, int beginPosition, NameEntityType type, double score, PNAMEENTITY pEntity)
{
	if(NULL != pEntity)
	{
		pEntity->_highlight_length = (int)wcslen(name);
		pEntity->_name = (WCHAR*)CoTaskMemAlloc((pEntity->_highlight_length + 1) * sizeof(WCHAR));
		wcscpy_s(pEntity->_name, (pEntity->_highlight_length + 1), name);
		pEntity->_highlight_begin = beginPosition;
		pEntity->_type = type;
		pEntity->_score = score;
	}
}


