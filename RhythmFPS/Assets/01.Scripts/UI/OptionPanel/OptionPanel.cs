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
        _backPanel.enabled = true;
        _panel.localPosition = Vector3.zero;
    }

    public void ClosePanel()
    {
        _backPanel.enabled = false;
        Sequence seq = DOTween.Sequence();
        seq.Append(_panel.DOLocalMoveY(-900, 0.5f).SetEase(Ease.InOutBack));
        seq.AppendCallback(() =>
        {
            Destroy(this.gameObject); //풀매니저 써야하나
        });
    }
}
