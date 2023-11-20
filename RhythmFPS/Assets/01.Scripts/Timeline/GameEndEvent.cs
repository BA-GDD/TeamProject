using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GameEndEvent : MonoBehaviour
{
    [SerializeField]
    private PlayableDirector _director;


    public void DirectorEndEvent(PlayableDirector director)
    {
        //여기 추가하면 된다.
        GameManager.instance.GameClear(3);
        print("게임 끝");
    }
    public void GameEnd()
    {
        _director.stopped += DirectorEndEvent;
        _director.Play();
    }
}
