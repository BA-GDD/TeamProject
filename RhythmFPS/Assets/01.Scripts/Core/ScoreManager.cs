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

    private void Start()
    {
        RhythmManager.instance.onNotedTimeEvent.AddListener(FailScoreToBeat);
    }

    public void AddScrore(int damage)
    {
        Score += (1000 + (ComboManager.Instance.Combo * damage));
    }
    public void FailScoreToBeat()
    {
        Score = Mathf.Max(0, Score - 25);
    }


}
