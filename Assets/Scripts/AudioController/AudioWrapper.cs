using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioWrapper:MonoBehaviour
{
    public delegate void ChangeAudio(AudioClip audio,float vol);
    public static event ChangeAudio SwitchAudio;

    public void SwapOutAudio(AudioClip audio, float Vol) 
    {
        SwitchAudio?.Invoke(audio,Vol);

    }

}
