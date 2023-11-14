using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    [SerializeField] private float _speed = 3.5f;
    [SerializeField] private float _rotateSpeed = 0.5f;
    [SerializeField]
    private float _axeDamage;
    private Rigidbody _rb;
    private ProjectileHealth _health;
    [SerializeField]
    private LayerMask _groundLayerMask;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _health = GetComponent<ProjectileHealth>();
    }

    private void Update()
    {
        //Vector3 dir = GameManager.instance.playerTransform.position - transform.position;
        //Quaternion rot = Quaternion.Euler(dir);
        //Quaternion newRot = Quaternion.Lerp(transform.rotation, rot, 0.5f);
        //transform.rotation.SetLookRotation(newRot.eulerAngles);
        //transform.LookAt(GameManager.instance.playerTransform);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(GameManager.instance.playerTransform.position - transform.position),  _rotateSpeed * Time.deltaTime);

        _rb.velocity = transform.forward * _speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerHealth playerHealth))
        {
            playerHealth.TakeDamage(_axeDamage);
            _health.Die();
        }
        else if (other.gameObject.layer == _groundLayerMask)
        {
            _health.Die();
        }
    }
}