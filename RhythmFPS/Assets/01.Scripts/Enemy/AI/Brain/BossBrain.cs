using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBrain : EnemyBrain
{
    private BossAnimator _bossAnimator;
    public BossAnimator BossAnimator => _bossAnimator;

    protected override void Awake()
    {
        base.Awake();

        _bossAnimator = GetComponent<BossAnimator>();
    }

    public override void Attack()
    {
        
    }

    public override void SetDead()
    {
        base.SetDead();
        _bossAnimator.StopAnimation(true);
        _bossAnimator.SetDead();
    }
}
