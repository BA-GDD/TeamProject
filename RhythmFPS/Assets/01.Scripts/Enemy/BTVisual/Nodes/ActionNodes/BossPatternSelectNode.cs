using BTVisual;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPatternSelectNode : ActionNode
{
    protected override void OnStart()
    {
        float distance = Vector3.Distance(brain.transform.position, GameManager.instance.PlayerTransform.position);
        if((brain as BossBrain).wormCnt <= 0)
        {
            blackboard.curPattern = 3;
            (brain as BossBrain).spawnEnemyName = "Worm";
            (brain as BossBrain).wormCnt = 3;
        }
        else if((brain as BossBrain).spectorCnt <= 0)
        {
            blackboard.curPattern = 3;
            (brain as BossBrain).spawnEnemyName = "Specter";
            (brain as BossBrain).spectorCnt = 3;
        }
        else if (distance >= 18f)
        {
            blackboard.curPattern = 2;
        }
        else if (distance >= 10f)
        {
            blackboard.curPattern = 1;
        }
        else if (distance >= 0f)
        {
            blackboard.curPattern = 0;
        }
        (brain as BossBrain).BossAnimator.SetAttackPattern(blackboard.curPattern);
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        return State.SUCCESS;
    }
}
