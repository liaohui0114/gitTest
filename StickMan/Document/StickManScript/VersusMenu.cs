using System;
using UnityEngine;

public class VersusMenu : BaseMenu
{
    [SerializeField]
    private PropertyField _arena;
    [SerializeField]
    private PropertyField _p1Left;
    [SerializeField]
    private PropertyField _p1Right;
    [SerializeField]
    private PropertyField _p1Stickman;
    [SerializeField]
    private PropertyField _p2Left;
    [SerializeField]
    private PropertyField _p2Right;
    [SerializeField]
    private PropertyField _p2Stickman;
    [SerializeField]
    private Button _start;

    private void Awake()
    {
        this._start.Clicked += new Action<Button>(this.StartOnClicked);
    }

    public override void Init()
    {
        object[] args = new object[] { "scale", Vector3.zero, "time", 0.5f, "delay", 0.1f + UnityEngine.Random.Range((float) 0f, (float) 0.15f), "easetype", iTween.EaseType.easeOutElastic };
        iTween.ScaleFrom(this._p1Stickman.gameObject, iTween.Hash(args));
        object[] objArray2 = new object[] { "scale", Vector3.zero, "time", 0.5f, "delay", 0.1f + UnityEngine.Random.Range((float) 0f, (float) 0.15f), "easetype", iTween.EaseType.easeOutElastic };
        iTween.ScaleFrom(this._p1Left.gameObject, iTween.Hash(objArray2));
        object[] objArray3 = new object[] { "scale", Vector3.zero, "time", 0.5f, "delay", 0.1f + UnityEngine.Random.Range((float) 0f, (float) 0.15f), "easetype", iTween.EaseType.easeOutElastic };
        iTween.ScaleFrom(this._p1Right.gameObject, iTween.Hash(objArray3));
        object[] objArray4 = new object[] { "scale", Vector3.zero, "time", 0.5f, "delay", 0.1f + UnityEngine.Random.Range((float) 0f, (float) 0.15f), "easetype", iTween.EaseType.easeOutElastic };
        iTween.ScaleFrom(this._p2Stickman.gameObject, iTween.Hash(objArray4));
        object[] objArray5 = new object[] { "scale", Vector3.zero, "time", 0.5f, "delay", 0.1f + UnityEngine.Random.Range((float) 0f, (float) 0.15f), "easetype", iTween.EaseType.easeOutElastic };
        iTween.ScaleFrom(this._p2Left.gameObject, iTween.Hash(objArray5));
        object[] objArray6 = new object[] { "scale", Vector3.zero, "time", 0.5f, "delay", 0.1f + UnityEngine.Random.Range((float) 0f, (float) 0.15f), "easetype", iTween.EaseType.easeOutElastic };
        iTween.ScaleFrom(this._p2Right.gameObject, iTween.Hash(objArray6));
        object[] objArray7 = new object[] { "scale", Vector3.zero, "time", 0.5f, "delay", 0.1f + UnityEngine.Random.Range((float) 0f, (float) 0.15f), "easetype", iTween.EaseType.easeOutElastic };
        iTween.ScaleFrom(this._start.gameObject, iTween.Hash(objArray7));
        object[] objArray8 = new object[] { "scale", Vector3.zero, "time", 0.5f, "delay", 0.1f + UnityEngine.Random.Range((float) 0f, (float) 0.15f), "easetype", iTween.EaseType.easeOutElastic };
        iTween.ScaleFrom(this._arena.gameObject, iTween.Hash(objArray8));
    }

    private void StartOnClicked(Button button)
    {
        App.VersusSettings.P1Stickman = PropertyField.ParseStickmanType("Player1", this._p1Stickman.ValueName);
        App.VersusSettings.P1LeftWeapon = (WeaponType) (this._p1Left.Value - 1);
        App.VersusSettings.P1RigthtWeapon = (WeaponType) (this._p1Right.Value - 1);
        App.VersusSettings.P2Stickman = PropertyField.ParseStickmanType("Player2", this._p2Stickman.ValueName);
        App.VersusSettings.P2LeftWeapon = (WeaponType) (this._p2Left.Value - 1);
        App.VersusSettings.P2RigthtWeapon = (WeaponType) (this._p2Right.Value - 1);
        App.VersusSettings.Arena = (ArenaType) this._arena.Value;
    }
}

