using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AbilityWeaponType
{
    None,
    GrenadeLuncher,
}
public abstract class Ability : ScriptableObject
{
    public AbillityKey key;
    public AbilityWeaponType weaponType;
    protected AbilityWeapon _abilityWeapon;
    public abstract void Active(GameObject player);
}
