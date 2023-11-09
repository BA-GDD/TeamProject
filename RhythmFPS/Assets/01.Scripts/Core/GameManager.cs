using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    [HideInInspector]
    public Transform playerTransform;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("GameManager instance is already exist!");
        }

        instance = this;

        DontDestroyOnLoad(gameObject);
        //Debug
        GameStart();
    }

    /// <summary>
    /// ���� ���� �۾�
    /// </summary>
    public void GameStart()
    {
        playerTransform = FindAnyObjectByType<AgentController>().transform;

        if (!playerTransform)
        {
            Debug.LogError("Any player is not exist in game!");
        }
    }

    /// <summary>
    /// ���� ���� �۾�
    /// </summary>
    public void GameEnd()
    {

    }

    /// <summary>
    /// ���� ����� �۾�
    /// </summary>
    public void GameRestart()
    {
        //����� �� �ʿ��� �� �ʱ�ȭ
        GameStart();
    }
}
