// CppInterop_MarshalCallback.cpp : main project file.

#include "stdafx.h"
#include "CallBack_Delegate.h"

using namespace System;

int main(array<System::String ^> ^args)
{
    CallBack_Delegate^ obj = gcnew CallBack_Delegate();
	obj->TestCallback();

	Console::WriteLine("\r\n��������˳�...");
	Console::Read();

	return 0;
}
