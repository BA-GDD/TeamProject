using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityWeapon : MonoBehaviour
{
    public AbilityWeaponType type;
    public abstract void Action();
}
