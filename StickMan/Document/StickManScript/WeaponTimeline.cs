using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class WeaponTimeline
{
    public WeaponTimeline(WeaponType type)
    {
        this.WeaponType = type;
        this.Keys = new List<WeaponKey>();
    }

    public List<WeaponKey> Keys { get; private set; }

    public WeaponType WeaponType { get; private set; }
}

