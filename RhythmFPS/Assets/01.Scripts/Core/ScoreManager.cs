using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager _instance;
    public static ScoreManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<ScoreManager>();
            if (_instance == null)
                Debug.LogError($"{typeof(ScoreManager)} Has not exist in current Scene");
            return _instance;
        }
    }
    public int Score { get; private set; }

    public void AddScrore(int damage)
    {
        Score += (Mathf.Max(1000 - Mathf.FloorToInt(GameManager.instance.GetCurTime() / 60),100)) + (ComboManager.Instance.Combo * damage);
    }


}
