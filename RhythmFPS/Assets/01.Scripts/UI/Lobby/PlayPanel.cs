using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PlayPanel : MonoBehaviour
{
    [Header("������ ����")]
    [SerializeField] private Vector2 _sizeDelta;
    [SerializeField] private Vector2 _position;
    private PanelManaging _panelManaging;
    private RectTransform _myTransform;

    [SerializeField] private float _panelStretchValue;
    [SerializeField] private float _easingDuration;
    public bool isSelect;

    [Header("�� ��� ����")]
    [SerializeField] private Transform _panelManagerTrm;
    [SerializeField] private Vector2 _panelSizeDelta;
    [SerializeField] private RectTransform _panelSelectMark;
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
        _panelSelectMark.sizeDelta = _panelSizeDelta;
        _panelSelectMark.gameObject.SetActive(true);

        if (!_panelManaging.IsExistSelectPanel()) // �迭 ���鼭 isSelect�� �ִ��� ������ ����
        {
            _panelSelectMark.transform.localPosition = _position;
            #region [�г��� ���� �ȵ� ���� �� = _isSelect = false]
            Sequence seq = DOTween.Sequence();
            seq.Append
                (
                    DOTween.To(() => _myTransform.sizeDelta.y,
                        y => _myTransform.sizeDelta = new Vector2(_myTransform.sizeDelta.x, y),
                        _myTransform.sizeDelta.y + _panelStretchValue, _easingDuration)
                );
            seq.Join
                (
                    DOTween.To(() => _panelSelectMark.sizeDelta.y,
                        y => _panelSelectMark.sizeDelta = new Vector2(_panelSelectMark.sizeDelta.x, y),
                        _panelSelectMark.sizeDelta.y + _panelStretchValue, _easingDuration)
                );

            _panelManaging.EnterPointPnanel(this);
            #endregion
        }
        else
        {
            _panelSelectMark.transform.localPosition = transform.localPosition;
            _panelSelectMark.sizeDelta = _myTransform.sizeDelta;
        }
    }

    public void DestroyPanelMark()
    {
        _panelSelectMark.gameObject.SetActive(false);
    }

    public void OnExitPointerInThisPanel()
    {
        if (_panelManaging.IsExistSelectPanel()) return;

        Sequence seq = DOTween.Sequence();
        seq.Append(
        DOTween.To(() => _myTransform.sizeDelta.y,
                    y => _myTransform.sizeDelta = new Vector2(_myTransform.sizeDelta.x, y),
                    _sizeDelta.y, _easingDuration));

        seq.Join(DOTween.To(() => _panelSelectMark.sizeDelta.y,
                        y => _panelSelectMark.sizeDelta = new Vector2(_panelSelectMark.sizeDelta.x, y),
                        _panelSelectMark.sizeDelta.y - _panelStretchValue, _easingDuration));

        _panelManaging.ExitPointPnanel(this);
    }

    public void OnClickPointerInThisPanel()
    {
        PlayPanel pp = _panelManaging.FindSelectPanel();
        if(pp != null)
        {
            pp.isSelect = false;
            if (pp != this)
            {
                pp.OnExitPointerInThisPanel();
                //PanelMove(0);
                OnPointerInThisPanel();
                
                _panelManaging.ExitPointPnanel(pp);
                _panelManaging.EnterPointPnanel(this);
            }
            else
            {
                _panelManaging.selectMapEnterPanel = null;
                _panelManaging.selectPlayPanel = null;
                pp.OnExitPointerInThisPanel();
                _mapEnterPanel.HidePanel();
                return;
            }
        }

        isSelect = !isSelect;
        
        if(_panelManaging.selectPlayPanel != null)
        {
            _panelManaging.selectPlayPanel.OnExitPointerInThisPanel();
        }

        if(_panelManaging.selectMapEnterPanel != null)
        {
            _panelManaging.selectMapEnterPanel.HidePanel();
        }

        if (_panelManaging.selectMapEnterPanel == _mapEnterPanel) return;

        _mapEnterPanel.ActivePanel();
        _panelManaging.selectMapEnterPanel = _mapEnterPanel;
        _panelManaging.selectPlayPanel = this;
    }
}
