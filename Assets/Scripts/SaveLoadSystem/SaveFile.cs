using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveFile
{
    public string name;
    public bool spawnPositionIsSet = false;
    public Vector3 spawnPosition = Vector3.zero;
    public Vector3 cameraPosition;
    public List<PlanObjectData> planObjectsDataList = new List<PlanObjectData>();

    public void AddNewPlanObject(PlanObjectData planObject)
    {
        planObjectsDataList.Add(planObject);
    }


}
