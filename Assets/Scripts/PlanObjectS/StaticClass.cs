using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticClass 
{
    
    private static Vector3 scale = Vector3.zero;
    public static int planeScale = 500;
    private static Vector3 direction = Vector3.zero;
    public static Plan plan;
    //public static int scaleMode = 0;//0 - 0.01f, 1 - 0.1f, 2 - 1.0f
    //public static float scaleValue = 0;//one unit scale

    //z Coord Change
    private static float objectMultiplierZ = 0.003f;
    private static float anchorMultiplierZ = 0.002f;
    private static float wallMultiplierZ = 0.001f;

    //temp window length
    public static int windowLength;
    public static int doorLength;
    //public static int objectTypeMode = 0;
    //public static int createMode = 0;//0 - create, 1 - delete
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

    

    

    public static void UpdateSimpWall(GameObject planObjectWall, Vector3 point, Vector3 direction)
    {
        Vector3[] wallVertices = planObjectWall.GetComponent<MeshFilter>().mesh.vertices;
        Vector2[] uvs = planObjectWall.GetComponent<MeshFilter>().mesh.uv;
        var colliderSize = planObjectWall.GetComponent<BoxCollider>().size;
        var colliderCenter = planObjectWall.GetComponent<BoxCollider>().center;
        Mesh mesh = planObjectWall.GetComponent<MeshFilter>().mesh;
        if (direction == Vector3.right)
        {
            if (point.x > wallVertices[3].x)
            {
                wallVertices[3].x+=0.01f;
                wallVertices[2].x += 0.01f;
                colliderSize.x += 0.01f;
                colliderCenter.x += 0.01f / 2;
                uvs[3].x += 0.01f / (wallVertices[2].y - wallVertices[3].y);
                uvs[2].x += 0.01f / (wallVertices[2].y - wallVertices[3].y);
            }

            if (point.x < wallVertices[3].x - 0.001f)
            {
                wallVertices[3].x -= 0.01f;
                wallVertices[2].x -= 0.01f;
                colliderSize.x -= 0.01f;
                colliderCenter.x -= 0.01f / 2;
                uvs[3].x -= 0.01f / (wallVertices[2].y - wallVertices[3].y);
                uvs[2].x -= 0.01f / (wallVertices[2].y - wallVertices[3].y);
            }
        }

        if (direction == Vector3.left)
        {
            if (point.x < wallVertices[1].x)
            {
                wallVertices[0].x -= 0.01f;
                wallVertices[1].x -= 0.01f;
                colliderSize.x += 0.01f;
                colliderCenter.x -= 0.01f / 2;
                uvs[0].x -= 0.01f / (wallVertices[1].y - wallVertices[0].y);
                uvs[1].x -= 0.01f / (wallVertices[1].y - wallVertices[0].y);
            }

            if (point.x > wallVertices[1].x + 0.01f * 0.1f)
            {
                wallVertices[0].x += 0.01f;
                wallVertices[1].x += 0.01f;
                colliderSize.x -= 0.01f;
                colliderCenter.x += 0.01f / 2;
                uvs[0].x += 0.01f / (wallVertices[1].y - wallVertices[0].y);
                uvs[1].x += 0.01f / (wallVertices[1].y - wallVertices[0].y);
            }
        }
        if (direction == Vector3.up)
        {
            if (point.y > wallVertices[1].y)
            {
                wallVertices[1].y += 0.01f;
                wallVertices[2].y += 0.01f;
                colliderSize.y += 0.01f;
                colliderCenter.y += 0.01f / 2;
                uvs[1].y += 0.01f / (wallVertices[2].x - wallVertices[1].x);
                uvs[2].y += 0.01f / (wallVertices[2].x - wallVertices[1].x);
            }
            if (point.y < wallVertices[1].y - 0.01f * 0.1f)
            {
                wallVertices[1].y -= 0.01f;
                wallVertices[2].y -= 0.01f;
                colliderSize.y -= 0.01f;
                colliderCenter.y -= 0.01f / 2;
                uvs[1].y -= 0.01f / (wallVertices[2].x - wallVertices[1].x);
                uvs[2].y -= 0.01f / (wallVertices[2].x - wallVertices[1].x);
            }
        }

        if (direction == Vector3.down)
        {
            if (point.y < wallVertices[0].y)
            {
                wallVertices[0].y -= 0.01f;
                wallVertices[3].y -= 0.01f;
                colliderSize.y += 0.01f;
                colliderCenter.y -= 0.01f / 2;
                uvs[0].y += 0.01f / (wallVertices[0].x - wallVertices[3].x);
                uvs[3].y += 0.01f / (wallVertices[0].x - wallVertices[3].x);
            }
            if (point.y > wallVertices[0].y + 0.01f * 0.1f)
            {
                wallVertices[0].y += 0.01f;
                wallVertices[3].y += 0.01f;
                colliderSize.y -= 0.01f;
                colliderCenter.y += 0.01f / 2;
                uvs[0].y -= 0.01f / (wallVertices[0].x - wallVertices[3].x);
                uvs[3].y -= 0.01f / (wallVertices[0].x - wallVertices[3].x);
            }
        }

        planObjectWall.GetComponent<PlanObjectSimpWall>().SetDirection(direction);
        planObjectWall.GetComponent<PlanObjectSimpWall>().SetStartPoint();
        mesh.vertices = wallVertices;
        mesh.uv = uvs;
        planObjectWall.GetComponent<MeshFilter>().mesh = mesh;
        planObjectWall.GetComponent<MeshFilter>().mesh.RecalculateBounds();
        planObjectWall.GetComponent<BoxCollider>().size = colliderSize;
        planObjectWall.GetComponent<BoxCollider>().center = colliderCenter;
    }

    /*private static  Vector3[] GetVertices(Vector3 startpoint, Vector3 scale)
    {
        Vector3[] vertices = new Vector3[4];
        vertices[0] = startpoint;
        vertices[1] = new Vector3(startpoint.x, startpoint.y + scale.y * 0.01f);
        vertices[2] = new Vector3(startpoint.x + scale.x * 0.01f, startpoint.y + scale.y * 0.01f);
        vertices[3] = new Vector3(startpoint.x + scale.x * 0.01f, startpoint.y);
        return vertices;
    }*/

    public static void Undo()
    {
        Object.Destroy(plan.planGameObjects[plan.planGameObjects.Count - 1]);
        plan.planGameObjects.RemoveAt(plan.planGameObjects.Count - 1);
    }

    public static GameObject CreateAnchor(Vector3 startPoint, Vector3 scale, Material material, TMPro.TMP_Text hintText)
    {
        Vector3[] vertices = GetVertices(startPoint, scale, 0);
        GameObject newGameObject = new GameObject("Anchor");
        //TMPro.TMP_Text textObject = GameObject.Instantiate(hintText, new Vector3(startPoint.x + scale.x * GridScaler.scaleValue / 2, startPoint.y + scale.y * GridScaler.scaleValue / 2, -0.0001f), Quaternion.identity, newGameObject.transform);
        //textObject.text = scale.ToString();
        //textObject.transform.localScale -= new Vector3(1 - GridScaler.scaleValue * 0.2f, 1 - GridScaler.scaleValue * 0.2f);
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
        Vector3[] vertices = GetVertices(startPoint, scale, 0);
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

    /*public static GameObject CreateWindow(GameObject gameObjectWall, Vector3 point, Vector3 scale, Material material)
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
    }*/

    public static GameObject CreateWindow(Vector3 startPoint, Vector3 scale, Material material)
    {
        GameObject newGameObject = new GameObject("Window");
        Vector3[] vertices = GetVertices(startPoint, scale, 0);
        SetMesh(newGameObject, vertices, material);
        var planObjectWindow = newGameObject.AddComponent<PlanObjectWindow>();
        planObjectWindow.SetTag("PlanObject");
        newGameObject.transform.Translate(Vector3.back * anchorMultiplierZ);
        plan.planGameObjects.Add(newGameObject);
        plan.currentID++;
        return plan.planGameObjects[plan.planGameObjects.Count - 1];

    }

    public static GameObject CreateDoor(Vector3 startPoint, Vector3 scale, Material material)
    {
        GameObject newGameObject = new GameObject("Door");
        Vector3[] vertices = GetVertices(startPoint, scale, 0);
        SetMesh(newGameObject, vertices, material);
        var planObjectWindow = newGameObject.AddComponent<PlanObjectDoor>();
        planObjectWindow.SetTag("PlanObject");
        newGameObject.transform.Translate(Vector3.back * anchorMultiplierZ);
        plan.planGameObjects.Add(newGameObject);
        plan.currentID++;
        return plan.planGameObjects[plan.planGameObjects.Count - 1];
    }

    private static Vector3[] GetVertices(Vector3 startpoint, Vector3 scale, float depth)
    {
        Vector3[] vertices = new Vector3[4];
        vertices[0] = new Vector3(startpoint.x, startpoint.y, depth);
        vertices[1] = new Vector3(startpoint.x, startpoint.y + scale.y * 0.01f, depth);
        vertices[2] = new Vector3(startpoint.x + scale.x * 0.01f, startpoint.y + scale.y * 0.01f, depth);
        vertices[3] = new Vector3(startpoint.x + scale.x * 0.01f, startpoint.y, depth);
        return vertices;
    }

    private static Mesh SetMeshAttributes(Vector3[] vertices)
    {
        Mesh mesh = new Mesh();
        int[] newTriangles = new int[] { 0, 1, 2, 0, 2, 3 };
        Vector2[] newUV = new Vector2[4];
        newUV[0] = new Vector2(0, 0);
        newUV[1] = new Vector2(0, 1);
        newUV[2] = new Vector2(1, 1);
        newUV[3] = new Vector2(1, 0);
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
    public static Vector3 GetDirection(Vector3[] vertices, Vector3 point)
    {
        //direction = Vector3.zero;
        if (direction == Vector3.zero)
        {
            if (point.x > vertices[3].x - GridScaler.scaleValue * 0.1f)
            {
                direction = Vector3.right;
                Debug.Log("Right");
            }
            else if (point.x < vertices[0].x + GridScaler.scaleValue * 0.1f)
            {
                direction = Vector3.left;
                Debug.Log("Left");
            }
            else if (point.y > vertices[1].y - GridScaler.scaleValue * 0.1f)
            {
                direction = Vector3.up;
                Debug.Log("Up");
            }
            else if (point.y < vertices[0].y + GridScaler.scaleValue * 0.1f)
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

    public static void ChangeScaleX(int scaleX)
    {
        scale.x = scaleX;
    }

    public static void ChangeScaleY(int scaleY)
    {
        scale.y = scaleY;
    }

    public static Vector3 GetScale()
    {
        return scale;
    }
}
