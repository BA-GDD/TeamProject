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

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            FireBullet();
        }
        if(Input.GetKeyDown(KeyCode.Z))
        {
            ReChargingBullet();
        }
    }

    public void FireBullet()
    {
        _bullets[_bulletIdx-1].enabled = false;
        _bulletIdx--;
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
        _maganize.DOLocalRotateQuaternion(_maganize.localRotation * Quaternion.Euler(0, 0, -180), 0.6f);
        StartCoroutine(ReChargingBulletCo());
    }

    private IEnumerator ReChargingBulletCo()
    {
        for(int i = 0; i < _bullets.Length; i++)
        {
            _bulletIdx = Mathf.Clamp(_bulletIdx + 1, 0, 6);
            _bulletCount.text = $"{_bulletIdx} / 6";
            _bullets[5-i].enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
        _maganize.localRotation = Quaternion.identity;
    }
}
