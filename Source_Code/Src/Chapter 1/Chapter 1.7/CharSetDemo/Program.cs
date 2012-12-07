using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace CharSetDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //ReverseStringWrapper.Test();
            //ReverseStringWrapper.TestEx();


            //���·������ڲ���ͨ��CharSet��������Win32 API�в���MessageBox�������ķ�ʽ
            //MessageBoxAnsiWrapper.SayHelloWorld();
            //MessageBoxUnicodeWrapper.SayHelloWorld();

            ReverseStringAnsiWrapper.Test();
            ReverseStringUnicodeWrapper.Test();

            Console.WriteLine("\r\n��������˳�...");
            Console.Read();
        }
    }

    class ReverseStringWrapper
    {
        //Unmanaged function
        //void ReverseAnsiString(char* rawString, char* reversedString);
        //����������ʽ��ָ���ַ���ΪAnsi����Ϊ����Ĭ��ֵ
        [DllImport("NativeLib.dll" /*, CharSet = CharSet.Ansi*/)]
        static extern void ReverseAnsiString(string rawString, StringBuilder reversedString);

        //Unmanaged function
        //void ReverseUnicodeString(wchar_t* rawString, wchar_t* reversedString)
        //���ڷ��йܺ������õ��ǿ��ַ���������������ʽ��ָ��CharSet
        [DllImport("NativeLib.dll", CharSet = CharSet.Unicode)]
        static extern void ReverseUnicodeString(string rawString, StringBuilder reversedString);

        public static void Test()
        {
            string rawString = "Bill Gates";
            StringBuilder reversedString = new StringBuilder(rawString.Length);

            ReverseAnsiString(rawString, reversedString);
            Console.WriteLine("Using ANSI, raw string: {0}, reversed string: {1}", rawString, reversedString);

            ReverseUnicodeString(rawString, reversedString);
            Console.WriteLine("Using Unicode, raw string: {0}, reversed string: {1}", rawString, reversedString);
        }

        public static void TestEx()
        {
            string rawAnsiString = "Bill Gates";
            StringBuilder reversedAnsiString = new StringBuilder(rawAnsiString.Length);
            ReverseAnsiString(rawAnsiString, reversedAnsiString);
            Console.WriteLine("Raw Ansi string: {0}, reversed Ansi string: {1}", rawAnsiString, reversedAnsiString);

            string rawUnicodeString = "�ȶ����Ǵ�";
            StringBuilder reversedUnicodeString = new StringBuilder(rawUnicodeString.Length);
            ReverseUnicodeString(rawUnicodeString, reversedUnicodeString);
            Console.WriteLine("Raw Unicode string: {0}, reversed Unicode string: {1}", rawUnicodeString, reversedUnicodeString);
        }
    }

    class ReverseStringAnsiWrapper
    {
        //Unmanaged function
        //void __cdecl ReverseStringA(char* rawString, char* reversedString);
        //����������ʽ��ָ���ַ���ΪAnsi����Ϊ����Ĭ��ֵ
        [DllImport("NativeLib.dll" , CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        static extern void ReverseString(string rawString, StringBuilder reversedString);

        public static void Test()
        {
            string rawString = "Bill Gates";
            StringBuilder reversedString = new StringBuilder(rawString.Length);

            ReverseString(rawString, reversedString);
            Console.WriteLine("Using ANSI version, \r\nraw string: {0}, \r\nreversed string: {1}", rawString, reversedString);
        }
    }
    
    class ReverseStringUnicodeWrapper
    {
        //Unmanaged function
        //void __cdecl ReverseStringW(wchar_t* rawString, wchar_t* reversedString);
        //���ڷ��йܺ������õ��ǿ��ַ���������������ʽ��ָ��CharSet
        [DllImport("NativeLib.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        static extern void ReverseString(string rawString, StringBuilder reversedString);

        public static void Test()
        {
            string rawUnicodeString = "�ȶ����Ǵ�";
            StringBuilder reversedUnicodeString = new StringBuilder(rawUnicodeString.Length);
            ReverseString(rawUnicodeString, reversedUnicodeString);
            Console.WriteLine("Using Unicode version, \r\nraw Unicode string: {0}, \r\nreversed Unicode string: {1}", rawUnicodeString, reversedUnicodeString);
        }
    }

    class MessageBoxAnsiWrapper
    {
        // Win32 API
        // int MessageBox(      
        //    HWND hWnd,
        //    LPCTSTR lpText,
        //    LPCTSTR lpCaption,
        //    UINT uType
        //);
        [DllImport("user32.dll", CharSet = CharSet.Ansi)]
        public static extern int MessageBox(
            int hwnd,
            string lpText,
            string lpCaption,
            int wType
        );

        public static void SayHelloWorld()
        {
            MessageBox(0, "Hello world!", "Welcome", 0);
        }
    }

    class MessageBoxUnicodeWrapper
    {
        // Win32 API
        // int MessageBox(      
        //    HWND hWnd,
        //    LPCTSTR lpText,
        //    LPCTSTR lpCaption,
        //    UINT uType
        //);
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern int MessageBox(
            int hwnd,
            string lpText,
            string lpCaption,
            int wType
        );

        public static void SayHelloWorld()
        {
            MessageBox(0, "���磬��ã�", "Welcome", 0);
        }
    }

}
