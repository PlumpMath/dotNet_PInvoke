using System;
using System.Collections.Generic;
using System.Text;


namespace DotNetVersionOfWin32
{
    class Program
    {
        static void Main(string[] args)
        {

            //���ļ�����
            string srcFileExist = "srcXMLFile.xml";
            //���ļ������������ļ�����Ϊֻ��
            string destFileReadOnly = "destXMLFileReadOnly.xml";

            //Դ�ļ�������
            string srcFileNotExist = "srcXMLFile1.xml";
            string destFile = "destXMLFile.xml";

            // ���Զ�Win32 API��������ƽ̨���õķ���
            //CopyFileWin32.Test(srcFileExist, destFileReadOnly, true);
            //CopyFileWin32.Test(srcFileNotExist, destFile, true);

            // �����йܷ���
            CopyFileManaged.Test(srcFileExist, destFileReadOnly, true);
            CopyFileManaged.Test(srcFileNotExist, destFile, true);


            // ���Զ�Win32 API��������ƽ̨���õķ���
            //try
            //{
            //    CopyFileWin32.Test(srcFile, destFile, true);
            //}
            //catch (Exception exc)
            //{
            //    Console.WriteLine("Copying file using Win32 P/Invoke, error message: {0}", exc.Message);
            //}

            //// �����йܷ���
            //try
            //{
            //    CopyFileManaged.Test(srcFile, destFile, true);
            //}
            //catch (Exception exc)
            //{
            //    Console.WriteLine("Copying file using managed fuction, error message: {0}", exc.Message);
            //}

            Console.WriteLine("\r\n��������˳�...");
            Console.Read();
        }
    }
}
