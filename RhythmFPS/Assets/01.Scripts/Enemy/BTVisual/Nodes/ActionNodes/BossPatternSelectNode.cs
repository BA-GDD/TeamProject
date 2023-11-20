using BTVisual;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPatternSelectNode : ActionNode
{
    protected override void OnStart()
    {
        float distance = Vector3.Distance(brain.transform.position, GameManager.instance.PlayerTransform.position);
        if (distance >= 18f)
        {
            blackboard.curPattern = 2;
            (brain as BossBrain).BossAnimator.SetAttackPattern(blackboard.curPattern);
        }
        else if (distance >= 10f)
        {
            blackboard.curPattern = 1;
            (brain as BossBrain).BossAnimator.SetAttackPattern(blackboard.curPattern);
        }
        else if (distance >= 0f)
        {
            blackboard.curPattern = 0;
            (brain as BossBrain).BossAnimator.SetAttackPattern(blackboard.curPattern);
        }
        //blackboard.curPattern = 3;
        //(brain as BossBrain).BossAnimator.SetAttackPattern(blackboard.curPattern);
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        return State.SUCCESS;
    }
}
