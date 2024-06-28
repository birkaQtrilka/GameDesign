using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Health _followedHealth;
    [SerializeField] Image _fillImage;
    private void Awake()
    {
        GetComponent<Canvas>().worldCamera = Utils.MainCam;
    }
    private void OnEnable()
    {
        _followedHealth.HealthUpdate += OnHealthUpdate;
    }
    private void OnDisable()
    {
        _followedHealth.HealthUpdate -= OnHealthUpdate;
    }
    private void OnHealthUpdate(int newHealth)
    {
        _fillImage.fillAmount = newHealth / (float)_followedHealth.MaxHealth;
    }
    private void FixedUpdate()
    {
        transform.rotation = Quaternion.identity;
    }
}
