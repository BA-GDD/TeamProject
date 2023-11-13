using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProjectileHealth : PoolableMono, IDamageable
{
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

    }

    public void Die()
    {
        onDieTrigger?.Invoke();
        PoolManager.Instance.Push(this);
    }

    public void TakeDamage(int damage)
    {
        _currentHitPoint -= damage;
        onHitTrigger?.Invoke();
        if (_currentHitPoint <= 0)
        {
            Die();
        }
    }
}
