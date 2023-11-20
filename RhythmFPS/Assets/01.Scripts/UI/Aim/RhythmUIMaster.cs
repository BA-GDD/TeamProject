using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmUIMaster : MonoBehaviour
{
    [SerializeField] private Transform _takerParent;
    [SerializeField] private RhythmTaker[] _takerPrefab;

    [SerializeField] private Transform[] _spawnPos;

    [SerializeField] private float _spawnTime;

    private int _orderCount = 0;

    [SerializeField] private AimRythmer _aimRythmer;

    private void Start()
    {
        RhythmManager.instance.onNotedTimeAction += SpawnRhythmUI;
    }

    public void SpawnRhythmUI()
    {
        if (_orderCount == 2) _orderCount = 0;
        for (int i = 0; i < 2; i++)
        {
            RhythmTaker rt = PoolManager.Instance.Pop(_takerPrefab[_orderCount].name) as RhythmTaker;
            rt.transform.SetParent(_takerParent);
            rt.transform.localPosition = _spawnPos[i].localPosition;
            rt.MoveStart(new Vector3(Mathf.Pow(-1, i) * 97, 0, 0), _aimRythmer);
        }

        _orderCount++;
    }
    
}
