using Services.Ads;
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
            _button.onClick.AddListener(SkipLevel);
            _adService.RewardedVideoFinished += UpdateProgress;
        }

        private void SkipLevel() =>
            _adService.ShowRewardedAd();

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
            _adService.RewardedVideoFinished -= UpdateProgress;
        }
    }
}