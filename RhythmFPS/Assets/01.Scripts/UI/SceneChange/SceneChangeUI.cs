using System.Collections;
using UnityEngine;
using DG.Tweening;

public class SceneChangeUI : MonoBehaviour
{
    [SerializeField] private RectTransform[] _screenBar;
    [SerializeField] private float _eaingTime;

    [ContextMenu("StartChange")]
    public void StartChange(SceneType changeScene)
    {
        RectTransform rt = (RectTransform)transform;
        rt.SetAsFirstSibling();

        for(int i = 0; i < _screenBar.Length; i++)
        {
            _screenBar[i].localScale = new Vector3(1, 0, 1);
        }

        StartCoroutine(ScreenChangeCo(1));
    }

    public void EndChange()
    {
        StartCoroutine(ScreenChangeCo(0));
    }

    IEnumerator ScreenChangeCo(float value)
    {
        for(int i = 0; i < _screenBar.Length; i++)
        {
            yield return new WaitForSeconds(0.1f);
            _screenBar[i].DOScaleY(value, _eaingTime).SetEase(Ease.InQuart);
        }
        // ¾À Ã¼ÀÎÁö
        EndChange();
    }
}
