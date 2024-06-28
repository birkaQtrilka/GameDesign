using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Song")]
public class Song : ScriptableObject
{
    [SerializeField] AudioClip _beat;
    [SerializeField] int _bpm;
    public int Bpm => _bpm;
    public AudioClip Beat => _beat;
}
