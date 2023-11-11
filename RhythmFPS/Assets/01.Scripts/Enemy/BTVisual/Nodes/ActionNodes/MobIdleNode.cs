using BTVisual;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobIdleNode : ActionNode
{
    protected override void OnStart()
    {
        brain.StopChase();
    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        return State.SUCCESS;
    }
}
