using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace FreeUnmanagedMemory
{
    class Program
    {
        //Unmanaged function 
        //wchar_t* GetStringMalloc();
        [DllImport("NativeLib.dll", 
            CallingConvention = CallingConvention.Cdecl, 
            CharSet = CharSet.Unicode
            )]
        static extern string GetStringMalloc();

        //Unmanaged function 
        //wchar_t* GetStringMalloc();
        [DllImport("NativeLib.dll",
            CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Unicode,
           EntryPoint = "GetStringMalloc"
            )]
        static extern IntPtr GetStringMallocViaIntPtr();

        //Unmanaged function 
        //wchar_t* GetStringNew();
        [DllImport("NativeLib.dll", 
            CallingConvention = CallingConvention.Cdecl, 
            CharSet = CharSet.Unicode
            )]
        static extern IntPtr GetStringNew();

        //Unmanaged function 
        //wchar_t* GetStringCoTaskMemAlloc();
        [DllImport("NativeLib.dll", 
            CallingConvention = CallingConvention.Cdecl, 
            CharSet = CharSet.Unicode
            )]
        static extern string GetStringCoTaskMemAlloc();

        //Unmanaged function 
        //wchar_t* GetStringCoTaskMemAlloc();
        [DllImport("NativeLib.dll",
            CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Unicode,
            EntryPoint = "GetStringCoTaskMemAlloc"
            )]
        static extern IntPtr GetStringCoTaskMemAllocViaIntPtr();

        //Unmanaged function 
        //void FreeMallocMemory(void* pbuffer);
        [DllImport("NativeLib.dll", 
            CallingConvention = CallingConvention.Cdecl, 
            CharSet = CharSet.Unicode
            )]
        static extern void FreeMallocMemory(IntPtr pbuffer);

        //Unmanaged function 
        //void FreeMallocMemory(void* pbuffer);
        [DllImport("NativeLib.dll", 
            CallingConvention = CallingConvention.Cdecl, 
            CharSet = CharSet.Unicode
            )]
        static extern void FreeMallocMemory(string pbuffer);

        //Unmanaged function 
        //void FreeNewMemory(void* pbuffer);
        [DllImport("NativeLib.dll", 
            CallingConvention = CallingConvention.Cdecl, 
            CharSet = CharSet.Unicode
            )]
        static extern void FreeNewMemory(IntPtr pbuffer);

        //Unmanaged function 
        //void FreeCoTaskMemAllocMemory(void* pbuffer);
        [DllImport("NativeLib.dll",
            CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Unicode
            )]
        static extern void FreeCoTaskMemAllocMemory(IntPtr pbuffer);        

        static void Main(string[] args)
        {
            //ReturnStringMemoryLeakTest();

            //MemoryReleaseTestViaCoTaskMemAlloc();

            //GetStringMallocViaIntPtrTest();

            //GetStringNewViaIntPtrTest();

            MemoryReleaseTestViaIntPtrCoTaskMemAlloc();

            Console.WriteLine("\r\n��������˳�...");
            Console.Read();
        }

        /// <summary>
        /// ��ָ���ַ�����ָ����ͳ��йܵ�string���ͣ�
        /// �������ڴ�й©
        /// </summary>
        static void ReturnStringMemoryLeakTest()
        {
            string stringFromMalloc = GetStringMalloc();
            //��ͼ�ͷ��Ѿ������ͳ��й����͵�string
            // ��ʧ�ܵ�
            //FreeMallocMemory(stringFromMalloc);
        }

        /// <summary>
        /// �����޸ķ��йܺ������ڴ����
        /// �������ͷŷ��й��ڴ�
        /// </summary>
        static void MemoryReleaseTestViaCoTaskMemAlloc()
        {  
            string stringViaCoTaskMemAlloc = GetStringCoTaskMemAlloc();
        }

        static void GetStringMallocViaIntPtrTest()
        {
            IntPtr stringPtr = GetStringMallocViaIntPtr();
            //���string
            string stringFromMalloc = Marshal.PtrToStringUni(stringPtr);

            //���÷��йܺ������ͷŵ����й��ڴ�
            FreeMallocMemory(stringPtr);
        }

        static void GetStringNewViaIntPtrTest()
        {
            IntPtr stringPtr = GetStringNew();
            //���string
            string stringFromNew = Marshal.PtrToStringUni(stringPtr);

            //���÷��йܺ������ͷŵ����й��ڴ�
            FreeNewMemory(stringPtr);
        }

        static void MemoryReleaseTestViaIntPtrCoTaskMemAlloc()
        {
            IntPtr coTaskMemAllocIntPtr = GetStringCoTaskMemAllocViaIntPtr();
            //���string
            string stringFromCoTaskMemAlloc = Marshal.PtrToStringUni(coTaskMemAllocIntPtr);

            //�����ܹ��ͷ���CoTaskMemAlloc������ڴ�
            //�ķ��йܺ������ͷŵ����й��ڴ�
            FreeCoTaskMemAllocMemory(coTaskMemAllocIntPtr);

            //����ֱ�ӵ���Marshal.FreeCoTaskMem�ͷŵ����й��ڴ�
            //Marshal.FreeCoTaskMem(coTaskMemAllocIntPtr);
        }

    }
}

