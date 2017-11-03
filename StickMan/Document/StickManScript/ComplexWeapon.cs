using System;
using UnityEngine;

public class ComplexWeapon : BaseWeapon
{
    public WeaponPart[] Damagables;

    protected override void Awake()
    {
        base.Awake();
        for (int i = 0; i < this.Damagables.Length; i++)
        {
            this.Damagables[i].Parent = this;
            base._parts.Add(this.Damagables[i].transform);
        }
        base.WeaponBodyToSet = this.Damagables[0].rigidbody;
    }

    public void ProcessOnCollision(Collision collision, Rigidbody weaponBody)
    {
        base.Process(collision, weaponBody);
    }

    public override void Release()
    {
        if (base.Stickman != null)
        {
            Collider[] componentsInChildren = base.Stickman.GetComponentsInChildren<Collider>();
            for (int i = 0; i < componentsInChildren.Length; i++)
            {
                for (int j = 0; j < this.Damagables.Length; j++)
                {
                    base.EnableCollision(componentsInChildren[i], this.Damagables[j].collider, 1f);
                }
            }
        }
        base.Release();
    }

    public override void SetToStickman(Stickman stickman, StickmanPart part)
    {
        base.SetToStickman(stickman, part);
        if ((base.Stickman != null) && (base.Stickman.CurrentHealth > 0))
        {
            Collider[] componentsInChildren = base.Stickman.GetComponentsInChildren<Collider>();
            for (int i = 0; i < componentsInChildren.Length; i++)
            {
                for (int j = 0; j < this.Damagables.Length; j++)
                {
                    Physics.IgnoreCollision(componentsInChildren[i], this.Damagables[j].collider, true);
                }
            }
        }
    }
}

