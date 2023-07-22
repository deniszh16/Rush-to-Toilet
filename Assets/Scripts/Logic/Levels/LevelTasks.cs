using System.Collections;
using Logic.Sounds;
using Services.PersistentProgress;
using Services.SaveLoad;
using UnityEngine;
using Zenject;

namespace Logic.Levels
{
    public class LevelTasks : MonoBehaviour
    {
        [Header("Номер уровня")]
        [SerializeField] private int _number;

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

        private IPersistentProgressService _progressService;
        private ISaveLoadService _saveLoadService;

        [Inject]
        private void Construct(IPersistentProgressService progressService, ISaveLoadService saveLoadService)
        {
            _progressService = progressService;
            _saveLoadService = saveLoadService;
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
        }

        private void UpdateProgress()
        {
            int points = 0;
            
            foreach (Character character in _characters)
                points += character.DrawWithMouse.AmountOfPoints;
            
            int score = _number * 100 - points / 10;
            if (score > 0) _progressService.UserProgress.Score += score;
            
            if (_progressService.UserProgress.Progress <= _number) 
                _progressService.UserProgress.Progress += 1;
            
            _progressService.UserProgress.AddAttempt(_number, victory: true);
            _saveLoadService.SaveProgress();
        }

        private void ActivateLosing() =>
            _ = StartCoroutine(ShowLosingPanel());

        private IEnumerator ShowLosingPanel()
        {
            yield return new WaitForSeconds(1.5f);
            _losingPanel.SetActive(true);
            _audioSource.clip = _losingSound;
            _playingSound.PlaySound();
            
            _progressService.UserProgress.Score += 1;
            _progressService.UserProgress.AddAttempt(_number, victory: false);
            _saveLoadService.SaveProgress();
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