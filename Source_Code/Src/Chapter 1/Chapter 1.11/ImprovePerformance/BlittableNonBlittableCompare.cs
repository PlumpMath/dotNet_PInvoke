using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace ImprovePerformance
{
    class BlittableNonBlittableCompare
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

            testChar = '��';
            int blittableResult = UnmanagedFuction.IsAsciiBlittable((byte)testChar);
            Console.WriteLine(
                "{0} is{1} an ASCII character.",
                testChar, blittableResult != 0 ? "" : " not");

            //����һ���������Ĳ��Դ������Ա�����
            const int testCycle = 600000;
            bool isAscii = false;

            //���ڼ�ʱ
            Stopwatch stopWatch = new Stopwatch();

            //����Nonblittable����
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

            //����blittable����
            stopWatch.Reset();
            stopWatch.Start();
            for (int i = 0; i < testCycle; i++)
            {
                isAscii = (UnmanagedFuction.IsAsciiBlittable((byte)'c') != 0);
            }
            stopWatch.Stop();
            Console.WriteLine(
                "Call blittable function IsAsciiBlittable {0} times, "
                + "take {1} milliseconds",
                testCycle, stopWatch.ElapsedMilliseconds);

            // ֻʹ���йܴ��������ͬ�Ĳ���
            stopWatch.Reset();
            stopWatch.Start();
            for (int i = 0; i < testCycle; i++)
            {
                isAscii = ((byte)'c' < 0x80);
            }
            stopWatch.Stop();
            Console.WriteLine(
                "Only using managed code to do same test, "
                + "take {0} milliseconds",
                stopWatch.ElapsedMilliseconds);

            // ֻʹ�÷��йܴ��������ͬ�Ĳ���
            stopWatch.Reset();
            stopWatch.Start();
            UnmanagedFuction.UnmanagedTest();
            stopWatch.Stop();
            Console.WriteLine(
                "Only using unmanaged code to do same test, "
                + "take {0} milliseconds",
                stopWatch.ElapsedMilliseconds);
        }

    }
}