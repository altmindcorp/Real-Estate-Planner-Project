using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorParams
{
    public float lengthX;
    public float lengthY;
    public Vector3 startPosition;

    public FloorParams(float lengthX, float lengthY, Vector3 startPosition)
    {
        this.lengthX = lengthX;
        this.lengthY = lengthY;
        this.startPosition = startPosition;
    }
}
