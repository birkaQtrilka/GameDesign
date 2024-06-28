using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeatVisualiser : MonoBehaviour
{
    [SerializeField] Image _visualiser;
    [SerializeField] float speed;
    SongManager _songManager;
    void Start()
    {
        _songManager = SongManager.Instance;

    }
    void Update()
    {
        transform.localScale -= Vector3.one * Time.deltaTime * speed ;
        if (_songManager.OnBeatMargin)
            _visualiser.color = Color.green;
        else
            _visualiser.color = Color.red;
    }
    private void OnEnable()
    {
        StartCoroutine(WaitUntillNotNull());
    }
    IEnumerator WaitUntillNotNull()
    {
        yield return new WaitUntil(() => _songManager != null);
        _songManager.Beat += OnBeat;
    }
    private void OnBeat()
    {
        transform.localScale = Vector3.one;
    }

    private void OnDisable()
    {
        _songManager.Beat -= OnBeat;

    }
}
