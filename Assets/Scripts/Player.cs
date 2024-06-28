using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] SpellSlot _slot;
    [SerializeField] ComboVisualiser _comboUI;
    [SerializeField] CombinationListUI _spellList;

    public SpellSlot Slot => _slot;
    public ComboVisualiser ComboUI => _comboUI;
    public CombinationListUI SpellList => _spellList;
}
