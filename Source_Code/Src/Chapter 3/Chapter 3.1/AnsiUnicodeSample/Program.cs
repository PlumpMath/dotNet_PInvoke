using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace AnsiUnicodeSample
{
    class Program
    {
        static void Main(string[] args)
        {

            /****************Ansi 6 demos******************/
            // ʹ��Ĭ��CharSet��ʾ�����ܹ���ȷ����
            DefaultCharSetDemo.Test();

            // ��ʽ��ָ��CharSetΪCharSet.Ansi��ʾ�����ܹ���ȷ����
            CharSetAsAnsiDemo.Test();

            // ʹ��Ĭ��CharSet����ָ��EntryPointΪGetUserNameA
            // ��ʾ��, �ܹ���ȷ����
            DefaultCharSetAndEntryPointSpecifiedDemo.Test();

            //��ʽ��ָ��CharSetΪCharSet.Ansi����ָ��
            // EntryPointΪGetUserNameA��ʾ�����ܹ���ȷ����
            CharSetAsAnsiAndEntryPointSpecifiedDemo.Test();

            // ָ��EntryPointΪGetUserNameA����ȴָ��CharSet
            // ΪCharSet.Unicode������������Ľ��
            CharSetAndEntryPointMismatchDemo.Test();

            // ָ��EntryPointΪGetUserNameA����ȴָ��CharSet
            // ΪCharSet.Auto����Windows XP�����У����������Ľ��            
            CharSetAsAutoAndEntryPointDemo.Test();


            /****************Unicode 5 demos******************/
            // ��ʽ��ָ��CharSetΪCharSet.Unicode��ʾ�����ܹ���ȷ����
            CharSetAsUnicodeDemo.Test();

            // ��ʽ��ָ��CharSetΪCharSet.Unicode����EntryPointΪGetUserNameW���ܹ���ȷ����
            CharSetAsUnicodeAndEntryPointMatchedDemo.Test();

            // ָ��EntryPointΪGetUserNameW����ȴָ��CharSet
            // ΪCharSet.Ansi������������Ľ��
            CharSetAsUnicodeAndEntryPointMismatchDemo.Test();

            // ָ��EntryPointΪGetUserNameW��ͬʱָ��CharSet
            // ΪCharSet.Auto����Windows XP�£��ܹ���ȷ����
            CharSetAsAutoAndEntryPointSpecifiedDemo.Test();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\r\n���²��Է���������ʾ�׳��쳣�����");
            Console.ResetColor();

            // ����ExactSpellingΪtrue��ͬʱָ��CharSet
            // ΪCharSet.Unicode�����׳��쳣
            CharSetAsUnicodeButWithExactSpellingTrueDemo.Test();

            // ָ��CharSetΪCharSet.Unicode������ȴ�޸��˺�
            // �����ƣ����׳��쳣
            CharSetAsUnicodeButWithModifiedRootFunctionNameDemo.Test();


            Console.WriteLine("\r\n��������˳�...");
            Console.Read();	

        }
        


    }
}
