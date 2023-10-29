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

        if (pos.x >= maximumBoundOfMap.x)
            pos.x = maximumBoundOfMap.x;
        
        else if (pos.x <= minimumBoundOfMap.x)
            pos.x = minimumBoundOfMap.x;
        
        else if (pos.z >= maximumBoundOfMap.y)
            pos.z = maximumBoundOfMap.y;
        
        else if(pos.z <= minimumBoundOfMap.y)
            pos.z = minimumBoundOfMap.y;
        
        pos.y = Mathf.Abs(pos.y);
        brain.movePos = brain.targetTrm.position + pos;


        blackboard.curPattern = Random.Range(0, 3);
        return State.SUCCESS;
    }
}
