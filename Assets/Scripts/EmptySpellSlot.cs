using System;
using UnityEngine;
//[Serializable]
//public struct SoundDataStr
//{
//    [SerializeField] AudioClip _audioClip;
//    [SerializeField, Range(0f, 1f)] float _volume;
//    public readonly AudioClip Clip => _audioClip;
//    public readonly float Volume => _volume;
//}
public class EmptySpellSlot : MonoBehaviour
{
    [SerializeField] SoundData _soundData;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<SpellMaker>(out var pm))
        {
            GlobalSounds.Instance.PlaySound(_soundData);
            pm.Slot.EquipedSpell = null;
            Destroy(gameObject);
        }
    }
}
