using UnityEngine;
using System.Collections;

public static class Utils {
 
    public static GameObject FindHideChildGameObject(GameObject parent, string childName)
    {
        if (parent.name == childName)
        {
            return parent;
        }
        if (parent.transform.childCount < 1)
        {
            return null;
        }
        GameObject obj = null;
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            GameObject go = parent.transform.GetChild(i).gameObject;
            obj = FindHideChildGameObject(go, childName);
            if (obj != null)
            {
                break;
            }
        }
        return obj;
    }


}
