using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace ManagedMultiplyCaller
{
    class Program
    {
        static void Main(string[] args)
        {
            MultiplyManagedCaller.MultiplyTest();
        }
    }


    class MultiplyManagedCaller
    {
        //Unmanaged function
        //int __stdcall Multiply(int factorA, int factorB);

        //�������йܺ���
        [DllImport("NativeLib.dll")]
        static extern int Multiply(int factorA, int factorB);

        public static void MultiplyTest()
        {
            //���÷��йܺ���
            int factorA = 100, factorB = 8;
            int result = Multiply(factorA, factorB);

            //��ӡ���
            Console.WriteLine(string.Format("{0} * {1} = {2} ", factorA, factorB, result));

            Console.WriteLine("\r\n��������˳�...");
            Console.Read();
        }
    }

}
