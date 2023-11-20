using BTVisual;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobEnemySpawnNode : ActionNode
{
    [SerializeField] private GameObject _mobEnemyPrefab;
    [SerializeField] private List<Vector3> _spawnPointList;

    protected override void OnStart()
    {
        (brain as BossBrain).isMove = false;
        (brain as BossBrain).BossAnimator.OnAnimationTrigger += OnMobEnemySpawnHandle;
        (brain as BossBrain).BossAnimator.SetAttackTrigger(true);
        (brain as BossBrain).isCanAttack = false;
    }

    protected override void OnStop()
    {
        (brain as BossBrain).BossAnimator.OnAnimationTrigger -= OnMobEnemySpawnHandle;
        (brain as BossBrain).BossAnimator.SetAttackTrigger(false);
        (brain as BossBrain).BossAnimator.SetAnimationClipEndState(false);
        (brain as BossBrain).isMove = true;
    }

    protected override State OnUpdate()
    {
        if ((brain as BossBrain).BossAnimator.isAnimationClipEnd)
        {
            return State.SUCCESS;
        }
        else
        {
            (brain as BossBrain).timer = 0;
            return State.RUNNING;
        }
    }

    private void OnMobEnemySpawnHandle()
    {
        for (int i = 0; i < 3; ++i)
        {
            PoolableMono mobEnemy = PoolManager.Instance.Pop("Worm");
            mobEnemy.transform.position = brain.transform.position + _spawnPointList[i];
        }
    }
}
