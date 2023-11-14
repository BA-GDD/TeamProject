using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    [SerializeField] private float _speed = 3.5f;
    [SerializeField] private float _rotateSpeed = 0.5f;
    private Rigidbody _rb;
    [SerializeField]
    private LayerMask _playerLayerMask;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(GameManager.instance.playerTransform.position - transform.position),  _rotateSpeed * Time.deltaTime);

        _rb.velocity = transform.forward * _speed;

        Collider[] colliders = Physics.OverlapSphere(transform.position, 1f, _playerLayerMask);
        if(colliders.Length > 0)
        {
            colliders[0].GetComponent<IDamageable>().TakeDamage(10);
            Destroy(gameObject);
        }
    }
}