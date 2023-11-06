using BTVisual;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobEnemySpawnNode : ActionNode
{
    public GameObject mobEnemyPrefab;
    [SerializeField]
    private List<Vector3> _spawnPointList;

    protected override void OnStart()
    {

    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        for(int i = 0; i < 4; ++i)
        {
            GameObject newMob = Instantiate(mobEnemyPrefab, brain.transform.position + _spawnPointList[i], Quaternion.identity);
        }
        blackboard.curPattern = Random.Range(0, 2);
        return State.SUCCESS;
    }
}
