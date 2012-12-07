// �����#ifdef�����ʹ�ú궨��ķ�ʽ�����˵�����̬���ӿ⺯�����������ö�̬���ӿ�������
// ���ļ����ᰴ�ձ�����ѡ����ָ����NATIVELIB_EXPORTS���Ž��б��롣
#ifdef NATIVELIB_EXPORTS
#define NATIVELIB_API __declspec(dllexport)
#else
#define NATIVELIB_API __declspec(dllimport)
#endif

// ʾ��4.1
// Managed C++�е�ƽ̨���ú�C++ Interop
extern "C" NATIVELIB_API int __cdecl MultiplyTwoNumbers(int numOne, int numTwo);
extern "C" NATIVELIB_API bool __cdecl ReverseString(const wchar_t* rawString, wchar_t* stringBuffer, int bufferSize);

// ʾ��4.2
class NATIVELIB_API CUnmanagedClass
{
public:
	CUnmanagedClass();
	virtual ~CUnmanagedClass();
	void PassInt(int intValue);
	void PassString(char* strValue);
	char* ReturnString();
};

// ʾ��4.6
// ��������ʱ�쳣
extern "C" NATIVELIB_API void __cdecl CauseUnmanagedExceptions(int type);
// ��throw�ؼ�����ʽ�׳��쳣
extern "C" NATIVELIB_API void __cdecl ThrowUnmanagedExceptions(int type);

// �Զ���C++�쳣�࣬��ThrowUnmanagedExceptionsʹ��
class CUnmanagedException
{
public:
	CUnmanagedException(void) {}
	~CUnmanagedException(void)
	{
	    delete [] m_Message;
	}

	CUnmanagedException(const wchar_t* message, int errorCode)
	{
	    m_ErrorCode = errorCode;
		size_t bufLength = wcslen(message) + 1;
		m_Message   = new wchar_t[bufLength];
		wcscpy_s(m_Message, bufLength, message);
	}

	const wchar_t* GetErrorMessage()
	{
		return (const wchar_t*)m_Message;
	}

	int GetErrorCode()
	{
		return m_ErrorCode;
	}

private:
	wchar_t* m_Message;
	int   m_ErrorCode;
};