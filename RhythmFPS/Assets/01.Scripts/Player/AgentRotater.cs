using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentRotater : MonoBehaviour
{
    private float _rotateX;
    private float _rotateY;

    private Transform _eye;

    public float HorizontalRespons;
    public float VerizontalRespons;
    private void Awake()
    {
        _eye = transform.Find("Visual/Eye");
    }
    public void Rotate(Vector2 value)
    {
        _rotateX += value.y * HorizontalRespons;
        _rotateX = Mathf.Clamp(_rotateX, -70, 70);
        _rotateY += value.x * VerizontalRespons;
        transform.rotation = Quaternion.Euler(0, _rotateY, 0);
        _eye.localRotation = Quaternion.Euler(-_rotateX, 0, 0);
    }
}
