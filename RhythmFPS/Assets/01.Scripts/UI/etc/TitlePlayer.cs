using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
using Random = UnityEngine.Random;

public class TitlePlayer : MonoBehaviour
{
    private Camera _mainCam;
    [SerializeField] private Animator _animator;
    private bool _canChange;

    private readonly int _shootHash = Animator.StringToHash("isShoot");
    private int _saveHash;

    private List<Action> _poseChangeActionList = new List<Action>();

    private void Awake()
    {
        _mainCam = Camera.main;
    }

    private void Start()
    {
        _canChange = true;
        _poseChangeActionList.Add(ChangeShootTrack);
    }

    public void ChangeShootTrack()
    {
        if (!_canChange) return;

        transform.rotation = Quaternion.Euler(new Vector3(1.95835888f, 154.693558f, 355.130463f));
        _animator.SetBool(_shootHash, true);
        _saveHash = _shootHash;
        _canChange = false;
    }

    public void ShakeScreen()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(_mainCam.transform.DOShakePosition(0.5f, 1, 8));
        seq.AppendCallback(() =>
        {
            _mainCam.transform.position = new Vector3(0, 1, -10);
        });
    }

    public void ResetPose()
    {
        transform.rotation = Quaternion.Euler(new Vector3(8.69114304f, 238.51358f, 1.56860518f));
        _animator.SetBool(_saveHash, false);

        _canChange = true;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = _mainCam.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out RaycastHit hit))
            {
                _poseChangeActionList[Random.Range(0, _poseChangeActionList.Count)]?.Invoke();
            }
        }
    }
}
