using System;
using UnityEngine;
[CreateAssetMenu(menuName = "SoundData")]
public class SoundData : ScriptableObject
{
    [SerializeField] AudioClip _audioClip;
    [SerializeField, Range(0f, 1f)] float _volume;
    public AudioClip Clip => _audioClip;
    public float Volume => _volume;

}
