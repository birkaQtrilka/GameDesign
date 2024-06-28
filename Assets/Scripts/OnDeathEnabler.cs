using Cinemachine;
using System.Collections;
using UnityEngine;
[RequireComponent(typeof(ParticleSystem))]
public class OnDeathEnabler : MonoBehaviour
{
    [SerializeField] Health _observingDeath;
    [SerializeField] bool _setActive = true;
    [SerializeField] GameObject _objectToEnable;
    [SerializeField] CinemachineVirtualCamera _playerCam;
    [SerializeField] CinemachineVirtualCamera _myCam;
    [SerializeField] float _cameraTransitionTime;
    [SerializeField] float _shakeTime;
    [SerializeField] float _shakeIntensity;
    [SerializeField] float _explosionTime;
    [SerializeField] float _explosionShakeIntensity;
    [SerializeField] float _observeTime;
    [SerializeField] float _endFOV;

    CinemachineBasicMultiChannelPerlin _perlin;
    ParticleSystem _particleSystem;
    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        _perlin = _myCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

    }
    void OnEnable()
    {
        _observingDeath.BeforeDeath += OnDeath;

    }
    void OnDisable()
    {
        if(_observingDeath != null)
            _observingDeath.BeforeDeath -= OnDeath;

    }

    void OnDeath()
    {
        _observingDeath.BeforeDeath -= OnDeath;

        StartCoroutine(Animation());
    }
    IEnumerator Animation()
    {
        _myCam.m_Lens.OrthographicSize = _playerCam.m_Lens.OrthographicSize;
        int playerCamPrio = _myCam.Priority;
        _myCam.Priority = playerCamPrio + 1;

        yield return new WaitForSeconds(_cameraTransitionTime);
        //start cam shake
        _perlin.m_AmplitudeGain = _shakeIntensity;

        float copyTime = 0;
        float initFOV = _myCam.m_Lens.OrthographicSize;
        while (copyTime < _shakeTime)
        {
            yield return null;
            _myCam.m_Lens.OrthographicSize = Utils.Lerp(initFOV, _endFOV, copyTime / _shakeTime);
            copyTime += Time.deltaTime;
        }
        _perlin.m_AmplitudeGain = _explosionShakeIntensity;
        _objectToEnable.SetActive(_setActive);
        _particleSystem.Play();


        copyTime = 0;
        while (copyTime < _explosionTime)
        {
            yield return null;
            _myCam.m_Lens.OrthographicSize = Utils.Lerp(_endFOV, initFOV, copyTime / _explosionTime);
            copyTime += Time.deltaTime;

        }
        _perlin.m_AmplitudeGain = 0;

        yield return new WaitForSeconds(_observeTime);
        _myCam.Priority = playerCamPrio;
    }
}
