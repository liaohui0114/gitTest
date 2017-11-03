using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class WeaponPart : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        this.Parent.ProcessOnCollision(collision, base.rigidbody);
    }

    public ComplexWeapon Parent { get; set; }
}

