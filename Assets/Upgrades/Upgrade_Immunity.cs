using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class Upgrade_Immunity : Upgrade
{
    [SerializeField] float _immunityTime;
    [SerializeField] int _healOnActivation;
    public override void Perform(GameObject player)
    {
        var health = player.GetComponent<Health>();
        //health.HealthUpdate += OnHealthUpdate; 
    }

    private void OnHealthUpdate(Health health, int amount)
    {
        if (amount > 0) return;
        health.Heal(_healOnActivation);
        health.StartCoroutine(Behaviour(health.gameObject, health));
       // health.HealthUpdate -= OnHealthUpdate;

    }

    IEnumerator Behaviour(GameObject player, Health playerHealth)
    {
        var renderers = player.GetComponentsInChildren<SpriteRenderer>().ToList();
        renderers.Add(player.GetComponent<SpriteRenderer>());
        var currTime = _immunityTime;
        while(currTime>0)
        {
            yield return null;
            foreach(var renderer in renderers)
            {
                var c = renderer.color;
                var a = Mathf.Clamp(Mathf.Sin(currTime), 0, 1);
                renderer.color = new Color(c.r, c.g, c.b, a);
            }
            currTime -= Time.deltaTime;
        }
    }
}
