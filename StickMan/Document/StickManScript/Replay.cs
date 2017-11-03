using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class Replay
{
    public Replay()
    {
        this.StickmanTimelines = new List<StickmanTimeline>();
        this.WeaponTimelines = new List<WeaponTimeline>();
        this.EventsTimeline = new ReplayEventsTimeline();
    }

    public ReplayEventsTimeline EventsTimeline { get; set; }

    public List<StickmanTimeline> StickmanTimelines { get; private set; }

    public List<WeaponTimeline> WeaponTimelines { get; private set; }
}

