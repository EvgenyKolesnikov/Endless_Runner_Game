using UnityEngine.Events;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class GoogleAdMobController : MonoBehaviour
{
    private BannerView bannerView;
    private InterstitialAd interstitialAd;
    private RewardedAd rewardedAd;
    private RewardedInterstitialAd rewardedInterstitialAd;

    public UnityEvent OnAdLoadedEvent;
    public UnityEvent OnAdFailedToLoadEvent;
    public UnityEvent OnAdOpeningEvent;
    public UnityEvent OnAdFailedToShowEvent;
    public UnityEvent OnUserEarnedRewardEvent;
    public UnityEvent OnAdClosedEvent;
    public UnityEvent OnAdLeavingApplicationEvent;
    public Text statusText;

    #region UNITY MONOBEHAVIOR METHODS

    public void Start()
    {
        MobileAds.SetiOSAppPauseOnBackground(true);
        MobileAds.SetApplicationMuted(true);

        List<String> deviceIds = new List<String>() { AdRequest.TestDeviceSimulator };

        // Добавьте несколько идентификаторов тестовых устройств (замените их собственными идентификаторами).
#if UNITY_IPHONE
        //deviceIds.Add("");
#elif UNITY_ANDROID
        deviceIds.Add("78aea533-fa84-475b-8506-759ee8e828e2");
#endif

        // Настроить TagForChildDirectedTreatment и идентификаторы тестовых устройств.
        RequestConfiguration requestConfiguration =
        new RequestConfiguration.Builder()
        .SetTagForChildDirectedTreatment(TagForChildDirectedTreatment.Unspecified)
        .SetTestDeviceIds(deviceIds).build();

        MobileAds.SetRequestConfiguration(requestConfiguration);

        // Инициализировать Google Mobile Ads SDK.
        MobileAds.Initialize(HandleInitCompleteAction);
    }

    
    private void HandleInitCompleteAction(InitializationStatus initstatus)
    {
        // Обратные вызовы от GoogleMobileAds не гарантируются
        // основной поток.
        // В этом примере мы используем MobileAdsEventExecutor для планирования этих вызовов на
        // следующий цикл Update ().
        MobileAdsEventExecutor.ExecuteInUpdate(() => {
            Debug.Log("Инициализация завершена");
            //RequestBannerAd();
            //RequestAndLoadInterstitialAd();
        });
    }
    
    #endregion

    #region HELPER METHODS

    private AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder()
            .AddTestDevice(AdRequest.TestDeviceSimulator)
            .AddTestDevice("F829369069EEA678")
            .AddKeyword("unity-admob-sample")
            .TagForChildDirectedTreatment(false)
            .AddExtra("color_bg", "9B30FF")
            .Build();
    }

    #endregion
    
    #region BANNER ADS
    
    public void RequestBannerAd()
    {
        Debug.Log("Запрос рекламного баннера.");
        // Эти рекламные блоки настроены на постоянный показ тестовых объявлений.
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = "ca-app-pub-3173404135237399/7495456840";
#elif UNITY_IPHONE
        string adUnitId = "";
#else
        string adUnitId = "unexpected_platform";
#endif
        // Очистить баннер перед повторным использованием
        if (bannerView != null)
        {
            bannerView.Destroy();
        }

        // Создаем баннер 320x50 в верхней части экрана
        bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);

        // Добавляем обработчики событий
        bannerView.OnAdLoaded += (sender, args) => OnAdLoadedEvent.Invoke();
        bannerView.OnAdFailedToLoad += (sender, args) => OnAdFailedToLoadEvent.Invoke();
        bannerView.OnAdOpening += (sender, args) => OnAdOpeningEvent.Invoke();
        bannerView.OnAdClosed += (sender, args) => OnAdClosedEvent.Invoke();
        bannerView.OnAdLeavingApplication += (sender, args) => OnAdLeavingApplicationEvent.Invoke();

        // Загружаем рекламный баннер
        bannerView.LoadAd(CreateAdRequest());
    }

    public void DestroyBannerAd()
    {
        if (bannerView != null)
        {
            bannerView.Destroy();
        }
    }
    
    #endregion
    

    #region INTERSTITIAL ADS

    public void RequestAndLoadInterstitialAd()
    {
        Debug.Log("Запрос межстраничной рекламы.");
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = "ca-app-pub-3173404135237399/3120395890";
#elif UNITY_IPHONE
        string adUnitId = "";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Очистить межстраничное объявление перед его использованием
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
        }

        interstitialAd = new InterstitialAd(adUnitId);

        // Добавляем обработчики событий
        interstitialAd.OnAdLoaded += (sender, args) => OnAdLoadedEvent.Invoke();
        interstitialAd.OnAdFailedToLoad += (sender, args) => OnAdFailedToLoadEvent.Invoke();
        interstitialAd.OnAdOpening += (sender, args) => OnAdOpeningEvent.Invoke();
        interstitialAd.OnAdClosed += (sender, args) => OnAdClosedEvent.Invoke();
        interstitialAd.OnAdLeavingApplication += (sender, args) => OnAdLeavingApplicationEvent.Invoke();

        // Загружаем межстраничное объявление
        interstitialAd.LoadAd(CreateAdRequest());
    }

    public void ShowInterstitialAd()
    {
        if (interstitialAd.IsLoaded())
        {
            interstitialAd.Show();
        }
        else
        {
            Debug.Log("Межстраничное объявление еще не готово");
        }
    }

    public void DestroyInterstitialAd()
    {
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
        }
    }
    #endregion

    #region REWARDED ADS

    public void RequestAndLoadRewardedAd()
    {
        Debug.Log("Запрос объявления с вознаграждением.");
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = "ca-app-pub-3173404135237399/2613785973";
#elif UNITY_IPHONE
        string adUnitId = "";
#else
        string adUnitId = "unexpected_platform";
#endif

        // создаем новый экземпляр объявления с вознаграждением
        rewardedAd = new RewardedAd(adUnitId);

        // Добавляем обработчики событий
        rewardedAd.OnAdLoaded += (sender, args) => OnAdLoadedEvent.Invoke();
        rewardedAd.OnAdFailedToLoad += (sender, args) => OnAdFailedToLoadEvent.Invoke();
        rewardedAd.OnAdOpening += (sender, args) => OnAdOpeningEvent.Invoke();
        rewardedAd.OnAdFailedToShow += (sender, args) => OnAdFailedToShowEvent.Invoke();
        rewardedAd.OnAdClosed += (sender, args) => OnAdClosedEvent.Invoke();
        rewardedAd.OnUserEarnedReward += (sender, args) => OnUserEarnedRewardEvent.Invoke();

        // Создаем пустой запрос объявления
        rewardedAd.LoadAd(CreateAdRequest());
    }

    public void ShowRewardedAd()
    {
        if (rewardedAd != null)
        {
            rewardedAd.Show();
        }
        else
        {
            Debug.Log("Объявление с вознаграждением еще не готово.");
        }
    }

    public void RequestAndLoadRewardedInterstitialAd()
    {
        Debug.Log("Запрос межстраничного объявления с вознаграждением.");
        // Эти рекламные блоки настроены на постоянный показ тестовых объявлений.
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
            string adUnitId = "ca-app-pub-3173404135237399/2613785973";
#elif UNITY_IPHONE
            string adUnitId = "";
#else
            string adUnitId = "unexpected_platform";
#endif

        // Создаем межстраничное объявление.
        RewardedInterstitialAd.LoadAd(adUnitId, CreateAdRequest(), (rewardedInterstitialAd, error) =>
        {

            if (error != null)
            {
                MobileAdsEventExecutor.ExecuteInUpdate(() => {
                    Debug.Log("Не удалось загрузить RewardedInterstitialAd, ошибка: " + error);
                });
                return;
            }

            this.rewardedInterstitialAd = rewardedInterstitialAd;
            MobileAdsEventExecutor.ExecuteInUpdate(() => {
                Debug.Log("RewardedInterstitialAd загружено");
            });
            // Регистрируемся на рекламные события.
            this.rewardedInterstitialAd.OnAdDidPresentFullScreenContent += (sender, args) =>
            {
                MobileAdsEventExecutor.ExecuteInUpdate(() => {
                    Debug.Log("Показано межстраничное объявление с вознаграждением.");
                });
            };
            this.rewardedInterstitialAd.OnAdDidDismissFullScreenContent += (sender, args) =>
            {
                MobileAdsEventExecutor.ExecuteInUpdate(() => {
                    Debug.Log("Межстраничное объявление с вознаграждением закрыто.");
                });
                this.rewardedInterstitialAd = null;
            };
            this.rewardedInterstitialAd.OnAdFailedToPresentFullScreenContent += (sender, args) =>
            {
                MobileAdsEventExecutor.ExecuteInUpdate(() => {
                    Debug.Log("Не удалось показать межстраничное объявление с вознаграждением.");
                });
                this.rewardedInterstitialAd = null;
            };
        });
    }

    public void ShowRewardedInterstitialAd()
    {
        if (rewardedInterstitialAd != null)
        {
            rewardedInterstitialAd.Show((reward) => {
                MobileAdsEventExecutor.ExecuteInUpdate(() => {
                    Debug.Log("Пользователь вознагражден: " + reward.Amount);
                });
            });
        }
        else
        {
            Debug.Log("Объявление с вознаграждением еще не готово.");
        }
    }

    #endregion
}