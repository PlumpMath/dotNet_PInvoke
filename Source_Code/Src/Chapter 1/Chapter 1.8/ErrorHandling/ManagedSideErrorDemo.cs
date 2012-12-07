using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace ErrorHandling
{
    public class DLLErrorDemo
    {
        //��һ���������ڵķ��й�dll������
        [DllImport("SomeDLL.dll")]
        static extern void DoSomeThingFunc(int paramInt);

        public static void TestNonDll()
        {
            try
            {
                DoSomeThingFunc(100);
                Console.WriteLine("Finish to call functin DoSomeThingFunc.");
            }
            catch (DllNotFoundException dllNotFoundExc)
            {
                Console.WriteLine("DllNotFoundException was detected, error message: \r\n{0}",
                    dllNotFoundExc.Message);
            }
        }
    }

    public class EntryPointErrorDemo1
    {
        // ��������һ��NativeLib.dll�в������ڵĳ�������
        [DllImport("NativeLib.dll")]
        static extern float Divide(float factorA, float factorB);

        public static void TestNonFunction()
        {
            try
            {
                float result = Divide(100F, 818F);
                Console.WriteLine("Divide result from unmanaged function is {0}.", result);
            }
            catch (DllNotFoundException dllNotFoundExc)
            {
                Console.WriteLine("DllNotFoundException was detected, error message: \r\n{0}",
                    dllNotFoundExc.Message);
            }
            catch (EntryPointNotFoundException entryPointExc)
            {
                Console.WriteLine("EntryPointNotFoundException was detected, error message: \r\n{0}",
                                   entryPointExc.Message);
            }
        }
    }

    public class EntryPointErrorDemo2
    {
        // ��������һ��NativeLib.dll�д��ڵĺ�����Multiply������ȴʹ�ô������������
        [DllImport("NativeLib.dll")]
        static extern double Multiply(double factorA, double factorB);

        public static void TestNonFunctionWithWrongParamType()
        {
            try
            {
                double result = Multiply(100, 818);
                Console.WriteLine("Multiply result from unmanaged function is {0}.", result);
            }
            catch (DllNotFoundException dllNotFoundExc)
            {
                Console.WriteLine("DllNotFoundException was detected, error message: \r\n{0}",
                    dllNotFoundExc.Message);
            }
            catch (EntryPointNotFoundException entryPointExc)
            {
                Console.WriteLine("EntryPointNotFoundException was detected, error message: \r\n{0}",
                                   entryPointExc.Message);
            }
        }
    }

    public class CharSetErrorDemo
    {
        //Unmanaged function
        //void ReverseUnicodeString(wchar_t* rawString, wchar_t* reversedString)
        //���ڷ��йܺ������õ��ǿ��ַ���������������ʽ��ָ��CharSetΪUnicode
        //���ｫ�ַ��������ָ��ΪAnsi
        [DllImport("NativeLib.dll", CharSet = CharSet.Ansi)]
        static extern void ReverseUnicodeString(string rawString, StringBuilder reversedString);

        public static void Test()
        {
            try
            {
                string rawString = "ABCDE";
                StringBuilder reversedString = new StringBuilder(rawString.Length);

                ReverseUnicodeString(rawString, reversedString);
                Console.WriteLine("Using Ansi as wrong CharSet, raw string: {0}, reversed string: {1}",
                    rawString, reversedString);
            }
            catch (Exception exc)
            {
                Console.WriteLine("Exception from ReverseAnsiString: {0}",
                    exc.Message);
            }
        }
    }


}
