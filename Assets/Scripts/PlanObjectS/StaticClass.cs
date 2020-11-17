using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticClass 
{
    private static Vector3 direction = Vector3.zero;
    public static Plan plan;
    private static float anchorMultiplierZ = 0.002f;
    private static float wallMultiplierZ = 0.001f;
    public static float windowLength = 4;
    
    public static int mode = 0;//0 - create, 1 - delete
    public static void SetCurrentPlan(Plan currentPlan)
    {
        plan = currentPlan;
    }

    public static void DebugInfo()
    {
        foreach (GameObject planObject in plan.planGameObjects)
        {
            /*if (planObject is PlanObjectWindow)
            {
                PlanObjectWindow windowObject = planObject as PlanObjectWindow;
                Debug.Log("Window id: " + windowObject.id + ", bottom height: " + windowObject.GetBottomHeight());
            }

            if (planObject is PlanObjectSimpWall)
            {
                PlanObjectSimpWall simpWallObject = planObject as PlanObjectSimpWall;
                Debug.Log("Wall id: " + simpWallObject.id + ", height: " + simpWallObject.GetHeight());
            }

            if (planObject is PlanObjectDoor)
            {
                PlanObjectDoor doorObject = planObject as PlanObjectDoor;
                Debug.Log("Wall id: " + doorObject.id + ", height: " + doorObject.GettopHeight());
            }*/
        }
    }
    public static Vector3 GetDirection(Vector3[] vertices, Vector3 point)
    {
        //direction = Vector3.zero;
        if (direction == Vector3.zero)
        {
            if (point.x > vertices[3].x-1)
            {
                direction = Vector3.right;
                Debug.Log("Right");
            }
            else if (point.x < vertices[0].x+1)
            {
                direction = Vector3.left;
                Debug.Log("Left");
            }
            else if (point.y > vertices[1].y-1)
            {
                direction = Vector3.up;
                Debug.Log("Up");
            }
            else if (point.y < vertices[0].y+1)
            {
                direction = Vector3.down;
                Debug.Log("Down");
            }
        }
        else if (point.x > vertices[0].x && point.x < vertices[3].x && point.y > vertices[0].y && point.y < vertices[1].y)
        {
            direction = Vector3.zero;
            Debug.LogWarning("Zero");
        }
        return direction;
    }

    public static void UpdateSimpWall(GameObject planObjectWall, Vector3 point, Vector3 direction)
    {
        Vector3[] wallVertices = planObjectWall.GetComponent<MeshFilter>().mesh.vertices;
        var colliderSize = planObjectWall.GetComponent<BoxCollider>().size;
        var colliderCenter = planObjectWall.GetComponent<BoxCollider>().center;
        Mesh mesh = planObjectWall.GetComponent<MeshFilter>().mesh;
        if (direction == Vector3.right)
        {
            if (point.x > wallVertices[3].x)
            {
                wallVertices[3].x++;
                wallVertices[2].x++;
                colliderSize.x++;
                colliderCenter.x++;
            }

            if (point.x < wallVertices[3].x - 1)
            {
                wallVertices[3].x--;
                wallVertices[2].x--;
                colliderSize.x--;
                colliderCenter.x--;
            }
        }

        if (direction == Vector3.left)
        {
            if (point.x < wallVertices[1].x)
            {
                wallVertices[0].x--;
                wallVertices[1].x--;
                colliderSize.x++;
                colliderCenter.x--;
            }

            if (point.x > wallVertices[1].x + 1)
            {
                wallVertices[0].x++;
                wallVertices[1].x++;
                colliderSize.x--;
                colliderCenter.x++;
            }
        }
        if (direction == Vector3.up)
        {
            if (point.y > wallVertices[1].y)
            {
                wallVertices[1].y++;
                wallVertices[2].y++;
                colliderSize.y++;
                colliderCenter.y++;
            }
            if (point.y < wallVertices[1].y - 1)
            {
                wallVertices[1].y--;
                wallVertices[2].y--;
                colliderSize.y--;
                colliderCenter.y--;
            }
        }

        if (direction == Vector3.down)
        {
            if (point.y < wallVertices[0].y)
            {
                wallVertices[0].y--;
                wallVertices[3].y--;
                colliderSize.y++;
                colliderCenter.y--;
            }
            if (point.y > wallVertices[0].y + 1)
            {
                wallVertices[0].y++;
                wallVertices[3].y++;
                colliderSize.y--;
                colliderCenter.y++;
            }
        }

        planObjectWall.GetComponent<PlanObjectSimpWall>().SetDirection(direction);
        planObjectWall.GetComponent<PlanObjectSimpWall>().SetStartPoint();
        mesh.vertices = wallVertices;
        planObjectWall.GetComponent<MeshFilter>().mesh = mesh;
        planObjectWall.GetComponent<BoxCollider>().size = colliderSize;
        planObjectWall.GetComponent<BoxCollider>().center = colliderCenter;
    }

    private static  Vector3[] GetVertices(Vector3 startpoint, Vector3 scale)
    {
        Vector3[] vertices = new Vector3[4];
        vertices[0] = startpoint;
        vertices[1] = new Vector3(startpoint.x, startpoint.y + scale.y * 1);
        vertices[2] = new Vector3(startpoint.x + scale.x * 1, startpoint.y + scale.y * 1);
        vertices[3] = new Vector3(startpoint.x + scale.x * 1, startpoint.y);
        return vertices;
    }

    private static Vector3[] GetVertices(Vector3 startpoint, Vector3 scale, float depth)
    {
        Vector3[] vertices = new Vector3[4];
        vertices[0] = new Vector3(startpoint.x, startpoint.y, depth);
        vertices[1] = new Vector3(startpoint.x, startpoint.y + scale.y * 1, depth);
        vertices[2] = new Vector3(startpoint.x + scale.x * 1, startpoint.y + scale.y * 1, depth);
        vertices[3] = new Vector3(startpoint.x + scale.x * 1, startpoint.y, depth);
        return vertices;
    }

    private static Mesh SetMeshAttributes(Vector3[] vertices)
    {
        Mesh mesh = new Mesh();
        int[] newTriangles = new int[] { 0, 1, 2, 0, 2, 3 };
        Vector2[] newUV = new Vector2[4];
        for (int i = 0; i < 4; i++)
        {
            newUV[i] = new Vector2(vertices[i].x, vertices[i].y);
        }

        mesh.vertices = vertices;
        mesh.uv = newUV;
        mesh.triangles = newTriangles;
        return mesh;
    }

    private static void SetMesh(GameObject obj, Vector3[] vertices, Material material)
    {
        Mesh mesh = SetMeshAttributes(vertices);
        obj.AddComponent<MeshFilter>().mesh = mesh;
        obj.AddComponent<MeshRenderer>().material = material;
        obj.AddComponent<BoxCollider>();
    }

    public static GameObject CreateAnchor(Vector3 startPoint, Vector3 scale, Material material)
    {
        Vector3[] vertices = GetVertices(startPoint, scale);
        GameObject newGameObject = new GameObject("Anchor");
        PlanObjectAnchor anchorPlanObject = newGameObject.AddComponent<PlanObjectAnchor>();
        SetMesh(newGameObject, vertices, material);
        anchorPlanObject.SetTag("PlanObject");
        anchorPlanObject.SetID(plan.currentID);
        anchorPlanObject.SetScale(scale);
        newGameObject.transform.Translate(Vector3.back * anchorMultiplierZ);
        plan.planGameObjects.Add(newGameObject);
        Debug.Log("Create Anchor");
        plan.currentID++;
        return plan.planGameObjects[plan.planGameObjects.Count - 1];
    }

    public static GameObject CreateSimpWall(Vector3 startPoint, Vector3 scale, Material material)
    {
        Vector3[] vertices = GetVertices(startPoint, scale);
        GameObject newGameObject = new GameObject("Simple Wall");
        PlanObjectSimpWall planObjectSimpWall = newGameObject.AddComponent<PlanObjectSimpWall>();
        SetMesh(newGameObject, vertices, material);
        planObjectSimpWall.SetTag("PlanObject");
        planObjectSimpWall.SetID(plan.currentID);
        planObjectSimpWall.SetScale(scale);
        newGameObject.transform.Translate(Vector3.back * wallMultiplierZ);
        //currentPlanObject.SetDirection(direction);
        plan.planGameObjects.Add(newGameObject);
        plan.currentID++;
        return plan.planGameObjects[plan.planGameObjects.Count - 1];
    }

    public static GameObject CreateWindow(GameObject gameObjectWall, Vector3 point, Vector3 scale, Material material)
    {
        Vector3 startPoint = Vector3.zero;
        var planObjectWall = gameObjectWall.GetComponent<PlanObject>() as PlanObjectSimpWall;
        var direction = planObjectWall.GetDirection();
        if (direction == Vector3.left || direction == Vector3.right)
        {
            startPoint = new Vector3((int)point.x, planObjectWall.GetVertices()[0].y, 0);
        }
        else if (direction == Vector3.up || direction == Vector3.down)
        {
            startPoint = new Vector3(planObjectWall.GetVertices()[0].x, (int)point.y, 0);
            var temp = scale.x;
            scale.x = scale.y;
            scale.y = temp;
        }
        GameObject newGameObject = new GameObject("Window" + plan.currentID);
        Vector3[] vertices = GetVertices(startPoint, scale, 0);
        SetMesh(newGameObject, vertices, material);
        var planObjectWindow = newGameObject.AddComponent<PlanObjectWindow>();
        planObjectWindow.SetTag("PlanObject");
        plan.planGameObjects.Add(newGameObject);
        plan.currentID++;
        Debug.Log("Create Window");
        return plan.planGameObjects[plan.planGameObjects.Count - 1];
    }

    public static GameObject CreateWindow(Vector3 startPoint, Vector3 scale, Material material)
    {
        GameObject newGameObject = new GameObject("Window");
        Vector3[] vertices = GetVertices(startPoint, scale);
        SetMesh(newGameObject, vertices, material);
        var planObjectWindow = newGameObject.AddComponent<PlanObjectWindow>();
        planObjectWindow.SetTag("PlanObject");
        newGameObject.transform.Translate(Vector3.back * anchorMultiplierZ);
        plan.planGameObjects.Add(newGameObject);
        plan.currentID++;
        return plan.planGameObjects[plan.planGameObjects.Count - 1];

    }
}
