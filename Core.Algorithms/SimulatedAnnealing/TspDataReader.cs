using System;
using System.IO;

namespace Algorithms.SimulatedAnnealing
{
    public static class TspDataReader
    {
        private static int _dim = 15;
        private static double[,] _data;
        public static double ComputeDistance(int[] t)
        {
            var distance = 0;
            for (var i = 0; i < _dim - 1; i++)
                distance += Convert.ToInt32(_data[t[i], t[i + 1]]);
            distance += Convert.ToInt32(_data[t[_dim - 1], t[0]]);
            return distance;
        }
        public static double[,] GetData()
        {
            return _data;
        }
        public static void ComputeData()
        {
            _data = new double[_dim, _dim];

            try
            {
                var str = new StreamReader(Environment.CurrentDirectory + "\\algo-data\\tsp.txt");
                for (var i = 0; i < _dim; i++)
                {
                    var line = str.ReadLine();
                    if (line == null)
                    {
                        continue;
                    }
                    var st = line.Split(' ');
                    for (var j = 0; j < _dim; j++)
                        _data[i, j] = double.Parse(st[j]);
                }
                str.Close();
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.ToString());
            }
            catch (IOException e)
            {
                Console.WriteLine(e.ToString());
            }

        }
    }
}
