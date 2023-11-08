using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEvent : MonoBehaviour
{
    [SerializeField]
    private AgentWeapon _weapon;
    public void PushBulletInWeapon()
    {
        _weapon.CurWeapon.AddBullet();
    }
}
