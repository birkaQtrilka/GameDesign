using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [SerializeField] Health _health;
    [SerializeField] TextMeshProUGUI _textMesh;
    void OnEnable()
    {
        _health.HealthUpdate += OnHealthChange;
    }
    void OnDisable()
    {
        _health.HealthUpdate -= OnHealthChange;

    }
    void OnHealthChange(int newAmount)
    {
        _textMesh.text = $"{newAmount}/{_health.MaxHealth}";
    }

    
}
