using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalsUndoHandler : MonoBehaviour
{
    public static PortalsUndoHandler Instance { get; private set; }
    List<Portal> _portalsPlaced = new();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

    }

    public void AddPortal(Portal portal)
    {
        _portalsPlaced.Add(portal);
    }

    public void RemovePortal(Portal portal)
    {
        _portalsPlaced.Remove(portal);
    }

    private void Update()
    {
        var portalsCount = _portalsPlaced.Count;
        if (Input.GetKeyDown(KeyCode.E) && portalsCount > 0)
        {
            Destroy(_portalsPlaced[portalsCount - 1].gameObject);
        }
    }

    private void OnDestroy()
    {
        Instance = null;
    }
}
