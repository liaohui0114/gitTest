using SimpleLocalization.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Events;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    private AudioSource _audio;
    private float _cameraZ;
    private Campaign _campaign;
    private static bool _campaignIsCompleted;
    private readonly List<Stickman> _deadStickmen = new List<Stickman>();
    private int _emit;
    private int _enemiesKilled;
    private readonly List<ReplayEvent> _events = new List<ReplayEvent>();
    private bool _isReady;
    private Level _level;
    private bool _particlesOn;
    private static float _replayLength;
    private float _replayStartTime;
    private bool _soundsOn;
    private readonly Dictionary<int, StickmanTimeline> _stickmanTimelines = new Dictionary<int, StickmanTimeline>();
    private readonly List<Stickman> _stickmen = new List<Stickman>();
    private float _time;
    private static int _unityAdsCout;
    private bool _wasBooster;
    private readonly List<BaseWeapon> _weapons = new List<BaseWeapon>();
    private readonly Dictionary<int, WeaponTimeline> _weaponTimelines = new Dictionary<int, WeaponTimeline>();
    [CompilerGenerated]
    private static System.Func<Stickman, bool> <>f__am$cache42;
    [CompilerGenerated]
    private static System.Func<Stickman, bool> <>f__am$cache43;
    [CompilerGenerated]
    private static System.Func<Stickman, int> <>f__am$cache44;
    [CompilerGenerated]
    private static System.Func<Stickman, bool> <>f__am$cache45;
    [CompilerGenerated]
    private static System.Func<Stickman, bool> <>f__am$cache46;
    [CompilerGenerated]
    private static System.Func<Stickman, float> <>f__am$cache47;
    [CompilerGenerated]
    private static System.Func<Stickman, float> <>f__am$cache48;
    [CompilerGenerated]
    private static System.Func<Stickman, float> <>f__am$cache49;
    [CompilerGenerated]
    private static System.Func<Stickman, float> <>f__am$cache4A;
    [CompilerGenerated]
    private static System.Func<Stickman, bool> <>f__am$cache4B;
    public TextMesh BestScore;
    public AudioClip[] BlockAudioClips;
    public float BlowForce = 250f;
    public float BlowTime = 0.25f;
    public SpriteRenderer BonePrefab;
    public float CameraSmoothTime = 0.1f;
    [Space(10f)]
    public int CampaignIndex = -1;
    public GameObject CirclePrefab;
    public float CurrentTimestep;
    public AudioClip DeathAudioClip;
    public float FixedTimeStep = 0.02f;
    public FlyingText FlyingTextPrefab;
    public GameWindow GameWindow;
    public float Gravity = -30f;
    public Camera GUICamera;
    public AudioClip HeadAudioClip;
    public AudioClip[] HitAudioClips;
    public int IterationsCount = 6;
    public TextMesh Kills;
    public int LevelIndex = -1;
    public LevelWindow LevelWindow;
    public Camera MainCamera;
    public Button MainMenu;
    public TextMesh Money;
    public FlyingText MoneyFlyingTextPrefab;
    public MusicBox MusicBox;
    public Button NextLevel;
    public GameObject OldTV;
    public Button Pause;
    public vJoystick Player1Joystick;
    public Stickman Player1Stickman;
    public vJoystick Player2Joystick;
    public Stickman Player2Stickman;
    public GameObject QuadPrefab;
    public Button Replay;
    public GameObject ReplayIcon;
    public GameObject ReplayObjects;
    public Slider ReplaySpeedSlider;
    public AudioClip ShootAudioClip;
    public SpriteRenderer SkullBonePrefab;
    public float SlowMoScale = 0.25f;
    public AudioClip[] SwordAudioClips;
    public TextMesh Text;
    public TutorWindow TutorWindow;

    public void ActivateBooster(BoosterType type)
    {
        bool flag = this.Player1Stickman.CurrentHealth <= 0;
        if (App.IsCooperative)
        {
            flag = flag && (this.Player2Stickman.CurrentHealth <= 0);
        }
        if (!flag && !this._wasBooster)
        {
            try
            {
                GoogleAnalytics.SendEvent("Activate Booster", type.ToString(), null, 0x7fffffff);
            }
            catch (Exception exception)
            {
                UnityEngine.Debug.LogException(exception);
            }
            this._wasBooster = true;
            switch (type)
            {
                case BoosterType.Health:
                    this.Player1Stickman.Heal();
                    ParticlesManager.CreateParticle(ParticlesType.Health, (Vector3) this.Player1Stickman.Position, Vector3.zero);
                    if (App.IsCooperative)
                    {
                        this.Player2Stickman.Heal();
                        ParticlesManager.CreateParticle(ParticlesType.Health, (Vector3) this.Player2Stickman.Position, Vector3.zero);
                    }
                    break;

                case BoosterType.Strength:
                    this.Player1Stickman.Damage *= 2;
                    ParticlesManager.CreateParticle(ParticlesType.Strength, (Vector3) this.Player1Stickman.Position, Vector3.zero);
                    if (App.IsCooperative)
                    {
                        this.Player2Stickman.Damage *= 2;
                        ParticlesManager.CreateParticle(ParticlesType.Strength, (Vector3) this.Player2Stickman.Position, Vector3.zero);
                    }
                    break;

                case BoosterType.Kill:
                {
                    if (<>f__am$cache43 == null)
                    {
                        <>f__am$cache43 = s => !s.IsPlayer;
                    }
                    if (<>f__am$cache44 == null)
                    {
                        <>f__am$cache44 = s => s.CurrentHealth;
                    }
                    Stickman stickman = this._stickmen.Where<Stickman>(<>f__am$cache43).OrderByDescending<Stickman, int>(<>f__am$cache44).FirstOrDefault<Stickman>();
                    if (stickman != null)
                    {
                        stickman.Hit(stickman.CurrentHealth);
                    }
                    break;
                }
            }
        }
    }

    public void AddReplayEvent(ReplayEventType type, Vector2 position, object data = null)
    {
        this._events.Add(new ReplayEvent(Time.time - this._replayStartTime, type, position, data, 0.06666667f));
    }

    private void AddStickman(Stickman stickman, StickmanType type)
    {
        this._stickmen.Add(stickman);
        this._stickmanTimelines.Add(stickman.Id, new StickmanTimeline(type));
    }

    public void AddStickmanKey(Stickman stickman)
    {
        this._stickmanTimelines[stickman.Id].Keys.Add(new StickmanKey(Time.time - this._replayStartTime, stickman.GetPose()));
    }

    private void AddWeapon(BaseWeapon weapon, WeaponType type)
    {
        this._weapons.Add(weapon);
        this._weaponTimelines.Add(weapon.Id, new WeaponTimeline(type));
    }

    public void AddWeaponKey(BaseWeapon weapon)
    {
        this._weaponTimelines[weapon.Id].Keys.Add(new WeaponKey(Time.time - this._replayStartTime, weapon.GetPoses(), weapon.GetAdditionalPose()));
    }

    private void Awake()
    {
        int num = PlayerSettings.Gravity.Value;
        this.Gravity = -num * 5;
        int num2 = PlayerSettings.BlowForce.Value;
        this.BlowForce = num2 * 0x2710;
        int num3 = PlayerSettings.Slowmo.Value;
        this.SlowMoScale = 0.5f - (num3 * 0.05f);
        this.FixedTimeStep = (PlayerSettings.PrecisePhysics.Value != 1) ? 0.016f : 0.01f;
        this._soundsOn = PlayerSettings.Sounds.Value == 1;
        this._particlesOn = PlayerSettings.Particles.Value == 1;
        this.Pause.Clicked += new Action<Button>(this.PauseOnClicked);
        this.Replay.Clicked += new Action<Button>(this.ReplayOnClicked);
        this.GameWindow.Init(this);
        if (PlayerSettings.DontShowTutorial.Value || (App.Replay != null))
        {
            this.TutorWindow.Hide();
        }
        else if (App.GameType == GameType.Single)
        {
            this.TutorWindow.Show();
        }
        if (App.Replay == null)
        {
            AdManager.TryToShowInterstitial(null);
            AdManager.TryToLoadInterstitial(null);
            _unityAdsCout++;
            if (Advertisement.IsReady() && (_unityAdsCout == 6))
            {
                Advertisement.Show();
                _unityAdsCout = 0;
            }
        }
    }

    private void CameraUpdate()
    {
        if (<>f__am$cache46 == null)
        {
            <>f__am$cache46 = s => (s.CurrentHealth > 0) && s.gameObject.activeSelf;
        }
        List<Stickman> source = this._stickmen.Where<Stickman>(<>f__am$cache46).ToList<Stickman>();
        if (source.Count != 0)
        {
            if (<>f__am$cache47 == null)
            {
                <>f__am$cache47 = s => s.Position.x;
            }
            float num = source.Min<Stickman>(<>f__am$cache47) - 3f;
            if (<>f__am$cache48 == null)
            {
                <>f__am$cache48 = s => s.Position.x;
            }
            float num2 = source.Max<Stickman>(<>f__am$cache48) + 3f;
            if (<>f__am$cache49 == null)
            {
                <>f__am$cache49 = s => s.Position.y;
            }
            float num3 = source.Max<Stickman>(<>f__am$cache49) + 3f;
            if (<>f__am$cache4A == null)
            {
                <>f__am$cache4A = s => s.Position.y;
            }
            float num4 = source.Min<Stickman>(<>f__am$cache4A) - 3f;
            float num5 = num2 - num;
            float num6 = num3 - num4;
            float currentVelocity = 0f;
            Vector3 zero = Vector3.zero;
            Vector3 target = new Vector3((num + num2) * 0.5f, (num3 + num4) * 0.5f, this._cameraZ);
            float num8 = 0.5f * ((num5 <= num6) ? (num6 * 1.5f) : (num5 * 0.85f));
            if (num8 < 20f)
            {
                num8 = 20f;
            }
            this.MainCamera.orthographicSize = Mathf.SmoothDamp(this.MainCamera.orthographicSize, num8, ref currentVelocity, this.CameraSmoothTime * Time.timeScale, 300f);
            this.MainCamera.transform.position = Vector3.SmoothDamp(this.MainCamera.transform.position, target, ref zero, this.CameraSmoothTime * Time.timeScale, 300f);
        }
    }

    [ContextMenu("Complete Level")]
    private void CompleteLevel()
    {
        if (<>f__am$cache45 == null)
        {
            <>f__am$cache45 = s => !s.IsPlayer;
        }
        IEnumerator<Stickman> enumerator = this._stickmen.Where<Stickman>(<>f__am$cache45).GetEnumerator();
        try
        {
            while (enumerator.MoveNext())
            {
                Stickman current = enumerator.Current;
                current.Hit(current.CurrentHealth);
            }
        }
        finally
        {
            if (enumerator == null)
            {
            }
            enumerator.Dispose();
        }
    }

    private void CpuControl()
    {
        if (<>f__am$cache4B == null)
        {
            <>f__am$cache4B = s => s.IsPlayer && (s.CurrentHealth > 0);
        }
        List<Stickman> source = this._stickmen.Where<Stickman>(<>f__am$cache4B).ToList<Stickman>();
        List<Stickman> list2 = (from s in this._stickmen
            where (!s.IsPlayer && (s != this.Player1Stickman)) && (s != this.Player2Stickman)
            select s).ToList<Stickman>();
        for (int i = 0; i < list2.Count; i++)
        {
            <CpuControl>c__AnonStoreyF yf = new <CpuControl>c__AnonStoreyF {
                cpuStickman = list2[i]
            };
            if (yf.cpuStickman.CurrentHealth <= 0)
            {
                this._enemiesKilled++;
                base.StartCoroutine(this.Delayed(new System.Action(yf.<>m__1F), 5f));
                if ((App.GameType == GameType.Single) && (App.CurrentCampaign > -1))
                {
                    PlayerSettings.Money.SetAndSave(PlayerSettings.Money.Value + yf.cpuStickman.Money);
                    this.Money.text = PlayerSettings.Money.Value.ToString();
                    FlyingText text = (FlyingText) UnityEngine.Object.Instantiate(this.MoneyFlyingTextPrefab);
                    text.TextMesh.text = yf.cpuStickman.Money.ToString();
                    text.transform.position = ((Vector3) yf.cpuStickman.Position) - new Vector3(0f, 0f, 1f);
                }
                this._deadStickmen.Add(yf.cpuStickman);
                this._stickmen.Remove(yf.cpuStickman);
            }
            else
            {
                Stickman stickman = source.OrderBy<Stickman, float>(new System.Func<Stickman, float>(yf.<>m__20)).FirstOrDefault<Stickman>();
                Collider[] colliderArray = Physics.OverlapSphere((Vector3) yf.cpuStickman.Position, (float) (20 + yf.cpuStickman.Difficulty));
                if (((Time.frameCount % 120) == 0) && (UnityEngine.Random.Range(0, 100) > (yf.cpuStickman.Difficulty * 10)))
                {
                    yf.cpuStickman.TimeToWait = 1f;
                }
                if (((stickman == null) || (stickman.CurrentHealth <= 0)) || (yf.cpuStickman.TimeToWait > 0f))
                {
                    if (colliderArray.Length == 0)
                    {
                        yf.cpuStickman.Move(Vector2.up);
                    }
                }
                else
                {
                    Vector2 zero = Vector2.zero;
                    int num2 = 30;
                    Vector2 vector2 = stickman.Position - yf.cpuStickman.Position;
                    if (vector2.y > -20f)
                    {
                        zero.y = 1f;
                    }
                    zero.x = vector2.x;
                    if (((Mathf.Abs(vector2.magnitude) < num2) && (yf.cpuStickman.Velocity.magnitude > 20f)) && yf.cpuStickman.IsUseRoll)
                    {
                        zero = new Vector2(-vector2.x, -1f);
                    }
                    for (int j = 0; j < colliderArray.Length; j++)
                    {
                        if (colliderArray[j].GetComponent<DamagableObject>() != null)
                        {
                            float num4 = Mathf.Abs((float) (colliderArray[j].transform.position.x - yf.cpuStickman.Position.x));
                            float num5 = Mathf.Abs((float) (colliderArray[j].transform.position.y - yf.cpuStickman.Position.y));
                            if (num4 > num5)
                            {
                                if (colliderArray[j].transform.position.x >= yf.cpuStickman.Position.x)
                                {
                                    zero.x = -1f;
                                }
                                else
                                {
                                    zero.x = 1f;
                                }
                            }
                            else if (colliderArray[j].transform.position.y >= yf.cpuStickman.Position.y)
                            {
                                zero.y = -1f;
                            }
                            else
                            {
                                zero.y = 1f;
                            }
                            UnityEngine.Debug.DrawLine((Vector3) yf.cpuStickman.Position, colliderArray[j].transform.position);
                        }
                    }
                    yf.cpuStickman.Move(zero);
                }
            }
        }
    }

    public void CreateBlockParticles(Vector2 position)
    {
        if (this._particlesOn)
        {
            ParticlesManager.CreateParticle(ParticlesType.Block, (Vector3) position, Vector3.zero);
            this.AddReplayEvent(ReplayEventType.BlockParticles, position, null);
        }
    }

    public void CreateHitParticles(Vector2 position, bool withCircle)
    {
        if (this._particlesOn)
        {
            ParticlesManager.CreateParticle(ParticlesType.Blood, (Vector3) position, Vector3.zero);
            if (withCircle)
            {
                ParticlesManager.CreateParticle(ParticlesType.HitCircle, (Vector3) position, Vector3.zero);
            }
            this.AddReplayEvent(ReplayEventType.HitParticles, position, withCircle);
        }
    }

    public FlyingText CreateText(string text, Color color, Vector2 position, int size = 0, float time = 0, float moveSpeed = -1, float moveTime = 0, float fadeDelay = 0)
    {
        FlyingText text2 = (FlyingText) UnityEngine.Object.Instantiate(this.FlyingTextPrefab);
        text2.TextMesh.text = text;
        text2.transform.position = (Vector3) position;
        text2.TextMesh.color = color;
        if (size > 0)
        {
            text2.TextMesh.fontSize = size;
        }
        if (time > 0f)
        {
            text2.LifeTime = time;
        }
        if (moveSpeed > -1f)
        {
            text2.MoveSpeed = moveSpeed;
        }
        if (moveTime > 0f)
        {
            text2.MoveTime = moveTime;
        }
        if (fadeDelay > 0f)
        {
            text2.FadeDelay = fadeDelay;
        }
        return text2;
    }

    [DebuggerHidden]
    private IEnumerator Delayed(System.Action action, float delay) => 
        new <Delayed>c__Iterator5 { 
            delay = delay,
            action = action,
            <$>delay = delay,
            <$>action = action
        };

    private void FixedUpdate()
    {
        if (this._isReady)
        {
            this.PlayerControl();
            this.CpuControl();
        }
    }

    private Vector2 GetFreePosition()
    {
        Vector2 vector = new Vector2();
        for (int i = 0; i < 10; i++)
        {
            vector = new Vector3((float) UnityEngine.Random.Range(-35, 0x23), (float) UnityEngine.Random.Range(3, 40));
            bool flag = true;
            for (int j = 0; j < this._stickmen.Count; j++)
            {
                Vector2 vector2 = vector - this._stickmen[j].Position;
                if (vector2.magnitude <= 15f)
                {
                    flag = false;
                    break;
                }
            }
            if (flag)
            {
                return vector;
            }
        }
        return vector;
    }

    private void GiveWeaponL(Stickman stickman, WeaponType weaponType)
    {
        if (weaponType != WeaponType.None)
        {
            BaseWeapon weapon = App.CreateWeapon(weaponType);
            weapon.transform.position = stickman.WeaponLTransform.position;
            weapon.transform.rotation = stickman.WeaponLTransform.rotation;
            this.AddWeapon(weapon, weaponType);
        }
    }

    private void GiveWeaponR(Stickman stickman, WeaponType weaponType)
    {
        if (weaponType != WeaponType.None)
        {
            BaseWeapon weapon = App.CreateWeapon(weaponType);
            weapon.transform.position = stickman.WeaponRTransform.position;
            weapon.transform.rotation = stickman.WeaponRTransform.rotation;
            this.AddWeapon(weapon, weaponType);
        }
    }

    [ContextMenu("Health Booster")]
    private void HealthBooster()
    {
        this.ActivateBooster(BoosterType.Health);
    }

    [ContextMenu("Kill Booster")]
    private void KillBooster()
    {
        this.ActivateBooster(BoosterType.Kill);
    }

    private void LateUpdate()
    {
        this.CameraUpdate();
    }

    private void LoadReplay()
    {
        this.StopReplay();
        Replay replay = new Replay();
        replay.StickmanTimelines.AddRange(this._stickmanTimelines.Values);
        replay.WeaponTimelines.AddRange(this._weaponTimelines.Values);
        replay.EventsTimeline.Keys.AddRange(this._events);
        App.Replay = replay;
        Application.LoadLevel(1);
    }

    private void OnDestroy()
    {
        this.Pause.Clicked -= new Action<Button>(this.PauseOnClicked);
        this.Replay.Clicked -= new Action<Button>(this.ReplayOnClicked);
        this.ReplaySpeedSlider.onValueChanged.RemoveAllListeners();
    }

    private void PauseOnClicked(Button button)
    {
        if (App.GameType != GameType.Single)
        {
            this.GameWindow.SetLocked(true, Sl.GetValue("BoostersUnavailable"));
        }
        else if (this._wasBooster)
        {
            this.GameWindow.SetLocked(true, Sl.GetValue("OnlyOneBooster"));
        }
        this.GameWindow.Show();
    }

    public void PlayBlockSound()
    {
        if (this._soundsOn)
        {
            AudioClip clip = this.BlockAudioClips[UnityEngine.Random.Range(0, this.BlockAudioClips.Length)];
            this._audio.PlayOneShot(clip);
        }
    }

    public void PlayDeathSound()
    {
        if (this._soundsOn)
        {
            this._audio.PlayOneShot(this.DeathAudioClip);
        }
    }

    private void PlayerControl()
    {
        if ((this.Player1Stickman != null) && (this.Player1Stickman.CurrentHealth > 0))
        {
            Vector2 direction = new Vector2();
            if ((this.Player1Joystick != null) && this.Player1Joystick.gameObject.activeSelf)
            {
                direction = this.Player1Joystick.Value;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                direction += new Vector2(-1f, 0f);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                direction += new Vector2(1f, 0f);
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                direction += new Vector2(0f, 1f);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                direction += new Vector2(0f, -1f);
            }
            this.Player1Stickman.Move(direction);
        }
        if ((this.Player2Stickman != null) && (this.Player2Stickman.CurrentHealth > 0))
        {
            Vector2 vector2 = new Vector2();
            if ((this.Player2Joystick != null) && this.Player2Joystick.gameObject.activeSelf)
            {
                vector2 = this.Player2Joystick.Value;
            }
            if (Input.GetKey(KeyCode.A))
            {
                vector2 += new Vector2(-1f, 0f);
            }
            if (Input.GetKey(KeyCode.D))
            {
                vector2 += new Vector2(1f, 0f);
            }
            if (Input.GetKey(KeyCode.W))
            {
                vector2 += new Vector2(0f, 1f);
            }
            if (Input.GetKey(KeyCode.S))
            {
                vector2 += new Vector2(0f, -1f);
            }
            this.Player2Stickman.Move(vector2);
        }
    }

    public void PlayHitSound(bool head = false)
    {
        if (this._soundsOn)
        {
            if (head)
            {
                this._audio.PlayOneShot(this.HeadAudioClip);
            }
            else
            {
                AudioClip clip = this.HitAudioClips[UnityEngine.Random.Range(0, this.HitAudioClips.Length)];
                this._audio.PlayOneShot(clip);
            }
        }
    }

    public void PlayShootSound()
    {
        if (this._soundsOn)
        {
            this._audio.PlayOneShot(this.ShootAudioClip);
        }
    }

    public void PlaySwordSound()
    {
        if (this._soundsOn)
        {
            AudioClip clip = this.SwordAudioClips[UnityEngine.Random.Range(0, this.SwordAudioClips.Length)];
            this._audio.PlayOneShot(clip);
        }
    }

    [DebuggerHidden]
    private IEnumerator RecordReplay(float frequency) => 
        new <RecordReplay>c__Iterator6 { 
            frequency = frequency,
            <$>frequency = frequency,
            <>f__this = this
        };

    private void ReplayOnClicked(Button button)
    {
        if (App.Replay != null)
        {
            App.Replay = null;
            if (_campaignIsCompleted)
            {
                App.ReturnToLevelSelect = true;
                Application.LoadLevel(0);
            }
            else
            {
                Application.LoadLevel(1);
            }
        }
        else
        {
            this.LoadReplay();
        }
    }

    private void ReplayUpdate()
    {
        if (this._stickmen.Count == App.Replay.StickmanTimelines.Count)
        {
            for (int i = 0; i < App.Replay.StickmanTimelines.Count; i++)
            {
                StickmanTimeline timeline = App.Replay.StickmanTimelines[i];
                Stickman stickman = this._stickmen[i];
                if (timeline.Keys[0].Time <= this._time)
                {
                    if (timeline.Keys[timeline.Keys.Count - 1].Time < this._time)
                    {
                        if (stickman.gameObject.activeSelf)
                        {
                            stickman.gameObject.SetActive(false);
                        }
                        continue;
                    }
                    if (!stickman.gameObject.activeSelf)
                    {
                        stickman.gameObject.SetActive(true);
                    }
                    StickmanKey key = timeline.Keys[0];
                    StickmanKey key2 = timeline.Keys[timeline.Keys.Count - 1];
                    for (int m = 1; m < timeline.Keys.Count; m++)
                    {
                        StickmanKey key3 = timeline.Keys[m];
                        if (this._time < key3.Time)
                        {
                            key = timeline.Keys[m - 1];
                            key2 = timeline.Keys[m];
                            break;
                        }
                    }
                    float time = (this._time - key.Time) / (key2.Time - key.Time);
                    stickman.SetPose(key.Pose, key2.Pose, time);
                }
            }
            for (int j = 0; j < App.Replay.WeaponTimelines.Count; j++)
            {
                WeaponTimeline timeline2 = App.Replay.WeaponTimelines[j];
                BaseWeapon weapon = this._weapons[j];
                if (timeline2.Keys[0].Time <= this._time)
                {
                    if (timeline2.Keys[timeline2.Keys.Count - 1].Time < this._time)
                    {
                        if (weapon.gameObject.activeSelf)
                        {
                            weapon.gameObject.SetActive(false);
                        }
                        continue;
                    }
                    if (!weapon.gameObject.activeSelf)
                    {
                        weapon.gameObject.SetActive(true);
                    }
                    WeaponKey key4 = timeline2.Keys[0];
                    WeaponKey key5 = timeline2.Keys[timeline2.Keys.Count - 1];
                    for (int n = 1; n < timeline2.Keys.Count; n++)
                    {
                        WeaponKey key6 = timeline2.Keys[n];
                        if (this._time < key6.Time)
                        {
                            key4 = timeline2.Keys[n - 1];
                            key5 = timeline2.Keys[n];
                            break;
                        }
                    }
                    float num6 = (this._time - key4.Time) / (key5.Time - key4.Time);
                    weapon.SetPoses(key4.Poses, key5.Poses, num6);
                    weapon.SetAdditionalPose(key4.Additional, key5.Additional, num6);
                }
            }
            for (int k = 0; k < App.Replay.EventsTimeline.Keys.Count; k++)
            {
                ReplayEvent item = App.Replay.EventsTimeline.Keys[k];
                if (((this._time >= item.Time) && (this._time <= (item.Time + item.Duration))) && !this._events.Contains(item))
                {
                    this._events.Add(item);
                    switch (item.Type)
                    {
                        case ReplayEventType.HitParticles:
                            this.CreateHitParticles(item.Position, (bool) item.Data);
                            break;

                        case ReplayEventType.BlockParticles:
                            goto Label_0404;
                    }
                }
                continue;
            Label_0404:
                this.CreateBlockParticles(item.Position);
            }
            this._time += Time.deltaTime;
            if (this._time >= _replayLength)
            {
                Application.LoadLevel(1);
            }
        }
    }

    public void SlowMo(bool twice = false)
    {
        iTween.Stop(base.gameObject);
        if ((App.GameType == GameType.SingleEndless) || (App.GameType == GameType.MultiEndless))
        {
            twice = false;
        }
        base.transform.localScale = (Vector3) ((Vector3.one * this.SlowMoScale) * (!twice ? 1f : 0.5f));
        object[] args = new object[] { "scale", Vector3.one, "delay", 0.25, "time", 0.15f };
        iTween.ScaleTo(base.gameObject, iTween.Hash(args));
    }

    [DebuggerHidden]
    private IEnumerator Start() => 
        new <Start>c__Iterator4 { <>f__this = this };

    private void StopReplay()
    {
        base.StopAllCoroutines();
    }

    [ContextMenu("Strength Booster")]
    private void StrengthBooster()
    {
        this.ActivateBooster(BoosterType.Strength);
    }

    private void Update()
    {
        <Update>c__AnonStoreyE ye = new <Update>c__AnonStoreyE {
            <>f__this = this
        };
        if (this.IterationsCount != Physics.solverIterationCount)
        {
            Physics.solverIterationCount = this.IterationsCount;
        }
        if (this.Gravity != Physics.gravity.y)
        {
            Physics.gravity = new Vector3(0f, this.Gravity, 0f);
        }
        if (base.transform.localScale.x != Time.timeScale)
        {
            Time.timeScale = base.transform.localScale.x;
            if (Time.timeScale > 0f)
            {
                Time.fixedDeltaTime = this.FixedTimeStep * Time.timeScale;
                this.CurrentTimestep = Time.fixedDeltaTime;
            }
        }
        if (App.Replay != null)
        {
            this.ReplayUpdate();
        }
        if (!this.IsOver && this._isReady)
        {
            if (App.GameType == GameType.Single)
            {
                this._time += Time.deltaTime;
                this.BestScore.text = Sl.GetValue("Time:") + " " + ((int) this._time);
                if ((this._level.Type == LevelType.SurviveTime) && (this._time >= this._level.TypeValue))
                {
                    this.BestScore.color = new Color(0.5f, 1f, 0.5f);
                }
                if (this._level.Type == LevelType.KillCountOfEnemies)
                {
                    this.Kills.text = Sl.GetValue("Kills:") + " " + this._enemiesKilled;
                    if (this._enemiesKilled >= this._level.TypeValue)
                    {
                        this.Kills.color = new Color(0.5f, 1f, 0.5f);
                    }
                }
            }
            bool flag = this.Player1Stickman.CurrentHealth <= 0;
            bool flag2 = (this.Player2Stickman != null) && (this.Player2Stickman.CurrentHealth <= 0);
            if (<>f__am$cache42 == null)
            {
                <>f__am$cache42 = s => !s.IsPlayer && (s.CurrentHealth > 0);
            }
            bool flag3 = this._stickmen.Count<Stickman>(<>f__am$cache42) == 0;
            if ((App.GameType != GameType.SingleEndless) && (App.GameType != GameType.MultiEndless))
            {
                if (flag3 && (this._level != null))
                {
                    if (this._emit < this._level.EmitEnemies.Count)
                    {
                        List<StickmanDesc> list = this._level.EmitEnemies[this._emit];
                        for (int i = 0; i < list.Count; i++)
                        {
                            StickmanType type2 = list[i].Type;
                            Stickman stickman2 = App.CreateStickman(type2);
                            stickman2.transform.position = (Vector3) this.GetFreePosition();
                            stickman2.IsPlayer = false;
                            stickman2.Health = Mathf.RoundToInt((stickman2.Health * this._campaign.EnemyHealthModifier) * list[i].HealthModifier);
                            this.AddStickman(stickman2, type2);
                            this.GiveWeaponL(stickman2, list[i].LeftWeapon);
                            this.GiveWeaponR(stickman2, list[i].RightWeapon);
                        }
                        this._emit++;
                        return;
                    }
                    if (this._level.EndlessEnemies.Count > 0)
                    {
                        for (int j = 0; j < this._level.EndlessEnemies.Count; j++)
                        {
                            StickmanDesc desc = this._level.EndlessEnemies[j];
                            StickmanType type3 = desc.Type;
                            Stickman stickman3 = App.CreateStickman(type3);
                            stickman3.transform.position = (Vector3) this.GetFreePosition();
                            stickman3.IsPlayer = false;
                            stickman3.Health = Mathf.RoundToInt((stickman3.Health * this._campaign.EnemyHealthModifier) * desc.HealthModifier);
                            this.AddStickman(stickman3, type3);
                            this.GiveWeaponL(stickman3, desc.LeftWeapon);
                            this.GiveWeaponR(stickman3, desc.RightWeapon);
                        }
                        return;
                    }
                }
                if (App.IsCooperative)
                {
                    if (!flag3 && (!flag || !flag2))
                    {
                        return;
                    }
                }
                else if (!flag3 && !flag)
                {
                    return;
                }
                this.IsOver = true;
                ye.completed = false;
                bool flag4 = flag3 && flag;
                if (App.GameType != GameType.Single)
                {
                    if (App.GameType == GameType.Versus)
                    {
                        ye.completed = !flag;
                    }
                }
                else
                {
                    if (this._level != null)
                    {
                        switch (this._level.Type)
                        {
                            case LevelType.KillThemAll:
                                ye.completed = flag3 && (!flag || !flag2);
                                break;

                            case LevelType.KillCountOfEnemies:
                                ye.completed = this._enemiesKilled >= this._level.TypeValue;
                                break;

                            case LevelType.SurviveTime:
                                ye.completed = this._time >= this._level.TypeValue;
                                break;
                        }
                    }
                    base.StartCoroutine(this.Delayed(new System.Action(ye.<>m__14), 0.35f));
                }
                if (!ye.completed)
                {
                    this.Text.text = !flag4 ? Sl.GetValue("GameLOSE!") : Sl.GetValue("GameDRAW!");
                    this.Text.transform.localScale = (Vector3) (Vector3.one * 0.25f);
                    iTween.FadeTo(this.Text.gameObject, 1f, Time.timeScale * 0.25f);
                    iTween.ScaleTo(this.Text.gameObject, (Vector3) (Vector3.one * 2f), Time.timeScale * 0.5f);
                    if (App.GameType == GameType.Versus)
                    {
                        this.Text.text = !flag4 ? Sl.GetValue("GameP2WINS!") : Sl.GetValue("GameDRAW!");
                        this.NextLevel.Text = Sl.GetValue("RETRY");
                        iTween.ScaleTo(this.MainMenu.gameObject, Vector3.one, Time.timeScale * 0.5f);
                        iTween.ScaleTo(this.NextLevel.gameObject, Vector3.one, Time.timeScale * 0.5f);
                    }
                    else
                    {
                        try
                        {
                            if (App.IsCooperative)
                            {
                                GoogleAnalytics.SendEvent("Coop Level Not Completed", this._campaign.Name + " Level " + (App.CurrentLevel + 1), null, 0x7fffffff);
                            }
                            else
                            {
                                GoogleAnalytics.SendEvent("Level Not Completed", this._campaign.Name + " Level " + (App.CurrentLevel + 1), null, 0x7fffffff);
                            }
                        }
                        catch (Exception exception3)
                        {
                            UnityEngine.Debug.LogException(exception3);
                        }
                    }
                }
                else
                {
                    if (App.GameType == GameType.Single)
                    {
                        if (!App.IsCooperative)
                        {
                            PlayerSettings.SetLevelStars(App.CurrentCampaign, App.CurrentLevel, 0, 1);
                            if (!this._wasBooster)
                            {
                                PlayerSettings.SetLevelStars(App.CurrentCampaign, App.CurrentLevel, 1, 1);
                            }
                            switch (this._level.Star3Type)
                            {
                                case Star3Type.Health:
                                    if (((this.Player1Stickman.CurrentHealth * 100f) / ((float) this.Player1Stickman.Health)) >= this._level.Star3Value)
                                    {
                                        PlayerSettings.SetLevelStars(App.CurrentCampaign, App.CurrentLevel, 2, 1);
                                    }
                                    break;

                                case Star3Type.MoreThanTime:
                                    if (this._time >= this._level.Star3Value)
                                    {
                                        PlayerSettings.SetLevelStars(App.CurrentCampaign, App.CurrentLevel, 2, 1);
                                    }
                                    break;

                                case Star3Type.LessThanTime:
                                    if (((int) this._time) <= this._level.Star3Value)
                                    {
                                        PlayerSettings.SetLevelStars(App.CurrentCampaign, App.CurrentLevel, 2, 1);
                                    }
                                    break;

                                case Star3Type.EnemiesCount:
                                    if (this._enemiesKilled >= this._level.Star3Value)
                                    {
                                        PlayerSettings.SetLevelStars(App.CurrentCampaign, App.CurrentLevel, 2, 1);
                                    }
                                    break;
                            }
                            try
                            {
                                GoogleAnalytics.SendEvent("Level Completed", this._campaign.Name + " Level " + (App.CurrentLevel + 1), "Stars " + PlayerSettings.GetLevelStarsCount(App.CurrentCampaign, App.CurrentLevel), 0x7fffffff);
                            }
                            catch (Exception exception2)
                            {
                                UnityEngine.Debug.LogException(exception2);
                            }
                            App.CurrentLevel++;
                            if ((App.CurrentCampaign == PlayerSettings.OpenedCampaign.Value) && (App.CurrentLevel > PlayerSettings.OpenedLevel.Value))
                            {
                                PlayerSettings.OpenedLevel.SetAndSave(App.CurrentLevel);
                            }
                            this.Text.transform.localScale = (Vector3) (Vector3.one * 0.25f);
                            if (App.CurrentLevel >= App.Campaigns[App.CurrentCampaign].Levels.Count)
                            {
                                if (PlayerSettings.OpenedCampaign.Value < (App.CurrentCampaign + 1))
                                {
                                    PlayerSettings.OpenedCampaign.SetAndSave(App.CurrentCampaign + 1);
                                    PlayerSettings.OpenedLevel.SetAndSave(0);
                                }
                                this.Text.text = string.Format(Sl.GetValue("GameCampaignCompleted!"), Sl.GetValue(this._campaign.Name));
                                this.Text.fontSize = Mathf.RoundToInt(this.Text.fontSize * 0.75f);
                                _campaignIsCompleted = true;
                            }
                            else
                            {
                                this.Text.text = Sl.GetValue("GameWIN!");
                            }
                        }
                        else
                        {
                            PlayerSettings.SetCoopLevelStars(App.CurrentCampaign, App.CurrentLevel, 0, 1);
                            if (!this._wasBooster)
                            {
                                PlayerSettings.SetCoopLevelStars(App.CurrentCampaign, App.CurrentLevel, 1, 1);
                            }
                            switch (this._level.Star3Type)
                            {
                                case Star3Type.Health:
                                    if ((((this.Player1Stickman.CurrentHealth * 100f) / ((float) this.Player1Stickman.Health)) >= this._level.Star3Value) || (((this.Player2Stickman.CurrentHealth * 100f) / ((float) this.Player1Stickman.Health)) >= this._level.Star3Value))
                                    {
                                        PlayerSettings.SetCoopLevelStars(App.CurrentCampaign, App.CurrentLevel, 2, 1);
                                    }
                                    break;

                                case Star3Type.MoreThanTime:
                                    if (this._time >= this._level.Star3Value)
                                    {
                                        PlayerSettings.SetCoopLevelStars(App.CurrentCampaign, App.CurrentLevel, 2, 1);
                                    }
                                    break;

                                case Star3Type.LessThanTime:
                                    if (((int) this._time) <= this._level.Star3Value)
                                    {
                                        PlayerSettings.SetCoopLevelStars(App.CurrentCampaign, App.CurrentLevel, 2, 1);
                                    }
                                    break;

                                case Star3Type.EnemiesCount:
                                    if (this._enemiesKilled >= this._level.Star3Value)
                                    {
                                        PlayerSettings.SetCoopLevelStars(App.CurrentCampaign, App.CurrentLevel, 2, 1);
                                    }
                                    break;
                            }
                            try
                            {
                                GoogleAnalytics.SendEvent("Coop Level Completed", this._campaign.Name + " Level " + (App.CurrentLevel + 1), "Stars " + PlayerSettings.GetLevelStarsCount(App.CurrentCampaign, App.CurrentLevel), 0x7fffffff);
                            }
                            catch (Exception exception)
                            {
                                UnityEngine.Debug.LogException(exception);
                            }
                            App.CurrentLevel++;
                            if ((App.CurrentCampaign == PlayerSettings.OpenedCoopCampaign.Value) && (App.CurrentLevel > PlayerSettings.OpenedCoopLevel.Value))
                            {
                                PlayerSettings.OpenedCoopLevel.SetAndSave(App.CurrentLevel);
                            }
                            this.Text.transform.localScale = (Vector3) (Vector3.one * 0.25f);
                            if (App.CurrentLevel >= App.CoopCampaigns[App.CurrentCampaign].Levels.Count)
                            {
                                if (PlayerSettings.OpenedCoopCampaign.Value < (App.CurrentCampaign + 1))
                                {
                                    PlayerSettings.OpenedCoopCampaign.SetAndSave(App.CurrentCampaign + 1);
                                    PlayerSettings.OpenedCoopLevel.SetAndSave(0);
                                }
                                this.Text.text = string.Format(Sl.GetValue("GameCampaignCompleted!"), Sl.GetValue(this._campaign.Name));
                                this.Text.fontSize = Mathf.RoundToInt(this.Text.fontSize * 0.75f);
                                _campaignIsCompleted = true;
                            }
                            else
                            {
                                this.Text.text = Sl.GetValue("GameWIN!");
                            }
                        }
                    }
                    iTween.FadeTo(this.Text.gameObject, 1f, Time.timeScale * 0.25f);
                    iTween.ScaleTo(this.Text.gameObject, (Vector3) (Vector3.one * 2f), Time.timeScale * 0.5f);
                    if (App.GameType == GameType.Versus)
                    {
                        this.Text.text = Sl.GetValue("GameP1WINS!");
                        this.NextLevel.Text = Sl.GetValue("RETRY");
                        iTween.ScaleTo(this.MainMenu.gameObject, Vector3.one, Time.timeScale * 0.5f);
                        iTween.ScaleTo(this.NextLevel.gameObject, Vector3.one, Time.timeScale * 0.5f);
                    }
                }
                this.IsControlsAvailable = false;
                this.Pause.gameObject.SetActive(false);
                this.Replay.gameObject.SetActive(true);
                object[] args = new object[] { "scale", Vector3.zero, "time", 0.5f, "delay", 0.35f, "easetype", iTween.EaseType.easeOutElastic };
                iTween.ScaleFrom(this.Replay.gameObject, iTween.Hash(args));
                base.Invoke("StopReplay", 1f);
            }
            else
            {
                if ((App.GameType == GameType.SingleEndless) && (PlayerSettings.BestSingleEndless.Value < this._enemiesKilled))
                {
                    this.BestScore.text = Sl.GetValue("Best:") + " " + this._enemiesKilled;
                    PlayerSettings.BestSingleEndless.SetAndSave(this._enemiesKilled);
                }
                else if ((App.GameType == GameType.MultiEndless) && (PlayerSettings.BestMultiEndless.Value < this._enemiesKilled))
                {
                    this.BestScore.text = Sl.GetValue("Best:") + " " + this._enemiesKilled;
                    PlayerSettings.BestMultiEndless.SetAndSave(this._enemiesKilled);
                }
                if (flag3)
                {
                    Vector2 vector = new Vector2();
                    for (int k = 0; k < 10; k++)
                    {
                        vector = new Vector3((float) UnityEngine.Random.Range(-40, 40), (float) UnityEngine.Random.Range(3, 40));
                        Vector2 vector5 = vector - this.Player1Stickman.Position;
                        if (vector5.magnitude > 15f)
                        {
                            break;
                        }
                        if (this.Player2Stickman != null)
                        {
                            Vector2 vector6 = vector - this.Player2Stickman.Position;
                            if (vector6.magnitude > 15f)
                            {
                                break;
                            }
                        }
                    }
                    StickmanType simple = StickmanType.Simple;
                    if (this._enemiesKilled >= 20)
                    {
                        simple = StickmanType.Thief;
                    }
                    else if (this._enemiesKilled >= 10)
                    {
                        simple = StickmanType.Hard;
                    }
                    Stickman stickman = App.CreateStickman(simple);
                    stickman.transform.position = (Vector3) vector;
                    stickman.IsPlayer = false;
                    stickman.Health = 1;
                    this.AddStickman(stickman, simple);
                }
                if (flag && ((this.Player2Stickman == null) || flag2))
                {
                    this.IsOver = true;
                    this.Text.text = this._enemiesKilled + " " + Sl.GetValue("KILLS");
                    this.Text.transform.localScale = (Vector3) (Vector3.one * 0.25f);
                    iTween.FadeTo(this.Text.gameObject, 1f, Time.timeScale * 0.25f);
                    iTween.ScaleTo(this.Text.gameObject, (Vector3) (Vector3.one * 2f), Time.timeScale * 0.5f);
                    this.NextLevel.Text = Sl.GetValue("RETRY");
                    iTween.ScaleTo(this.MainMenu.gameObject, Vector3.one, Time.timeScale * 0.5f);
                    iTween.ScaleTo(this.NextLevel.gameObject, Vector3.one, Time.timeScale * 0.5f);
                    this.IsControlsAvailable = false;
                    this.Pause.gameObject.SetActive(false);
                    this.Replay.gameObject.SetActive(true);
                    object[] objArray1 = new object[] { "scale", Vector3.zero, "time", 0.5f, "delay", 0.35f, "easetype", iTween.EaseType.easeOutElastic };
                    iTween.ScaleFrom(this.Replay.gameObject, iTween.Hash(objArray1));
                    base.Invoke("StopReplay", 1f);
                }
            }
        }
    }

    public bool IsControlsAvailable
    {
        get => 
            this.Player1Joystick.gameObject.activeSelf;
        set
        {
            this.Player1Joystick.gameObject.SetActive(value);
            if ((App.GameType == GameType.Versus) || (App.GameType == GameType.MultiEndless))
            {
                this.Player2Joystick.gameObject.SetActive(value);
            }
        }
    }

    public bool IsOver { get; private set; }

    [CompilerGenerated]
    private sealed class <CpuControl>c__AnonStoreyF
    {
        internal Stickman cpuStickman;

        internal void <>m__1F()
        {
            UnityEngine.Object.Destroy(this.cpuStickman.gameObject);
        }

        internal float <>m__20(Stickman s) => 
            Vector2.Distance(this.cpuStickman.Position, s.Position);
    }

    [CompilerGenerated]
    private sealed class <Delayed>c__Iterator5 : IDisposable, IEnumerator, IEnumerator<object>
    {
        internal object $current;
        internal int $PC;
        internal System.Action <$>action;
        internal float <$>delay;
        internal System.Action action;
        internal float delay;

        [DebuggerHidden]
        public void Dispose()
        {
            this.$PC = -1;
        }

        public bool MoveNext()
        {
            uint num = (uint) this.$PC;
            this.$PC = -1;
            switch (num)
            {
                case 0:
                    this.$current = new WaitForSeconds(this.delay);
                    this.$PC = 1;
                    return true;

                case 1:
                    this.action();
                    this.$PC = -1;
                    break;
            }
            return false;
        }

        [DebuggerHidden]
        public void Reset()
        {
            throw new NotSupportedException();
        }

        object IEnumerator<object>.Current =>
            this.$current;

        object IEnumerator.Current =>
            this.$current;
    }

    [CompilerGenerated]
    private sealed class <RecordReplay>c__Iterator6 : IDisposable, IEnumerator, IEnumerator<object>
    {
        internal object $current;
        internal int $PC;
        internal float <$>frequency;
        internal List<Stickman>.Enumerator <$s_31>__0;
        internal List<Stickman>.Enumerator <$s_32>__2;
        internal List<BaseWeapon>.Enumerator <$s_33>__4;
        internal Game <>f__this;
        internal Stickman <stickman>__1;
        internal Stickman <stickman>__3;
        internal BaseWeapon <weapon>__5;
        internal float frequency;

        [DebuggerHidden]
        public void Dispose()
        {
            this.$PC = -1;
        }

        public bool MoveNext()
        {
            uint num = (uint) this.$PC;
            this.$PC = -1;
            switch (num)
            {
                case 0:
                    this.<>f__this._replayStartTime = Time.time;
                    break;

                case 1:
                    break;
                    this.$PC = -1;
                    goto Label_01BB;

                default:
                    goto Label_01BB;
            }
            this.<$s_31>__0 = this.<>f__this._stickmen.GetEnumerator();
            try
            {
                while (this.<$s_31>__0.MoveNext())
                {
                    this.<stickman>__1 = this.<$s_31>__0.Current;
                    this.<>f__this.AddStickmanKey(this.<stickman>__1);
                }
            }
            finally
            {
                this.<$s_31>__0.Dispose();
            }
            this.<$s_32>__2 = this.<>f__this._deadStickmen.GetEnumerator();
            try
            {
                while (this.<$s_32>__2.MoveNext())
                {
                    this.<stickman>__3 = this.<$s_32>__2.Current;
                    if (this.<stickman>__3 != null)
                    {
                        this.<>f__this.AddStickmanKey(this.<stickman>__3);
                    }
                }
            }
            finally
            {
                this.<$s_32>__2.Dispose();
            }
            this.<$s_33>__4 = this.<>f__this._weapons.GetEnumerator();
            try
            {
                while (this.<$s_33>__4.MoveNext())
                {
                    this.<weapon>__5 = this.<$s_33>__4.Current;
                    if (this.<weapon>__5 != null)
                    {
                        this.<>f__this.AddWeaponKey(this.<weapon>__5);
                    }
                }
            }
            finally
            {
                this.<$s_33>__4.Dispose();
            }
            Game._replayLength = Time.time - this.<>f__this._replayStartTime;
            this.$current = new WaitForSeconds(this.frequency);
            this.$PC = 1;
            return true;
        Label_01BB:
            return false;
        }

        [DebuggerHidden]
        public void Reset()
        {
            throw new NotSupportedException();
        }

        object IEnumerator<object>.Current =>
            this.$current;

        object IEnumerator.Current =>
            this.$current;
    }

    [CompilerGenerated]
    private sealed class <Start>c__Iterator4 : IDisposable, IEnumerator, IEnumerator<object>
    {
        internal object $current;
        internal int $PC;
        internal List<StickmanTimeline>.Enumerator <$s_28>__1;
        internal List<WeaponTimeline>.Enumerator <$s_29>__4;
        internal Game <>f__this;
        internal Vector3 <aspectDiff>__0;
        internal Stickman <enemy>__9;
        internal Exception <ex>__12;
        internal Exception <ex>__7;
        internal int <i>__10;
        internal int <i>__8;
        internal Stickman <stickman>__3;
        internal StickmanTimeline <timeline>__2;
        internal WeaponTimeline <timeline>__5;
        internal BaseWeapon <weapon>__11;
        internal BaseWeapon <weapon>__6;

        internal void <>m__21(float f)
        {
            this.<>f__this.transform.localScale = (Vector3) (Vector3.one * f);
        }

        [DebuggerHidden]
        public void Dispose()
        {
            this.$PC = -1;
        }

        public bool MoveNext()
        {
            uint num = (uint) this.$PC;
            this.$PC = -1;
            switch (num)
            {
                case 0:
                {
                    this.<>f__this.MainMenu.transform.localScale = Vector3.zero;
                    this.<>f__this.NextLevel.transform.localScale = Vector3.zero;
                    this.<>f__this.BestScore.gameObject.SetActive(false);
                    this.<>f__this._cameraZ = this.<>f__this.MainCamera.transform.position.z;
                    this.<>f__this._audio = this.<>f__this.audio;
                    iTween.Init(this.<>f__this.gameObject);
                    if (App.Replay == null)
                    {
                        Time.timeScale = 0.01f;
                    }
                    this.<>f__this.gameObject.transform.localScale = (Vector3) (Vector3.one * Time.timeScale);
                    this.<aspectDiff>__0 = new Vector3(this.<>f__this.GUICamera.orthographicSize * (1.777778f - this.<>f__this.GUICamera.aspect), 0f, 0f);
                    Transform transform = this.<>f__this.Pause.transform;
                    transform.position += this.<aspectDiff>__0;
                    this.<>f__this.Pause.gameObject.SetActive(false);
                    Transform transform2 = this.<>f__this.Replay.transform;
                    transform2.position += this.<aspectDiff>__0;
                    this.<>f__this.Replay.gameObject.SetActive(false);
                    Transform transform3 = this.<>f__this.ReplayIcon.transform;
                    transform3.position -= this.<aspectDiff>__0;
                    this.<>f__this.OldTV.transform.localScale = new Vector3(this.<>f__this.OldTV.transform.localScale.x * (this.<>f__this.GUICamera.aspect / 1.777778f), this.<>f__this.OldTV.transform.localScale.y, this.<>f__this.OldTV.transform.localScale.z);
                    this.<>f__this.OldTV.SetActive(false);
                    this.<>f__this.ReplayObjects.SetActive(false);
                    Transform transform4 = this.<>f__this.Player1Joystick.transform;
                    transform4.position += this.<aspectDiff>__0;
                    Transform transform5 = this.<>f__this.Player2Joystick.transform;
                    transform5.position -= this.<aspectDiff>__0;
                    Transform transform6 = this.<>f__this.Money.transform;
                    transform6.position -= this.<aspectDiff>__0;
                    this.<>f__this.Money.gameObject.SetActive(false);
                    if (App.Replay == null)
                    {
                        bool flag;
                        Game._campaignIsCompleted = false;
                        switch (PlayerSettings.JoystickPosition.Value)
                        {
                            case 0:
                                flag = true;
                                this.<>f__this.Player2Joystick.IsStatic = flag;
                                this.<>f__this.Player1Joystick.IsStatic = flag;
                                goto Label_06CC;

                            case 1:
                                flag = true;
                                this.<>f__this.Player2Joystick.IsStatic = flag;
                                this.<>f__this.Player1Joystick.IsStatic = flag;
                                if (((App.GameType == GameType.Single) || (App.GameType == GameType.SingleEndless)) && !App.IsCooperative)
                                {
                                    this.<>f__this.Player1Joystick.transform.position = this.<>f__this.Player2Joystick.transform.position;
                                    this.<>f__this.Player1Joystick.TouchArea = this.<>f__this.Player2Joystick.TouchArea;
                                }
                                goto Label_06CC;

                            case 2:
                                flag = false;
                                this.<>f__this.Player2Joystick.IsStatic = flag;
                                this.<>f__this.Player1Joystick.IsStatic = flag;
                                if (((App.GameType == GameType.Single) || (App.GameType == GameType.SingleEndless)) && !App.IsCooperative)
                                {
                                    this.<>f__this.Player1Joystick.TouchArea = new Rect(this.<>f__this.Player1Joystick.TouchArea.x, this.<>f__this.Player1Joystick.TouchArea.y, this.<>f__this.Player1Joystick.TouchArea.width * 2f, this.<>f__this.Player1Joystick.TouchArea.height);
                                }
                                goto Label_06CC;
                        }
                        break;
                    }
                    this.<>f__this.gameObject.transform.localScale = Vector3.one;
                    this.<>f__this.IsControlsAvailable = false;
                    this.<>f__this.Text.gameObject.SetActive(false);
                    this.<>f__this.OldTV.SetActive(true);
                    this.<>f__this.Replay.gameObject.SetActive(true);
                    this.<>f__this.Replay.Text = Sl.GetValue("EXIT");
                    this.<>f__this.ReplayObjects.SetActive(true);
                    this.<>f__this.ReplaySpeedSlider.onValueChanged.AddListener(new UnityAction<float>(this.<>m__21));
                    this.<>f__this.ReplaySpeedSlider.value = Time.timeScale;
                    this.<$s_28>__1 = App.Replay.StickmanTimelines.GetEnumerator();
                    try
                    {
                        while (this.<$s_28>__1.MoveNext())
                        {
                            this.<timeline>__2 = this.<$s_28>__1.Current;
                            this.<stickman>__3 = App.CreateStickman(this.<timeline>__2.StickmanType);
                            this.<stickman>__3.PrepareToReplay();
                            this.<stickman>__3.gameObject.SetActive(false);
                            this.<>f__this._stickmen.Add(this.<stickman>__3);
                        }
                    }
                    finally
                    {
                        this.<$s_28>__1.Dispose();
                    }
                    this.<$s_29>__4 = App.Replay.WeaponTimelines.GetEnumerator();
                    try
                    {
                        while (this.<$s_29>__4.MoveNext())
                        {
                            this.<timeline>__5 = this.<$s_29>__4.Current;
                            this.<weapon>__6 = App.CreateWeapon(this.<timeline>__5.WeaponType);
                            this.<weapon>__6.PrepareToReplay();
                            this.<weapon>__6.gameObject.SetActive(false);
                            this.<>f__this._weapons.Add(this.<weapon>__6);
                        }
                    }
                    finally
                    {
                        this.<$s_29>__4.Dispose();
                    }
                    goto Label_1607;
                }
                case 1:
                    goto Label_0F9C;

                case 2:
                    if (PlayerSettings.Music.Value > 0)
                    {
                        this.<>f__this.MusicBox.gameObject.SetActive(true);
                    }
                    if (App.GameType == GameType.Single)
                    {
                        goto Label_1334;
                    }
                    if (App.GameType == GameType.Versus)
                    {
                        this.<>f__this.Text.text = Sl.GetValue("GameVS");
                    }
                    else if ((App.GameType == GameType.SingleEndless) || (App.GameType == GameType.MultiEndless))
                    {
                        this.<>f__this.Text.text = Sl.GetValue("GameEndless");
                    }
                    this.<>f__this.Text.transform.localScale = (Vector3) (Vector3.one * 0.25f);
                    iTween.ScaleTo(this.<>f__this.Text.gameObject, (Vector3) (Vector3.one * 1.5f), Time.timeScale * 0.5f);
                    this.$current = new WaitForSeconds(Time.timeScale);
                    this.$PC = 3;
                    goto Label_1609;

                case 3:
                    goto Label_1334;

                case 4:
                    this.<>f__this.Text.text = "2";
                    this.<>f__this.Text.transform.localScale = (Vector3) (Vector3.one * 0.25f);
                    iTween.ScaleTo(this.<>f__this.Text.gameObject, (Vector3) (Vector3.one * 2f), Time.timeScale * 0.5f);
                    this.$current = new WaitForSeconds(Time.timeScale);
                    this.$PC = 5;
                    goto Label_1609;

                case 5:
                    this.<>f__this.Text.text = "1";
                    this.<>f__this.Text.transform.localScale = (Vector3) (Vector3.one * 0.25f);
                    iTween.ScaleTo(this.<>f__this.Text.gameObject, (Vector3) (Vector3.one * 2f), Time.timeScale * 0.5f);
                    this.$current = new WaitForSeconds(Time.timeScale);
                    this.$PC = 6;
                    goto Label_1609;

                case 6:
                {
                    this.<>f__this.Text.text = Sl.GetValue("GameGO!");
                    this.<>f__this.Text.transform.localScale = (Vector3) (Vector3.one * 0.25f);
                    iTween.ScaleTo(this.<>f__this.Text.gameObject, (Vector3) (Vector3.one * 5f), 0.25f);
                    iTween.FadeTo(this.<>f__this.Text.gameObject, 0f, 0.25f);
                    iTween.ScaleTo(this.<>f__this.gameObject, Vector3.one, 0.1f);
                    this.<>f__this._isReady = true;
                    this.<>f__this.Pause.gameObject.SetActive(true);
                    object[] args = new object[] { "scale", Vector3.zero, "time", Time.timeScale * 5f, "easetype", iTween.EaseType.easeOutElastic };
                    iTween.ScaleFrom(this.<>f__this.Pause.gameObject, iTween.Hash(args));
                    this.<>f__this.StartCoroutine(this.<>f__this.RecordReplay(0.03333334f));
                    this.$PC = -1;
                    goto Label_1607;
                }
                default:
                    goto Label_1607;
            }
        Label_06CC:
            try
            {
                GoogleAnalytics.SendEvent("GameType", !App.IsCooperative ? App.GameType.ToString() : "Cooperative", null, 0x7fffffff);
            }
            catch (Exception exception)
            {
                this.<ex>__7 = exception;
                UnityEngine.Debug.LogException(this.<ex>__7);
            }
            if (App.GameType == GameType.Versus)
            {
                App.CreateArena(App.VersusSettings.Arena);
                this.<>f__this.Player2Joystick.gameObject.SetActive(true);
                this.<>f__this.Player1Stickman = App.CreateStickman(App.VersusSettings.P1Stickman);
                this.<>f__this.Player1Stickman.transform.position = new Vector3(-20f, 3f);
                this.<>f__this.Player1Stickman.IsPlayer = true;
                this.<>f__this.GiveWeaponL(this.<>f__this.Player1Stickman, App.VersusSettings.P1LeftWeapon);
                this.<>f__this.GiveWeaponR(this.<>f__this.Player1Stickman, App.VersusSettings.P1RigthtWeapon);
                this.<>f__this.AddStickman(this.<>f__this.Player1Stickman, App.VersusSettings.P1Stickman);
                this.<>f__this.Player2Stickman = App.CreateStickman(App.VersusSettings.P2Stickman);
                this.<>f__this.Player2Stickman.transform.position = new Vector3(20f, 3f);
                this.<>f__this.Player2Stickman.IsPlayer = false;
                this.<>f__this.GiveWeaponL(this.<>f__this.Player2Stickman, App.VersusSettings.P2LeftWeapon);
                this.<>f__this.GiveWeaponR(this.<>f__this.Player2Stickman, App.VersusSettings.P2RigthtWeapon);
                this.<>f__this.AddStickman(this.<>f__this.Player2Stickman, App.VersusSettings.P2Stickman);
                goto Label_1204;
            }
            if (App.GameType != GameType.Single)
            {
                if ((App.GameType == GameType.SingleEndless) || (App.GameType == GameType.MultiEndless))
                {
                    App.CreateArena(ArenaType.Origin);
                    this.<>f__this.BestScore.gameObject.SetActive(true);
                    this.<>f__this.Player1Stickman = App.CreateStickman(StickmanType.Player1);
                    this.<>f__this.Player1Stickman.transform.position = new Vector3(-20f, 3f);
                    this.<>f__this.Player1Stickman.IsPlayer = true;
                    this.<>f__this.AddStickman(this.<>f__this.Player1Stickman, StickmanType.Player1);
                    this.<>f__this.BestScore.text = Sl.GetValue("Best:") + " " + PlayerSettings.BestSingleEndless.Value;
                    if (App.GameType == GameType.MultiEndless)
                    {
                        this.<>f__this.Player2Joystick.gameObject.SetActive(true);
                        this.<>f__this.Player2Stickman = App.CreateStickman(StickmanType.Player2);
                        this.<>f__this.Player2Stickman.transform.position = new Vector3(20f, 3f);
                        this.<>f__this.Player2Stickman.IsPlayer = true;
                        this.<>f__this.AddStickman(this.<>f__this.Player2Stickman, StickmanType.Player2);
                        this.<>f__this.BestScore.text = Sl.GetValue("Best:") + " " + PlayerSettings.BestMultiEndless.Value;
                    }
                }
                goto Label_1204;
            }
            if (this.<>f__this.CampaignIndex > -1)
            {
                App.CurrentCampaign = this.<>f__this.CampaignIndex;
            }
            if (this.<>f__this.LevelIndex > -1)
            {
                App.CurrentLevel = this.<>f__this.LevelIndex;
            }
            this.<>f__this._campaign = !App.IsCooperative ? App.Campaigns[App.CurrentCampaign] : App.CoopCampaigns[App.CurrentCampaign];
            this.<>f__this._level = this.<>f__this._campaign.Levels[App.CurrentLevel];
            App.CreateArena(this.<>f__this._level.ArenaType);
            this.<>f__this.Player2Joystick.gameObject.SetActive(App.IsCooperative);
            this.<>f__this.Player1Stickman = App.CreateStickman(this.<>f__this._level.Player1.Type);
            this.<>f__this.Player1Stickman.transform.position = (Vector3) this.<>f__this._level.Player1.Position;
            this.<>f__this.Player1Stickman.IsPlayer = true;
            this.<>f__this.Player1Stickman.Health = Mathf.RoundToInt((this.<>f__this.Player1Stickman.Health * this.<>f__this._campaign.PlayerHealthModifier) * this.<>f__this._level.Player1.HealthModifier);
            this.<>f__this.GiveWeaponL(this.<>f__this.Player1Stickman, this.<>f__this._level.Player1.LeftWeapon);
            this.<>f__this.GiveWeaponR(this.<>f__this.Player1Stickman, this.<>f__this._level.Player1.RightWeapon);
            this.<>f__this.AddStickman(this.<>f__this.Player1Stickman, this.<>f__this._level.Player1.Type);
            if (App.IsCooperative)
            {
                this.<>f__this.Player2Stickman = App.CreateStickman(this.<>f__this._level.Player2.Type);
                this.<>f__this.Player2Stickman.transform.position = (Vector3) this.<>f__this._level.Player2.Position;
                this.<>f__this.Player2Stickman.IsPlayer = true;
                this.<>f__this.Player2Stickman.Health = Mathf.RoundToInt((this.<>f__this.Player2Stickman.Health * this.<>f__this._campaign.PlayerHealthModifier) * this.<>f__this._level.Player2.HealthModifier);
                this.<>f__this.GiveWeaponL(this.<>f__this.Player2Stickman, this.<>f__this._level.Player2.LeftWeapon);
                this.<>f__this.GiveWeaponR(this.<>f__this.Player2Stickman, this.<>f__this._level.Player2.RightWeapon);
                this.<>f__this.AddStickman(this.<>f__this.Player2Stickman, this.<>f__this._level.Player2.Type);
            }
            this.<i>__8 = 0;
            while (this.<i>__8 < this.<>f__this._level.Enemies.Count)
            {
                this.<enemy>__9 = App.CreateStickman(this.<>f__this._level.Enemies[this.<i>__8].Type);
                this.<enemy>__9.transform.position = (Vector3) this.<>f__this._level.Enemies[this.<i>__8].Position;
                this.<enemy>__9.IsPlayer = false;
                this.<enemy>__9.Health = Mathf.RoundToInt((this.<enemy>__9.Health * this.<>f__this._campaign.EnemyHealthModifier) * this.<>f__this._level.Enemies[this.<i>__8].HealthModifier);
                this.<>f__this.GiveWeaponL(this.<enemy>__9, this.<>f__this._level.Enemies[this.<i>__8].LeftWeapon);
                this.<>f__this.GiveWeaponR(this.<enemy>__9, this.<>f__this._level.Enemies[this.<i>__8].RightWeapon);
                this.<>f__this.AddStickman(this.<enemy>__9, this.<>f__this._level.Enemies[this.<i>__8].Type);
                this.<i>__8++;
            }
            this.<i>__10 = 0;
            while (this.<i>__10 < this.<>f__this._level.Weapons.Count)
            {
                this.<weapon>__11 = App.CreateWeapon(this.<>f__this._level.Weapons[this.<i>__10].Type);
                this.<weapon>__11.transform.position = (Vector3) this.<>f__this._level.Weapons[this.<i>__10].Position;
                this.<>f__this.AddWeapon(this.<weapon>__11, this.<>f__this._level.Weapons[this.<i>__10].Type);
                this.<i>__10++;
            }
            this.<>f__this.Money.gameObject.SetActive(true);
            this.<>f__this.Money.text = PlayerSettings.Money.Value.ToString();
            this.<>f__this.BestScore.gameObject.SetActive(true);
            this.<>f__this.BestScore.text = Sl.GetValue("Time:") + " 0";
            if (this.<>f__this._level.Type == LevelType.KillCountOfEnemies)
            {
                this.<>f__this.Kills.gameObject.SetActive(true);
                this.<>f__this.Kills.text = Sl.GetValue("Kills:") + " 0";
            }
            this.<>f__this.LevelWindow.Init(App.CurrentCampaign, App.CurrentLevel);
            if (App.WasRestart)
            {
                goto Label_0FEB;
            }
            this.<>f__this.LevelWindow.Show(false, false);
            Time.timeScale = 0f;
            this.<>f__this.gameObject.transform.localScale = (Vector3) (Vector3.one * Time.timeScale);
            this.<>f__this.IsControlsAvailable = false;
        Label_0F9C:
            while (this.<>f__this.LevelWindow.IsShown)
            {
                this.$current = new WaitForEndOfFrame();
                this.$PC = 1;
                goto Label_1609;
            }
            Time.timeScale = 0.01f;
            this.<>f__this.gameObject.transform.localScale = (Vector3) (Vector3.one * Time.timeScale);
            this.<>f__this.IsControlsAvailable = true;
        Label_0FEB:
            App.WasRestart = false;
            try
            {
                if (App.IsCooperative)
                {
                    GoogleAnalytics.SendEvent("Coop Level Start", this.<>f__this._campaign.Name + " Level " + (App.CurrentLevel + 1), null, 0x7fffffff);
                }
                else
                {
                    GoogleAnalytics.SendEvent("Level Start", this.<>f__this._campaign.Name + " Level " + (App.CurrentLevel + 1), null, 0x7fffffff);
                }
            }
            catch (Exception exception2)
            {
                this.<ex>__12 = exception2;
                UnityEngine.Debug.LogException(this.<ex>__12);
            }
        Label_1204:
            this.<>f__this.Text.text = string.Empty;
            this.$current = new WaitForSeconds(Time.timeScale * 0.5f);
            this.$PC = 2;
            goto Label_1609;
        Label_1334:
            this.<>f__this.Text.text = "3";
            this.<>f__this.Text.transform.localScale = (Vector3) (Vector3.one * 0.25f);
            iTween.ScaleTo(this.<>f__this.Text.gameObject, (Vector3) (Vector3.one * 2f), Time.timeScale * 0.5f);
            this.$current = new WaitForSeconds(Time.timeScale);
            this.$PC = 4;
            goto Label_1609;
        Label_1607:
            return false;
        Label_1609:
            return true;
        }

        [DebuggerHidden]
        public void Reset()
        {
            throw new NotSupportedException();
        }

        object IEnumerator<object>.Current =>
            this.$current;

        object IEnumerator.Current =>
            this.$current;
    }

    [CompilerGenerated]
    private sealed class <Update>c__AnonStoreyE
    {
        internal Game <>f__this;
        internal bool completed;

        internal void <>m__14()
        {
            this.<>f__this.LevelWindow.Show(true, this.completed);
        }
    }
}

