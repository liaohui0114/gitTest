using System;
using Data;

public class WeaponFactory : Factory<Weapon> {

    private static WeaponFactory instance;

    public static WeaponFactory Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new WeaponFactory(Table.WEAPON_MANIFEST);
            }
            return instance;
        }
    }

    public WeaponFactory(string collection) : base(collection)
    {
    }
 
    protected override Weapon Build(DocumentWrapper property)
    {
        string id = property.GetStringValue("id");
        string position = property.GetStringValue("position");
        float mass = property.GetFloatValue("mass");
        int layerOrder = property.GetIntValue("layer_order");
        float armor = property.GetFloatValue("armor");
        float damage = property.GetFloatValue("damage");
        string path = "res/" + property.GetStringValue("path");

        return new Weapon(id, position, mass, layerOrder, armor, damage, path);
    }
}
