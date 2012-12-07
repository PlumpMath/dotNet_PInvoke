#pragma once

using namespace System;

// ��ǰ����
class CUnmanagedClass;

namespace CppInteropWrapper
{
	public ref class CMCppWrapper
	{
	public:
		CMCppWrapper();
		virtual ~CMCppWrapper();
		void PassInt(int intValue);
		void PassString(String^ strValue);
		String^ ReturnString();

	private:
		CUnmanagedClass* m_pUnmanagedObj;
	};
}