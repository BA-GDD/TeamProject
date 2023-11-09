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

    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        if (!brain.agent.isStopped)
        {
            return State.RUNNING;
        }
        else
        {
            for (int i = 0; i < 3; ++i)
            {
                GameObject newMob = Instantiate(_mobEnemyPrefab, brain.transform.position + _spawnPointList[i], Quaternion.identity);
            }
            blackboard.curPattern = Random.Range(0, 2);
            return State.SUCCESS;
        }
    }
}
