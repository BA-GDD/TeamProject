using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


public enum AbillityKey
{
    RightButton,
    Shift
}
public class AgentAbility : MonoBehaviour
{
    private Dictionary<string, Ability> _keyAction = new();
    private Dictionary<AbilityWeaponType, AbilityWeapon> _abilityWeaponsDIctionary = new();
    [SerializeField]private List<Ability> _defaultAbility;
    [SerializeField]private List<AbilityWeapon> _abilityWeapons;
    [SerializeField] private InputReader _inputReader;

    public void OffAbilityWeapon()
    {
        foreach (var item in _abilityWeaponsDIctionary)
        {
            item.Value.gameObject.SetActive(false);
        }
    }

    private void Awake()
    {
        //foreach(var key in (int[])Enum.GetValues(typeof(AbillityKey)))
        //{
        //_keyAction?.Add((AbillityKey)key,null);
        //}
        foreach (AbilityWeapon weapon in _abilityWeapons)
        {
            _abilityWeaponsDIctionary.Add(weapon.type, weapon);
        }
        foreach (Ability ability in _defaultAbility)
        {
            _keyAction.Add(ability.key.ToString(), ability);
        }
        _inputReader.OnAbillityEvent += OnAbilityHandle;
    }
    private void OnAbilityHandle(string key)
    {
        StringBuilder builder = new StringBuilder(key);
        builder.Replace(" ", "");
        ActiveAbility(builder.ToString());
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
        if (RhythmManager.instance.Judgement() == false) return;

        if (_keyAction.ContainsKey(key))
        {
            _keyAction[key]?.Active(gameObject);
        }
    }

    public AbilityWeapon GetAbilityWeapon(AbilityWeaponType type)
    {
        if(_abilityWeaponsDIctionary.ContainsKey(type))
        {
            return _abilityWeaponsDIctionary[type];
        }
        return null;
    }
}