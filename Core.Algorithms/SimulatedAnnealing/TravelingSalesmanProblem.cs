using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Algorithms.SimulatedAnnealing
{
    public class TravelingSalesmanProblem
    {
        private List<int> _nextOrder = new List<int>();
        private double[,] _distances;
        private readonly Random _random = new Random();

        public double ShortestDistance { get; set; } = 0;
        public string FilePath { get; set; }
        public List<int> CitiesOrder { get; set; } = new List<int>();
        public double Temperature { get; set; }
        public double Distance { get; set; }
        public double Delta { get; set; }
        public double CoolingRate { get; set; }
        public double AbsoluteTemperature { get; set; }
        public int Iteration { get; set; }
        public int DataSize { get; set; }

        public TravelingSalesmanProblem()
        {
            Temperature = 10000;
            CoolingRate = 0.9999;
            AbsoluteTemperature = 0.00001;
        }
        /// <summary>
        /// Load cities from the text file representing the adjacency matrix
        /// </summary>
        public void LoadCities()
        {
            var reader = new StreamReader(FilePath);
            var cities = reader.ReadToEnd();
            reader.Close();
            var rows = cities.Split('\n');
            _distances = new double[rows.Length, rows.Length];

            for (var i = 0; i < rows.Length - 1; i++)
            {
                var distance = rows[i].Split(' ');
                for (var j = 0; j < distance.Length; j++)
                {
                    if (distance[j].Contains("\r"))
                        distance[j] = distance[j].Replace("\r", string.Empty);
                    _distances[i, j] = double.Parse(distance[j]);
                }

                // The number of rows in this matrix represent the number of cities. We are representing each city by an index from 0 to n - 1, where N is the total number of cities.
                CitiesOrder.Add(i);
            }

            if (CitiesOrder.Count < 1)
                throw new Exception(@"No cities to order.");
        }
        /// <summary>
        /// Calculate the total distance which is the objective function
        /// </summary>
        /// <param name="order">A list containing the order of cities</param>
        /// <returns></returns>
        public double GetTotalDistance(IList<int> order)
        {
            double distance = 0;

            for (var i = 0; i < order.Count - 1; i++)
            {
                distance += _distances[order[i], order[i + 1]];
            }
            if (order.Count > 0)
            {
                distance += _distances[order[order.Count - 1], 0];
            }

            return distance;
        }
        /// <summary>
        /// Get the next random arrangements of cities
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public List<int> GetNextArrangement(IList<int> order)
        {
            var newOrder = new List<int>();

            foreach (var t in order)
                newOrder.Add(t);

            // We will only rearrange two cities by random, starting point should be always zero - so zero should not be included.
            var firstRandomCityIndex = _random.Next(1, newOrder.Count);
            var secondRandomCityIndex = _random.Next(1, newOrder.Count);
            var dummy = newOrder[firstRandomCityIndex];

            newOrder[firstRandomCityIndex] = newOrder[secondRandomCityIndex];
            newOrder[secondRandomCityIndex] = dummy;

            return newOrder;
        }
        /// <summary>
        /// Start the annealing process.
        /// </summary>
        public void Anneal()
        {
            // Start iteration.
            Iteration = -1;
            try
            {
                LoadCities();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Distance = GetTotalDistance(CitiesOrder);

            while (Temperature > AbsoluteTemperature)
            {
                _nextOrder = GetNextArrangement(CitiesOrder);
                Delta = GetTotalDistance(_nextOrder) - Distance;
                // If the new order has a smaller distance, or if the new order has a larger distance but satisfies Boltzman condition, then accept the arrangement.
                if ((Delta < 0) || (Distance > 0 && Math.Exp(-Delta / Temperature) > _random.NextDouble()))
                {
                    for (var i = 0; i < _nextOrder.Count; i++)
                        CitiesOrder[i] = _nextOrder[i];

                    Distance = Delta + Distance;
                }

                // Cool down.
                Temperature *= CoolingRate;

                Iteration++;
            }

            ShortestDistance = Distance;
        }

        public bool CreateDatasetFromRandom(int size, string fileName, int upperLimit = 9)
        {
            // Create datasets for the tsp problems with different values of increasing size separation (distance).
            // Save as text file.
            var random = new Random(DateTime.Now.Second);
            var bufferBuilder = new StringBuilder();
            var columns = size; // The current dataset size.
            var rows = size; // The current dataset size.
            // Set the zeroth element, the place where it starts (plus a space).
            bufferBuilder.Append(@"0.0 ");
            var dataFile = new StreamWriter(Environment.CurrentDirectory + @"\" + fileName, false);

            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < columns - 1; j++)
                {
                    bufferBuilder.Append(Convert.ToDouble(random.Next(1, upperLimit)).ToString("N1"));
                    bufferBuilder.Append(@" ");
                }
                bufferBuilder = bufferBuilder.Replace(@"\r", string.Empty);
                dataFile.WriteLine(bufferBuilder.ToString().TrimEnd());
                bufferBuilder.Clear();
                columns = 16;
            }

            dataFile.Dispose();

            return true;
        }
    }
}
