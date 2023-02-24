using System;

namespace Animals.Core.Addins
{
    public class Personality
    {
        public class Mood
        {
            public static string CurrentMood { get; set; }
            private static Moods _currentMood;

            public enum Moods { happy, sad, angry, depressed, mellow }
            public Mood(int seed)
            {
                var sortie = new Random(Convert.ToInt32(seed));
                var store = sortie.Next(0, 4);
                CurrentMood = SetMood(store);
            }

            public static string SetMood(int mood)
            {
                switch (mood)
                {
                    case 0:
                        _currentMood = Moods.happy;
                        break;
                    case 1:
                        _currentMood = Moods.sad;
                        break;
                    case 2:
                        _currentMood = Moods.angry;
                        break;
                    case 3:
                        _currentMood = Moods.depressed;
                        break;
                    case 4:
                        _currentMood = Moods.mellow;
                        break;
                }
                CurrentMood = _currentMood.ToString();
                return CurrentMood;
            }

            public static string GetMood()
            {
                return CurrentMood;
            }
        }
    }
}
