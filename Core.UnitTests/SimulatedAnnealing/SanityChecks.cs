using System;
using System.Linq;
using System.Text;
using Algorithms.SimulatedAnnealing;
using Animals.Core.Utililties;
using NUnit.Framework;

namespace Core.UnitTests.SimulatedAnnealing
{
    [TestFixture]
    public class SanityChecks
    {
        public TimeSpan TestDuration { get; set; }
        public DateTime TestStart { get; set; }
        public DateTime TestStop { get; set; }

        //[TestCase(15, 9)]
        [TestCase(15000, 9000)]
        public void CreateRandomDataset(int size, int upperLimit)
        {
            TestStart = DateTime.Now;
            var dataset = new TravelingSalesmanProblem();
            dataset.CreateDatasetFromRandom(size, @"algo-data\Datafile" + size + @".txt", upperLimit);
            TestStop = DateTime.Now;
            TestDuration = TestStop - TestStart;
            Console.WriteLine(@"Time duration: " + TestDuration);
        }

        //[TestCase(15)]
        [TestCase(15000)]
        public void TspProblemTest(int fileNumber)
        {
            TestStart = DateTime.Now;
            var problem = new TravelingSalesmanProblem { FilePath = @"algo-data\Datafile" + fileNumber + @".txt" };
            problem.Anneal();

            var path = new StringBuilder();
            for (var i = 0; i < problem.CitiesOrder.Count - 1; i++)
            {
                path.Append(problem.CitiesOrder[i] + @" -> ");
            }
            path.Append(problem.CitiesOrder[problem.CitiesOrder.Count - 1]);
            TestStop = DateTime.Now;
            TestDuration = TestStop - TestStart;
            Console.WriteLine(@"Shortest route: " + path);
            Console.WriteLine(@"The shortest distance is: " + problem.ShortestDistance + @" with " + problem.Iteration + @" iterations.");
            Console.WriteLine(@"Time duration: " + TestDuration);
            Logger.WriteLog(@"Running test with " + fileNumber + @" x,y length data.", Logger.LogType.Information, Logger.LogCaller.TestFramework);
            Logger.WriteLog(@"Shortest route: " + path, Logger.LogType.Information, Logger.LogCaller.TestFramework);
            Logger.WriteLog(@"The shortest distance is: " + problem.ShortestDistance + @" with " + problem.Iteration + @" iterations.", Logger.LogType.Information, Logger.LogCaller.TestFramework);
            Logger.WriteLog(@"Time duration: " + TestDuration, Logger.LogType.Information, Logger.LogCaller.TestFramework);
        }
        [Test]
        public void AnnealingTest()
        {
            TestStart = DateTime.Now;
            var ann = new Annealing(1E-30);
            var distance = ann.StartAnnealing();
            TestStop = DateTime.Now;
            TestDuration = TestStop - TestStart;
            Console.WriteLine(distance);
            Console.WriteLine(@"Time duration: " + TestDuration);
        }
        
        public void LinqAnnealing()
        {
            var result = (from res in Enumerable.Range(0, 1)
                          let r = new Random()
                          let initial = Enumerable.Range(0, 10).Select(i => r.Next(-10, 10))
                          let iterations = Enumerable.Range(0, 1000)

                          let schedule = (Func<int, float>)
                                         (x => 4 + (float)Math.Sin(x))


                          from iteration in iterations
                          let temperature = schedule(iteration)
                          let state = Enumerable.Range(0, 10).Select(i => r.Next(-10, 10))
                          let deltaE = state.Sum() - initial.Sum()

                          where deltaE > 0 ||
                                Math.Pow(Math.E, deltaE / temperature) > r.NextDouble()
                          select state.ToList()
              ).OrderBy(s => s.Sum()).First();

            Console.WriteLine(result);
        }
    }
}
