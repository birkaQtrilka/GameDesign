using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[SelectionBase]
public class BeatBarVisualizer : MonoBehaviour
{
    [SerializeField] Image _bar;
    [SerializeField] Image _marginZone;
    [SerializeField] Color _onBeatColor = Color.green;
    [SerializeField] Color _offBeatColor = Color.red;
    [SerializeField, Range(0, 1)] float _alpha;
    [SerializeField] Transform pointA;
    [SerializeField] Transform pointB;
    [SerializeField] float lastProgress;
    SongManager _songManager;

    void Start()
    {
        _songManager = SongManager.Instance;
        _marginZone.transform.position = Vector3.Lerp(pointA.position, pointB.position, 0.5f);
        _marginZone.rectTransform.sizeDelta = new Vector2(Vector2.Distance(pointA.position, pointB.position) * _songManager.BeatErrorMargin*2, _marginZone.rectTransform.sizeDelta.y);
        //code the area of zone
    }
    
    void Update()
    {
        //colloring
        if (_songManager.OnBeatMargin)
            _marginZone.color = _onBeatColor;
        else
            _marginZone.color = _offBeatColor;
        _marginZone.ChangeAlpha(_alpha);

        var progress = _songManager.BeatProgress + 0.5f;
        if(progress > 1) 
            progress--;
        
        if(Mathf.Abs(progress - lastProgress) > 0.5f)
            (pointA, pointB) = (pointB, pointA);
        _bar.transform.position = Vector3.Lerp(pointA.position, pointB.position, progress);
        
        lastProgress = progress;
    }
    
}
