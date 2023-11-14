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

    public bool isMove = true;

    public GameObject weapon;
    public GameObject shield;

    public Transform jumpUpCheckPos;
    public Transform jumpDownCheckPos;
    public Transform collisionCheckPos;

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
        if(isDead != true)
        {
            if (timer < coolTime)
            {
                timer += Time.deltaTime;
            } 
            else
            {
                isCanAttack = true;
                isMove = false;
            }

            //Debug.Log(Vector3.Distance(transform.position, GameManager.instance.playerTransform.position) <= 4f);
            if (isMove)
            {
                //Debug.Log("¿òÁ÷ÀÓ");
                _bossAnimator.SetMove(true);
                StartChase();
            }
            else
            {

                _bossAnimator.SetMove(false);
                StopChase();
            }
        }
    }

    public override void SetDead()
    {
        base.SetDead();
        _bossAnimator.StopAnimation(true);
        _bossAnimator.SetDead();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 3f);
    }
}
