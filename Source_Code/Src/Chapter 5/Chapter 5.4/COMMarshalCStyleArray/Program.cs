using System;
using System.Collections.Generic;
using System.Text;
using SampleCOMDataType.Interop;
using System.Runtime.InteropServices;

namespace COMMarshalCStyleArray
{
    class Program
    {
        static void Main(string[] args)
        {
            MarshalCStyleArray();

            Console.WriteLine("\r\n��������˳�...");
            Console.Read();	
        }

        private static void MarshalCStyleArray()
        {
            int[] intArray = new int[] { 1, 3, 5, 7, 9 };

            Console.WriteLine("���������е�Ԫ��Ϊ��");
            foreach (int i in intArray)
            {
                Console.WriteLine(i);
            }

            // ����COM�����ʾ��
            MarshalCOMDataTypeClass comObj = new MarshalCOMDataTypeClass();

            // �������������ڷ��й��ڴ�����ռ�ڴ�Ĵ�С
            int bufferSize = Marshal.SizeOf(typeof(int)) * intArray.Length;

            // Ϊ�������������й��ڴ�
            IntPtr pArray = Marshal.AllocCoTaskMem(bufferSize);

            // ������������͵����й��ڴ���
            Marshal.Copy(intArray, 0, pArray, intArray.Length);

            // ����COM��������������Ԫ��֮��
            int sum = comObj.MarshalCStylelArray(pArray, intArray.Length);

            // �ͷŷ��й��ڴ�
            Marshal.FreeCoTaskMem(pArray);

            Console.WriteLine("\n������Ԫ��֮��Ϊ��{0}", sum);

        }
    }
}
