using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour, ISpacialObject
{
    [SerializeField] private float _power;
    [SerializeField] private LayerMask _playerLayer;
    RaycastHit hitInfo;
    private void Update()
    {
        Active();
    }
    public void Active()
    {
        if (CheckRequirements())
        {
            if (hitInfo.collider.TryGetComponent<AgentMovement>(out AgentMovement movement))
            {
                movement.YVelocity = _power;
            }
        }
    }

    public bool CheckRequirements()
    {
        return Physics.BoxCast(transform.position, transform.localScale * 0.5f, Vector3.up, out hitInfo, transform.rotation, 0.3f, _playerLayer);
    }
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
#endif 
}

