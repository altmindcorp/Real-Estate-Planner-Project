using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class ObjectsDataRepository
{
    public static int currentID = 0;
    public static List<PlanObjectData> planObjectsDataList = new List<PlanObjectData>();

    public static void ChangeMesh(Mesh mesh, int id)
    {
        planObjectsDataList[id].mesh = mesh;
    }

    
}
