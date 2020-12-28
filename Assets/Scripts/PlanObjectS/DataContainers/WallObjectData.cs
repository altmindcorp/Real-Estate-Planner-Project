using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WallObjectData : PlanObjectData
{
    public Vector2 maxSpawnBounds;
    public Vector2 minSpawnBounds;
    public Vector3 orientation;
    public float height;
    public List<int> wallChildsIdList = new List<int>();

    public WallObjectData(Mesh mesh, Vector3 position, Vector3 orientation, Vector3 minSpawnBounds, Vector3 maxSpawnBounds, float height, int id)
    {
        this.triangles = mesh.triangles;
        this.uvs = mesh.uv;
        this.vertices = mesh.vertices;

        this.position = position;
        this.orientation = orientation;
        this.maxSpawnBounds = maxSpawnBounds;
        this.minSpawnBounds = minSpawnBounds;

        this.height = height;
        this.id = id;
        
    }

    public void ChangeWallOrientation(Vector3 orientation)
    {
        this.orientation = orientation;
    }

    public void AddWallChildId(int id)
    {
        wallChildsIdList.Add(id);
    }

    public void RemoveWallChildId(int id)
    {
        wallChildsIdList.RemoveAll(x => x == id);
    }
}
