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

    protected PlayerAnimator _animator;

    public bool isReadyReload;
    public AnimationClip relaodStartClip;
    public AnimationClip relaodClip;
    public AnimationClip reloadEndClip;

    public AudioClip fireAudio;
    public AudioClip reloadAudio;

    public UnityEvent fireFeedback;


    public virtual void Init(PlayerAnimator animator)
    {
        _cam = Define.MainCam;
        _animator = animator;
    }
    /// <summary>
    ///  실제 총이 발사하는 함수
    /// </summary>
    public abstract bool Fire();
    /// <summary>
    ///  실제 총이 발사하는 장전
    /// </summary>
    public abstract void Reload();
    public virtual void AddBullet()
    {
        _currentBullet++;
        if (_currentBullet == _maxBullet)
        {
            isReadyReload = false;
            _animator.SetRigBoolIsReload(false);
            _animator.SetRigTriggerReload(false);
        }
    }
}
