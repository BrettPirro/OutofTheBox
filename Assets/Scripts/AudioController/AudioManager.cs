using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.CompilerServices;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    AudioSource m_AudioSource;
    bool m_IsPlaying = false;
    [SerializeField][Range(0,1)]float AudioTransitionTime;



    private void OnEnable()
    {
        AudioWrapper.SwitchAudio += AudioChange;
    }

    private void OnDisable()
    {
        AudioWrapper.SwitchAudio -= AudioChange;
    }

    private void OnDestroy()
    {
        AudioWrapper.SwitchAudio -= AudioChange;
    }

    private void Awake()
    {
        AudioWrapper.SwitchAudio += AudioChange;
        m_AudioSource = GetComponent<AudioSource>();
    }


  

    



    IEnumerator FadeAudioOut(float delay) 
    {
       
        while (m_AudioSource.volume > 0) 
        {
            m_AudioSource.volume -=Time.deltaTime/delay;
            yield return null;
        }

       
    }

    IEnumerator FadeAudioIn(float delay,float Vol)
    {
        while (m_AudioSource.volume < Vol)
        {
            m_AudioSource.volume += Time.deltaTime/delay;
            yield return null;
        }
        m_AudioSource.Play();
    }

    IEnumerator ChangeAudio(AudioClip audio,float delay,float Vol) 
    {
        m_IsPlaying=true;
        yield return FadeAudioOut(delay);
        m_AudioSource.clip = audio;
        yield return FadeAudioIn(delay,Vol);
        m_IsPlaying=false;
    }

    public void AudioChange(AudioClip audioClip, float vol) 
    {
        if (m_IsPlaying) { return; }
        StartCoroutine(ChangeAudio(audioClip, AudioTransitionTime,vol));
    }

}
