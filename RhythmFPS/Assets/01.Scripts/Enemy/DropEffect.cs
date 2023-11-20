using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DropEffect : PoolableMono
{
    ParticleSystem _boom;

    public override void Init()
    {
        //�Ұ� ���� �� ���ุ ��ų��
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
