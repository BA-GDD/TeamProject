using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimRythmer : MonoBehaviour
{
    [SerializeField] private Transform _canvasTrm;
    [SerializeField] private RythmTaker _rythmTaker;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            SpawnRythmTaker();
        }
    }

    public void SpawnRythmTaker()
    {
        Instantiate(_rythmTaker, _canvasTrm);
    }
}
