using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobBrain : EnemyBrain
{
    [SerializeField]
    private MobAnimator _animator;
    [SerializeField]
    private AudioClip _attackSound;

    public MobAnimator Animator => _animator;
    private EnemySound _soundPlayer;

    public override void Init()
    {
        isDead = false;
        _soundPlayer = transform.Find("EnemySound").GetComponent<EnemySound>();
    }

    public override void SetDead()
    {
        base.SetDead();
        _animator.SetDieTrigger(true);
    }
    public void PlayAttackSound()
    {
        _soundPlayer.PlayAttackSound(_attackSound);
    }
}
