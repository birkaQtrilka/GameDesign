using UnityEngine;
[CreateAssetMenu(menuName = "Spells/Unlock")]
public class Spell_Unlock : Spell
{
    [SerializeField] float range;
    [SerializeField] ContactFilter2D _contactFilter;
    readonly Collider2D[] _results = new Collider2D[2];
    public override bool TryPerform(GameObject player)
    {
        var renderer = player.GetComponent<SpriteRenderer>();
        int dir = player.transform.rotation.eulerAngles.y == 180 ? -1 : 1;
        var point = new Vector3((renderer.bounds.extents.x + range - .3f) * dir, 0);
        point += player.transform.position;
        //spawn collider 
        Physics2D.OverlapCircle(point, .1f, _contactFilter, _results);
        bool opened = false;
        foreach (Collider2D c in _results) 
            if(c != null)
            {
                c.GetComponent<Door>().Open();
                opened = true;
            }
        return opened;
    }
}
