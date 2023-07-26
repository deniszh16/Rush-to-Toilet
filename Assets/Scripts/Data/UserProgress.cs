using System;
using System.Collections.Generic;

namespace Data
{
    [Serializable]
    public class UserProgress
    {
        public int Score;
        public int Progress;

        public int LabyrinthLevels;
        public int LevelsWithThreeCharacters;

        public List<int> Attempts;
        
        public int Hints;
        public int HintsUsed;

        public SoundData SoundData;
        public LanguageData LanguageData;
        public AchievementsData AchievementsData;

        public event Action ScoreChanged;
        public event Action HintsChanged;

        public UserProgress()
        {
            Progress = 1;
            Hints = 2;
            
            Attempts = new List<int>();
            SoundData = new SoundData();
            LanguageData = new LanguageData();
            AchievementsData = new AchievementsData();
        }

        public void ChangeScore(int value)
        {
            Score += value;
            ScoreChanged?.Invoke();
        }
        
        public void ChangeHints(int value)
        {
            Hints += value;
            HintsChanged?.Invoke();
        }

        public void AddAttempt(int levelNumber, bool victory)
        {
            if (Attempts.Count < levelNumber)
            {
                Attempts.Add(1);
                return;
            }
            
            if (levelNumber >= Progress && victory == false)
                Attempts[levelNumber - 1] += 1;
        }

        public int GetLevelsPassedOnFirstTry()
        {
            int quantity = 0;
            foreach (int attempt in Attempts)
            {
                if (attempt == 1)
                    quantity++;
            }

            return quantity;
        }
    }
}