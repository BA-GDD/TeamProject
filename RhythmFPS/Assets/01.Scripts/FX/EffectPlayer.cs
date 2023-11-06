using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using MoreMountains;
using MoreMountains.Feedbacks;

public class EffectPlayer : MonoBehaviour
{
    public List<ParticleSystem> fx = new List<ParticleSystem>();
    private MMF_Player player;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            EffectPlay();
        }
    }

    private void Awake()
    {
        player = GetComponent<MMF_Player>();
    }

    public void EffectPlay()
    {
        fx.ForEach(e => e.Play());
        player.PlayFeedbacks();
    }
}
