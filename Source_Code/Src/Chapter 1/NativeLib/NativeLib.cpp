// NativeLib.cpp
//

#include "stdafx.h"
#include "NativeLib.h"

#include <stdio.h>
#include <windows.h>
#include <stdlib.h>
#include <exception>
#include "objbase.h"    //CoTaskMemAlloc
#include <ctype.h>  //__isascii ��iswascii

/******1.2С������****************/
void __stdcall PrintMsg(const char* msg)
{
	printf("%s\n", msg);
	return;
}

/******1.4С������****************/
int __stdcall Multiply(int factorA, int factorB)
{
    return factorA * factorB;
}

/******1.6С������****************/
void __stdcall PrintMsgByFlag(void* msgData, int flag)
{
    if (flag == 1) //integer
    {
        int* pID = (int*)msgData;
		printf("Your ID is: %i\n", *pID);
    }
	else if(flag == 2) // char
	{
		char* pName = (char*)msgData;
		printf("Your name is: %s\n", pName);
	}
}

/******1.7С������****************/
void ReverseAnsiString(char* rawString, char* reversedString)
{
	int strLength = (int)strlen(rawString);

	strcpy(reversedString, rawString);

	char tempChar;
	for(int i = 0; i < strLength/2; i++)
	{
		tempChar = reversedString[i];
		reversedString[i] = reversedString[strLength - 1 - i];
		reversedString[strLength - 1 - i] = tempChar;
	}
}

void ReverseUnicodeString(wchar_t* rawString, wchar_t* reversedString)
{
	int strLength = (int)wcslen(rawString);

	wcscpy(reversedString, rawString);

	wchar_t tempChar;
	for(int i = 0; i < strLength/2; i++)
	{
		tempChar = reversedString[i];
		reversedString[i] = reversedString[strLength - 1 - i];
		reversedString[strLength - 1 - i] = tempChar;
	}
}


void __cdecl ReverseStringA(char* rawString, char* reversedString)
{
	ReverseAnsiString(rawString, reversedString);
}

void __cdecl ReverseStringW(wchar_t* rawString, wchar_t* reversedString)
{
	ReverseUnicodeString(rawString, reversedString);
}

/******1.8С������****************/
int __stdcall Divide(int numerator, int denominator)
{
    return numerator / denominator;
}

void UnmanagedExceptionFromCpp()
{
	throw std::exception("Exception from C++");
}

void UnmanagedExcetionViaRaiseException()
{
	//	void WINAPI RaiseException(
	//  __in  DWORD dwExceptionCode,
	//  __in  DWORD dwExceptionFlags,
	//  __in  DWORD nNumberOfArguments,
	//  __in  const ULONG_PTR *lpArguments
	//);
	//http://msdn.microsoft.com/en-us/library/ms680552(VS.85).aspx
	//http://msdn.microsoft.com/en-us/library/ms679356(VS.85).aspx

	//�׳�һ���ṹ�����쳣��Ϣ
	RaiseException(EXCEPTION_ACCESS_VIOLATION, EXCEPTION_NONCONTINUABLE, 0, 0);
}

void UnmanagedExcetionViaRaiseExceptionNoRegular()
{
	//�׳�һ���ṹ�����쳣��Ϣ
	RaiseException(123, EXCEPTION_NONCONTINUABLE, 0, 0);
}

/******1.9С������****************/
wchar_t* GetStringMalloc()
{
	int iBufferSize = 128;
	wchar_t* pBuffer = (wchar_t*)malloc(iBufferSize);
	if(NULL != pBuffer)
	{
		wcscpy_s(pBuffer, iBufferSize/sizeof(wchar_t), L"String from MALLOC");
	}
	return pBuffer;
}

wchar_t* GetStringNew()
{
	int iBufferSize = 128;
	wchar_t* pBuffer = new wchar_t[iBufferSize];
	if(NULL != pBuffer)
	{
		wcscpy_s(pBuffer, iBufferSize/sizeof(wchar_t), L"String from NEW");
	}
	return pBuffer;
}

wchar_t* GetStringCoTaskMemAlloc()
{
	int iBufferSize = 128;
	wchar_t* pBuffer = (wchar_t*)CoTaskMemAlloc(iBufferSize);
	if(NULL != pBuffer)
	{
		wcscpy_s(pBuffer, iBufferSize/sizeof(wchar_t), L"String from CoTaskMemAlloc");
	}
	return pBuffer;
}

void FreeMallocMemory(void* pBuffer)
{
	if(NULL != pBuffer)
	{
		free(pBuffer);
		pBuffer = NULL;
	}
}

void FreeNewMemory(void* pBuffer)
{
	if(NULL != pBuffer)
	{
		delete pBuffer;
		pBuffer = NULL;
	}
}

void FreeCoTaskMemAllocMemory(void* pBuffer)
{
	if(NULL != pBuffer)
	{
		CoTaskMemFree(pBuffer);
		pBuffer = NULL;
	}
}

/******1.11С������****************/
//int __isascii(int c);
//int iswascii(wint_t c);

bool IsAsciiNonblittable(char asciiChar)
{
    return (__isascii(asciiChar) != 0);
}

int IsAsciiBlittable(__int8 asciiChar)
{
    return __isascii(asciiChar);
}

void UnmanagedTest()
{
    const int testCycle = 600000;
    bool result = false;
    for(int i = 0; i < testCycle; i++)
    {
		result = IsAsciiNonblittable('c');
    }
}

bool IsWasciiNonblittable(wchar_t wasciiChar)
{
    return (iswascii(wasciiChar) != 0);
}

//int IsWasciiBlittable(__int16 wasciiChar)
//{
//    return iswascii(wasciiChar);
//}
