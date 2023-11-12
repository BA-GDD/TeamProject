using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboManager : MonoBehaviour
{

    private static ComboManager _instance;
    public static ComboManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ComboManager>();
            }
            if (_instance == null)
            {
                Debug.LogWarning($"Warning! {typeof(ComboManager)} is not have this scene");
                return null;
            }
            return _instance;
        }
    }
    private int _combo;
    public void AddCombo()
    {
        _combo++;
        UIManager.Instanace.HandlePlusCombo?.Invoke();
    }
    public void ResetCombo()
    {
        _combo = 0;
        UIManager.Instanace.HandleResetCombo?.Invoke();
    }
}
