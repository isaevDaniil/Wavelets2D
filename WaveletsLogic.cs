using System;
using System.Collections.Generic;

namespace Wavelets2D
{
    public class WaveletsLogic
    {
        public static List<List<double>> transformCoefs = new List<List<double>>();
        public delegate double Wavelet(int a, int b, double t);
        public static int aMin = 2;
        public static int aMax = 4;
        
        public static double MHAT(int a, int b, double t)
        {
            return (1 - Math.Pow(((t - b) / a), 2)) * Math.Exp(Math.Pow(((t - b) / a), 2) / (-2));
        }

        public static double Wave(int a, int b, double t)
        {
            return ((t - b) / a) * Math.Exp(Math.Pow(((t - b) / a), 2) / (-2));
        }

        public static List<List<double>> DisreteTransform(List<double> signal, Wavelet selectedWavelet)
        {
            List<List<double>> resultCoefs = new List<List<double>>();
            int bMax = signal.Count;
            for (int a = aMin; a <= aMax; a += 1)
            {
                var tempList = new List<double>();
                for (int b = 0; b < bMax; b++)
                {
                    double sum = 0;
                    for (int i = 0; i < signal.Count; i++)
                    {
                        sum += signal[i] * selectedWavelet(a, b, i);

                    }
                    sum *= 1 / Math.Sqrt(a);
                    tempList.Add(sum);
                }
                resultCoefs.Add(tempList);
            }
            return resultCoefs;
        }
    }
}
