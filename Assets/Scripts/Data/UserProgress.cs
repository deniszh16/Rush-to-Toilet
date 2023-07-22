using System;
using System.Collections.Generic;

namespace Data
{
    [Serializable]
    public class UserProgress
    {
        public int Score;
        public int Progress;

        public List<int> Attempts;
        
        public int Hints;
        public int HintsUsed;

        public SoundData SoundData;
        public LanguageData LanguageData;

        public event Action ScoreChanged;
        public event Action HintsChanged;

        public UserProgress()
        {
            Progress = 1;
            Hints = 2;
            
            Attempts = new List<int>();
            SoundData = new SoundData();
            LanguageData = new LanguageData();
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

            if (levelNumber < Progress && victory)
                return;
            
            Attempts[levelNumber - 1] += 1;
        }
    }
}