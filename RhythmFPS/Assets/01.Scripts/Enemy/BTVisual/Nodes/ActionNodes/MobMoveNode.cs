using BTVisual;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobMoveNode : ActionNode
{
    protected override void OnStart()
    {
        brain.StartChase();
    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        return State.SUCCESS;
    }
}
