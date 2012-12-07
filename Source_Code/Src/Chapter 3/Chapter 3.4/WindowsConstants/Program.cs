using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace WindowsConstants
{
    class Program
    {
        static void Main(string[] args)
        {

            // ʵ�ʲ���ʱ��Ӧ���޸ĳ���Ҫ��õ��ļ���·����
            // ������ʾ���ļ���ֻ���һ����ϴ��ڵ��ļ�
            string filePath = Path.Combine(Environment.CurrentDirectory, "Sample_Chapter_Bak.docx");

            string fileTypeName = ManagedSHGetFileInfoWrapper.GetFileTypeName(filePath);
            Console.WriteLine(fileTypeName);

            System.Drawing.Icon iconForMSWord = ManagedSHGetFileInfoWrapper.GetFileIcon(filePath, false, IconSize.Small);
            Console.WriteLine("Icon file handle:" + iconForMSWord.Handle);

            // More about how to save icons from C#, can be found at:
            //Icons in Win32
            //http://msdn.microsoft.com/library/en-us/dnwui/html/msdn_icons.asp 
            // How to save Icon using C#
            //http://www.kennyandkarin.com/Kenny/CodeCorner/Tools/IconBrowser/

            Console.Write("����վ�����ƣ�");
            ManagedSHGetFileInfoTest.FetchProcDisplayName();

            Console.WriteLine("\r\n��������˳�...");
            Console.Read();
        }

        

    }
}

