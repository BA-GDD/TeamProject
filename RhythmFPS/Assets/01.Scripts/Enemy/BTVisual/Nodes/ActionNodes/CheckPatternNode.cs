using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BTVisual;
public class CheckPatternNode : ActionNode
{
    public int curPatternIndex = 0;
    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        brain.canRotate = true;
        Debug.Log($"Check pattern index! {curPatternIndex} ");
        if (blackboard.curPattern != curPatternIndex)
        {
            return State.FAILURE;
        }

        return State.SUCCESS;
    }
}
