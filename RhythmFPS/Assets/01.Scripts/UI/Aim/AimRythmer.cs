using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimRythmer : MonoBehaviour
{
    [SerializeField] private Transform _canvasTrm;
    [SerializeField] private RythmTaker _rythmTaker;
    [SerializeField] private float _matChTime;

    private void Awake()
    {
        _canvasTrm = GameObject.Find("UICanvas").transform;
    }

    public void SpawnRhythm()
    {
        StartCoroutine(SpawnRhythmCo(UIManager.Instanace.rhythmTurm));
    }

    IEnumerator SpawnRhythmCo(float spawnTime)
    {
        yield return new WaitForSeconds(spawnTime - _matChTime);
        while(true)
        {


            yield return new WaitForSeconds(spawnTime - _matChTime);
            Instantiate(_rythmTaker, _canvasTrm).matchTime = _matChTime;
            yield return new WaitForSeconds(_matChTime);

        }
        
        //PoolManager.Instance.Pop("RhythmTaker").transform.SetParent(_canvasTrm);
    }
}
