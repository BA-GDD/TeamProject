using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BTVisual;

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
        Vector3 pos = Random.insideUnitSphere;
        Vector3 finalPos = brain.transform.position + pos;
        finalPos.y = Mathf.Abs(finalPos.y);
        finalPos.y += 10.0f;
        RaycastHit hit;
        bool isHit = Physics.Raycast(brain.transform.position, finalPos - brain.transform.position, out hit, Vector2.Distance(brain.transform.position, pos),
            ground);

        if (isHit)
        {
            brain.movePos = hit.point;
            return State.SUCCESS;
        }
        brain.movePos = finalPos;

        return State.SUCCESS;
    }
}
