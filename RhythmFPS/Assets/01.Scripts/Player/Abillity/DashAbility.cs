using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Dash")]
public class DashAbility : Ability
{
    public float dashSpeed = 50f;
    public float dashTime = 0.1f;

    private AgentMovement _movement;
    private MMF_Player _feedbackPlayer;
    private PlayerSound _soundPlayer;
    public override void Active(GameObject player)
    {
        if (_movement == null)
        {
            _movement = player.GetComponent<AgentMovement>();
            _feedbackPlayer = player.transform.Find("Feel").GetComponent<MMF_Player>();
            _soundPlayer = player.transform.Find("PlayerSound").GetComponent<PlayerSound>();
        }
        _movement.StartCoroutine(Dash());
    }
    IEnumerator Dash()
    {
        Vector3 dir = _movement.InputforVec;
        _movement.canMove = false;
        _movement.StopImmediately();
        _movement.YVelocity = 0;

        _soundPlayer.PlayerDashClip();

        float percent = 0;
        float timer= 0;
        _feedbackPlayer.PlayFeedbacks();
        while (percent < 1)
        {
            timer += Time.deltaTime;
            percent = timer / dashTime;
            _movement.ManualMove(dir * Time.deltaTime * dashSpeed);
            yield return null;
        }
        _movement.canMove = true;
    }
}
