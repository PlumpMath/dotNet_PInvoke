#pragma once

using namespace System;

namespace CppInteropWrapper
{

	// �й��쳣�࣬���ڷ�װ�ɷ��йܴ���������쳣��Ϣ
	public ref class WrapperException 
		: public ApplicationException
	{
	public:
		WrapperException(String^ message, int errorCode) 
			: ApplicationException(message)
		{
			m_ErrorCode = errorCode;
		};

		property int ErrorCode
		{
			int get() { return m_ErrorCode; };
		}
	private:
		int m_ErrorCode; 
	};

	// ������й��쳣��ö������
	public enum class UnmanagedExceptionType
	{
		DivedeByZero,
		AccessInvalidMemory,
		ThrowErrorCode,
		ThrowCustomException,
		ThrowStdException
	};

	// �������ThrowUnmanagedExceptions�����İ�װ��
	public ref class ThrowUnmanagedExceptionWrapperClass
	{
	public:
		static void CallUnmanagedException(UnmanagedExceptionType type);
	};

}