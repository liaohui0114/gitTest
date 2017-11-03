using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class Campaign
{
    public Campaign(string name)
    {
        this.Name = name;
        this.Levels = new List<Level>();
        float num = 1f;
        this.EnemyHealthModifier = num;
        this.PlayerHealthModifier = num;
    }

    public float EnemyHealthModifier { get; set; }

    public List<Level> Levels { get; set; }

    public string Name { get; private set; }

    public float PlayerHealthModifier { get; set; }
}

