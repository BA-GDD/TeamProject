using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimRythmer : MonoBehaviour
{
    [SerializeField] private Transform _canvasTrm;
    [SerializeField] private RythmTaker _rythmTaker;

    private void Awake()
    {
        _canvasTrm = GameObject.Find("UICanvas").transform;
    }

    public void SpawnRhythm(float turm)
    {
        StartCoroutine(SpawnRythmCo(turm));
    }

    public IEnumerator SpawnRythmCo(float turm)
    {
        while (true)
        {
            Instantiate(_rythmTaker, _canvasTrm);
            yield return new WaitForSeconds(turm);
        }
    }

}
