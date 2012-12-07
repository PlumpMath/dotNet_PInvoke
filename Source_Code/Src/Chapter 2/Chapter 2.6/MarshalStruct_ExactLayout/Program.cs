using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace MarshalStruct_ExactLayout
{
    class Program
    {
        private const string _dllName = "NativeLib.dll";

        //#pragma pack(1)
        //typedef struct _MSEMPLOYEE_EX2
        //{
        //    UINT      EmployeeID;       //4 bytes -> Offset 0
        //    USHORT    EmployedYear;     //2 bytes -> Offset 4
        //    BYTE      CurrentLevel;     //1 bytes -> Offset 6
        //    wchar_t*  Alias;            //4 bytes -> Offset 7
        //    wchar_t*  DisplayName;      //4 bytes -> Offset 11
        //    wchar_t*  OfficeAddress;    //4 bytes -> Offset 15
        //    wchar_t*  OfficePhone;      //4 bytes -> Offset 19
        //    wchar_t*  Title;            //4 bytes -> Offset 23
        //    USHORT    RegionId;         //2 bytes -> Offset 27
        //    int       ZipCode;          //4 bytes -> Offset 29
        //    double    CurrentSalary;    //8 bytes -> Offset 33
        //} MSEMPLOYEE_EX2, *PMSEMPLOYEE_EX2;
        //#pragma pack()

        // ����������ʾָ�������һ���ֶΣ����Բ���ָ��StructLayout��Size���Դ�СΪ41
        // ����˼����Pack���Բ���һ��Ҫ��ʾָ��Ϊ1, ��������йܴ��붨��һ�¡�
        // ��ʱ��ȻMarshaler��ʾ�Ĵ�СΪ48�����ǲ�����Ӱ�쵽�����
         [StructLayout(LayoutKind.Explicit)]
        //[StructLayout(LayoutKind.Explicit, Pack = 1)]
        private struct MsEmployeeEx2
        {
            [FieldOffset(0)]    public uint EmployeeID;
            [FieldOffset(4)]    public ushort EmployedYear;
            [FieldOffset(6)]    public byte CurrentLevel;
            [FieldOffset(27)]   public ushort RegionId;
            [FieldOffset(29)]   public int ZipCode;
            [FieldOffset(33)]   public double CurrentSalary;
        }

        [StructLayout(LayoutKind.Explicit, Size = 41)]
        private struct MsEmployeeEx2_Partial
        {
            [FieldOffset(0)]    public uint EmployeeID;
            [FieldOffset(4)]    public ushort EmployedYear;
            [FieldOffset(6)]    public byte CurrentLevel;

            // ����ɾ�������һ���ֶεĶ��壬������Ҫʹ��StructLayout��Size����ָ����СΪ41
            //[FieldOffset(27)]   public ushort RegionId;
            //[FieldOffset(29)]   public int ZipCode;
            //[FieldOffset(33)]   public double CurrentSalary;
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct MsEmployeeEx2_Partial2
        {
            [FieldOffset(0)]    public uint EmployeeID;
            [FieldOffset(4)]    public ushort EmployedYear;
            [FieldOffset(6)]    public byte CurrentLevel;
            //[FieldOffset(27)]   public ushort RegionId;
            //[FieldOffset(29)]   public int ZipCode;
            //[FieldOffset(33)]   public double CurrentSalary;
        }

        // void __cdecl GetEmployeeInfoEx2(PMSEMPLOYEE_EX2 pEmployee);
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        private extern static void GetEmployeeInfoEx2(ref MsEmployeeEx2 employee);

        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "GetEmployeeInfoEx2")]
        private extern static void GetEmployeeInfoEx2_PartialStruct(ref MsEmployeeEx2_Partial employee);

        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "GetEmployeeInfoEx2")]
        private extern static void GetEmployeeInfoEx2_PartialStruct2(ref MsEmployeeEx2_Partial2 employee);

        //typedef union _SIMPLEUNION
        //{
        //    int i;
        //    double d;
        //} SIMPLEUNION, *PSIMPLEUNION;
        [StructLayout(LayoutKind.Explicit)]
        public struct SimpleUnion
        {
            [FieldOffset(0)]
            public int i;
            [FieldOffset(0)]
            public double d;
        }

        // void __cdecl TestUnion(SIMPLEUNION u, int type)
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void TestUnion(SimpleUnion u, int type);

        //typedef union _SIMPLEUNION2
        //{
        //    int i;
        //    char str[128];
        //} SIMPLEUNION2, *PSIMPLEUNION2;
        [StructLayout(LayoutKind.Explicit, Size = 128)]
        public struct SimpleUnion2_1
        {
            [FieldOffset(0)]
            public int i;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct SimpleUnion2_2
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string str;
        }

        // void __cdecl TestUnion2(SIMPLEUNION2 u, int type)
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void TestUnion2(SimpleUnion2_1 u, int type);

        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void TestUnion2(SimpleUnion2_2 u, int type);

        static void Main(string[] args)
        {
            TestStructExactLayout();

            TestStructExactLayout2();

            // ��������п��ܵ��³������
            //TestStructExactLayout3();

            TestUnion();

            TestUnion2();

            Console.WriteLine("\r\n��������˳�...");
            Console.Read();
        }

        private static void TestStructExactLayout()
        {
            ShowMarshalSize(typeof(MsEmployeeEx2));

            // ����һ�����󣬲����Գ�ʼֵ
            MsEmployeeEx2 employee = new MsEmployeeEx2();
            employee.EmployeeID = 10001;
            employee.EmployedYear = 1;
            employee.CurrentLevel = 59;
            employee.RegionId = 1000;
            employee.ZipCode = 16;
            employee.CurrentSalary = 123456;

            GetEmployeeInfoEx2(ref employee);

            Console.WriteLine("Ա����Ϣ:");
            Console.WriteLine("ID: {0}", employee.EmployeeID);
            Console.WriteLine("����: {0}", employee.EmployedYear);
            Console.WriteLine("ְ��: {0}", employee.CurrentLevel);
            Console.WriteLine("�������: {0}", employee.RegionId);
            Console.WriteLine("��������: {0}", employee.ZipCode);
            Console.WriteLine("����: {0}", employee.CurrentSalary);
        }

        private static void TestStructExactLayout2()
        {
            ShowMarshalSize(typeof(MsEmployeeEx2_Partial));

            // ����һ�����󣬲����Գ�ʼֵ
            MsEmployeeEx2_Partial employee = new MsEmployeeEx2_Partial();
            employee.EmployeeID = 10001;
            employee.EmployedYear = 1;
            employee.CurrentLevel = 59;

            GetEmployeeInfoEx2_PartialStruct(ref employee);

            Console.WriteLine("Ա����Ϣ:");
            Console.WriteLine("ID: {0}", employee.EmployeeID);
            Console.WriteLine("����: {0}", employee.EmployedYear);
            Console.WriteLine("ְ��: {0}", employee.CurrentLevel);
        }

        private static void TestStructExactLayout3()
        {
            ShowMarshalSize(typeof(MsEmployeeEx2_Partial2));

            // ����һ�����󣬲����Գ�ʼֵ
            MsEmployeeEx2_Partial2 employee = new MsEmployeeEx2_Partial2();
            employee.EmployeeID = 10001;
            employee.EmployedYear = 1;
            employee.CurrentLevel = 59;

            // ���ڽṹ���������󣬵��·��Ͳ���������Ľ��ṹ��ӳ�䵽�ڴ��С�
            // ��ᵼ�·��йܴ����ȡ��Ϣʧ�ܣ���������Ӧ�ó��������
            GetEmployeeInfoEx2_PartialStruct2(ref employee);

            Console.WriteLine("Ա����Ϣ:");
            Console.WriteLine("ID: {0}", employee.EmployeeID);
            Console.WriteLine("����: {0}", employee.EmployedYear);
            Console.WriteLine("ְ��: {0}", employee.CurrentLevel);
        }

        private static void TestUnion()
        {
            SimpleUnion u = new SimpleUnion();
            u.i = 10;
            TestUnion(u, 1);

            u.d = 10.10;
            TestUnion(u, 2);
        }

        private static void TestUnion2()
        {
            SimpleUnion2_1 u1Integer = new SimpleUnion2_1();
            u1Integer.i = 10;
            TestUnion2(u1Integer, 1);

            SimpleUnion2_2 u1String = new SimpleUnion2_2();
            u1String.str = "*** This is a string. ***";
            TestUnion2(u1String, 2);		
        }

        private static void ShowMarshalSize(Type type)
        {
            Console.WriteLine("\n�йܴ��붨��Ľṹ��({0})�ڷ��йܴ����еĴ�СΪ({1})�ֽ�", type.Name, Marshal.SizeOf(type));
        }

    }
}
