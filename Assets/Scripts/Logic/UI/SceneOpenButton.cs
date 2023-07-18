using Services.SceneLoader;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Logic.UI
{
    public class SceneOpenButton : MonoBehaviour
    { 
        [Header("Кнопка открытия")]
        [SerializeField] private Button _button;
        [SerializeField] private Scenes _scene;
        
        private ISceneLoaderService _sceneLoaderService;

        [Inject]
        private void Construct(ISceneLoaderService sceneLoaderService) =>
            _sceneLoaderService = sceneLoaderService;

        private void Awake() =>
            _button.onClick.AddListener(GoToScene);

        private void GoToScene() =>
            _sceneLoaderService.LoadSceneAsync(_scene, screensaver: true, delay: 0f);
        
        private void OnDestroy() =>
            _button.onClick.RemoveListener(GoToScene);
    }
}