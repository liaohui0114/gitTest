using System;
using UnityEngine;

public class BoosterButton : Button
{
    [SerializeField]
    private TextMesh _countText;
    [SerializeField]
    private SpriteRenderer _image;
    private bool _shadow;
    public BoosterType Type;

    private void Start()
    {
        this.UpdateCount();
    }

    public void UpdateCount()
    {
        int boosters = PlayerSettings.GetBoosters(this.Type);
        this._countText.text = "x" + boosters;
        this.IsShadowed = !base.IsEnabled;
    }

    public int Count =>
        PlayerSettings.GetBoosters(this.Type);

    public bool IsShadowed
    {
        get => 
            this._shadow;
        set
        {
            this._shadow = value;
            this._image.color = !this._shadow ? Color.white : new Color(0.35f, 0.35f, 0.35f);
        }
    }
}

