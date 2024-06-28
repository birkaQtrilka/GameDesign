using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellSlot : MonoBehaviour
{
    //GameObject _player;
    SpellMaker _maker;
    [SerializeField] Image _spellMenuImg;
    [SerializeField] Sprite _emptyMenuSprite;
    [SerializeField] float _notPerformColorStayTime = 0.15f;
    [SerializeField] GameObject _textObj;
    [SerializeField] Spell _equipedSpell;
    Coroutine _equipedCoroutine;
    public Spell EquipedSpell 
    {
        get
        { 
            return _equipedSpell;
        }
        set 
        {
            if (value == null)
            {
                _spellMenuImg.sprite = _emptyMenuSprite;
                _textObj.SetActive(false);
            }
            else
            {
                _spellMenuImg.sprite = value.MenuImage;
                _textObj.SetActive(true);
            }
            _equipedSpell = value;
            _spellMenuImg.preserveAspect = true;
        } 
    }
    private void Awake()
    {
        EquipedSpell = _equipedSpell;
    }
    private void Start()
    {
        _maker = FindObjectOfType<SpellMaker>();
        _maker.CreatedSpell += OnSpellCreate;
    }

    private void OnSpellCreate(Spell spell)
    {
        EquipedSpell = spell;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q) && EquipedSpell != null)
        {

            if (EquipedSpell.TryPerform(_maker.gameObject))
            {
                GlobalSounds.Instance.PlaySound(EquipedSpell.PerformSound);
                //EquipedSpell = null;
            }
            else
            {
                if(_equipedCoroutine != null)
                    StopCoroutine(_equipedCoroutine);
                _equipedCoroutine = StartCoroutine(CannotUseSpellVisualiser());
            }

        }
    }
    IEnumerator CannotUseSpellVisualiser()
    {
        _spellMenuImg.color = Color.red;
        yield return new WaitForSeconds(_notPerformColorStayTime);
        _spellMenuImg.color = Color.white;

    }
}
