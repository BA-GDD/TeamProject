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

        return NavMesh.CalculatePath(brain.transform.position, GameManager.instance.playerTransform.position, NavMesh.AllAreas, path) ? State.SUCCESS : State.FAILURE;
    }
}
