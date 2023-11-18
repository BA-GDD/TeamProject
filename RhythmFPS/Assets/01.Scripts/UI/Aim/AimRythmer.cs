using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class AimRythmer : MonoBehaviour
{
    [SerializeField] private Image _aimMaster;
    [SerializeField] private Color _normalColor;
    [SerializeField] private Color _matchColor;

    public void MatChRhythm()
    {
        _aimMaster.color = _matchColor;
        StartCoroutine(WaitRhythm());
    }

    IEnumerator WaitRhythm()
    {
        for (int i = 0; i < 6; i++)
            yield return new WaitForEndOfFrame();

        Debug.Log("Reset");
        _aimMaster.color = _normalColor;
    }
}
