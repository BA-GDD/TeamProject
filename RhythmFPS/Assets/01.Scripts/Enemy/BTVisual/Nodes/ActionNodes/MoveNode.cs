using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BTVisual;
using Core;
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
        /*Debug.Log("Moved");
        Vector3 pos = Random.onUnitSphere * 10.0f;
        Vector3 finalPos = brain.targetTransform.position + pos;
        finalPos.y = Mathf.Abs(finalPos.y);

        RaycastHit hit;
        bool isHit = Physics.Raycast(brain.transform.position, finalPos - brain.transform.position, out hit, Vector2.Distance(brain.transform.position,pos), 
            LayerMask.NameToLayer(Define.GROUND));

        if (isHit)
        {
            return State.SUCCESS;
        }

        Mathf.Clamp(finalPos.x, minimumBoundOfMap.x, maximumBoundOfMap.x);
        Mathf.Clamp(finalPos.z, minimumBoundOfMap.y, maximumBoundOfMap.y);
       
        brain.moveDestination = finalPos;

        blackboard.curPattern = Random.Range(0, 3);*/
        return State.SUCCESS;
    }
}
