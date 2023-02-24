namespace Algorithms.SimulatedAnnealing.Optimizers
{
    /// <summary>
    /// Optimization status codes.
    /// </summary>
    public enum OptimizationStatus
    {

        /*
         * Common
         */

        /// <summary>
        /// Optimization successfully completed.
        /// </summary>
        Normal,

        /// <summary>
        /// Too few variables.
        /// </summary>
        N_TooSmall,

        /// <summary>
        /// Invalid number of interpolation conditions.
        /// </summary>
        NPT_OutOfRange,

        /// <summary>
        /// Specified maximum number of function evaluations must exceed number of interpolation conditions.
        /// </summary>
        MAXFUN_NotLargerThan_NPT,

        /// <summary>
        /// Maximum number of iterations (function/constraints evaluations) reached during optimization.
        /// </summary>
        MAXFUN_Reached,

        /// <summary>
        /// Size of rounding error is becoming damaging, terminating prematurely.
        /// </summary>
        X_RoundingErrorsPreventUpdate,

        /*
         * LINCOA specific
         */

        /// <summary>
        /// Constraint gradient is too small.
        /// </summary>
        ConstraintGradientIsZero,

        /// <summary>
        /// Denominator in updating formula is too small.
        /// </summary>
        UpdatingFormulaDenominatorZero,

        /*
         * BOBYQA specific
         */

        /// <summary>
        /// Insufficient number of variable bounds.
        /// </summary>
        VariableBoundsArrayTooShort,

        /// <summary>
        /// Invalid variable bounds specification.
        /// </summary>
        InvalidBoundsSpecification,

        /// <summary>
        /// Distance between lower and upper bound is insufficient for one or more variables.
        /// </summary>
        BoundsRangeTooSmall,

        /// <summary>
        /// Denominator cancellation.
        /// </summary>
        DenominatorCancellation,

        /// <summary>
        /// Reduction of trust-region step failed.
        /// </summary>
        TrustRegionStepReductionFailure

    }
}