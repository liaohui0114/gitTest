using SimpleLocalization.Models;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(TextMesh))]
public class Button : MonoBehaviour
{
    private Color _color;
    protected bool _hold;
    [SerializeField]
    private bool _isEnabled = true;
    private Vector3 _scale;
    private Vector3 _startMouse;
    public ButtonActions Action;
    public bool IsWithTween = true;
    protected TextMesh text;

    public event Action<Button> Clicked;

    protected virtual void Awake()
    {
        this._scale = base.transform.localScale;
        this.text = base.GetComponent<TextMesh>();
        this._color = this.text.color;
        this.IsEnabled = this._isEnabled;
    }

    private static T MoveTo<T>() where T: BaseMenu
    {
        T local = UnityEngine.Object.FindObjectOfType<T>();
        object[] args = new object[] { "x", local.transform.position.x, "y", local.transform.position.y, "time", 0.5f };
        iTween.MoveTo(Camera.main.gameObject, iTween.Hash(args));
        local.Init();
        return local;
    }

    protected virtual void OnMouseDown()
    {
        if (this._isEnabled)
        {
            if (this.IsWithTween)
            {
                iTween.ScaleTo(base.gameObject, (Vector3) (this._scale * 0.95f), 0.15f);
            }
            this._startMouse = Input.mousePosition;
            this._hold = true;
        }
    }

    protected virtual void OnMouseDrag()
    {
        if (this._hold)
        {
            Vector3 vector = Input.mousePosition - this._startMouse;
            if (vector.magnitude > 25f)
            {
                if (this.IsWithTween)
                {
                    iTween.ScaleTo(base.gameObject, this._scale, 0.15f);
                }
                this._hold = false;
            }
        }
    }

    protected virtual void OnMouseUp()
    {
        if (this._isEnabled && this._hold)
        {
            this._hold = false;
            if (this.Clicked != null)
            {
                this.Clicked(this);
            }
            if (this.IsWithTween)
            {
                iTween.ScaleTo(base.gameObject, this._scale, 0.15f);
            }
            switch (this.Action)
            {
                case ButtonActions.Main:
                    MoveTo<MainMenu>();
                    break;

                case ButtonActions.Campaign:
                    MoveTo<CampaignMenu>();
                    break;

                case ButtonActions.MenuScene:
                    Application.LoadLevel(0);
                    break;

                case ButtonActions.BattleScene:
                    Application.LoadLevel(1);
                    break;

                case ButtonActions.VersusBattleScene:
                    App.GameType = GameType.Versus;
                    Application.LoadLevel(1);
                    break;

                case ButtonActions.Pause:
                {
                    <OnMouseUp>c__AnonStoreyD yd = new <OnMouseUp>c__AnonStoreyD {
                        game = UnityEngine.Object.FindObjectOfType<Game>()
                    };
                    if (yd.game != null)
                    {
                        yd.game.transform.localScale = Vector3.zero;
                        yd.game.IsControlsAvailable = false;
                        MessageBox.Show("Pause", "Do you realy want \n to quit to main menu?", new System.Action(yd.<>m__E), new System.Action(yd.<>m__F), true, 0f, yd.game.GUICamera);
                    }
                    break;
                }
                case ButtonActions.Exit:
                    MessageBox.Show(Sl.GetValue("Exit Game"), Sl.GetValue("ExitDesc"), new System.Action(Application.Quit), null, true, 0.15f, null);
                    break;

                case ButtonActions.Options:
                    MoveTo<OptionsMenu>();
                    break;

                case ButtonActions.SinglePlayer:
                    MoveTo<SinglePlayerMenu>();
                    break;

                case ButtonActions.MultiPlayer:
                    MoveTo<MultiPlayerMenu>();
                    break;

                case ButtonActions.Versus:
                    MoveTo<VersusMenu>();
                    break;

                case ButtonActions.StartSingleEndless:
                    App.GameType = GameType.SingleEndless;
                    Application.LoadLevel(1);
                    break;

                case ButtonActions.StartMultipleEndless:
                    App.GameType = GameType.MultiEndless;
                    Application.LoadLevel(1);
                    break;

                case ButtonActions.Level:
                    MoveTo<LevelMenu>();
                    break;

                case ButtonActions.Shop:
                    MoveTo<ShopMenu>();
                    break;

                case ButtonActions.CoopCampaign:
                    MoveTo<CoopCampaignMenu>();
                    break;

                case ButtonActions.CoopLevel:
                    MoveTo<CoopLevelMenu>();
                    break;

                case ButtonActions.MoreGames:
                    Application.OpenURL("https://play.google.com/store/apps/developer?id=ViperGames");
                    try
                    {
                        GoogleAnalytics.SendEvent("Other", "Press More Games", null, 0x7fffffff);
                    }
                    catch (Exception exception)
                    {
                        Debug.LogException(exception);
                    }
                    break;
            }
        }
    }

    public bool IsEnabled
    {
        get => 
            this._isEnabled;
        set
        {
            this._isEnabled = value;
            this.text.color = !this._isEnabled ? Color.gray : this._color;
        }
    }

    public string Text
    {
        get => 
            this.text.text;
        set
        {
            this.text.text = value;
        }
    }

    [CompilerGenerated]
    private sealed class <OnMouseUp>c__AnonStoreyD
    {
        internal Game game;

        internal void <>m__E()
        {
            this.game.transform.localScale = Vector3.one;
            Application.LoadLevel(0);
        }

        internal void <>m__F()
        {
            this.game.transform.localScale = Vector3.one;
            this.game.IsControlsAvailable = true;
        }
    }
}

