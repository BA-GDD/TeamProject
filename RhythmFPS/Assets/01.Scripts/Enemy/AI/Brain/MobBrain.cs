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

    protected override void Update()
    {
        base.Update();

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

    public override void Move()
    {
        moveDestination.y = transform.position.y;

        if (Vector3.Distance(transform.position, moveDestination) > status.attackRange)
        {
            transform.position = Vector3.Lerp(transform.position, moveDestination, Time.deltaTime * status.moveSpeed);
        }

        if (canRotate)
        {
            Vector3 direction = targetTransform.position - transform.position;
            direction.y = 0f;
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
