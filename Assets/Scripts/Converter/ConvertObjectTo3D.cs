using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ConvertObjectTo3D
{
    public static PlanObject planObject;
    public static List<PlanObjectWindow> planObjectWindowsList = new List<PlanObjectWindow>();
    public static List<PlanObjectSimpWall> planObjectSimpWallList = new List<PlanObjectSimpWall>();
    //public static List<Vector3[]> wallVerticesList = new List<Vector3[]>();
    //public static List<Vector3[]> windowVerticesList = new List<Vector3[]>();

    public static void GetPlanObjects()
    {
        foreach (GameObject planGameObject in StaticClass.plan.planGameObjects)
        {
            planObject = planGameObject.GetComponent<PlanObject>();
            if (planObject is PlanObjectSimpWall)
            {
                planObjectSimpWallList.Add(planObject as PlanObjectSimpWall);
            }
        }
    }


    /*public static void ConvertWallsTo3D()//button
    {
        Debug.Log("Plan Game Objects: " + StaticClass.plan.planGameObjects.Count);
        foreach (Vector3[] vertices in ConvertObjectTo3D.wallVerticesList)
        {
            Mesh newMesh = ConvertObjectTo3D.GetSimpleWallMesh(vertices, 0.2f);
            GameObject newObject = new GameObject();

            newObject.AddComponent<MeshRenderer>().material = wallMaterial;
            newObject.AddComponent<MeshFilter>().mesh = newMesh;
            Debug.Log("Created object");
        }

    }*/

    public static Mesh CreateSimpleWallGameObject(Vector3[] vertices, float height)
    {
        Vector3[] changedVertices = ChangeDimension(vertices);
        Vector3[] newVertices = new Vector3[8];
        for (int i =0; i<4; i++)
        {
            newVertices[i] = changedVertices[i];
            newVertices[i + 4] = changedVertices[i] + Vector3.up * height;
        }

        int[] newTriangles = Get3DTriangles();
        Vector2[] newUVs = GetUVS(newVertices);

        Mesh newMesh = new Mesh();
        newMesh.vertices = newVertices;
        newMesh.triangles = newTriangles;
        newMesh.uv = newUVs;

        return newMesh;
    }

    public static Mesh CreateWindowGameObject(Vector3[] vertices, float positionY, float height)
    {
        Vector3[] changedVertices = ChangeDimension(vertices);
        Vector3[] newVertices = new Vector3[8];
        for (int i = 0; i<4; i++)
        {
            newVertices[i] = changedVertices[i] + Vector3.up * positionY;
            newVertices[i + 4] = changedVertices[i] + Vector3.up * (positionY + height);
        }

        int[] newTriangles = Get3DTriangles();
        Vector2[] newUVs = GetUVS(newVertices);

        Mesh newMesh = new Mesh();
        newMesh.vertices = newVertices;
        newMesh.triangles = newTriangles;
        newMesh.uv = newUVs;

        return newMesh;
    }

    public static Mesh CreateDoorGameObject(Vector3[] vertices, float height)
    {
        Vector3[] changedVertices = ChangeDimension(vertices);
        Vector3[] newVertices = new Vector3[8];
        /*for (int i = 0; i < 4; i++)
        {
            newVertices[i] = changedVertices[i] + Vector3.up * positionY;
            newVertices[i + 4] = changedVertices[i] + Vector3.up * (positionY + height);
        }*/

        int[] newTriangles = Get3DTriangles();
        Vector2[] newUVs = GetUVS(newVertices);

        Mesh newMesh = new Mesh();
        newMesh.vertices = newVertices;
        newMesh.triangles = newTriangles;
        newMesh.uv = newUVs;

        return newMesh;
    }

    public static Mesh CreateLowWallObject(Vector3[] vertices, float height)
    {
        Vector3[] changedVertices = ChangeDimension(vertices);
        Vector3[] newVertices = new Vector3[8];
        for (int i = 0; i < 4; i++)
        {
            newVertices[i] = changedVertices[i];
            newVertices[i + 4] = changedVertices[i] + Vector3.up * height;
        }

        int[] newTriangles = Get3DTriangles();
        Vector2[] newUVs = GetUVS(newVertices);

        Mesh newMesh = new Mesh();
        newMesh.vertices = newVertices;
        newMesh.triangles = newTriangles;
        newMesh.uv = newUVs;

        return newMesh;
    }

    public static Mesh CreateHighWallObject(Vector3[] vertices, float positionY, float height)
    {
        Vector3[] changedVertices = ChangeDimension(vertices);
        Vector3[] newVertices = new Vector3[8];
        for (int i = 0; i < 4; i++)
        {
            newVertices[i] = changedVertices[i] + Vector3.up * positionY;
            newVertices[i + 4] = changedVertices[i] + Vector3.up * height;
        }

        int[] newTriangles = Get3DTriangles();
        Vector2[] newUVs = GetUVS(newVertices);

        Mesh newMesh = new Mesh();
        newMesh.vertices = newVertices;
        newMesh.triangles = newTriangles;
        newMesh.uv = newUVs;

        

        return newMesh;
    }

    /*public static Mesh[] GetWindowMeshes()
    {
        Mesh[] meshes = new Mesh[3];

    }*/

    private static int[] Get3DTriangles()
    {
        List<int> triangles = new List<int>();

        for (int i = 0; i < 3; i++)
        {
            triangles.Add(0 + i);
            triangles.Add(1 + i);
            triangles.Add(5 + i);
            triangles.Add(0 + i);
            triangles.Add(5 + i);
            triangles.Add(4 + i);
        }

        triangles.Add(3);
        triangles.Add(0);
        triangles.Add(4);
        triangles.Add(3);
        triangles.Add(4);
        triangles.Add(7);

        triangles.Add(0);
        triangles.Add(2);
        triangles.Add(1);

        triangles.Add(0);
        triangles.Add(3);
        triangles.Add(2);

        triangles.Add(4);
        triangles.Add(5);
        triangles.Add(6);

        triangles.Add(4);
        triangles.Add(6);
        triangles.Add(7);

        return triangles.ToArray();
    }

    private static Vector2[] GetUVS(Vector3[] meshVertices)
    {
        Vector2[] uvs = new Vector2[8];
        for (int i =0; i<8; i++)
        {
            uvs[i].x = meshVertices[i].x;
            uvs[i].y = meshVertices[i].y;
        }
        return uvs;
    }

    private static Vector3[] ChangeDimension(Vector3[] vertices)
    {
        Vector3[] newVertices = new Vector3[4];

        for (int i = 0; i< 4; i++)
        {
            newVertices[i].z = vertices[i].y;
            newVertices[i].y = 0;
            newVertices[i].x = vertices[i].x;
        }

        return newVertices;
    }

}
