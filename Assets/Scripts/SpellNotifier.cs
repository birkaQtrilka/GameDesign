using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class SpellNotifier : MonoBehaviour
{
    [SerializeField] Image _menuImageDisplayer;
    [SerializeField] TextMeshProUGUI _textMesh;
    [SerializeField] float _notifyTimer = 3f;
    public static SpellNotifier Instance { get; private set; }  
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
        Instance.gameObject.SetActive(false);
    }
    public void Notify(Spell spell)
    {
        _textMesh.text = $"New! {spell.name} Spell!! use combination {spell.Combination.Select(a=>a.ToString()).Aggregate((a, b) => a.ToString() + "," + b.ToString())}";
        _menuImageDisplayer.sprite = spell.MenuImage;
        _menuImageDisplayer.preserveAspect = true;
        gameObject.SetActive(true);
        StartCoroutine(DisableAfterTime());
    }
    IEnumerator DisableAfterTime() 
    {
        yield return new WaitForSeconds(_notifyTimer);
        gameObject.SetActive(false);
    }
}
