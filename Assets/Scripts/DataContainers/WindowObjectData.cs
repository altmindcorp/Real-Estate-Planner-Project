using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowObjectData : WallChildObjectData
{
    public float positionHeight;

    public WindowObjectData(Mesh mesh, Vector3 position, Vector3 orientation, float height, float positionHeight, float length, int id)
    {
        this.mesh = mesh;
        this.position = position;
        this.positionHeight = positionHeight;
        this.height = height;
        this.orientation = orientation;
        this.length = length;
        this.id = id;
    }
}
