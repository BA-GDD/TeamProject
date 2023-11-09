using BTVisual;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InRangeNode : ActionNode
{
    protected override void OnStart()
    {

    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        if (Vector3.Distance(brain.transform.position, brain.agent.destination) > brain.status.attackRange)
        {
            return State.FAILURE;
        }

        return State.SUCCESS;
    }
}
