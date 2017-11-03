using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class StickmanTimeline
{
    public StickmanTimeline(StickmanType stickmanType)
    {
        this.StickmanType = stickmanType;
        this.Keys = new List<StickmanKey>();
    }

    public List<StickmanKey> Keys { get; private set; }

    public StickmanType StickmanType { get; private set; }
}

