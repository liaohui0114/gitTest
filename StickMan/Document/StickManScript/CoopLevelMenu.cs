using SimpleLocalization.Models;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CoopLevelMenu : BaseMenu
{
    [SerializeField]
    private TextMesh _campaignName;
    [SerializeField]
    private int _columns = 5;
    [SerializeField]
    private PropertyField _difficulty;
    [SerializeField]
    private float _elementSize = 1f;
    [SerializeField]
    private LevelIcon _levelIconPrefab;
    private readonly List<LevelIcon> _levelIcons = new List<LevelIcon>();
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
    private GameObject _random;
    [SerializeField]
    private Button _randomStartButton;
    [SerializeField]
    private int _rows = 3;

    private void Awake()
    {
        this._randomStartButton.Clicked += new Action<Button>(this.RandomStartButtonOnClicked);
    }

    public override void Init()
    {
        for (int i = 0; i < this._levelIcons.Count; i++)
        {
            UnityEngine.Object.Destroy(this._levelIcons[i].gameObject);
        }
        Campaign campaign = App.CoopCampaigns[App.CurrentCampaign];
        this._levelIcons.Clear();
        this._campaignName.text = Sl.GetValue(campaign.Name).ToUpper();
        if (App.CurrentCampaign == -1)
        {
            this._random.SetActive(true);
        }
        else
        {
            this._random.SetActive(false);
            float num2 = (-this._elementSize * this._columns) * 0.5f;
            float num3 = (this._elementSize * this._rows) * 0.5f;
            for (int j = 0; j < this._rows; j++)
            {
                for (int k = 0; k < this._columns; k++)
                {
                    int level = (this._columns * j) + k;
                    if (level > (campaign.Levels.Count - 1))
                    {
                        break;
                    }
                    LevelIcon item = (LevelIcon) UnityEngine.Object.Instantiate(this._levelIconPrefab);
                    item.transform.parent = base.transform;
                    item.transform.localPosition = new Vector3(num2 + (this._elementSize * (k + 0.5f)), num3 - (this._elementSize * (j + 0.5f)));
                    item.Number = level;
                    item.Stars = PlayerSettings.GetCoopLevelStars(App.CurrentCampaign, level);
                    if (App.CurrentCampaign < PlayerSettings.OpenedCoopCampaign.Value)
                    {
                        item.IsEnabled = true;
                    }
                    else
                    {
                        item.IsEnabled = level <= PlayerSettings.OpenedCoopLevel.Value;
                    }
                    object[] args = new object[] { "scale", Vector3.zero, "time", 0.5f, "delay", 0.1f + UnityEngine.Random.Range((float) 0f, (float) 0.15f), "easetype", iTween.EaseType.easeOutElastic };
                    iTween.ScaleFrom(item.gameObject, iTween.Hash(args));
                    this._levelIcons.Add(item);
                }
            }
        }
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = new Color(1f, 1f, 1f, 0.1f);
        Gizmos.matrix = Matrix4x4.TRS(base.transform.position, base.transform.rotation, base.transform.lossyScale);
        float num = (-this._elementSize * this._columns) * 0.5f;
        float num2 = (this._elementSize * this._rows) * 0.5f;
        for (int i = 0; i < this._columns; i++)
        {
            for (int j = 0; j < this._rows; j++)
            {
                Gizmos.DrawWireCube(new Vector3(num + (this._elementSize * (i + 0.5f)), num2 - (this._elementSize * (j + 0.5f))), new Vector3(this._elementSize, this._elementSize));
            }
        }
    }

    private void RandomStartButtonOnClicked(Button button)
    {
        App.GenerateRandomCoopCampaign(PropertyField.ParseStickmanType("Player1", this._p1Stickman.ValueName), PropertyField.ParseStickmanType("Player2", this._p2Stickman.ValueName), (WeaponType) (this._p1Left.Value - 1), (WeaponType) (this._p1Right.Value - 1), (WeaponType) (this._p2Left.Value - 1), (WeaponType) (this._p2Right.Value - 1), this._difficulty.Value);
        App.CurrentLevel = 0;
        App.IsCooperative = true;
    }
}

