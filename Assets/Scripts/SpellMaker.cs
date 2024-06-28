using Cinemachine.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpellMaker : MonoBehaviour
{
    [SerializeField] SpellSlot _slot;
    [SerializeField] List<Spell> _availableSpells;
    [SerializeField] CombinationListUI _ui;
    [SerializeField] int _comboLength;
    [SerializeField] bool _loseComboOnBeatMiss;
    [SerializeField] int _allowedBeatsToSkip;
    [SerializeField] UnityEvent _comboStep;

    public SpellSlot Slot => _slot;
    public int ComboLength => _comboLength;
    public List<Spell> AvailableSpells { get => _availableSpells; set => _availableSpells = value; }

    string[] _myCombo = new string[4];
    SongManager _songManager;
    bool _wasBeatWithCode;
    bool _wasBeat;
    int _comboIndex = 0;
    int _beatNum = 0;
    int _codeInputBeatNum;
    public event Action<Spell> CreatedSpell;
    public event Action FailCreatedSpell;
    public event Action<string> ComboStep;
    public event Action FailedCombo;

    void Awake()
    {
        _myCombo = new string[_comboLength];
    }
    void Start()
    {
        _songManager = SongManager.Instance;
        foreach (var spell in _availableSpells)
            _ui.Add(spell);
    }
    public bool AddAvailableIfNone(Spell spell)
    {
        //notify player about new combination
        if(_availableSpells.Contains(spell)) return false;
        
        SpellNotifier.Instance.Notify(spell);
        _availableSpells.Add(spell);
        _ui.Add(spell);
        return true;
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TrySpellCombo("H");
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            TrySpellCombo("J");
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            TrySpellCombo("K");
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            TrySpellCombo("L");
        }
        else
            TrySpellCombo(null);

        
    }

    void TrySpellCombo(string code)
    {
        var onBeat = _songManager.OnBeatMargin;
        if (onBeat && !_wasBeat)
        {
            _wasBeat = true;
            _beatNum++;//counting the beats so I can determine on how many beats I didn't input anything
        }
        else if (!onBeat)
        {
            _wasBeatWithCode = false;
            _wasBeat = false;
        }

        if (code == null)//fail combo if you skip a beat
        {
            if (_loseComboOnBeatMiss && _codeInputBeatNum < _beatNum - _allowedBeatsToSkip && !onBeat)
            {
                ClearCombo();
                FailedCombo?.Invoke();
            }

            return;
        }


        if (onBeat && !_wasBeatWithCode)//I need _wasBeatWithCode so I run the code bellow only one frame
        {
            _wasBeatWithCode = true;
            _myCombo[_comboIndex++] = code;

            _codeInputBeatNum = _beatNum;
            ComboStep?.Invoke(code);
            _comboStep?.Invoke();

            if (_comboIndex == _comboLength)
            {
                var spell = CheckCastedSpell();
                if (spell != null)
                {
                    CreatedSpell?.Invoke(spell);
                }
                else
                    FailCreatedSpell?.Invoke();
                ClearCombo();
            }

        }
        else if (!onBeat)
        {
            ClearCombo();
            FailedCombo?.Invoke();
        }
    }

    void ClearCombo()
    {
        _comboIndex = 0;
        //_myCombo = new int[_comboLength];
    }
    Spell CheckCastedSpell()
    {
        foreach (var spell in _availableSpells)
        {
            if (spell.Combination.Length > _comboLength) continue;
            var combination = spell.Combination;
            int i = 0;
            for (; i < combination.Length; i++)
                if (combination[i] != _myCombo[i])
                    break;

            if (i == _comboLength)
                return spell;
        }
        return null;
    }
    
}
