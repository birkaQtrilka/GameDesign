using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageVisualiser : MonoBehaviour
{
    [SerializeField] SpriteRenderer _subjectRenderer;
    [SerializeField] Color _appliedTint;
    [SerializeField] float _stayTime;
    Color _defaultColor;
    Coroutine _coroutine;
    public void Perform()
    {
        if(_coroutine != null)
        {
            _subjectRenderer.color = _defaultColor;
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
        _coroutine = StartCoroutine(Visualise());
    }
    IEnumerator Visualise()
    {
        _defaultColor = _subjectRenderer.color;
        _subjectRenderer.color = _appliedTint;
        yield return new WaitForSeconds(_stayTime);
        _subjectRenderer.color = _defaultColor;

        _coroutine = null;
    }
}
