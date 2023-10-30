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
        if (Vector3.Distance(transform.position, movePos) <= .1f) isMove = false;
        else isMove = true;
        
        transform.position = Vector3.Lerp(transform.position, movePos, Time.deltaTime * 10.0f);

        if (isRot) transform.rotation = Quaternion.LookRotation(targetTrm.position - transform.position);
    }
}
