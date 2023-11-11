using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Grenade")]
public class GrenadeAbility : Ability
{
    private PlayerAnimator _animator;
    private AgentWeapon _agentWeapon;
    public override void Active(GameObject player)
    {
        if(_abilityWeapon == null)
        {
            _abilityWeapon = player.GetComponent<AgentAbility>().GetAbilityWeapon(weaponType);
        }
        if(_animator == null)
        {
            _animator = player.transform.Find("Visual").GetComponent<PlayerAnimator>();
        }
        if(_agentWeapon == null)
        {
            _agentWeapon = player.GetComponent<AgentWeapon>();
        }

        _abilityWeapon.gameObject.SetActive(true);
        _animator.SetRigTriggerReloadCancel(_agentWeapon.GetCurWeaponReloading());
        _animator.SetRigTriggerAbility(true);
        _animator.SetRigBoolIsReload(false);
        
    }
}
