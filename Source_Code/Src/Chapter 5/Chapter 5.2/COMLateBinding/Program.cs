using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace COMLateBinding
{
    class Program
    {
        static void Main(string[] Args)
        {
            // ͨ��ProgID����COM��������
            Type comType = Type.GetTypeFromProgID(
                "SampleCOMSimple.SimpleCOMCalculator.1");

            // Ҳ����ͨ��CLSID����COM��������
            //Guid clsid = new Guid("C7386CE7-47B0-43C4-82D4-5FFA7A359EEA");
            //Type comType = Type.GetTypeFromCLSID(clsid);

            // ����COM����ʵ��
            Object comObj = Activator.CreateInstance(comType);

            // ���÷�������
            Object[] methodArgs = { 6, 4 };

            // ���üӷ�
            Object result = comType.InvokeMember(
                "Add", BindingFlags.InvokeMethod, null,
                comObj, methodArgs);

            Console.WriteLine("���㣺{0} + {1} = {2}",
                methodArgs[0], methodArgs[1], result);

            // ���ü���
            result = comType.InvokeMember(
                "Subtract", BindingFlags.InvokeMethod, null,
                comObj, methodArgs);

            Console.WriteLine("���㣺{0} - {1} = {2}",
                methodArgs[0], methodArgs[1], result);

            // ���ó˷�
            result = comType.InvokeMember(
                "Multiply", BindingFlags.InvokeMethod, null,
                comObj, methodArgs);

            Console.WriteLine("���㣺{0} * {1} = {2}",
                methodArgs[0], methodArgs[1], result);

            // ���ó���
            result = comType.InvokeMember(
                "Divide", BindingFlags.InvokeMethod, null,
                comObj, methodArgs);

            Console.WriteLine("���㣺{0} / {1} = {2}",
                methodArgs[0], methodArgs[1], result);

            // ���ý׳˷���
            // ���ڲ�����Ҫ�ӷ����з��ؽ����Ҫʹ��ParameterModifier
            ParameterModifier paramMod = new ParameterModifier(1);
            paramMod[0] = true;

            int factorialArg = 4;
            Object[] methodArgs1 = { factorialArg };
            result = comType.InvokeMember(
                "Factorial", BindingFlags.InvokeMethod, null,
                comObj, methodArgs1, new ParameterModifier[] { paramMod },
                null, null);
            Console.WriteLine("���㣺!{0} = {1}",
                            factorialArg, methodArgs1[0]);

            Console.WriteLine("\r\n��������˳�...");
            Console.Read();	
        }
    }
}
