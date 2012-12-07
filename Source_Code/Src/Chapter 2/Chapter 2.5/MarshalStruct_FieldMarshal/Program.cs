using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace MarshalStruct_FieldMarshal
{
    class Program
    {
        private const string _dllName = "NativeLib.dll";

        //typedef struct _MSEMPLOYEE_EX
        //{
        //    UINT employeeID;
        //    wchar_t* displayName; 
        //    char* alias; 
        //    bool isInRedmond;
        //    BOOL isFamale;
        //} MSEMPLOYEE_EX, *PMSEMPLOYEE_EX;

        [StructLayout(LayoutKind.Sequential)]
        private struct MsEmployeeEx
        {
            public uint EmployeeID;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string DisplayName;
            [MarshalAs(UnmanagedType.LPStr)]
            public string Alias;
            [MarshalAs(UnmanagedType.I1)]
            public bool IsInRedmond;
            public short EmployedYear;
            [MarshalAs(UnmanagedType.Bool)]
            public bool IsFamale;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MsEmployeeEx_Wrong
        {
            public uint EmployeeID;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string DisplayName;
            [MarshalAs(UnmanagedType.LPStr)]
            public string Alias;
            // ���������ָ�����͵ķ�ʽ��.NET��bool���ͻᱻĬ�Ϸ��ͳ�4�ֽڵ�Win32 BOOL����
            // �⽫���½ṹ���ڷ��йܴ����еĴ�С�����ı䡣
            // [MarshalAs(UnmanagedType.I1)]
            public bool IsInRedmond;
            public short EmployedYear;
            [MarshalAs(UnmanagedType.Bool)]
            public bool IsFamale;
        }

        // void __cdecl GetEmployeeInfoEx(PMSEMPLOYEE_EX pEmployee);
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        private extern static void GetEmployeeInfoEx(ref MsEmployeeEx employee);

        // void __stdcall PrintEmployeeInfoEx(MSEMPLOYEE_EX pEmployee);
        [DllImport(_dllName, CallingConvention = CallingConvention.StdCall)]
        private extern static void PrintEmployeeInfoEx(MsEmployeeEx_Wrong employee);

        // ������ʾָ���������������ʹ�����׳�EntryPointNotFoundException�쳣��
        // Ҳ����õ���ȷ�Ľ������Ϊ�ڴ�Ĳ����Ѿ����ƻ�
        //[DllImport(_dllName, CallingConvention = CallingConvention.StdCall, EntryPoint = "_PrintEmployeeInfoEx@20")]
        //private extern static void PrintEmployeeInfoEx(MsEmployeeEx_Wrong employee);

        static void Main(string[] args)
        {
            TestGetEmployeeEx();

            // ������������ᵼ�¡�EntryPointNotFoundException���쳣�׳���
            // ����û����ȷ��ָ���ṹ���������ֶεķ��ͷ�ʽ�����½ṹ��
            // �ڷ���ʱ�Ĵ�С����йܴ��벻һ�¡�
            // �����ڷ��йܴ���ʹ��stdcall, ʹ�����յĺ���������Ҫ�����ڲ�����С��
            // ���Դ���Ľṹ�嶨�壬��ʹDllImportAttribute���Ҳ�����Ӧ�ĺ�����ڡ�
            // TestPrintEmployeeInfoEx();

            Console.WriteLine("\r\n��������˳�...");
            Console.Read();
        }

        private static void TestGetEmployeeEx()
        {
            ShowMarshalSize(typeof(MsEmployeeEx));
            MsEmployeeEx employee = new MsEmployeeEx();
            employee.EmployeeID = 10001;
            GetEmployeeInfoEx(ref employee);

            Console.WriteLine("Ա����Ϣ:");
            Console.WriteLine("ID: {0}", employee.EmployeeID);
            Console.WriteLine("Alias: {0}", employee.Alias);
            Console.WriteLine("����: {0}", employee.DisplayName);
            Console.WriteLine("�Ա�: {0}", employee.IsFamale ? "Ů" : "��");
            Console.WriteLine("����: {0}", employee.EmployedYear);
            Console.WriteLine("�Ƿ����ܲ�: {0}", employee.IsInRedmond ? "��" : "��");
        }

        private static void TestPrintEmployeeInfoEx()
        {
            ShowMarshalSize(typeof(MsEmployeeEx_Wrong));
            MsEmployeeEx_Wrong employee = new MsEmployeeEx_Wrong();
            employee.EmployeeID = 10001;
            employee.Alias = "xcui";
            employee.DisplayName = "Xiaoyuan Cui";
            employee.IsFamale = false;
            employee.IsInRedmond = false;
            employee.EmployedYear = 2;

            PrintEmployeeInfoEx(employee);
        }

        private static void ShowMarshalSize(Type type)
        {
            Console.WriteLine("�йܴ��붨��Ľṹ��({0})�ڷ��йܴ����еĴ�СΪ({1})�ֽ�", type.Name, Marshal.SizeOf(type));
        }
    }
}
