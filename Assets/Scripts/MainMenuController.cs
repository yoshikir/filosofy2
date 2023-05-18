using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public TMP_Text coins;
    public TMP_Text gems;

    private void Start()
    {
        GameManager.Instance.CoinsChanged += HandleCoinsChanged;
        GameManager.Instance.GemsChanged += HandleGemsChanged;
        coins.text = GameManager.Instance.Coins.ToString();
        gems.text = GameManager.Instance.Gems.ToString();
    }

    private void HandleCoinsChanged(int newcoins)
    {
        coins.text = newcoins.ToString();
    }

    private void HandleGemsChanged(int newGems)
    {
        gems.text = newGems.ToString();
    }

    public void SelectStage(string stage)
    {
        GameManager.Instance.SelectedStage = stage;
    }

    public void LoadStageSelected()
    {
        GameManager.Instance.LoadStageSelected();
    }
}
