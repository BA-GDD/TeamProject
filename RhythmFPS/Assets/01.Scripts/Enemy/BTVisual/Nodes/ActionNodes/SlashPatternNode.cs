using BTVisual;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashPatternNode : ActionNode
{
    // Ÿ�� ����
    private Transform _hitPoint;
    private RaycastHit _hit;
    private LayerMask _playerLayerMask;

    protected override void OnStart()
    {

    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        //if(brain.)

        //ĳ��Ʈ�� ����

        // 1Ÿ
        // �缱 ����
        Physics.SphereCast(brain.weapon.transform.position, 10f, Vector3.forward, out _hit, 0f, _playerLayerMask);

        // 2Ÿ
        // �� ����
        Physics.SphereCast(brain.weapon.transform.position, 10f, Vector3.forward, out _hit, 0f, _playerLayerMask);

        // 3Ÿ
        // �缱 ����
        Physics.SphereCast(brain.weapon.transform.position, 10f, Vector3.forward, out _hit, 0f, _playerLayerMask);

        return State.SUCCESS;
    }
}
