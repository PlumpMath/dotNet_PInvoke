using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace MarshalArray
{
    class Program
    {
        private const string _dllName = "NativeLib.dll";

        // ������ʾ��
        // UINT __cdecl TestArrayOfChar(char charArray[], int arraySize);
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private extern static uint TestArrayOfChar([In, Out] char[] charArray, int arraySize);

        // int __cdecl TestArrayOfInt(int intArray[], int arraySize);
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        private extern static int TestArrayOfInt(int[] intArray, int arraySize);

        // �ַ�������ʾ��
        // void __cdecl TestArrayOfString(char* ppStrArray[], int size)
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private extern static void TestArrayOfString([In, Out] string[] charArray, int arraySize);

        // int __cdecl TestRefArrayOfString(void** strArray, int* size)
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private extern static int TestRefArrayOfString(out IntPtr charArray, out int arraySize);

        static void Main(string[] args)
        {
            TestCharArray();

            TestIntArray();

            TestStringArray();

            TestRefStringArray();

            Console.WriteLine("\r\n��������˳�...");
            Console.Read();
        }

        private static void TestCharArray()
        {
            char[] charArray = new char[] { 'a', '1', 'b', '2', 'c', '3', '4' };
            Console.WriteLine("\n����ǰ�ַ�����Ԫ��Ϊ��");
            foreach (char c in charArray)
            {
                Console.Write("{0} ", c);
            }

            uint digitCount = TestArrayOfChar(charArray, charArray.Length);

            Console.WriteLine("\n����ǰ�ַ����������ָ���Ϊ��{0}", digitCount);

            Console.WriteLine("\n���ú��ַ�����Ԫ��Ϊ��");
            foreach (char c in charArray)
            {
                Console.Write("{0} ", c);
            }
        }

        private static void TestIntArray()
        {
            int[] intArray = new int[] { 1, 2, 3, 4, 5, 6, 7 };
            Console.WriteLine("\n\n����ǰ��������Ԫ��Ϊ��");
            foreach (int i in intArray)
            {
                Console.Write("{0} ", i);
            }

            int sum = TestArrayOfInt(intArray, intArray.Length);

            Console.WriteLine("\n����ǰ��������������Ԫ��֮��Ϊ��{0}", sum);

            Console.WriteLine("\n���ú���������Ԫ��Ϊ��");
            foreach (int i in intArray)
            {
                Console.Write("{0} ", i);
            }

            Console.WriteLine();
        }

        private static void TestStringArray()
        {
            string[] strings = new string[] { 
                "This is the first string.",
                "Those are brown horse.",
                "The quick brown fox jumps over a lazy dog." };

            Console.WriteLine("\nԭʼ�ַ��������е�Ԫ��Ϊ��");
            foreach (string originalString in strings)
            {
                Console.WriteLine(originalString);
            }

            TestArrayOfString(strings, strings.Length);

            Console.WriteLine("�޸ĺ��ַ��������е�Ԫ��Ϊ��");
            foreach (string reversedString in strings)
            {
                Console.WriteLine(reversedString);
            }
        }

        private static void TestRefStringArray()
        {
            IntPtr arrayPtr;

            // ��Ϊ�������ڷ��йܴ����ڷ���ģ�������Ҫͨ������ֵ���������
            // ������arraySize��returnCount�ķ���ֵӦ����һ����
            int arraySize;
            int returnCount = TestRefArrayOfString(out arrayPtr, out arraySize);
            // ���ݷ���ֵȷ���ַ������������йܴ������������Ӧ��ָ������
            IntPtr[] arrayPtrs = new IntPtr[returnCount];
            // �����й������е����ݿ������йܴ�����
            Marshal.Copy(arrayPtr, arrayPtrs, 0, returnCount);

            Console.WriteLine("\n���ص��ַ���������Ԫ�صĸ���Ϊ��{0}", returnCount);
            Console.WriteLine("�ַ���Ԫ�أ�");
            // �����ַ������飬���ڴ�����յĽ��
            string[] strings = new string[returnCount];
            for (int i = 0; i < returnCount; i++)
            {
                strings[i] = Marshal.PtrToStringUni(arrayPtrs[i]);
                // �ͷŷ��й��ַ����ڴ�
                Marshal.FreeCoTaskMem(arrayPtrs[i]);
                Console.WriteLine("#{0}: {1}", i, strings[i]);
            }

            // �ͷŷ��й��ַ��������ڴ�
            Marshal.FreeCoTaskMem(arrayPtr);
        }
    }
}
