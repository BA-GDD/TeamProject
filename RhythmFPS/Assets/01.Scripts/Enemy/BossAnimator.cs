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

    private Animator _animator;
    public Animator Animator => _animator;

    public void StopAnimation(bool value)
    {
        _animator.speed = value ? 0 : 1; //true일 때 0으로 만들어서 정지, 
        //아닐때 1로 만들어서 다시 재생
    }

    public void SetDead()
    {
        _animator.SetBool(_isDeadHash, true);
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
}
