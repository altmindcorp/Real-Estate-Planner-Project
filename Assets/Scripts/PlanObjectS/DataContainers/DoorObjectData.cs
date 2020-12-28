using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DoorObjectData : WallChildObjectData
{
    
    public DoorObjectData(Vector3 position)
    {
        this.position = position;
    }

    public DoorObjectData(Mesh mesh, Vector3 position, Vector3 orientation, float length, int id)
    {
        //this.mesh = mesh;
        this.vertices = mesh.vertices;
        this.uvs = mesh.uv;
        this.triangles = mesh.triangles;
        this.position = position;
        this.orientation = orientation;
        this.length = length;
        this.id = id;
    }
}
