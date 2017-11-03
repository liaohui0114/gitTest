using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CampaignMenu : BaseMenu
{
    [SerializeField]
    private CampaignIcon _campaignIconPrefab;
    private readonly List<CampaignIcon> _campaigns = new List<CampaignIcon>();
    private bool _hold;
    private bool _inited;
    [SerializeField]
    private TextMesh _money;
    private Vector2 _mouseStart;
    private float _offset;
    private Transform _parent;
    [SerializeField]
    private Vector2 _scrollZone;
    private CampaignIcon _selected;
    [CompilerGenerated]
    private static System.Func<CampaignIcon, float> <>f__am$cacheA;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(base.transform.position, new Vector3(this._scrollZone.x, this._scrollZone.y));
    }

    private void Start()
    {
        this._parent = new GameObject("CampaignScroll").transform;
        this._parent.parent = base.transform;
        this._parent.localPosition = Vector3.zero;
        this.UpdateCampaigns();
        this._inited = true;
    }

    private void Update()
    {
        Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            Rect rect = new Rect(base.transform.position.x - (this._scrollZone.x * 0.5f), base.transform.position.y - (this._scrollZone.y * 0.5f), this._scrollZone.x, this._scrollZone.y);
            if (rect.Contains(point))
            {
                this._mouseStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                this._hold = true;
            }
        }
        if (this._hold && Input.GetMouseButtonUp(0))
        {
            this._hold = false;
            if (<>f__am$cacheA == null)
            {
                <>f__am$cacheA = c => Mathf.Abs(c.transform.position.x);
            }
            CampaignIcon item = this._campaigns.OrderBy<CampaignIcon, float>(<>f__am$cacheA).First<CampaignIcon>();
            if (item == this._selected)
            {
                if (item.transform.position.x < -4f)
                {
                    item = this._campaigns[Mathf.Clamp(this._campaigns.IndexOf(item) + 1, 0, this._campaigns.Count - 1)];
                }
                else if (item.transform.position.x > 4f)
                {
                    item = this._campaigns[Mathf.Clamp(this._campaigns.IndexOf(item) - 1, 0, this._campaigns.Count - 1)];
                }
            }
            object[] args = new object[] { "x", -item.transform.position.x, "islocal", true, "time", 0.35f, "easetype", iTween.EaseType.easeOutBack };
            iTween.MoveAdd(this._parent.gameObject, iTween.Hash(args));
            this._offset = this._parent.localPosition.x - item.transform.position.x;
            this._selected = item;
        }
        if (this._hold)
        {
            Vector2 vector9 = point - this._mouseStart;
            this._parent.localPosition = new Vector3(this._offset + vector9.x, 0f, 0f);
        }
        foreach (CampaignIcon icon2 in this._campaigns)
        {
            icon2.transform.localScale = (Vector3) (Vector3.one * Mathf.Clamp((float) (1f - Mathf.Abs((float) (icon2.transform.position.x / 100f))), (float) 0.5f, (float) 1f));
        }
    }

    public void UpdateCampaigns()
    {
        foreach (CampaignIcon icon in this._campaigns)
        {
            UnityEngine.Object.Destroy(icon.gameObject);
        }
        this._campaigns.Clear();
        for (int i = 0; i < App.Campaigns.Count; i++)
        {
            CampaignIcon item = (CampaignIcon) UnityEngine.Object.Instantiate(this._campaignIconPrefab);
            item.transform.parent = this._parent;
            item.CampaignIndex = App.Campaigns.Keys[i];
            item.CanBeBought = item.CampaignIndex == (PlayerSettings.OpenedCampaign.Value + 1);
            item.IsEnabled = item.CanBeBought || (item.CampaignIndex <= PlayerSettings.OpenedCampaign.Value);
            item.transform.localPosition = new Vector3((float) (item.CampaignIndex * 40), 0f, 0f);
            this._campaigns.Add(item);
            if (!this._inited && (item.CampaignIndex == PlayerSettings.LastCampaign.Value))
            {
                this._offset = -item.transform.position.x;
                Transform transform = this._parent.transform;
                transform.position += (Vector3) (Vector3.right * this._offset);
                this._selected = item;
            }
        }
        this._money.text = PlayerSettings.Money.Value.ToString();
    }
}

