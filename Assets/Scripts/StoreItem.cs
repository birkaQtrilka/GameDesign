using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StoreItem : MonoBehaviour
{
    [SerializeField] int _cost;
    [SerializeField] Upgrade _upgrade;
    [SerializeField] TextMeshPro _costTextMesh;
    [SerializeField] TextMeshPro _descriptionTextMesh;
    [SerializeField] SoundData _buySound;
    [SerializeField] SoundData _cantBuySound;

    public int Cost => _cost;

    void Start()
    {
        if(GameManager.Instance.BoughtUpgrades)
        {
            Destroy(gameObject);
            return;
        }

        _costTextMesh.text = "$" + _cost.ToString();
        _descriptionTextMesh.text = _upgrade.Description;
    }

    public void SetUpgrade(Upgrade upgrade)
    {
        _upgrade = upgrade;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(GameManager.Instance.CoinAmount >= _cost)
            {
                GlobalSounds.Instance.PlaySound(_buySound);
                _upgrade.Perform(collision.gameObject);
                GameManager.Instance.CoinAmount -= _cost;

                Destroy(gameObject);
                GameManager.Instance.AddUsedUpgrade(_upgrade);

            }
            else
                GlobalSounds.Instance.PlaySound(_cantBuySound);

            
        }
    }
}
