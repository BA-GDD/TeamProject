using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestUI : MonoBehaviour
{
    public Metronome metronome;
    public Image background;
    public Image fillImage;
    public void Update()
    {
        fillImage.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, metronome.percent);
    }
}
