using BTVisual;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobRangeNode : ActionNode
{
    protected override void OnStart()
    {

    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        return Vector3.Distance(brain.transform.position, GameManager.instance.playerTransform.position) > brain.status.attackRange ? State.FAILURE : State.SUCCESS;
    }
}
