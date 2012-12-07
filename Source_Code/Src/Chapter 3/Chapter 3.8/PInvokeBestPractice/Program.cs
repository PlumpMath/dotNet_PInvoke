using System;
using System.Collections.Generic;
using System.Text;

namespace PInvokeBestPractice
{
    class Program
    {
        static void Main(string[] args)
        {
            // �������ʵ�����ᳫ�ķ�ʽ����MessageBeep����
            Console.WriteLine("Wait for the system sound ...");
            NativeSound.MessageBeep(BeepTypes.MB_ICONEXCLAMATION);

            Console.WriteLine();

            // ʹ�ò�ͬ������������GetVersionInfoEx������
            PerformanceTest perf = new PerformanceTest();
            perf.PerfTest(1000 * 1000);
            perf.ExplicitLayoutTest(1000 * 1000);

            Console.WriteLine("\r\n��������˳�...");
            Console.Read();
        }
    }
}
