using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class RankImg : MonoBehaviour
{
    [SerializeField] private Sprite[] _imgs;
    [SerializeField] private float[] _scoreLimits; // юс╫ц
    private ResultSyntex _resultSyntex;

    public void SetPos(Vector2 pos, float score)
    {
        int num = 0;
        Image img = GetComponent<Image>();
        _resultSyntex = GameObject.Find("Result/TextGroup/ResultSyntex").GetComponent<ResultSyntex>();

        transform.localPosition = pos;

        for(int i = 0; i < _scoreLimits.Length; i++)
        {
            if(score >= _scoreLimits[i])
            {
                img.sprite = _imgs[i];
                num = i;
            }
        }
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOScale(new Vector3(0.7f, 0.7f), 0.6f).SetEase(Ease.InOutQuart)).SetUpdate(true); ;
        seq.AppendCallback(() =>
        {
            _resultSyntex.SetSyntex(num);
        });
    }
}
