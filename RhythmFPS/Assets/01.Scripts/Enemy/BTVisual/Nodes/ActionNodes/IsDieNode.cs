using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BTVisual;
public class IsDieNode : ActionNode
{
    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        if(brain.health.CurrentHitPoint <= 0)
        {
            return State.SUCCESS;
        }
        return State.FAILURE;
    }
}
