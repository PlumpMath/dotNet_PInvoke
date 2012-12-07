using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Handles
{
    class SafeHandleSample
    {
        // ����ֻ��Ϊ�˽�����ʾ�����Ծ�û�в���
        // ������ö���ж��峣��������
        const uint FILE_SHARE_READ = 0x00000001;
        const uint OPEN_EXISTING = 3;

        // Win32 API
        // DWORD GetFileSize(HANDLE hFile, LPDWORD lpFileSizeHigh);
        [DllImport("Kernel32.dll", EntryPoint = "GetFileSize")]
        static extern int GetFileSizeSafe(SafeFileHandle fileHandle, IntPtr fileSizeHigh);

        // Win32 API
        //  HANDLE CreateFile(
        //      LPCTSTR lpFileName,
        //      DWORD dwDesiredAccess,
        //      DWORD dwShareMode,
        //      LPSECURITY_ATTRIBUTES lpSecurityAttributes,
        //      DWORD dwCreationDisposition,
        //      DWORD dwFlagsAndAttributes,
        //      HANDLE hTemplateFile
        //  ); 

        [DllImport("Kernel32.dll", SetLastError = true, EntryPoint = "CreateFile")]
        static extern SafeFileHandle CreateFileSafe(
            string fileName,
            uint desiredAccess,
            uint shareMode,
            IntPtr securityAttributes,
            uint creationDisposition,
            uint flagsAndAttributes,
            IntPtr templateFile);

        public void Test()
        {
            // ���������ٵ���CloseHandle()���ͷŵ��ļ������
            // ������ΪSafeFileHandle�Ѿ�ʵ����IDisposable�ӿڣ�
            // ��˾���ᱻCLR�ͷŻ��ڵ���Dispose()ʱ���ͷ�
            using (SafeFileHandle fileHandle = OpenFile("TestFile.txt"))
            {
                int fileSize = GetFileSizeSafe(fileHandle, IntPtr.Zero);
                Console.WriteLine("GetFileSizeSafe returns: {0}", fileSize);
            }
        }

        /// <summary>
        /// ��һ���ļ��Ա���ȡ
        /// </summary>
        /// <param name="fileName">�ļ���</param>
        /// <returns></returns>
        private SafeFileHandle OpenFile(string fileName)
        {
            SafeFileHandle fileHandle = CreateFileSafe(
                           fileName,
                           0,
                           FILE_SHARE_READ,
                           IntPtr.Zero,
                           OPEN_EXISTING,
                           0,
                           IntPtr.Zero);

            if (fileHandle.IsInvalid)
            {
                int lastErrorCode = Marshal.GetLastWin32Error();
                throw new System.ComponentModel.Win32Exception(lastErrorCode);
            }
            else 
            {
                return fileHandle;
            }
        }

    }

    /// <summary>
    /// ʹ��Win32 Handles�Ĳ���ȫ�ķ�ʽ
    /// </summary>
    class UnsafeHandleSample
    {
        // ����ֻ��Ϊ�˽�����ʾ�����Ծ�û�в���
        // ������ö���ж��峣��������
        const uint FILE_SHARE_READ = 0x00000001;
        const uint OPEN_EXISTING = 3;

        // Win32 API
        // DWORD GetFileSize(HANDLE hFile, LPDWORD lpFileSizeHigh);
        [DllImport("Kernel32.dll", EntryPoint = "GetFileSize")]
        static extern int GetFileSizeUnsafe(IntPtr fileHandle, IntPtr fileSizeHigh);

        // Win32 API
        //  HANDLE CreateFile(
        //      LPCTSTR lpFileName,
        //      DWORD dwDesiredAccess,
        //      DWORD dwShareMode,
        //      LPSECURITY_ATTRIBUTES lpSecurityAttributes,
        //      DWORD dwCreationDisposition,
        //      DWORD dwFlagsAndAttributes,
        //      HANDLE hTemplateFile
        //  ); 

        [DllImport("Kernel32.dll", SetLastError = true, EntryPoint = "CreateFile")]
        static extern IntPtr CreateFileUnsafe(
            string fileName,
            uint desiredAccess,
            uint shareMode,
            IntPtr securityAttributes,
            uint creationDisposition,
            uint flagsAndAttributes,
            IntPtr templateFile);

        // Win32 API
        //  BOOL CloseHandle(
        //      HANDLE hObject
        //  ); 
        [DllImport("Kernel32.dll", SetLastError = true)]
        static extern bool CloseHandle(IntPtr fileHandle);

        public void Test()
        {
            IntPtr fileHandle = CreateFileUnsafe(
                "TestFile.txt",
                0,
                FILE_SHARE_READ,
                IntPtr.Zero,
                OPEN_EXISTING,
                0,
                IntPtr.Zero);

            if (fileHandle == IntPtr.Zero)
            {
                int lastErrorCode = Marshal.GetLastWin32Error();
                throw new System.ComponentModel.Win32Exception(lastErrorCode);
            }
            int fileSize = GetFileSizeUnsafe(fileHandle, IntPtr.Zero);
            Console.WriteLine("GetFileSizeUnsafe returns: {0}", fileSize);

            // ������﷢�����쳣��������������д�Ĵ���
            // ������δ֪������׳����쳣����ô���޷���
            // ֤������ر���

            // ���������ϣ����ʹ��try/finally�������֤�رվ����
            // Ҳ�ǲ��ܱ�֤��һ���ܹرվ���ģ�����������һ
            // ��AppDomain��ж�����⣬�Ͳ�һ���ܱ�֤��

            // ����CloseHandle�ͷŵ��������ֹ����й©����
            bool ret = CloseHandle(fileHandle);
        }

    }
}
