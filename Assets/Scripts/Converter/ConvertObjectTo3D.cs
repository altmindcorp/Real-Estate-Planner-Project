using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class ConvertObjectTo3D
{
    public static PlanObject planObject;
    public static Floor floorObject;
    public static List<PlanObjectWindow> planObjectWindowsList = new List<PlanObjectWindow>();
    public static List<PlanObjectSimpWall> planObjectSimpWallList = new List<PlanObjectSimpWall>();
    public static List<Floor> floorObjectsList = new List<Floor>();
    //public static List<Vector3[]> wallVerticesList = new List<Vector3[]>();
    //public static List<Vector3[]> windowVerticesList = new List<Vector3[]>();

    /*public static void GetPlanObjects()
    {
        foreach (GameObject planGameObject in StaticClass.plan.planGameObjects)
        {
            if (planGameObject.GetComponent<PlanObject>()!= null)
            {
                planObject = planGameObject.GetComponent<PlanObject>();
                if (planObject is PlanObjectSimpWall)
                {
                    planObjectSimpWallList.Add(planObject as PlanObjectSimpWall);
                }
            }

            else if (planGameObject.GetComponent<Floor>() != null)
            {
                floorObject = planGameObject.GetComponent<Floor>();
                floorObjectsList.Add(floorObject);
            }
        }
    }


    public static void ConvertWallsTo3D()//button
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

    public static Mesh CreateFloorGameObject(Mesh mesh)
    {
        Vector3[] changedVertices = ChangeDimension(mesh.vertices);
        Mesh newMesh = new Mesh();
        newMesh.vertices = changedVertices;
        newMesh.triangles = mesh.triangles;
        newMesh.uv = mesh.uv;

        return newMesh;
        
    }


    /*public static Mesh CreateFloorGameObject(Vector3[] vertices)
    {
        Vector3[] changedVertices = ChangeDimension(vertices);
        Mesh newMesh = new Mesh();
        newMesh.vertices = changedVertices;
        newMesh.triangles = mesh.triangles;
        newMesh.uv = mesh.uv;

        return newMesh;

    }*/

    public static Mesh CreateCeiling(Mesh mesh)
    {
        Vector3[] changedVertices = ChangeDimension(mesh.vertices);
        Mesh newMesh = new Mesh();
        newMesh.vertices = changedVertices;
        newMesh.triangles = mesh.triangles.Reverse().ToArray();
        newMesh.uv = mesh.uv;

        return newMesh;
    }
    /*public static Mesh CreateSimpleWall3DObject(Vector3[] vertices, float height)
    {
        Vector3[] changedVertices = ChangeDimension(vertices);
        Vector3 newVertices = new 
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

    public static Mesh CreateSimpleWallGameObject(Mesh mesh, float height)
    {
        Vector3[] changedVertices = ChangeDimension(mesh.vertices);
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
        float lengthX = meshVertices[3].x - meshVertices[0].x;
        float lengthY = meshVertices[4].y - meshVertices[0].y;
        float lengthZ = meshVertices[1].z - meshVertices[0].z;
        //Debug.Log("Length: " + lengthX + " " + lengthY + " " + lengthZ);

        Vector2[] uvs = new Vector2[8];

        uvs[0] = new Vector3(0, 0);
        uvs[1] = new Vector3(lengthZ / 3 , 0);
        uvs[2] = new Vector3((lengthX + lengthZ)/3, 0);
        uvs[3] = new Vector3(lengthX / 3 , 0);

        uvs[4] = new Vector3(0, lengthY);
        uvs[5] = new Vector3(lengthZ / 3, lengthY);
        uvs[6] = new Vector3((lengthX + lengthZ)/3, lengthY);
        uvs[7] = new Vector3(lengthX / 3 , lengthY);

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
