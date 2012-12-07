using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace PInvokeBestPractice
{
    /// <summary>
    /// ʹ��C#�е�ö���������궨��
    /// </summary>
    enum BeepTypes
    {
        MB_SIMPLE = -1,                                               // Simple beep.
        MB_OK = 0x00000000,                                     // SystemDefault
        MB_ICONHAND = 0x00000010,                     // SystemHand
        MB_ICONQUESTION = 0x00000020,            // SystemQuestion
        MB_ICONEXCLAMATION = 0x00000030,   // SystemExclamation
        MB_ICONASTERISK = 0x00000040              // SystemAsterisk
    }

    /// <summary>
    /// �����йܷ�����װ��һ����̬��װ����
    /// </summary>
    static class NativeSound
    {
        /// <summary>
        /// ���Ƕ�Win32 API����MessageBeep��һ����װ����
        /// </summary>
        /// <param name="type"></param>
        public static void MessageBeep(BeepTypes type)
        {
            if (!Enum.IsDefined(typeof(BeepTypes), type))
            {
                throw new ArgumentException("BeepType can not be recognized!");
            }

            if (!MessageBeep((UInt32)type))
            {
                // ���ʧ�ܣ�������Ĵ����룬
                // ���׳���Ӧ��Win32�쳣
                Int32 err = Marshal.GetLastWin32Error();
                throw new Win32Exception(err);
            }
        }

        // �����йܺ������йܶ�������Ϊһ��˽�з���
        [DllImport("User32.dll", SetLastError = true)]
        private static extern Boolean MessageBeep(UInt32 beepType);

    }
   
}
