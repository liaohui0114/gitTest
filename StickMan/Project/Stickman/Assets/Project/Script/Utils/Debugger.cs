using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugger : MonoBehaviour {

    public static void Log(string message)
    {
        Debug.Log(message);
    }

    public static void LogError(string message)
    {
        Debug.LogError(message);
    }

}
