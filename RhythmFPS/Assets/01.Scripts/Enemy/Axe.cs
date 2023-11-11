using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    private Transform _targetTrm;
    [SerializeField] private float _speed = 3.5f;
    private Rigidbody _rb;
    

    private void Awake()
    {
        _targetTrm = GameManager.instance.playerTransform;
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        
    }
}