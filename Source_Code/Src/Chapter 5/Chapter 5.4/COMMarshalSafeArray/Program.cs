using System;
using System.Collections.Generic;
using System.Text;
using SampleCOMDataType.Interop;

namespace COMMarshalSafeArray
{
    class Program
    {
        static void Main(string[] args)
        {
            MarshalCOMSafeArray();

            Console.WriteLine("\r\n��������˳�...");
            Console.Read();	
        }

        private static void MarshalCOMSafeArray()
        {
            MarshalCOMDataTypeClass comObj = new MarshalCOMDataTypeClass();
            string[] stringArray = new string[] { "This", "is", "a", "SAFEARRAY", "sample" };

            Console.WriteLine("��ʼ�ַ������飺");
            foreach (string str in stringArray)
            {
                Console.WriteLine(str);
            }

            string result = comObj.SortArray(stringArray);
            Console.WriteLine("\n���÷��ؽ����{0}", result);
            Console.WriteLine("\n�������ַ������飺");
            foreach (string str in stringArray)
            {
                Console.WriteLine(str);
            }
        }
    }
}
