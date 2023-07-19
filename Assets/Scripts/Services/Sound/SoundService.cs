using System;
using Services.PersistentProgress;
using Services.SaveLoad;
using UnityEngine;
using Zenject;

namespace Services.Sound
{
    public class SoundService : MonoBehaviour, ISoundService
    {
        [Header("Музыкальный компонент")]
        [SerializeField] private AudioSource _audioSource;

        public bool SoundActivity { get; set; }
        public bool MusicActivity { get; set; }
        
        public event Action SoundChanged;
        public event Action MusicChanged;
        
        private IPersistentProgressService _progressService;
        private ISaveLoadService _saveLoadService;

        [Inject]
        private void Construct(IPersistentProgressService progressService, ISaveLoadService saveLoadService)
        {
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }
        
        public void SwitchSound()
        {
            bool activity = _progressService.UserProgress.SoundData.SoundActivity;
            
            SoundActivity = !activity;
            _progressService.UserProgress.SoundData.ChangeActivityOfSounds(SoundActivity);
            _saveLoadService.SaveProgress();
            SoundChanged?.Invoke();
        }

        public void SwitchMusic()
        {
            bool activity = _progressService.UserProgress.SoundData.MusicActivity;

            MusicActivity = !activity;
            _progressService.UserProgress.SoundData.ChangeActivityOfMusic(MusicActivity);
            _saveLoadService.SaveProgress();
            BackgroundMusicActivation();
            MusicChanged?.Invoke();
        }

        public void BackgroundMusicActivation()
        {
            if (MusicActivity && _audioSource.isPlaying == false) _audioSource.Play();
            else _audioSource.Stop();
        }
    }
}