using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AbillityKey
{
    RightButton,
    Shift
}
public class AgentAbility : MonoBehaviour
{
    private Dictionary<string, Ability> _keyAction = new();
    [SerializeField]private List<Ability> _defaultAbility;


    private void Awake()
    {
        //foreach(var key in (int[])Enum.GetValues(typeof(AbillityKey)))
        //{
        //_keyAction?.Add((AbillityKey)key,null);
        //}
        foreach (Ability ability in _defaultAbility)
        {
            _keyAction.Add(ability.key.ToString(), ability);
        }
    }
    public void ChangeAbility(Ability ability)
    {
        string keyName = ability.key.ToString();
        if (!_keyAction.ContainsKey(keyName))
            _keyAction.Add(keyName, ability);
        else
            _keyAction[keyName] = ability;

    }

    public void ActiveAbility(string key)
    {
        if (RhythmManager.instance.Judgement(RhythmAction.Ability) == false) return;

        if (_keyAction.ContainsKey(key))
        {
            _keyAction[key]?.Active(gameObject);
        }
    }
}