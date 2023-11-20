using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecterAttack : EnemyAttack
{
    [SerializeField]
    private LayerMask _playerLayerMask;
    [SerializeField]
    private MobAnimator _animator;
    [SerializeField]
    private AudioClip _attackSound;

    public override void Attack()
    {
        if (_attackTimer >= _brain.status.attackDelay)
        {
            ((MobBrain)_brain).PlayAttackSound();

            Collider[] colliders = Physics.OverlapSphere(transform.position, _brain.status.attackRange, _playerLayerMask);

            if (colliders.Length > 0 && colliders[0].TryGetComponent(out PlayerHealth playerHealth))
            {
                playerHealth.TakeDamage(_brain.status.attackPower);
            }

            _animator.SetAttackTrigger(true);

            _attackTimer = 0f;
        }
    }
}
