using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TextShaker : MonoBehaviour
{
    public float duration = 1.0f;
    public float strength = 1.0f;
    private void OnEnable()
    {
        transform.GetComponent<RectTransform>().DOShakeAnchorPos(duration, strength);
    }
    private void OnDisable()
    {
        
    }
}
