using Services.PersistentProgress;
using Services.SaveLoad;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Logic.UI
{
    public class GettingHints : MonoBehaviour
    {
        [Header("Кнопка получения")]
        [SerializeField] private Button _button;

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
            _button.onClick.AddListener(ShowRewardedAds);
        }

        private void ShowRewardedAds()
        {
            
        }

        private void AddHints()
        {
            _progressService.UserProgress.ChangeHints(2);
            _saveLoadService.SaveProgress();
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(ShowRewardedAds);
        }
    }
}