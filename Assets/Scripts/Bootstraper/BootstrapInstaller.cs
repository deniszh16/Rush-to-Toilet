using PimDeWitte.UnityMainThreadDispatcher;
using Services.PersistentProgress;
using Services.Achievements;
using Services.Localization;
using Services.GooglePlay;
using Services.SceneLoader;
using Services.SaveLoad;
using Services.Sound;
using Services.Ads;
using UnityEngine;
using Zenject;

namespace Bootstraper
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private SceneLoaderService _sceneLoader;
        [SerializeField] private SoundService _soundService;
        [SerializeField] private AchievementsService _achievementsService;
        [SerializeField] private UnityMainThreadDispatcher _dispatcher;
        
        private IPersistentProgressService _progressService;
        private ISaveLoadService _saveLoadService;
        
        public override void InstallBindings()
        {
            BindPersistentProgress();
            BindSaveLoadService();
            BindLocalizationService();
            BindSceneLoader();
            BindSoundService();
            BindGooglePlayService();
            BindAchievementsService();
            BindAdService();
            BindUnityMainThreadDispatcher();
        }
        
        private void BindPersistentProgress()
        {
            _progressService = new PersistentProgressService();
            Container.BindInstance(_progressService).AsSingle();
        }
        
        private void BindSaveLoadService()
        {
            _saveLoadService = new SaveLoadService(_progressService);
            Container.BindInstance(_saveLoadService).AsSingle();
        }
        
        private void BindSceneLoader()
        {
            SceneLoaderService sceneLoader = Container.InstantiatePrefabForComponent<SceneLoaderService>(_sceneLoader);
            Container.Bind<ISceneLoaderService>().To<SceneLoaderService>().FromInstance(sceneLoader).AsSingle();
        }

        private void BindSoundService()
        {
            SoundService soundService = Container.InstantiatePrefabForComponent<SoundService>(_soundService);
            Container.Bind<ISoundService>().To<SoundService>().FromInstance(soundService).AsSingle();
        }
        
        private void BindLocalizationService()
        {
            ILocalizationService localizationService = new LocalizationService(_progressService, _saveLoadService);
            localizationService.LoadTranslations();
            Container.BindInstance(localizationService).AsSingle();
        }
        
        private void BindAchievementsService()
        {
            AchievementsService achievementsService =
                Container.InstantiatePrefabForComponent<AchievementsService>(_achievementsService);
            Container.Bind<IAchievementsService>().To<AchievementsService>().FromInstance(achievementsService).AsSingle();
        }
        
        private void BindGooglePlayService()
        {
            IGooglePlayService googlePlayService = new GooglePlayService();
            Container.BindInstance(googlePlayService).AsSingle();
        }

        private void BindAdService()
        {
            IAdService adService = new AdService();
            Container.BindInstance(adService).AsSingle();
            adService.Initialization();
        }
        
        private void BindUnityMainThreadDispatcher()
        {
            UnityMainThreadDispatcher dispatcher =
                Container.InstantiatePrefabForComponent<UnityMainThreadDispatcher>(_dispatcher);
            Container.BindInstance(dispatcher).AsSingle();
        }
    }
}