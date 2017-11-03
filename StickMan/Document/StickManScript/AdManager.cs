using GoogleMobileAds.Api;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;

public static class AdManager
{
    private static InterstitialAd _ad;
    private static UnityAction _closedAction;
    private static int _counter;
    private static int _frequency;
    private static UnityAction _loadedAction;
    private static AdRequest _request;
    private static bool _showLog;
    [CompilerGenerated]
    private static EventHandler<EventArgs> <>f__am$cache8;
    [CompilerGenerated]
    private static EventHandler<EventArgs> <>f__am$cache9;
    [CompilerGenerated]
    private static EventHandler<AdFailedToLoadEventArgs> <>f__am$cacheA;

    private static bool CheckForInit()
    {
        if (_ad != null)
        {
            return true;
        }
        Debug.LogWarning(typeof(AdManager).Name + " is not inited!");
        return false;
    }

    public static void Init(string adUnitId, bool showLog = false, AdRequest adRequest = null, int frequency = 1)
    {
        if (_ad != null)
        {
            Debug.LogWarning(typeof(AdManager).Name + " is already inited!");
        }
        else
        {
            try
            {
                _ad = new InterstitialAd(adUnitId);
                if (adRequest == null)
                {
                }
                _request = new AdRequest.Builder().Build();
                _showLog = showLog;
                _frequency = frequency;
                _counter = _frequency - 2;
                if (_frequency < 1)
                {
                    _frequency = 1;
                }
                if (<>f__am$cache8 == null)
                {
                    <>f__am$cache8 = delegate (object sender, EventArgs args) {
                        if (_loadedAction != null)
                        {
                            _loadedAction();
                        }
                        _loadedAction = null;
                        InterstitialIsLoaded = true;
                        if (_showLog)
                        {
                            Debug.Log(typeof(AdManager).Name + " interstitial is loaded");
                        }
                    };
                }
                _ad.AdLoaded += <>f__am$cache8;
                if (<>f__am$cache9 == null)
                {
                    <>f__am$cache9 = delegate (object sender, EventArgs args) {
                        if (_closedAction != null)
                        {
                            _closedAction();
                        }
                        _closedAction = null;
                        if (_showLog)
                        {
                            Debug.Log(typeof(AdManager).Name + " interstitial is closed");
                        }
                    };
                }
                _ad.AdClosed += <>f__am$cache9;
                if (<>f__am$cacheA == null)
                {
                    <>f__am$cacheA = (sender, args) => Debug.LogWarning(typeof(AdManager).Name + " failed to load interstitial.\n" + args.Message);
                }
                _ad.AdFailedToLoad += <>f__am$cacheA;
                if (_showLog)
                {
                    Debug.Log(typeof(AdManager).Name + " inited");
                }
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
            }
        }
    }

    public static void TryToLoadInterstitial(UnityAction loadedAction = null)
    {
        if ((CheckForInit() && !InterstitialIsLoaded) && (_frequency > 0))
        {
            _counter++;
            if (_counter >= _frequency)
            {
                _counter = 0;
                try
                {
                    _ad.LoadAd(_request);
                }
                catch (Exception exception)
                {
                    Debug.LogException(exception);
                }
                if (_loadedAction != null)
                {
                    _loadedAction = loadedAction;
                }
                if (_showLog)
                {
                    Debug.Log(typeof(AdManager).Name + " load interstitial");
                }
            }
        }
    }

    public static bool TryToShowInterstitial(UnityAction closedAction = null)
    {
        if ((!CheckForInit() || !InterstitialIsLoaded) || (_frequency <= 0))
        {
            return false;
        }
        try
        {
            _ad.Show();
        }
        catch (Exception exception)
        {
            Debug.LogException(exception);
        }
        InterstitialIsLoaded = false;
        if (_closedAction != null)
        {
            _closedAction = closedAction;
        }
        if (_showLog)
        {
            Debug.Log(typeof(AdManager).Name + " show interstitial");
        }
        return true;
    }

    public static bool InterstitialIsLoaded
    {
        [CompilerGenerated]
        get => 
            <InterstitialIsLoaded>k__BackingField;
        [CompilerGenerated]
        private set
        {
            <InterstitialIsLoaded>k__BackingField = value;
        }
    }
}

