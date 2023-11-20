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

        if (brain.agent.enabled)
        {
            brain.agent.CalculatePath(GameManager.instance.PlayerTransform.position, path);
        }
        else
        {
            brain.agent.enabled = true;

            brain.agent.CalculatePath(GameManager.instance.PlayerTransform.position, path);

            brain.agent.enabled = false;
        }

        return path.status == NavMeshPathStatus.PathComplete ? State.SUCCESS : State.FAILURE;
    }
}
