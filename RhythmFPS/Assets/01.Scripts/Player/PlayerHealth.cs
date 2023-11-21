using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : AgentHealth
{
    public UnityEvent hitEvent;
    public UnityEvent dieEvent;

    public bool isCanHit;
    private void Awake()
    {
        _curHP = _maxHP;
        isCanHit = true;
    }
    public override void TakeDamage(float damage)
    {
        if (isCanHit == false) return;
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
