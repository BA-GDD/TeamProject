using BTVisual;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushPatternNode : ActionNode
{
    [SerializeField] private LayerMask _layerMask;

    protected override void OnStart()
    {

    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        Vector3 targetPos = brain.transform.position;
        RaycastHit hit;
        bool isHit = Physics.SphereCast(brain.transform.position, 10f, Vector3.forward, out hit, 0f, _layerMask);

        if (isHit)
        {
            brain.movePos = hit.point;
            return State.SUCCESS;
        }
        brain.movePos = targetPos;
        //0.375 sec
        return State.SUCCESS;
    }
}
