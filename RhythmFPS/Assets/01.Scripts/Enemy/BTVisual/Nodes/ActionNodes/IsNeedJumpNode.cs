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
        if (path.status == NavMeshPathStatus.PathPartial
            || Vector3.Distance(brain.transform.position, GameManager.instance.PlayerTransform.position) > 25f)
        {
            Debug.Log("점프해야함");
            return State.SUCCESS;
        }

        return State.FAILURE;
    }
}
