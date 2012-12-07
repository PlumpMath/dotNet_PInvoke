using System;
using System.Collections.Generic;
using System.Text;
using CppInteropWrapper;

namespace CSharpClient
{
    class Program
    {
        static void Main(string[] args)
        {

            int resultNumber = 0;

            // ����Managed C++����ʵ��
	        WrapperClass wrapperObj = new WrapperClass();
        	
            // ���ó˷�������P/Invoke)
	        resultNumber = wrapperObj.MultiplyNumbers_PInvoke(2, 3);
            Console.WriteLine("�˷���P/Invoke����2x3 = {0}", resultNumber);

	        // ���ó˷�������C++ Interop)
	        resultNumber = wrapperObj.MultiplyNumbers_CppInterop(4, 5);
            Console.WriteLine("�˷���C++ Interop����4x5 = {0}", resultNumber);

	        // ��ת�ַ�������(P/Invoke)
	        wrapperObj.ReverseString_PInvoke();

	        // ��ת�ַ�������(C++ Interop)
	        wrapperObj.ReverseString_CppInterop();

            Console.WriteLine("\r\n��������˳�...");
            Console.Read();	
        }
    }
}
