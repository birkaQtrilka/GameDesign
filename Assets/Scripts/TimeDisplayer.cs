using System;
using TMPro;
using UnityEngine;

public class TimeDisplayer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _timeTxtMesh;
    private void Start()
    {
        var playTime = DateTime.Now - GameManager.Instance.StartTime;
        var mins = playTime.Minutes > 9 ? playTime.Minutes.ToString() : ("0" + playTime.Minutes);
        var secs = playTime.Seconds > 9 ? playTime.Seconds.ToString() : ("0" + playTime.Seconds);
        _timeTxtMesh.text = $"{ mins }:{ secs }";

    }
}
