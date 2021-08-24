using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AdsManager : MonoBehaviour {

	public static AdsManager Instance;

	[Header("Main Settings")]
	public bool useLogging = false;

	public bool dntDestroyOnLoad = false;
	[HideInInspector] public bool isTesting = false;

	[Header("Keys Settings")]
	public bool useNormalBanner = true;
	public bool useInterstitialBanner = true;
	public bool useRewardBasedVideo = false;

	public float timeBetweenIntBanner = 60;

	[Header("Banner Keys")]
	public string bannerID = "";
	public string interstitialID = "";
	public string rewardedID = "";
	public string sdkKey = "";

	private static IAds adsScript;

	public static Action OnRewardedVideoShown;
	public static Action OnRewardedVideoShown_Lock;

	public static Action OnAdsInitEvent;
	public static Action OnAdsNotInitEvent;

	private static bool isInit = false;
	private static bool canShowInterstitial = true;

	public void SetKeys(string bannerID, string interstitialID, string rewardedID, string sdkKey) {
		this.bannerID = bannerID;
		this.interstitialID = interstitialID;
		this.rewardedID = rewardedID;
		this.sdkKey = sdkKey;
	}

	void Awake() {
		InitAwake();
	}

	void InitAwake() {
		if (dntDestroyOnLoad) {
			DontDestroyOnLoad(gameObject);
			if (Instance == this) {
				Destroy(gameObject);
			} else {
				Instance = this;
			}
		} else {
			Instance = this;
		}

		isInit = false;
	}

	public static void InitAds(IAds newAdsScript) {
		adsScript = newAdsScript;

		//Clear self after success or fail
		adsScript.OnRewardedAdComplete += () => {
			OnRewardedVideoShown_Lock?.Invoke();

			OnRewardedVideoShown = null; 
		};

		isInit = true;

		OnAdsInitEvent?.Invoke();

		if (Instance.useLogging) {
			Debug.Log("AdsManager initialized!");
		}
	}

	public static void ShowBanner() {
		if (isInit) {
			if (Instance.useLogging) {
				Debug.Log("AdsManager ShowBanner!");
			}

			adsScript.ShowBanner();
		}
	}

	public static void HideBanner() {
		if (isInit) {
			if (AdsManager.Instance.useLogging) {
				Debug.Log("AdsManager HideBanner!");
			}

			adsScript.HideBanner();
		}
	}

	public static void ShowInterstitial(bool instantShow = false) {
		if (isInit && (canShowInterstitial || instantShow)) {
			if (AdsManager.Instance.useLogging) {
				Debug.Log("AdsManager ShowInterstitial!" + ((instantShow) ? " Instant!" : ""));
			}

			adsScript.ShowInterstitial();

			if (!instantShow) {
				canShowInterstitial = false;
				AdsManager.Instance.Invoke("UpdateInterstitialAvailable", AdsManager.Instance.timeBetweenIntBanner);
			}
		}
	}

	void UpdateInterstitialAvailable() {
		canShowInterstitial = true;

		if (Instance.useLogging) {
			Debug.Log("AdsManager Can Show Interstitial!");
		}
	}

	public static void ShowRewarded() {
		#if UNITY_EDITOR
		if (Instance.useLogging) {
			Debug.Log("Editor AdsManager ShowRewarded!");
		}

        //Debug.Log(OnRewardedVideoShown.);
		adsScript.ShowRewarded(OnRewardedVideoShown);

		#else
		if (isInit) {
			if (Instance.useLogging) {
				Debug.Log("AdsManager ShowRewarded!");
			}

			adsScript.ShowRewarded(OnRewardedVideoShown);
		}
		#endif
	}

	public static void ShowRewarded(Action _onRewardedVideoShown) { 
		#if UNITY_EDITOR
		if (Instance.useLogging) {
			Debug.Log("Editor AdsManager ShowRewarded!");
		}

		adsScript.ShowRewarded(_onRewardedVideoShown);
		#else
		if (isInit) {
			if (Instance.useLogging) {
				Debug.Log("AdsManager ShowRewarded!");
			}

			adsScript.ShowRewarded(_onRewardedVideoShown);
		}
		#endif
	}

	public static void DisableAds() {
		if(Instance == null) {
			return;
		}

		if (Instance.useLogging) {
			Debug.Log("AdsManager DisableAds!");
		}

		if (isInit) {
			DelayedDisableAds();
		} else {
			OnAdsInitEvent += DelayedDisableAds;
		}
	}

	private static void DelayedDisableAds() {
		if (Instance.useLogging) {
			Debug.Log("AdsManager Delayed DisableAds!");
		}

		OnAdsInitEvent -= DelayedDisableAds;

		adsScript.DisableAds();
	}


	public static bool IsRewardedAvailable() 
	{
		#if UNITY_EDITOR
        return true;
		#else
        if (isInit) {
			if (Instance.useLogging) {
				Debug.Log("AdsManager IsRewardedAvailable: " + adsScript.IsRewardedAvailable());
			}

			return adsScript.IsRewardedAvailable();
		}

		return false;
		#endif
	}
}

public abstract class IAds : MonoBehaviour {
	public abstract void InitAds();

	public abstract void ShowBanner();
	public abstract void HideBanner();
	public abstract void ShowInterstitial();
	public abstract void ShowRewarded(Action rewardCallback);

	public abstract void DisableAds();
	public abstract bool IsRewardedAvailable();

	public Action OnRewardedAdComplete;
	protected bool OnRewardedAdCompleteIsNull() {
		return OnRewardedAdComplete == null;
	}
	protected void RewardedAdCompleteEvent() {
		OnRewardedAdComplete?.Invoke();
	}
}