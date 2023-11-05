using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : ScriptableObject
{
    public AbillityKey key;
    public abstract void Active(GameObject player);
}
