using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] int _startingCoins;
    [SerializeField] int _coins;

    public int CurrScene => _currScene;
    public static GameManager Instance { get; private set; }
    public bool Reloaded { get; private set; }

    public int CoinAmount
    {
        get => _coins;
        set
        {
            _coins = value;
            _textUI.text = _coins.ToString();
        }
    }
    public DateTime StartTime { get; private set; }
    public bool BoughtUpgrades {  get; private set; }
    
    List<Spell> _previousAvailableSpells;
    int _currScene = 0;

    TextMeshProUGUI _textUI;
    //saving stuff
    SpellMaker _playerSpellMaker;
    int _coinsAtStartScene;
    Vector2? _lastCheckPoint;
    Spell _lastUsedSpell;

    List<Upgrade> _usedUpgrades = new();


    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        _textUI = FindObjectOfType<MoneyUI>().TextMesh;
        _playerSpellMaker = FindObjectOfType<SpellMaker>();

        CoinAmount = _startingCoins;
        _coinsAtStartScene = _coins;

    }

    void Start()
    {
        StartTime = DateTime.Now;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode arg1)
    {
        _currScene = scene.buildIndex;
        _textUI = FindObjectOfType<MoneyUI>().TextMesh;
        _textUI.text = _coins.ToString();

        _playerSpellMaker = FindObjectOfType<SpellMaker>();

        SongManager.Instance.RestartSong();

        if (_playerSpellMaker == null) return;

        //reapply upgrades
        foreach (var up in _usedUpgrades)
        {
            up.Perform(_playerSpellMaker.gameObject);
        }

        if (Reloaded && _lastCheckPoint.HasValue)
        {
            _playerSpellMaker.gameObject.transform.position = _lastCheckPoint.Value;
            _playerSpellMaker.Slot.EquipedSpell = _lastUsedSpell;
        }

        if (_previousAvailableSpells != null)
            foreach (var prev in _previousAvailableSpells)
                if(!_playerSpellMaker.AvailableSpells.Contains(prev)) _playerSpellMaker.AvailableSpells.Add(prev);

        _previousAvailableSpells = null;

    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

    }

    public void LoadScene(int sceneNum)
    {
        _currScene = sceneNum;
        _coinsAtStartScene = _coins;
        _previousAvailableSpells = _playerSpellMaker.AvailableSpells;
        _lastCheckPoint = null;
        Reloaded = false;
        BoughtUpgrades = false;
        SceneManager.LoadScene(sceneNum);
    }

    public void SaveState(Vector2 checkpointPos)
    {
        _lastCheckPoint = checkpointPos;
        
        _lastUsedSpell = _playerSpellMaker.Slot.EquipedSpell;

        _previousAvailableSpells = new(_playerSpellMaker.AvailableSpells);
        
    }

    public void ReloadScene()
    {

        CoinAmount = _coinsAtStartScene;
        Reloaded = true;
        SceneManager.LoadScene(_currScene);
    }

    public void AddUsedUpgrade(Upgrade upgrade)
    {
        _usedUpgrades.Add(upgrade);
        BoughtUpgrades = true;
    }

}
