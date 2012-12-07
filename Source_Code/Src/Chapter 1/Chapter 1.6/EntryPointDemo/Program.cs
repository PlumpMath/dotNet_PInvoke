using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace EntryPointDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintMsgWrapper.Test();

            PrintMsgWrapperEx.PrintInfo(1);
            PrintMsgWrapperEx.PrintInfo("Bill Gates");

            Console.WriteLine("\r\n��������˳�...");
            Console.Read();
        }
    }

    class PrintMsgWrapper
    {
        // Unmanaged function
        //void __stdcall PrintMsgByFlag(void* data, int flag)

        //�������йܺ���
        //����һ����������Ϊ��һ�����ĺ���
        [DllImport("NativeLib.dll", EntryPoint = "PrintMsgByFlag")]
        static extern void PrintID(ref int id, int flag);
        //����һ�������ַ���Ϊ��һ�����ĺ���
        [DllImport("NativeLib.dll", EntryPoint = "PrintMsgByFlag")]
        static extern void PrintName(string name, int flag);

        public static void Test()
        {
            //���÷��йܺ���
            int id = 100;
            string name = "Bill Gates";

            PrintID(ref id, 1);
            PrintName(name, 2);

            // ���������������Ľ��
            //PrintID(ref id, 2);
            //PrintName(name, 1);
        }
    }

    class PrintMsgWrapperEx
    {
        // Unmanaged function
        //void __stdcall PrintMsgByFlag(void* data, int flag)

        //�������йܺ���
        //����һ����������Ϊ��һ�����ĺ���
        [DllImport("NativeLib.dll")]
        static extern void PrintMsgByFlag(ref int id, int flag);
        //����һ�������ַ���Ϊ��һ�����ĺ���
        [DllImport("NativeLib.dll")]
        static extern void PrintMsgByFlag(string name, int flag);

        public static void PrintInfo(int id)
        {
            PrintMsgByFlag(ref id, 1);
        }

        public static void PrintInfo(string name)
        {
            PrintMsgByFlag(name, 2);
        }
    }

}
