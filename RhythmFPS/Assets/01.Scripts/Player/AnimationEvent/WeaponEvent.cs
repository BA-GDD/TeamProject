using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEvent : MonoBehaviour
{
    [SerializeField]
    private AgentWeapon _weapon;
    [SerializeField]
    private AgentAbility _agentAbility;

    public void Init(Transform rootTrm)
    {

    }
    public void PushBulletInWeapon()
    {
        _weapon.CurWeapon.AddBullet();
    }
    public void ShotGrenade()
    {
        _agentAbility.GetAbilityWeapon(AbilityWeaponType.GrenadeLuncher).Action();
    }
    public void OffAbilityWeapon()
    {
        _agentAbility.OffAbilityWeapon();
    }
}
