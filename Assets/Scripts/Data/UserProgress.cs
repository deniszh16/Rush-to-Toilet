using System;

namespace Data
{
    [Serializable]
    public class UserProgress
    {
        public int Score;
        public int Progress;

        public LanguageData LanguageData;
        
        public event Action ScoreChanged;

        public UserProgress()
        {
            Progress = 1;
            LanguageData = new LanguageData();
        }

        public void ChangeScore(int value)
        {
            Score += value;
            ScoreChanged?.Invoke();
        }
    }
}