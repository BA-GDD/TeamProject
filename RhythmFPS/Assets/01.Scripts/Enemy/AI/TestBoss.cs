using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBoss : EnemyBrain
{
    [SerializeField] private GameObject _mobEnemyPrefab;
    [SerializeField] private List<Vector3> _spawnPointList;

    public override void Attack()
    {
        
    }

    private void SlashAttack()
    {
        // �ִϸ��̼� ���� �߰�

    }

    private void RushAttack()
    {

    }

    private void MobEnemySpawn()
    {
        // �ִϸ��̼� ���� �߰�
        // ����� ������ ��ġ�� ���� ��ȯ�� ����
        for (int i = 0; i < 4; ++i)
        {
            GameObject newMob = Instantiate(_mobEnemyPrefab, transform.position + _spawnPointList[i], Quaternion.identity);
        }
    }

    public override void Move()
    {
        if (Vector3.Distance(transform.position, movePos) <= .1f) isMove = false;
        else isMove = true;
        
        transform.position = Vector3.Lerp(transform.position, movePos, Time.deltaTime * 10.0f);

        if (isRot) transform.rotation = Quaternion.LookRotation(targetTrm.position - transform.position);
    }
}
