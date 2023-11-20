using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadingBar : MonoBehaviour
{
    [SerializeField] private Slider _loadingBar;
    [SerializeField] private TextMeshProUGUI _percentText;

    private void Start()
    {
        _loadingBar.value = 0;
        _percentText.text = "0%";
    }

    public void SetPercent(float value)
    {
        _loadingBar.value = value;
        _percentText.text = $"{Mathf.FloorToInt(value * 100)}%";
    }
}
