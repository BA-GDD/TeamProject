using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobBrain : EnemyBrain
{
    [SerializeField]
    private MobAnimator _animator;
    public MobAnimator Animator => _animator;
    private float _attackTimer;

    protected override void Awake()
    {
        base.Awake();

        _attackTimer = status.attackDelay;
    }

    private void Update()
    {
        _attackTimer += Time.deltaTime;
    }

    public override void Init()
    {
        isDead = false;
    }

    public override void Attack()
    {
        if (_attackTimer >= status.attackDelay)
        {
            _animator.SetAttackTrigger(true);

            _attackTimer = 0f;
        }
    }

    public override void SetDead()
    {
        base.SetDead();
        _animator.SetDieTrigger(true);
    }

    public void OnDieEvent()
    {
        PoolManager.Instance.Push(this);
    }
}
