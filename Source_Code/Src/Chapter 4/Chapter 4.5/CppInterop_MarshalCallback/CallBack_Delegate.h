#pragma once

using namespace System::Runtime::InteropServices;

// �����й�ί��
[UnmanagedFunctionPointer(CallingConvention::Cdecl)]
public delegate int CallbackDelegate(int num0, int num1);

public ref class CallBack_Delegate
{
public:
	void TestCallback(void);
private:
	// �Իص���ʵ��
	int CallBack_Add(int num0, int num1);
};
