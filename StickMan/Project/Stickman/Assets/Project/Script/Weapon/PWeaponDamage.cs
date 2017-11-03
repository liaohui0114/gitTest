using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PWeaponDamage : MonoBehaviour
{

    private event GameConfig.VoidDelegate WeaponHitHandler;

    public abstract void Init(ArrayList args);

    public abstract float Damage(GameObject self, GameObject other);
}
