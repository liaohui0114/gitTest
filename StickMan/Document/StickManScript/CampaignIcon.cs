using SimpleLocalization.Models;
using System;
using UnityEngine;

public class CampaignIcon : Button
{
    [SerializeField]
    private SpriteRenderer _image;
    [SerializeField]
    private GameObject _locked;
    [SerializeField]
    private TextMesh _name;
    [SerializeField]
    private TextMesh _stars;
    [SerializeField]
    private GameObject _unlock;
    [SerializeField]
    private SpriteRenderer _unlockCoinsIcon;
    [SerializeField]
    private TextMesh _unlockCoinsText;
    [Space(10f)]
    public int CampaignIndex;
    public bool CanBeBought;
    public bool IsCoop;

    protected override void OnMouseUp()
    {
        if (base.IsEnabled && base._hold)
        {
            if (this.CanBeBought)
            {
                MessageBox.Show(Sl.GetValue("Unlock Campaign"), string.Format(Sl.GetValue("UnlockDesc"), this._name.text), delegate {
                    if (this.IsCoop)
                    {
                        PlayerSettings.OpenedCoopCampaign.SetAndSave(this.CampaignIndex);
                        PlayerSettings.OpenedCoopLevel.SetAndSave(0);
                        PlayerSettings.Money.SetAndSave(PlayerSettings.Money.Value - App.CoopCampaignUnlockCost);
                    }
                    else
                    {
                        PlayerSettings.OpenedCampaign.SetAndSave(this.CampaignIndex);
                        PlayerSettings.OpenedLevel.SetAndSave(0);
                        PlayerSettings.Money.SetAndSave(PlayerSettings.Money.Value - App.CampaignUnlockCost);
                    }
                    UnityEngine.Object.FindObjectOfType<CoopCampaignMenu>().UpdateCampaigns();
                    UnityEngine.Object.FindObjectOfType<CampaignMenu>().UpdateCampaigns();
                }, null, true, 0.15f, null);
            }
            else
            {
                App.CurrentCampaign = this.CampaignIndex;
                if (this.IsCoop)
                {
                    PlayerSettings.LastCoopCampaign.SetAndSave(this.CampaignIndex);
                }
                else
                {
                    PlayerSettings.LastCampaign.SetAndSave(this.CampaignIndex);
                }
                base.OnMouseUp();
            }
        }
    }

    private void Start()
    {
        Campaign campaign = !this.IsCoop ? App.Campaigns[this.CampaignIndex] : App.CoopCampaigns[this.CampaignIndex];
        this._name.text = Sl.GetValue(campaign.Name);
        Sprite sprite = Resources.Load<Sprite>((!this.IsCoop ? "Campaigns/" : "CoopCampaigns/") + this.CampaignIndex);
        if (sprite != null)
        {
            this._image.sprite = sprite;
        }
        this._locked.SetActive(false);
        if (this.CampaignIndex == -1)
        {
            this._stars.gameObject.SetActive(false);
            this._locked.SetActive(!this.IsCoop ? (PlayerSettings.OpenedCampaign.Value <= 0) : (PlayerSettings.OpenedCoopCampaign.Value <= 0));
            base.IsEnabled = !this._locked.activeSelf;
            if (!base.IsEnabled)
            {
                this._image.color = new Color(0.25f, 0.25f, 0.25f);
            }
            this._unlock.SetActive(false);
        }
        else
        {
            this._stars.gameObject.SetActive(true);
            int num = campaign.Levels.Count * 3;
            int num2 = 0;
            for (int i = 0; i < campaign.Levels.Count; i++)
            {
                num2 += !this.IsCoop ? PlayerSettings.GetLevelStarsCount(this.CampaignIndex, i) : PlayerSettings.GetCoopLevelStarsCount(this.CampaignIndex, i);
            }
            this._stars.text = num2 + "/" + num;
            this._unlock.SetActive(this.CanBeBought);
            if (this.CanBeBought)
            {
                int num4 = !this.IsCoop ? App.CampaignUnlockCost : App.CoopCampaignUnlockCost;
                this._image.color = new Color(0.25f, 0.25f, 0.25f);
                this._unlockCoinsText.text = num4.ToString();
                this._stars.gameObject.SetActive(false);
                if (PlayerSettings.Money.Value < num4)
                {
                    Color color = new Color(0.5f, 0.5f, 0.5f);
                    this._unlockCoinsText.color = color;
                    this._unlockCoinsIcon.color = color;
                    base.IsEnabled = false;
                }
            }
            if (!base.IsEnabled)
            {
                if (!this.CanBeBought)
                {
                    this._locked.SetActive(true);
                    this._image.color = new Color(0.25f, 0.25f, 0.25f);
                }
                this._stars.gameObject.SetActive(false);
            }
        }
    }
}

