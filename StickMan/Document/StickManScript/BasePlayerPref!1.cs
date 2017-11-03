using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;

public abstract class BasePlayerPref<T>
{
    protected BasePlayerPref(string key, [Optional] T defaultValue)
    {
        this.Key = key;
        this.Default = defaultValue;
    }

    public virtual void Save()
    {
        PlayerPrefs.Save();
    }

    public virtual void Set(T value)
    {
        this.Value = value;
    }

    public void SetAndSave(T value)
    {
        this.Set(value);
        this.Save();
    }

    public T Default { get; protected set; }

    public string Key { get; protected set; }

    public T Value { get; protected set; }
}

