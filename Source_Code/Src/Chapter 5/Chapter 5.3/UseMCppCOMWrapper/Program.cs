using System;
using System.Collections.Generic;
using System.Text;
using MCppCOMWrapperLib;

namespace UseMCppCOMWrapper
{
    class Program
    {
        static void Main(string[] args)
        {
            MCppCOMWrapper wrapperObj = new MCppCOMWrapper();
            string inString = "�����ַ���";
            string outString = wrapperObj.ProcessString(inString);

            Console.WriteLine("��ʼʱ�ַ���Ϊ��{0}", inString);
            Console.WriteLine("�޸ĺ��ַ���Ϊ��{0}", outString);

            Console.WriteLine("\r\n��������˳�...");
            Console.Read();	
        }
    }
}
