using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class ShakeText : MonoBehaviour
{
    [Header("켜주고 쉐이크할 텍스트 넣으면 된다")]
    [SerializeField] private List<TextMeshProUGUI> _texts = new List<TextMeshProUGUI>();

    [Space(10)]
    [Header("텍스트 하나 나오고 다음 꺼 켜질 때 까지 기다릴 시간 넣어주면 된다.")]
    [SerializeField] private float _delayTime = 1.0f;

    #region Coroutine Caching
    WaitForSeconds _waitSeconds;
    #endregion

    private void Awake()
    {
        if(_texts != null && _texts.Count > 0)
        {
            _texts.ForEach(obj => obj.gameObject.SetActive(false));
        }
    }

    private void Start()
    {
        _waitSeconds = new WaitForSeconds(_delayTime);
    }

    [ContextMenu("Debugger")]
    public void Debugger()
    {
        StartCoroutine(SequenceShaker());
    }

    private IEnumerator SequenceShaker()
    {
        for (int i = 0; i < _texts.Count; i++)
        {
            Active(_texts[i], true, .5f, 10.0f);
            yield return _waitSeconds;
        }
    }

    private void Active(TextMeshProUGUI text,bool isShake, float duration = 1.0f, float strength = 1.0f)
    {
        if (text == null) return;
        
        RectTransform rect = text.rectTransform;
        GameObject obj = text.gameObject;
        
        obj.SetActive(true);
        if (isShake)
            rect.DOShakeAnchorPos(duration, strength);
    }
}
