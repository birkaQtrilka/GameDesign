using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[SelectionBase]
public class Hazard : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Health>(out var healthHandler))
        {
            healthHandler.DoDamage(99999);
            //GameManager.Instance.ReloadScene();
            //Destroy(collision.gameObject);
        }
    }
    
}
