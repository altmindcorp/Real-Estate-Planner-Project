using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class ObjectsDataRepository
{
    public static Vector3 spawnPointPosition = Vector3.zero;
    public static int currentID = 0;
    public static List<PlanObjectData> planObjectsDataList = new List<PlanObjectData>();
    public static GameObject PlayerContainer;

    public static void ChangeMesh(Mesh mesh, int id)
    {
        if (planObjectsDataList.Find(x => x.id == id).mesh = mesh)
        {

        }
        else
        {
            Debug.LogError("No object with ID");
        }    
    }

    static void Update()
    {
        PlayerContainer.transform.position = new Vector3(spawnPointPosition.x, 1, spawnPointPosition.y);


        GameObject.CreatePrimitive(PrimitiveType.Cube);
    }
}
