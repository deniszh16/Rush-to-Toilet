namespace Services.Ads
{
    public interface IAdService
    {
        public void Initialization();
        public void ShowRewardedAd();
        public void ShowInterstitialAd();
    }
}