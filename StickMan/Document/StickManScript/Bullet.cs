using System;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float _speed = 100f;

    private void OnCollisionEnter(Collision collision)
    {
        this.Parent.ProcessOnCollision(collision, base.rigidbody);
    }

    private void Update()
    {
        if (base.transform.parent == null)
        {
            base.transform.Translate(this._speed * Time.deltaTime, 0f, 0f, Space.Self);
        }
    }

    public GunWeapon Parent { get; set; }
}

