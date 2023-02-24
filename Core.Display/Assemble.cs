using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using Boagaphish.Schema;
using Boagaphish.Writer;

namespace Core.Display
{
    public class Assemble
    {
        public List<string> Emotions;
        readonly StreamReader _featuresReader = new StreamReader(Environment.CurrentDirectory + @"\" + ConfigurationManager.AppSettings["featuresFilePath"]);

        /// <summary>
        /// Initializes a new instance of the <see cref="Assemble"/> class which assembles a new animal.
        /// </summary>
        public Assemble()
        {
            // Build the neural network schema.
            var list = new List<object>();
            var baseOne = new BaseSchema("base", 64, 48, 0, 0, list);
            var nextOne = new NextSchema("next", 7, 16, 4, 13, list);
            var timeSchema = new TimeSchema("time", 4, 2, 0, 0, list);

            string feature;
            Emotions = new List<string>();

            while ((feature = _featuresReader.ReadLine()) != null)
            {
                Emotions.Add(feature);  
            } 
        }
    }

    public enum Mood { Happy, Sad, None, Angry, Hyper, Content }
    //public enum Mood { Happy, Confident, Energized, Helped, Insecure, Sad, Hurt, Tired }

    public class Concept
    {
        public string Left { get; set; }
        public string Right { get; set; }
        public string Get { get; set; }
        public string Stop { get; set; }
        public string Up { get; set; }
        public string Down { get; set; }
    }

    public class Life
    {
        public double HoursAlive { get; set; }
        public double MinutesAlive { get; set; }
        public double SecondsAlive { get; set; }

        //readonly StreamReader _timeReader = new StreamReader(ConfigurationManager.AppSettings["timeFilePath"]);

        public Life(double aliveTime)
        {
            Logging.LogMessage(ConfigurationManager.AppSettings["logFilePath"], "Life constructor init", Logging.LogType.Information);
            Logging.ActiveTime.LogTime(aliveTime, ConfigurationManager.AppSettings["timeFilePath"], false);
        }

    }
}
