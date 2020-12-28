using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadController : MonoBehaviour
{
    public void SaveFile()
    {
        ObjectsDataRepository.SaveCurrentFile("testsave");
    }

    public void LoadFile()
    {
        if (ObjectsDataRepository.LoadSaveFile("testsave"))
        {
            Debug.Log("Data list position count: " + ObjectsDataRepository.currentSaveFile.planObjectsDataList.Count);
            Debug.Log("Loaded");
        }

        else
        {
            Debug.LogError("Not loaded");
        }
    }
}
