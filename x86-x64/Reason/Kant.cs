
namespace Animals.Core.Reason
{
    public class Kant
    {
        public enum Gauge { TranscendentalAnalytic, TranscendentalAthestetic, Priori, Posteriori }
        /// <summary>
        /// The state of guage.
        /// </summary>
        public static Gauge GaugeState { get; set; }
    }
}
