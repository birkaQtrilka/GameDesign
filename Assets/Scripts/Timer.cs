using System.Collections;
using System;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField] float _time;
    [SerializeField] bool startTimer;
    [SerializeField] UnityEvent OnTimerEnd;
    public event Action TimerEnd;
    Coroutine _routine;
    public float Time
    {
        get { return _time; }
        set { _time = value; }
    }

    public void StartTimer()
    {
        if (_routine != null)
            StopCoroutine(_routine);
        _routine = StartCoroutine(Wait());
        startTimer = false;
    }

    private void FixedUpdate()
    {
        if(startTimer)
        {
            if(_routine != null)
                StopCoroutine(_routine);
            _routine = StartCoroutine(Wait());
            startTimer = false;
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(_time);
        OnTimerEnd?.Invoke();
        TimerEnd?.Invoke();
    }
}
