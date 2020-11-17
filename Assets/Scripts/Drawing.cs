using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Drawing : MonoBehaviour
{
    private Plan plan = new Plan();
    private Vector2[] coords = new Vector2[4];
    // Start is called before the first frame update

    void Start()
    {
        //GeneratePlan();//default room
        StaticClass.SetCurrentPlan(plan);
    }

    /*private void AddWindow(Plan plan, Vector3[] coords, int id, float bottomHeight, float topHeight)
    {
        PlanObjectWindow windowObject = new PlanObjectWindow(coords, id, bottomHeight, topHeight);
        plan.planObjects.Add(windowObject);
    }

    private void AddSimpWall(Plan plan, Vector3[] coords, int id, float height)
    {
        PlanObjectSimpWall simpWallObject = new PlanObjectSimpWall(coords, id, height);
        plan.planObjects.Add(simpWallObject);
    }

    private void AddDoor(Plan plan, Vector3[] coords, int id, float topHeight)
    {
        PlanObjectDoor doorObject = new PlanObjectDoor(coords, id, topHeight);
        plan.planObjects.Add(doorObject);
    }

    private void GeneratePlan()
    {
        AddSimpWall(plan, GetCoords(0.15f, 0.5f, 0.1f, 0.4f, -0.002f), 0, 3);
        AddWindow(plan, GetCoords(0.5f, 1, 0.1f, 0.4f, -0.002f), 1, 0.9f, 1);
        AddSimpWall(plan, GetCoords(1, 1.5f, 0.1f, 0.4f, -0.002f), 2, 3);
        AddSimpWall(plan, GetCoords(1.2f, 1.5f, -1.5f, 0.1f, -0.002f), 3, 3);
    }

    private Vector3[] GetCoords(float leftX, float rightX, float bottomY, float topY, float z)
    {
        Vector3[] verticesArray = new Vector3[4];
        verticesArray[0] = new Vector3(leftX, topY, z);
        verticesArray[1] = new Vector3(rightX, topY, z);
        verticesArray[2] = new Vector3(rightX, bottomY, z);
        verticesArray[3] = new Vector3(leftX, bottomY, z);
        return verticesArray;
    }

    public void InstantiateObject()
    {
        
    }*/


}
