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
        if (Vector2.Distance(new Vector2(brain.transform.position.x, brain.transform.position.z), new Vector2(brain.targetTrm.position.x, brain.targetTrm.position.z)) > (brain as MobBrainSample).attackRange)
        {
            return State.FAILURE;
        }

        return State.SUCCESS;
    }
}
