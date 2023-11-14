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
        //yield return new WaitForSeconds(spawnTime - _matChTime);
        while(true)
        {

            RythmTaker rt = PoolManager.Instance.Pop("RhythmTaker") as RythmTaker;
            Debug.Log(rt);
            rt.transform.SetParent(_canvasTrm);
            rt.transform.localPosition = Vector3.zero;
            rt.matchTime = _matChTime;
            yield return new WaitForSeconds(spawnTime - _matChTime);
            rt.SetRhythm();
            yield return new WaitForSeconds(_matChTime);

        }
    }
}
