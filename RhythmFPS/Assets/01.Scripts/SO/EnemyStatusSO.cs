using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/EnemyStatus")]
public class EnemyStatusSO : ScriptableObject
{
    public float moveSpeed;
    public float attackPower;
    public float attackRange;
    public float attackDelay;
    public float maxHitPoint;
}
