using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirCal : MonoBehaviour
{
    private void Start()
    {
        transform.rotation = Quaternion.LookRotation((Camera.main.transform.position - transform.position).normalized);
    }
}
