using BTVisual;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRangeNode : ActionNode
{
    [SerializeField] private float _distance;

    protected override void OnStart()
    {

    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        return Vector3.Distance(brain.transform.position, GameManager.instance.PlayerTransform.position) > _distance ? State.FAILURE : State.SUCCESS;
    }
}
