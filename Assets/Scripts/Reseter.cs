using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reseter : MonoBehaviour
{
    public void Reset()
    {
        GameManager.Instance.ReloadScene();
    }
}