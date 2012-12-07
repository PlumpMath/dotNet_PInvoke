using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;

namespace PassManagedObject
{
    class ManagedObjectInCallback
    {
        delegate bool EnumWindowProc(int windowHandle, IntPtr param);

        // ���йܶ�����ΪLPARAM��������
        // Win32 API
        // BOOL EnumDesktopWindows(
        //  __in          HDESK hDesktop,
        //  __in          WNDENUMPROC lpfn,
        //  __in          LPARAM lParam);
        [DllImport("user32.dll")]
        static extern bool EnumDesktopWindows(
            IntPtr desktopHandle, 
            EnumWindowProc cb, 
            IntPtr param);

        // Win32 API
        // int GetWindowText(HWND hWnd, LPTSTR lpString, int nMaxCount);
        [DllImport("user32.dll")]
        static extern int GetWindowText(
            int windowHandle, 
            StringBuilder textBuffer, 
            int bufferLength);

        /// <summary>
        /// ����ص�����������ʾ����ı���
        /// </summary>
        /// <param name="handle">����ľ��</param>
        /// <param name="param">����</param>
        /// <returns></returns>
        private static bool DisplayWindowsTitleProc(int windowHandle, IntPtr param)
        {
            GCHandle gch = (GCHandle)param;
            TextWriter tw = gch.Target as TextWriter;
            if (null != tw)
            {
                StringBuilder buffer = new StringBuilder(1024);
                int size = GetWindowText(windowHandle, buffer, buffer.Capacity);
                if (size != 0)
                {
                    tw.WriteLine("Window Handle: 0x{0:x8}  Title: {1}", windowHandle, buffer);
                }
                return true;
            }
            else
            {
                return false;
            }
        }	

        public void Test()
        {   
            // ���TextWriter�йܶ��󽫻ᱻ���ݸ�Win32 API����
            // �������һֱ���ڣ�ֱ�����ý���
            TextWriter outputWriter = System.Console.Out;

            // ʹ��GCHandle����ֹTextWriter������������������
            GCHandle gch = GCHandle.Alloc(outputWriter, GCHandleType.Normal);
            
            EnumWindowProc cb = new EnumWindowProc(DisplayWindowsTitleProc);

            // P/Invoke���ڵ��ý���ǰ���Զ���ֹ����
            // ��������ί�н��л���
            EnumDesktopWindows(IntPtr.Zero, cb, (IntPtr)gch);

            // ��������ҪGCHandleʱ���������Free���������ͷŵ�
            gch.Free();
        }
    }


}
