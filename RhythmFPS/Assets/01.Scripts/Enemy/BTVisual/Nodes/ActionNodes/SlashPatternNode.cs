using BTVisual;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashPatternNode : ActionNode
{
    // Ÿ�� ����
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
            // �ִϸ��̼� �߰��ؾ���
            RaycastHit hit;

            // 1Ÿ
            // �缱 ����
            if (Physics.SphereCast(brain.transform.position, 5f, Vector3.forward, out hit, 0f, _playerLayerMask))
            {
                if (hit.collider.TryGetComponent<AgentHealth>(out AgentHealth health))
                {
                    health.TakeDamage(10);
                }
            }

            // 2Ÿ
            // �� ����
            if (Physics.SphereCast(brain.transform.position, 5f, Vector3.forward, out hit, 0f, _playerLayerMask))
            {
                if (hit.collider.TryGetComponent<AgentHealth>(out AgentHealth health))
                {
                    health.TakeDamage(10);
                }
            }

            // 3Ÿ
            // �缱 ����
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
