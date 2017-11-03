using SimpleLocalization.Models;
using System;
using UnityEngine;

public class GameWindow : MonoBehaviour
{
    [SerializeField]
    private Button _continue;
    private Game _game;
    [SerializeField]
    private BoosterButton _health;
    [SerializeField]
    private BoosterButton _kill;
    [SerializeField]
    private GameObject _lock;
    [SerializeField]
    private TextMesh _lockText;
    [SerializeField]
    private Button _mainMenu;
    [SerializeField]
    private Button _restart;
    [SerializeField]
    private BoosterButton _strength;
    private float _timeScale;

    private void Awake()
    {
        this._health.Clicked += new Action<Button>(this.BoosterOnClicked);
        this._strength.Clicked += new Action<Button>(this.BoosterOnClicked);
        this._kill.Clicked += new Action<Button>(this.BoosterOnClicked);
        this._mainMenu.Clicked += new Action<Button>(this.MainMenuOnClicked);
        this._restart.Clicked += new Action<Button>(this.RestartOnClicked);
        this._continue.Clicked += new Action<Button>(this.ContinueOnClicked);
        this._lock.SetActive(false);
    }

    private void BoosterOnClicked(Button button)
    {
        BoosterButton button2 = (BoosterButton) button;
        if (button2.Count > 0)
        {
            this._game.ActivateBooster(button2.Type);
            PlayerSettings.AddBooster(button2.Type, -1);
            button2.UpdateCount();
            button2.IsEnabled = button2.Count > 0;
        }
        this.Hide();
    }

    private void ContinueOnClicked(Button button)
    {
        this.Hide();
    }

    public void Hide()
    {
        this._game.Pause.gameObject.SetActive(true);
        this._game.transform.localScale = (Vector3) (Vector3.one * this._timeScale);
        this._game.IsControlsAvailable = true;
        iTween.Resume(this._game.gameObject);
        base.gameObject.SetActive(false);
    }

    public void Init(Game game)
    {
        this._game = game;
        base.gameObject.SetActive(true);
        base.gameObject.SetActive(false);
        this._mainMenu.Text = (App.GameType != GameType.Single) ? Sl.GetValue("MAIN MENU") : Sl.GetValue("TO LEVELS");
    }

    private void MainMenuOnClicked(Button obj)
    {
        App.ReturnToLevelSelect = true;
        Application.LoadLevel(0);
    }

    private void OnDestroy()
    {
        this._mainMenu.Clicked -= new Action<Button>(this.MainMenuOnClicked);
        this._restart.Clicked -= new Action<Button>(this.RestartOnClicked);
        this._continue.Clicked -= new Action<Button>(this.ContinueOnClicked);
    }

    private void RestartOnClicked(Button button)
    {
        Application.LoadLevel(1);
    }

    public void SetLocked(bool locked, string text)
    {
        this._lock.SetActive(locked);
        this._lockText.text = text;
    }

    public void Show()
    {
        this._timeScale = this._game.transform.localScale.x;
        this._game.Pause.gameObject.SetActive(false);
        this._game.transform.localScale = Vector3.zero;
        this._game.IsControlsAvailable = false;
        iTween.Pause(this._game.gameObject);
        base.gameObject.SetActive(true);
    }
}

