using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCard : MonoBehaviour
{
    public GameObject card;

    public void Select()
    {
        GameManager.Instance.selectedCharacter = card;
    }
}
