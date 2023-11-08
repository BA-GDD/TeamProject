using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    private Animator _animator;
    public Animator Animator => _animator;

    public void StopAnimation(bool value)
    {
        _animator.speed = value ? 0 : 1; //true�� �� 0���� ���� ����, 
        //�ƴҶ� 1�� ���� �ٽ� ���
    }
}
