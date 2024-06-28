using UnityEngine;

[CreateAssetMenu(menuName = "Upgrade/AttackSpeed")]

public class Upgrade_AttackSpeed : Upgrade
{
    [SerializeField] float _addAmount;
    public override void Perform(GameObject player)
    {

        player.GetComponent<PlayerAttack>().AttackCooldown -= _addAmount;

    }
}
