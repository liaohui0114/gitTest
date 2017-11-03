using UnityEngine;
using System.Collections;

public interface IArmable  {

    void Arm(Weapon weapon);

    void Disarm(Weapon weapon);
}
