using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace ImprovePerformance
{
    class UnicodeANSIConversionTest
    {
        public static void PerfTest()
        {
            // Ϊ�˴���һ����ƽ�Ĳ��Ի������ڽ���
            // ��ʽ�ԱȲ���ǰ������Ҫ�Է��йܺ���
            // ����һ��ƽ̨���ã��Ա���ɼ��ط���
            // ��DLL�����Һ�����ڵ�ȹ���
            char testChar = 'h';
            bool nonblittableResult = UnmanagedFuction.IsAsciiNonblittable(testChar);
            Console.WriteLine(
                "{0} is{1} an ASCII character.",
                testChar, nonblittableResult ? "" : " not");

            bool blittableResult = UnmanagedFuction.IsWasciiNonblittable(testChar);
            Console.WriteLine(
                "{0} is{1} an ASCII character.",
                testChar, blittableResult ? "" : " not");

            //����һ���������Ĳ��Դ������Ա�����
            const int testCycle = 600000;
            bool isAscii = false;

            //���ڼ�ʱ
            Stopwatch stopWatch = new Stopwatch();

            //����Nonblittable����IsAsciiNonblittable
            stopWatch.Start();
            for (int i = 0; i < testCycle; i++)
            {
                isAscii = UnmanagedFuction.IsAsciiNonblittable('c');
            }
            stopWatch.Stop();
            Console.WriteLine(
                "Call nonblittable function IsAsciiNonblittable {0} times, "
                + "take {1} milliseconds",
                testCycle, stopWatch.ElapsedMilliseconds);

            //����Nonblittable����IsWasciiNonblittable
            stopWatch.Reset();
            stopWatch.Start();
            for (int i = 0; i < testCycle; i++)
            {
                isAscii = UnmanagedFuction.IsWasciiNonblittable('c');
            }
            stopWatch.Stop();
            Console.WriteLine(
                "Call nonblittable function IsWasciiNonblittable {0} times, "
                + "take {1} milliseconds",
                testCycle, stopWatch.ElapsedMilliseconds);
        }
    }
}
