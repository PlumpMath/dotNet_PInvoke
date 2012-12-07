using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace RetWin32ErrorCode
{
    public class RetErrorCodeDemo
    {
        //Win32 API
        //DWORD WINAPI GetFileAttributes(
        //  __in  LPCTSTR lpFileName
        //);

        // �������һ�δ��󣬲�ʹ��Unicode�汾��Win32 ����
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern uint GetFileAttributes(string lpFileName);

        public static void TestErrorCode()
        {
            GetFileAttributes("FileNotFoundDemo.txt");

            // ������һ�λ�õĴ���
            int lastErrorCode = Marshal.GetLastWin32Error();
            Console.WriteLine("GetFileAttributes last win32 error code: {0}", lastErrorCode);
        }

        public static void TestFormatErrorMsg()
        {
            // ��ͼ���һ�������ڵ��ļ�������
            GetFileAttributes("FileNotFoundDemo.txt");

            // ͨ��Win32 API�е�FormatMessage����������һ�λ�õĴ�����Ϣ
            Console.WriteLine("GetFileAttributes last win32 error message: {0}", FormatErrorCode.GetLastErrorMsg());
        }

        public static void TestErrorMsgByWin32Exception()
        {

            // ��ͼ���һ�������ڵ��ļ�������
            GetFileAttributes("FileNotFoundDemo.txt");

            // ������һ�λ�õĴ���
            int lastErrorCode = Marshal.GetLastWin32Error();

            // ��Win32�Ĵ�����ת����һ���û��Ѻõ��й��쳣
            Win32Exception win32Exception = new Win32Exception(lastErrorCode);
            Console.WriteLine("GetFileAttributes last win32 error message: {0}", win32Exception.Message);
        }

        public static void TestErrorMsgByWin32ExceptionDefault()
        {
            // ��ͼ���һ�������ڵ��ļ�������
            GetFileAttributes("FileNotFoundDemo.txt");

            // ʹ��Win32��ȱʡ���캯����ʹ���Զ�����Marshal.GetLastWin32Error()��
            // ��Win32�Ĵ�����ת����һ���û��Ѻõ��й��쳣
            Win32Exception win32Exception = new Win32Exception();
            Console.WriteLine("GetFileAttributes last win32 error message: {0}", win32Exception.Message);
        }
    }

    public class WithoutSetLastErrorDemo
    {
        // �������������
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "GetFileAttributesW")]
        public static extern uint GetFileAttributesWithoutSetLastError(string lpFileName);

        public static void Test()
        {
            // ��ͼ���һ�������ڵ��ļ�������
            GetFileAttributesWithoutSetLastError("FileNotFoundDemo.txt");

            // ������һ�λ�õĴ�����Ϊû�н�SetLastError����Ϊtrue��
            // ����޷��ɹ������ȷ�Ĵ�����
            int lastErrorCode = Marshal.GetLastWin32Error();
            Console.WriteLine("GetFileAttributes last win32 error code: {0}", lastErrorCode);
        }
    }

    public class FormatErrorCode
    {
        const uint FORMAT_MESSAGE_ALLOCATE_BUFFER = 0x00000100;
        const uint FORMAT_MESSAGE_IGNORE_INSERTS = 0x00000200;
        const uint FORMAT_MESSAGE_FROM_SYSTEM = 0x00001000;

        //Win32 API
        //DWORD WINAPI FormatMessage(
        //  __in      DWORD dwFlags,
        //  __in_opt  LPCVOID lpSource,
        //  __in      DWORD dwMessageId,
        //  __in      DWORD dwLanguageId,
        //  __out     LPTSTR lpBuffer,
        //  __in      DWORD nSize,
        //  __in_opt  va_list* Arguments
        //);

        [DllImport("kernel32.dll")]
        public static extern uint FormatMessage(uint dwFlags, IntPtr lpSource,
            uint dwMessageId, uint dwLanguageId, ref IntPtr lpMsgBuf,
            uint nSize, IntPtr Arguments);

        //Win32 API
        //HLOCAL WINAPI LocalFree(
        //  __in  HLOCAL hMem
        //);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr LocalFree(IntPtr hMem);

        public static string GetLastErrorMsg()
        {
            int lastError = Marshal.GetLastWin32Error();

            IntPtr lpMsgBuf = IntPtr.Zero;

            uint dwChars = FormatMessage(
                FORMAT_MESSAGE_ALLOCATE_BUFFER | FORMAT_MESSAGE_FROM_SYSTEM
                | FORMAT_MESSAGE_IGNORE_INSERTS,
                IntPtr.Zero, (uint)lastError, 0, ref lpMsgBuf, 0, IntPtr.Zero);

            if (dwChars == 0)
            {
                return "";
            }

            string errorMsg = Marshal.PtrToStringAnsi(lpMsgBuf);
            // �ͷ��ڴ�
            LocalFree(lpMsgBuf);

            return errorMsg;
        }
    }

}
