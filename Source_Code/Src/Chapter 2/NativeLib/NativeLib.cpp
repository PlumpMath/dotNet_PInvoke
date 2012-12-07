// NativeLib.cpp : Defines the entry point for the DLL application.
//

#include "stdafx.h"
#include "NativeLib.h"
#include "objbase.h"    //CoTaskMemAlloc
#include <stdio.h>      //sprintf
#include <locale.h>

// �ַ���ʾ��
void __cdecl TestStringArgumentsFixLength(
	const wchar_t* inString, wchar_t* outString, int bufferSize)
{
	if(NULL != inString)
	{
		wcscpy_s(outString, bufferSize, inString);
	}
}
//******************************************************************

void __cdecl TestStringArgumentOut(int id, wchar_t** ppString)
{
	if(NULL != ppString)
	{
		int bufferSize = 128;
		*ppString = (wchar_t*)CoTaskMemAlloc(bufferSize);
		swprintf_s(*ppString, bufferSize/sizeof(wchar_t), L"Out string of ID: %d", id);
	}
}
//******************************************************************

void __cdecl TestStringMarshalArguments(const char* inAnsiString, const wchar_t* inUnicodeString, wchar_t* outUnicodeString, int outBufferSize)
{
	size_t ansiStrLength = strlen(inAnsiString);
	size_t uniStrLength = wcslen(inUnicodeString);

	size_t totalSize = ansiStrLength + uniStrLength + 2;
	wchar_t* tempBuffer = new(std::nothrow) wchar_t[totalSize];
	if(NULL == tempBuffer)
	{
		return;
	}

	wmemset(tempBuffer, 0, totalSize);
	mbstowcs(tempBuffer, inAnsiString, totalSize);
	wcscat_s(tempBuffer, totalSize, L" ");
	wcscat_s(tempBuffer, totalSize, inUnicodeString);
	wcscpy_s(outUnicodeString, outBufferSize, tempBuffer);
	delete[] tempBuffer;
}

wchar_t* __cdecl TestStringAsResult(int id)
{
	int bufferSize = 64;
	wchar_t* result = (wchar_t*)CoTaskMemAlloc(bufferSize);
	swprintf_s(result, bufferSize/sizeof(wchar_t), L"Result of ID: %d", id);
	return result;
}
//******************************************************************

BSTR __cdecl TestBSTRString(BSTR* ppString)
{
	*ppString = NULL;
	*ppString = SysAllocString(L"BSTR string from argument");
	
	BSTR result = SysAllocString(L"Return BSTR string");
	return result;
}
//******************************************************************

// �ṹ��ʾ��
void __cdecl TestStructArgumentByVal(SIMPLESTRUCT simpleStruct)
{
	ShowNativeStructSize(sizeof(SIMPLESTRUCT));
	// ��ӡ�ṹ������
	wprintf(L"\n�ṹ��ԭ����: int = %d, short = %d, float = %f, double = %f\n", 
		simpleStruct.intValue, simpleStruct.shortValue, simpleStruct.floatValue, simpleStruct.doubleValue);

	simpleStruct.intValue += 10;
}

void __cdecl TestStructArgumentByRef(PSIMPLESTRUCT pStruct)
{
	ShowNativeStructSize(sizeof(SIMPLESTRUCT));

	if( NULL != pStruct)
	{
		// ��ӡ��ʼ����
		wprintf(L"\n�ṹ��ԭ����: int = %d, short = %d, float = %f, double = %f\n", 
			pStruct->intValue, pStruct->shortValue, pStruct->floatValue, pStruct->doubleValue);
		
		// �޸�����
		pStruct->intValue++;
		pStruct->shortValue++;
		pStruct->floatValue += 1;
		pStruct->doubleValue += 1;
	}
}
//******************************************************************

PSIMPLESTRUCT __cdecl TestReturnStruct(void)
{
	// ʹ��CoTaskMemAlloc�����ڴ�
	PSIMPLESTRUCT pStruct = (PSIMPLESTRUCT)CoTaskMemAlloc(
		sizeof(SIMPLESTRUCT));

	pStruct->intValue = 5;
	pStruct->shortValue = 4;
	pStruct->floatValue = 3.0;
	pStruct->doubleValue = 2.1;

	return pStruct;
}


//******************************************************************

void __cdecl TestReturnStructFromArg(PSIMPLESTRUCT* ppStruct)
{
	if( NULL != ppStruct)
	{
		*ppStruct = (PSIMPLESTRUCT)CoTaskMemAlloc(
			sizeof(SIMPLESTRUCT));

		(*ppStruct)->intValue = 5;
		(*ppStruct)->shortValue = 4;
		(*ppStruct)->floatValue = 3.0;
		(*ppStruct)->doubleValue = 2.1;
	}
	return;
}

PSIMPLESTRUCT __cdecl TestReturnNewStruct(void)
{
	// ʹ��new�����ڴ�
	PSIMPLESTRUCT pStruct = new SIMPLESTRUCT();

	pStruct->intValue = 5;
	pStruct->shortValue = 4;
	pStruct->floatValue = 3.0;
	pStruct->doubleValue = 2.1;

	return pStruct;
}
//******************************************************************

