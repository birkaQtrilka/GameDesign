using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Upgrade/CritMultiplier")]
public class Upgrade_Crit : Upgrade
{
    [SerializeField] float _multiplier;
    [SerializeField] Sprite _visual;


    public override void Perform(GameObject player)
    {
        var attacker = player.GetComponent<PlayerAttack>();
        attacker.SetCritMult(_multiplier, _visual);
    }

}
