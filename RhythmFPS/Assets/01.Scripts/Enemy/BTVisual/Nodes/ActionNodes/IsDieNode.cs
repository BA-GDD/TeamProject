using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BTVisual;
public class IsDieNode : ActionNode
{
    private bool _alreadyDie = false;

    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        if(brain.health.CurrentHitPoint <= 0 && !_alreadyDie)
        {
            _alreadyDie = true;
            return State.SUCCESS;
        }
        return State.FAILURE;
    }
}
