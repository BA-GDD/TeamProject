using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Grenade")]
public class GrenadeAbility : Ability
{
    [SerializeField] private float _cooltime = 3;

    private PlayerAnimator _animator;
    private AgentWeapon _agentWeapon;

    private bool _isCanActive = true;

    private void OnEnable()
    {
        _isCanActive = true;
    }
    public override void Active(GameObject player)
    {
        if (_isCanActive == false)
            return;

        _isCanActive = false;

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
        UIManager.Instanace.HandleUseSkill(_cooltime);
        CoroutineDelegate.Instance.StartCoroutine(CoolDown());
        
    }
    private IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(_cooltime);
        _isCanActive = true;
    }
}
