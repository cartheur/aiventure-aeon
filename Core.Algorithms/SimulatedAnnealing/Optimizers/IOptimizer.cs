using System.IO;

namespace Algorithms.SimulatedAnnealing.Optimizers
{
    /// <summary>
    /// General interface for optimizers in the CS Numerics class library.
    /// </summary>
    interface IOptimizer
    {
        /// <summary>
        /// Gets or sets the number of maximum function calls.
        /// </summary>
        int MaximumFunctionCalls { get; set; }
        /// <summary>
        /// Gets or sets the print level to the logger.
        /// </summary>
        int PrintLevel { get; set; }
        /// <summary>
        /// Gets or sets the logger to which the optimizer log information should be sent.
        /// </summary>
        TextWriter Logger { get; set; }
        /// <summary>
        /// Find a local minimum of provided objective function satisfying the provided linear constraints.
        /// </summary>
        /// <param name="x0">Initial variable array.</param>
        /// <returns>Summary of the optimization result.</returns>
        OptimizationSummary FindMinimum(double[] x0);

    }
}