using Services.PersistentProgress;
using Services.SaveLoad;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Logic.UI
{
    public class LevelSkip : MonoBehaviour
    {
        [Header("Кнопка получения")]
        [SerializeField] private Button _button;
        
        [Header("Кнопки экрана")]
        [SerializeField] private GameObject _nextButton;
        [SerializeField] private GameObject _restartButton;

        private IPersistentProgressService _progressService;
        private ISaveLoadService _saveLoadService;

        [Inject]
        private void Construct(IPersistentProgressService progressService, ISaveLoadService saveLoadService)
        {
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        private void Awake()
        {
            _button.onClick.AddListener(SkipLevel);
        }

        private void SkipLevel()
        {
            
        }

        private void UpdateProgress()
        {
            _nextButton.SetActive(true);
            _restartButton.SetActive(false);
            _progressService.UserProgress.Progress += 1;
            _saveLoadService.SaveProgress();
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(SkipLevel);
        }
    }
}