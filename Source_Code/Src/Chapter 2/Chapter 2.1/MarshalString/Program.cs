using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace MarshalString
{
    class Program
    {
        private const string _dllName = "NativeLib.dll";

        // �ַ���ʾ����������
        // void __cdecl TestStringArgumentsInOut(const wchar_t* inString, wchar_t* outString, int bufferSize);
        [DllImport(_dllName, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        private extern static void TestStringArgumentsFixLength(string inString, StringBuilder outString, int bufferSize);

        // NATIVELIB_API void __cdecl TestStringArgumentOut(int id, wchar_t** ppString);
        [DllImport(_dllName, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl, EntryPoint = "TestStringArgumentOut")]
        private extern static void TestStringArgumentOutIntPtr(int id, ref IntPtr outString);
        
        [DllImport(_dllName, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        private extern static void TestStringArgumentOut(int id, ref string outString);

        // void __cdecl TestStringMarshalArguments(const char* inAnsiString, const wchar_t* inUnicodeString, wchar_t* outUnicodeString, int outBufferSize)
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        private extern static void TestStringMarshalArguments(
            [MarshalAs(UnmanagedType.LPStr)] string inAnsiString, 
            [MarshalAs(UnmanagedType.LPWStr)] string inUnicodeString,
            [MarshalAs(UnmanagedType.LPWStr)] StringBuilder outStringBuffer,
            int outBufferSize);
        
        // void wchar_t* __cdecl TestStringAsResult()
        [DllImport(_dllName, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl, EntryPoint = "TestStringAsResult")]
        private extern static IntPtr TestStringAsResultIntPtr(int id);

        [DllImport(_dllName, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        private extern static string TestStringAsResult(int id);

        // BSTR* __cdecl TestBSTRString(BSTR* ppString)
        [DllImport(_dllName, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        private extern static IntPtr TestBSTRString(out IntPtr bstrPtr);
        

        static void Main(string[] args)
        {
            TestMarshalArguments();

            TestStringArgumentsFixLength();

            TestArgumentsOut();

            TestStringResult();

            TestBSTR();

            Console.WriteLine("\r\n��������˳�...");
            Console.Read();
        }



        /// <summary>
        /// �����ַ������йܴ�������ڴ档
        /// </summary>
        private static void TestStringArgumentsFixLength()
        {
            string inString = "This is a input string.";

            int bufferSize = inString.Length;
            StringBuilder sb = new StringBuilder(bufferSize);

            // �����ʼ����һ������ΪN��StringBuilder������ʵ�Ϸ��Ͳ������ṩ��N+1��������
            // ���������һ���ַ��ռ������ڷ��й��ַ�����Ҫһ�����ַ���β����StringBuilder���á�
            TestStringArgumentsFixLength(inString, sb, bufferSize + 1);

            // ������溯�����õ����һ��������StringBuilder���������ָ������Ȼ���н����ȷ����������
            // �����ܵ���ִ�н����йܴ����С����п�����.NET Framework��СBUG��
            // ��������˷�������һ�������ʱ�䡣
            // TestStringArgumentsFixLength(inString, sb, sb.Capacity + 1);

            Console.WriteLine("Original: {0}", inString);
            Console.WriteLine("Copied: {0}", sb.ToString());
        }
        
        /// <summary>
        /// ��Ϊ���������ַ������ɷ��йܺ��������ڴ档
        /// </summary>
        private static void TestArgumentsOut()
        {
            // 1. IntPtr
            string strResult;
            IntPtr strIntPtr = IntPtr.Zero;
            TestStringArgumentOutIntPtr(1, ref strIntPtr);
            strResult = Marshal.PtrToStringUni(strIntPtr);
            // ����IntPtr���ݵı�������Ҫ�ֹ��ͷŷ��й��ڴ�
            Marshal.FreeCoTaskMem(strIntPtr);
            Console.WriteLine("Return string IntPtr: {0}", strResult);

            // 2. String
            TestStringArgumentOut(2, ref strResult);
            Console.WriteLine("Return string value: {0}", strResult);
        }

        private static void TestMarshalArguments()
        {
            string string1 = "Hello";
            string string2 = "���磡";
            int outBufferSize = string1.Length + string2.Length + 2;
            StringBuilder outBuffer = new StringBuilder(outBufferSize);
            TestStringMarshalArguments(string1, string2, outBuffer, outBufferSize);

            Console.WriteLine("���йܺ������ص��ַ���Ϊ��{0}", outBuffer.ToString());
        }

        /// <summary>
        /// ��Ϊ��������ֵ�������ַ������
        /// </summary>
        private static void TestStringResult()
        {
            // 1. IntPtr
            string result;
            IntPtr strPtr = TestStringAsResultIntPtr(1);
            result = Marshal.PtrToStringUni(strPtr);
            // ����IntPtr���ݵı�������Ҫ�ֹ��ͷŷ��й��ڴ�
            Marshal.FreeCoTaskMem(strPtr);
            Console.WriteLine("Return string IntPtr: {0}", result);

            // 2. String
            result = TestStringAsResult(2);
            Console.WriteLine("Return string value: {0}", result);
        }

        /// <summary>
        /// ��BSTR��Ϊ�����������ֵ�Ͳ���
        /// </summary>
        private static void TestBSTR()
        {
            IntPtr pString = IntPtr.Zero;
            IntPtr result = IntPtr.Zero;

            result = TestBSTRString(out pString);

            if (IntPtr.Zero != pString)
            {
                string argString = Marshal.PtrToStringBSTR(pString);
                Console.WriteLine("����������BSTRֵ��{0}", argString);

                // �ͷ�BSTR
                Marshal.FreeBSTR(pString);
            }

            if (IntPtr.Zero != result)
            {
                string retString = Marshal.PtrToStringBSTR(result);
                Console.WriteLine("�������ص�BSTRֵ��{0}", retString);

                // �ͷ�BSTR
                Marshal.FreeBSTR(result);
            }

        }
    }
}
