using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BTVisual;

public class DieNode : ActionNode
{
    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        brain.enemyHealth.Die();
        return State.SUCCESS;
    }
}
