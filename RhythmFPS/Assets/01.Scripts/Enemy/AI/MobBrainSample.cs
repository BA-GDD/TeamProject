using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobBrainSample : EnemyBrain
{
    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField]
    private float attackDelay = 2f;
    public float attackRange = 3f;
    private float attackTimer;

    protected override void Awake()
    {
        base.Awake();

        attackTimer = attackDelay;
    }

    protected override void Update()
    {
        base.Update();

        attackTimer += Time.deltaTime;
    }

    public override void Attack()
    {
        if (attackTimer >= attackDelay)
        {
            Debug.Log("Mob Attack");

            attackTimer = 0f;
        }
    }

    public override void Move()
    {
        movePos.y = transform.position.y;

        if (Vector3.Distance(transform.position, movePos) > attackRange)
        {
            transform.position = Vector3.Lerp(transform.position, movePos, Time.deltaTime * moveSpeed);
        }

        if (isRot)
        {
            Vector3 direction = targetTrm.position - transform.position;
            direction.y = 0f;
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
