using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BTVisual;

public class DieNode : ActionNode
{
    protected override void OnStart()
    {
        brain.health.Die();
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        return State.SUCCESS;
    }
}
