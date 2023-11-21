using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentRotater : MonoBehaviour
{
    private float _rotateX;
    private float _rotateY;

    [SerializeField]private Transform _eye;

    public float HorizontalRespons;
    public float VerizontalRespons;

    [SerializeField] InputReader _inputReader;

    private void Start()
    {
        _inputReader.rotationCameraEvt += Rotate;
    }

    public void Rotate(Vector2 value)
    {
        //if (StopRotate == true) return;
        float settingValue = 1;
        if (SaveManager.Instance != null)
        {

            settingValue = Mathf.Lerp(0.1f, 3f, SaveManager.Instance.data.senseValue);
        }
        _rotateX += value.y * HorizontalRespons * settingValue;
        _rotateX = Mathf.Clamp(_rotateX, -70, 80);
        _rotateY += value.x * VerizontalRespons * settingValue;
        transform.rotation = Quaternion.Euler(0, _rotateY, 0);
        _eye.localRotation = Quaternion.Euler(-_rotateX, 0, 0);
    }

    private void OnDestroy()
    {
        _inputReader.rotationCameraEvt -= Rotate;
    }
}
