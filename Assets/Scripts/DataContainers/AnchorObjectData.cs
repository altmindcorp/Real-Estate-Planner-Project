using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorObjectData : PlanObjectData
{
    public AnchorObjectData(Mesh mesh, Material material, Vector3 position, int id)
    {
        this.mesh = mesh;
        this.material = material;
        this.position = position;
        this.id = id;
    }

    public AnchorObjectData(Mesh mesh, Vector3 position, int id)
    {
        this.mesh = mesh;
        this.position = position;
        this.id = id;
    }
}
