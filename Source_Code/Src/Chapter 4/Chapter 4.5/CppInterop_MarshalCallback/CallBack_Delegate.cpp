#include "StdAfx.h"
#include "CallBack_Delegate.h"
#include "NativeCallback.h"

using namespace System;
using namespace System::Runtime::InteropServices;

void CallBack_Delegate::TestCallback(void)
{
	// �ó�Ա����CallBack_Addʵ����ί�ж���
	CallbackDelegate^ cbDelegate = gcnew CallbackDelegate(this, 
		&CallBack_Delegate::CallBack_Add);
	// GCHandle���Է�ֹί�ж��󱻻���
	GCHandle gchDelegate = GCHandle::Alloc(cbDelegate);
	// ����ί�ж���
	IntPtr pFunc 
		= Marshal::GetFunctionPointerForDelegate(cbDelegate);
	// ��ָ��ת��ΪPCallbackFunc��
	PCallbackFunc pcbFunc 
		= static_cast<PCallbackFunc>(pFunc.ToPointer());
	// ����ʵ������
	CCallbackClient client;
	client.UseCallback(pcbFunc, 1, 2);
	// �ͷ�GCHandle�������������������ͷ�ί�ж���
	gchDelegate.Free();
}

// �ûص�����ʵ�����������ļӺͼ���
int CallBack_Delegate::CallBack_Add(int num0, int num1)
{
	return num0 + num1;
}
