using System;
using UnityEngine;

public class DemoScript : MonoBehaviour
{
    public GUISkin guiSkin;

    private void OnGUI()
    {
        GUI.TextArea(new Rect((float) ((Screen.width / 2) - 100), 50f, 200f, 30f), "Click Here", this.guiSkin.textArea);
    }
}

