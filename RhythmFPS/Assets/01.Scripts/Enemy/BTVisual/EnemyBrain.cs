using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBrain : PoolableMono
{
    //[HideInInspector]
    //public GameObject weapon;
    [HideInInspector]
    public NavMeshAgent agent;
    [HideInInspector]
    public EnemyAttack attack;
    [HideInInspector]
    public EnemyHealth health;
    [HideInInspector]
    public bool canRotate;
    [HideInInspector]
    public bool isDead;
    public EnemyStatusSO status;

    public bool isOnTheRoof = false;

    protected virtual void Awake()
    {
        canRotate = true;
        attack = GetComponentInChildren<EnemyAttack>();
        health = GetComponent<EnemyHealth>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = status.moveSpeed;
    }

    protected async virtual void OnEnable()
    {
        await Task.Delay(1000);
        RhythmManager.instance.onNotedTimeAction += attack.Attack;
    }

    protected virtual void Start()
    {
        StartChase();
    }

    public virtual void SetDead()
    {
        isDead = true;
        RhythmManager.instance.onNotedTimeAction -= attack.Attack;
    }

    public virtual void StartChase()
    {
        agent.isStopped = false;

        agent.SetDestination(GameManager.instance.PlayerTransform.position);
    }

    public virtual void StopChase()
    {
        agent.ResetPath();

        agent.isStopped = true;
    }
}
