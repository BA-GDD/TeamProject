using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


[System.Serializable]
public class LoadingUIElement
{
    public Sprite loadingImg;
    public string loadingInfoTxt;
}

public class LoadingUI : MonoBehaviour
{
    [SerializeField] private LoadingBar _loadingBar;
    [SerializeField] private TextMeshProUGUI _infoText;
    [SerializeField] private Image _loadingImg;

    [SerializeField] private List<LoadingUIElement> _elementList = new List<LoadingUIElement>();

    public void LoadingStart()
    {
        int idx = Random.Range(0, _elementList.Count);
        LoadingUIElement luie = _elementList[idx];
        _infoText.text = luie.loadingInfoTxt;
        _loadingImg.sprite = luie.loadingImg;
    }

    public void SetProgress(float progress)
    {
        _loadingBar.SetPercent(progress);
    }
}
