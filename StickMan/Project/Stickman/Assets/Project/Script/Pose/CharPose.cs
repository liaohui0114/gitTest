using UnityEngine;
using System;
using System.Collections;
using System.Reflection;
using System.Linq;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

[Serializable]
public class CharPose :ICloneable {

    public string id;
    public float unit_length;

    public float spine_mass;
    public float spine_limit;
    public float spine_angle;
    public float spine_length;
    public float spine_thickness;

    public float head_mass;
    public float head_limit;
    public float head_angle;
    public float head_length;
    public float head_thickness;

    public float upArmL_mass;
    public float upArmL_limit;
    public float upArmL_angle;
    public float upArmL_length;
    public float upArmL_thickness;

    public float upArmR_mass;
    public float upArmR_limit;
    public float upArmR_angle;
    public float upArmR_length;
    public float upArmR_thickness;

    public float downArmL_mass;
    public float downArmL_limit;
    public float downArmL_angle;
    public float downArmL_length;
    public float downArmL_thickness;

    public float downArmR_mass ;
    public float downArmR_limit;
    public float downArmR_angle;
    public float downArmR_length;
    public float downArmR_thickness;

    public float upLegL_mass;
    public float upLegL_limit;
    public float upLegL_angle;
    public float upLegL_length;
    public float upLegL_thickness;

    public float upLegR_mass;
    public float upLegR_limit;
    public float upLegR_angle;
    public float upLegR_length;
    public float upLegR_thickness;

    public float downLegL_mass;
    public float downLegL_limit;
    public float downLegL_angle;
    public float downLegL_length;
    public float downLegL_thickness;

    public float downLegR_mass;
    public float downLegR_limit;
    public float downLegR_angle;
    public float downLegR_length;
    public float downLegR_thickness;


    public CharPose(string id, params float[] paramList)
    {
        this.id = id;

        Type charPoseType = typeof(CharPose);
        FieldInfo[] info = charPoseType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
 
        Debug.Assert(paramList.Length == info.Length -1, "parameter length error");

        for(int i=0;i < paramList.Length;i++)
        {
            info[i+1].SetValue(this,paramList[i]);
        }
    }

    public object Clone()
    {
        using (Stream objectStream = new MemoryStream())
        {
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(objectStream, this);
            objectStream.Seek(0, SeekOrigin.Begin);
            return formatter.Deserialize(objectStream) as CharPose;
        }
    }

}
