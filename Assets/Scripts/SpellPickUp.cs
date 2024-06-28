using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[SelectionBase]
public class SpellPickUp : PickUp
{
    [SerializeField] Spell spell;
    [SerializeField] UnityEvent OnCollect;
    public override void Grab(GameObject player)
    {
        var spellMaker = player.GetComponent<SpellMaker>();
        spellMaker.AddAvailableIfNone(spell);

        spellMaker.Slot.EquipedSpell = spell;
        OnCollect?.Invoke();
        Destroy(gameObject);

    }
}
