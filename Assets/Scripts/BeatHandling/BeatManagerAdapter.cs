using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class BeatManagerAdapter : MonoBehaviour
{
    [SerializeField] UnityEvent OnBeat;

    void DelegateEvent()
    {
        OnBeat.Invoke();
    }
    
    void OnEnable()
    {
        StartCoroutine(FrameSkip());
    }

    IEnumerator FrameSkip()
    {
        yield return null;
        if (SongManager.Instance != null)
            SongManager.Instance.Beat += DelegateEvent;
        else
            Debug.LogWarning("Cannot find SongManager");

    }

    void OnDisable()
    {
        if(SongManager.Instance != null)
            SongManager.Instance.Beat -= DelegateEvent;

    }

}
