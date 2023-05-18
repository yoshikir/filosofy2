using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleMainMenus : MonoBehaviour
{
    private Toggle _toggle;
    public GameObject objeto;

    private void Start()
    {
        _toggle = gameObject.GetComponent<Toggle>();
    }

    public void Toggle(){
        if (_toggle.isOn)
        {
            objeto.SetActive(true);
        }
        else
        {
            objeto.SetActive(false);
        }
    }

    public void ClearToggled()
    {
        if (!_toggle.isOn) return;
        _toggle.SetIsOnWithoutNotify(false);
        objeto.SetActive(false);
    }
}
