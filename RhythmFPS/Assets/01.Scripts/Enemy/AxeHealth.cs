using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AxeHealth : MonoBehaviour, IDamageable
{
    public UnityEvent onHitTrigger;
    public UnityEvent onDieTrigger;
    private float _currentHitPoint;
    public float CurrentHitPoint => _currentHitPoint;

    public void Die()
    {
        onDieTrigger?.Invoke();
        Destroy(gameObject);
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
