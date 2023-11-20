using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class ShakeText : MonoBehaviour
{
    [Header("���ְ� ����ũ�� �ؽ�Ʈ ������ �ȴ�")]
    [SerializeField] private List<TextMeshProUGUI> _texts = new List<TextMeshProUGUI>();

    [Space(10)]
    [Header("�ؽ�Ʈ �ϳ� ������ ���� �� ���� �� ���� ��ٸ� �ð� �־��ָ� �ȴ�.")]
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
