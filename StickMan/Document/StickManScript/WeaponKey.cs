using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class WeaponKey
{
    public WeaponKey(float time, IEnumerable<WeaponPose> poses, object additional)
    {
        this.Time = time;
        this.Poses = new List<WeaponPose>(poses);
        this.Additional = additional;
    }

    public object Additional { get; private set; }

    public List<WeaponPose> Poses { get; private set; }

    public float Time { get; private set; }
}

