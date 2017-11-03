using UnityEngine;
using System;
 
public class Weapon:ICloneable  {

    public IEquipable owner;

    public string Id { get; set; }
    public string Position { get; set; }
    public int LayerOrder { get; set; }
    public float Armor { get; set; }
    public float Damage { get; set; }
    public string Path { get; set; }
    public float Mass { get; set; }
    public GameObject sprite;

    public Weapon(string id, string position, float mass, int layerOrder, float armor, float damage, string path)
    {
        this.Id = id;
        this.Position = position;
        this.Mass = mass;
        this.LayerOrder = layerOrder;
        this.Armor = armor;
        this.Damage = damage;
        this.Path = path;
    }

    public object Clone()
    {
        return new Weapon(this.Id,this.Position,this.Mass,this.LayerOrder,this.Armor,this.Damage,this.Path);
    }
}
