using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class GlobalSounds : MonoBehaviour
{
    //AudioSource _audioSource;
    List<AudioSource> activeSources = new();
    Stack<AudioSource> inactiveSources ;
    public static GlobalSounds Instance { get; private set; }
    void Awake()
    {
        //Debug.Log("song awake");
        if (Instance != null && Instance != this)
        {
            Debug.Log("destroying song manager");
            Destroy(this);
        }
        else
        {
            inactiveSources = new Stack<AudioSource>(GetComponents<AudioSource>());
            foreach (AudioSource source in inactiveSources) 
                InitSource(source);
            Instance = this;

            //DontDestroyOnLoad(this);
            //DontDestroyOnLoad(_audioSource);
        }
    }

    


    void InitSource(AudioSource source)
    {
        source.playOnAwake = false;
    }
    AudioSource GetSource()
    {
        AudioSource source;
        if(inactiveSources.Count == 0)
        {
            source = gameObject.AddComponent<AudioSource>();
            InitSource(source);
        }
        else
            source = inactiveSources.Pop();

        activeSources.Add(source);
        return source;

    }
    void ReleaseSource(AudioSource audioSource)
    {
        activeSources.Remove(audioSource);
        inactiveSources.Push(audioSource);
    }
    public void PlaySound(SoundData sound)
    {
        var source = GetSource();
        source.clip = sound.Clip;
        source.volume = sound.Volume;
        source.Play();
    }
    void FixedUpdate()
    {
        for (int i = 0; i < activeSources.Count; i++)
        {
            var source = activeSources[i];
            if(!source.isPlaying)
            {
                ReleaseSource(source);
                i--;
            }
        }
    }
}
