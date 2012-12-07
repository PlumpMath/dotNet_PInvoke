#include "StdAfx.h"
#include "WrapperClass.h"
#include "..\..\NativeLib\NativeLib.h"

namespace CppInteropWrapper
{

	// ͨ��P/Invoke���ú���MultiplyTwoNumbers
	int WrapperClass::MultiplyNumbers_PInvoke(int numA, int numB)
	{
		return PInvokeMultiplyNumbers(numA, numB);
	}

	// ͨ��c++ Interop���ú���MultiplyTwoNumbers
	int WrapperClass::MultiplyNumbers_CppInterop(int numA, int numB)
	{
		return MultiplyTwoNumbers(numA, numB);
	}

	// ͨ��P/Invoke���ú���ReverseString
	void WrapperClass::ReverseString_PInvoke()
	{
		// ����StringBuilder��Ϊ�ַ���������
		String^ rawString = L"String_PInvoke";
		StringBuilder^ buf  = gcnew StringBuilder(rawString->Length);
		int bufferSize = buf->Capacity + 1;
		
		// P/Invoke����ReverseString����
		PInvokeReverseString(rawString, buf, bufferSize);
		Console::WriteLine("��ת�ַ��������P/Invoke����{0} -> {1}", rawString, buf->ToString());
	}

	// ͨ��c++ Interop���ú���ReverseString
	void WrapperClass::ReverseString_CppInterop()
	{
		wchar_t* buf = new wchar_t[500];
		// c++ Interop����ReverseString����
		ReverseString(L"String_CppInterop", buf, 500);

		// ���ݷ���ֵ����System.String��ʵ��
		String^ result = gcnew String(buf);
		
		delete [] buf;
		Console::WriteLine("��ת�ַ��������C++ Interop����{0} -> {1}", "String_CppInterop", result);
	}

}
