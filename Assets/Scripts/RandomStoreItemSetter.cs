using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(StoreItem))]
public class RandomStoreItemSetter : MonoBehaviour
{
    [SerializeField] Upgrade[] _selectionOfUpgrades;
    StoreItem _storeItem;

    void Awake()
    {
        _storeItem = GetComponent<StoreItem>();
        _storeItem.SetUpgrade(_selectionOfUpgrades.GetRandomItem());
    }
}
