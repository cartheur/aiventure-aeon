using System;

namespace Algorithms.SimulatedAnnealing
{
    public class Annealing
    {
        private readonly Random rnd = new Random();
        // Probability parameters.
        public double Epsilon { get; set; }
        public double Alpha { get; set; }
        public double Temperature { get; set; }
        public double Distance { get; set; }
        public double Delta { get; set; }
        public int Iteration { get; set; }

        public Annealing(double epsilon = 1E-5)
        {
            Temperature = 400;
            Alpha = 0.999;
            Epsilon = epsilon;
        }

        private static void Assign(int[] c, int[] n)
        {
            for (var i = 0; i < c.Length; i++)
                c[i] = n[i];
        }
        /// <summary>
        /// Compute a new next configuration and save the old next as current.
        /// </summary>
        /// <param name="c">The current configuration</param>
        /// <param name="n">The next configuration</param>
        private void ComputeNext(int[] c, int[] n)
        {
            for (int i = 0; i < c.Length; i++)
                n[i] = c[i];
            var i1 = rnd.Next(14) + 1;
            var i2 = rnd.Next(14) + 1;
            var aux = n[i1];
            n[i1] = n[i2];
            n[i2] = aux;
        }
        public string StartAnnealing()
        {
            TspDataReader.ComputeData();
            // A configuration of elements (cities).
            int[] currentConfiguration = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };
            // The next configuration of cities to be tested.
            var nextConfiguration = new int[15];
            // Start the iteration cycle.
            Iteration = -1;
            // Compute the distance.
            Distance = TspDataReader.ComputeDistance(currentConfiguration);
            // While the temperature didn't reach epsilon.
            while (Temperature > Epsilon)
            {
                Iteration++;
                // Get the next random permutation of distances.
                ComputeNext(currentConfiguration, nextConfiguration);
                // Compute the distance of the new permuted configuration.
                Delta = TspDataReader.ComputeDistance(nextConfiguration) - Distance;
                // If the new distance is better accept it and assign it.
                if (Delta < 0)
                {
                    Assign(currentConfiguration, nextConfiguration);
                    Distance = Delta + Distance;
                }
                else
                {
                    double proba = rnd.Next();
                    // If the new distance is worse accept it but with a probability level.
                    // If the probability is less than E to the power -delta/temperature, otherwise the old value is kept.
                    if (proba < Math.Exp(-Delta / Temperature))
                    {
                        Assign(currentConfiguration, nextConfiguration);
                        Distance = Delta + Distance;
                    }
                }
                // Apply a cooling process to every iteration.
                Temperature *= Alpha;
                // Print every 500 iterations.
                if (Iteration % 500 == 0)
                    Console.WriteLine(Distance);
            }

            try
            {
                return @"The best distance is " + Distance + " with " + Iteration + " iterations.";
            }
            catch
            {
                return @"error";
            }
        }
    }
}
