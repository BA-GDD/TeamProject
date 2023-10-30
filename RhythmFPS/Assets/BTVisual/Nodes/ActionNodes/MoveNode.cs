using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BTVisual;

public class MoveNode : ActionNode
{
    public Vector2 maximumBoundOfMap;
    public Vector2 minimumBoundOfMap;

    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        Debug.Log("Moved");
        Vector3 pos = Random.onUnitSphere * 10.0f;
        pos.y = Mathf.Abs(pos.y);

        if (Mathf.Abs(pos.x - brain.targetTrm.position.x) < 2.0f) pos.x = 2.0f;
        if (Mathf.Abs(pos.z - brain.targetTrm.position.z) < 2.0f) pos.z = 2.0f;

        Mathf.Clamp(pos.x, minimumBoundOfMap.x, maximumBoundOfMap.x);
        Mathf.Clamp(pos.z, minimumBoundOfMap.y, maximumBoundOfMap.y);
       
        brain.movePos = brain.targetTrm.position + pos;

        blackboard.curPattern = Random.Range(0, 3);
        return State.SUCCESS;
    }
}
