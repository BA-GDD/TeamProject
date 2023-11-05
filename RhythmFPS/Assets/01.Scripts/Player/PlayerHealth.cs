using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : AgentHealth
{
    public UnityEvent hitEvent;
    public UnityEvent dieEvent;

    public override void TakeDamage(int damage)
    {
        hitEvent?.Invoke();
        base.TakeDamage(damage);
    }
    public override void Die()
    {
        dieEvent?.Invoke();
    }
}
