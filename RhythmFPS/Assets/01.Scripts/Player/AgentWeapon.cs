using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AgentWeapon : MonoBehaviour
{
    [SerializeField] private Weapon _curWeapon;
    [SerializeField] private InputReader _inputReader;
    public Weapon CurWeapon => _curWeapon;
    private PlayerAnimator _animator;
    private GunSound _gunSound;

    private void Awake()
    {
        _animator = transform.Find("Visual").GetComponent<PlayerAnimator>();
        _gunSound = transform.Find("GunSound").GetComponent<GunSound>();
        _curWeapon?.Init(_animator);
    }

    private void Start()
    {
        _inputReader.fireEvnet += Active;
        _inputReader.reloadEvent += Reload;
    }
    public void Active()
    {
        if (RhythmManager.instance.Judgement() == false) return;
        bool? isAttack = _curWeapon?.Fire();
        if (isAttack.Value == true)
        {
            _gunSound?.PlayFireSound();
        }
        else
        {
            _gunSound?.PlayLackOfAmmoSound();
        }
    }

    public void Reload()
    {
        if (RhythmManager.instance.Judgement() == false) return;
        _curWeapon?.Reload();
    }
    public bool GetCurWeaponReloading()
    {
        return _curWeapon.isReadyReload;
    }
    public void ChangeWeapon(Weapon newWeapon)
    {
        _curWeapon = newWeapon;
        _animator.ChangeWeaponAnimation(newWeapon.relaodStartClip);
        _animator.ChangeWeaponAnimation(newWeapon.relaodClip);
        _animator.ChangeWeaponAnimation(newWeapon.reloadEndClip);
    }
    private void OnDestroy()
    {
        _inputReader.fireEvnet -= Active;
        _inputReader.reloadEvent -= Reload;
    }
}
