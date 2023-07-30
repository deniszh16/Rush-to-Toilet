using Logic.UI;
using Services.PersistentProgress;
using Services.SaveLoad;
using UnityEngine;
using Zenject;

namespace Services.Achievements
{
    public class AchievementsService : MonoBehaviour, IAchievementsService
    {
        [Header("Плашка достижения")]
        [SerializeField] private Animator _animator;
        
        private static readonly int DepartureAnimation = Animator.StringToHash("Appearance");
        
        [Header("Перевод текста")]
        [SerializeField] private TextTranslation _textTranslation;

        private IPersistentProgressService _persistentProgress;
        private ISaveLoadService _saveLoadService;

        [Inject]
        private void Construct(IPersistentProgressService persistentProgress, ISaveLoadService saveLoadService)
        {
            _persistentProgress = persistentProgress;
            _saveLoadService = saveLoadService;
        }

        public bool CheckAchievement(int number) =>
            _persistentProgress.UserProgress.AchievementsData.Achievements[number - 1];

        public void StartAchievementCheck()
        {
            if (CheckAchievement(number: 1) == false && _persistentProgress.UserProgress.Progress > 1)
            {
                UnlockAchievement(number: 1);
            }

            if (CheckAchievement(number: 3) == false && _persistentProgress.UserProgress.HintsUsed > 0)
            {
                UnlockAchievement(number: 3);
            }
            
            if (CheckAchievement(number: 4) == false && _persistentProgress.UserProgress.Progress > 10)
            {
                UnlockAchievement(number: 4);
            }
            
            if (CheckAchievement(number: 6) == false && _persistentProgress.UserProgress.LabyrinthLevels >= 15)
            {
                UnlockAchievement(number: 6);
            }
            
            if (CheckAchievement(number: 7) == false && _persistentProgress.UserProgress.Hints > 2)
            {
                UnlockAchievement(number: 7);
            }
            
            if (CheckAchievement(number: 8) == false && _persistentProgress.UserProgress.LevelsWithThreeCharacters >= 15)
            {
                UnlockAchievement(number: 8);
            }
            
            if (CheckAchievement(number: 9) == false && _persistentProgress.UserProgress.Progress > 50)
            {
                UnlockAchievement(number: 9);
            }
            
            if (CheckAchievement(number: 11) == false && _persistentProgress.UserProgress.DrawnLines >= 70)
            {
                UnlockAchievement(number: 11);
            }
            
            if (CheckAchievement(number: 12) == false && _persistentProgress.UserProgress.DrawnLines >= 120)
            {
                UnlockAchievement(number: 12);
            }
        }

        public void UnlockAchievement(int number)
        {
            if (_persistentProgress.UserProgress.AchievementsData.Achievements[number - 1] != true)
            {
                ShowAchievementBar(number);
                _persistentProgress.UserProgress.AchievementsData.Achievements[number - 1] = true;
                _saveLoadService.SaveProgress();
            }
        }

        private void ShowAchievementBar(int number)
        {
            _textTranslation.ChangeKey("achievement_" + number);
            _animator.Play(DepartureAnimation);
        }
    }
}