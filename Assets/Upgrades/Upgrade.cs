using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Upgrade : ScriptableObject
{
    [SerializeField] string _description;
    public string Description => _description;
    public abstract void Perform(GameObject player);
}
