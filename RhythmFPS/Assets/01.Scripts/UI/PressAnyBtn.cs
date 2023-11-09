using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class PressAnyBtn : MonoBehaviour
{
    [SerializeField] private RectTransform _startScene;
    [SerializeField] private Image _volume;
    [SerializeField] private Color[] _colors = new Color[3];
    [SerializeField] private Transform _mainPanel;
    [SerializeField] private List<Image> _bitLineList = new List<Image>();
    private TextMeshProUGUI _pressABtn;
    private bool isGameStart;
    [SerializeField] private float _turm;

    private void Awake()
    {
        _pressABtn = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        isGameStart = false;
        StartCoroutine(FadeText());
    }
    IEnumerator FadeText()
    {
        float fadecount = 1;
        while (!isGameStart)
        {
            _pressABtn.color = new Color(1, 1, 1, fadecount);
            fadecount += 0.02f;
            yield return new WaitForSeconds(0.03f);
            if (fadecount >= 1)
            {
                fadecount = 0;
            }
        }
    }

    private void Update()
    {
        if (Input.anyKeyDown || Input.GetMouseButtonDown(0) || 
            Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            if (isGameStart) return;
            isGameStart = true;
            _pressABtn.enabled = false;
            StartCoroutine(GameStart());
        }
    }

    IEnumerator GameStart()
    {
        for (int j = 0; j < 3; j++)
        {
            Color co = _colors[j];

            _volume.DOColor(co, _turm);
            _pressABtn.DOColor(co, _turm);
            for (int i = 0; i < _bitLineList.Count; i++)
            {
                _bitLineList[i].DOColor(co, _turm);
            }
            yield return new WaitForSeconds(_turm);
        }

        Debug.Log("게임 스타트 로직 구현 해주새요 >.<"); // 할 수 있지?
        Destroy(_startScene.gameObject);
    }
}
