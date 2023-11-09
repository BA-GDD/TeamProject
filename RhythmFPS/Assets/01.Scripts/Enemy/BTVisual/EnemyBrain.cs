using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBrain : MonoBehaviour
{
    public Transform targetTrm;
    public Vector3 movePos;
    public bool isMove;
    public bool isRot;
    public bool isDead;
    public EnemyHealth enemyHealth;

    // 새로 추가한 것
    public GameObject weapon;
    private BossAnimator _bossAnimator;
    public BossAnimator BossAnimator => _bossAnimator;


    public abstract void Attack();
    public abstract void Move();

    protected virtual void Awake()
    {
        isRot = true;
        movePos = transform.position;
        enemyHealth = GetComponent<EnemyHealth>();
        targetTrm = GameManager.instance.playerTransform;
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
