using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeTextName : MonoBehaviour
{
    public GettingName GettingName;
    // Start is called before the first frame update
    void Start()
    {
        switch (GettingName)
        {
            case GettingName.Stage:
                GetComponent<TMP_Text>().text = GameManager.Instance.SelectedStage;
                break;
            case GettingName.CharName:
                GetComponent<TMP_Text>().text = GameManager.Instance.selectedCharacter.name;
                break;
            case GettingName.UserName:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public enum GettingName
{
    Stage,
    CharName,
    UserName,
}
