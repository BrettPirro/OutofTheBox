using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioInstance : AudioWrapper
{
    [SerializeField] AudioClip clip;
    [SerializeField][Range(0,1)] float volume;

    void Start()
    {
        SwapOutAudio(clip, volume);
    }

    
}
