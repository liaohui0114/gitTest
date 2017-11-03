using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;

[ExecuteInEditMode, AddComponentMenu("")]
public class StickmanPart : MonoBehaviour
{
    private UnityEngine.Color _baseColor;
    private Mesh _bodyMesh;
    [SerializeField]
    private bool _canGetHit;
    [SerializeField]
    private bool _canHit;
    private GameObject _circleEnd;
    private GameObject _circleStart;
    [SerializeField]
    private UnityEngine.Color _color = UnityEngine.Color.white;
    private Mesh _endMesh;
    private Game _game;
    [SerializeField]
    private float _length = 1f;
    private GameObject _quad;
    private Rigidbody _rigidbody;
    private Mesh _startMesh;
    [SerializeField]
    private float _thickness = 1f;
    private float _time;
    [SerializeField]
    private bool _twoSides = true;

    [DebuggerHidden]
    private IEnumerator AddForce(Vector2 force, Vector2 position, float time) => 
        new <AddForce>c__Iterator9 { 
            time = time,
            force = force,
            position = position,
            <$>time = time,
            <$>force = force,
            <$>position = position,
            <>f__this = this
        };

    private void Awake()
    {
        this._game = UnityEngine.Object.FindObjectOfType<Game>();
        this._quad = base.transform.FindChild("body").gameObject;
        this._circleStart = base.transform.FindChild("start").gameObject;
        this._circleEnd = base.transform.FindChild("end").gameObject;
        if (Application.isPlaying)
        {
            this._startMesh = this._circleStart.GetComponent<MeshFilter>().mesh;
            this._bodyMesh = this._quad.GetComponent<MeshFilter>().mesh;
            this._endMesh = this._circleEnd.GetComponent<MeshFilter>().mesh;
            this.Color = this._baseColor = this._color;
        }
        if (base.transform.parent != null)
        {
            this.Stickman = base.transform.parent.GetComponent<Stickman>();
        }
        this._rigidbody = base.rigidbody;
        this.Transform = base.transform;
    }

    public void Blink()
    {
        this._time = Time.time;
        base.transform.localPosition = new Vector3(base.transform.localPosition.x, base.transform.localPosition.y, -0.1f);
    }

    public static StickmanPart Create(string name = "Part", float length = 1, float thickness = 1, float mass = 1, bool twoSides = false)
    {
        StickmanPart part = new GameObject(name).AddComponent<StickmanPart>();
        part._thickness = thickness;
        part._length = length;
        part._twoSides = twoSides;
        part._rigidbody = part.rigidbody;
        part._rigidbody.mass = mass;
        part._rigidbody.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezePositionZ;
        part._rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
        part._quad = (GameObject) UnityEngine.Object.Instantiate(part._game.QuadPrefab);
        part._quad.name = "body";
        part._quad.transform.parent = part.transform;
        part._circleStart = (GameObject) UnityEngine.Object.Instantiate(part._game.CirclePrefab);
        part._circleStart.name = "start";
        part._circleStart.transform.parent = part.transform;
        part._circleEnd = (GameObject) UnityEngine.Object.Instantiate(part._game.CirclePrefab);
        part._circleEnd.name = "end";
        part._circleEnd.transform.parent = part.transform;
        part.UpdatePart();
        return part;
    }

