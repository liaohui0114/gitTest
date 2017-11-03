using System;
using UnityEngine;

public class Weapon : BaseWeapon
{
    private Collider _collider;
    private float _disableTime;
    private Vector3 _lastPosition;
    private Rigidbody _rigidbody;

    protected override void Awake()
    {
        base.Awake();
        base.WeaponBodyToSet = base.rigidbody;
        this._rigidbody = base.rigidbody;
        this._collider = base.collider;
    }

    private void FixedUpdate()
    {
        if ((this._collider != null) && (this._rigidbody != null))
        {
            if ((Time.time - this._disableTime) > 0.3f)
            {
                this._collider.enabled = true;
            }
            if (base.Stickman != null)
            {
                Vector3 vector = this._rigidbody.position - this._lastPosition;
                if (vector.sqrMagnitude == 0f)
                {
                    this._collider.enabled = false;
                    this._disableTime = Time.time;
                }
            }
            this._lastPosition = this._rigidbody.position;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        base.Process(collision, this._rigidbody);
    }

    private void OnDrawGizmos()
    {
        if ((this._collider != null) && (this._rigidbody != null))
        {
            Gizmos.color = !this._collider.enabled ? Color.gray : Color.green;
            Gizmos.DrawSphere(this._rigidbody.position, 1f);
        }
    }

    public override void Release()
    {
        if (base.Stickman != null)
        {
            Collider[] componentsInChildren = base.Stickman.GetComponentsInChildren<Collider>();
            for (int i = 0; i < componentsInChildren.Length; i++)
            {
                base.EnableCollision(componentsInChildren[i], this._collider, 1f);
            }
        }
        base.Release();
    }

    public override void SetToStickman(Stickman stickman, StickmanPart part)
    {
        base.SetToStickman(stickman, part);
        if (((base.Stickman != null) && (base.Stickman.CurrentHealth > 0)) && (this._collider != null))
        {
            Collider[] componentsInChildren = base.Stickman.GetComponentsInChildren<Collider>();
            for (int i = 0; i < componentsInChildren.Length; i++)
            {
                if (componentsInChildren[i] != this._collider)
                {
                    Physics.IgnoreCollision(componentsInChildren[i], this._collider, true);
                }
            }
        }
    }
}

