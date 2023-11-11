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

    public float timer;
    public float coolTime = 3f;
    public bool isCanAttack = false;

    protected override void Awake()
    {
        base.Awake();
        navAgent = GetComponent<NavMeshAgent>();
        _bossAnimator = GetComponent<BossAnimator>();
    }

    public override void Attack()
    {

    }

    private void Update()
    {
        if(timer < coolTime)
        {
            timer += Time.deltaTime;
        }
        else
        {
            isCanAttack = true;
        }

        //Debug.Log(Vector3.Distance(transform.position, GameManager.instance.playerTransform.position) <= 4f);
        if(!(Vector3.Distance(transform.position, GameManager.instance.playerTransform.position) <= 3f))
        {
            _bossAnimator.SetMove(true);
            StartChase();
        }
        else
        {
            _bossAnimator.SetMove(false);
            StopChase();
        }
    }

    public override void SetDead()
    {
        base.SetDead();
        _bossAnimator.StopAnimation(true);
        _bossAnimator.SetDead();
    }
}
