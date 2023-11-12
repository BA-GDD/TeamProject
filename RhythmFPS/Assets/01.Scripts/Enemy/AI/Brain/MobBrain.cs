using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobBrain : EnemyBrain
{
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

    public override void Attack()
    {
        if (_attackTimer >= status.attackDelay)
        {
            Debug.Log("Mob Attack");

            _attackTimer = 0f;
        }
    }
}
