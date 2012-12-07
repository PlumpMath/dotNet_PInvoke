// CppInterop_MarshalString.cpp : main project file.

#include "stdafx.h"
#include <stdio.h>
#include <string.h>
#include <vcclr.h>

using namespace System;
using namespace System::Runtime::InteropServices;

#pragma unmanaged
class CMarshalString
{
public:
	static char* CMarshalString::ReverseStringAnsi(const char *str)
	{
		size_t length = strlen(str);
		char* newBuffer = new char[length+1];
		strcpy_s(newBuffer, length+1, str);
		return _strrev(newBuffer);
	}

	static wchar_t* CMarshalString::ReverseStringUnicode(const wchar_t *str)
	{
		size_t length = wcslen(str);
		wchar_t* newBuffer = new wchar_t[length+1];
		wcscpy_s(newBuffer, length+1, str);
		return _wcsrev(newBuffer);
	}
};
#pragma managed

public ref class MarshalStringWrapper
{
public:
	String^ MarshalStringAnsi(String^ inputString)
	{

		// ���й��ַ�������Ϊ���й�Ansi�ַ���
		IntPtr pStrPtr
			= Marshal::StringToHGlobalAnsi(inputString);
		const char* pStr 
			= static_cast<char*>(pStrPtr.ToPointer());
		// ��ָ�봫�ݸ����йܷ���
		char* pNewStr 
			= CMarshalString::ReverseStringAnsi(pStr);
		// ���ݷ���ֵ�����й��ַ���
		String^ retString = gcnew String(pNewStr);
		// �ͷ���Ϊ���йܺ������ص��ַ����ڴ�
		delete [] pNewStr;
		// �ͷŷ��͵����й��ڴ��е�Ansi�ַ���
		Marshal::FreeHGlobal(pStrPtr);

		return retString;
	}

	String^ MarshalStringUnicode(String^ inputString)
	{
		// ��PtrToStringChars���ָ���й��ַ������ڲ�ָ��
		// pin_ptr���������ڴ棬���ⱻ�����������ͷ�
		pin_ptr<const wchar_t> pStr 
			= PtrToStringChars(inputString);
		// ��ָ�봫�ݸ����йܷ���
		wchar_t* pNewStr 
			= CMarshalString::ReverseStringUnicode(pStr);
		// ���ݷ���ֵ�����й��ַ���
		String^ retString = gcnew String(pNewStr);
		// �ͷ���Ϊ���йܺ������ص��ַ����ڴ�
		delete [] pNewStr;

		return retString;
	}
};

int main(array<System::String ^> ^args)
{
	MarshalStringWrapper^ wrapperObj = gcnew MarshalStringWrapper();
	String^ inputString;
	String^ resultString;

	// ����Unicode�汾
	inputString = "Test Unicode";
	Console::WriteLine("\nԭʼ�ַ�����{0}", inputString);
	resultString = wrapperObj->MarshalStringUnicode(inputString);
	Console::WriteLine("����ַ�����{0}", resultString);

	// ����Ansi�汾
	inputString = "Test Ansi";
	Console::WriteLine("\nԭʼ�ַ�����{0}", inputString);
	resultString = wrapperObj->MarshalStringAnsi(inputString);
	Console::WriteLine("����ַ�����{0}", resultString);

	Console::WriteLine("\r\n��������˳�...");
	Console::Read();

    return 0;
}
