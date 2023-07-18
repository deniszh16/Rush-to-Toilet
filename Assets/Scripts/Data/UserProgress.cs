using System;

namespace Data
{
    [Serializable]
    public class UserProgress
    {
        public int Score;

        public event Action ScoreChanged;

        public UserProgress()
        {
        }

        public void ChangeScore(int value)
        {
            Score += value;
            ScoreChanged?.Invoke();
        }
    }
}