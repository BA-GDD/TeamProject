using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeBullet : PoolableMono
{
    [SerializeField] private float _explosionRadius = 3f;
    [SerializeField] private LayerMask _canDamageLayer;
    [SerializeField] private PoolableMono _exprosionVfx;

    private Rigidbody _rigid;
    private Collider[] _overlapBuffer = new Collider[100];

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
    }
    public void Fire(Vector3 dir)
    {
        _rigid.AddForce((dir * 100), ForceMode.Impulse);
    }
    public override void Init()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        Explosion();
        Vector3 point = other.ClosestPoint(transform.position);
        Transform vfxEFfect =  PoolManager.Instance.Pop(_exprosionVfx.name).transform;

        vfxEFfect.position = point;
        vfxEFfect.rotation = Quaternion.LookRotation(transform.position - point);

        _rigid.velocity = Vector3.zero;
        PoolManager.Instance.Push(this);
    }

    private void Explosion()
    {
        int count = Physics.OverlapSphereNonAlloc(transform.position, _explosionRadius, _overlapBuffer, _canDamageLayer);
        if (count > 0)
        {
            for (int i = 0; i < count; i++)
            {
                if (_overlapBuffer[i].TryGetComponent<IDamageable>(out IDamageable damageable))
                {
                    if (damageable as PlayerHealth)
                    {
                        AgentMovement movement = ((PlayerHealth)damageable).transform.GetComponent<AgentMovement>();
                        movement.isAddDir = true;
                        movement.AddForce((movement.transform.position+Vector3.up*0.5f - transform.position).normalized * (Vector3.Distance(movement.transform.position, transform.position) + 5));
                        Debug.Log(damageable);
                        continue;
                    }
                    damageable.TakeDamage(5);
                }
            }
        }
    }
}
