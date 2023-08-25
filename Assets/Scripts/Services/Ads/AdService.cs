using System;
using AppodealStack.Monetization.Api;
using AppodealStack.Monetization.Common;
using UnityEngine;

namespace Services.Ads
{
    public class AdService : IAdService
    {
        private const string AppKey = "45af83962ea33ae0f7573cd3afcbd7232daafb7e08885f87";

        public AdService()
        {
            Appodeal.MuteVideosIfCallsMuted(true);
            Initialization();
        }

        private void Initialization()
        {
            int adTypes = AppodealAdType.Interstitial | AppodealAdType.RewardedVideo;
            AppodealCallbacks.Sdk.OnInitialized += OnInitializationFinished;
            Appodeal.Initialize(AppKey, adTypes);
        }
        
        private void OnInitializationFinished(object sender, SdkInitializedEventArgs e) =>
            Debug.Log("Advertising initialized!");

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