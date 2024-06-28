using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

[CreateAssetMenu(menuName = "Spells/Portal")]
public class Spell_Portal : Spell
{
    [SerializeField] Portal _prefab;
    [SerializeField] string[] cantGoThrough = { "Door", "Ground" };
    public override bool TryPerform(GameObject player)
    {
        //if there's something in front, don't perform
        
        //spawn in front
        var instance = Instantiate(_prefab, player.transform);
        var playerSpriteRenderer = player.GetComponent<SpriteRenderer>();//change this mess
        instance.transform.localPosition = Vector3.right * playerSpriteRenderer.bounds.extents.x;

        var distanceToEnter = Vector2.Distance(player.transform.position, instance.EnterPortal.transform.position);
        var distanceToExit = Vector2.Distance(player.transform.position, instance.ExitPortal.transform.position);

        var blocks = Physics2D.RaycastAll(player.transform.position, player.transform.right, distanceToExit, LayerMask.GetMask(cantGoThrough));
        
        var anotherPortal = Physics2D.RaycastAll(player.transform.position, player.transform.right, distanceToEnter, LayerMask.GetMask("Portal")).Length > 1;
        if (blocks.Length > 0 || anotherPortal)
        {
            Destroy(instance.gameObject);
            return false;
        }

        //snap to grid
        var pos = instance.transform.position;
        //var signX = pos.x < 0 ? -1 : 1;
        var signY = pos.y < 0 ? -1 : 1;
        instance.transform.position = new(/*(Mathf.Floor(Mathf.Abs(pos.x)) + 1f) * signX*/pos.x, (Mathf.Floor(Mathf.Abs(pos.y)) + 0.5f) * signY, pos.z);

        instance.transform.SetParent(null);
        return true;
    }
}
