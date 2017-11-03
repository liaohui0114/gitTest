using System;
using System.Runtime.CompilerServices;

public class StickmanKey
{
    public StickmanKey(float time, StickmanPose pose)
    {
        this.Time = time;
        this.Pose = pose;
    }

    public StickmanPose Pose { get; private set; }

    public float Time { get; private set; }
}

