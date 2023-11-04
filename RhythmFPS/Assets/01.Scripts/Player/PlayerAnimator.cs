using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    #region 애니메이션 변수 hash들
    private readonly int _speedHash = Animator.StringToHash("speed");

    private readonly int _fireHash = Animator.StringToHash("fire");
    private readonly int _isReloadHash = Animator.StringToHash("is-reload");
    private readonly int _reloadHash = Animator.StringToHash("reload");
    private readonly int _reloadCancleHash = Animator.StringToHash("reloadCancel");
    #endregion

    private Animator _animator;
    private Animator _rigAnimator;
    private AnimationClip[] _clips;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigAnimator = transform.Find("Rigging").GetComponent<Animator>();
        _clips = _rigAnimator.runtimeAnimatorController.animationClips;

    }
    private void Start()
    {
        _rigAnimator.enabled = true;
    }
    public void SetFloatSpeed(float value)
    {
        _animator.SetFloat(_speedHash, value);
    }
    public void SetRigTriggerFIre(bool value)
    {
        if(value)
            _rigAnimator.SetTrigger(_fireHash);
        else
            _rigAnimator.ResetTrigger(_fireHash);
    }
    public void SetRigBoolIsReload(bool value)
    {
        _rigAnimator.SetBool(_isReloadHash, value);
    }
    public void SetRigTriggerReload(bool value)
    {
        if (value)
            _rigAnimator.SetTrigger(_reloadHash);
        else
            _rigAnimator.ResetTrigger(_reloadHash);
    }
    public void SetRigTriggerReloadCancel(bool value)
    {
        if (value)
            _rigAnimator.SetTrigger(_reloadCancleHash);
        else
            _rigAnimator.ResetTrigger(_reloadCancleHash);
    }
    public void ChangeWeaponAnimation(AnimationClip newClip)
    {
        for(int i = 0; i < _clips.Length; i++)
        {
            if(_clips[i].name == newClip.name)
            {
                _clips[i] = newClip;
                break;
            }
        }
    }
}
