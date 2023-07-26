using System;
using System.Collections.Generic;

namespace Data
{
    [Serializable]
    public class AchievementsData
    {
        public List<bool> Achievements;

        public AchievementsData()
        {
            Achievements = new List<bool>();
            for (int i = 0; i < 10; i++)
                Achievements.Add(false);
        }
    }
}