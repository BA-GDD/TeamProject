using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("GameManager instance is already exist!");
        }

        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// 게임 시작 작업
    /// </summary>
    public void GameStart()
    {

    }

    /// <summary>
    /// 게임 종료 작업
    /// </summary>
    public void GameEnd()
    {

    }

    /// <summary>
    /// 게임 재시작 작업
    /// </summary>
    public void GameRestart()
    {
        //재시작 시 필요한 것 초기화
        GameStart();
    }
}
