using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GunWeapon : Weapon
{
    [SerializeField]
    private Bullet _bullet;
    [SerializeField]
    private Transform _bulletPosition;
    private bool _isShoot;
    [SerializeField]
    private SpriteRenderer _muzzleFlashSprite;
    [SerializeField]
    private Transform _muzzleFlashTransform;
    [SerializeField]
    private float _rechargeTime = 1f;
    private float _time;

    protected override void Awake()
    {
        base.Awake();
        Physics.IgnoreCollision(this._bullet.collider, base.collider);
        this._bullet.rigidbody.isKinematic = true;
        this._bullet.Parent = this;
        this._bullet.transform.parent = base.transform;
        this._bullet.gameObject.SetActive(false);
        this._muzzleFlashTransform.localScale = Vector3.zero;
        object[] args = new object[] { "a", 0, "time", 0 };
        iTween.ColorTo(this._muzzleFlashSprite.gameObject, iTween.Hash(args));
    }

    public override object GetAdditionalPose() => 
        new AdditionalPose { 
            BulletPosition = this._bullet.transform.position,
            BulletRotation = this._bullet.transform.rotation,
            IsBulletVisible = this._bullet.gameObject.activeSelf,
            MuzzleScale = this._muzzleFlashTransform.localScale,
            MuzzleColor = this._muzzleFlashSprite.material.color,
            IsShoot = this._isShoot
        };

    public override void PrepareToReplay()
    {
        base.PrepareToReplay();
        this._bullet.transform.parent = null;
    }

    public void ProcessOnCollision(Collision collision, Rigidbody weaponBody)
    {
        BaseWeapon component = collision.gameObject.GetComponent<BaseWeapon>();
        if (((component != null) && (component.Stickman != null)) && (component != this))
        {
            Vector3 vector = component.rigidbody.position - collision.contacts[0].point;
            base.AddForce(component.rigidbody, (Vector2) ((vector.normalized * base.Game.BlowForce) * base.BlowMultiplier), collision.contacts[0].point, base.Game.BlowTime);
        }
        else
        {
            base.Process(collision, weaponBody);
        }
        this._bullet.gameObject.SetActive(false);
        this._bullet.transform.parent = base.transform;
    }

    public override void SetAdditionalPose(object startPose, object endPose, float time)
    {
        AdditionalPose pose = (AdditionalPose) startPose;
        AdditionalPose pose2 = (AdditionalPose) endPose;
        this._bullet.transform.rotation = pose2.BulletRotation;
        if (pose2.IsShoot)
        {
            this._bullet.transform.position = pose2.BulletPosition;
        }
        else
        {
            this._bullet.transform.position = Vector3.Lerp(pose.BulletPosition, pose2.BulletPosition, time);
        }
        this._bullet.gameObject.SetActive(pose.IsBulletVisible);
        this._muzzleFlashTransform.localScale = Vector3.Lerp(pose.MuzzleScale, pose2.MuzzleScale, time);
        this._muzzleFlashSprite.material.color = Color.Lerp(pose.MuzzleColor, pose2.MuzzleColor, time);
    }

    public override void SetToStickman(Stickman stickman, StickmanPart part)
    {
        base.SetToStickman(stickman, part);
        if (base.Stickman != null)
        {
            this._time = Time.time + UnityEngine.Random.Range(0f, this._rechargeTime);
        }
    }

    [DebuggerHidden]
    private IEnumerator StartBullet(float delay) => 
        new <StartBullet>c__Iterator7 { 
            delay = delay,
            <$>delay = delay,
            <>f__this = this
        };

    protected override void Update()
    {
        base.Update();
        if ((base.Game.IsControlsAvailable && (base.Stickman != null)) && ((Time.time - this._time) >= this._rechargeTime))
        {
            base.StartCoroutine(this.StartBullet(0.05f));
            base.Game.PlayShootSound();
            this._muzzleFlashTransform.localScale = Vector3.zero;
            object[] args = new object[] { "scale", Vector3.one, "time", 0.1f, "easetype", iTween.EaseType.linear };
            iTween.ScaleTo(this._muzzleFlashTransform.gameObject, iTween.Hash(args));
            object[] objArray2 = new object[] { "a", 1, "time", 0f };
            iTween.ColorTo(this._muzzleFlashSprite.gameObject, iTween.Hash(objArray2));
            object[] objArray3 = new object[] { "a", 0, "delay", 0.05f, "time", 0.05f };
            iTween.ColorTo(this._muzzleFlashSprite.gameObject, iTween.Hash(objArray3));
            this._time = Time.time;
        }
    }

    [CompilerGenerated]
    private sealed class <StartBullet>c__Iterator7 : IDisposable, IEnumerator, IEnumerator<object>
    {
        internal object $current;
        internal int $PC;
        internal float <$>delay;
        internal GunWeapon <>f__this;
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
                    this.<>f__this._bullet.transform.parent = null;
                    this.<>f__this._bullet.gameObject.SetActive(true);
                    this.<>f__this._bullet.transform.position = this.<>f__this._bulletPosition.position;
                    this.<>f__this._bullet.transform.rotation = this.<>f__this._bulletPosition.rotation;
                    this.<>f__this._isShoot = true;
                    this.<>f__this.Game.AddWeaponKey(this.<>f__this);
                    this.<>f__this._isShoot = false;
                    Vector3 vector = this.<>f__this.WeaponBodyToSet.position - this.<>f__this._bulletPosition.position;
                    this.<>f__this.AddForce(this.<>f__this.WeaponBodyToSet, (Vector2) ((vector.normalized * this.<>f__this.Game.BlowForce) * 0.25f), this.<>f__this._bulletPosition.position, this.<>f__this.Game.BlowTime);
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

    private class AdditionalPose
    {
        public Vector3 BulletPosition;
        public Quaternion BulletRotation;
        public bool IsBulletVisible;
        public bool IsShoot;
        public Color MuzzleColor;
        public Vector3 MuzzleScale;
    }
}

