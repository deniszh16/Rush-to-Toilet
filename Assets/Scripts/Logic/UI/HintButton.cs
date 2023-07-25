using Services.PersistentProgress;
using Services.SaveLoad;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Logic.UI
{
    public class HintButton : MonoBehaviour
    {
        [Header("Кнопка подсказки")]
        [SerializeField] private Button _button;
        
        [Header("Панель подсказки")]
        [SerializeField] private GameObject _hintsPanel;
        
        [Header("Линии подсказки")]
        [SerializeField] private GameObject _hints;
        
        private IPersistentProgressService _progressService;
        private ISaveLoadService _saveLoadService;

        [Inject]
        private void Construct(IPersistentProgressService progressService, ISaveLoadService saveLoadService)
        {
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        private void Awake() =>
            _button.onClick.AddListener(UseHint);

        private void UseHint()
        {
            if (_progressService.UserProgress.Hints > 0)
            {
                _hints.SetActive(true);
                _progressService.UserProgress.ChangeHints(value: -1);
                _progressService.UserProgress.HintsUsed += 1;
                _saveLoadService.SaveProgress();
            }
            else
            {
                _hintsPanel.SetActive(true);
            }
        }

        private void OnDestroy() =>
            _button.onClick.RemoveListener(UseHint);
    }
}