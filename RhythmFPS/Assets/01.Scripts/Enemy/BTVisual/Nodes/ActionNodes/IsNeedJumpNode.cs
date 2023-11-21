using BTVisual;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IsNeedJumpNode : ActionNode
{
    [SerializeField]
    private LayerMask _groundLayerMask;

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
        if (path.status == NavMeshPathStatus.PathPartial
            || Vector3.Distance(brain.transform.position, GameManager.instance.PlayerTransform.position) > 25f)
        {
            Debug.Log("점프해야함");
            return State.SUCCESS;
        }


        return State.FAILURE;
    }
}
