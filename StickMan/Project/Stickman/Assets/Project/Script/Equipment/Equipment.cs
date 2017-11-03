using UnityEngine;
using System;

public class Equipment: ICloneable{

    public IEquipable owner;
  
    public string Id { get; set; }
    public string Position { get; set; }
    public int LayerOrder { get; set; }
    public float Armor { get; set; }
    public float Damage { get; set; }
    public string Path { get; set; }
    public float Mass { get; set; }
    public int isBody { get; set; }
    public GameObject sprite = null;
    public float Hp { get; set; }

    public Equipment(string id,string position,int isBody, int layerOrder,float mass, float armor, float damage,string path)
    {
        this.Id = id;
        this.Position = position;
        this.LayerOrder = layerOrder;
        this.Mass = mass;
        this.Armor = armor;
        this.Damage = damage;
        this.Path = path;
        this.Hp = armor;
        this.isBody = isBody;

    }
    public object Clone()
    {
        return new Equipment(this.Id, this.Position,this.isBody,this.LayerOrder,this.Mass, this.Armor, this.Damage, this.Path);
    }
}
