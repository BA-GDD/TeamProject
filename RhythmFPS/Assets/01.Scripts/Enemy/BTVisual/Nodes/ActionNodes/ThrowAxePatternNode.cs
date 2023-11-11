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
        //(brain as BossBrain).BossAnimator.SetAttackTrigger(false);
        brain.StartChase(); 
    }

    protected override State OnUpdate()
    {
        (brain as BossBrain).BossAnimator.SetAttackPattern(2);
        (brain as BossBrain).BossAnimator.SetAttackTrigger(true);
        (brain as BossBrain).isCanAttack = false;
        (brain as BossBrain).timer = 0;
        return State.SUCCESS;
    }

    private void OnThrowAxeHandle()
    {
        GameObject thrownAxe = Instantiate(axePrefab, brain.transform.position, Quaternion.identity);
        Debug.Log("Axe Throw Pattern");
    }
}
