using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitPlayerMovement : MonoBehaviour
{
    private AgentController _agentMovement;
    private void OnEnable()
    {
        if(TryGetComponent<AgentController>(out _agentMovement))
        {
            _agentMovement.enabled = false;
        }
    }
    private void OnDisable()
    {
        if(_agentMovement != null)
        {
            _agentMovement.enabled = true;
        }
    }
}