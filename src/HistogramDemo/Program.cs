using System;
using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics.Statistics;

namespace HistogramDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var data = new double[] {
                1.01,0.93,0.11,0.6,0.9,2.76,3.25,6.93,2.97,3.02,3.17,3.09,0.6,0.9,8.7,9.4,0.3,3.7,0.4,2.78,3.06,3.28,0.56,3.6,2.58,7.79,16.73,1.05,1.26,0.95,5.04,2.97,3.39,0.59,7.15,0.5,2.66,14.91,0.29,0.43,0.29,0.1,0.2,0.3,0.5,0.7,3.9,0.9,2.4,0.9,4.6,1.4,0.2,0.4,0.8,0.1,1.4,2.3,5.7,0.1,0.2,0.25,0.1,0.4,0.05,0.5,1.8,0.2,0.4,0.4,0.3,1.9,0.3,8.37,1.06,0.23,0.57,7.15,3.0,5.62,6.16,0.2,17.5,0.3,3.6,1.2,0.1,0.27,0.36,0.33,0.08,0.2,0.66,0.1,0.85,0.1,1.5,1.0,2.2,2.1,0.9,2.6,1.2,3.0,0.2,1.3,0.5,0.1,0.2,0.05,1.0,0.7,0.1,3.2,3.7,1.7,3.1,3.3,0.5,0.4,1.0,3.7,4.0,1.2,0.45,0.06,1.1,0.4,1.96,3.46,3.4,4.74,0.91,7.45,0.15,0.3,0.1,1.4,0.8,0.3,0.4,0.4,0.4,0.6,0.2,0.5,0.5,0.3,0.5,1.5,1.7,1.2,0.1,0.8,1.3,2.98,0.3,0.47,0.2,0.4,0.42,0.4,0.2,0.2,1.22,0.3,0.3,0.9,1.09,0.19,0.28,0.3,0.6,0.78,3.2,0.2,0.24,0.3,1.2,2.9,27.23,8.86,38.4,14.89,47.72,10.23,13.19,10.82,4.99,0.4,16.15,18.03,13.1,7.93,18.87,11.9,34.83,13.61,11.47,5.39,16.58,3.42,9.35,40.47,17.98,0.87,0.2,0.2,0.48,0.21,0.1,0.8,2.8,0.3,1.83,4.09,1.17,57.21,2.17,0.22,15.46,2.36,3.3,47.0,68.0,143.0,109.0,176.0,184.0,132.0,71.0,116.0,190.0,92.0,106.0,133.0,122.0,162.0,94.0,28.0,194.0,49.0,186.0,126.0,71.0,76.0,110.0,100.0,42.0,196.0,17.0,115.0,140.0,94.0,23.0
            };
            DisplayHistogram(data);
        }
        /// <summary>
        /// Display histogram from the array
        /// </summary>
        /// <param name="data">Source array</param>
        public static void DisplayHistogram(IEnumerable<double> data)
        {
            var blockSymbol = Convert.ToChar(9608);

            var rowMaxLength = Console.WindowWidth - 1;
            rowMaxLength = (rowMaxLength / 10) * 10;
            var rowCount = rowMaxLength / 3;

            var histogram = new Histogram(data, rowMaxLength);

            // Find the absolute peak
            var maxBucketCount = 0.0;
            for (var i = 0; i < histogram.BucketCount; i++)
            {
                if (histogram[i].Count > maxBucketCount)
                {
                    maxBucketCount = histogram[i].Count;
                }
            }

            // Number of bucket counts between rows
            var rowStep = maxBucketCount / rowCount;

            // Draw histogram line-by-line
            Console.WriteLine();

            for (var row = 0; row < rowCount; row++)
            {
                for (var col = 0; col < histogram.BucketCount; col++)
                {
                    if (histogram[col].Count >= maxBucketCount)
                    {
                        Console.Write(blockSymbol);
                    }
                    else
                    {
                        Console.Write(@" ");
                    }
                }

                Console.SetCursorPosition(0, Console.CursorTop + 1);
                maxBucketCount -= rowStep;
            }

            // Calculate distanse between label in X axis
            var axisStep = histogram.BucketCount / 2;

            var leftLabel = histogram.LowerBound.ToString("N");
            var middleLabel = ((histogram.UpperBound + histogram.LowerBound) / 2.0).ToString("N");
            var rightLabel = histogram.UpperBound.ToString("N");

            Console.Write(leftLabel);
            for (var j = 0; j < axisStep - leftLabel.Length; j++)
            {
                Console.Write(@" ");
            }

            Console.Write(middleLabel);
            for (var j = 0; j < axisStep - middleLabel.Length; j++)
            {
                Console.Write(@" ");
            }

            Console.Write(rightLabel);

            Console.WriteLine();
        }

        /// <summary>
        /// Display histogram from the array
        /// </summary>
        /// <param name="data">Source array</param>
        public static void DisplayHistogram(IEnumerable<int> data)
        {
            DisplayHistogram(data.Select(x => (double)x));
        }
    }
}
