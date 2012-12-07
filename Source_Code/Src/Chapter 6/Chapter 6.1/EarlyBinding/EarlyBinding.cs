using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace EarlyBinding
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public class WeatherManager
    {
        protected int _operationCount;

        public double GetTemperatureCELSIUS()
        {
            _operationCount++;
            return 28;
        }

        // ��ClassInterfaceTypeΪAutoDualʱ��������°汾
        // ��WeatherManager���м�����GetWindSpeed()�·�����
        // �����ᵼ��ԭ�е�COM�ͻ��˵��ô�����Ϊ�µķ���
        // �ı���COM�ӿڶ���Ĳ��֡�
        //public double GetWindSpeed()
        //{
        //    _operationCount++;
        //    return 10;
        //}

        public double ConvertCelsius2Fahrenheit(double CelsiusDegree)
        {
            _operationCount++;
            return CelsiusDegree * 1.8 + 32;
        }

        public int OperationCount
        {
            get { return _operationCount; } 
        }
    }
}
