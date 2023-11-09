using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBoss : EnemyBrain
{
    public override void Attack()
    {
        
    }

    public override void Move()
    {
        if (Vector3.Distance(transform.position, moveDestination) <= 1f) isMove = false;
        else isMove = true;
        
        transform.position = Vector3.Lerp(transform.position, moveDestination, Time.deltaTime * 10.0f);

        if (canRotate) transform.rotation = Quaternion.LookRotation(targetTransform.position - transform.position);
    }
}
