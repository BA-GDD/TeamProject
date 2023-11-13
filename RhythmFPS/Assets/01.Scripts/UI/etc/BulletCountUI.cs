using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class BulletCountUI : MonoBehaviour
{
    [SerializeField] private Transform _maganize;
    [SerializeField] private Image[] _bullets;
    [SerializeField] private TextMeshProUGUI _bulletCount;

    private int _bulletIdx = 6;

    public void FireBullet()
    {
        Debug.Log(_bulletIdx);
        if (_bulletIdx - 1 < 0) return;
        
        _bullets[_bulletIdx - 1].enabled = false;
        _bulletIdx = Mathf.Clamp(_bulletIdx - 1, 0, 6);
        _bulletCount.text = $"{_bulletIdx} / 6";

        Quaternion currRot = _maganize.localRotation;
        _maganize.DOLocalRotateQuaternion(currRot * Quaternion.Euler(0, 0, -60), 0.3f);

        if(_bulletIdx == 6)
        {
            _bulletIdx = 0;
        }
    }

    public void ReChargingBullet()
    {
        Debug.Log(_bulletIdx);
        if (_bulletIdx + 1 > 6) return;
        Quaternion currRot = _maganize.localRotation;
        _maganize.DOLocalRotateQuaternion(currRot * Quaternion.Euler(0, 0, 60), 0.3f);

        _bullets[_bulletIdx].enabled = true;
        _bulletIdx = Mathf.Clamp(_bulletIdx + 1, 0, 6);
        _bulletCount.text = $"{_bulletIdx} / 6";
    }

}
