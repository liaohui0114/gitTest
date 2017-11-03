using Data;

public class EquipmentFactory : Factory<Equipment>
{
    public EquipmentFactory(string collection) : base(collection)
    {
    }

    private static EquipmentFactory instance;

    public static EquipmentFactory Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new EquipmentFactory(Table.EQUIPMENT_MANIFEST);
            }
            return instance;
        }
    }
 
    protected override Equipment Build(DocumentWrapper property)
    {
        string id = property.GetStringValue("id");
        string position = property.GetStringValue("position");
        int isBody = property.GetIntValue("is_body");
        int layerOrder = property.GetIntValue("layer_order");
        float mass = property.GetFloatValue("mass");
        float armor = property.GetFloatValue("armor");
        float damage = property.GetFloatValue("damage");
        string path = "res/" + property.GetStringValue("path"); 

        return new Equipment(id, position, isBody, layerOrder, mass, armor, damage, path);
    }

}
