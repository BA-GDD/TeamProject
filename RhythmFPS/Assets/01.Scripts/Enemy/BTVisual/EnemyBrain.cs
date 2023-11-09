using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBrain : MonoBehaviour
{
    public GameObject weapon;
    public NavMeshAgent agent;
    public EnemyHealth enemyHealth;
    public EnemyStatusSO status;
    public bool isMove;
    public bool canRotate;
    public bool isDead;

    public abstract void Attack();

    protected virtual void Awake()
    {
        canRotate = true;
        enemyHealth = GetComponent<EnemyHealth>();
        agent = GetComponent<NavMeshAgent>();
    }

    public virtual void SetDead()
    {
        isDead = true;
    }
}
