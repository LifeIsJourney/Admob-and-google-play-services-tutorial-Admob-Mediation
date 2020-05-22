using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

[DefaultExecutionOrder(-50)]
public class GoogleAds :MonoBehaviour
{
    public bool publishing=true;

    private InterstitialAd interstitial;

    private RewardedAd rewardedAd;

    private BannerView bannerView;

    public AdPosition adPosition = AdPosition.Top;

    public string InterstitialAdsStringAndroidPublishing = "";
    public string RewardedAdsStringAndroidPublishing = "";

    string InterstitialAdsStringAndroid = "ca-app-pub-3940256099942544/1033173712";
   
    string InterstitialAdsStringIos = "ca-app-pub-3940256099942544/4411468910";

    string RewardedAdsStringAndroid = "ca-app-pub-3940256099942544/5224354917";
    string RewardedAdsStringIos = "ca-app-pub-3940256099942544/1712485313";
   
     string BannerAdStringAndroid = "ca-app-pub-3940256099942544/6300978111";
    string BannerAdStringIos = "ca-app-pub-3940256099942544/2934735716";

    public Action AdsStarted, AdsClosed, AdsCompleteGiveReward;

   private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });

        RequestInterstitial();
        RequestReward();
        RequestBanner();
        
    }
    private void RequestInterstitial()
    {
#if UNITY_ANDROID
        string adUnitId = publishing?InterstitialAdsStringAndroidPublishing: InterstitialAdsStringAndroid;
#elif UNITY_IPHONE
        string adUnitId = InterstitialAdsStringIos ;
#else
        string adUnitId = "unexpected_platform";
#endif

        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);
        #region Ads Event
        // Called when an ad request has successfully loaded.
        this.interstitial.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        this.interstitial.OnAdOpening += HandleOnAdOpened;
        // Called when the ad is closed.
        this.interstitial.OnAdClosed += HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        this.interstitial.OnAdLeavingApplication += HandleOnAdLeavingApplication;
        #endregion
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);
    }

    private void RequestReward()
    {
        string adUnitId;
#if UNITY_ANDROID
        adUnitId = publishing?RewardedAdsStringAndroidPublishing: RewardedAdsStringAndroid;
#elif UNITY_IPHONE
            adUnitId = RewardedAdsStringIos;
#else
            adUnitId = "unexpected_platform";
#endif

        this.rewardedAd = new RewardedAd(adUnitId);
        #region Ads Event
        // Called when an ad request has successfully loaded.
        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        // Called when an ad request failed to load.
        this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // Called when an ad is shown.
        this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        // Called when an ad request failed to show.
        this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        // Called when the user should be rewarded for interacting with the ad.
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;
        #endregion
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);

    }

    private void RequestBanner()
    {

#if UNITY_ANDROID
        string adUnitId = BannerAdStringAndroid;
#elif UNITY_IPHONE
            string adUnitId = BannerAdStringIos;
#else
            string adUnitId = "unexpected_platform";
#endif

        this.bannerView = new BannerView(adUnitId, AdSize.SmartBanner, adPosition);
        #region Ads Event
        // Called when an ad request has successfully loaded.
        this.bannerView.OnAdLoaded += this.HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.bannerView.OnAdFailedToLoad += this.HandleOnAdFailedToLoad;
        // Called when an ad is clicked.
        this.bannerView.OnAdOpening += this.HandleOnAdOpened;
        // Called when the user returned from the app after an ad click.
        this.bannerView.OnAdClosed += this.HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        this.bannerView.OnAdLeavingApplication += this.HandleOnAdLeavingApplication;
        #endregion
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerView.LoadAd(request);
    }

    public void ShowInterstitialAd()
    {
        if (interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }
        else { RequestInterstitial(); }
    }

    public void ShowRewadedAd()
    {
        if (rewardedAd.IsLoaded())
        {
            this.rewardedAd.Show();
        }
        else { RequestReward(); }
    }

    #region Interstitial Event
    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
       print("HandleAdLoaded event received");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
       print("HandleFailedToReceiveAd event received with message: "
                            + args.Message);
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
       print("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
       print("HandleAdClosed event received");
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
       print("HandleAdLeavingApplication event received");
    }

    #endregion

    #region Reward Event
    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
      print("HandleRewardedAdLoaded event received");
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
    {
       print(
            "HandleRewardedAdFailedToLoad event received with message: "
                             + args.Message);
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
       print("HandleRewardedAdOpening event received"+ sender);
        if (AdsStarted != null) { AdsStarted.Invoke();}
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
       print(
            "HandleRewardedAdFailedToShow event received with message: "
                             + args.Message);
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        //  StartCoroutine(HelpCour());
        if (AdsClosed != null) { AdsClosed.Invoke(); }
        print("HandleRewardedAdClosed event received");
        // MobileConsole.ShowToast("HandleRewardedAdClosed event received");
        RequestReward();
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        print(
            "HandleRewardedAdRewarded event received for "
                        + amount.ToString() + " " + type);
        if (AdsCompleteGiveReward != null) { AdsCompleteGiveReward.Invoke(); }
    }

    IEnumerator HelpCour()
    {
        yield return new WaitForSeconds(0.1f);
       

    }
    #endregion


}
