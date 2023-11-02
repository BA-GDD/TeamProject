using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpHeartBit : MonoBehaviour
{
    private float _timeDelta;
    [SerializeField] private Image _heartbitLine;

    private void Update()
    {
        if (Time.frameCount % 3 == 0)
        {
            if (_heartbitLine.fillAmount >= 1)
            {
                _heartbitLine.fillAmount = 0;
            }
            _heartbitLine.fillAmount += 0.01f;
        }
    }
}