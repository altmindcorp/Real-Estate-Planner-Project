using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallObjectData : PlanObjectData
{
    public Vector3 orientation;
    public float height;
    public List<WallChildObjectData> wallChildObjectsDataList = new List<WallChildObjectData>();

    public WallObjectData(Mesh mesh, Material material, Vector3 position, int id)
    {
        this.mesh = mesh;
        this.material = material;
        this.position = position;
        this.id = id;
    }

    public WallObjectData(Mesh mesh, Vector3 position, int id, float height)
    {
        this.mesh = mesh;
        this.position = position;
        this.id = id;
        this.height = height;
    }


}
