using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;

    private void Start()
    {
        SpawnRandomized();
    }

    [ContextMenu("SpawnMethod")]
    public void SpawnRandomized()
    {
        foreach (var e in enemies)
        {
            GameObject obj = Instantiate(e);
            Vector3 rand_pos = Random.onUnitSphere * 10.0f;
            rand_pos.y = Mathf.Abs(rand_pos.y);
            if (rand_pos.x - SubManager.Instance.playerTrm.position.x <= 4.0f) rand_pos.x = 4.0f;
            if (rand_pos.z - SubManager.Instance.playerTrm.position.z <= 4.0f) rand_pos.z = 4.0f;
            obj.transform.position = rand_pos;
        }
    }
}
