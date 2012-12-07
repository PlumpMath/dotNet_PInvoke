using System;
using System.Collections.Generic;
using System.Text;
using SampleCOMErrorInfo.Interop;
using System.Runtime.InteropServices;

namespace COMMarshalErrorInfo
{
    class Program
    {
        static void Main(string[] args)
        {
            MarshalCOMErrorInfo();

            Console.WriteLine("\r\n��������˳�...");
            Console.Read();	
        }

        private static void MarshalCOMErrorInfo()
        {
            MarshalErrorInfoClass comObj = new MarshalErrorInfoClass();

            for (int i = 0; i < 8; i++)
            {
                try
                {
                    comObj.GenerateCOMError(i);
                    Console.WriteLine("\n��{0}�ε���û���쳣��", i);
                }
                catch (Exception e)
                {
                    int hResult = Marshal.GetHRForException(e);
                    Console.WriteLine("\n��{0}�ε����׳��쳣�����ͣ�{1}��HRESULT = 0x{2:X}��\n��Ϣ��{3}��\n���룺{4}",
                        i, e.GetType().Name, hResult, e.Message, e.Source);
                }
            }
        }
    }
}
