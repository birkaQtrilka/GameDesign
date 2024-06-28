using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombinationListUI : MonoBehaviour
{
    [SerializeField] RectTransform _container;
    [SerializeField] GameObject _listElementPrefab;
    [SerializeField] TextMeshProUGUI _noSpellsTextMesh;
    readonly Dictionary<Spell, GameObject> _list = new();
    public void Add(Spell spell)
    {
        if (_list.ContainsKey(spell)) return;

        var instance = Instantiate(_listElementPrefab, _container);
        var combination = spell.Combination;
        var parsedCombo = combination.Select(a=> a.ToString()).Aggregate((a, b) => a + "," + b);
        var instImage = instance.transform.Find("Image").GetComponent<Image>();
        instImage.sprite = spell.MenuImage;
        instImage.preserveAspect = true;
        instance.GetComponentInChildren<TextMeshProUGUI>().text = $"Name: {spell.name}, combination: {parsedCombo}\n{spell.Description}";
        _list.Add(spell, instance);
        //_container.gameObject.SetActive(true);

    }


    public void Remove(Spell spell) 
    {
        if(_list.ContainsKey(spell))
        {
            Destroy(_list[spell]);
            _list.Remove(spell);
        }
        
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.C)) 
        {
            if(_container.gameObject.activeInHierarchy)
            {
                _noSpellsTextMesh.gameObject.SetActive(false);
                _container.gameObject.SetActive(false);
            }
            else
            {
                if (_list.Count == 0)
                    _noSpellsTextMesh.gameObject.SetActive(true);

                _container.gameObject.SetActive(true);
            }
        }
    }
}
