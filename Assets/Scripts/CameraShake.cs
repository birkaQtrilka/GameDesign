using Cinemachine;
using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera _cam;
    [SerializeField] float _intensity;
    [SerializeField] float _time;
    CinemachineBasicMultiChannelPerlin _perlin;
    void Awake()
    {
        _perlin = _cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

    }
    public void Shake()
    {
        StartCoroutine(ShakeTimer());
    }
    IEnumerator ShakeTimer()
    {
        _perlin.m_AmplitudeGain = _intensity;
        yield return new WaitForSeconds(_time);
        _perlin.m_AmplitudeGain = 0;

    }
}
