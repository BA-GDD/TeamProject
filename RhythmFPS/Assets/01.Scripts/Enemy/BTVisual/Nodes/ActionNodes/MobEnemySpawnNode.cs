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
        brain.BossAnimator.OnAnimationTrigger += OnMobEnemySpawnHandle;
    }

    protected override void OnStop()
    {
        brain.BossAnimator.OnAnimationTrigger -= OnMobEnemySpawnHandle;
        brain.BossAnimator.SetAttackTrigger(false);
    }

    protected override State OnUpdate()
    {
        if (brain.isMove)
        {
            return State.RUNNING;
        }
        else
        {
            brain.BossAnimator.SetAttackPattern(3);
            brain.BossAnimator.SetAttackTrigger(true);
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
