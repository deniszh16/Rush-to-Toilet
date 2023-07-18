using Services.SceneLoader;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Logic.UI
{
    public class LevelOpenButton : MonoBehaviour
    {
        [Header("Кнопка открытия")]
        [SerializeField] private Button _button;
        [SerializeField] private int _number;
        
        private ISceneLoaderService _sceneLoaderService;

        [Inject]
        private void Construct(ISceneLoaderService sceneLoaderService) =>
            _sceneLoaderService = sceneLoaderService;

        private void Awake() =>
            _button.onClick.AddListener(GoToLevel);

        private void GoToLevel() =>
            _sceneLoaderService.LoadLevelAsync(_number);

        private void OnDestroy() =>
            _button.onClick.RemoveListener(GoToLevel);
    }
}