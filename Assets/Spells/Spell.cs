using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : ScriptableObject
{
    [SerializeField] string[] _combination; 
    [SerializeField] Sprite _menuImage;
    [SerializeField] SoundData _performSound;
    [SerializeField] string _description;
    public Sprite MenuImage => _menuImage;
    public string[] Combination => _combination;
    public SoundData PerformSound => _performSound;
    public string Description => _description;
#if UNITY_EDITOR
    private void OnValidate()
    {
        if (_combination != null && _combination.Length > 4)
        {
            Debug.LogError("Can not have more than 4 values");
            _combination = new string[4];
        }
    }
#endif
    public abstract bool TryPerform(GameObject player);
}
