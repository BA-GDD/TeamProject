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
        _activePanelMark = Instantiate(_panelSelectMark, _panelManagerTrm);

        if (_panelManaging.isSelect == false)
        {
            _activePanelMark.transform.localPosition = _position;
            #region [패널이 선택 안돼 있을 때 = _isSelect = false]
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
            #endregion
        }
        else
        {
            _activePanelMark.transform.localPosition = transform.localPosition;
            _activePanelMark.sizeDelta = _myTransform.sizeDelta;
        }
    }
    public void OnExitPointerInThisPanel()
    {
        Destroy(_activePanelMark.gameObject);

        if (_panelManaging.isSelect) return;

        DOTween.To(() => _myTransform.sizeDelta.y,
                    y => _myTransform.sizeDelta = new Vector2(_myTransform.sizeDelta.x, y),
                    _sizeDelta.y, _easingDuration);
        _panelManaging.ExitPointPnanel(_myTransform);
    }
    public void OnClickPointerInThisPanel()
    {
        _panelManaging.isSelect = !_panelManaging.isSelect;
        
        if(_panelManaging.selectMapEnterPanel != null)
        {
            _panelManaging.selectMapEnterPanel.HidePanel();
        }

        if (_panelManaging.selectMapEnterPanel == _mapEnterPanel) return;

        _mapEnterPanel.ActivePanel();
        _panelManaging.selectMapEnterPanel = _mapEnterPanel;
    }
}
