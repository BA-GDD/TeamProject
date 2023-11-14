using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using TMPro;
using DG.Tweening;
public class TextFunc : PlayableBehaviour
{
    public TMP_Text text;
    public float playDuration = 1.0f;
    public float strength = 1.0f;

    public override void PrepareFrame(Playable playable, FrameData info)
    {
        Active(playDuration, strength);
    }

    private void Active(float duration = 1.0f, float strength = 1.0f)
    {
        if (text == null) return;

        RectTransform rect = text.rectTransform;
        GameObject obj = text.gameObject;

        obj.SetActive(true);
        rect.DOShakeAnchorPos(duration, strength);
    }
}
