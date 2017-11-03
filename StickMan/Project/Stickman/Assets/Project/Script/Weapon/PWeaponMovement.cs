using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PWeaponMovement : MonoBehaviour
{

    public abstract void Init(ArrayList args);
    public abstract float Move(GameObject self);
}
