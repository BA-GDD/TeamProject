using UnityEngine;

public abstract class PoolableMono : MonoBehaviour
{
    public abstract void Init();
    public virtual void PushObject()
    {
        PoolManager.Instance.Push(this);
    }
}