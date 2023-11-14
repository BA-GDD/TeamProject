using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobBrain : EnemyBrain
{
    [SerializeField]
    private MobAnimator _animator;
    public MobAnimator Animator => _animator;

    public override void Init()
    {
        isDead = false;
    }

    public override void SetDead()
    {
        base.SetDead();
        _animator.SetDieTrigger(true);
    }

    public void OnDieEvent()
    {
        PoolManager.Instance.Push(this);
    }
}
