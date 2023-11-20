using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DropEffect : PoolableMono
{
    ParticleSystem _boom;

    public override void Init()
    {
        //할거 없음 걍 실행만 시킬듯
        _boom = GetComponent<ParticleSystem>();
        _boom.Play();
        StartCoroutine(GoToPoolCoroutine());
    }

    public void GoToPool()
    {
        PoolManager.Instance.Push(this);
    }

    IEnumerator GoToPoolCoroutine()
    {
        yield return new WaitForSeconds(3.0f);
        GoToPool();
    }
}
