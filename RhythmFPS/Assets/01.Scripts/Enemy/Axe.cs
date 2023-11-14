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
        //Destroy(gameObject, 10f);
    }

    private void Update()
    {
        //Vector3 dir = GameManager.instance.playerTransform.position - transform.position;
        //Quaternion rot = Quaternion.Euler(dir);
        //Quaternion newRot = Quaternion.Lerp(transform.rotation, rot, 0.5f);
        //transform.rotation.SetLookRotation(newRot.eulerAngles);
        //transform.LookAt(GameManager.instance.playerTransform);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(GameManager.instance.PlayerTransform.position - transform.position),  _rotateSpeed * Time.deltaTime);

        _rb.velocity = transform.forward * _speed;

        Collider[] colliders = Physics.OverlapSphere(transform.position, 1f, _playerLayerMask);
        if(colliders.Length > 0)
        {
            colliders[0].GetComponent<IDamageable>().TakeDamage(10);
            Debug.Log($"던진 도끼에 맞았고 남은 체력: {colliders[0].GetComponent<AgentHealth>().CurHP}");
            Destroy(gameObject);
        }
    }
}