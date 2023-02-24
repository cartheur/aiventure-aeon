using System;
using System.Runtime.Remoting.Activation;

namespace Animals.Core.Reason
{
    //[ReasonAttribute(false, OptionalParameter = "optional data")]
    /// <summary>
    /// The real reason noumenon.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class ReasonAttribute : Attribute
    {
        public string ReasonValue { get; set; }
        public static bool ReasonActive { get; set; }

        public ReasonAttribute(string value, bool active)
        {
            ReasonValue = value;
            ReasonActive = active;
        }
    }

    /// <summary>
    /// Attribute indicating what part of executing code is eligible for real reason.
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class KantianAttribute : Attribute
    {
        private readonly int _level;
        private bool _vetted;

        public KantianAttribute(Kant.Gauge value, int level)
        {
            if (ReasonAttribute.ReasonActive)
            {
                Value = value;
                _level = level;
                _vetted = false;
            }
            if (level > 3)
                ComputeCorrection();
        }

        public static Kant.Gauge Value { get; private set; }

        public virtual int Level
        {
            get { return _level; }
        }

        public virtual bool Vetted
        {
            get { return _vetted; }
            set { _vetted = value; }
        }

        public static Schopenhauer.Correction Correction { get; set; }

        private void ComputeCorrection()
        {
            switch (_level)
            {
                case 2:
                    Correction = Schopenhauer.Correction.None;
                    break;
            }

            if (_level > 2 && _level <= 4)
                Correction = Schopenhauer.Correction.Light;
            if (_level > 4 && _level <=7)
                Correction = Schopenhauer.Correction.Medium;
            if (_level > 7 && _level <= 10)
                Correction = Schopenhauer.Correction.Heavy;
        }
    }
}
