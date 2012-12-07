// ManagedCPPClient.cpp : main project file.

#include "stdafx.h"

using namespace System;
using namespace CppInteropWrapper;

int main(array<System::String ^> ^args)
{
    int resultNumber = 0;

    // ����Managed C++����ʵ��
	WrapperClass^ wrapperObj = gcnew WrapperClass();
	
    // ���ó˷�������P/Invoke)
	resultNumber = wrapperObj->MultiplyNumbers_PInvoke(2, 3);
    Console::WriteLine("�˷���P/Invoke����2x3 = {0}", resultNumber);

	// ���ó˷�������C++ Interop)
	resultNumber = wrapperObj->MultiplyNumbers_CppInterop(4, 5);
    Console::WriteLine("�˷���C++ Interop����4x5 = {0}", resultNumber);

	// ��ת�ַ�������(P/Invoke)
	wrapperObj->ReverseString_PInvoke();

	// ��ת�ַ�������(C++ Interop)
	wrapperObj->ReverseString_CppInterop();
    
	Console::WriteLine("\r\n��������˳�...");
	Console::Read();

	return 0;
}
