using BTVisual;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashPatternNode : ActionNode
{
    // 타격 지점
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

        //캐스트만 따로

        // 1타
        // 사선 베기
        Physics.SphereCast(brain.weapon.transform.position, 10f, Vector3.forward, out _hit, 0f, _playerLayerMask);

        // 2타
        // 종 베기
        Physics.SphereCast(brain.weapon.transform.position, 10f, Vector3.forward, out _hit, 0f, _playerLayerMask);

        // 3타
        // 사선 베기
        Physics.SphereCast(brain.weapon.transform.position, 10f, Vector3.forward, out _hit, 0f, _playerLayerMask);

        return State.SUCCESS;
    }
}
