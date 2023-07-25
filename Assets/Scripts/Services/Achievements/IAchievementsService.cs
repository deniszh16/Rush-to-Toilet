namespace Services.Achievements
{
    public interface IAchievementsService
    {
        public bool CheckAchievement(int number);
        public void StartAchievementCheck();
        public void UnlockAchievement(int number);
    }
}