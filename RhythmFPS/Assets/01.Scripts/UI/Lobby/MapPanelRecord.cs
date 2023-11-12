using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPanelRecord : MonoBehaviour
{
    [SerializeField] private RectTransform _recordTrm;
    [SerializeField] private float _turnSpeed;
    private Vector3 _currentRot;

    [field:SerializeField] public bool IsTurn { get; set; }

    private void Update()
    {
        if(IsTurn)
        {
            _currentRot = _recordTrm.localEulerAngles;
            _currentRot.z += _turnSpeed * Time.deltaTime;
            _recordTrm.localEulerAngles = _currentRot;
        }
    }
}
