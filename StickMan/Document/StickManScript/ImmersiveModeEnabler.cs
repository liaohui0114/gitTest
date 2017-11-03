using System;
using UnityEngine;

public class ImmersiveModeEnabler : MonoBehaviour
{
    private static bool created;
    private AndroidJavaClass javaClass;
    private AndroidJavaObject javaObj;
    private bool paused;
    private AndroidJavaObject unityActivity;

    private void Awake()
    {
        if (!Application.isEditor)
        {
            this.HideNavigationBar();
        }
        if (!created)
        {
            UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
            created = true;
        }
        else
        {
            UnityEngine.Object.Destroy(base.gameObject);
        }
    }

    private void HideNavigationBar()
    {
        ImmersiveModeEnabler enabler = this;
        lock (enabler)
        {
            using (this.javaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                this.unityActivity = this.javaClass.GetStatic<AndroidJavaObject>("currentActivity");
            }
            if (this.unityActivity != null)
            {
                using (this.javaClass = new AndroidJavaClass("com.rak24.androidimmersivemode.Main"))
                {
                    if (this.javaClass != null)
                    {
                        this.javaObj = this.javaClass.CallStatic<AndroidJavaObject>("instance", new object[0]);
                        if (this.javaObj != null)
                        {
                            object[] objArray1 = new object[] { delegate {
                                object[] args = new object[] { this.unityActivity };
                                this.javaObj.Call("EnableImmersiveMode", args);
                            } };
                            this.unityActivity.Call("runOnUiThread", objArray1);
                        }
                    }
                }
            }
        }
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if ((hasFocus && (this.javaObj != null)) && !this.paused)
        {
            object[] objArray1 = new object[] { delegate {
                object[] args = new object[] { this.unityActivity };
                this.javaObj.CallStatic("ImmersiveModeFromCache", args);
            } };
            this.unityActivity.Call("runOnUiThread", objArray1);
        }
    }

    private void OnApplicationPause(bool pausedState)
    {
        this.paused = pausedState;
    }

    public void PinThisApp()
    {
        if (this.javaObj != null)
        {
            object[] args = new object[] { this.unityActivity };
            this.javaObj.CallStatic("EnableAppPin", args);
        }
    }

    public void UnPinThisApp()
    {
        if (this.javaObj != null)
        {
            object[] args = new object[] { this.unityActivity };
            this.javaObj.CallStatic("DisableAppPin", args);
        }
    }
}

