using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spit : MonoBehaviour
{
    [SerializeField]
    private LayerMask _groundLayerMask;
    [SerializeField]
    private float _moveSpeed;
    private Rigidbody _rigidbody;
    public Rigidbody RigidbodyGetter => _rigidbody;
    private EnemyBrain _brain;
    public EnemyBrain Brain
    {
        get
        {
            return _brain;
        }

        set
        {
            _brain = value;
        }
    }
    private ProjectileHealth _health;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _health = GetComponent<ProjectileHealth>();

    }

    private void Update()
    {
        _rigidbody.velocity = transform.forward * _moveSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Ãæµ¹");

        if (other.TryGetComponent(out PlayerHealth playerHealth))
        {
            //playerHealth.TakeDamage(_brain.status.attackPower);
            _health.Die();
        }
        else if (other.gameObject.layer == _groundLayerMask)
        {
            _health.Die();
        }
    }
}
