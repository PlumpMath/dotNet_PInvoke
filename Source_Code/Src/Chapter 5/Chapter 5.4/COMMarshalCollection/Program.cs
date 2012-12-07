using System;
using System.Collections.Generic;
using System.Text;
using SampleCOMCollection.Interop;

namespace COMMarshalCollection
{
    class Program
    {
        static void Main(string[] args)
        {
            MarshalCOMCollection();

            Console.WriteLine("\r\n��������˳�...");
            Console.Read();	
        }

        private static void MarshalCOMCollection()
        {
            MarshalCOMCollectionClass comObj = new MarshalCOMCollectionClass();
            Console.WriteLine("��ʼͼ������{0}", comObj.Count);
            Console.WriteLine("ͼ���б�");
            foreach (string dinosaur in comObj)
            {
                Console.WriteLine("\t{0}", dinosaur);
            }

            string addedElement = "����ͨ.NET�������ԣ�P/Invoke��C++ Interop��COM Interop��";
            Console.WriteLine("\n�����ͼ�飺{0}", addedElement);
            comObj.AddElement(addedElement);

            Console.WriteLine("\n��Ӻ�ͼ������{0}", comObj.Count);
            Console.WriteLine("ͼ���б�");
            foreach (string dinosaur in comObj)
            {
                Console.WriteLine("\t{0}", dinosaur);
            }
        }
    }
}
