using System;
using System.Collections.Generic;
using System.Text;

namespace MarshalFullExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Test();

            Console.WriteLine("\r\n��������˳�...");
            Console.Read();
        }

        private static void Test()
        {
            NameEntityType allType = NameEntityType.OrganizationName | NameEntityType.PersonName | NameEntityType.PlaceName;
            using (NameFinderWrapper nameFinder = new NameFinderWrapper(allType))
            {
                bool isInit = nameFinder.Initialize(@".\data");
                if (isInit)
                {
                    List<NameEntity> nameResults;
                    string text = @"���죬����ȫ�����ص�����ѧ�Ӿۼ���������΢�������о�Ժ��ȡ�˺�С��Ժ��������Ժ���͹�����Ժ���ľ����ݽ���";
                    Console.WriteLine("�����ı���{0}{1}", Environment.NewLine, text);
                    bool isSuccess = nameFinder.FindNames(text, out nameResults);
                    if (isSuccess)
                    {
                        Console.WriteLine("���ֽ����ɹ���������£�");
                        Console.WriteLine("\t     ����\t\t����\t     ��ʼλ��\t����\tģ�͸���");
                        Console.WriteLine("          -------------------------------------------------------------");
                        foreach (NameEntity name in nameResults)
                        {
                            Console.WriteLine("{0, 15}\t{1, 18}\t{2, -4}\t{3, 2}\t{4, -4}",
                                name.Name,
                                name.Type,
                                name.HighlightBegin,
                                name.HighlightLength,
                                name.Score);
                        }

                        Console.WriteLine();
                    }
                }
            }
        }

    }
}
