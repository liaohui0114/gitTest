using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

[AddComponentMenu("")]
public abstract class BaseWeapon : MonoBehaviour
{
    private HingeJoint _joint;
    protected readonly List<Transform> _parts = new List<Transform>();
    private float _startTime;
    public float BlowMultiplier = 1f;
    public bool CanBeReleased = true;
    public bool CanHitAnyPart;
    public float DamageMultiplier = 1f;
    public float EnemyBlowMultiplier = 1f;
    public string[] ExceptedParts;
    public bool IsDeadly;
    public bool IsMetallic;
    public string WeaponName;

    protected BaseWeapon()
    {
    }

    protected void AddForce(Rigidbody body, Vector2 force, Vector2 position, float time)
    {
        base.StopCoroutine("StartForce");
        base.StartCoroutine(this.AddForceCoroutine(body, force, position, time));
    }

    [DebuggerHidden]
    private IEnumerator AddForceCoroutine(Rigidbody body, Vector2 force, Vector2 position, float time) => 
        new <AddForceCoroutine>c__Iterator1 { 
            time = time,
            body = body,
            force = force,
            position = position,
            <$>time = time,
            <$>body = body,
            <$>force = force,
            <$>position = position
        };

    protected virtual void Awake()
    {
        this.Game = UnityEngine.Object.FindObjectOfType<Game>();
        this._startTime = Time.time;
        this._parts.Add(base.transform);
        this.Id = base.GetInstanceID();
    }

    protected void EnableCollision(Collider col1, Collider col2, float delay)
    {
        base.StopCoroutine("StartForce");
        base.StartCoroutine(this.EnableCollisionCoroutine(col1, col2, delay));
    }

    [DebuggerHidden]
    private IEnumerator EnableCollisionCoroutine(Collider col1, Collider col2, float delay) => 
        new <EnableCollisionCoroutine>c__Iterator2 { 
            delay = delay,
            col2 = col2,
            col1 = col1,
            <$>delay = delay,
            <$>col2 = col2,
            <$>col1 = col1
        };

    public virtual object GetAdditionalPose() => 
        null;

    public virtual IEnumerable<WeaponPose> GetPoses()
    {
        WeaponPose[] poseArray = new WeaponPose[this._parts.Count];
        for (int i = 0; i < poseArray.Length; i++)
        {
            poseArray[i] = new WeaponPose { 
                Position = this._parts[i].position,
                Rotation = this._parts[i].rotation
            };
        }
        return poseArray;
    }

    public virtual void PrepareToReplay()
    {
        this._startTime = float.MaxValue;
        UnityEngine.Object.Destroy(this._joint);
        UnityEngine.Object.Destroy(base.rigidbody);
        Collider[] components = base.GetComponents<Collider>();
        if (components != null)
        {
            for (int j = 0; j < components.Length; j++)
            {
                UnityEngine.Object.Destroy(components[j]);
            }
        }
        for (int i = 0; i < base.transform.childCount; i++)
        {
            StickmanPart component = base.transform.GetChild(i).GetComponent<StickmanPart>();
            if (component != null)
            {
                UnityEngine.Object.Destroy(component.hingeJoint);
                UnityEngine.Object.Destroy(component.rigidbody);
                UnityEngine.Object.Destroy(component.collider);
            }
        }
    }

    protected void Process(Collision collision, Rigidbody weaponBody)
    {
        StickmanPart component = collision.gameObject.GetComponent<StickmanPart>();
        if (component != null)
        {
            if (this.Stickman == null)
            {
                this.SetToStickman(component.Stickman, component);
            }
            else if ((((this.Stickman.CurrentHealth > 0) && (component.Stickman != this.Stickman)) && (component.Stickman.CurrentHealth > 0)) && (this.Stickman.IsPlayer != component.Stickman.IsPlayer))
            {
                Vector3 point = collision.contacts[0].point;
                if (component.CanHit || component.CanGetHit)
                {
                    if (this.BlowMultiplier > 0f)
                    {
                        Vector3 vector2 = weaponBody.position - point;
                        this.AddForce(weaponBody, (Vector2) ((vector2.normalized * this.Game.BlowForce) * this.BlowMultiplier), point, this.Game.BlowTime);
                    }
                    Vector3 vector3 = component.rigidbody.position - point;
                    component.StartForce((Vector2) ((vector3.normalized * this.Game.BlowForce) * this.EnemyBlowMultiplier), point, this.Game.BlowTime);
                }
                bool twice = false;
                bool flag2 = false;
                if (this.ExceptedParts != null)
                {
                    flag2 = Array.IndexOf<string>(this.ExceptedParts, component.name) >= 0;
                }
                if (component.CanGetHit || (this.CanHitAnyPart && !flag2))
                {
                    this.Game.CreateHitParticles(collision.contacts[0].point, !this.IsMetallic);
                    component.Stickman.Hit(!this.IsDeadly ? Mathf.RoundToInt(this.Stickman.Damage * this.DamageMultiplier) : component.Stickman.Health);
                    component.Blink();
                    if (component.Stickman.CurrentHealth <= 0)
                    {
                        this.Game.PlayDeathSound();
                        twice = true;
                    }
                    else if (this.IsMetallic)
                    {
                        this.Game.PlaySwordSound();
                    }
                    else
                    {
                        this.Game.PlayHitSound(false);
                    }
                    string str = string.Empty;
                    if (component.name.Contains("Head"))
                    {
                        str = "Head";
                    }
                    else if (component.name.Contains("Arm"))
                    {
                        str = "Arm";
                    }
                    else if (component.name.Contains("Leg"))
                    {
                        str = "Leg";
                    }
                    else if (component.name.Contains("Spine"))
                    {
                        str = "Body";
                    }
                    this.Game.CreateText(str + " " + this.WeaponName, Color.red, point, 0, 0f, -1f, 0f, 0f);
                    if (str == "Head")
                    {
                        component.Stickman.ReleaseWeapons();
                    }
                }
                else if (component.CanHit)
                {
                    this.Game.CreateBlockParticles(collision.contacts[0].point);
                    this.Game.PlayBlockSound();
                }
                if (component.CanHit || component.CanGetHit)
                {
                    this.Game.SlowMo(twice);
                }
            }
        }
    }

