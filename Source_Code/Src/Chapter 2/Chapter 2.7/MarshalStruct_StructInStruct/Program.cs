using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace MarshalStruct_StructInStruct
{
    class Program
    {
        private const string _dllName = "NativeLib.dll";

        //typedef struct _PERSONNAME
        //{
        //    char* first;
        //    char* last;
        //    char* displayName;
        //} PERSONNAME, *PPERSONNAME;
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct PersonName
        {
            public string first;
            public string last;
            public string displayName;
        }

        //typedef struct _PERSON
        //{
        //    PPERSONNAME pName;
        //    int age; 
        //} PERSON, *PPERSON;
        [StructLayout(LayoutKind.Sequential)]
        public struct Person
        {
            public IntPtr name;
            public int age;
        }

        //void __cdecl TestStructInStructByRef(PPERSON pPerson);
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private extern static void TestStructInStructByRef(ref Person person);

        //typedef struct _PERSON2
        //{
        //    PERSONNAME name;
        //    int age; 
        //} PERSON2, *PPERSON2;
        [StructLayout(LayoutKind.Sequential)]
        public struct Person2
        {
            public PersonName name;
            public int age;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Person2_Flattened
        {
            public string first;
            public string last;
            public string displayName;
            public int age;
        }

        //void __cdecl TestStructInStructByVal(PPERSON2 pPerson);
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private extern static void TestStructInStructByVal(ref Person2 person);

        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private extern static void TestStructInStructByVal(ref Person2_Flattened person);
        
        static void Main(string[] args)
        {
            TestStructAsRefField();

            TestStructAsValField();

            TestStructAsValFieldFlattened();

            Console.WriteLine("\r\n��������˳�...");
            Console.Read();
        }

        private static void TestStructAsRefField()
        {
            Console.WriteLine("\n�ṹ����Ϊ�������ͳ�Ա");
            // ��������
            PersonName name = new PersonName();
            name.last = "Cui";
            name.first = "Xiaoyuan";

            // ������
            Person person = new Person();
            person.age = 27;

            IntPtr nameBuffer = Marshal.AllocCoTaskMem(Marshal.SizeOf(name));
            Marshal.StructureToPtr(name, nameBuffer, false);

            person.name = nameBuffer;

            Console.WriteLine("����ǰ��ʾ����Ϊ��{0}", name.displayName);
            TestStructInStructByRef(ref person);

            PersonName newValue =
                (PersonName)Marshal.PtrToStructure(person.name, typeof(PersonName));

            // �ͷ��ڷ��йܴ����з����PersonNameʵ���ڴ�
            Marshal.DestroyStructure(nameBuffer, typeof(PersonName));

            Console.WriteLine("���ú���ʾ����Ϊ��{0}", newValue.displayName);

        }

        private static void TestStructAsValField()
        {
            Console.WriteLine("\n�ṹ����Ϊֵ���ͳ�Ա");
            Person2 person = new Person2();
            person.name.last = "Huang";
            person.name.first = "Jizhou";
            person.name.displayName = string.Empty;
            person.age = 26;
            
            TestStructInStructByVal(ref person);
        }

        private static void TestStructAsValFieldFlattened()
        {
            Console.WriteLine("\n�ṹ����Ϊֵ���ͳ�Ա��flattened��");
            Person2_Flattened person = new Person2_Flattened();
            person.last = "Huang";
            person.first = "Jizhou";
            person.displayName = string.Empty;
            person.age = 26;

            TestStructInStructByVal(ref person);
        }

    }
}
