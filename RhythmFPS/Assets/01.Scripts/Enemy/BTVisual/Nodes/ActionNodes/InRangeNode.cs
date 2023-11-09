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
        Vector2 myCoord = brain.transform.position;
        Vector2 targetCoord = brain.targetTransform.position;
        myCoord.y = targetCoord.y = 0f;

        if (Vector2.Distance(myCoord, targetCoord) > (brain as MobBrain).status.attackRange)
        {
            return State.FAILURE;
        }

        return State.SUCCESS;
    }
}
