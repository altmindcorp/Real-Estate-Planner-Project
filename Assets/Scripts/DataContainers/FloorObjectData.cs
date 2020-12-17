using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorObjectData : PlanObjectData
{

    public FloorObjectData(Mesh mesh, Material material, Vector3 position, int id)
    {
        this.mesh = mesh;
        this.material = material;
        this.position = position;
        this.id = id;
    }
}
