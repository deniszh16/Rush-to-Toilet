using Services.PersistentProgress;
using Services.SceneLoader;
using Services.SaveLoad;
using UnityEngine;
using Zenject;
using Data;
using Services.Localization;

namespace Bootstraper
{
    public class GameBootstraper : MonoBehaviour
    {
        private ISceneLoaderService _sceneLoaderService;
        private IPersistentProgressService _progressService;
        private ISaveLoadService _saveLoadService;
        private ILocalizationService _localizationService;
        
        [Inject]
        private void Construct(ISceneLoaderService sceneLoaderService, IPersistentProgressService progressService,
            ISaveLoadService saveLoadService, ILocalizationService localizationService)
        {
            _sceneLoaderService = sceneLoaderService;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
            _localizationService = localizationService;
        }
        
        private void Start()
        {
            LoadProgressOrInitNew();
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