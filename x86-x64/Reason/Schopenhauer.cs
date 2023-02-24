
namespace Animals.Core.Reason
{

    public class Schopenhauer
    {
        public enum Correction { None, Light, Medium, Heavy }
        /// <summary>
        /// The state of correction.
        /// </summary>
        public static Correction CorrectionState { get; set; }
    }
}
