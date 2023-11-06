using BTVisual;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobEnemySpawnNode : ActionNode
{
    public GameObject mobEnemyPrefab;

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
            GameObject newMob = Instantiate(mobEnemyPrefab);
            newMob.transform.position = new Vector3(i * 2f, 0, i * 2f);
        }
        blackboard.curPattern = Random.Range(0, 2);
        return State.SUCCESS;
    }
}
