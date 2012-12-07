using System;
using System.Collections.Generic;
using System.Text;
using SampleCOMSimple.Interop;
using System.Threading;

namespace COMThreadApartment
{
    class Program
    {
        //[MTAThread]
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("��ʼApartmentΪ: {0}",
                Thread.CurrentThread.GetApartmentState());

            int result = 0;

            // ����STA COM����
            SimpleCOMCalculatorClass staObj
                = new SimpleCOMCalculatorClass();
            result = staObj.Add(3, 5);
            Console.WriteLine("STA���󷽷���3 + 5 = {0}, Threading: {1}",
                result, Thread.CurrentThread.GetApartmentState());

            // ����MTA COM����
            COMMTAObjClass mtaObj
                = new COMMTAObjClass();
            result = mtaObj.AddIntegers(3, 5);
            Console.WriteLine("MTA���󷽷�: 3 + 5 = {0}, Threading: {1}",
                result, Thread.CurrentThread.GetApartmentState());

            // ʹ��SetApartmentState����Apartment
            try
            {
                Thread.CurrentThread.SetApartmentState(
                    ApartmentState.MTA);
                Console.WriteLine(
                    "����SetApartmentState����: {0}", ApartmentState.MTA);
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(
                    "SetApartmentState�׳��쳣��Ϣ: {0}",
                    e.Message);
            }

            // ʹ��TrySetApartmentState����Apartment
            try
            {
                bool changeStatus =
                    Thread.CurrentThread.TrySetApartmentState(
                        ApartmentState.MTA);
                Console.WriteLine(
                    "TrySetApartmentState���ؽ��: ����{0}",
                    changeStatus ? "�ɹ�" : "ʧ��");
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(
                    "TrySetApartmentStates�׳��쳣��Ϣ: {0}",
                    e.Message);
            }

            Console.WriteLine("\r\n��������˳�...");
            Console.Read();	
        }
    }
}
