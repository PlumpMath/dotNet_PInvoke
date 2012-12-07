using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace ErrorHandling
{
    public class IncorrectParamTest
    {
        // Unmanaged function
        //void __stdcall PrintMsgByFlag(void* data, int flag)

        //�������йܺ���
        //����һ����������Ϊ��һ�����ĺ���
        [DllImport("NativeLib.dll", EntryPoint = "PrintMsgByFlag")]
        static extern void PrintID(ref int id, int flag);

        public static void Test()
        {
            //���÷��йܺ���
            int id = 100;

            //PrintID(ref id, 1);

            // ���������������Ľ��
            PrintID(ref id, 2);
        }
    }

    public class DivideByZeroTest
    {
        //Unmanaged function
        //extern "C" __declspec(dllexport) int __stdcall Divide(int numerator, int denominator);
        [DllImport("NativeLib.dll")]
        static extern int Divide(int numerator, int denominator);

        public static void Test()
        {
            try
            {
                //���÷��йܺ���
                int numerator = 8, denominator = 0;
                int result = Divide(numerator, denominator);

                //��ӡ���
                Console.WriteLine(string.Format("{0} / {1} = {2} ", numerator, denominator, result));
            }
            catch (DivideByZeroException DBZExc)
            {
                Console.WriteLine("DivideByZeroException from Divide: \r\n{0}",
                    DBZExc.Message);
            }
        }
    }

    public class UnallocatedBuffer
    {
        //Unmanaged function
        //void ReverseUnicodeString(wchar_t* rawString, wchar_t* reversedString)
        //���ڷ��йܺ������õ��ǿ��ַ���������������ʽ��ָ��CharSetΪUnicode
        [DllImport("NativeLib.dll", CharSet = CharSet.Unicode)]
        static extern void ReverseUnicodeString(string rawString, StringBuilder reversedString);

        public static void Test()
        {
            try
            {
                string rawString = "Bill Gates";
                //���ﲻ�����ڴ�ռ䣬������������
                StringBuilder reversedString = null;

                ReverseUnicodeString(rawString, reversedString);
                Console.WriteLine("Raw string: {0}, reversed string: {1}",
                    rawString, reversedString);
            }
            catch (AccessViolationException exc)
            {
                Console.WriteLine("AccessViolationException from ReverseUnicodeString: \r\n{0}",
                    exc.Message);
            }
        }
    }

    public class UnmanageException
    {
        //Unmanaged function
        //extern "C" __declspec(dllexport) void UnmanagedExceptionFromCpp();
        [DllImport("NativeLib.dll")]
        static extern void UnmanagedExceptionFromCpp();

        //Unmanaged function
        //extern "C" __declspec(dllexport) void UnmanagedExcetionViaRaiseException();
        [DllImport("NativeLib.dll")]
        static extern void UnmanagedExcetionViaRaiseException();

        //Unmanaged function
        //extern "C" __declspec(dllexport) void UnmanagedExcetionViaRaiseExceptionNoRegular();
        [DllImport("NativeLib.dll")]
        static extern void UnmanagedExcetionViaRaiseExceptionNoRegular();

        public static void TestCppException()
        {
            try
            {
                UnmanagedExceptionFromCpp();
            }
            catch (SEHException SEHExc)
            {
                Console.WriteLine("SEHException from UnmanagedExceptionFromCpp: \r\n{0}",
                                   SEHExc.Message);
            }
        }

        public static void TestCStyleException()
        {
            try
            {
                UnmanagedExcetionViaRaiseException();
            }
            catch (SEHException SEHExc)
            {
                Console.WriteLine("SEHException from UnmanagedExcetionViaRaiseException: \r\n{0}",
                                   SEHExc.Message);
            }
            catch (AccessViolationException exc)
            {
                Console.WriteLine("AccessViolationException from ReverseUnicodeString: \r\n{0}",
                    exc.Message);
            }
        }

        public static void TestCStyleExceptionNoRegular()
        {
            try
            {
                UnmanagedExcetionViaRaiseExceptionNoRegular();
            }
            catch (SEHException SEHExc)
            {
                Console.WriteLine("SEHException from UnmanagedExcetionViaRaiseExceptionNoRegular: \r\n{0}",
                                   SEHExc.Message);
            }
        }
    }

}
