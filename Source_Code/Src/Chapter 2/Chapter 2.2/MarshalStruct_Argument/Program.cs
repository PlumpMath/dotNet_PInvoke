using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace MarshalStruct_Argument
{
    class Program
    {
        private const string _dllName = "NativeLib.dll";

        // �ṹ��ʾ����������
        //typedef struct _SIMPLESTRUCT
        //{
        //    int    intValue;
        //    short  shortValue;
        //    float  floatValue;
        //    double doubleValue;
        //} SIMPLESTRUCT, *PSIMPLESTRUCT;
        [StructLayout(LayoutKind.Sequential)]
        private struct ManagedSimpleStruct
        {
            public int intValue;
            public short shortValue;
            public float floatValue;
            public double doubleValue;
        }

        // void __cdecl TestStructArgumentByVal(SIMPLESTRUCT simpleStruct);
        [DllImport(_dllName, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        private extern static void TestStructArgumentByVal(ManagedSimpleStruct argStruct);

        // void __cdecl TestStructArgument(PSIMPLESTRUCT pStruct);
        [DllImport(_dllName, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        private extern static void TestStructArgumentByRef(ref ManagedSimpleStruct argStruct);

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct ManagedSimpleStruct_WrongPack
        {
            public int intValue;
            public short shortValue;
            public float floatValue;
            public double doubleValue;
        }

        // void __cdecl TestStructArgument(PARGUMENTSTRUCT pStruct);
        [DllImport(_dllName, CharSet = CharSet.Unicode,
            CallingConvention = CallingConvention.Cdecl,
            EntryPoint = "TestStructArgumentByRef")]
        private extern static void TestStructArgumentWrongPack(ref ManagedSimpleStruct_WrongPack argStruct);

        static void Main(string[] args)
        {
            TestStructArgumentVal();

            TestCorrectPack();

            TestWrongPack();

            Console.WriteLine("\r\n��������˳�...");
            Console.Read();
        }

        private static void TestStructArgumentVal()
        {
            ManagedSimpleStruct simpleStruct = new ManagedSimpleStruct();
            simpleStruct.intValue = 10;
            simpleStruct.shortValue = 20;
            simpleStruct.floatValue = 3.5f;
            simpleStruct.doubleValue = 6.8f;

            TestStructArgumentByVal(simpleStruct);

            Console.WriteLine("\n�ṹ�������ݣ�int = {0}, short = {1}, float = {2:f6}, double = {3:f6}",
                simpleStruct.intValue, simpleStruct.shortValue, simpleStruct.floatValue, simpleStruct.doubleValue);
        }

        private static void TestCorrectPack()
        {
            Console.WriteLine("�йܴ��붨��Ľṹ���ڷ��йܴ����еĴ�СΪ��{0}�ֽ�", Marshal.SizeOf(typeof(ManagedSimpleStruct)));
            ManagedSimpleStruct argStruct = new ManagedSimpleStruct();
            argStruct.intValue = 1;
            argStruct.shortValue = 2;
            argStruct.floatValue = 3.0f;
            argStruct.doubleValue = 4.5f;
            TestStructArgumentByRef(ref argStruct);

            Console.WriteLine("\n�ṹ�������ݣ�int = {0}, short = {1}, float = {2:f6}, double = {3:f6}",
                argStruct.intValue, argStruct.shortValue, argStruct.floatValue, argStruct.doubleValue);
        }

        private static void TestWrongPack()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n========================\n������ڴ���뷽ʽ:");
            Console.ResetColor();
            Console.WriteLine("�йܴ��붨��Ľṹ���ڷ��йܴ����еĴ�СΪ��{0}�ֽ�", Marshal.SizeOf(typeof(ManagedSimpleStruct_WrongPack)));
            ManagedSimpleStruct_WrongPack argStruct = new ManagedSimpleStruct_WrongPack();
            argStruct.intValue = 1;
            argStruct.shortValue = 2;
            argStruct.floatValue = 3.0f;
            argStruct.doubleValue = 4.5f;
            TestStructArgumentWrongPack(ref argStruct);

            Console.WriteLine("\n�ṹ�������ݣ�int = {0}, short = {1}, float = {2:f6}, double = {3:f6}",
                argStruct.intValue, argStruct.shortValue, argStruct.floatValue, argStruct.doubleValue);
        }

    }
}
