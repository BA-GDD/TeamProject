using BTVisual;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobEnemySpawnNode : ActionNode
{
    [SerializeField] private GameObject _mobEnemyPrefab;
    [SerializeField] private List<Vector3> _spawnPointList;
    private bool _alreadyInit = false;

    protected override void OnStart()
    {
        RhythmManager.instance.onNotedTimeAction += OnPatternInitHandle;
    }

    void OnPatternInitHandle()
    {
        if (!_alreadyInit)
        {
            (brain as BossBrain).IsCanAttack = false;
            (brain as BossBrain).IsMove = false;
            (brain as BossBrain).BossAnimator.OnAnimationTrigger += OnMobEnemySpawnHandle;
            (brain as BossBrain).BossAnimator.SetAttackTrigger(true);
            _alreadyInit = true;
        }
    }

    protected override void OnStop()
    {
        RhythmManager.instance.onNotedTimeAction -= OnPatternInitHandle;
        (brain as BossBrain).BossAnimator.OnAnimationTrigger -= OnMobEnemySpawnHandle;
        (brain as BossBrain).BossAnimator.SetAttackTrigger(false);
        (brain as BossBrain).BossAnimator.SetAnimationClipEndState(false);
        (brain as BossBrain).IsMove = true;
        (brain as BossBrain).IsCanAttack = true;
        _alreadyInit = false;
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
        Debug.Log("mob spawn");
        for (int i = 0; i < 3; ++i)
        {
            PoolableMono mobEnemy = PoolManager.Instance.Pop((brain as BossBrain).spawnEnemyName);
            mobEnemy.transform.position = brain.transform.position + _spawnPointList[i];
        }
    }
}
