using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AnchorObjectData : PlanObjectData
{
    public Vector3 scale;
    public AnchorObjectData(Mesh mesh, Vector3 position, Vector3 scale, int id)
    {
        //this.mesh = mesh;
        //this.material = material;
        this.scale = scale;
        this.vertices = mesh.vertices;
        this.uvs = mesh.uv;
        this.triangles = mesh.triangles;
        this.position = position;
        this.id = id;
    }
}
