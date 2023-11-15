using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PressAnyBtn : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _pressABtn;
    private bool isGameStart;
    [SerializeField] private float _turm;

    private Color _textColor;

    private void Start()
    {
        isGameStart = false;
        _textColor = _pressABtn.color;
        StartCoroutine(FadeText());
    }
    IEnumerator FadeText()
    {
        float fadecount = 1;
        while (!isGameStart)
        {
            _textColor.a = fadecount;
            _pressABtn.color = _textColor;
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
        if (isGameStart) return;

        if (Input.anyKeyDown || Input.GetMouseButtonDown(0) || 
            Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            isGameStart = true;
            _pressABtn.enabled = false;
            UIManager.Instanace.HandleUIChange(SceneType.lobby);
        }
    }
}
