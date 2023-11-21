using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class OptionPanel : MonoBehaviour
{
    [SerializeField] private Image _backPanel;
    [SerializeField] private RectTransform _panel;
    private Sequence _seq;

    public void OpenPanel()
    {
        _seq.Kill();
        
        transform.localPosition = Vector3.zero;
        _panel.localPosition = Vector3.zero;
        gameObject.SetActive(true);
        _backPanel.enabled = true;
    }

    public void Close()
    {
        UIManager.Instanace.HandleActiveOptionPanel?.Invoke();
    }

    public void ClosePanel()
    {
        _backPanel.enabled = false;
        _seq = DOTween.Sequence();
        _seq.Append(_panel.DOLocalMoveY(-900, 0.5f).SetEase(Ease.InOutBack));
        _seq.AppendCallback(() =>
        {
            gameObject.SetActive(false);
        });
    }
}
