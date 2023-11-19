using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class LoadingUIElement
{
    public Sprite loadingImg;
    public string loadingInfoTxt;
}

public class LoadingUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _infoText;
    [SerializeField] private TextMeshProUGUI _prtcentage;

    [SerializeField] private Image _loadingImg;

    private List<LoadingUIElement> _elementList = new List<LoadingUIElement>();

    private void Start()
    {
        int idx = Random.Range(0, _elementList.Count);
        LoadingUIElement luie = _elementList[idx];
        _infoText.text = luie.loadingInfoTxt;
        _loadingImg.sprite = luie.loadingImg;
    }
}
