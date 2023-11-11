using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PanelFadeColor : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Color _normalColor;
    [SerializeField] private Color _fadeColor;
    [SerializeField] private float _fadeTime;

    public void FadeInColor()
    {
        _image.DOColor(_fadeColor, _fadeTime);
    }

    public void FadeOutColor()
    {
        _image.DOColor(_normalColor, _fadeTime);
    }

    public void ActiveOptionPanel()
    {
        UIManager.Instanace.HandleActiveOptionPanel?.Invoke();
    }
}
