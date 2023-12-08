using System.Collections;
using Services.PersistentProgress;
using Services.Achievements;
using Services.GooglePlay;
using Services.SaveLoad;
using Logic.Sounds;
using Services.Ads;
using UnityEngine;
using Zenject;

namespace Logic.Levels
{
    public class LevelTasks : MonoBehaviour
    {
        [Header("Номер уровня")]
        [SerializeField] private int _number;
        
        [Header("Уровень лабиринт")]
        [SerializeField] private bool _labyrinth;

        [Header("Персонажи уровня")]
        [SerializeField] private Character[] _characters;
        
        [Header("Персонажи уровня")]
        [SerializeField] private Toilet[] _toilets;

        [Header("Панель победы")]
        [SerializeField] private GameObject _victoryPanel;
        [SerializeField] private ParticleSystem[] _confetti;
        
        [Header("Панель проигрыша")]
        [SerializeField] private GameObject _losingPanel;

        [Header("Компоненты звука")]
        [SerializeField] private PlayingSound _playingSound;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _victorySound;
        [SerializeField] private AudioClip _losingSound;

        public int Number => _number;

        private int _linesDrawn;
        private int _crowdedToilets;
        private bool _losing;
        
        private IPersistentProgressService _progressService;
        private ISaveLoadService _saveLoadService;
        private IAchievementsService _achievementsService;
        private IGooglePlayService _googlePlayService;
        private IAdService _adService;

        [Inject]
        private void Construct(IPersistentProgressService progressService, ISaveLoadService saveLoadService,
            IAchievementsService achievementsService, IGooglePlayService googlePlayService, IAdService adService)
        {
            _progressService = progressService;
            _saveLoadService = saveLoadService;
            _achievementsService = achievementsService;
            _googlePlayService = googlePlayService;
            _adService = adService;
        }

        private void Start()
        {
            foreach (Toilet toilet in _toilets)
                toilet.ToiletIsFull += AddFilledToilet;

            foreach (Character character in _characters)
                character.Faced += ActivateLosing;
        }

        public void AddLine()
        {
            _linesDrawn++;

            if (_linesDrawn >= _characters.Length)
            {
                foreach (Character character in _characters)
                    character.StartMovement();
            }
        }

        private void AddFilledToilet()
        {
            _crowdedToilets++;

            if (_crowdedToilets >= _toilets.Length)
                _ = StartCoroutine(ShowVictoryPanel());
        }

        private IEnumerator ShowVictoryPanel()
        {
            yield return new WaitForSeconds(2.5f);
            _victoryPanel.SetActive(true);
            _audioSource.clip = _victorySound;
            _playingSound.PlaySound();
            
            foreach (ParticleSystem effect in _confetti)
            {
                effect.gameObject.SetActive(true);
                effect.Play();
            }
            
            UpdateProgress();
            ShowAds();
        }

        private void UpdateProgress()
        {
            int points = 0;
            
            foreach (Character character in _characters)
                points += character.DrawWithMouse.AmountOfPoints;
            
            int score = _number * 100 - points / 10;
            if (score > 0) _progressService.GetUserProgress.Score += score;
            
            if (_progressService.GetUserProgress.Progress <= _number) 
                _progressService.GetUserProgress.Progress += 1;

            if (_labyrinth) _progressService.GetUserProgress.LabyrinthLevels += 1;
            if (_characters.Length >= 3) _progressService.GetUserProgress.LevelsWithThreeCharacters += 1;
            _progressService.GetUserProgress.DrawnLines += _linesDrawn;
            
            _progressService.GetUserProgress.AddAttempt(_number, victory: true);
            _saveLoadService.SaveProgress();
            _achievementsService.StartAchievementCheck();
        }

        private void ActivateLosing(GameObject reasonLosing)
        {
            if (_losing) return;
            
            _ = StartCoroutine(ShowLosingPanel());
            _losing = true;

            if (reasonLosing.GetComponent<Character>())
            {
                _achievementsService.UnlockAchievement(2);
                _googlePlayService.UnlockAchievement(GPGSIds.achievement_forehead_to_forehead);
            }

            if (reasonLosing.GetComponent<Poop>())
            {
                _achievementsService.UnlockAchievement(5);
                _googlePlayService.UnlockAchievement(GPGSIds.achievement_a_bad_odor);
            }
        }

        private IEnumerator ShowLosingPanel()
        {
            yield return new WaitForSeconds(1.5f);
            _losingPanel.SetActive(true);
            _audioSource.clip = _losingSound;
            _playingSound.PlaySound();

            _progressService.GetUserProgress.Score += 1;
            _progressService.GetUserProgress.AddAttempt(_number, victory: false);
            _progressService.GetUserProgress.DrawnLines += _linesDrawn;
            _saveLoadService.SaveProgress();
            ShowAds();
        }

        private void ShowAds()
        {
            AdsCounter.Counter++;
            if (AdsCounter.Counter % 4 == 0)
                _adService.ShowInterstitialAd();
        }

        private void OnDestroy()
        {
            foreach (Toilet toilet in _toilets)
                toilet.ToiletIsFull -= AddFilledToilet;
            
            foreach (Character character in _characters)
                character.Faced -= ActivateLosing;
        }
    }
}