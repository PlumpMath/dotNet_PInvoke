#pragma once

// ����_com_ptr_t����ָ��
#import "SampleCOMSimple.tlb" no_namespace

using namespace System;
using namespace System::Runtime::InteropServices;

namespace MCppCOMWrapperLib
{
	// �����й�C++��װ��
	public ref class MCppCOMWrapper
	{
	public:
		String^ ProcessString(String^ managedString)
		{
			String^ result	= String::Empty;
			// ���һ����COM���������
			ICOMMTAObjPtr comObj(__uuidof(COMMTAObj));
			if (!comObj)
			{
				return L"����COM����ʧ��";
			}

			// ��System.String������ͳ�BSTR
			IntPtr pBSTR = Marshal::StringToBSTR(managedString);
			BSTR inBSTR = static_cast<BSTR>(pBSTR.ToPointer());
			
			// ����COM���󷽷�
			_bstr_t pResultBSTR = comObj->ProcessString(inBSTR);
			// �ͷŷ��͵����й��ڴ��BSTR�ַ���
			Marshal::FreeBSTR(pBSTR);
			// Ϊ���ؽ������System.Stringʵ����������
			result = gcnew System::String((wchar_t*)pResultBSTR);
			return result;
		}
	};
}
