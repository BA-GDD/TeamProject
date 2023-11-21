using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormAttack : EnemyAttack
{
    [SerializeField]
    private Transform _spitPosition;
    [SerializeField]
    private MobAnimator _animator;
    [SerializeField]
    private float rotateSpeed = 10f;

    protected override void Update()
    {
        base.Update();

        Vector3 direction = GameManager.instance.PlayerTransform.position - transform.parent.position;
        direction.y = 0f;
        transform.parent.rotation = Quaternion.Slerp(transform.parent.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotateSpeed);
    }

    public override void Attack()
    {
        if (_attackTimer >= _brain.status.attackDelay && _isAttack)
        {
            ((MobBrain)_brain).PlayAttackSound();

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
