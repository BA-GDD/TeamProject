using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossBrain : EnemyBrain
{
    private BossAnimator _bossAnimator;
    public BossAnimator BossAnimator => _bossAnimator;

    public float timer;
    private float _coolTime = 3f;
    private bool _isCanAttack = false;
    public bool IsCanAttack
    {
        get => _isCanAttack;
        set => _isCanAttack = value;
    }

    private bool _isMove = true;
    public bool IsMove
    {
        get => _isMove;
        set => _isMove = value;
    }

    [SerializeField] private GameObject _weapon;
    public GameObject Weapon => _weapon;
    [SerializeField] private GameObject _shield;
    public GameObject Shield => _shield;

    public Transform collisionCheckPos;

    public int wormCnt = 0;
    public int spectorCnt = 0;
    public string spawnEnemyName = "";

    protected override void Awake()
    {
        base.Awake();
        _bossAnimator = GetComponent<BossAnimator>();
    }

    public override void Init()
    {

    }

    private void Update()
    {
        if(isDead != true)
        {
            if (timer < _coolTime)
            {
                timer += Time.deltaTime;
            } 
            else
            {
                _isCanAttack = true;
                _isMove = false;
            }

            if (_isMove)
            {
                _bossAnimator.SetMove(true);
                if (agent.enabled)
                {
                    StartChase();
                }
            }
            else
            {
                _bossAnimator.SetMove(false);
                if (agent.enabled)
                {
                    StopChase();
                }
            }
        }
    }

    public override void SetDead()
    {
        _bossAnimator.SetMove(false);
        StopChase();
        _isCanAttack = false;
        base.SetDead();
        _bossAnimator.SetDead();
        GameManager.instance.GameClear(0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 3f);
    }
}
