using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
[RequireComponent(typeof(AudioSource))]
public class SongManager : MonoBehaviour
{

    [SerializeField] Song _song;
    [SerializeField] float _beatErrorMargin;
    [SerializeField] float _volumeGainTime;
    [SerializeField, Range(0, 1)] float _songVolume;
    [SerializeField] bool isLvl3;

    public event Action OnSongStart;
    public event Action Beat;
    public Song CurrentSong => _song;
    public static SongManager Instance { get; private set; }
    public bool OnBeatMargin { get; private set; }
    public float BeatProgress {  get; private set; }
    public Song Song => _song;
    public float BeatErrorMargin => _beatErrorMargin;

    AudioSource _audioSource;
    double secPerBeat;
    double songPosition;
    double songPositionInBeats;
    bool start = false;
    double scheduleTime;
    double songLength;
    readonly WaitForSeconds _fixedWait = new(0.02f);

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.Log("destroying song manager");
            Destroy(this);
        }
        else
        {
            _audioSource = GetComponent<AudioSource>();

            Instance = this;
            
        }
        
    }

    void OnDestroy()
    {
        OnSongStart = null;
        Beat = null;
        Instance = null;
    }

    public void RestartSong()
    {
        secPerBeat = 60d / _song.Bpm;

        //totalBeats = (int)(_song.Bpm * (songLength / 60f));

        _audioSource.clip = Song.Beat;
        scheduleTime = AudioSettings.dspTime + 1.5d;
        start = false;
        songLength = _audioSource.clip.samples / _audioSource.clip.frequency;
        _audioSource.PlayScheduled(scheduleTime);

    }

    IEnumerator FadeOutVolume()
    {
        var currVolumeGainTime = _volumeGainTime;
        var endVolume = _songVolume;
        _audioSource.volume = 0;
        while (currVolumeGainTime > 0)
        {
            var progress = 1 - currVolumeGainTime / _volumeGainTime;
            _audioSource.volume = endVolume * progress;
            yield return _fixedWait;
            currVolumeGainTime -= 0.02f;
        }
    }

    void Update()
    {
        var currDspTime = AudioSettings.dspTime;
        if (!start && currDspTime >= scheduleTime)
        {

            StopAllCoroutines();
            StartCoroutine(FadeOutVolume());
            OnSongStart?.Invoke();

            //Debug.Log($"totalBeats {totalBeats}, scheduleTime {scheduleTime}, secPerBeat {secPerBeat}, dspSongTime {dspSongTime}, songPosition {currDspTime-dspSongTime}");
            start = true;
        }

        if (!start)
            return;

        //literally no clue why this is the case but fuck it
        if(isLvl3)
            songPosition = _audioSource.timeSamples / (float)_audioSource.clip.frequency - (secPerBeat * .5f);// - (GameManager.Instance.Reloaded ? 0 : (secPerBeat / 2));
        else
            songPosition = _audioSource.timeSamples / (float)_audioSource.clip.frequency  ;// - (GameManager.Instance.Reloaded ? 0 : (secPerBeat / 2));
        //Debug.Log($"songPos: {songPosition}, yt tut: {_audioSource.timeSamples / (_audioSource.clip.frequency )}");

        songPositionInBeats = songPosition / secPerBeat;
        var currBeatNum = (int)Math.Floor(songPositionInBeats);
        var progress = (float)songPositionInBeats - currBeatNum;

        OnBeatMargin = 1f - progress < _beatErrorMargin || progress < _beatErrorMargin;

        if (BeatProgress - progress > 0.5f)
        {
            Beat?.Invoke();
        }

        BeatProgress = progress;

        if (songPosition > songLength)
            RestartSong();
    }

}
