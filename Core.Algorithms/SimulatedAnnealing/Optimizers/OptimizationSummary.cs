namespace Algorithms.SimulatedAnnealing.Optimizers
{
    /// <summary>
    /// Summary of output from optimization.
    /// </summary>
    public class OptimizationSummary
    {
        /// <summary>
        /// Initializes an instance of the optimization summary.
        /// </summary>
        /// <param name="status">Status of the completed optimization.</param>
        /// <param name="nf">Number of function evaluations.</param>
        /// <param name="x">Optimized variable array.</param>
        /// <param name="f">Optimal value of the objective function.</param>
        /// <param name="g">If defined, values of constraint functions at optimum.</param>
        internal OptimizationSummary(OptimizationStatus status, int nf, double[] x, double f, double[] g = null)
        {
            Status = status;
            Evals = nf;
            X = x;
            F = f;
            G = g;
        }

        /// <summary>
        /// Gets the status of the completed optimization.
        /// </summary>
        public OptimizationStatus Status { get; private set; }

        /// <summary>
        /// Gets the number of function evaluations.
        /// </summary>
        public int Evals { get; private set; }

        /// <summary>
        /// Gets the optimized variable array.
        /// </summary>
        public double[] X { get; private set; }

        /// <summary>
        /// Gets the optimal value of the objective function.
        /// </summary>
        public double F { get; private set; }

        /// <summary>
        /// Gets the values of the constraint functions at optimum, if defined.
        /// </summary>
        public double[] G { get; private set; }

    }
}