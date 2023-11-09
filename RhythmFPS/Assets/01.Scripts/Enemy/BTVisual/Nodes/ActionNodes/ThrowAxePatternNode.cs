using BTVisual;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowAxePatternNode : ActionNode
{
    public GameObject axePrefab;

    protected override void OnStart()
    {
        brain.StopChase();
        (brain as BossBrain).BossAnimator.OnAnimationTrigger += OnThrowAxeHandle;
    }

    protected override void OnStop()
    {
        (brain as BossBrain).BossAnimator.OnAnimationTrigger -= OnThrowAxeHandle;
        (brain as BossBrain).BossAnimator.SetAttackTrigger(false);
    }

    protected override State OnUpdate()
    {
        (brain as BossBrain).BossAnimator.SetAttackPattern(2);
        (brain as BossBrain).BossAnimator.SetAttackTrigger(true);
        blackboard.curPattern = Random.Range(0, 2);
        return State.SUCCESS;
    }

    private void OnThrowAxeHandle()
    {
        GameObject thrownAxe = Instantiate(axePrefab, brain.transform.position, Quaternion.identity);
    }
}
