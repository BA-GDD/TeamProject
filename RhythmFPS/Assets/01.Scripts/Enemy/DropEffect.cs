using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropEffect : PoolableMono
{
    ParticleSystem _boom;
    ParticleSystem.Particle _boomParticle;
    public override void Init()
    {
        //할거 없음 걍 실행만 시킬듯
        _boom = GetComponent<ParticleSystem>();
        _boomParticle = GetComponent<ParticleSystem.Particle>();
        _boom.Play();

        StartCoroutine(GoToPoolCoroutine());
    }

    public void GoToPool()
    {
        PoolManager.Instance.Push(this);
    }

    IEnumerator GoToPoolCoroutine()
    {
        yield return new WaitForSeconds(_boomParticle.startLifetime);
        GoToPool();
    }
}
