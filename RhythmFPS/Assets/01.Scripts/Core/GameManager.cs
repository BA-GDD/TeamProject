using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Core;
using UnityEngine.InputSystem;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public PoolingListSO poolingList;

    private Transform _playerTransform;
    public Transform PlayerTransform
    {
        get
        {
            if (_playerTransform == null)
                _playerTransform = FindAnyObjectByType<AgentMovement>().transform;
            if (_playerTransform == null)
                Debug.LogError("player Is Not have this Scene");//영어 알빠노
            return _playerTransform;
        }
    }

    public DifficultyType difficult;
    private float _startTime;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("GameManager instance is already exist!");
            Destroy(gameObject);
            return;
        }

        instance = this;

        PoolManager.Instance = new PoolManager(transform);
        foreach (PoolingItem item in poolingList.PoolList)
        {
            PoolManager.Instance.CreatePool(item.Prefab, item.Count);
        }
        DontDestroyOnLoad(gameObject);
        //Debug
        Time.timeScale = 1;

    }

    private void Update()
    {
    }
    /// <summary>
    /// ���� ���� �۾�
    /// </summary>
    public void GameStart(DifficultyType stageDifficult)
    {
        difficult = stageDifficult;

        SceneChange(SceneType.inGame);

        //playerTransform = FindAnyObjectByType<AgentController>().transform;

        //if (!playerTransform)
        //{
        //    Debug.LogError("Any player is not exist in game!");
        //}
    }

    /// <summary>
    /// 이새끼가 씬 바꾸는 녀석임 코루틴 실행 실킬겨
    /// </summary>
    /// <param name="sceneType"></param>
    public void SceneChange(SceneType sceneType)
    {
        StartCoroutine(SceneChangeCor(sceneType));

    }

    private IEnumerator SceneChangeCor(SceneType sceneType)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneType.ToString());
        UIManager.Instanace.SceneChange();
        while (async.progress < 1)
        {
            print(async.progress);
            UIManager.Instanace.UIHud.SetProgress(async.progress);
            yield return null;
        }
        UIManager.Instanace.HandleUIChange(sceneType);
        if(sceneType == SceneType.inGame)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;
        }
    }
    /// <summary>
    /// ���� ���� �۾�
    /// </summary>
    public void GameClear(int star)
    {

        MapInfo map = SaveManager.Instance.LoadMapInfoOrDefault(difficult);
        int score = ScoreManager.Instance.Score;
        int combo = ComboManager.Instance.maxCombo;
        UIManager.Instanace.HandleGameClear(combo, GetCurTime(), 10);
        if (map.Equals(default(MapInfo)))
        {
            SaveManager.Instance.data.mapDatas.Add(new MapInfo
            {
                difficultyType = difficult,
                starCount = star,
                bestScore = score,
                maxCombo = combo
            });
        }
        else
        {
            map.starCount = Mathf.Max(map.starCount, star);
            map.bestScore = Mathf.Max(map.bestScore, score);
            map.maxCombo = Mathf.Max(map.maxCombo, combo);
            SaveManager.Instance.SaveMapInfo(map);
        }
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    //�
    /// </summary>
    public void GameRestart()
    {
        //����� �� �ʿ��� �� �ʱ�ȭ
    }

    public void StartTimer()
    {
        _startTime = Time.time;
    }
    public float GetCurTime()
    {
        return Time.time - _startTime;
    }

    public void GamePause()
    {
        Time.timeScale = 0;
    }

    public void GameResume()
    {
        Time.timeScale = 1;
    }
}
