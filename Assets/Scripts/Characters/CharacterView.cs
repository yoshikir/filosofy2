using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterView : MonoBehaviour
{
    public Character character;

    public Image backOfTheCard;
    public GameObject finalText;
    public TMP_Text evoBarText;
    public TMP_Text starText;
    public Image evoBar;
    private Image charImage;
    
    private void Start()
    {
        charImage = gameObject.transform.Find("CharImage").GetComponent<Image>();
        charImage.sprite = character.evolutions[character.evolution];
        evoBar.fillAmount = (float)character.experience / 10;
        evoBarText.text = character.experience + "/10";
        backOfTheCard.color = character.colors[character.evolution];
        starText.text = (character.evolution + 1).ToString();
        if (character.evolution == 3)
        {
            finalText.SetActive(true);
        }
        else
        {
            finalText.SetActive(false);
        }
    }

    public void changeExp(int exp)
    {
        character.experience += exp;
        if (character.experience>=10)
        {
            character.experience = 0;
            if (character.evolution<=2)
            {
                character.evolution += 1;
                if (character.evolution==3)
                {
                    finalText.SetActive(true);
                }
            }
        }

        if (charImage != null) charImage.sprite = character.evolutions[character.evolution];
        evoBar.fillAmount = (float)character.experience / 10;
        evoBarText.text = character.experience + "/10";
        backOfTheCard.color = character.colors[character.evolution];
        starText.text = (character.evolution + 1).ToString();
    }
}
