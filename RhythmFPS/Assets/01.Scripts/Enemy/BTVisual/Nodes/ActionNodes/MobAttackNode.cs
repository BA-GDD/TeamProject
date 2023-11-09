using BTVisual;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobAttackNode : ActionNode
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
        brain.Attack();

        return State.SUCCESS;
    }
}
