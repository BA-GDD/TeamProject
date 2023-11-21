using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class WarnningPanel : MonoBehaviour
{
    private Image _blackPanel;
    [SerializeField] Transform _warnPanel;
    [SerializeField] private TextMeshProUGUI _syntexText;
    [SerializeField] private float _bottomYValue;

    private void Awake()
    {
        _blackPanel = GetComponent<Image>();
        _blackPanel.color = new Color(0, 0, 0, 0);
    }

    private void Start()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(_blackPanel.DOFade(0.24f, 0.2f));
        seq.Join(_warnPanel.DOLocalMoveY(0, 0.5f));
    }

    public void SetText(string syntex)
    {
        _syntexText.text = syntex;
    }

    public void Accept()
    {
        Destroy(gameObject);
    }
}
