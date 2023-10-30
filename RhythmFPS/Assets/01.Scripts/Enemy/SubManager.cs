using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubManager : MonoBehaviour
{
    private static SubManager _instance;
    public static SubManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindAnyObjectByType<SubManager>();
            }
            return _instance;   
        }
    }

    public Transform playerTrm;
}
