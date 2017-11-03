using Data;

public class CharFactory :Factory<CharPose> {

    private static CharFactory instance;

    private CharFactory(string collection) : base(collection)
    {
    }

    public static CharFactory Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new CharFactory(Table.CHAR_MANIFEST);
            }
            return instance;
        }
    }


    protected override CharPose Build(DocumentWrapper property)
    {
        string id = property.GetStringValue("id");
        float unit_length = property.GetFloatValue("unit_length"); 

        float spine_mass = property.GetFloatValue("spine_mass");
        float spine_limit = property.GetFloatValue("spine_limit");
        float spine_angle = property.GetFloatValue("spine_angle");
        float spine_length = property.GetFloatValue("spine_length");
        float spine_thickness = property.GetFloatValue("spine_thickness");

        float head_mass = property.GetFloatValue("head_mass");
        float head_limit = property.GetFloatValue("head_limit");
        float head_angle = property.GetFloatValue("head_angle");
        float head_length = property.GetFloatValue("head_length");
        float head_thickness = property.GetFloatValue("head_thickness");

        float upArmL_mass = property.GetFloatValue("upArmL_mass");
        float upArmL_limit = property.GetFloatValue("upArmL_limit");
        float upArmL_angle = property.GetFloatValue("upArmL_angle");
        float upArmL_length = property.GetFloatValue("upArmL_length");
        float upArmL_thickness = property.GetFloatValue("upArmL_thickness");

        float upArmR_mass = property.GetFloatValue("upArmR_mass");
        float upArmR_limit = property.GetFloatValue("upArmR_limit");
        float upArmR_angle = property.GetFloatValue("upArmR_angle");
        float upArmR_length = property.GetFloatValue("upArmR_length");
        float upArmR_thickness = property.GetFloatValue("upArmR_thickness");

        float downArmL_mass = property.GetFloatValue("downArmL_mass");
        float downArmL_limit = property.GetFloatValue("downArmL_limit");
        float downArmL_angle = property.GetFloatValue("downArmL_angle");
        float downArmL_length = property.GetFloatValue("downArmL_length");
        float downArmL_thickness = property.GetFloatValue("downArmL_thickness");

        float downArmR_mass = property.GetFloatValue("downArmR_mass");
        float downArmR_limit = property.GetFloatValue("downArmR_limit");
        float downArmR_angle = property.GetFloatValue("downArmR_angle");
        float downArmR_length = property.GetFloatValue("downArmR_length");
        float downArmR_thickness = property.GetFloatValue("downArmR_thickness");

        float upLegL_mass = property.GetFloatValue("upLegL_mass");
        float upLegL_limit = property.GetFloatValue("upLegL_limit");
        float upLegL_angle = property.GetFloatValue("upLegL_angle");
        float upLegL_length = property.GetFloatValue("upLegL_length");
        float upLegL_thickness = property.GetFloatValue("upLegL_thickness");

        float upLegR_mass = property.GetFloatValue("upLegR_mass");
        float upLegR_limit = property.GetFloatValue("upLegR_limit");
        float upLegR_angle = property.GetFloatValue("upLegR_angle");
        float upLegR_length = property.GetFloatValue("upLegR_length");
        float upLegR_thickness = property.GetFloatValue("upLegR_thickness");

        float downLegL_mass = property.GetFloatValue("downLegL_mass");
        float downLegL_limit = property.GetFloatValue("downLegL_limit");
        float downLegL_angle = property.GetFloatValue("downLegL_angle");
        float downLegL_length = property.GetFloatValue("downLegL_length");
        float downLegL_thickness = property.GetFloatValue("downLegL_thickness");

        float downLegR_mass = property.GetFloatValue("downLegR_mass");
        float downLegR_limit = property.GetFloatValue("downLegR_limit");
        float downLegR_angle = property.GetFloatValue("downLegR_angle");
        float downLegR_length = property.GetFloatValue("downLegR_length");
        float downLegR_thickness = property.GetFloatValue("downLegR_thickness");
 

        return new CharPose(id,
                            unit_length,
                            spine_mass,
                            spine_limit,
                            spine_angle,
                            spine_length,
                            spine_thickness,

                            head_mass,
                            head_limit,
                            head_angle,
                            head_length,
                            head_thickness,

                            upArmL_mass,
                            upArmL_limit,
                            upArmL_angle,
                            upArmL_length,
                            upArmL_thickness,

                            upArmR_mass,
                            upArmR_limit,
                            upArmR_angle,
                            upArmR_length,
                            upArmR_thickness,

                            downArmL_mass,
                            downArmL_limit,
                            downArmL_angle,
                            downArmL_length,
                            downArmL_thickness,

                            downArmR_mass,
                            downArmR_limit,
                            downArmR_angle,
                            downArmR_length,
                            downArmR_thickness,

                            upLegL_mass,
                            upLegL_limit,
                            upLegL_angle,
                            upLegL_length,
                            upLegL_thickness,

                            upLegR_mass,
                            upLegR_limit,
                            upLegR_angle,
                            upLegR_length,
                            upLegR_thickness,

                            downLegL_mass,
                            downLegL_limit,
                            downLegL_angle,
                            downLegL_length,
                            downLegL_thickness,

                            downLegR_mass,
                            downLegR_limit,
                            downLegR_angle,
                            downLegR_length,
                            downLegR_thickness );
    }
}
