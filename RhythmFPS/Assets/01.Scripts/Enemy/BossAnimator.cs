using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimator : MonoBehaviour
{
    private readonly int _isDeadHash = Animator.StringToHash("is_dead");
    private readonly int _isAttackHash = Animator.StringToHash("attack");
    private readonly int _isMoveHash = Animator.StringToHash("is_move");
    private readonly int _attackCnt = Animator.StringToHash("attack_cnt");
    private readonly int _deadHash = Animator.StringToHash("dead");

    public event Action OnAnimationTrigger = null;

    public bool isAnimationClipEnd = false;

    [SerializeField]
    private Animator _animator;
    public Animator Animator => _animator;

    public void OnAnimationStop()
    {
        _animator.speed = 0;
    }

    public void OnAnimationPlay()
    {
        _animator.speed = 1;
    }

    public void OnAnimationEvent()
    {
        print("OnAnimation");
        OnAnimationTrigger?.Invoke();
    }

    public void OnAnimationEndEvent()
    {
        isAnimationClipEnd = true;
        SetAttackTrigger(false);
        SetMove(true);
    }

    public void StopAnimation(bool value)
    {
        _animator.speed = value ? 0 : 1; //true일 때 0으로 만들어서 정지, 
        //아닐때 1로 만들어서 다시 재생
    }

    public void SetDead()
    {
        _animator.SetTrigger(_deadHash);
    }

    public void SetAttackTrigger(bool value)
    {
        if (value)
            _animator.SetTrigger(_isAttackHash);
        else
            _animator.ResetTrigger(_isAttackHash);
    }

    public void SetAttackPattern(int value)
    {
        _animator.SetInteger(_attackCnt, value);
    }

    public void SetMove(bool value)
    {
        _animator.SetBool(_isMoveHash, value);
    }

    public void SetAnimationClipEndState(bool value)
    {
        isAnimationClipEnd = value;
    }
}
