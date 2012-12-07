using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;

namespace DynamicPInvoke
{
    class DynamicPInvokeViaEmit
    {
        public static void Test()
        {
            string currentDirectory = 
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string dllPath = Path.Combine(currentDirectory, 
                @"nativelibfordynamicpinvoke\NativeLibForDynamicPInvoke.dll");

            //��������ͷ���ֵ������
            Type[] parameterTypes = new Type[] { typeof(int), typeof(int) };
            int factorA = 100, factorB = 8;
            Type returnType = typeof(int);
            
            //���ò�����ֵ
            object[] parameterValues = { factorA, factorB };

            // ���ú�����ڵ㣬�����Ǿ�������������ĺ�����
            // ԭ�����ڷ��й�DLLʹ�õĵ���Լ����__stdcall
            // ���ʹ��Multiply�������׳�һ�����µ��쳣
            //Unhandled Exception: System.Reflection.TargetInvocationException: Exception has
            //been thrown by the target of an invocation. ---> System.EntryPointNotFoundException: 
            //Unable to find an entry point named 'Multiply' in DLL 'D:\nativelibfordynamicpinvoke\
            //NativeLibForDynamicPInvoke.dll'.
            string entryPoint = "_Multiply@8";

            //����DllImport���ֶ�
            CallingConvention nativeCallConv = CallingConvention.StdCall;
            CharSet nativeCharSet = CharSet.Ansi;

            //���÷��������η���������Ҫ����static��extern
            MethodAttributes methodAttr = 
                MethodAttributes.Static
                | MethodAttributes.Public
                | MethodAttributes.PinvokeImpl;

            //���ж�̬ƽ̨����
            object result = DynamicDllFunctionInvoke(
                dllPath, 
                entryPoint, 
                methodAttr, 
                nativeCallConv,
                nativeCharSet, 
                returnType, 
                parameterTypes, 
                parameterValues
                );

            //��ӡ���
            Console.WriteLine(
                string.Format("{0} * {1} = {2} ", 
                factorA, 
                factorB, 
                result)
                );
        }

        public static object DynamicDllFunctionInvoke(
            string dllPath,
            string entryPoint,
            MethodAttributes methodAttr,
            CallingConvention nativeCallConv,
            CharSet nativeCharSet,
            Type returnType,
            Type[] parameterTypes,
            object[] parameterValues
            )
        {
            string dllName = Path.GetFileNameWithoutExtension(dllPath);
            // ����һ����̬����(assembly)��ģ��(module)
            AssemblyName assemblyName = new AssemblyName();
            assemblyName.Name = string.Format("A{0}{1}", 
                dllName, 
                Guid.NewGuid().ToString( "N" )
                );
            AssemblyBuilder dynamicAssembly =
              AppDomain.CurrentDomain.DefineDynamicAssembly(
              assemblyName, AssemblyBuilderAccess.Run);
            ModuleBuilder dynamicModule =
              dynamicAssembly.DefineDynamicModule(
              string.Format("M{0}{1}", 
              dllName, 
              Guid.NewGuid().ToString("N"))
              );

            // ʹ��ָ������Ϣ����ƽ̨����ǩ��
            MethodBuilder dynamicMethod = 
                dynamicModule.DefinePInvokeMethod(
                entryPoint,
                dllPath,
                methodAttr,
                CallingConventions.Standard,
                returnType,
                parameterTypes,
                nativeCallConv,
                nativeCharSet
                );

            // ��������
            dynamicModule.CreateGlobalFunctions();

            // ���ƽ̨���õķ���
            MethodInfo methodInfo = 
                dynamicModule.GetMethod(entryPoint, parameterTypes);
            // ���÷��йܺ�������÷��صĽ��
            object result = methodInfo.Invoke(null, parameterValues);
            return result;
        }

    }
}


 class DynamicPInvokeViaEmit
    {
        public static void Test()
        {
            string currentDirectory = 
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string dllPath = Path.Combine(currentDirectory, 
                @"nativelibfordynamicpinvoke\NativeLibForDynamicPInvoke.dll");

            //��������ͷ���ֵ������
            Type[] parameterTypes = new Type[] { typeof(int), typeof(int) };
            int factorA = 100, factorB = 8;
            Type returnType = typeof(int);
            
            //���ò�����ֵ
            object[] parameterValues = { factorA, factorB };

            // ���ú�����ڵ㣬�����Ǿ�������������ĺ�����
            // ԭ�����ڷ��й�DLLʹ�õĵ���Լ����__stdcall
            // ���ʹ��Multiply�������׳�һ�����µ��쳣
            //Unhandled Exception: System.Reflection.TargetInvocationException: Exception has
            //been thrown by the target of an invocation. ---> System.EntryPointNotFoundException: 
            //Unable to find an entry point named 'Multiply' in DLL 'D:\nativelibfordynamicpinvoke\
            //NativeLibForDynamicPInvoke.dll'.
            string entryPoint = "_Multiply@8";

            //����DllImport���ֶ�
            CallingConvention nativeCallConv = CallingConvention.StdCall;
            CharSet nativeCharSet = CharSet.Ansi;

            //���÷��������η���������Ҫ����static��extern
            MethodAttributes methodAttr = 
                MethodAttributes.Static
                | MethodAttributes.Public
                | MethodAttributes.PinvokeImpl;

            //���ж�̬ƽ̨����
            object result = DynamicDllFunctionInvoke(
                dllPath, 
                entryPoint, 
                methodAttr, 
                nativeCallConv,
                nativeCharSet, 
                returnType, 
                parameterTypes, 
                parameterValues
                );

            //��ӡ���
            Console.WriteLine(
                string.Format("{0} * {1} = {2} ", 
                factorA, 
                factorB, 
                result)
                );
        }

        public static object DynamicDllFunctionInvoke(
            string dllPath,
            string entryPoint,
            MethodAttributes methodAttr,
            CallingConvention nativeCallConv,
            CharSet nativeCharSet,
            Type returnType,
            Type[] parameterTypes,
            object[] parameterValues
            )
        {
            string dllName = Path.GetFileNameWithoutExtension(dllPath);
            // ����һ����̬����(assembly)��ģ��(module)
            AssemblyName assemblyName = new AssemblyName();
            assemblyName.Name = string.Format("A{0}{1}", 
                dllName, 
                Guid.NewGuid().ToString( "N" )
                );
            AssemblyBuilder dynamicAssembly =
              AppDomain.CurrentDomain.DefineDynamicAssembly(
              assemblyName, AssemblyBuilderAccess.Run);
            ModuleBuilder dynamicModule =
              dynamicAssembly.DefineDynamicModule(
              string.Format("M{0}{1}", 
              dllName, 
              Guid.NewGuid().ToString("N"))
              );

            // ʹ��ָ������Ϣ����ƽ̨����ǩ��
            MethodBuilder dynamicMethod = 
                dynamicModule.DefinePInvokeMethod(
                entryPoint,
                dllPath,
                methodAttr,
                CallingConventions.Standard,
                returnType,
                parameterTypes,
                nativeCallConv,
                nativeCharSet
                );

            // ��������
            dynamicModule.CreateGlobalFunctions();

            // ���ƽ̨���õķ���
            MethodInfo methodInfo = 
                dynamicModule.GetMethod(entryPoint, parameterTypes);
            // ���÷��йܺ�������÷��صĽ��
            object result = methodInfo.Invoke(null, parameterValues);
            return result;
        }

    }