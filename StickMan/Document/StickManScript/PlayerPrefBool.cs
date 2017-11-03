using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerPrefBool : BasePlayerPref<bool>
{
    public PlayerPrefBool(string key, bool defaultValue = false) : base(key, defaultValue)
    {
        base.Value = PlayerPrefs.GetInt(base.Key, !defaultValue ? 0 : 1) == 1;
    }

    public override void Save()
    {
        PlayerPrefs.SetInt(base.Key, !base.Value ? 0 : 1);
        base.Save();
    }
}

