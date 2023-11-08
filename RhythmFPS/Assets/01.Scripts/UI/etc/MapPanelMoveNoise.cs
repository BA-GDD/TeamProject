using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPanelMoveNoise : MonoBehaviour
{
    public bool canNoiseBG;
    [SerializeField] private float _noiseSpeed;
    [SerializeField] private float _noiseLerpSpeed;
    [SerializeField] private Vector2 _moveLimitValue;
    [SerializeField] private Vector2 _moveLerpStartValue;
    private RectTransform _bgImageTrm;

    private Vector3 dir;
    private Vector3 _moveToPosition;
    private Vector3 _currentPosition;
    private Vector3 _normalizePosition;

    private void Awake()
    {
        _bgImageTrm = (RectTransform)transform;
        canNoiseBG = false;
    }

    private void Update()
    {
        if (!canNoiseBG) return;

        dir = Input.mousePosition - _bgImageTrm.position;
        _currentPosition = _bgImageTrm.localPosition;
        _moveToPosition = _currentPosition += dir * _noiseSpeed * Time.deltaTime;
        _normalizePosition =
        new Vector3(Mathf.Clamp(_moveToPosition.x, -_moveLimitValue.x, _moveLimitValue.x),
                    Mathf.Clamp(_moveToPosition.y, -_moveLimitValue.y, _moveLimitValue.y));

        _bgImageTrm.localPosition =
            Vector3.Lerp(_bgImageTrm.localPosition, _normalizePosition, Time.deltaTime * _noiseLerpSpeed);
    }
}
