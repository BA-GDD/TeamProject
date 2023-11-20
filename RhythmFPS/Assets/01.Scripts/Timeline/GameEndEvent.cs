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

        BroadcastMessage("PushObject");
        GameManager.instance.GameClear(3);
        print("���� ��");
    }
    public void GameEnd()
    {
        GameManager.instance.PlayerTransform.GetComponent<PlayerHealth>().isCanHit = false;
        _director.stopped += DirectorEndEvent;
        _director.Play();
    }
}
