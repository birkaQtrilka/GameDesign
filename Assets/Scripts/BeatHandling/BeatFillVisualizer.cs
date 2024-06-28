using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Image))]
public class BeatFillVisualizer : MonoBehaviour
{
    Image _fill;
    SongManager _songManager;
    private void Awake()
    {
        _fill = GetComponent<Image>();
    }
    private void Start()
    {
        _songManager = SongManager.Instance;

    }
    private void FixedUpdate()
    {
        _fill.fillAmount = _songManager.BeatProgress;
        if (_songManager.OnBeatMargin)
            _fill.color = Color.green;
        else
            _fill.color = Color.red;
    }
}
