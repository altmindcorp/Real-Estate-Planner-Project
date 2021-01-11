using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WindowObjectData : WallChildObjectData
{
    public float positionHeight;

    public WindowObjectData(Mesh mesh, Vector3 position, Vector3 orientation, float height, float positionHeight, float length, int id, int wallID)
    {
        this.vertices = mesh.vertices;
        this.triangles = mesh.triangles;
        this.uvs = mesh.uv;
        
        this.position = position;
        this.positionHeight = positionHeight;
        this.height = height;
        this.orientation = orientation;
        this.length = length;
        this.id = id;

        this.wallID = wallID;
    }
}
