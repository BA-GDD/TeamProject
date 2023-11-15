using System.Collections;
using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class SceneChangeUI : MonoBehaviour
{
    private RectTransform _thisTrm;
    [SerializeField] private RectTransform[] _screenBar;
    [SerializeField] private float _eaingTime;

    [SerializeField] private List<GameObject> _sceneUIList = new List<GameObject>();
    private SceneType toChangeScene;

    [ContextMenu("StartChange")]
    public void StartChange(SceneType sType)
    {
        _thisTrm = (RectTransform)transform;
        _thisTrm.SetAsFirstSibling();

        for(int i = 0; i < _screenBar.Length; i++)
        {
            _screenBar[i].localScale = new Vector3(1, 0, 1);
        }

        StartCoroutine(ScreenChangeCo(1));
    }

    public void EndChange()
    {
        if (UIManager.Instanace.currentSceneObject != null)
            Destroy(UIManager.Instanace.currentSceneObject);

        UIManager.Instanace.currentSceneObject = Instantiate(_sceneUIList[(int)toChangeScene], _canvasTrm);
        UIManager.Instanace.currentSceneType = toChangeScene;

        _thisTrm.SetAsFirstSibling();
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
