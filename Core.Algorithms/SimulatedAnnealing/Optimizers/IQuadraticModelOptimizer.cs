namespace Algorithms.SimulatedAnnealing.Optimizers
{
    /// <summary>
    /// Interface for derivative-free optimizers following the basic principles of M.J.D. Powell's quadratic model trust region optimizers.
    /// </summary>
    internal interface IQuadraticModelOptimizer : ITrustRegionOptimizer
    {
        /// <summary>
        /// Gets or sets the number of interpolation conditions.
        /// </summary>
        int InterpolationConditions { get; set; }
    }
}