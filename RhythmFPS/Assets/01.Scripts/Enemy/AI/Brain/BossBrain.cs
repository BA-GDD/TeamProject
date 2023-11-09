using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossBrain : EnemyBrain
{
    public NavMeshAgent navAgent;

    private BossAnimator _bossAnimator;
    public BossAnimator BossAnimator => _bossAnimator;

    protected override void Awake()
    {
        base.Awake();
        navAgent = GetComponent<NavMeshAgent>();
        _bossAnimator = GetComponent<BossAnimator>();
        navAgent.SetDestination(GameManager.instance.playerTransform.position);
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
