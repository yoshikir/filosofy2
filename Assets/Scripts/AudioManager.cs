using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    #region Vars

    ///////////// SINGLETON /////////////
    private static AudioManager _instance;

    ///////////// SOUNDS /////////////
    public Sound[] sounds;

    #endregion

    #region Properties

    public static AudioManager Instance
    {
        get
        {
            // Class instantiated
            if (_instance != null) return _instance;

            _instance = FindObjectOfType<AudioManager>();

            // Found an existing instance
            if (_instance != null) return _instance;

            // Create the instance
            GameObject obj = new GameObject
            {
                name = "@" + nameof(AudioManager)
            };
            _instance = obj.AddComponent<AudioManager>();

            return _instance;
        }
    }

    #endregion

    #region UnityCB

    private void Awake()
    {
        // Singleton pattern
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        InitializeSourceSound();
    }

    #endregion

    #region Initializing Methods

    /// <summary>
    /// Initialize sound array
    /// </summary>
    private void InitializeSourceSound()
    {
        foreach (var s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;

            s.volume = 1;
            s.source.volume = s.volume;
            var pitch = s.pitch <= 0.1f
                ? 1.0f
                : s.pitch;
            s.pitch = pitch;
            s.source.pitch = pitch;
            s.source.loop = s.loopable;
        }
    }
    

    #endregion

    #region Media Methods

    public void Play(AudioName audioName)
    {
        Sound s = Array.Find(
            sounds,
            sound => sound.name.Equals(audioName)
        );

        StopCurrent(s);

        s.source.Play();
    }

    public void PlayWithNoStop(AudioName audioName)
    {
        Sound s = Array.Find(
            sounds,
            sound => sound.name.Equals(audioName)
        );

        s?.source.Play();
    }

    public void StopCurrent(Sound soundType)
    {
        Sound s = Array.Find(
            sounds,
            sound => sound.source.isPlaying && sound == soundType
        );

        s?.source.Stop();
    }

    #endregion

    #region Sound Methods
    public void PlayPressButton()
    {
        AudioManager.Instance.PlayWithNoStop(AudioName.PressButton);
    }
    
    public void PlayBuyShop()
    {
        AudioManager.Instance.PlayWithNoStop(AudioName.BuyShop);
    }
    
    public void PlayCancelButton()
    {
        AudioManager.Instance.PlayWithNoStop(AudioName.CancelButton);
    }
    
    public void PlayMainBattleSong()
    {
        AudioManager.Instance.PlayWithNoStop(AudioName.MainBattleSong);
    }
    
    public void PlayOverMenuSong()
    {
        AudioManager.Instance.PlayWithNoStop(AudioName.OverMenuSong);
    }

    public void PlayLetsGo()
    {
        AudioManager.Instance.PlayWithNoStop(AudioName.LetsGo);
    }
    
    public void PlayGoodChoice()
    {
        AudioManager.Instance.PlayWithNoStop(AudioName.GoodChoice);
    }
    public void PlayBadChoice()
    {
        AudioManager.Instance.PlayWithNoStop(AudioName.BadChoice);
    }

    #endregion
}