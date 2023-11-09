using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBrain : MonoBehaviour
{
    public Transform targetTransform;
    public Vector3 moveDestination;
    public bool isMove;
    public bool canRotate;
    public bool isDead;
    public EnemyHealth enemyHealth;

    // 새로 추가한 것
    public GameObject weapon;
    public EnemyStatusSO status;
    private BossAnimator _bossAnimator;
    public BossAnimator BossAnimator => _bossAnimator;


    public abstract void Attack();
    public abstract void Move();

    protected virtual void Awake()
    {
        canRotate = true;
        moveDestination = transform.position;
        enemyHealth = GetComponent<EnemyHealth>();
        targetTransform = GameManager.instance.playerTransform;
        _bossAnimator = GetComponent<BossAnimator>();
    }

    protected virtual void Update()
    {
        Move();
    }

    public void SetDead()
    {
        isDead = true;
        _bossAnimator.StopAnimation(true);
        _bossAnimator.SetDead();
    }
}
