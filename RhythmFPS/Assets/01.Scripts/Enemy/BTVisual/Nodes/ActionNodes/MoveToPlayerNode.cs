using BTVisual;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPlayerNode : ActionNode
{
    public LayerMask ground;

    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        return State.SUCCESS;
    }
}
