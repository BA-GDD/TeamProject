using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayPanel : MonoBehaviour
{
    [Header("사이즈 셋팅")]
    [SerializeField] private Vector2 _sizeDelta;
    [SerializeField] private Vector2 _position;
    private PanelManaging _panelManaging;
    private RectTransform _myTransform;

    [SerializeField] private float _panelStretchValue;
    [SerializeField] private float _easingDuration;

    [Header("맵 페널 셋팅")]
    [SerializeField] private Transform _panelManagerTrm;
    [SerializeField] private RectTransform _panelSelectMark;
    private RectTransform _activePanelMark;
    [SerializeField] private MapEnterPanel _mapEnterPanel;

    private bool _isSelect;

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
        if (_isSelect) return;

        _activePanelMark = Instantiate(_panelSelectMark, _panelManagerTrm);
        _activePanelMark.transform.localPosition = _position;

        Sequence seq = DOTween.Sequence();
        seq.Append
            (
                DOTween.To(() => _myTransform.sizeDelta.y,
                    y => _myTransform.sizeDelta = new Vector2(_myTransform.sizeDelta.x, y),
                    _myTransform.sizeDelta.y + _panelStretchValue, _easingDuration)
            );
        seq.Join
            (
                DOTween.To(() => _activePanelMark.sizeDelta.y,
                    y => _activePanelMark.sizeDelta = new Vector2(_activePanelMark.sizeDelta.x, y),
                    _activePanelMark.sizeDelta.y + _panelStretchValue, _easingDuration)
            );

        _panelManaging.EnterPointPnanel(_myTransform);
    }
    public void OnExitPointerInThisPanel()
    {
        if (_isSelect) return;

        Destroy(_activePanelMark.gameObject);

        DOTween.To(() => _myTransform.sizeDelta.y,
                    y => _myTransform.sizeDelta = new Vector2(_myTransform.sizeDelta.x, y),
                    _sizeDelta.y, _easingDuration);

        _panelManaging.ExitPointPnanel(_myTransform);
    }
    public void OnClickPointerInThisPanel()
    {
        if(_panelManaging.selectMapEnterPanel != null)
        {
            _panelManaging.selectMapEnterPanel.HidePanel();
        }

        if (_panelManaging.selectMapEnterPanel == _mapEnterPanel) return;

        _mapEnterPanel.ActivePanel();
        _panelManaging.selectMapEnterPanel = _mapEnterPanel;
    }
}
