namespace Algorithms.SimulatedAnnealing.Optimizers
{
    /// <summary>
    /// Interface for derivative-free optimizers following the basic principles of M.J.D. Powell's trust region optimizers.
    /// </summary>
    internal interface ITrustRegionOptimizer : IOptimizer
    {
        /// <summary>
        /// Gets or sets the final value of the trust region radius.
        /// </summary>
        double TrustRegionRadiusStart { get; set; }

        /// <summary>
        /// Gets or sets the start value of the trust region radius.
        /// </summary>
        double TrustRegionRadiusEnd { get; set; }
    }
}