using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using SampleCOMSimple.Interop;

namespace ReleaseCOMObject
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            ReleaseCOMObject();

            Console.WriteLine("\r\n��������˳�...");
            Console.Read();	
        }

        private static void ReleaseCOMObject()
        {
            SimpleCOMCalculatorClass comObj = new SimpleCOMCalculatorClass();
            int integer = 5;
            Console.Write("!{0} = ", integer);
            try
            {
                comObj.Factorial(ref integer);
                // ��ʾCOM�������ý��
                Console.WriteLine("{0}", integer);
            }
            finally
            {
                // ��ʽ�ͷ�COM����
                Console.WriteLine("����Marshal.ReleaseComObject()�ͷ�COM����...");
                int referenceCount = Marshal.ReleaseComObject(comObj);
                Console.WriteLine("COM����Ŀǰ�����ü���Ϊ��{0}", referenceCount);
            }
            
            Console.WriteLine("�ٴε���COM����ķ���Add()��");
            try
            {
                int result = comObj.Add(3, 5);
                Console.WriteLine("3 + 5 = {0}", result);
            }
            catch (Exception e)
            {
                Console.WriteLine("�쳣�׳���{0}\n��Ϣ��{1}", e.GetType().Name, e.Message);
            }
        }
    }
}
