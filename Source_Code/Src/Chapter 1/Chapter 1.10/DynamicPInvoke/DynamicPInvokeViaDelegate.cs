using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Reflection;

namespace DynamicPInvoke
{
    class DynamicPInvokeViaDelegate
    {
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        delegate int MultiplyDelegate(int factorA, int factorB);

        public static void Test()
        {
            string entryPoint = "_Multiply@8";
            string currentDirectory = 
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string dllPath = Path.Combine(currentDirectory, 
                    @"nativelibfordynamicpinvoke\NativeLibForDynamicPInvoke.dll");
            
            //��̬������Ҫƽ̨���õķ��й�DLL
            IntPtr dllAddr = Win32API.LoadLibrary(dllPath);
            if (dllAddr == IntPtr.Zero)
            {
                throw new DllNotFoundException(string.Format("Can not load {0}, please check.", 
                    dllPath));
            }

            //�����Ҫ���õĺ����ĵ�ַ
            IntPtr procAddr = Win32API.GetProcAddress(dllAddr, entryPoint);
            if (procAddr == IntPtr.Zero)
            {
                throw new EntryPointNotFoundException(
                    string.Format("Can not find entry point \"{0}\" in dll \"{1}\", please check.", 
                    entryPoint, dllPath));
            }

            //ʹ�ô��������ͺ���ָ��
            MultiplyDelegate multiplyDelegate =
                (MultiplyDelegate)Marshal.GetDelegateForFunctionPointer(
                procAddr, typeof(MultiplyDelegate));

            //���÷��йܺ���
            int factorA = 100, factorB = 8;
            int result = multiplyDelegate(factorA, factorB);

            //�ͷż��ص�DLL
            bool isFree = Win32API.FreeLibrary(dllAddr);
            if (isFree)
            {
                Console.WriteLine("Successfully  free {0}.",
                    Path.GetFileName(dllPath));
            }
            else
            {
                throw new Exception(string.Format("Can not free {0}.",
                    Path.GetFileName(dllPath)));
            }

            //��ӡ���
            Console.WriteLine(string.Format("{0} * {1} = {2} ", factorA, factorB, result));
        }

    }
}
