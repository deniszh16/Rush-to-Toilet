using Services.PersistentProgress;
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
        [SerializeField] private Image _buttonImage;
        [SerializeField] private Sprite _activeButton;
        
        [Header("Номер уровня")]
        [SerializeField] private int _number;

        private ISceneLoaderService _sceneLoaderService;
        private IPersistentProgressService _progressService;

        [Inject]
        private void Construct(ISceneLoaderService sceneLoaderService, IPersistentProgressService progressService)
        {
            _sceneLoaderService = sceneLoaderService;
            _progressService = progressService;
        }

        private void Awake()
        {
            if (_progressService.GetUserProgress.Progress >= _number)
            {
                _buttonImage.sprite = _activeButton;
                _button.interactable = true;
            }
            
            _button.onClick.AddListener(GoToLevel);
        }

        private void GoToLevel() =>
            _sceneLoaderService.LoadLevelAsync(_number);

        private void OnDestroy() =>
            _button.onClick.RemoveListener(GoToLevel);
    }
}