using BTVisual;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPatternSelectNode : ActionNode
{
    protected override void OnStart()
    {
        blackboard.curPattern = Random.Range(0, 3);
        (brain as BossBrain).BossAnimator.SetAttackPattern(blackboard.curPattern);
        Debug.Log($"Selected pattern is pattern{blackboard.curPattern}");
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        return State.SUCCESS;
    }
}