    public virtual void Release()
    {
        this.Stickman = null;
        UnityEngine.Object.Destroy(this._joint);
        this._startTime = Time.time;
    }

    public virtual void SetAdditionalPose(object startPose, object endPose, float time)
    {
    }

    public virtual void SetPoses(List<WeaponPose> startPoses, List<WeaponPose> endPoses, float time)
    {
        for (int i = 0; i < this._parts.Count; i++)
        {
            this._parts[i].rotation = Quaternion.Lerp(startPoses[i].Rotation, endPoses[i].Rotation, time);
            this._parts[i].position = Vector3.Lerp(startPoses[i].Position, endPoses[i].Position, time);
        }
    }

    public virtual void SetToStickman(Stickman stickman, StickmanPart part)
    {
        if (((stickman != null) && (stickman.CurrentHealth > 0)) && stickman.CanHoldWeapon)
        {
            Transform weaponLTransform = null;
            if ((stickman.WeaponLTransform.parent == part.transform) && (stickman.WeaponL == null))
            {
                weaponLTransform = stickman.WeaponLTransform;
                stickman.WeaponL = this;
            }
            else if ((stickman.WeaponRTransform.parent == part.transform) && (stickman.WeaponR == null))
            {
                weaponLTransform = stickman.WeaponRTransform;
                stickman.WeaponR = this;
            }
            if (weaponLTransform != null)
            {
                this.Stickman = stickman;
                this.WeaponBodyToSet.transform.position = weaponLTransform.position;
                this.WeaponBodyToSet.transform.rotation = weaponLTransform.rotation;
                this._joint = this.WeaponBodyToSet.gameObject.AddComponent<HingeJoint>();
                this._joint.connectedBody = part.rigidbody;
                this._joint.anchor = Vector3.zero;
                this._joint.axis = Vector3.forward;
                this._joint.useLimits = true;
                JointLimits limits = new JointLimits {
                    min = -5f,
                    max = 5f,
                    minBounce = 0.5f,
                    maxBounce = 0.5f
                };
                this._joint.limits = limits;
                this._joint.useSpring = true;
                JointSpring spring = new JointSpring {
                    damper = part.hingeJoint.spring.damper,
                    spring = part.hingeJoint.spring.spring
                };
                this._joint.spring = spring;
            }
        }
    }

    protected virtual void Update()
    {
        if ((this.Stickman == null) && ((Time.time - this._startTime) >= 15f))
        {
            UnityEngine.Object.Destroy(base.gameObject);
        }
    }

    public Game Game { get; protected set; }

    public int Id { get; private set; }

    public Stickman Stickman { get; protected set; }

    public Rigidbody WeaponBodyToSet { get; protected set; }

    [CompilerGenerated]
    private sealed class <AddForceCoroutine>c__Iterator1 : IDisposable, IEnumerator, IEnumerator<object>
    {
        internal object $current;
        internal int $PC;
        internal Rigidbody <$>body;
        internal Vector2 <$>force;
        internal Vector2 <$>position;
        internal float <$>time;
        internal float <startTme>__0;
        internal Rigidbody body;
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
                    goto Label_0087;
            }
            if ((Time.time - this.<startTme>__0) < this.time)
            {
                this.body.AddForceAtPosition((Vector3) this.force, (Vector3) this.position);
                this.$current = new WaitForFixedUpdate();
                this.$PC = 1;
                return true;
            }
            this.$PC = -1;
        Label_0087:
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
    private sealed class <EnableCollisionCoroutine>c__Iterator2 : IDisposable, IEnumerator, IEnumerator<object>
    {
        internal object $current;
        internal int $PC;
        internal Collider <$>col1;
        internal Collider <$>col2;
        internal float <$>delay;
        internal Collider col1;
        internal Collider col2;
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
                {
                    bool flag = true;
                    this.col2.enabled = flag;
                    this.col1.enabled = flag;
                    Physics.IgnoreCollision(this.col1, this.col2, false);
                    this.$PC = -1;
                    break;
                }
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
}

