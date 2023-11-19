using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class MapEnterPanel : MonoBehaviour
{
    [SerializeField] private Ease _ease;
    [Header("ÁÂÇ¥")]
    public Vector2 mapPanelHidePos;
    public Vector2 mapPanelLobbyPos;
    public float mapPanelEasingTime;

    [Space(10)]
    public TextMeshProUGUI bestScore;
    public TextMeshProUGUI maxCombo;

    [Space(10)]
    public Button startButton;

    private MapPanelMoveNoise _moveNoise;
    private MapPanelSpectrum _mapPanelSpectrum;

    private bool _isSelect;
    public bool IsSelect
    {
        get
        {
            return _isSelect;
        }
        set
        {
            _isSelect = value;
            _moveNoise.canNoiseBG = _isSelect;
            _mapPanelSpectrum.isRhythming = _isSelect;
            // ½ºÆåÆ®·³ °è»êµµ
        }
    }

    private void Awake()
    {
        _moveNoise = transform.Find("Image").GetComponent<MapPanelMoveNoise>();
        _mapPanelSpectrum = transform.Find("Spectrum").GetComponent<MapPanelSpectrum>();
    }

    public void ActivePanel()
    {
        IsSelect = true;
        transform.DOLocalMove(mapPanelLobbyPos, mapPanelEasingTime).SetEase(_ease);
        transform.SetAsFirstSibling();
    }

    public void HidePanel()
    {
        IsSelect = false;
        transform.DOLocalMove(mapPanelHidePos, mapPanelEasingTime).SetEase(_ease);
    }
    public void SetText(string socre, string combo)
    {
        bestScore.text = $"<color=grey>BestScore</color> {socre}";
        maxCombo.text = $"<color=grey>Max Combo</color> {combo}";
    }
}
