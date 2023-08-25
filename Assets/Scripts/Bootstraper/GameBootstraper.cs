using Services.PersistentProgress;
using Services.Localization;
using Services.SceneLoader;
using Services.SaveLoad;
using Services.Sound;
using UnityEngine;
using Zenject;
using Data;

namespace Bootstraper
{
    public class GameBootstraper : MonoBehaviour
    {
        private ISceneLoaderService _sceneLoaderService;
        private IPersistentProgressService _progressService;
        private ISaveLoadService _saveLoadService;
        private ISoundService _soundService;
        private ILocalizationService _localizationService;
        
        [Inject]
        private void Construct(ISceneLoaderService sceneLoaderService, IPersistentProgressService progressService,
            ISaveLoadService saveLoadService, ISoundService soundService, ILocalizationService localizationService)
        {
            _sceneLoaderService = sceneLoaderService;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
            _soundService = soundService;
            _localizationService = localizationService;
        }

        private void Awake() =>
            Screen.sleepTimeout = SleepTimeout.NeverSleep;

        private void Start()
        {
            LoadProgressOrInitNew();
            _soundService.SoundActivity = _progressService.UserProgress.SoundData.SoundActivity;
            _soundService.MusicActivity = _progressService.UserProgress.SoundData.MusicActivity;
            _localizationService.SetCurrentLanguage(_progressService.UserProgress.LanguageData.Language);
            _sceneLoaderService.LoadSceneAsync(Scenes.MainMenu, screensaver: false, delay: 1.5f);
        }

        private void LoadProgressOrInitNew()
        {
            _progressService.UserProgress =
                _saveLoadService.LoadProgress() ?? new UserProgress();
        }
    }
}