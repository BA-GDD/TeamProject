using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBrain : PoolableMono
{
    [HideInInspector]
    public GameObject weapon;
    [HideInInspector]
    public NavMeshAgent agent;
    [HideInInspector]
    public EnemyHealth enemyHealth;
    [HideInInspector]
    public bool canRotate;
    [HideInInspector]
    public bool isDead;
    public EnemyStatusSO status;

    public abstract void Attack();

    protected virtual void Awake()
    {
        canRotate = true;
        enemyHealth = GetComponent<EnemyHealth>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = status.moveSpeed;
    }

    protected virtual void Start()
    {
        StartChase();
    }

    public virtual void SetDead()
    {
        isDead = true;
    }

    public virtual void StartChase()
    {
        agent.isStopped = false;

        agent.SetDestination(GameManager.instance.playerTransform.position);
    }

    public virtual void StopChase()
    {
        agent.ResetPath();

        agent.isStopped = true;
    }
}
