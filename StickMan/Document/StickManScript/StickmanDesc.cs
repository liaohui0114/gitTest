using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class StickmanDesc
{
    public StickmanDesc()
    {
        WeaponType none = WeaponType.None;
        this.RightWeapon = none;
        this.LeftWeapon = none;
        this.HealthModifier = 1f;
    }

    public float HealthModifier { get; set; }

    public WeaponType LeftWeapon { get; set; }

    public Vector2 Position { get; set; }

    public WeaponType RightWeapon { get; set; }

    public StickmanType Type { get; set; }
}

