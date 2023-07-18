using Services.PersistentProgress;
using Services.SceneLoader;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Logic.UI
{
    public class LastLevelOpenButton : MonoBehaviour
    {
        [Header("Кнопка открытия")]
        [SerializeField] private Button _button;
        
        private ISceneLoaderService _sceneLoaderService;
        private IPersistentProgressService _progressService;
        
        [Inject]
        private void Construct(ISceneLoaderService sceneLoaderService, IPersistentProgressService progressService)
        {
            _sceneLoaderService = sceneLoaderService;
            _progressService = progressService;
        }
        
        private void Awake() =>
            _button.onClick.AddListener(GoToLevel);

        private void GoToLevel()
        {
            int number = _progressService.UserProgress.Progress;
            _sceneLoaderService.LoadLevelAsync(number);
        }

        private void OnDestroy() =>
            _button.onClick.RemoveListener(GoToLevel);
    }
}