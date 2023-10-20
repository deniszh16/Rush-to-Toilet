using AppodealStack.Monetization.Api;
using AppodealStack.Monetization.Common;

namespace Services.Ads
{
    public class AdService : IAdService
    {
        private const string AppKey = "45af83962ea33ae0f7573cd3afcbd7232daafb7e08885f87";

        public void Initialization()
        {
            Appodeal.MuteVideosIfCallsMuted(true);
            int adTypes = AppodealAdType.Interstitial | AppodealAdType.RewardedVideo;
            AppodealCallbacks.Sdk.OnInitialized += OnInitializationFinished;
            Appodeal.Initialize(AppKey, adTypes);
        }

        private void OnInitializationFinished(object sender, SdkInitializedEventArgs e)
        {
        }

        public void ShowInterstitialAd()
        {
            if (Appodeal.IsLoaded(AppodealAdType.Interstitial))
                Appodeal.Show(AppodealShowStyle.Interstitial);
        }

        public void ShowRewardedAd()
        {
            if (Appodeal.IsLoaded(AppodealAdType.RewardedVideo))
                Appodeal.Show(AppodealShowStyle.RewardedVideo);
        }
    }
}