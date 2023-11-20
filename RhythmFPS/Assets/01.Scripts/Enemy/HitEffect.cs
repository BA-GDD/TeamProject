using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : PoolableMono
{
    public override void Init()
    {
        StartCoroutine(GotoPoolCor());
    }
    public IEnumerator GotoPoolCor()
    {
        yield return new WaitForSeconds(3f);
        PoolManager.Instance.Push(this);
    }
}
