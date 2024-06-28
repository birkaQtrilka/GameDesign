using UnityEngine;


public class NullParent : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var other = collision.collider;
        if (other.gameObject.activeInHierarchy && other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }
}
