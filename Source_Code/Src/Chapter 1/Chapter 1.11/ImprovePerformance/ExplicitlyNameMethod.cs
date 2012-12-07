using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace ImprovePerformance
{
    class ExplicitlyNameMethod
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

            StringBuilder sbW = new StringBuilder(rawString.Length);
            UnmanagedFuction.ReverseStringA(rawString, sbW);

            //����һ���������Ĳ��Դ������Ա�����
            const int testCycle = 600000;

            //���ڼ�ʱ
            Stopwatch stopWatch = new Stopwatch();

            //����δ����ExactSpelling�ķ��йܺ���
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

            //��������ExactSpelling = true�ķ��йܺ���
            stopWatch.Reset();
            stopWatch.Start();
            for (int i = 0; i < testCycle; i++)
            {
                sbW = new StringBuilder(rawString.Length);
                UnmanagedFuction.ReverseStringA(rawString, sbW);
            }
            stopWatch.Stop();
            Console.WriteLine(
                "Call ReverseStringA {0} times, "
                + "take {1} milliseconds",
                testCycle, stopWatch.ElapsedMilliseconds);
        }

    }
}

