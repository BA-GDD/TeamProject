using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLobby : MonoBehaviour
{
    private Camera _mainCam;
    [SerializeField] private Animator _animator;

    private int[] _hashArray = { Animator.StringToHash("isHand"), Animator.StringToHash("isPla") };
    private int _saveIdx;

    private bool _isPlaying;

    private void Awake()
    {
        _mainCam = Camera.main;
    }

    private void OnMouseDown()
    {
        if (_isPlaying == true) return;

        Ray ray = _mainCam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if(hit.collider.gameObject == gameObject)
            {
                _saveIdx = Time.frameCount % 2;
                _animator.SetBool(_hashArray[_saveIdx], true);
                _isPlaying = true;
            }
        }
    }

    public void ResetAnimation()
    {
        _animator.SetBool(_hashArray[_saveIdx], false);
        _isPlaying = false;
    }
}
