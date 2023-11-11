using BTVisual;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCooltimeCheckNode : ActionNode
{
    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        if ((brain as BossBrain).isCanAttack)
        {
            return State.SUCCESS;
        }
        else
            return State.RUNNING;
    }
}
