using Services.Ads;
using UnityEngine.UI;
using UnityEngine;
using Zenject;

namespace Logic.UI
{
    public class ViewRewardAds : MonoBehaviour
    {
        [Header("Кнопка просмотра")]
        [SerializeField] private Button _button;
        
        [Header("Тип подсказки")]
        [SerializeField] private RewardTypes _rewardTypes;
        
        [Header("Компонент награды")]
        [SerializeField] private AdsAward _adsAward;
        
        private IAdService _adService;
        
        [Inject]
        private void Construct(IAdService adService) =>
            _adService = adService;

        private void OnEnable()
        {
            _button.onClick.AddListener(_adService.ShowRewardedAd);
            _button.onClick.AddListener(ChangeRewardType);
        }

        private void ChangeRewardType() =>
            _adsAward.ChangeRewardType(_rewardTypes);

        private void OnDisable()
        {
            _button.onClick.RemoveListener(_adService.ShowRewardedAd);
            _button.onClick.RemoveListener(ChangeRewardType);
        }
    }
}