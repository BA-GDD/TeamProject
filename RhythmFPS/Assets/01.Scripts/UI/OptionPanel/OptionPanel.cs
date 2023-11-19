using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class OptionPanel : MonoBehaviour
{
    [SerializeField] private Image _backPanel;
    [SerializeField] private RectTransform _panel;

    public void OpenPanel()
    {
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
        Sequence seq = DOTween.Sequence();
        seq.Append(_panel.DOLocalMoveY(-900, 0.5f).SetEase(Ease.InOutBack));
        seq.AppendCallback(() =>
        {
            gameObject.SetActive(false);
        });
    }
}
