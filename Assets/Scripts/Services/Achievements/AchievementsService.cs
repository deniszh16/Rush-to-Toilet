using Logic.UI;
using Services.GooglePlay;
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

        private IGooglePlayService _googlePlayService;
        private IPersistentProgressService _persistentProgress;
        private ISaveLoadService _saveLoadService;

        [Inject]
        private void Construct(IGooglePlayService googlePlayService, IPersistentProgressService persistentProgress, ISaveLoadService saveLoadService)
        {
            _googlePlayService = googlePlayService;
            _persistentProgress = persistentProgress;
            _saveLoadService = saveLoadService;
        }

        public bool CheckAchievement(int number) =>
            _persistentProgress.GetUserProgress.AchievementsData.Achievements[number - 1];

        public void StartAchievementCheck()
        {
            if (CheckAchievement(number: 1) == false && _persistentProgress.GetUserProgress.Progress > 1)
            {
                UnlockAchievement(number: 1);
                _googlePlayService.UnlockAchievement(GPGSIds.achievement_wheres_my_toilet);
            }

            if (CheckAchievement(number: 3) == false && _persistentProgress.GetUserProgress.HintsUsed > 0)
            {
                UnlockAchievement(number: 3);
                _googlePlayService.UnlockAchievement(GPGSIds.achievement_need_help);
            }
            
            if (CheckAchievement(number: 4) == false && _persistentProgress.GetUserProgress.Progress > 10)
            {
                UnlockAchievement(number: 4);
                _googlePlayService.UnlockAchievement(GPGSIds.achievement_good_start);
            }
            
            if (CheckAchievement(number: 6) == false && _persistentProgress.GetUserProgress.LabyrinthLevels >= 15)
            {
                UnlockAchievement(number: 6);
                _googlePlayService.UnlockAchievement(GPGSIds.achievement_labyrinths_master);
            }
            
            if (CheckAchievement(number: 7) == false && _persistentProgress.GetUserProgress.Hints > 2)
            {
                UnlockAchievement(number: 7);
                _googlePlayService.UnlockAchievement(GPGSIds.achievement_more_hints);
            }
            
            if (CheckAchievement(number: 8) == false && _persistentProgress.GetUserProgress.LevelsWithThreeCharacters >= 15)
            {
                UnlockAchievement(number: 8);
                _googlePlayService.UnlockAchievement(GPGSIds.achievement_three_toilets);
            }
            
            if (CheckAchievement(number: 9) == false && _persistentProgress.GetUserProgress.Progress > 50)
            {
                UnlockAchievement(number: 9);
                _googlePlayService.UnlockAchievement(GPGSIds.achievement_experienced_player);
            }
            
            if (CheckAchievement(number: 11) == false && _persistentProgress.GetUserProgress.DrawnLines >= 70)
            {
                UnlockAchievement(number: 11);
                _googlePlayService.UnlockAchievement(GPGSIds.achievement_solid_lines);
            }
            
            if (CheckAchievement(number: 12) == false && _persistentProgress.GetUserProgress.DrawnLines >= 120)
            {
                UnlockAchievement(number: 12);
                _googlePlayService.UnlockAchievement(GPGSIds.achievement_artist);
            }
        }

        public void UnlockAchievement(int number)
        {
            if (_persistentProgress.GetUserProgress.AchievementsData.Achievements[number - 1] != true)
            {
                ShowAchievementBar(number);
                _persistentProgress.GetUserProgress.AchievementsData.Achievements[number - 1] = true;
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