using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class RankImg : MonoBehaviour
{
    [SerializeField] private Sprite[] _imgs;
    [SerializeField] private float[] _scoreLimits; // юс╫ц

    public void SetPos(Vector2 pos, float score)
    {
        Image img = GetComponent<Image>();
        transform.localPosition = pos;

        for(int i = 0; i < _scoreLimits.Length; i++)
        {
            if(score >= _scoreLimits[i])
            {
                img.sprite = _imgs[i];
            }
        }

        transform.DOScale(new Vector3(0.7f, 0.7f), 0.6f).SetEase(Ease.InOutQuart);
    }
}
