using Logic.Levels;
using Services.SceneLoader;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Logic.UI
{
    public class LevelRestart : MonoBehaviour
    {
        [Header("Кнопка рестарта")]
        [SerializeField] private Button _button;
        
        [Header("Компонент уровня")]
        [SerializeField] private LevelTasks _levelTasks;
        
        private ISceneLoaderService _sceneLoaderService;

        [Inject]
        private void Construct(ISceneLoaderService sceneLoaderService) =>
            _sceneLoaderService = sceneLoaderService;

        private void Awake() =>
            _button.onClick.AddListener(RestartCurrentLevel);

        private void RestartCurrentLevel() =>
            _sceneLoaderService.LoadLevelAsync(_levelTasks.Number);

        private void OnDestroy() =>
            _button.onClick.RemoveListener(RestartCurrentLevel);
    }
}