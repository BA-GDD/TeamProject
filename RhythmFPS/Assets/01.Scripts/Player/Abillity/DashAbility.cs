using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Dash")]
public class DashAbility : Ability
{
    public float dashSpeed = 50f;
    public float dashTime = 0.1f;

    AgentMovement movement;
    public override void Active(GameObject player)
    {
        if (movement == null)
        {
            movement = player.GetComponent<AgentMovement>();
        }
        movement.StartCoroutine(Dash());
    }
    IEnumerator Dash()
    {
        Vector3 dir = movement.InputforVec;
        movement.canMove = false;
        movement.StopImmediately();
        movement.YVelocity = 0;

        float percent = 0;
        float timer= 0;
        while (percent < 1)
        {
            timer += Time.deltaTime;
            percent = timer / dashTime;
            movement.ManualMove(dir * Time.deltaTime * dashSpeed);
            yield return null;
        }
        movement.canMove = true;
    }
}
