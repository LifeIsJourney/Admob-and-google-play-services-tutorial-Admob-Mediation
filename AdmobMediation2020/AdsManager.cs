using UnityEngine.UI;
using UnityEngine;
[RequireComponent(typeof(GoogleAds))]
public class AdsManager : MonoBehaviour
{   
    public enum AdsType { Interstitial,Rewarded}
     AdsType adsType;

    public enum AdsEvent {Default,Open,Closed,GiveReward }
    AdsEvent adsEvent;
    GoogleAds ads;
    public System.Action RewardedAdStarted, RewadedAdsCompleted;

    public TMPro.TextMeshProUGUI AdsEventText;
    public Button InterstitialAdsBtn, RewarededAdsBtn,PublishingOrNot;

    public bool adStartedBool, RewardGivenBool, AdClosedBool;
    // Start is called before the first frame update
    void Start()
    {
        ads = GetComponent<GoogleAds>();
        ads.AdsClosed += AdsClosedEvent;
        ads.AdsStarted += AdsOpenEvent;
        ads.AdsCompleteGiveReward += AdsRewardEvent;

        InterstitialAdsBtn.onClick.AddListener(() => ShowAds(AdsType.Interstitial));
        RewarededAdsBtn.onClick.AddListener(() => ShowAds(AdsType.Rewarded));
        PublishingOrNot.onClick.AddListener(ChangePublishing);

        if (ads.publishing) { PublishingOrNot.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Real Ads"; }
        else { PublishingOrNot.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Test Ads"; }

        UpdateDisplay();
    }

   public void ShowAds( AdsType _adsType)
    {
        switch (_adsType)
        {
            case AdsType.Interstitial:
                ads.ShowInterstitialAd();
                break;
            case AdsType.Rewarded:
                ads.ShowRewadedAd();
                break;
            default:
                break;
        }
    }

    public void AdsOpenEvent()
    {
        adStartedBool = true; ;
        UpdateDisplay();
    } public void AdsClosedEvent()
    {
        AdClosedBool = true;
        UpdateDisplay();
    }
    public void AdsRewardEvent()
    {
        RewardGivenBool = true;
        UpdateDisplay();
    }
    void UpdateDisplay()
    {
        AdsEventText.text = "AdStarted "+adStartedBool+" Reward Given " +RewardGivenBool
            +" AdClosed "+AdClosedBool;
        ShowToast(adsEvent.ToString());
    }

    public void Reset()
    {
        adStartedBool = RewardGivenBool = AdClosedBool = false;
    }
    public void ChangePublishing()
    {
        ads.publishing = !ads.publishing;
        if (ads.publishing) { PublishingOrNot.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Real Ads"; }
        else { PublishingOrNot.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Test Ads"; }
        Reset();
    }
    public void ShowToast(string text)
    {
#if UNITY_EDITOR
        Debug.LogError(text);
#elif UNITY_ANDROID

        AndroidJavaClass toastClass =
                   new AndroidJavaClass("android.widget.Toast");

        object[] toastParams = new object[3];
        AndroidJavaClass unityActivity =
          new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        toastParams[0] =
                     unityActivity.GetStatic<AndroidJavaObject>
                               ("currentActivity");
        toastParams[1] = text;
        toastParams[2] = toastClass.GetStatic<int>
                               ("LENGTH_SHORT");

        AndroidJavaObject toastObject =
                        toastClass.CallStatic<AndroidJavaObject>
                                      ("makeText", toastParams);
        toastObject.Call("show");
#else
#endif

    }
}
