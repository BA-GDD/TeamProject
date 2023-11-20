using Lofelt.NiceVibrations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProjectileHealth : PoolableMono, IDamageable
{
    [SerializeField]
    private int _lifeTime;
    [SerializeField]
    private float _maxHitPoint;
    public UnityEvent onHitTrigger;
    public UnityEvent onDieTrigger;
    private float _currentHitPoint;
    public float CurrentHitPoint => _currentHitPoint;

    private void Awake()
    {
        _currentHitPoint = _maxHitPoint;
    }

    public override void Init()
    {
        Invoke("Die", _lifeTime);
    }

    public void Die()
    {
        if (gameObject.activeSelf)
        {
            onDieTrigger?.Invoke();
            PoolManager.Instance.Push(this);
        }
    }

    public void TakeDamage(float damage)
    {
        _currentHitPoint -= damage;
        onHitTrigger?.Invoke();
        if (_currentHitPoint <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(float damage, Vector3 point, Vector3 normal)
    {
        _currentHitPoint -= damage;
        onHitTrigger?.Invoke();
        if (_currentHitPoint <= 0)
        {
            Die();
        }
    }
}
