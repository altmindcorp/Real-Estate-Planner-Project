using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanObjectWindow : PlanObject
{ 
    private float bottomHeight; //in sm
    private float topHeight;
    private Vector3 orientation;

    public float GetBottomHeight()
    {
        return bottomHeight;
    }

    public float GetTopHeight()
    {
        return topHeight;
    }

    public Vector3 GetOrientation()
    {
        return orientation;
    }

    public void SetOrientation(Vector3 orientation)
    {
        this.orientation = orientation;
    }
}
