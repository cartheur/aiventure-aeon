using System;
using System.Linq;
using Algorithms.SimulatedAnnealing.Optimizers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.UnitTests.SimulatedAnnealing.Issues
{
    [TestClass]
    public class GH1
    {
        [TestMethod]
        public void Cobyla_FindMinimum_ConstraintsShouldBeSatisfied()
        {
            var x0 = new[] { 0.705, 0.0075, 0.075, 0, 0.0375, 0, 0.05, 0.125 };
            var cobyla = new Cobyla(x0.Length, 3 + x0.Length, Calcfc)
                             {
                                 MaximumFunctionCalls = 100000,
                                 TrustRegionRadiusStart = 1.0,
                                 TrustRegionRadiusEnd = 1.0e-6,
                                 PrintLevel = 1,
                                 Logger = Console.Out
                             };

            var summary = cobyla.FindMinimum(x0);
            Assert.AreEqual(OptimizationStatus.Normal, summary.Status);
            Assert.IsTrue(summary.G.All(c => c >= -1.0e-6));
        }

        private static void Calcfc(int n, int m, double[] x, out double f, double[] con)
        {
            for (var i = 0; i < x.Length; i++)
            {
                con[i] = x[i];
            }

            var sum = 0.0;
            for (var i = 0; i < n; i++)
            {
                sum += x[i];
            }
            con[n] = sum - 0.9999;
            con[n + 1] = 1.0001 - sum;

            double[][] matrix =
                {
                    new[] { -0.25, -0.25, 0.75, 0.75, -0.25, -0.25, 0.75, 0.75 },
                    new[] { 0.0, -0.8, 0.0, 0.2, 0.0, -0.8, 0.0, 0.2 },
                    new[] { 0.0, 0.0, 0.0, 0.0, -0.6, -0.6, 0.4, 0.4 },
                    new[] { 0.0, 0.0, -0.7, -0.7, 0.0, 0.0, 0.3, 0.3 },
                    new[] { 0.0, 0.0, -0.5, 0.5, 0.0, 0.0, -0.5, 0.5 },
                    new[] { -0.05, -0.05, 0.0, 0.0, 0.95, 0.95, 0.0, 0.0 },
                    new[] { -0.01, 0.99, 0.0, 0.0, -0.01, 0.99, 0.0, 0.0 }
                };
            var matrix_m_x = new double[matrix.GetLength(0)];
            sum = 0;
            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                for (var j = 0; j < matrix[i].Length; j++)
                {
                    matrix_m_x[i] = matrix_m_x[i] + matrix[i][j] * x[j];
                }
                sum += Math.Abs(matrix_m_x[i]);
            }
            con[n + 2] = 0.06651 - sum;

            var entropy = 0.0;
            for (var i = 0; i < n; i++)
            {
                var d = x[i];
                if (d <= 0.0)
                {
                    continue;
                }
                entropy -= d * Math.Log(d, 2.0);
            }

            f = -entropy;
        }
    }
}
