using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MapEnterPanel : MonoBehaviour
{
    [SerializeField] private Ease _ease;
    [Header("ÁÂÇ¥")]
    public Vector2 mapPanelHidePos;
    public Vector2 mapPanelLobbyPos;
    public float mapPanelEasingTime;

    public void ActivePanel()
    {
        transform.DOLocalMove(mapPanelLobbyPos, mapPanelEasingTime).SetEase(_ease);
        transform.SetAsFirstSibling();
    }

    public void HidePanel()
    {
        transform.DOLocalMove(mapPanelHidePos, mapPanelEasingTime).SetEase(_ease);
    }
}