void __cdecl FreeStruct(PSIMPLESTRUCT pStruct)
{
	if(NULL != pStruct)
	{
		delete pStruct;
		pStruct = NULL;
	}
}
//******************************************************************

void __cdecl GetEmployeeInfo(PMSEMPLOYEE pEmployee)
{
	if(NULL != pEmployee)
	{
		// ����Ӧ�ø���pEmployee->employeeID����д��Ϣ����
		pEmployee->employedYear = 2;
		pEmployee->alias = (char*)CoTaskMemAlloc(255);
		pEmployee->displayName = (char*)CoTaskMemAlloc(255);

		strcpy_s(pEmployee->alias, 255, "xcui");
		strcpy_s(pEmployee->displayName, 255, "Xiaoyuan Cui");
	}
}
//******************************************************************

void __cdecl GetEmployeeInfo2(PMSEMPLOYEE2 pEmployee)
{
	if(NULL != pEmployee)
	{
		// ����Ӧ�ø���pEmployee->employeeID����д��Ϣ����
		pEmployee->employedYear = 2;
		strcpy_s(pEmployee->alias, 255, "jizhou");
		strcpy_s(pEmployee->displayName, 255, "Jizhou Huang");
	}
}
//******************************************************************

void __cdecl GetEmployeeInfoEx(PMSEMPLOYEE_EX pEmployee)
{
	ShowNativeStructSize(sizeof(MSEMPLOYEE_EX));

	if(NULL != pEmployee)
	{
		// ����Ӧ�ø���pEmployee->employeeID����д��Ϣ����
		pEmployee->alias = (char*)CoTaskMemAlloc(255);
		pEmployee->displayName = (wchar_t*)CoTaskMemAlloc(255 * 2);

		strcpy_s(pEmployee->alias, 255, "xcui");
		wcscpy_s(pEmployee->displayName, 255, L"����Դ");

		pEmployee->isInRedmond = false;
		pEmployee->employedYear = 2;
		pEmployee->isFamale = false;
	}
}
//******************************************************************

void __stdcall PrintEmployeeInfoEx(MSEMPLOYEE_EX pEmployee)
{
	// ֧�������ַ���ʾ
	_wsetlocale(LC_ALL, L"chs");

	ShowNativeStructSize(sizeof(MSEMPLOYEE_EX));

	wprintf(L"Ա����Ϣ��\n");
	wprintf(L"ID��%d\n", pEmployee.employeeID);
	printf("Alias��%s\n", pEmployee.alias);
	wprintf(L"������%s\n", pEmployee.displayName);
	wprintf(L"�Ա�%d\n", pEmployee.isFamale);
	wprintf(L"���䣺%d\n", pEmployee.employedYear);
	wprintf(L"�Ƿ����ܲ���%d\n", pEmployee.isInRedmond);
	
}
//******************************************************************

void __cdecl GetEmployeeInfoEx2(PMSEMPLOYEE_EX2 pEmployee)
{
	// ֧�������ַ���ʾ
	_wsetlocale(LC_ALL, L"chs");

	ShowNativeStructSize(sizeof(MSEMPLOYEE_EX2));

	if(NULL != pEmployee)
	{
		// ����Ӧ�ø���pEmployee->EmployeeID����д��Ϣ����
		pEmployee->EmployedYear = 2;
		pEmployee->CurrentLevel = 60;
		pEmployee->RegionId = 1100;

		// ����ʾԭ�����������룬Ȼ���ڸ���
		wprintf(L"ԭ�������룺%d\n", pEmployee->ZipCode);
		pEmployee->ZipCode = 100080;
		pEmployee->CurrentSalary = 200000;
	}

}
//******************************************************************

void __cdecl TestStructInStructByVal(PPERSON2 pPerson)
{
	// ֧�������ַ���ʾ
	setlocale(LC_ALL, "chs");
	printf("�� = %s\n�� = %s\n���� = %i\n\n", 
		pPerson->name.last, pPerson->name.first, pPerson->age);
}
//******************************************************************

void __cdecl TestStructInStructByRef(PPERSON pPerson)
{
	size_t firstLen = strlen(pPerson->pName->first);
	size_t lastLen = strlen(pPerson->pName->last);

	char* temp = (char*)CoTaskMemAlloc(sizeof(char) * (firstLen + lastLen + 2));
	sprintf_s(temp, firstLen + lastLen + 2, "%s %s", pPerson->pName->last, pPerson->pName->first);

	CoTaskMemFree(pPerson->pName->displayName);
	pPerson->pName->displayName = temp;
}
//******************************************************************

void __cdecl TestUnion(SIMPLEUNION u, int type)
{
	// ֧�������ַ���ʾ
	_wsetlocale(LC_ALL, L"chs");
	switch(type)
	{
	case 1:
		printf("\n������-integer: %i\n", u.i);
		break;
	case 2:
		printf("\n������-double: %f\n", u.d);
		break;
	default:
		printf("type������Χ����\n");
		break;
	}
}
//******************************************************************

