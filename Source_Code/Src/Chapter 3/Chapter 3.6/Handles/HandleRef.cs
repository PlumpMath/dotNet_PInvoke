using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;

namespace Handles
{
    /// <summary>
    /// ��HandleRef �����İ�װ
    /// </summary>
    class HandleRefSample
    {
        // Win32 API
        // DWORD GetFileSize(HANDLE hFile, LPDWORD lpFileSizeHigh);
        [DllImport("Kernel32.dll")]
        static extern int GetFileSize(HandleRef fileHandle, IntPtr fileSizeHigh);

        public void Test()
        {
            // ��ѵ������ǲ���FileStream����������
            // Ϊ����ʾ����HandleRef�ķ������ͷ���
            // ���汻ע�͵���ʹ��FileStream�ķ���

            //using (FileStream fs = new FileStream("TestFile.txt", FileMode.Open))
            //{
            //    HandleRef handleRef = new HandleRef(fs, fs.SafeFileHandle.DangerousGetHandle());
            //    int fileSize = GetFileSize(handleRef, IntPtr.Zero);
            //    Console.WriteLine("GetFileSize returns: {0}", fileSize);
            //}

            // ������ֻ��Ϊ����ʾ����ѵ�����Ӧ��
            // �����汻ע�͵��ķ�������ȥ��
            FileStream fs = new FileStream("TestFile.txt", FileMode.Open);
            HandleRef handleRef = new HandleRef(fs, fs.Handle);
            int fileSize = GetFileSize(handleRef, IntPtr.Zero);
            Console.WriteLine("GetFileSize returns: {0}", fileSize);
        }
    }
}