    private void OnCollisionEnter(Collision collision)
    {
        StickmanPart component = collision.gameObject.GetComponent<StickmanPart>();
        if (((((component != null) && (component.Stickman != this.Stickman)) && ((this.Stickman.CurrentHealth > 0) && (component.Stickman.CurrentHealth > 0))) && (this.Stickman.IsPlayer != component.Stickman.IsPlayer)) && (this._rigidbody.velocity.sqrMagnitude >= component._rigidbody.velocity.sqrMagnitude))
        {
            Vector3 point = collision.contacts[0].point;
            if (this.CanHit || component.CanHit)
            {
                Vector3 vector4 = this._rigidbody.position - point;
                this.StartForce((Vector2) (vector4.normalized * this._game.BlowForce), point, this._game.BlowTime);
                Vector3 vector5 = component._rigidbody.position - point;
                component.StartForce((Vector2) (vector5.normalized * this._game.BlowForce), point, this._game.BlowTime);
            }
            bool twice = false;
            if (this.CanHit && component.CanGetHit)
            {
                this._game.CreateHitParticles(point, true);
                component.Stickman.Hit(this.Stickman.Damage);
                component.Blink();
                if (component.Stickman.CurrentHealth <= 0)
                {
                    this._game.PlayDeathSound();
                    twice = true;
                }
                else
                {
                    this._game.PlayHitSound(false);
                }
                string str = string.Empty;
                if (base.name.Contains("Head"))
                {
                    str = "Butt";
                }
                else if (base.name.Contains("Arm"))
                {
                    str = "Punch";
                }
                else if (base.name.Contains("Leg"))
                {
                    str = "Kick";
                }
                string str2 = string.Empty;
                if (component.name.Contains("Head"))
                {
                    str2 = "Head";
                }
                else if (component.name.Contains("Arm"))
                {
                    str2 = "Arm";
                }
                else if (component.name.Contains("Leg"))
                {
                    str2 = "Leg";
                }
                else if (component.name.Contains("Spine"))
                {
                    str2 = "Body";
                }
                if (base.name.Contains("Head") && component.name.Contains("Head"))
                {
                    str2 = "Head";
                    str = "Bounce";
                }
                this._game.CreateText(str2 + " " + str, UnityEngine.Color.red, point, 0, 0f, -1f, 0f, 0f);
                if (str2 == "Head")
                {
                    component.Stickman.ReleaseWeapons();
                }
            }
            if (this.CanGetHit && component.CanHit)
            {
                this._game.CreateHitParticles(point, true);
                this.Stickman.Hit(component.Stickman.Damage);
                this.Blink();
                if (this.Stickman.CurrentHealth <= 0)
                {
                    this._game.PlayDeathSound();
                    twice = true;
                }
                else
                {
                    this._game.PlayHitSound(false);
                }
                string str3 = string.Empty;
                if (component.name.Contains("Head"))
                {
                    str3 = "Butt";
                }
                else if (component.name.Contains("Arm"))
                {
                    str3 = "Punch";
                }
                else if (component.name.Contains("Leg"))
                {
                    str3 = "Kick";
                }
                string str4 = string.Empty;
                if (base.name.Contains("Head"))
                {
                    str4 = "Head";
                }
                else if (base.name.Contains("Arm"))
                {
                    str4 = "Arm";
                }
                else if (base.name.Contains("Leg"))
                {
                    str4 = "Leg";
                }
                else if (base.name.Contains("Spine"))
                {
                    str4 = "Body";
                }
                if (base.name.Contains("Head") && component.name.Contains("Head"))
                {
                    str4 = "Head";
                    str3 = "Bounce";
                }
                this._game.CreateText(str4 + " " + str3, UnityEngine.Color.red, point, 0, 0f, -1f, 0f, 0f);
                if (str4 == "Head")
                {
                    this.Stickman.ReleaseWeapons();
                }
            }
            if ((this.CanHit && !this.CanGetHit) && (component.CanHit && !component.CanGetHit))
            {
                this._game.CreateBlockParticles(point);
                this._game.PlayBlockSound();
            }
            if (this.CanHit || component.CanHit)
            {
                this._game.SlowMo(twice);
                this._game.AddStickmanKey(this.Stickman);
                this._game.AddStickmanKey(component.Stickman);
            }
        }
    }

    internal void StartForce(Vector2 force, Vector2 position, float time)
    {
        base.StopAllCoroutines();
        base.StartCoroutine(this.AddForce(force, position, time));
    }

