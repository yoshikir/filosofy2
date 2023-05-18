using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Sound
{
    #region Vars

    ///////////// Information /////////////
    [Header("Audio Information")]
    
    public AudioName name;
    
    ///////////// Audio References /////////////
    [Header("Audio References")]
    
    public AudioClip clip;
    [HideInInspector] public AudioSource source;
    
    ///////////// Audio Properties /////////////
    [Header("Audio Properties")]

    [Range(0f, 1f)] public float volume;
    [Range(.1f, 3f)] public float pitch;
    public bool loopable;
    

    #endregion
}

public enum AudioName
{
    //SFX//
    PressButton,
    CancelButton,
    BuyShop,
    MainBattleSong,
    OverMenuSong,
    LetsGo,
    GoodChoice,
    BadChoice
}
