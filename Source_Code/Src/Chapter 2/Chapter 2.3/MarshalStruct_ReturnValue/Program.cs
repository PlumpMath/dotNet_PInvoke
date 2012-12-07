using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace MarshalStruct_ReturnValue
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

        // PSIMPLESTRUCT __cdecl TestStructAsResult(void);
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        private extern static IntPtr TestReturnStruct();

        // PSIMPLESTRUCT __cdecl TestReturnNewStruct(void)
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        private extern static IntPtr TestReturnNewStruct();

        // void __cdecl FreeStruct(PSIMPLESTRUCT pStruct)
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        private extern static void FreeStruct(IntPtr pStruct);

        // void __cdecl TestReturnStructFromArg(PSIMPLESTRUCT* ppStruct)
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        private extern static void TestReturnStructFromArg(ref IntPtr pStruct);

        static void Main(string[] args)
        {
            TestReturnStructByNew();

            TestReturnStructByCoTaskMemAlloc();

            TestReturnStructByArg();

            Console.WriteLine("\r\n��������˳�...");
            Console.Read();
        }

        private static void TestReturnStructByNew()
        {
            IntPtr pStruct = TestReturnNewStruct();
            ManagedSimpleStruct retStruct =
                (ManagedSimpleStruct)Marshal.PtrToStructure(pStruct, typeof(ManagedSimpleStruct));

            // �ڷ��йܴ�����ʹ��new/malloc������ڴ棬
            // ��Ҫ���ö�Ӧ���ͷ��ڴ���ͷŷ��������ͷŵ�
            FreeStruct(pStruct);

            Console.WriteLine("\n���йܺ������صĽṹ�����ݣ�int = {0}, short = {1}, float = {2:f6}, double = {3:f6}",
                retStruct.intValue, retStruct.shortValue, retStruct.floatValue, retStruct.doubleValue);
 
        }

        private static void TestReturnStructByCoTaskMemAlloc()
        {
            IntPtr pStruct = TestReturnStruct();
            ManagedSimpleStruct retStruct =
                (ManagedSimpleStruct)Marshal.PtrToStructure(pStruct, typeof(ManagedSimpleStruct));

            // �ڷ��йܴ�����ʹ��CoTaskMemAlloc������ڴ棬����ʹ��Marshal.FreeCoTaskMem�����ͷ�
            Marshal.FreeCoTaskMem(pStruct);

            Console.WriteLine("\n���صĽṹ�����ݣ�int = {0}, short = {1}, float = {2:f6}, double = {3:f6}",
                retStruct.intValue, retStruct.shortValue, retStruct.floatValue, retStruct.doubleValue);
        }

        private static void TestReturnStructByArg()
        {
            IntPtr ppStruct = IntPtr.Zero;

            TestReturnStructFromArg(ref ppStruct);

            ManagedSimpleStruct retStruct =
                (ManagedSimpleStruct)Marshal.PtrToStructure(ppStruct, typeof(ManagedSimpleStruct));

            // �ڷ��йܴ�����ʹ��CoTaskMemAlloc������ڴ棬����ʹ��Marshal.FreeCoTaskMem�����ͷ�
            Marshal.FreeCoTaskMem(ppStruct);

            Console.WriteLine("\n���صĽṹ�����ݣ�int = {0}, short = {1}, float = {2:f6}, double = {3:f6}",
                retStruct.intValue, retStruct.shortValue, retStruct.floatValue, retStruct.doubleValue);

        }
    }
}
