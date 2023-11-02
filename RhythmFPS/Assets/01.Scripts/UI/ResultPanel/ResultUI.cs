using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;
using DG.Tweening;
using System;

public class ResultUI : MonoBehaviour
{
    [Header("참조")]
    [SerializeField] private Transform _resultPanel;
    [SerializeField] private TextMeshProUGUI[] _resultSyntex;
    [SerializeField] private TextMeshProUGUI _infoTxt;
    [SerializeField] private Transform _scoreGroupTrm;
    [SerializeField] private RankImg _rank;

    [Header("셋팅")]
    [SerializeField] private Vector2 _startPos;
    [SerializeField] private Vector2 _createPos;
    [SerializeField] private Vector2 _scoreGroupStartPos;
    [SerializeField] private Vector2 _merginValue;
    [SerializeField] private string[] _scoreTxtGroup = new string[4];
    [SerializeField] private Vector2 _rankPos;

    private void Start()
    {
        ActiveResultPanel(10, 10, 200, 10);
    }

    public void ActiveResultPanel(float score, float combo, float time, float damage) // 패널
    {
        string t = $"{Mathf.FloorToInt(time / 60)}m {time % 60}";
        StartCoroutine(GeneradeScorePosition(score, combo, t, damage));
    }

    IEnumerator GeneradeScorePosition(float score, float combo, string clearTime, float damage)
    {
        string [] txts = { score.ToString(), combo.ToString(), clearTime, damage.ToString() };
        Sequence seq = DOTween.Sequence();
        seq.Append(_resultPanel.DOLocalMoveY(0, 0.6f));

        for (int i = 0; i < txts.Length; i++)
        {
            StringBuilder sb = new StringBuilder();
            TextMeshProUGUI txt = Instantiate(_infoTxt, _scoreGroupTrm);
            txt.transform.localPosition = new Vector2(70, (_scoreGroupStartPos + (i * _merginValue)).y);

            sb.Append(_scoreTxtGroup[i]);
            sb.Append(txts[i]);

            txt.text = sb.ToString();

            txt.transform.DOLocalMove(_scoreGroupStartPos + (i * _merginValue), 0.6f).SetEase(Ease.InOutQuart);
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(0.5f);
        Instantiate(_rank, _resultPanel).SetPos(_rankPos, Convert.ToInt32(score));
        
    }
}
