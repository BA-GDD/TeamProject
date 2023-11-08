using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    private readonly int _isDeadHash = Animator.StringToHash("is_dead");
    private readonly int _isAttackHash = Animator.StringToHash("attack");
    private readonly int _isMoveHash = Animator.StringToHash("is_move");

    private Animator _animator;
    public Animator Animator => _animator;

    public void StopAnimation(bool value)
    {
        _animator.speed = value ? 0 : 1; //true�� �� 0���� ���� ����, 
        //�ƴҶ� 1�� ���� �ٽ� ���
    }

    public void SetDead()
    {
        _animator.SetBool(_isDeadHash, true);
    }

}
