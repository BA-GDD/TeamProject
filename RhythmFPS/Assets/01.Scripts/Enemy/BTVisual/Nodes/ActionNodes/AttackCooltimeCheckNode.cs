using BTVisual;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCooltimeCheckNode : ActionNode
{
    protected override void OnStart()
    {
        blackboard.curPattern = -1;
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        Debug.Log("check");
        if ((brain as BossBrain).isCanAttack)
        {
            Debug.Log("Start Attack");
            return State.SUCCESS;
        }
        else
            return State.RUNNING;
    }
}
