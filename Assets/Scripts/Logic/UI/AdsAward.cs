using AppodealStack.Monetization.Common;
using PimDeWitte.UnityMainThreadDispatcher;
using Services.PersistentProgress;
using Services.SaveLoad;
using Services.Ads;
using UnityEngine;
using Zenject;

namespace Logic.UI
{
    public class AdsAward : MonoBehaviour
    {
        [Header("Кнопка далее")]
        [SerializeField] private GameObject _nextButton;
        
        private RewardTypes _rewardTypes;
        
        private IPersistentProgressService _progressService;
        private ISaveLoadService _saveLoadService;
        
        [Inject]
        private void Construct(IPersistentProgressService progressService, ISaveLoadService saveLoadService,
            IAdService adService)
        {
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        private void Awake()
        {
            _rewardTypes = RewardTypes.Hints;
            AppodealCallbacks.RewardedVideo.OnClosed += OnRewardedVideoClosed;
            //AppodealCallbacks.RewardedVideo.OnFinished += OnRewardedVideoFinished;
        }
        
        private void OnRewardedVideoClosed(object sender, RewardedVideoClosedEventArgs e) =>
            UnityMainThreadDispatcher.Instance().Enqueue(ChooseReward);

        private void OnRewardedVideoFinished(object sender, RewardedVideoFinishedEventArgs e) =>
            UnityMainThreadDispatcher.Instance().Enqueue(ChooseReward);

        private void ChooseReward()
        {
            if (_rewardTypes == RewardTypes.Hints)
            {
                _progressService.UserProgress.ChangeHints(2);
                _saveLoadService.SaveProgress();
            }
            else
            {
                _nextButton.SetActive(true);
                _progressService.UserProgress.Progress += 1;
                _saveLoadService.SaveProgress();
            }
        }

        public void ChangeRewardType(RewardTypes rewardTypes) =>
            _rewardTypes = rewardTypes;

        private void OnDestroy()
        {
            AppodealCallbacks.RewardedVideo.OnClosed -= OnRewardedVideoClosed;
            //AppodealCallbacks.RewardedVideo.OnFinished -= OnRewardedVideoFinished;
        }
    }
}