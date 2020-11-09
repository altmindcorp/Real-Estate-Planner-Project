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
        GeneratePlan();//default room
        StaticClass.SetCurrentPlan(plan);
    }

    private void AddWindow(Plan plan, Vector2[] coords, int id, float bottomHeight, float topHeight)
    {
        PlanObjectWindow windowObject = new PlanObjectWindow(coords, id, bottomHeight, topHeight);
        plan.planObjects.Add(windowObject);
    }

    private void AddSimpWall(Plan plan, Vector2[] coords, int id, float height)
    {
        PlanObjectSimpWall simpWallObject = new PlanObjectSimpWall(coords, id, height);
        plan.planObjects.Add(simpWallObject);
    }

    private void AddDoor(Plan plan, Vector2[] coords, int id, float topHeight)
    {
        PlanObjectDoor doorObject = new PlanObjectDoor(coords, id, topHeight);
        plan.planObjects.Add(doorObject);
    }

    private void GeneratePlan()
    {
        AddSimpWall(plan, GetCoords(0.15f, 0.5f, 0.1f, 0.4f), 0, 3);
        AddWindow(plan, GetCoords(0.5f, 1, 0.1f, 0.4f), 1, 0.9f, 1);
        AddSimpWall(plan, GetCoords(1, 1.5f, 0.1f, 0.4f), 2, 3);
        AddSimpWall(plan, GetCoords(1.2f, 1.5f, -1.5f, 0.1f), 3, 3);
    }

    private Vector2[] GetCoords(float leftX, float rightX, float bottomY, float topY)
    {
        Vector2[] verticesArray = new Vector2[4];
        verticesArray[0] = new Vector2(leftX, topY);
        verticesArray[1] = new Vector2(rightX, topY);
        verticesArray[2] = new Vector2(rightX, bottomY);
        verticesArray[3] = new Vector2(leftX, bottomY);
        return verticesArray;
    }

    public void GenerateMeshes()
    {
        SceneManager.LoadScene("3DEditorScene");
        //Generate meshes function
    }


}
