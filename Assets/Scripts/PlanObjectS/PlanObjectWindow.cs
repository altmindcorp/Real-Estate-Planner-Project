using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanObjectWindow : PlanObject
{ 
    private float bottomHeight; //in sm
    private float topHeight; 

    public PlanObjectWindow(Vector2[] coords, int id, float bottomHeight, float topHeight)
    {
        this.bottomHeight = bottomHeight;
        this.topHeight = topHeight;
        this.coords = coords;
        this.id = id;
    }

    public float GetBottomHeight()
    {
        return bottomHeight;
    }

    public float GetTopHeight()
    {
        return topHeight;
    }
}
