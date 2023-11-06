using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
using UnityEngine.Events;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected int _maxBullet;
    [SerializeField] protected LayerMask _whatIsEnemy;

    protected int _currentBullet;
    protected Camera _cam;
    protected bool _isReadyReload;

    protected PlayerAnimator _animator;

    public AnimationClip relaodStartClip;
    public AnimationClip relaodClip;
    public AnimationClip reloadEndClip;

    public UnityEvent fireFeedback;
    public UnityEvent lackOfAmmoEvent;


    public virtual void Init(PlayerAnimator animator)
    {
        _cam = Define.MainCam;
        _animator = animator;
    }
    /// <summary>
    ///  실제 총이 발사하는 함수
    /// </summary>
    public abstract void Fire();
    /// <summary>
    ///  실제 총이 발사하는 장전
    /// </summary>
    public abstract void Reload();
    public virtual void AddBullet()
    {
        _currentBullet++;
        if (_currentBullet == _maxBullet)
        {
            _isReadyReload = false;
            _animator.SetRigBoolIsReload(false);
        }
    }
}
