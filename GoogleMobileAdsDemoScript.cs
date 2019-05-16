using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;

public class GoogleMobileAdsDemoScript : MonoBehaviour
{
    private BannerView bannerView;

    private InterstitialAd interstitial;

    private RewardBasedVideoAd rewardBasedVideo;
    //when publishing app 
    public bool publishingApp;
    // Start is called before the first frame update
    void Start()
    {
        if (publishingApp)
        {
#if UNITY_ANDROID
            string appId = "ca-app-pub-1339385848497432~1207556841";
#elif UNITY_IPHONE
            string appId = "ca-app-pub-3940256099942544~1458002511";
#else
            string appId = "unexpected_platform";
#endif

            // Initialize the Google Mobile Ads SDK.
            MobileAds.Initialize(appId);

        }


        //REWARDED ADS

        //REQUEST VIDEO
        this.RequestRewardBasedVideo();
        RequestInterstitial();
        RequestBanner();

       


    }
    private void RequestInterstitial()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/1033173712";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);
        if (publishingApp)
        {
            // Create an empty ad request.
            AdRequest request = new AdRequest.Builder().Build();
            // Load the interstitial with the request.
            this.interstitial.LoadAd(request);

        }
        else
        {
            // Create an empty ad request.
            AdRequest request = new AdRequest.Builder()
                .AddTestDevice("2077ef9a63d2b398840261c8221a0c9b").Build();
            // Load the interstitial with the request.
            this.interstitial.LoadAd(request);
        }


    }
    private void RequestRewardBasedVideo()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
            string adUnitId = "unexpected_platform";
#endif
        rewardBasedVideo = RewardBasedVideoAd.Instance;
        if (publishingApp)
        {
            // Create an empty ad request.
            AdRequest request = new AdRequest.Builder().Build();
            // Load the rewarded video ad with the request.
            this.rewardBasedVideo.LoadAd(request, adUnitId);
        }
        else
        {
            // Create an empty ad request.
            AdRequest request = new AdRequest.Builder()
            .AddTestDevice("2077ef9a63d2b398840261c8221a0c9b").Build();
            // Load the rewarded video ad with the request.
            this.rewardBasedVideo.LoadAd(request, adUnitId);
        }



    }
    private void RequestBanner()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/6300978111";
#elif UNITY_IPHONE
      string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
      string adUnitId = "unexpected_platform";
#endif

        // Create a smart banner which adjust its size automativally and place it on the top
        bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Top);
        if (publishingApp)
        {
            // Create an empty ad request.
            AdRequest request = new AdRequest.Builder().Build();
            // Load the interstitial with the request.
            this.bannerView.LoadAd(request);

        }
        else
        {
            // Create an empty ad request.
            AdRequest request = new AdRequest.Builder()
                .AddTestDevice("2077ef9a63d2b398840261c8221a0c9b").Build();
            // Load the interstitial with the request.
            this.bannerView.LoadAd(request);
        }
    }
    //to show banner ad
    public void DisplayBannerAds()
    {
        bannerView.Show();
    }
    public void LoadIntestellarAds()
    {
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }
    }
    public void DiplayRewardVideo()
    {
        if (rewardBasedVideo.IsLoaded())
        {
            rewardBasedVideo.Show();
        }
    }

    // Update is called once per frame
   
}
