using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SoundPlayAsset : PlayableAsset
{
    public AudioPlayer player;
    public AudioClip clip;  
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var p = ScriptPlayable<SoundPlay>.Create(graph);
        
        var b = p.GetBehaviour();
        b.player = player;  
        b.clip = clip;  
        return p;
    }
}
