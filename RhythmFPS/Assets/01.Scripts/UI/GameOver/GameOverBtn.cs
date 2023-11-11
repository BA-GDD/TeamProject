using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public abstract class GameOverBtn : MonoBehaviour
{
    public GameObject gameOverUI;
    [SerializeField] private Image _upperImage;
    [SerializeField] private Color[] _loopColor;
    [SerializeField] private float _easingTime;

    private Tween _colorTwwen;

    public abstract void ClickLogic();

    public void UpImage()
    {
        Sequence seq = DOTween.Sequence();
        for(int i = 0; i < _loopColor.Length; i++)
        {
            seq.Append(_upperImage.DOColor(_loopColor[i], _easingTime));
        }
        seq.SetLoops(-1);
        _colorTwwen = seq;
    }

    public void DownImage()
    {
        _colorTwwen.Kill();
        _upperImage.DOColor(Color.white, _easingTime);
    }
}
