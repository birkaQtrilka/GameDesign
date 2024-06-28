using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class PickUp : MonoBehaviour 
{
    [SerializeField] SoundData _sound;
    public SoundData Sound => _sound;
    public abstract void Grab(GameObject player);
}
