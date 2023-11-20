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
        //���� �߰��ϸ� �ȴ�.
        GameManager.instance.GameClear(3);
        print("���� ��");
    }
    public void GameEnd()
    {
        _director.stopped += DirectorEndEvent;
        _director.Play();
    }
}
