using BTVisual;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashPatternNode : ActionNode
{
    // 타격 지점
    private Transform _hitPoint;

    private LayerMask _playerLayerMask;
    private int slashCnt = 0;

    protected override void OnStart()
    {

    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        if (!brain.agent.isStopped)
        {
            return State.RUNNING;
        }
        else
        {
            // 애니메이션 추가해야함
            RaycastHit hit;

            // 1타
            // 사선 베기
            if (Physics.SphereCast(brain.transform.position, 5f, Vector3.forward, out hit, 0f, _playerLayerMask))
            {
                if (hit.collider.TryGetComponent<AgentHealth>(out AgentHealth health))
                {
                    health.TakeDamage(10);
                }
            }

            // 2타
            // 종 베기
            if (Physics.SphereCast(brain.transform.position, 5f, Vector3.forward, out hit, 0f, _playerLayerMask))
            {
                if (hit.collider.TryGetComponent<AgentHealth>(out AgentHealth health))
                {
                    health.TakeDamage(10);
                }
            }

            // 3타
            // 사선 베기
            if (Physics.SphereCast(brain.transform.position, 5f, Vector3.forward, out hit, 0f, _playerLayerMask))
            {
                if (hit.collider.TryGetComponent<AgentHealth>(out AgentHealth health))
                {
                    health.TakeDamage(10);
                }
            }

            return State.SUCCESS;
        }
    }
}
