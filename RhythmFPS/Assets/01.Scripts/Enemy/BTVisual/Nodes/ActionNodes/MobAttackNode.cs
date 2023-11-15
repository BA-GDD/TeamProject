using BTVisual;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobAttackNode : ActionNode
{
    protected override void OnStart()
    {
        brain.StopChase();
        (brain as MobBrain).Animator.SetIsMove(false);
        (brain as MobBrain).Animator.SetIsAttack(true);
    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        brain.transform.rotation = Quaternion.LookRotation(GameManager.instance.PlayerTransform.position - brain.transform.position);

        brain.attack.Attack();

        return State.SUCCESS;
    }
}
