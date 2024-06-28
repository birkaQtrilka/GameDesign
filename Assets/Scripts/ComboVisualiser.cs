using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComboVisualiser : MonoBehaviour
{
    [SerializeField] SpellMaker _spellMaker;
    [SerializeField] TextMeshProUGUI _codeElementPrefab;
    [SerializeField] Image _background;
    [SerializeField, Range(0, 1)] float _backgroundAlpha; 
    [SerializeField] Color _defaultColor;
    [SerializeField] Color _successColor;
    [SerializeField] Color _createColor;
    [SerializeField] Color _failColor;
    [SerializeField] Color _failCreateColor;
    [SerializeField] float _colorStayTime;

    int _spellIndex;
    WaitForSeconds _cooldown;
    List<TextMeshProUGUI> _codeElements = new();
    VerticalLayoutGroup _elementAlligner;

    void Awake()
    {
        _elementAlligner = _background.GetComponent<VerticalLayoutGroup>();
        GenerateTextElements();
        _cooldown = new(_colorStayTime);

        _defaultColor.a = _backgroundAlpha;
        _successColor.a = _backgroundAlpha;
        _failColor.a = _backgroundAlpha;
        _createColor.a = _backgroundAlpha;
        _failCreateColor.a = _backgroundAlpha;

    }
    private void Start()
    {

        StartCoroutine(Test());
    }
    IEnumerator Test()
    {
        yield return null;
        _elementAlligner.enabled = false;
        _background.gameObject.SetActive(false);
    }
    void OnEnable()
    {
        _spellMaker.FailedCombo += OnComboFail;
        _spellMaker.ComboStep += OnComboStep;
        _spellMaker.CreatedSpell += OnSpellCreate;
        _spellMaker.FailCreatedSpell += OnFailToCreateSpell;
    }

    void OnDisable()
    {
        _spellMaker.FailedCombo -= OnComboFail;
        _spellMaker.ComboStep -= OnComboStep;
        _spellMaker.CreatedSpell -= OnSpellCreate;
        _spellMaker.FailCreatedSpell -= OnFailToCreateSpell;

    }
    void GenerateTextElements()
    {
        _elementAlligner.enabled = true;
        //_elementAlligner.
        _background.gameObject.SetActive(true);

        _codeElements.Clear();
        for (int i = 0; i < _background.transform.childCount; i++)
            Destroy(_background.transform.GetChild(i).gameObject);

        for (int i = 0; i < _spellMaker.ComboLength; i++)
            _codeElements.Add( Instantiate(_codeElementPrefab, _background.transform));

        //_elementAlligner.enabled = false;

    }
    void OnSpellCreate(Spell spell)
    {
        _background.color = _createColor;
        _spellIndex = 0;
        StopAllCoroutines();

        StartCoroutine(AfterCoolDown(() =>
        {
            _background.gameObject.SetActive(false);
        }));

    }
    void OnFailToCreateSpell()
    {
        _background.color = _failCreateColor;
        _spellIndex = 0;

        StopAllCoroutines();

        StartCoroutine(AfterCoolDown(() =>
        {
            _background.gameObject.SetActive(false);
        }));
    }
    void OnComboStep(string code)
    {
        if (_spellIndex == 0)
        {
            foreach (var c in _codeElements)
                c.gameObject.SetActive(false);
            _background.gameObject.SetActive(true);
        }
        
        _background.color = _successColor;
        var codeElement = _codeElements[_spellIndex];
        codeElement.text = code;
        codeElement.gameObject.SetActive(true);

        if (++_spellIndex == _spellMaker.ComboLength) return;
        StopAllCoroutines();
        StartCoroutine(AfterCoolDown(() =>
            _background.color = _defaultColor
        ));
    }

    void OnComboFail()
    {
        if (_spellIndex == 0) return;
        _spellIndex = 0;
        _background.color = _failColor;
        StopAllCoroutines();
        StartCoroutine(AfterCoolDown(() =>
        {
            _background.gameObject.SetActive(false);

        }));

    }
    IEnumerator AfterCoolDown(Action action)
    {
        yield return _cooldown;
        action();
    }
}
