using BTVisual;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CheckPathNode : ActionNode
{
    protected override void OnStart()
    {

    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        NavMeshPath path = new NavMeshPath();

        brain.agent.CalculatePath(GameManager.instance.PlayerTransform.position, path);

        return path.status == NavMeshPathStatus.PathComplete ? State.SUCCESS : State.FAILURE;
    }
}
