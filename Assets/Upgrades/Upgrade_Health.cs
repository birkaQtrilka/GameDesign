using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="Upgrade/MaxLife")]
public class Upgrade_Health : Upgrade
{
    [SerializeField] int _healAmount;
    public override void Perform(GameObject player)
    {
        var health = player.GetComponent<Health>();
        health.IncreaseMaxLife(_healAmount);
        health.Heal(_healAmount);
    }
}
