using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
public class GoogleAdmobMediationHelper : MonoBehaviour
{
	protected static GoogleAdmobMediationHelper instance;
    public static GoogleAdmobMediationHelper Instance
    {
        get
        {
            if (instance != null)
            {
                return instance;
            }
            instance = FindObjectOfType<GoogleAdmobMediationHelper>();
            if (instance != null)
            {
                return instance;
            }
            GameObject obj = new GameObject();
            obj.name="Ads";
            instance = obj.AddComponent<GoogleAdmobMediationHelper>();
            return instance;
        }
    }

    private BannerView bannerView;

    private InterstitialAd interstitial;

    private RewardBasedVideoAd rewardBasedVideo;
    //when publishing app 
    public bool publishingApp;

    public string testRewardId, testIntestellarId, testGameAppId;

    [Header("Publishing")]
    public string rewardId, intestellarId, gameAppId;
   

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        if (publishingApp)
        {
#if UNITY_ANDROID
            string appId = "ca-app-pub-1339385848497432~874091076";
#elif UNITY_IPHONE
            string appId = "ca-app-pub-3940256099942544~1458002511";
#else
            string appId = "unexpected_platform";
#endif

            // Initialize the Google Mobile Ads SDK.
            MobileAds.Initialize(appId);

        }


        
       
        //REQUEST VIDEO
        this.RequestRewardBasedVideo();
        RequestInterstitial();
       
    }
   
    private void RequestInterstitial()
    {
        string adUnitId = "";
        if (publishingApp)
        {
    #if UNITY_ANDROID
             adUnitId = "ca-app-pub-1339385848497432/314711626";
#endif

        }
        else
        {
#if UNITY_ANDROID
            adUnitId = "ca-app-pub-3940256099942544/1033173712";
  #endif
        }


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
        string adUnitId = "";
        if (publishingApp)
        {
#if UNITY_ANDROID
            adUnitId = "ca-app-pub-1339385848497432/645965376";
#endif

        }
        else
        {
#if UNITY_ANDROID
            adUnitId = "ca-app-pub-3940256099942544/5224354917";
#endif
        }
        //REWARDED ADS
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

    public void LoadIntestellarAds()
    {
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
           
        }
        RequestInterstitial();
    }
    public void DiplayRewardVideo()
    {
        if (rewardBasedVideo.IsLoaded())
        {
            rewardBasedVideo.Show();
                      
        }
        this.RequestRewardBasedVideo();
    }
    public bool IsRewardedVideoLoaded()
    {
        return rewardBasedVideo.IsLoaded();
    }
    public bool IsIntestellarLoaded()
    {
        return interstitial.IsLoaded();
    }
}
