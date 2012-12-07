using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace ImprovePerformance
{
    class UsingIntPtr
    {
        public static void PerfTest()
        {
            // Ϊ�˴���һ����ƽ�Ĳ��Ի������ڽ���
            // ��ʽ�ԱȲ���ǰ������Ҫ�Է��йܺ���
            // ����һ��ƽ̨���ã��Ա���ɼ��ط���
            // ��DLL�����Һ�����ڵ�ȹ���
            string rawString = "Bill";
            StringBuilder sb = new StringBuilder(rawString.Length);
            UnmanagedFuction.ReverseString(rawString, sb);

            IntPtr reversedStringPtr = Marshal.AllocHGlobal(rawString.Length * 2);
            UnmanagedFuction.ReverseString(rawString, reversedStringPtr);
            string reversedString = Marshal.PtrToStringAnsi(reversedStringPtr);

            //����һ���������Ĳ��Դ������Ա�����
            const int testCycle = 600000;

            //���ڼ�ʱ
            Stopwatch stopWatch = new Stopwatch();

            //����Nonblittable����
            stopWatch.Start();
            for (int i = 0; i < testCycle; i++)
            {
                sb = new StringBuilder(rawString.Length);
                UnmanagedFuction.ReverseString(rawString, sb);
            }
            stopWatch.Stop();
            Console.WriteLine(
                "Call ReverseString {0} times, "
                + "take {1} milliseconds",
                testCycle, stopWatch.ElapsedMilliseconds);

            //����blittable����
            stopWatch.Reset();
            stopWatch.Start();
            for (int i = 0; i < testCycle; i++)
            {   
                UnmanagedFuction.ReverseString(rawString, reversedStringPtr);
                string newReversedString = Marshal.PtrToStringAnsi(reversedStringPtr);
            }
            Marshal.FreeHGlobal(reversedStringPtr);
            stopWatch.Stop();
            Console.WriteLine(
                "Call ReverseString with IntPtr as param type {0} times, "
                + "take {1} milliseconds",
                testCycle, stopWatch.ElapsedMilliseconds);

            Console.WriteLine("Press any key to exit");
            Console.Read();
        }

    }
}
