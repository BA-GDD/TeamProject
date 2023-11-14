using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormAttack : EnemyAttack
{
    [SerializeField]
    private Transform _spitPosition;
    [SerializeField]
    private MobAnimator _animator;

    public override void Attack()
    {
        if (_attackTimer >= _brain.status.attackDelay)
        {
            _animator.SetAttackTrigger(true);

            _attackTimer = 0f;
        }
    }

    public void OnAttackAnimationEnd()
    {
        PoolableMono spit = PoolManager.Instance.Pop("FXP_Spit");
        spit.GetComponent<Spit>().Brain = _brain;
        spit.transform.position = _spitPosition.position;
        spit.transform.rotation = Quaternion.LookRotation(GameManager.instance.PlayerTransform.position - transform.position);
    }
}
