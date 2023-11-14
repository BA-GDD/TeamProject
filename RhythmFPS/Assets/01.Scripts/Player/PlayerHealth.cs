using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : AgentHealth
{
    public UnityEvent hitEvent;
    public UnityEvent dieEvent;
    private void Awake()
    {
        _curHP = _maxHP;
    }
    public override void TakeDamage(float damage)
    {
        hitEvent?.Invoke();
        base.TakeDamage(damage);
        UIManager.Instanace.HandlePlayerGetDamage(damage);
    }
    public override void Die()
    {
        dieEvent?.Invoke();
        UIManager.Instanace.HandleGameOver?.Invoke();
    }
}
