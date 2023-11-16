using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PressAnyBtn : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _pressABtn;
    private bool isGameStart;
    [SerializeField] private float _blinkSpeed;

    private void Start()
    {
        isGameStart = false;
    }

    private void Update()
    {
        if (isGameStart) return;

        _pressABtn.alpha = (Mathf.Sin(Time.time * _blinkSpeed) + 1) * 0.5f;

        if (Input.anyKeyDown || Input.GetMouseButtonDown(0) || 
            Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            isGameStart = true;
            _pressABtn.enabled = false;
            UIManager.Instanace.HandleUIChange(SceneType.lobby);
        }
    }
}
