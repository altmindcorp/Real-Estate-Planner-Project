using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Plan
{
    public List<GameObject> planGameObjects = new List<GameObject>();
    public int currentID;
    //public List<PlanObject> planObjects = new List<PlanObject>();





    public void RemoveObjectWithID(int id)
    {
        //planObjects.RemoveAll(item => item.id == id);
    }

    public GameObject GetLastPlanObject()
    {
        return planGameObjects[planGameObjects.Count - 1];
    }
}
