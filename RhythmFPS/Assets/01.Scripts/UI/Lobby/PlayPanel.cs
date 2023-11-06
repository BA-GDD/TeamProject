using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayPanel : MonoBehaviour
{
    [SerializeField] private Vector2 _sizeDelta;
    [SerializeField] private Vector2 _position;
    private PanelManaging _panelManaging;
    private RectTransform _myTransform;

    [SerializeField] private float _panelStretchValue;
    [SerializeField] private float _easingDuration;

    private void Awake()
    {
        _panelManaging = transform.parent.GetComponent<PanelManaging>();
        _myTransform = (RectTransform)transform;
    }

    public void PanelMove(float moveValue)
    {
        float ypos = _position.y + moveValue;
        _myTransform.DOLocalMoveY(ypos, 0.2f);
    }

    public void OnPointerInThisPanel()
    {
        DOTween.To(() => _myTransform.sizeDelta.y,
                    y => _myTransform.sizeDelta = new Vector2(_myTransform.sizeDelta.x, y),
                    _myTransform.sizeDelta.y + _panelStretchValue, _easingDuration);

        _panelManaging.EnterPointPnanel(_myTransform);
    }

    public void OnExitPointerInThisPanel()
    {
        DOTween.To(() => _myTransform.sizeDelta.y,
                    y => _myTransform.sizeDelta = new Vector2(_myTransform.sizeDelta.x, y),
                    _sizeDelta.y, _easingDuration);

        _panelManaging.ExitPointPnanel(_myTransform);
    }
}
