using System;
using System.Collections.Generic;
using System.Text;
using SampleCOMDataType.Interop;
using System.Runtime.InteropServices;

namespace COMMarshalData
{
    class Program
    {
        static void Main(string[] args)
        {
            MarshalCommonDataType();

            Console.WriteLine("\r\n��������˳�...");
            Console.Read();	
        }

        private static void MarshalCommonDataType()
        {
            MarshalCOMDataTypeClass comObj = new MarshalCOMDataTypeClass();

            // �����ַ�
            byte outChar = (byte)0;
            comObj.MarshalChar((byte)'a', ref outChar);
            Console.WriteLine("MarshalChar: {0}", (char)outChar);

            sbyte outComChar = (sbyte)0;
            comObj.MarshalCOMChar((sbyte)'b', ref outComChar);
            Console.WriteLine("MarshalComChar: {0}", (char)outComChar);

            // �����ַ���
            string outBSTR = String.Empty;
            comObj.MarshalBSTR("BSTR�ַ���", ref outBSTR);
            Console.WriteLine("MarshalBSTR: {0}", outBSTR);

            string outLPSTR = String.Empty;
            comObj.MarshalLPSTR("LPSTR�ַ���", ref outLPSTR);
            Console.WriteLine("MarshalLPSTR: {0}", outLPSTR);

            // ����Decimal��Currency
            decimal outDecimal = 0;
            comObj.MarshalDECIMAL(2008.88m, ref outDecimal);
            Console.WriteLine("MarshalDecimal: {0}", outDecimal);

            decimal outCurrency = 0;
            comObj.MarshalCURRENCY(2008.88m, ref outCurrency);
            Console.WriteLine("MarshalCurrency: {0:C}", outCurrency);
            
            // ���Ͳ���ֵ
            sbyte outBool = 0;
            comObj.MarshalBoolean(1, ref outBool);
            Console.WriteLine("UseBoolean: {0}", outBool);

            bool outVariantBool = false;
            comObj.MarshalVariantBool(true, ref outVariantBool);
            Console.WriteLine("UseVariantBool: {0}", outVariantBool);

            // ����DATE
            DateTime outDate = new DateTime();
            comObj.MarshalDATE(new DateTime(2008, 8, 8, 20, 0, 0), ref outDate);
            Console.WriteLine("UseDate: {0}", outDate);
        }
    }
}
