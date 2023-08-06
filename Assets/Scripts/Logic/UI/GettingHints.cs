using Services.Ads;
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
        private IAdService _adService;

        [Inject]
        private void Construct(IPersistentProgressService progressService, ISaveLoadService saveLoadService, IAdService adService)
        {
            _progressService = progressService;
            _saveLoadService = saveLoadService;
            _adService = adService;
        }

        private void Awake()
        {
            _button.onClick.AddListener(ShowRewardedAds);
            _adService.RewardedVideoFinished += AddHints;
        }

        private void ShowRewardedAds() =>
            _adService.ShowRewardedAd();

        private void AddHints()
        {
            _progressService.UserProgress.ChangeHints(2);
            _saveLoadService.SaveProgress();
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(ShowRewardedAds);
            _adService.RewardedVideoFinished -= AddHints;
        }
    }
}