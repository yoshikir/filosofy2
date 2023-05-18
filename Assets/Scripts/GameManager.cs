using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager instance;

    // Getter for singleton instance
    public static GameManager Instance
    {
        get
        {
            // If no instance exists, create one
            if (instance == null)
            {
                instance = new GameObject("GameController").AddComponent<GameManager>();
            }

            return instance;
        }
    }
    // Awake function to set up the singleton
    private void Awake()
    {
        // Check if instance already exists
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    #endregion
    
    public string SelectedStage { get; set; }

    public GameObject selectedCharacter;

    #region Currency
    
    private int coins = 0;
    public delegate void CoinsChangedDelegate(int newCoins);
    public event CoinsChangedDelegate CoinsChanged;
    public int Coins
    {
        get => coins;
        set
        {
            coins = value;
            OnCoinChanged(coins);
        }
    }
    protected virtual void OnCoinChanged(int newCoins){
        PlayerPrefs.SetInt("Coins",newCoins);
        PlayerPrefs.Save();
        CoinsChanged?.Invoke(newCoins);
    }

    private int gems = 0;
    public delegate void GemsChangedDelegate(int newGems);
    public event GemsChangedDelegate GemsChanged;
    public int Gems
    {
        get => gems;
        set
        {
            gems = value;
            OnGemsChanged(gems);
        }
    }
    protected virtual void OnGemsChanged(int newGems)
    {
        PlayerPrefs.SetInt("Gems",newGems);
        PlayerPrefs.Save();
        GemsChanged?.Invoke(newGems);
    }
    
    #endregion
    
    public int lives = 3;
    public int jokers = 0;

    private void Start()
    {
        if(!PlayerPrefs.HasKey("Coins")) PlayerPrefs.SetInt("Coins",0);
        Coins = PlayerPrefs.GetInt("Coins");
        if(!PlayerPrefs.HasKey("Gems")) PlayerPrefs.SetInt("Gems",0);
        Gems = PlayerPrefs.GetInt("Gems");
        PlayerPrefs.Save();
    }

    public void LoadStageSelected()
    {
        LoadSceneAsync(SelectedStage);
    }
    
    private async void LoadSceneAsync(string sceneName)
    {
        // Start loading the scene asynchronously
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // Wait until the scene is fully loaded
        while (!asyncLoad.isDone)
        {
            // Calculate the loading progress as a percentage (0 to 1)
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            
            // Wait for the next frame to continue the loop
            await Task.Yield();
        }
    }

    public void takeLives(int amount)
    {
        lives -= amount;
    }

    public void addLives(int amount)
    {
        lives += amount;
        if (lives > 3) lives = 3;
    }

    public void changeCoin(int amount)
    {
        Coins += amount;
    }

    public void changeGems(int amount)
    {
        Gems += amount;
    }

    public void changeJokers(int amount)
    {
        jokers += amount;
    }
}
