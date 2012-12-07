using System;
using System.Collections.Generic;
using System.Text;
using SampleCOMEvent.Interop;

namespace COMMarshalEvent
{
    class Program
    {
        static void Main(string[] args)
        {
            MarshalCOMEvent();

            Console.WriteLine("\r\n��������˳�...");
            Console.Read();	
        }

        private static void MarshalCOMEvent()
        {
            MarshalCOMEventClass comObj = new MarshalCOMEventClass();
            comObj.OnStringChanged += new _IMarshalCOMEventEvents_OnStringChangedEventHandler(comObj_OnStringChanged);
            comObj.ChangeStringValue("ԭʼ�ַ���");
        }

        static void comObj_OnStringChanged(string originalString, string changedString)
        {
            Console.WriteLine("COM event fired.");
            Console.WriteLine("The original string = {0}", originalString);
            Console.WriteLine("The changed string = {0}", changedString);
        }
    }
}
