using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class PressAnyBtn : MonoBehaviour
{
    [SerializeField] private Image _volume;
    [SerializeField] private List<Image> _bitLineList = new List<Image>();
    private TextMeshProUGUI _pressABtn;
    private bool isGameStart;

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
        while (true)
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

            GameStart();
        }
    }

    private void GameStart()
    {
        Sequence seq = DOTween.Sequence();

        seq.Append()
    }
}
