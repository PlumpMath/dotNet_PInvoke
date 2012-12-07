using System;
using System.Collections.Generic;
using System.Text;
using SampleCOMDataType.Interop;

namespace COMMarshalStructure
{
    class Program
    {
        static void Main(string[] args)
        {
            MarshalCOMStructure();

            Console.WriteLine("\r\n��������˳�...");
            Console.Read();	
        }

        private static void MarshalCOMStructure()
        {
            MarshalCOMDataTypeClass comObj = new MarshalCOMDataTypeClass();

            SampleStruct sampleStruct = new SampleStruct();
            sampleStruct.ID = 1;
            sampleStruct.stringName = "TestString";

            Console.WriteLine("��ʼ�ṹ�����ݣ�\n\tSampleStruct.ID = {0}\n\tSampleStruct.stringName = {1}",
                sampleStruct.ID, sampleStruct.stringName);

            comObj.MarshalStructure(ref sampleStruct);

            Console.WriteLine("���º�ṹ�����ݣ�\n\tSampleStruct.ID = {0}\n\tSampleStruct.stringName = {1}",
                sampleStruct.ID, sampleStruct.stringName);
        }
    }
}
