using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StartCollector : MonoBehaviour
{
    [SerializeField] private RectTransform _star;
    [SerializeField] private Transform[] _starSpace = new Transform[3];

    public void SetStar(int startCount, bool isEasing)
    {
        StartCoroutine(SetStarCo(startCount, isEasing));
    }

    IEnumerator SetStarCo(int starCount, bool isEasing)
    {
        for(int i = 0; i < starCount; i++)
        {
            RectTransform st = Instantiate(_star, _starSpace[i]);
            if(isEasing)
            {
                st.localScale *= new Vector2(1.2f, 1.2f);
                st.DOScale(Vector2.one, 0.4f).SetEase(Ease.InExpo);
                yield return new WaitForSeconds(0.3f);
            }
        }
    }
}
