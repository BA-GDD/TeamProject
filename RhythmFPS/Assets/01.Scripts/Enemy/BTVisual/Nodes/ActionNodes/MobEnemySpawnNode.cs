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
        (brain as BossBrain).BossAnimator.OnAnimationTrigger += OnMobEnemySpawnHandle;
    }

    protected override void OnStop()
    {
        (brain as BossBrain).BossAnimator.OnAnimationTrigger -= OnMobEnemySpawnHandle;
        (brain as BossBrain).BossAnimator.SetAttackTrigger(false);
    }

    protected override State OnUpdate()
    {
        if (!brain.agent.isStopped)
        {
            return State.RUNNING;
        }
        else
        {
            (brain as BossBrain).BossAnimator.SetAttackPattern(3);
            (brain as BossBrain).BossAnimator.SetAttackTrigger(true);
            blackboard.curPattern = Random.Range(0, 2);
            return State.SUCCESS;
        }
    }

    private void OnMobEnemySpawnHandle()
    {
        for (int i = 0; i < 3; ++i)
        {
            GameObject newMob = Instantiate(_mobEnemyPrefab, brain.transform.position + _spawnPointList[i], Quaternion.identity);
        }
    }
}