    private void Update()
    {
        if (this._time != 0f)
        {
            float t = (Time.time - this._time) / 0.5f;
            if (t > 1f)
            {
                this._time = 0f;
                base.transform.localPosition = new Vector3(base.transform.localPosition.x, base.transform.localPosition.y, 0f);
            }
            else
            {
                this.Color = UnityEngine.Color.Lerp(UnityEngine.Color.red, this._baseColor, t);
            }
        }
    }

    private void UpdatePart()
    {
        this._quad.transform.parent = base.transform;
        this._quad.transform.localScale = new Vector3(this._thickness, this._length, 0f);
        this._quad.transform.localPosition = new Vector3(0f, -this._length * 0.5f);
        this._circleStart.transform.parent = base.transform;
        this._circleStart.transform.localScale = new Vector3(this._thickness, this._thickness, 0f);
        this._circleStart.transform.localPosition = new Vector3(0f, 0f);
        this._circleEnd.SetActive(this._twoSides);
        this._circleEnd.transform.parent = base.transform;
        this._circleEnd.transform.localScale = new Vector3(this._thickness, this._thickness, 0f);
        this._circleEnd.transform.localPosition = new Vector3(0f, -this._length);
        CapsuleCollider component = base.GetComponent<CapsuleCollider>();
        component.height = this._thickness + this._length;
        component.radius = this._thickness * 0.5f;
        component.center = new Vector3(0f, component.radius - (component.height * 0.5f));
    }

    public bool CanGetHit
    {
        get => 
            this._canGetHit;
        set
        {
            this._canGetHit = value;
        }
    }

    public bool CanHit
    {
        get => 
            this._canHit;
        set
        {
            this._canHit = value;
        }
    }

    public Material CircleMaterial =>
        this._circleStart?.renderer.sharedMaterial;

    public UnityEngine.Color Color
    {
        get => 
            this._color;
        set
        {
            this._color = value;
            if (Application.isPlaying)
            {
                this._startMesh.colors = new UnityEngine.Color[] { this._color, this._color, this._color, this._color };
                this._bodyMesh.colors = new UnityEngine.Color[] { this._color, this._color, this._color, this._color };
                this._endMesh.colors = new UnityEngine.Color[] { this._color, this._color, this._color, this._color };
            }
        }
    }

    public float Length
    {
        get => 
            this._length;
        set
        {
            this._length = Mathf.Clamp(value, 0f, float.MaxValue);
            this.UpdatePart();
        }
    }

    public Material QuadMaterial =>
        this._quad?.renderer.sharedMaterial;

    public Stickman Stickman { get; private set; }

    public float Thickness
    {
        get => 
            this._thickness;
        set
        {
            this._thickness = Mathf.Clamp(value, 0f, float.MaxValue);
            this.UpdatePart();
        }
    }

    public UnityEngine.Transform Transform { get; private set; }

    public bool TwoSides
    {
        get => 
            this._twoSides;
        set
        {
            this._twoSides = value;
            this.UpdatePart();
        }
    }

    [CompilerGenerated]
    private sealed class <AddForce>c__Iterator9 : IDisposable, IEnumerator, IEnumerator<object>
    {
        internal object $current;
        internal int $PC;
        internal Vector2 <$>force;
        internal Vector2 <$>position;
        internal float <$>time;
        internal StickmanPart <>f__this;
        internal float <startTme>__0;
        internal Vector2 force;
        internal Vector2 position;
        internal float time;

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
                    this.<startTme>__0 = Time.time;
                    break;

                case 1:
                    break;

                default:
                    goto Label_008C;
            }
            if ((Time.time - this.<startTme>__0) < this.time)
            {
                this.<>f__this._rigidbody.AddForceAtPosition((Vector3) this.force, (Vector3) this.position);
                this.$current = new WaitForFixedUpdate();
                this.$PC = 1;
                return true;
            }
            this.$PC = -1;
        Label_008C:
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
}

