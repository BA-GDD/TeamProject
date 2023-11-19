using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleUI : MonoBehaviour
{
    [SerializeField] private GameObject _titleMapInfoPrefab;
    private GameObject _titleMapInfo;


    private void Start()
    {
        _titleMapInfo = Instantiate(_titleMapInfoPrefab);
    }

    private void OnDestroy()
    {
        Destroy(_titleMapInfo);
    }
}