void __cdecl TestUnion2(SIMPLEUNION2 u, int type)
{
	// ֧�������ַ���ʾ
	_wsetlocale(LC_ALL, L"chs");
	switch(type)
	{
	case 1:
		printf("\n������-integer: %i\n", u.i);
		break;
	case 2:
		printf("\n������-string: %s\n", u.str);
		break;
	default:
		printf("type������Χ����\n");
		break;
	}
}
//******************************************************************

void __cdecl TestPointer2Pointer(PMSEMPLOYEE* ppStruct)
{
	if(NULL != ppStruct)
	{
		(*ppStruct)->employedYear = 2;
		(*ppStruct)->displayName = (char*)CoTaskMemAlloc(255);
		(*ppStruct)->alias = (char*)CoTaskMemAlloc(255);

		strcpy_s((*ppStruct)->alias, 255, "xcui");
		strcpy_s((*ppStruct)->displayName, 255, "Xiaoyuan Cui");
	}
}
//******************************************************************

// �����ַ������У������ַ�������
UINT __cdecl TestArrayOfChar(char charArray[], int arraySize)
{
	int result = 0;
	for(int i = 0; i < arraySize; i++)
	{
	    if (isdigit(charArray[i]))
	    {
		    result++;
			charArray[i] = '@';
		}
	}
	return result;
}
//******************************************************************

// �������������У�����Ԫ��֮�͡�����ÿ��Ԫ�ص�ֵ��10
int __cdecl TestArrayOfInt(int intArray[], int arraySize)
{
	int result = 0;
	for(int i = 0; i < arraySize; i++)
	{
		result += intArray[i];
		intArray[i] += 10;
	}
	return result;
}
//******************************************************************

void __cdecl TestArrayOfString(char* ppStrArray[], int size)
{
	for(int i = 0; i < size; i++)
	{
		// ��ת�ַ���
		ReverseAnsiStringInPlace(ppStrArray[i]);
	}
}
//******************************************************************

int __cdecl TestRefArrayOfString(void** strArray, int* size)
{
	int const elementCount = 10;
	int bytesForPointerArray = sizeof(wchar_t*) * elementCount;

	// Ϊ�ַ���ָ����������ڴ�
	wchar_t** pArray = (wchar_t**)CoTaskMemAlloc(bytesForPointerArray);
    // Ϊÿ���ַ���ָ������ڴ�
	for(int i = 0; i < elementCount; i++)
	{
		pArray[i] = (wchar_t*)CoTaskMemAlloc(255 * sizeof(wchar_t));
		
		swprintf_s(pArray[i], 255, L"�ַ��� %i", i);
	}
    
	*strArray = pArray;
	*size = elementCount;
    
	return elementCount;
}
//******************************************************************

// �ۺ�ʾ��
NAMEFINDERHANDLE CreateNameFinderInstance(__in NameEntityType type)
{
	CNameEntityFinder* finderObject = new(std::nothrow) CNameEntityFinder(type);
	return reinterpret_cast<NAMEFINDERHANDLE>(finderObject);
}

bool Initialize(__in NAMEFINDERHANDLE hHandle, __in_z const wchar_t* resourcePath)
{
	CNameEntityFinder* finderObject = reinterpret_cast<CNameEntityFinder*>(hHandle);
	if(NULL != finderObject)
	{
		return finderObject->Initialize(resourcePath);
	}
	else
	{
		return false;
	}
}

bool FindNames(__in NAMEFINDERHANDLE hHandle,
		__in_z const wchar_t* text, 
		__out PNAMEENTITY* nameArray,
		__out UINT* arraySize)
{
	CNameEntityFinder* finderObject = reinterpret_cast<CNameEntityFinder*>(hHandle);
	if(NULL != finderObject)
	{
		return finderObject->FindNames(text, nameArray, arraySize);
	}
	else
	{
		return false;
	}
}

void UnInitialize(__in NAMEFINDERHANDLE hHandle)
{
	CNameEntityFinder* finderObject = reinterpret_cast<CNameEntityFinder*>(hHandle);
	if(NULL != finderObject)
	{
		finderObject->UnInitialize();
	}
}

void DestroyInstance(__in NAMEFINDERHANDLE hHandle)
{
	CNameEntityFinder* finderObject = reinterpret_cast<CNameEntityFinder*>(hHandle);
	if(NULL != finderObject)
	{
		delete finderObject;
		finderObject = NULL;
	}
}
//******************************************************************

// ��������
void ShowNativeStructSize(size_t size)
{
	// ֧�������ַ���ʾ
	_wsetlocale(LC_ALL, L"chs");

	wprintf(L"���йܴ��붨��Ľṹ���СΪ(%d)�ֽڡ�\n", size);
}

void ReverseAnsiStringInPlace(char* rawString)
{
	int strLength = (int)strlen(rawString);

	char tempChar;
	for(int i = 0; i < strLength/2; i++)
	{
		tempChar = rawString[i];
		rawString[i] = rawString[strLength - 1 - i];
		rawString[strLength - 1 - i] = tempChar;
	}
}
