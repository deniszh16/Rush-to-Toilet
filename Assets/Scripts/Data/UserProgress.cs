using System;

namespace Data
{
    [Serializable]
    public class UserProgress
    {
        public int Score;
        public int Progress;

        public SoundData SoundData;
        public LanguageData LanguageData;

        public event Action ScoreChanged;

        public UserProgress()
        {
            Progress = 1;
            SoundData = new SoundData();
            LanguageData = new LanguageData();
        }

        public void ChangeScore(int value)
        {
            Score += value;
            ScoreChanged?.Invoke();
        }
    }
}