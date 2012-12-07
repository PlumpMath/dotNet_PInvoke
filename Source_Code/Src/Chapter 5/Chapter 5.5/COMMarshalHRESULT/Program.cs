using System;
using System.Collections.Generic;
using System.Text;
using SampleCOMErrorResult.Interop;
using System.Runtime.InteropServices;

namespace COMMarshalHRESULT
{
    class Program
    {
        static void Main(string[] args)
        {
            MarshalCOMHRESULT();

            Console.WriteLine();

            MarshalCOMReturnHRESULT();

            Console.WriteLine("\r\n��������˳�...");
            Console.Read();	
        }

        private static void MarshalCOMHRESULT()
        {
            MarshalCOMHRESULTClass comObj = new MarshalCOMHRESULTClass();

            for (int i = 0; i < 8; i++)
            {
                try
                {
                    comObj.GenerateHRESULTs(i);
                    Console.WriteLine("\n��{0}�ε���û���쳣��", i);
                }
                catch (Exception e)
                {
                    int hResult = Marshal.GetHRForException(e);
                    Console.WriteLine("\n��{0}�ε����׳��쳣�����ͣ�{1}��HRESULT = 0x{2:X}",
                        i, e.GetType().Name, hResult);
                }
            }
        }

        private static void MarshalCOMReturnHRESULT()
        {
            MarshalCOMHRESULTClass comObj = new MarshalCOMHRESULTClass();

            for (int i = 0; i < 8; i++)
            {
                int hResult;
                try
                {
                    hResult = comObj.ReturnHRESULTs(i);
                    Console.WriteLine("\n��{0}�ε���û���쳣��HRESULT = 0x{1:X}", i, hResult);
                }
                catch (Exception e)
                {
                    hResult = Marshal.GetHRForException(e);
                    Console.WriteLine("\n��{0}�ε����׳��쳣�����ͣ�{1}��HRESULT = 0x{2:X}",
                        i, e.GetType().Name, hResult);
                }
            }
        }

    }
}
