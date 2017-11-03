using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;

[AddComponentMenu("")]
public class GoogleAnalytics : MonoBehaviour
{
    private static string _baseString;
    private static GoogleAnalytics _instance;
    private static string _postString;

    public static void Init(string trackingId, string bundleID, string appName, string appVersion)
    {
        if (_instance == null)
        {
            _instance = new GameObject("Google Analytics").AddComponent<GoogleAnalytics>();
            UnityEngine.Object.DontDestroyOnLoad(_instance.gameObject);
            TrackingID = trackingId;
            BundleID = bundleID;
            AppName = appName;
            AppVersion = appVersion;
            ClientID = SystemInfo.deviceUniqueIdentifier;
            string str = Screen.width + "x" + Screen.height;
            object[] objArray1 = new object[] { "http://www.google-analytics.com/collect?v=1&ul=", Application.systemLanguage, "&sr=", str, "&an=", WWW.EscapeURL(AppName), "&a=448166238&tid=", TrackingID, "&aid=", BundleID, "&cid=", WWW.EscapeURL(ClientID), "&_u=.sB&av=", AppVersion, "&_v=ma1b3" };
            _baseString = string.Concat(objArray1);
            _postString = "&qt=2500&z=185";
        }
    }

    private static void Send(string url)
    {
        if (_instance == null)
        {
            UnityEngine.Debug.LogWarning("Google Analytics is not been inited!");
        }
        else
        {
            _instance.StartCoroutine(Send(new WWW(url)));
        }
    }

    [DebuggerHidden]
    private static IEnumerator Send(WWW www) => 
        new <Send>c__Iterator0 { 
            www = www,
            <$>www = www
        };

    public static void SendEvent(string eventCategory, string eventAction, string eventLabel = null, int eventValue = 0x7fffffff)
    {
        string[] textArray1 = new string[] { _baseString, "&t=event&ec=", WWW.EscapeURL(eventCategory), "&ea=", WWW.EscapeURL(eventAction) };
        string str = string.Concat(textArray1);
        if (!string.IsNullOrEmpty(eventLabel))
        {
            str = str + "&el=" + WWW.EscapeURL(eventLabel);
        }
        if (eventValue < 0x7fffffff)
        {
            str = str + "&ev=" + eventValue;
        }
        Send(str + _postString);
    }

    public static void SendScreen(string screenName)
    {
        Send(_baseString + "&t=appview&cd=" + WWW.EscapeURL(screenName) + _postString);
    }

    public static string AppName
    {
        [CompilerGenerated]
        get => 
            <AppName>k__BackingField;
        [CompilerGenerated]
        private set
        {
            <AppName>k__BackingField = value;
        }
    }

    public static string AppVersion
    {
        [CompilerGenerated]
        get => 
            <AppVersion>k__BackingField;
        [CompilerGenerated]
        private set
        {
            <AppVersion>k__BackingField = value;
        }
    }

    public static string BundleID
    {
        [CompilerGenerated]
        get => 
            <BundleID>k__BackingField;
        [CompilerGenerated]
        private set
        {
            <BundleID>k__BackingField = value;
        }
    }

    public static string ClientID
    {
        [CompilerGenerated]
        get => 
            <ClientID>k__BackingField;
        [CompilerGenerated]
        private set
        {
            <ClientID>k__BackingField = value;
        }
    }

    public static string TrackingID
    {
        [CompilerGenerated]
        get => 
            <TrackingID>k__BackingField;
        [CompilerGenerated]
        private set
        {
            <TrackingID>k__BackingField = value;
        }
    }

    [CompilerGenerated]
    private sealed class <Send>c__Iterator0 : IDisposable, IEnumerator, IEnumerator<object>
    {
        internal object $current;
        internal int $PC;
        internal WWW <$>www;
        internal WWW www;

        [DebuggerHidden]
        public void Dispose()
        {
            this.$PC = -1;
        }

        public bool MoveNext()
        {
            uint num = (uint) this.$PC;
            this.$PC = -1;
            switch (num)
            {
                case 0:
                case 1:
                    if (!this.www.isDone)
                    {
                        this.$current = new WaitForEndOfFrame();
                        this.$PC = 1;
                        return true;
                    }
                    if (!string.IsNullOrEmpty(this.www.error))
                    {
                        UnityEngine.Debug.LogWarning("Google Analytics error:\n" + this.www.error);
                    }
                    this.$PC = -1;
                    break;
            }
            return false;
        }

        [DebuggerHidden]
        public void Reset()
        {
            throw new NotSupportedException();
        }

        object IEnumerator<object>.Current =>
            this.$current;

        object IEnumerator.Current =>
            this.$current;
    }
}

