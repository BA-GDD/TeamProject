using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentWeapon : MonoBehaviour
{
    [SerializeField]private Weapon _curWeapon;
    private PlayerAnimator _animator;

    private void Awake()
    {
        _animator = transform.Find("Visual").GetComponent<PlayerAnimator>();
        _curWeapon?.Init(_animator);
    }
    public void Active()
    {
        if (RhythmManager.instance.Judgement(RhythmAction.Shoot) == false) return;
        _curWeapon?.Fire();
    }
    public void Reload()
    {
        if (RhythmManager.instance.Judgement(RhythmAction.Reload) == false) return;

        _curWeapon?.Reload();
    }
    public void ChangeWeapon(Weapon newWeapon)
    {
        _curWeapon = newWeapon;
        _animator.ChangeWeaponAnimation(newWeapon.relaodStartClip);
        _animator.ChangeWeaponAnimation(newWeapon.relaodClip);
        _animator.ChangeWeaponAnimation(newWeapon.reloadEndClip);
    }
}
