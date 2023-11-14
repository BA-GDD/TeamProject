using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using TMPro;

public class TextFuncAsset : PlayableAsset
{
    public TMP_Text text;
    public float playDuration = 1.0f;
    public float strength = 1.0f;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<TextFunc>.Create(graph);

        var behaviour = playable.GetBehaviour();
        behaviour.text = text;
        behaviour.strength = strength;
        behaviour.playDuration = playDuration;
        return playable;

    }
}
