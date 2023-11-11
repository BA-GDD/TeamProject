using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    [SerializeField] private float _speed = 3.5f;
    private Rigidbody _rb;
    [SerializeField]
    private LayerMask _playerLayerMask;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        //Destroy(gameObject, 10f);
    }

    private void Update()
    {
        transform.LookAt(GameManager.instance.playerTransform);
        _rb.velocity = transform.forward * _speed;

        Collider[] colliders = Physics.OverlapSphere(transform.position, 2f, _playerLayerMask);
        if(colliders.Length > 0)
        {
            colliders[0].GetComponent<IDamageable>().TakeDamage(10);
            Debug.Log($"던진 도끼에 맞았고 남은 체력: {colliders[0].GetComponent<AgentHealth>().CurHP}");
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 2f);
    }
}