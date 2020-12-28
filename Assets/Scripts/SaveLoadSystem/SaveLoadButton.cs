using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadButton : MonoBehaviour
{
    
    public GameObject panel;

    private void Start()
    {
        if (panel.activeSelf)
        {
            panel.gameObject.SetActive(false);
        }
    }
    public void OpenSaveLoadMenu()
    {
        if (panel.activeSelf)
        {
            panel.gameObject.SetActive(false);
        }
        else
        {
            panel.gameObject.SetActive(true);
        }
    }
}
